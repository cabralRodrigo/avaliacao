using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Avaliacao.Web.Bundle
{
    public class BundleRenderer : IBundleRenderer
    {
        private readonly IBundleProvider bundleProvider;
        private readonly IWebHostEnvironment environment;
        private readonly BundleOptions bundleOptions;

        public BundleRenderer(IBundleProvider bundleProvider, IWebHostEnvironment environment, IOptions<BundleOptions> bundleOptions)
        {
            this.bundleProvider = bundleProvider ?? throw new ArgumentNullException(nameof(bundleProvider));
            this.environment = environment ?? throw new ArgumentNullException(nameof(environment));
            this.bundleOptions = bundleOptions?.Value ?? throw new ArgumentNullException(nameof(bundleOptions));
        }

        public async Task<HtmlString> Estilo(string nomeBundle)
        {
            var bundle = await this.bundleProvider.BuscarBundlePorNome(nomeBundle);
            if (bundle.Tipo != "estilo")
                throw new ArgumentException($"O bundle '{nomeBundle}' deve ser do tipo 'estilo' para ser renderizado como um estilo.", nameof(nomeBundle));

            if (this.bundleOptions.RenderizarConteudoCompleto)
            {
                var tags = new StringBuilder();

                foreach (var arquivo in bundle.Conteudo)
                    tags.AppendLine($"<link rel=\"stylesheet\" type=\"text/css\" href=\"{this.ConverterCaminhoParaUrl(arquivo)}\"/>");

                return new HtmlString(tags.ToString());
            }
            else
                return new HtmlString($"<link rel=\"stylesheet\" type=\"text/css\" href=\"{this.ConverterCaminhoParaUrl(Path.Combine(this.bundleOptions.UrlBundles, bundle.Nome))}\"/>");
        }

        public async Task<HtmlString> Script(string nomeBundle)
        {
            var bundle = await this.bundleProvider.BuscarBundlePorNome(nomeBundle);
            if (bundle.Tipo != "script")
                throw new ArgumentException($"O bundle '{nomeBundle}' deve ser do tipo 'script' para ser renderizado como um script.", nameof(nomeBundle));

            if (this.bundleOptions.RenderizarConteudoCompleto)
            {
                var tags = new StringBuilder();

                foreach (var arquivo in bundle.Conteudo)
                    tags.AppendLine($"<script type=\"text/javascript\" src=\"{this.ConverterCaminhoParaUrl(arquivo)}\"></script>");

                return new HtmlString(tags.ToString());
            }
            else
                return new HtmlString($"<script type=\"text/javascript\" src=\"{this.ConverterCaminhoParaUrl(Path.Combine(this.bundleOptions.UrlBundles, bundle.Nome))}\"></script>");
        }

        private string ConverterCaminhoParaUrl(string caminho)
        {
            //É necessário resolver o caminho completo para normalizar os / e \ no caminho.
            var caminhoCompleto = Path.GetFullPath(Path.Combine(this.environment.ContentRootPath, caminho));
            var caminhoRelativo = Path.GetRelativePath(this.environment.ContentRootPath, caminhoCompleto);
            return $"/{caminhoRelativo.Replace("\\", "/")}";
        }
    }
}