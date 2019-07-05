using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Panacea.Modules.RoomControl.Models;

namespace Panacea.Modules.RoomControl.Automation
{
    internal interface IDeviceManager
    {
        Task<bool> InitAsync();
        Task<string> ReadPropertyAsync(string device, string prop);
        Task<int> WritePropertyAsync(string device, string prop, string val);
    }
    internal class VoidManager : IDeviceManager
    {
        Dictionary<string, string> properties;
        private List<Device> devices;

        public VoidManager(List<Device> devices)
        {
            this.devices = devices;
            foreach (Device dev in devices)
            {
                
            }
        }

        public async Task<bool> InitAsync()
        {
            properties = new Dictionary<string, string>();
            properties.Add("Present Value", "0");
            await Task.Delay(1500);
            return true;
        }
        public async Task<string> ReadPropertyAsync(string device, string prop)
        {
            await Task.Delay(1500);
            string val;
            if (properties.TryGetValue(prop, out val)){
                return val;
            }
            else
            {
                return "1";
            }
            //throw new NotImplementedException("ReadPropertyAsync device" + device + " prop: " + prop);
        }
        public async Task<int> WritePropertyAsync(string device, string prop, string val)
        {
            await Task.Delay(1500);
            properties[prop] = val;
            return 1;
            //throw new NotImplementedException("WritePropertyAsync" + device + " prop: " + prop + " val: " + val);
        }
    }
}
