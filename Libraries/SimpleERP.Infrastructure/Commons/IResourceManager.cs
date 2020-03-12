using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleERP.Infrastructure.Commons
{
    public interface IResourceManager
    {
        string GetValue(string key);
    }
}
