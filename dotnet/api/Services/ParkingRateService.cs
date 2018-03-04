using System;
using System.Linq;
using App.Repositories;

namespace App.Services
{
    /// <summary>
    ///     The ParkingRateService class.
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
        ///     The GetPrice method.
        ///     <para>Applies any needed transforms on the inputs, fetches the price from the data repository, and applies business logic.</para>
        /// </summary>
        /// <param name="start">The start date time offset.</param>
        /// <param name="end">The end date time offset.</param>
        /// <returns>Returns the price.</returns>
        public decimal? GetPrice(DateTimeOffset start, DateTimeOffset end)
        {
            if (DateTime.Compare(start.Date, end.Date) != 0)
            {
                return null;
            }

            var rates = _parkingRateRepository.GetParkingRatesData(start.DayOfWeek, start.TimeOfDay, end.TimeOfDay).Rates;

            if (rates.Count() == 0)
            {
                return null;
            }

            if (rates.Count() > 1)
            {
                throw new DataMisalignedException("The data should not overlap.");
            }

            return rates.Select(r => r.Price).FirstOrDefault();
        }
    }
}
