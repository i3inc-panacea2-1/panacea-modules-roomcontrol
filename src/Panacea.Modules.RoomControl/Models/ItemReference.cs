using System;
using System.Runtime.Serialization;

namespace Panacea.Modules.RoomControl.Models
{
    [DataContract]
    public class ItemReference
    {
        [DataMember(Name = "referenceString")]
        public String Reference { get; set; }

        [DataMember(Name = "refProperty")]
        public String Property { get; set; }

        [DataMember(Name = "refType")]
        public RefType RefType { get; set; }

        [DataMember(Name = "valueRange")]
        public ValueRange ValueRange { get; set; }

        public bool Writable
        {
            get { return RefType == RefType.Range; }
        }
        [DataMember(Name = "unitOfMeasurement")]
        public String MeasurementUnit { get; set; }
    }
}
