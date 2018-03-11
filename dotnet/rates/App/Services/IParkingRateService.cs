using System;

namespace App.Services {
    /// <summary>
    /// IParkingRateService
    /// <para>Parking rate service interface.</para>
    /// </summary>
    public interface IParkingRateService {
        /// <summary>
        /// GetPrice
        /// <para>Gets the price based on the day of the week and the start and end timespans.</para>
        /// </summary>
        /// <param name="dayOfWeek">The day of the week.</param>
        /// <param name="start">The start date time offset.</param>
        /// <param name="end">The end date time offset.</param>
        /// <returns>Returns the price.</returns>
        decimal? GetPrice(DayOfWeek dayOfWeek, TimeSpan start, TimeSpan end);
    }
}
