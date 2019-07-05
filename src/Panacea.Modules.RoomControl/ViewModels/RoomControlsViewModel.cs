using Panacea.Controls;
using Panacea.Core;
using Panacea.Modularity.Relays;
using Panacea.Modularity.UiManager;
using Panacea.Modules.RoomControl.Automation;
using Panacea.Modules.RoomControl.Controls;
using Panacea.Modules.RoomControl.Models;
using Panacea.Modules.RoomControl.Views;
using Panacea.Multilinguality;
using Panacea.Mvvm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Panacea.Modules.RoomControl.ViewModels
{
    [View(typeof(RoomControlsView))]
    public class RoomControlsViewModel : ViewModelBase
    {
        private PanaceaServices _core;
        private Translator _translator = new Translator("RoomControl");
        public List<Device> LightingDevices { get; set; }
        public List<Device> GlassDevices { get; set; }
        public List<Device> TemperatureDevices { get; set; }
        public GridLength GlassesColumnWidth { get; set; }
        public GridLength TemperatureColumnWidth { get; set; }
        public GridLength LightsColumnWidth { get; set; }
        public GridLength BlindsColumnWidth { get; set; }

        DispatcherTimer _timer = new DispatcherTimer();
        DispatcherTimer _idleTimer = new DispatcherTimer();
        private DispatcherTimer _reInitTimer;
        private bool _initializingManager = false;
        private IDeviceManager _temperatureManager;
        private List<Device> _devices;
        private DeviceServer _deviceServer;
        private bool _inited;
        private bool RelayManagerIsFtdiPresent = false;
        public RoomControlsViewModel(PanaceaServices core, List<Device> devices, DeviceServer deviceServer)
        {
            _core = core;
            _devices = devices;
            _deviceServer = deviceServer;
            LightingDevices = new List<Device>();
            GlassDevices = new List<Device>();
            TemperatureDevices = new List<Device>();
            if (devices.Any(d => d.Group.Type == DeviceType.Lighting))
                LightingDevices = devices.Where(d => d.Group.Type == DeviceType.Lighting).ToList();

            if (devices.Any(d => d.Group.Type == DeviceType.Temperature))
                TemperatureDevices = devices.Where(d => d.Group.Type == DeviceType.Temperature).ToList();

            if (devices.Any(d => d.Group.Type == DeviceType.Glass))
                GlassDevices = devices.Where(d => d.Group.Type == DeviceType.Glass).ToList();


            setupTimers();
            setupCommands();

#if DEBUG
            _temperatureManager = new VoidManager(_devices);
#else
            _temperatureManager = new TemperatureManager(_deviceServer.ServerUrl, _deviceServer.Username, _deviceServer.Password);
#endif
        }


        private void setupCommands()
        {
            StringValueChangedCommand = new RelayCommand(async args =>
            {
                if (args.GetType().IsArray)
                {
                    _idleTimer.Stop();
                    _idleTimer.Start();
                    object[] arr = args as object[];
                    var dev = arr[0] as Device;
                    string value = arr[1] as string;
                    if (_core.TryGetUiManager(out IUiManager ui))
                    {
                        await ui.DoWhileBusy(async () =>
                        {
                            _timer.Stop();
                            try
                            {
                                _core.WebSocket.PopularNotify("RoomControl", "DeviceGroup", dev.Group.Id);
                                _core.Logger.Debug("RoomControl", "Writing to device: " + value + " " + dev.Group.ItemReferences[0].Reference);
                                await _temperatureManager.WritePropertyAsync(dev.Group.ItemReferences[0].Reference, dev.Group.ItemReferences[0].Property, value);
                            }
                            catch
                            {
                                ui.Toast(
                                    new Translator("RoomControl").Translate("Failed to reach device. Please try again later"));
                                //((SliderControl)sender).Value = dev.Value;
                            }
                            try
                            {
                                await UpdateDeviceSafe(dev);
                            }
                            catch
                            {
                            }
                            if (ui.CurrentPage == this) _timer.Start();
                        });
                    }
                }
            });
            MinMaxValueChangedCommand = new RelayCommand(async args =>
            {
                Console.WriteLine("minmax value changed");
                var dev = args as Device;
                var value = dev.Value;
                _idleTimer.Stop();
                _idleTimer.Start();
                if (_core.TryGetUiManager(out IUiManager ui))
                {
                    await ui.DoWhileBusy(async () =>
                    {
                        _timer.Stop();
                        try
                        {
                            _core.WebSocket.PopularNotify("RoomControl", "DeviceGroup", dev.Group.Id);
                            _core.Logger.Debug("RoomControl", "Writing to device: " + value + " " + dev.Group.ItemReferences[0].Reference);
                            await _temperatureManager.WritePropertyAsync(dev.Group.ItemReferences[0].Reference, dev.Group.ItemReferences[0].Property, value.ToString());
                        }
                        catch
                        {
                            ui.Toast(
                                new Translator("RoomControl").Translate("Failed to reach device. Please try again later"));
                            //((MinMaxButtonsControl)sender).Value = dev.Value;
                        }
                        try
                        {
                            await UpdateDeviceSafe(dev);
                        }
                        catch
                        {
                        }
                        if (ui.CurrentPage == this) _timer.Start();
                    });
                }
            });
            BlindsUpMouseDownCommand = new RelayCommand(args =>
            {
                BlindsUpButton_OnMouseDown();
            });
            BlindsUpMouseUpCommand = new RelayCommand(args =>
            {
                BlindsUpButton_OnMouseUp();
            });
            BlindsDownMouseDownCommand = new RelayCommand(args =>
            {
                BlindsDownButton_OnMouseDown();
            });
            BlindsDownMouseUpCommand = new RelayCommand(args =>
            {
                BlindsDownButton_OnMouseUp();
            });
            BlindsStopCommand = new RelayCommand(args =>
            {
                BlindsStopButton_OnClick();
            });
        }
        private void setupTimers()
        {
            _timer.Interval = TimeSpan.FromSeconds(new Random().Next(55, 90));
            _idleTimer.Interval = TimeSpan.FromMinutes(2);
            _idleTimer.Tick += _idleTimer_Tick;
            _timer.Tick += _timer_Tick;
            _reInitTimer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromHours(new Random().Next(8, 9))
            };
            _reInitTimer.Tick += _reInitTimer_Tick;
            _reInitTimer.Start();
            _idleTimer.Start();
        }
        private void setColumnWidths()
        {
            if (RelayManagerIsFtdiPresent)
            {
                BlindsColumnWidth = new GridLength(4, GridUnitType.Star);
            }
            else
            {
                BlindsColumnWidth = new GridLength(0, GridUnitType.Star);
            }
            if (LightingDevices?.Count == 0)
            {
                LightsColumnWidth = new GridLength(0, GridUnitType.Star);
            }
            else
            {
                LightsColumnWidth = new GridLength(4, GridUnitType.Star);
            }
            if (TemperatureDevices?.Count == 0)
            {
                TemperatureColumnWidth = new GridLength(0, GridUnitType.Star);
            }
            else
            {
                TemperatureColumnWidth = new GridLength(6, GridUnitType.Star);
            }
            if (GlassDevices?.Count == 0)
            {
                GlassesColumnWidth = new GridLength(0, GridUnitType.Star);
            }
            else
            {
                GlassesColumnWidth = new GridLength(6, GridUnitType.Star);
            }
        }

        public override async void Activate()
        {
            if (!_inited) await InitializeManager();
            if (!_inited) return;
            if (_core.GetRelayManager(out IRelayManager relay))
            {
                RelayManagerIsFtdiPresent = relay.BlindsAttached;
            }
            setColumnWidths();
            _timer.Start();
            await RefreshDevices();
            base.Activate();
        }
        public override void Deactivate()
        {
            _idleTimer.Stop();
            _timer.Stop();
            base.Deactivate();
        }
        private void _idleTimer_Tick(object sender, EventArgs e)
        {
            if (_core.TryGetUiManager(out IUiManager ui))
            {
                //if (ui.CurrentPage == this) ui.GoHome();
            }
            else
            {
                _core.Logger.Error(this, "ui manager not loaded");
            }
            _idleTimer.Stop();
        }

        async void _reInitTimer_Tick(object sender, EventArgs e)
        {
            _inited = false;
            if (_core.TryGetUiManager(out IUiManager ui))
            {
                await InitializeManager();
            }
            else
            {
                _core.Logger.Error(this, "ui manager not loaded");
            }
        }

        async void _timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();
            _timer.Interval = TimeSpan.FromSeconds(new Random().Next(55, 90));
            await RefreshDevices();
            if (_core.TryGetUiManager(out IUiManager ui))
            {
                if (ui.CurrentPage != this) return;
            }
            _timer.Start();
        }
        private async Task InitializeManager()
        {
            if (_initializingManager) return;
            _initializingManager = true;
            if (_core.TryGetUiManager(out IUiManager ui))
            {
                await ui.DoWhileBusy(async () =>
                {
                    await Task.Run(async () =>
                   {
                       try
                       {
                           var completed = _temperatureManager.InitAsync().Wait(7000);
                           if (completed)
                           {
                               await RefreshDevices();
                               _inited = true;
                               _core.Logger.Debug("RoomControl", "Inited");
                           }
                       }
                       catch (Exception ex)
                       {
                           _core.Logger.Debug("RoomControl", ex.Message);
                       }
                   });
                });
                _initializingManager = false;
            }
        }

        async Task UpdateDevice(Device device, bool reinit = true)
        {
            if (_initializingManager) return;
            try
            {
                var txt =
                    await
                        _temperatureManager.ReadPropertyAsync(
                            device.Group.ItemReferences[0].Reference, device.Group.ItemReferences[0].Property);
                try
                {

                    var d = double.Parse(txt);
                    device.Label = d.ToString("#0.0", CultureInfo.InvariantCulture);
                    device.Value = d;
                }
                catch
                {
                    device.Label = txt;
                    try
                    {

                        var digitsOnly = new Regex(@"[^\d]");
                        device.Value = double.Parse(digitsOnly.Replace(txt, "")) - 1;
                    }
                    catch
                    {
                        _core.Logger.Debug("RoomControl", "Failed to parse value");
                    }
                }

            }
            catch (Exception ex)
            {
                _core.Logger.Debug("RoomControl", ex.Message);
                if (ex.InnerException != null)
                    _core.Logger.Debug("RoomControl", ex.InnerException.Message);
                device.Label = "--";
                if (ex.Message.Contains("Invalid session") && reinit)
                {
                    await InitializeManager();
                    try
                    {
                        await UpdateDeviceSafe(device, false);
                    }
                    catch
                    {
                    }
                }
                else throw;
            }
            finally
            {
                _initializingManager = false;
            }
        }

        private async Task RefreshDevices()
        {
            if (_initializingManager) return;
            if (_core.TryGetUiManager(out IUiManager ui))
            {
                if (ui.CurrentPage != this)
                {
                    _timer.Stop();
                    if (ui.CurrentPage != this) return;
                }
                await ui.DoWhileBusy(async () =>
                {
                    var errors = false;

                    foreach (var device in _devices)
                    {
                        if (!await UpdateDeviceSafe(device)) errors = true;
                    }
                    if (errors)
                        ui.Toast(
                            _translator.Translate(
                                "Operation completed with errors. Some devices did not respond or request timed out."));
                });
            }
        }

        Task<bool> UpdateDeviceSafe(Device device, bool reinit = true)
        {
            return Task.Run(() =>
            {
                try
                {
                    var task = UpdateDevice(device, reinit);
                    if (!task.Wait(5000))
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
                return true;
            });
        }

        public RelayCommand StringValueChangedCommand { get; private set; }
        public ICommand MinMaxValueChangedCommand { get; set; }
        public ICommand BlindsUpMouseDownCommand { get; set; }
        public ICommand BlindsUpMouseUpCommand { get; set; }
        public ICommand BlindsDownMouseDownCommand { get; set; }
        public ICommand BlindsDownMouseUpCommand { get; set; }
        public ICommand BlindsStopCommand { get; set; }

        private async void BlindsUpButton_OnMouseDown()
        {
            _idleTimer.Stop();
            _idleTimer.Start();
            _core.Logger.Debug(this, "n: 4, true");
            if (_core.GetRelayManager(out IRelayManager relay))
            {
                await relay.SetBlindsUpAsync(true);
            }

            //_core.GetRelayManager().FtdiSetRelayStatus(4, false);

        }

        private async void BlindsUpButton_OnMouseUp()
        {
            last_DirectionButton = BLINDS_UP;
            _idleTimer.Stop();
            _idleTimer.Start();
            if (_core.GetRelayManager(out IRelayManager relay))
            {
                await relay.SetBlindsUpAsync(false);
            }

        }


        private async void BlindsDownButton_OnMouseDown()
        {
            _idleTimer.Stop();
            _idleTimer.Start();
            if (_core.GetRelayManager(out IRelayManager relay))
            {
                await relay.SetBlindsDownAsync(true);
            }
        }

        private string last_DirectionButton;
        const string BLINDS_UP = "BlindsUpButton";
        const string BLINDS_DOWN = "BlindsDownButton";
        private async void BlindsDownButton_OnMouseUp()
        {
            last_DirectionButton = BLINDS_DOWN;
            _idleTimer.Stop();
            _idleTimer.Start();
            if (_core.GetRelayManager(out IRelayManager relay))
            {
                await relay.SetBlindsDownAsync(false);
            }
        }

        private async void BlindsStopButton_OnClick()
        {
            _idleTimer.Stop();
            _idleTimer.Start();
            if (last_DirectionButton == null) return;
            if (_core.GetRelayManager(out IRelayManager relay))
            {
                await relay.SetBlindsUpAsync(false);
                await relay.SetBlindsDownAsync(false);
                Thread.Sleep(101);
                if (last_DirectionButton == BLINDS_UP)
                {
                    await relay.SetBlindsDownAsync(true);
                }
                else
                {
                    await relay.SetBlindsUpAsync(true);
                }
                Thread.Sleep(101);
                await relay.SetBlindsUpAsync(false);
                await relay.SetBlindsDownAsync(false);
                last_DirectionButton = null;
            }
        }
    }
}