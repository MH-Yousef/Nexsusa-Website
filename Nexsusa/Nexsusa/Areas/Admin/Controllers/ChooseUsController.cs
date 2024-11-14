using Microsoft.AspNetCore.Mvc;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    public class ChooseUsController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
