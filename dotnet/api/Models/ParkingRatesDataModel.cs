using System.Collections.Generic;

namespace App.Models
{
    /// <summary>
    ///     The ParkingRatesDataModel class.
    /// </summary>
    public class ParkingRatesDataModel
    {
        /// <summary>
        ///     The Rates property.
        ///     <para>Gets or sets the rates.</para>
        /// </summary>
        /// <returns>Returns an enumerable of rates.</returns>
        public IEnumerable<ParkingRateDataModel> Rates { get; set; }
    }

}
