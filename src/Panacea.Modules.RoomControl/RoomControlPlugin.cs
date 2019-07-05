using Panacea.Core;
using Panacea.Modularity.UiManager;
using Panacea.Modules.RoomControl.Automation;
using Panacea.Modules.RoomControl.Models;
using Panacea.Modules.RoomControl.ViewModels;
using Panacea.Multilinguality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Panacea.Modules.RoomControl
{
    class RoomControlPlugin : ICallablePlugin
    {
        private PanaceaServices _core;
        private List<Device> _devices;
        private DeviceServer _deviceServer;

        public RoomControlPlugin(PanaceaServices core)
        {
            _core = core;
        }
        public Task BeginInit()
        {
            return Task.CompletedTask;
        }
        public void Call()
        {
            var viewModel = new RoomControlsViewModel(_core, _devices, _deviceServer);
            if (_core.TryGetUiManager(out IUiManager ui))
            {
                ui.Navigate(viewModel);
            }
            else
            {
                _core.Logger.Error(this, "ui manager not loaded");
            }
        }
        public void Dispose()
        {
            return;
        }
        public async Task EndInit()
        {
            var response = await _core.HttpClient.GetObjectAsync<GetRoomControlFullResponse>("room_control/get_terminal_devices/");
            if (response.Success)
            {
                _devices = response.Result.Devices.Select(d => d).ToList();
                _deviceServer = response.Result.Server;
            }
            return;
        }
        public Task Shutdown()
        {
            return Task.CompletedTask;
        }
    }
}
