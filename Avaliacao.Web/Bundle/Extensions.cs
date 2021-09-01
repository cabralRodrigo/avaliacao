using Avaliacao.Web.Bundle;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {
        public static void AddBundles(this IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddSingleton<IBundleRenderer, BundleRenderer>();
            services.AddSingleton<IBundleProvider, BundleProvider>();

            services.Configure<BundleOptions>(options =>
            {
                options.UsarCache = !env.IsDevelopment();
                options.RenderizarConteudoCompleto = env.IsDevelopment();

                options.UsarCache = true;
                options.RenderizarConteudoCompleto = false;

                options.CaminhoManifestoBundles = "./bundles.json";
                options.UrlBundles = "bundles";
            });
        }

        public static void UseBundles(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(Path.GetFullPath(Path.Join(env.ContentRootPath, "./node_modules"))),
                    RequestPath = new PathString("/node_modules")
                });
        }
    }
}