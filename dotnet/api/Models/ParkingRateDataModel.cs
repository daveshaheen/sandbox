using System;
using System.Collections.Generic;

namespace App.Models
{
    /// <summary>
    ///     The parking rate data model.
    /// </summary>
    public class ParkingRateDataModel
    {
        /// <summary>
        ///     Gets or sets the Days property.
        /// </summary>
        /// <value>Returns an enumerable of days.</value>
        public IEnumerable<DayOfWeek> Days { get; set; }

        /// <summary>
        ///     Gets or sets the Price property.
        /// </summary>
        /// <value>Returns the parking rate price.</value>
        public decimal Price { get; set; }

        /// <summary>
        ///     Gets or sets the start time.
        /// </summary>
        /// <value>Returns the start time.</value>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        ///     Gets or sets the end time.
        /// </summary>
        /// <value>Returns the end time.</value>
        public TimeSpan EndTime { get; set; }
    }
}
