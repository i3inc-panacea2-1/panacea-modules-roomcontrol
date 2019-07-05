using System.Runtime.Serialization;

namespace Panacea.Modules.RoomControl.Models
{
    [DataContract]
    public enum RefType
    {
        [DataMember(Name = "readOnly")]
        ReadOnly,
        [DataMember(Name = "range")]
        Range,
        [DataMember(Name = "onOff")]
        OnOff

    }
}
