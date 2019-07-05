using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Modules.RoomControl.Models
{

    [DataContract]
    public class ValueRange
    {
        [DataMember(Name = "minValue")]
        public int Min { get; set; }

        [DataMember(Name = "maxValue")]
        public int Max { get; set; }
    }
}
