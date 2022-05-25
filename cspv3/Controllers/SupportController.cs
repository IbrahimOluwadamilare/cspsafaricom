using Microsoft.AspNetCore.Mvc;

namespace cspv3.Controllers
{
    public class SupportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    } 
}