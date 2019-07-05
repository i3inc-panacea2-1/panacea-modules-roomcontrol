using Panacea.Models;
using System.Runtime.Serialization;

namespace Panacea.Modules.RoomControl.Models
{
    [DataContract]
    public class Device : ServerItem
    {
        [DataMember(Name = "deviceGroup")]
        public DeviceGroup Group { get; set; }
        private double _value;
        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }
        [DataMember(Name = "default")]
        public int Default { get; set; }
        private string _label;
        public string Label { get { return _label; } set { _label = value; OnPropertyChanged("Label"); } }
    }
}
