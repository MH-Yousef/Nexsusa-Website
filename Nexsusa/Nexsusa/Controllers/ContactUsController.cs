using Microsoft.AspNetCore.Mvc;

namespace Nexsusa_Api.Controllers
{
    public class ContactUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
