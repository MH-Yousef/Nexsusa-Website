using Microsoft.AspNetCore.Mvc;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    public class ContactUsController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
