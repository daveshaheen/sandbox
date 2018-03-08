using ProtoBuf;

namespace App.Models
{
    /// <summary>Parking Rate</summary>
    [ProtoContract]
    public class ParkingRate
    {
        /// <summary>Gets or sets the price.</summary>
        [ProtoMember(1)]
        public int Price { get; set; }
    }
}
