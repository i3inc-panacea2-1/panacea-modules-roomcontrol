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
        Dictionary<string, Dictionary<string,string>> properties;
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
            properties = new Dictionary<string, Dictionary<string,string>>();
            await Task.Delay(100);
            return true;
        }
        public async Task<string> ReadPropertyAsync(string device, string prop)
        {
            await Task.Delay(100);
            Dictionary<string, string> props;
            if (properties.TryGetValue(device, out props)){
                string val;
                if (props.TryGetValue(prop, out val))
                {
                    return val;
                }
                else
                {
                    return "11";
                }
            }
            else
            {
                return "22";
            }
            //throw new NotImplementedException("ReadPropertyAsync device" + device + " prop: " + prop);
        }
        public async Task<int> WritePropertyAsync(string device, string prop, string value)
        {
            await Task.Delay(100);
            Dictionary<string, string> props;
            if (properties.TryGetValue(device, out props))
            {
                string val;
                if (props.TryGetValue(prop, out val))
                {
                    props[prop] = value;
                    return 1;
                }
                else
                {
                    props[prop] = value;
                    return 1;
                }
            }
            else
            {
                properties[device] = new Dictionary<string, string>();
                properties[device][prop] = value;
            }
            return 1;
            //throw new NotImplementedException("WritePropertyAsync" + device + " prop: " + prop + " val: " + val);
        }
    }
}
