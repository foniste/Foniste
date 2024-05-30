using Microsoft.AspNetCore.Mvc;

namespace Foniste.Controllers
{
    public class MissionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
