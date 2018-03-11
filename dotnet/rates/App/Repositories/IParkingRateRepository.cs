using System;
using App.Models;

namespace App.Repositories {
    /// <summary>
    /// IParkingRateRepository
    /// <para>Contains the contract information for the methods and properties needed to retrieve the data from the parking rate repository.</para>
    /// </summary>
    public interface IParkingRateRepository {
        /// <summary>Gets the parking rates from the data repository.</summary>
        /// <param name="dayOfWeek">The day of the week.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <returns>Returns the ParkingRatesDataModel</returns>
        ParkingRatesDataModel GetParkingRatesData(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime);
    }
}
