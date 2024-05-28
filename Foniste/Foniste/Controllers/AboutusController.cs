using Microsoft.AspNetCore.Mvc;

namespace Foniste.Controllers
{
    public class AboutusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
