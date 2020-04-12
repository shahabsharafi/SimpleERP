using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleERP.Document.API.Infrastructure;
using SimpleERP.Document.API.Infrastructure.Contracts;
using SimpleERP.Document.API.Infrastructure.Data;
using SimpleERP.Document.API.Infrastructure.Repositories;
using SimpleERP.Libraries.API.Filters;
using SimpleERP.Libraries.Infrastructure.Commons;
using SimpleERP.Libraries.Infrastructure.Excel;
using SimpleERP.Libraries.Infrastructure.QueryHandler;

namespace SimpleERP.Document.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
            builder.SetBasePath(env.ContentRootPath)
                   //add configuration.json  
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true) // Duplicate == Json1
                   .AddJsonFile("resources.json", optional: false, reloadOnChange: true)
                   .AddEnvironmentVariables();

            Configuration = builder.Build();

            if (string.IsNullOrWhiteSpace(env.WebRootPath))
            {
                env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\UI\\SimpleERP.UI.Web", "wwwroot");
            }
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {            
            services.AddDbContext<ApplicationDbContext>(options => options
                .UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("SimpleERP.Document.API")
            ));

            services.AddSingleton(Configuration);

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IIssuerRepository, IssuerRepository>();
            services.AddScoped<IDomainRepository, DomainRepository>();
            services.AddScoped<ITypeRepository, TypeRepository>();
            services.AddScoped<IDocumentInfoRepository, DocumentInfoRepository>();
            services.AddScoped<IDocumentFileRepository, DocumentFileRepository>();
            services.AddScoped<IUnitOfRepository, UnitOfRepository>();
            services.AddScoped<IQueryHandler, AgGridQueryHandler>();
            services.AddScoped<IResourceManager, ResourceManager>();
            services.AddScoped<IExcelHelper, ExcelHelper>();

            services.AddCors();
            
            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(QueryHandlerFilterAction));
                options.Filters.Add(typeof(ExcelFilterAction));
                options.Filters.Add(typeof(ApiResultFilterAttribute));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });

            app.UseMvc();
        }

        
    }
}
