using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using App.Models;
using App.Services;
using App.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace App.Controllers.Parking
{
    /// <summary>
    ///     ParkingRatesController
    ///     <para>Contains the methods to return a view of parking rates based on the accept header.</para>
    /// </summary>
    /// <remarks>Inherits from <see cref="BaseController" /></remarks>
    [Route("api/{version}/parking")]
    public class ParkingRatesController : BaseController
    {
        private IParkingRateService _parkingRateService;

        /// <summary>
        ///     ParkingRatesController constructor
        /// </summary>
        /// <param name="parkingRateService">Parking rate service.</param>
        public ParkingRatesController(IParkingRateService parkingRateService) : base()
        {
            _parkingRateService = parkingRateService;
        }

        /// <summary>Get parking rates based on a start and end ISO 8601 date time formats.</summary>
        /// <param name="start">Required start date time in ISO 8601 format.</param>
        /// <param name="end">Required end date time in ISO 8601 format.</param>
        /// <returns>Returns an IActionResult. </returns>
        /// <response code="200">Response code of 200 returns the parking price. </response>
        /// <response code="400">Response code of 400 returns Bad Request with error messages if there are errors in the query. </response>
        /// <response code="404">Response code of 404 unavailable returns if there is no data available. </response>
        [HttpGet("rates")]
        [HttpGet("rates.{format}"), FormatFilter]
        [Produces("application/json", "application/xml", "application/x-protobuf")]
        [ProducesResponseType(typeof(ParkingRate), 200)]
        [ProducesResponseType(typeof(Message), 400)]
        [ProducesResponseType(typeof(Message), 404)]
        public IActionResult Get(
            [FromQuery, Required(ErrorMessage = "Start query string parameter is required.")] string start,
            [FromQuery, Required(ErrorMessage = "End query string parameter is required.")] string end)
        {
            var UTC8601Formats = DateTimeFormatUtility.ISO8601AcceptedFormats;
            if (string.IsNullOrWhiteSpace(start) || string.IsNullOrWhiteSpace(end))
            {
                ModelState.AddModelError("controller", "Unavailable");
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

            if (DateTime.Compare(startUTC8601.Date, endUTC8601.Date) != 0)
            {
                ModelState.AddModelError("end", "Start and end query string parameter are not on the same day.");
                return BadRequest(GetModelStateErrors(ModelState));
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("controller", "Unavailable");
                return BadRequest(GetModelStateErrors(ModelState));
            }

            var price = _parkingRateService.GetPrice(startUTC8601.DayOfWeek, startUTC8601.TimeOfDay, endUTC8601.TimeOfDay);
            if (price == null)
            {
                ModelState.AddModelError("controller", "Unavailable");
                return NotFound(GetModelStateErrors(ModelState));
            }

            return Ok(new ParkingRate
            {
                Price = Convert.ToInt32(Math.Round(price.Value))
            });
        }

        private Message GetModelStateErrors(ModelStateDictionary state)
        {
            return new Message
            {
                Content = state.Values.SelectMany(s => s.Errors).Select(e => e.ErrorMessage).ToArray()
            };
        }
    }
}
