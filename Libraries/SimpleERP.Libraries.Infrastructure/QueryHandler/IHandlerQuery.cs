using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace SimpleERP.Libraries.Infrastructure.QueryHandler
{
    public interface IQueryHandler
    {
        bool HasIds(IQueryCollection args);
        string[] GetIds(IQueryCollection args);
        bool HasFilter(IQueryCollection args);
        IQueryable ApplyQuery(IQueryable query, IQueryCollection args);
        Dictionary<string, object> Execute(IQueryable query, IQueryCollection args);
    }
}
