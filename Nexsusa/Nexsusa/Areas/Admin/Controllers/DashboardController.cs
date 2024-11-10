using Microsoft.AspNetCore.Mvc;

namespace Nexsusa_Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
