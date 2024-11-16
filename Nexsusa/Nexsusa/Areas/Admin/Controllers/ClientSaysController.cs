using Microsoft.AspNetCore.Mvc;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    public class ClientSaysController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
