using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SimpleERP.Document.API.Infrastructure.Contracts;
using SimpleERP.Document.API.Infrastructure.Data;

namespace SimpleERP.Document.API.Infrastructure.Repositories
{
    public class UnitOfRepository : IUnitOfRepository
    {
        private ApplicationDbContext _dbContext;
        private IDocumentInfoRepository _documentInfoRepository;

        public UnitOfRepository(ApplicationDbContext context, IDocumentInfoRepository documentInfoRepository)
        {
            this._dbContext = context;
            this._documentInfoRepository = documentInfoRepository;
        }
        
        public IDocumentInfoRepository DocumentInfoRepository
        {
            get
            {
                if (_documentInfoRepository == null)
                {
                    _documentInfoRepository = new DocumentInfoRepository(_dbContext);
                }

                return _documentInfoRepository;
            }
        }

        public async Task SaveAsync(CancellationToken cancellationToken, bool configureAwait = false)
        {
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(configureAwait);
        }
    }
}
