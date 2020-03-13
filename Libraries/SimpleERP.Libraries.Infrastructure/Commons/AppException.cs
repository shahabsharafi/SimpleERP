using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SimpleERP.Libraries.Infrastructure.Commons
{
    public class AppException: Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
