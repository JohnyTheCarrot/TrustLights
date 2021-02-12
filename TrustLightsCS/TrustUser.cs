using System.Collections.Generic;
using System.Linq;

namespace TrustLightsCS
{
    public class TrustUser
    {
        public TrustUser(TrustHub hub, BaseTrustUser trustUser)
        {
            Name = trustUser.person_name;
            Newsletter = trustUser.newsletter;
            IpCamOnly = trustUser.ipcam_only;
            Hub = hub;
            var homes = trustUser.homes.Select(baseTrustHome => new TrustHome(hub, baseTrustHome)).ToList();
            Homes = homes.ToArray();
        }

        public string Name { get; }
        public string Newsletter { get; }
        public bool IpCamOnly { get; }
        public TrustHome[] Homes { get; }
        public TrustHub Hub { get; }
    }
}
