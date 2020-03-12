using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleERP.Document.API.Infrastructure.Contracts
{
    public interface IUnitOfRepository
    {
        IDocumentInfoRepository DocumentInfoRepository { get; }
        Task SaveAsync(CancellationToken cancellationToken, bool configureAwait = false);
    }
}