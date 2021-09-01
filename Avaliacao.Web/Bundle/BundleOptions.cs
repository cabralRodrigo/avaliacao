namespace Avaliacao.Web.Bundle
{
    public class BundleOptions
    {
        /// <summary>
        /// Informa ao <see cref="IBundleProvider"/> se ele deve carregar o manifesto dos bundles toda vez que for necessário ou se irá cachear o arquivo.
        /// </summary>
        public bool UsarCache { get; set; }
        
        /// <summary>
        /// Informa ao <see cref="IBundleRenderer"/> se ele deve renderizar o bundle em um arquivo só ou deve renderizar uma tag para cada arquivo do bundle.
        /// </summary>
        public bool RenderizarConteudoCompleto { get; set; }

        /// <summary>
        /// Url utilizada para carregar os bundles no navegador.
        /// </summary>
        public string UrlBundles { get; set; }

        /// <summary>
        /// Caminho para o manifesto dos bundles.
        /// </summary>
        public string CaminhoManifestoBundles { get; set; }
    }
}