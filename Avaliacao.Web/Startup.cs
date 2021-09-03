using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Avaliacao.Web
{
    public class Startup
    {
        private readonly IWebHostEnvironment environment;
        private readonly string connectionString;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.environment = environment ?? throw new ArgumentNullException(nameof(environment));
            this.connectionString = configuration.GetValue<string>("ConnectionString");

            if (string.IsNullOrWhiteSpace(this.connectionString))
                throw new Exception("Não foi possível encontrar uma connection string válida no arquivo de configuração.");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            
            services.AddBundles(this.environment);
            services.AddNHibernate(this.connectionString);
            services.AddRepositorios();
            services.AddServicos();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (this.environment.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseBundles(this.environment);
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllerRoute(name: "default", pattern: "{controller=Cliente}/{action=Index}/{id?}"));
        }
    }
}