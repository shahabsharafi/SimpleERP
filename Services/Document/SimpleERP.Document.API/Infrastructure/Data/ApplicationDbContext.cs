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
        public DbSet<Domain> Domain { get; }
        public DbSet<Issuer> Issuer { get; }
        public DbSet<Type> Type { get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var schema = "DocumentManagement";
            builder.Entity<DocumentInfo>().ToTable("DocumentInfos", schema);
            builder.Entity<Domain>().ToTable("DocumentInfos", schema);
            builder.Entity<Issuer>().ToTable("DocumentInfos", schema);
            builder.Entity<Type>().ToTable("DocumentInfos", schema);

        }
    }
}



