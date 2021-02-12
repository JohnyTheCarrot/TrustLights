using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrustLightsCS
{
    public class TrustDeviceGroup : TrustDevice
    {
        public TrustDevice[] Devices;

        public TrustDeviceGroup(TrustHub hub, BaseTrustGroup baseTrustGroup, BaseTrustDevice device) : base(hub, device)
        {
            Devices = baseTrustGroup.modules.Select(hub.GetDevice).ToArray();
        }
    }
}
