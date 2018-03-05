using System;
using System.Linq;
using App.Repositories;

namespace App.Services
{
    /// <summary>
    ///     ParkingRateService
    ///     <para>Contains implementations for the methods and properties that handle the parking rates service logic.</para>
    /// </summary>
    public class ParkingRateService : IParkingRateService
    {
        private IParkingRateRepository _parkingRateRepository;

        /// <summary>
        ///     ParkingRateService constructor.
        /// </summary>
        /// <param name="parkingRateRepository">A parking rate repository.</param>
        public ParkingRateService(IParkingRateRepository parkingRateRepository)
        {
            _parkingRateRepository = parkingRateRepository;
        }

        /// <summary>
        ///     GetPrice
        ///     <para>Applies any needed transforms on the inputs, fetches the price from the data repository, and applies any needed logic.</para>
        /// </summary>
        /// <param name="dayOfWeek">The day of the week.</param>
        /// <param name="start">The start date time offset.</param>
        /// <param name="end">The end date time offset.</param>
        /// <returns>Returns the price.</returns>
        public decimal? GetPrice(DayOfWeek dayOfWeek, TimeSpan start, TimeSpan end)
        {
            if (start > end) {
                return null;
            }

            var rates = _parkingRateRepository.GetParkingRatesData(dayOfWeek, start, end).Rates;

            if (rates.Count() == 0)
            {
                return null;
            }

            if (rates.Count() > 1)
            {
                throw new DataMisalignedException("Rates overlap.");
            }

            return rates.Select(r => r.Price).FirstOrDefault();
        }
    }
}
