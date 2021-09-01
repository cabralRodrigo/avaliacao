using System.Threading.Tasks;

namespace Avaliacao.Web.Bundle
{
    /// <summary>
    /// Responsável por gerenciar o carregamento das informações dos bundles.
    /// </summary>
    public interface IBundleProvider
    {
        Task<InformacaoBundle> BuscarBundlePorNome(string nomeBundle);
    }
}