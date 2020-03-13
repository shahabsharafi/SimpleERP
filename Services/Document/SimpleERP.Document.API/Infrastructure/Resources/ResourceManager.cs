using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleERP.Libraries.Infrastructure.Commons;

namespace SimpleERP.Document.API.Infrastructure
{
    public class ResourceManager : IResourceManager
    {
        IConfigurationSection _resources;
        public ResourceManager(IConfiguration configuration)
        {
            this._resources = configuration.GetSection("resources");
        }

        public string GetValue(string key)
        {
            return this._resources[key];
        }
    }
}
