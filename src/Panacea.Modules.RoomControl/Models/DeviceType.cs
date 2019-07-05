using System.Runtime.Serialization;

namespace Panacea.Modules.RoomControl.Models
{
    [DataContract]
    public enum DeviceType
    {
        [DataMember(Name = "temperature")] Temperature,

        [DataMember(Name = "lighting")] Lighting,

        [DataMember(Name = "glass")] Glass
    }
}
