using Microsoft.AspNetCore.Mvc;

namespace ApiPanelDevExpress.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
