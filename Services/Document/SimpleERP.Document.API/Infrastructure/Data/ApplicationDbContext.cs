using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Document.API.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ILogger _logger;

        public ApplicationDbContext(DbContextOptions options, ILogger<ApplicationDbContext> logger)
            : base(options)
        {
            this._logger = logger;
        }

        public DbSet<DocumentInfo> DocumentInfos { get; }
        public DbSet<Domain> Domains { get; }
        public DbSet<Issuer> Issuers { get; }
        public DbSet<Type> Types { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }

    
}



