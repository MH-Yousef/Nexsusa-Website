using Microsoft.AspNetCore.Mvc;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    public class WhoWeAreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
