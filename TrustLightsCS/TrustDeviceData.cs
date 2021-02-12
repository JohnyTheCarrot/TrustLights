using System;
using System.Collections.Generic;
using System.Text;

namespace TrustLightsCS
{
    public class TrustDeviceData
    {
        public BaseTrustModule module { get; set; }
        public object scene { get; set; }
        public object scenario { get; set; }
        public object room { get; set; }
        public object zone { get; set; }
        public BaseTrustGroup group { get; set; }
    }
}
