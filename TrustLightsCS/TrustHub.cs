using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TrustLightsCS
{
    public class TrustHub
    {
        public string MacAddress { get; }
        public string Email { get; }
        public string Password { get; }
        public TrustUser User { get; }
        public TrustDevice[] Devices { get; internal set; }
        public TrustDeviceGroup[] Groups { get; }

        public TrustHub(string macAddress, string email, string password)
        {
            MacAddress = macAddress;
            Email = email;
            Password = password;
            var baseTrustDevices = TrustHttp.GetHubDevices(MacAddress, Email, Password);
            var devices = baseTrustDevices.Select(baseTrustDevice => new TrustDevice(this, baseTrustDevice)).ToList();
            Devices = devices.ToArray();
            User = new TrustUser(this, TrustHttp.LoginUser(MacAddress, Email, Password));

            // get groups
            Groups = Devices
                .Where(d => d.Data.group != null)
                .Select(d => new TrustDeviceGroup(this, d.Data.group, d.Base))
                .ToArray();
        }

        public TrustDevice GetDevice(int id)
        {
            return Devices.FirstOrDefault(device => device.Id == id);
        }

        public static TrustHub GetHub(string macAddress, string email, string password)
        {
            return TrustHttp.GetHub(macAddress, email, password);
        }
    }
}
