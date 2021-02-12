using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrustLightsCS
{
    public class TrustHome
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Mac { get; set; }
        public string AesKey { get; set; }
        public TrustHub Hub;
        public TrustDevice[] Devices;

        internal TrustHome(TrustHub hub, BaseTrustHome baseTrustHome)
        {
            Id = baseTrustHome.home_id;
            Name = baseTrustHome.home_name;
            Mac = baseTrustHome.mac;
            AesKey = baseTrustHome.aes_key;
            Hub = hub;

            var devices = Hub.Devices.Where(device =>
            {
                var isOwnedByHome = device.HomeId == Id;

                if (isOwnedByHome)
                    device.Home = this;

                return isOwnedByHome;
            }).ToList();

            Devices = devices.ToArray();
            hub.Devices = Devices;
        }
    }
}
