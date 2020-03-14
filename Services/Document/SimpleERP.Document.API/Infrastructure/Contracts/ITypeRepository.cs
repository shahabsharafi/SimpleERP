using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleERP.Libraries.Infrastructure.Commons;
using SimpleERP.Document.API.Infrastructure.Data;
using Type = SimpleERP.Document.API.Infrastructure.Data.Type;

namespace SimpleERP.Document.API.Infrastructure.Contracts
{
    public interface ITypeRepository :  IRepository<Type> 
    {
    }
}
