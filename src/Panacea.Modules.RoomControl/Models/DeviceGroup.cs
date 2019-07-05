using Panacea.Models;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Panacea.Modules.RoomControl.Models
{
    [DataContract]
    public class DeviceGroup : ServerItem
    {
        [DataMember(Name = "deviceType")]
        public DeviceType Type { get; set; }

        [DataMember(Name = "itemReferences")]
        public List<ItemReference> ItemReferences { get; set; }
    }
}
