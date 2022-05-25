using Microsoft.AspNetCore.Mvc;

namespace cspv3.Controllers
{
    public class DocsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
            public IActionResult FAQs()
        {
            return View();
        }
            public IActionResult PrivacyPolicy()
        {
            return View();
        }
            public IActionResult TermsAndConditions()
        {
            return View();
        }

        public IActionResult Help()
        {
            return View();
        }
    }
}