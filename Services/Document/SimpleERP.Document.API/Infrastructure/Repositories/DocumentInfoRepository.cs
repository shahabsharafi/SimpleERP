using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleERP.Document.API.Infrastructure.Data;
using SimpleERP.Infrastructure.Commons;
using SimpleERP.Document.API.Infrastructure.Contracts;

namespace SimpleERP.Document.API.Infrastructure.Repositories
{
    public class DocumentInfoRepository : Repository<DocumentInfo>, IDocumentInfoRepository
    {
        public DocumentInfoRepository(ApplicationDbContext context): base(context)
        {

        }
    }
}
