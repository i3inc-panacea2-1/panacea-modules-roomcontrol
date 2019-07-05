using System.Runtime.Serialization;

namespace Panacea.Modules.RoomControl.Models
{
    [DataContract]
    public class DeviceServer
    {
        [DataMember(Name = "serverUrl")]
        public string ServerUrl { get; set; }

        [DataMember(Name = "systemUsername")]
        public string Username { get; set; }

        [DataMember(Name = "systemPassword")]
        public string Password { get; set; }
    }
}
