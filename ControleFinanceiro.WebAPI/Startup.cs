using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.DAL;
using ControleFinanceiro.DAL.Interfaces;
using ControleFinanceiro.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ControleFinanceiro.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("Local");

            string pathLog = Configuration.GetSection("Logging").GetSection("Path").Value;
            string aplicacao = Configuration.GetSection("Logging").GetSection("NomePastaLog").Value;

            services.AddDbContext<Contexto>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IPlanosDAL, PlanosDAL>();
            services.AddScoped<ICiclosDAL, CiclosDAL>();
            services.AddScoped<IGastosDAL, GastosDAL>();
            services.AddScoped<IGanhosDAL, GanhosDAL>();

            services.AddSingleton<IGravadorLog, GravadorLog>(a => new GravadorLog(pathLog, aplicacao));            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
