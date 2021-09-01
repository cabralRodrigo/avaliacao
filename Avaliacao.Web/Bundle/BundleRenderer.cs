using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Avaliacao.Web.Bundle
{
    public class BundleRenderer : IBundleRenderer
    {
        private readonly IBundleProvider bundleProvider;
        private readonly BundleOptions bundleOptions;

        public BundleRenderer(IBundleProvider bundleProvider, IOptions<BundleOptions> bundleOptions)
        {
            this.bundleProvider = bundleProvider ?? throw new ArgumentNullException(nameof(bundleProvider));
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
                    tags.AppendLine($"<link rel=\"stylesheet\" type=\"text/css\" href=\"{arquivo}\"/>");
                
                return new HtmlString(tags.ToString());
            }
            else
                return new HtmlString($"<link rel=\"stylesheet\" type=\"text/css\" href=\"{this.bundleOptions.UrlBundles}/{bundle.Nome}\"/>");
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
                    tags.AppendLine($"<script type=\"text/javascript\" src=\"{arquivo}\"></script>");

                return new HtmlString(tags.ToString());
            }
            else
                return new HtmlString($"<script type=\"text/javascript\" src=\"{this.bundleOptions.UrlBundles}/{bundle.Nome}\"></script>");
        }
    }
}