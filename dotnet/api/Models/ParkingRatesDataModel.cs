using System.Collections.Generic;

namespace App.Models
{
    /// <summary>
    ///     The parking rates data model.
    /// </summary>
    public class ParkingRatesDataModel
    {
        /// <summary>
        ///     Gets or sets the rates.
        /// </summary>
        /// <value>Returns an enumerable of rates.</value>
        public IEnumerable<ParkingRateDataModel> Rates { get; set; }
    }

}
