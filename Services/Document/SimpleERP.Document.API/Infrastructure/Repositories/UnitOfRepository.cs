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
        private IDocumentFileRepository _documentFileRepository;
        private IIssuerRepository _issuerRepository;
        private IDomainRepository _domainRepository;
        private ITypeRepository _typeRepository;

        public UnitOfRepository(
            ApplicationDbContext context, 
            IDocumentInfoRepository documentInfoRepository,
            IDocumentFileRepository documentFileRepository,
            IIssuerRepository issuerRepository,
            IDomainRepository domainRepository,
            ITypeRepository typeRepository
        )
        {
            this._dbContext = context;
            this._documentInfoRepository = documentInfoRepository;
            this._documentFileRepository = documentFileRepository;
            this._issuerRepository = issuerRepository;
            this._domainRepository = domainRepository;
            this._typeRepository = typeRepository;
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

        public IDocumentFileRepository DocumentFileRepository
        {
            get
            {
                if (_documentFileRepository == null)
                {
                    _documentFileRepository = new DocumentFileRepository(_dbContext);
                }

                return _documentFileRepository;
            }
        }

        public IIssuerRepository IssuerRepository
        {
            get
            {
                if (_issuerRepository == null)
                {
                    _issuerRepository = new IssuerRepository(_dbContext);
                }

                return _issuerRepository;
            }
        }

        public IDomainRepository DomainRepository
        {
            get
            {
                if (_domainRepository == null)
                {
                    _domainRepository = new DomainRepository(_dbContext);
                }

                return _domainRepository;
            }
        }

        public ITypeRepository TypeRepository
        {
            get
            {
                if (_typeRepository == null)
                {
                    _typeRepository = new TypeRepository(_dbContext);
                }

                return _typeRepository;
            }
        }

        public async Task SaveAsync(CancellationToken cancellationToken, bool configureAwait = false)
        {
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(configureAwait);
        }
    }
}
