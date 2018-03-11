using System;
using System.Linq;
using App.Repositories;

namespace App.Services {
    /// <summary>
    /// ParkingRateService
    /// <para>Parking rate service implementation.</para>
    /// </summary>
    public class ParkingRateService : IParkingRateService {
        private IParkingRateRepository _parkingRateRepository;

        /// <summary>ParkingRateService constructor.</summary>
        /// <param name="parkingRateRepository">Parking rate repository.</param>
        public ParkingRateService(IParkingRateRepository parkingRateRepository) {
            _parkingRateRepository = parkingRateRepository;
        }

        /// <summary>
        /// GetPrice
        /// <para>Gets the price based on the day of the week and the start and end timespans.</para>
        /// </summary>
        /// <param name="dayOfWeek">The day of the week.</param>
        /// <param name="start">The start date time offset.</param>
        /// <param name="end">The end date time offset.</param>
        /// <returns>Returns the price.</returns>
        public decimal? GetPrice(DayOfWeek dayOfWeek, TimeSpan start, TimeSpan end) {
            if (start > end) {
                return null;
            }

            var rates = _parkingRateRepository.GetParkingRatesData(dayOfWeek, start, end).Rates;

            if (rates.Count() == 0) {
                return null;
            }

            if (rates.Count() > 1) {
                throw new DataMisalignedException("Rates overlap.");
            }

            return rates.Select(r => r.Price).FirstOrDefault();
        }
    }
}
