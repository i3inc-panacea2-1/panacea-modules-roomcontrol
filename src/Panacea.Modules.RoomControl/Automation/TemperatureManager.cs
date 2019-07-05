using MetasysSystemSecureDataAccess;
using MSSOAPLib30;
using MSXML2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Modules.RoomControl.Automation
{
    public class TemperatureManager : IDeviceManager
    {
        private string _ip, _username, _password;
        dynamic _dTimeClient;
        dynamic _dClient;
        MSSDAAPI _api;

        public TemperatureManager(string ip, string username, string password)
        {
            _ip = ip;
            _username = username;
            _password = password;
        }

        public Task<bool> InitAsync()
        {
            return Task.Run(() =>
            {
                try
                {
                    _dTimeClient = new SoapClient30();
                    _dClient = new SoapClient30();
                    _api = new MSSDAAPI();
                    object obj = new object();
                    _api.LoginUser(_ip, _username, _password, ref obj);
                    _dTimeClient.MSSoapInit("http://" + _ip + "/MetasysIII/WS/TimeManagement/TimeService.asmx?wsdl");
                    _dClient.MSSoapInit("http://" + _ip + "/MetasysIII/WS/BasicServices.asmx?wsdl");
                    _dClient.HeaderHandler = _api.headerHandler;
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }
        private string GetTimeAsync()
        {

            var nodelist = _dTimeClient?.GetCurrentTime() as IXMLDOMNodeList;
            return nodelist?[1]?.text;

        }
        public Task<string[]> GetDeviceListAsync()
        {
            if (_api == null) return Task.FromResult(new string[] { "n/a" });
            return Task.Run(() =>
            {
                var obj = new object();
                var i = _api.InitMethodAuthentication(GetTimeAsync(), "GetDeviceList", "", ref obj);
                var list = new string[1];
                _dClient.GetDeviceList("", ref list);
                return list;
            });
        }
        public Task<string[]> GetObjectListAsync(string reference)
        {
            if (_api == null) return Task.FromResult(new string[] { "n/a" });
            return Task.Run(() =>
            {
                object obj = new object();
                var i = _api.InitMethodAuthentication(GetTimeAsync(), "GetObjectList", reference, ref obj);
                var list = new string[0];
                _dClient.GetObjectList(reference, ref list);
                return list;
            });
        }
        public Task<string> ReadPropertyAsync(string device, string prop)
        {
            if (_api == null) return Task.FromResult("n/a");
            return Task.Run(() =>
            {
                var obj = new object();
                var i = _api.InitMethodAuthentication(GetTimeAsync(), "ReadProperty", device, ref obj);
                var str = "";
                var rel = "";
                var pr = "";
                float fl = 0;
                _dClient.ReadProperty(device, prop, ref str, ref fl, ref rel, ref pr);
                return str;
            });
        }
        public Task<int> WritePropertyAsync(string device, string prop, string val)
        {
            if (_api == null) return Task.FromResult(0);
            return Task.Run(() =>
            {
                var obj = new object();
                var i = _api.InitMethodAuthentication(GetTimeAsync(), "WriteProperty", device, ref obj);
                var rel = "";
                var pr = "";
                return (int)_dClient.WriteProperty(device, prop, val, ref rel, ref pr);
            });
        }

    }
}
