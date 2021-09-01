using Microsoft.AspNetCore.Html;
using System.Threading.Tasks;

namespace Avaliacao.Web.Bundle
{
    /// <summary>
    /// Responsável por renderizar os bundles para html.
    /// </summary>
    public interface IBundleRenderer
    {
        Task<HtmlString> Estilo(string nomeBundle);
        Task<HtmlString> Script(string nomeBundle);
    }
}