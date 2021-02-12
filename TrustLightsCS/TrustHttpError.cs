using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TrustLightsCS
{
    class TrustHttpError : Exception
    {
        public HttpStatusCode Code;

        public TrustHttpError(string message, HttpStatusCode code) : base(message)
        {
            Code = code;
        }
    }
}
