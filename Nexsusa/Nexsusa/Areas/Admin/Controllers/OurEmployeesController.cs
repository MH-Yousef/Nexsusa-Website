using Microsoft.AspNetCore.Mvc;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    public class OurEmployeesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
