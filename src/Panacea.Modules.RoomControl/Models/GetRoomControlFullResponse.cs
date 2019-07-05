using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Panacea.Modules.RoomControl.Models
{
    [DataContract]
    public class GetRoomControlFullResponse
    {
        [DataMember(Name = "devices")]
        public List<Device> Devices { get; set; }

        [DataMember(Name = "temperatureserver")]
        public DeviceServer Server { get; set; }
    }
}
