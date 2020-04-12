using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleERP.Document.API.Infrastructure.Data;
using SimpleERP.Libraries.Infrastructure.Commons;
using SimpleERP.Document.API.Infrastructure.Contracts;

namespace SimpleERP.Document.API.Infrastructure.Repositories
{
    public class DocumentFileRepository : Repository<DocumentFile>, IDocumentFileRepository
    {
        public DocumentFileRepository(ApplicationDbContext context): base(context)
        {

        }
    }
}
