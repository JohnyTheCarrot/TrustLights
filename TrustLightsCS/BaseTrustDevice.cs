using System;
using System.Collections.Generic;
using System.Text;

namespace TrustLightsCS
{

    public class BaseTrustDevice
    {
        public string home_id { get; set; }
        public string id { get; set; }
        public string version_status { get; set; }
        public string version_data { get; set; }
        public object rule { get; set; }
        public string data { get; set; }
        public string status { get; set; }
        public string created { get; set; }
    }

}
