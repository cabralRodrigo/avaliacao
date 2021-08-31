using Microsoft.AspNetCore.Mvc;

namespace Avaliacao.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => this.View();
    }
}