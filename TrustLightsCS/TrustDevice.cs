using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace TrustLightsCS
{
    public class TrustDevice
    {
        public TrustDevice(TrustHub hub, BaseTrustDevice baseTrustDevice)
        {
            HomeId = baseTrustDevice.home_id;
            Id = Convert.ToInt32(baseTrustDevice.id);
            RawData = baseTrustDevice.data;
            RawStatus = baseTrustDevice.status;
            Hub = hub;
            Base = baseTrustDevice;
        }

        public void SetColor(TrustColorRgb color)
        {
            var value = new TrustXyValue(color.ToXyz().ToYxy());
            var result = value.ToValue();
            var command = TrustCommand.SimpleCommand(Home.Mac, Home.AesKey, Convert.ToInt32(Id), 5, result);
            TrustHttp.SendCommand(Home.Mac, Hub.Email, Hub.Password, command);
        }

        public void TurnOn()
        {
            var command = TrustCommand.SimpleCommand(Home.Mac, Home.AesKey, Convert.ToInt32(Id), 3, 1);
            TrustHttp.SendCommand(Home.Mac, Hub.Email, Hub.Password, command);
        }

        public void TurnOff()
        {
            var command = TrustCommand.SimpleCommand(Home.Mac, Home.AesKey, Convert.ToInt32(Id), 3, 0);
            TrustHttp.SendCommand(Home.Mac, Hub.Email, Hub.Password, command);
        }

        internal BaseTrustDevice Base;
        public TrustHome Home { get; internal set; }
        public string HomeId { get; }
        public int Id { get; }
        public TrustHub Hub { get; }
        public string VersionStatus { get; }
        public string VersionData { get; }
        public object Rule { get; }
        private string RawData { get; }
        public TrustDeviceData Data
        {
            get
            {
                if (Home == null)
                    Home = Hub.User.Homes.FirstOrDefault(h => h.Id == HomeId);
                var serialized = TrustCryptography.Decrypt(RawData, Home.AesKey);
                Debug.WriteLine(serialized);
                var deserialized = JsonSerializer.Deserialize<TrustDeviceData>(serialized);
                return deserialized;
            }
        }

        private string RawStatus { get; }
        public string Status => TrustCryptography.Decrypt(RawStatus, Home.AesKey);
        public string Created { get; }
    }
}
