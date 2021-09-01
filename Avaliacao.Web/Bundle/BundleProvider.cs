using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Avaliacao.Web.Bundle
{
    public class BundleProvider : IBundleProvider
    {
        private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        private readonly BundleOptions bundleOptions;
        private InformacaoBundle[] cacheBundles;

        public BundleProvider(IOptions<BundleOptions> bundleOptions)
        {
            this.bundleOptions = bundleOptions?.Value ?? throw new ArgumentNullException(nameof(bundleOptions));
        }

        public Task<InformacaoBundle> BuscarBundlePorNome(string nomeBundle)
        {
            if (this.bundleOptions.UsarCache)
                return this.BuscarBundlePorNomeComCache(nomeBundle);
            else
                return this.BuscarBundlePorNomeSemCache(nomeBundle);
        }

        private async Task<InformacaoBundle> BuscarBundlePorNomeComCache(string nomeBundle)
        {
            if (this.cacheBundles is null)
                this.cacheBundles = await this.CarregarJsonBundles();

            var bundle = this.cacheBundles.SingleOrDefault(s => s.Nome == nomeBundle);

            if (bundle is null)
                throw new ArgumentException($"Não foi possível encontrar o bundle com o nome '{nomeBundle}'.", nameof(nomeBundle));

            return bundle;
        }

        private async Task<InformacaoBundle> BuscarBundlePorNomeSemCache(string nomeBundle)
        {
            var bundles = await this.CarregarJsonBundles();
            var bundle = bundles.SingleOrDefault(s => s.Nome == nomeBundle);

            if (bundle is null)
                throw new ArgumentException($"Não foi possível encontrar o bundle com o nome '{nomeBundle}'.", nameof(nomeBundle));

            return bundle;
        }

        private async Task<InformacaoBundle[]> CarregarJsonBundles()
        {
            try
            {
                using var stream = File.OpenRead(this.bundleOptions.CaminhoManifestoBundles);
                return await JsonSerializer.DeserializeAsync<InformacaoBundle[]>(stream, JsonOptions).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar as informações sobre os bundles do sistema.", ex);
            }
        }
    }
}