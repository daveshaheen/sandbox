using System;

namespace App.Services
{
    /// <summary>
    ///     The ParkingRateService interface.
    /// </summary>
    public interface IParkingRateService
    {
        /// <summary>
        ///     The GetPrice method.
        ///     <para>Applies any needed transforms on the inputs, fetches the price from the data repository, and applies business logic.</para>
        /// </summary>
        /// <param name="start">The start date time offset.</param>
        /// <param name="end">The end date time offset.</param>
        /// <returns>The price.</returns>
        decimal? GetPrice(DateTimeOffset start, DateTimeOffset end);
    }
}
