using Microsoft.AspNetCore.Mvc;

namespace SalesAnalysisPlatform.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}