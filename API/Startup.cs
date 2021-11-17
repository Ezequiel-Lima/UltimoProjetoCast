using API.Application.Services;
using API.Infrastructure;
using API.Infrastructure.Data;
using API.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomDbContext(Configuration);
            services.AddControllers();
            services.AddCors();
            //services.AddMvc();

            // Configurando o serviço de documentação do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CastCurso",
                    Description = "My API description for castcurso test"
                });
            });

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            services.AddScoped<IReadRepository<Curso>, ApplicationRepository<Curso>>();
            services.AddScoped<IWriteRepository<Curso>, ApplicationRepository<Curso>>();
            services.AddScoped<IReadRepository<Categoria>, ApplicationRepository<Categoria>>();
            services.AddScoped<ICursoService, CursoService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Conversor de Temperaturas");
            });

            app.UseCors(x => x.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    internal static class StartupExtensions
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDataContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                  sqlServerOptionsAction: sqlOptions =>
                  {
                      sqlOptions.MigrationsAssembly(typeof(ApplicationDataContext).GetTypeInfo().Assembly.GetName().Name);
                      //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                      sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                  }));

            return services;
        }
    }
}
