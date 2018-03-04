using System;
using System.Collections.Generic;

namespace App.Models
{
    /// <summary>
    ///     The ParkingRateDataModel class.
    /// </summary>
    public class ParkingRateDataModel
    {
        /// <summary>
        ///     The Days property.
        ///     <para>Gets or sets the Days property.</para>
        /// </summary>
        /// <value>Returns an enumerable of days.</value>
        public IEnumerable<DayOfWeek> Days { get; set; }

        /// <summary>
        ///     The Price property.
        ///     <para>Gets or sets the Price property.</para>
        /// </summary>
        /// <returns>Returns the parking rate price.</returns>
        public decimal Price { get; set; }

        /// <summary>
        ///     The StartTime property.
        ///     <para>Gets or sets the start time.</para>
        /// </summary>
        /// <returns>Returns the start time.</returns>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        ///     The EndTime property.
        ///     <para>Gets or sets the end time.</para>
        /// </summary>
        /// <returns>Returns the end time.</returns>
        public TimeSpan EndTime { get; set; }
    }
}
