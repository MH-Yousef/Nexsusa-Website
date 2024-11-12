using Microsoft.AspNetCore.Mvc;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NavbarController : Controller
    {
        public IActionResult Navbar()
        {
            return View();
        }
    }
}
