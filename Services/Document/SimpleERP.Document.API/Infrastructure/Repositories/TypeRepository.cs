using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleERP.Document.API.Infrastructure.Data;
using SimpleERP.Libraries.Infrastructure.Commons;
using SimpleERP.Document.API.Infrastructure.Contracts;
using Type = SimpleERP.Document.API.Infrastructure.Data.Type;

namespace SimpleERP.Document.API.Infrastructure.Repositories
{
    public class TypeRepository : Repository<Type>, ITypeRepository
    {
        public TypeRepository(ApplicationDbContext context): base(context)
        {

        }
    }
}
