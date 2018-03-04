using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using App.Models;
using App.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace App.Controllers.Parking
{
    /// <summary>
    ///     The ParkingRatesController class.
    ///     <para>Contains the methods to return a view of parking rates based on the content request.</para>
    /// </summary>
    /// <remarks>Inherits from <see cref="BaseController" /></remarks>
    [Produces("application/json", "application/xml"), Route("api/{version}/parking")]
    public class ParkingRatesController : BaseController
    {
        private IParkingRateService _parkingRateService;

        /// <summary>
        ///     The ParkingRatesController constructor.
        /// </summary>
        /// <param name="parkingRateService">A parking rate service.</param>
        public ParkingRatesController(IParkingRateService parkingRateService) : base()
        {
            _parkingRateService = parkingRateService;
        }

        /// <summary>
        ///     The Get method.
        ///     <para>
        ///         HttpGet endpoint to retrieve parking rate information. Expects ISO 8601 start and end query string parameters.
        ///         Formats the response based on the Accept header or using a format filter, i.e. rates.json or rates.xml.
        ///     </para>
        /// </summary>
        /// <param name="start">An ISO 8601 start date and time.</param>
        /// <param name="end">An ISO 8601 end date and time.</param>
        /// <returns>Returns an IActionResult as either json or xml with the requested parking rate information, a bad request if the parameters are invalid, or not found.</returns>
        [HttpGet("rates")]
        [HttpGet("rates.{format}"), FormatFilter]
        public IActionResult Get(
            [FromQuery, Required(ErrorMessage = "Start query string parameter is required.")] string start,
            [FromQuery, Required(ErrorMessage = "End query string parameter is required.")] string end)
        {
            var UTC8601Formats = new[] { "yyyyMMddTHH:mm:ssZ", "yyyy-MM-ddTHH:mm:ssZ" };

            if (string.IsNullOrWhiteSpace(start) || string.IsNullOrWhiteSpace(end))
            {
                return BadRequest(GetModelStateErrors(ModelState));
            }

            if (!DateTimeOffset.TryParseExact(start, UTC8601Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var startUTC8601))
            {
                ModelState.AddModelError("start", "Start query string parameter is not in ISO 8601 format.");
                return BadRequest(GetModelStateErrors(ModelState));
            }

            if (!DateTimeOffset.TryParseExact(end, UTC8601Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var endUTC8601))
            {
                ModelState.AddModelError("end", "End query string parameter is not in ISO 8601 format.");
                return BadRequest(GetModelStateErrors(ModelState));
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("controller", "Unavailable");
                return BadRequest(GetModelStateErrors(ModelState));
            }

            var price = _parkingRateService.GetPrice(startUTC8601, endUTC8601);
            if (price == null)
            {
                ModelState.AddModelError("controller", "Unavailable");
                return NotFound(GetModelStateErrors(ModelState));
            }

            return Ok(new ParkingRateViewModel
            {
                Price = $"{Convert.ToInt32(Math.Round(price.Value))}"
            });
        }

        private string[] GetModelStateErrors(ModelStateDictionary state)
        {
            return state.Values.SelectMany(s => s.Errors).Select(e => e.ErrorMessage).ToArray();
        }
    }
}
