using System;
using System.Collections.Generic;

namespace App.Models
{
    /// <summary>Parking rate data model</summary>
    public class ParkingRateDataModel
    {
        /// <summary>Gets or sets the Days property.</summary>
        public IEnumerable<DayOfWeek> Days { get; set; }

        /// <summary>Gets or sets the Price property.</summary>
        public decimal Price { get; set; }

        /// <summary>Gets or sets the start time.</summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>Gets or sets the end time.</summary>
        public TimeSpan EndTime { get; set; }
    }
}
