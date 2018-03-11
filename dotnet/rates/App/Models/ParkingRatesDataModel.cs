using System.Collections.Generic;

namespace App.Models {
    /// <summary>Parking rates data model.</summary>
    public class ParkingRatesDataModel {
        /// <summary>Gets or sets the rates.</summary>
        public IEnumerable<ParkingRateDataModel> Rates { get; set; }
    }

}
