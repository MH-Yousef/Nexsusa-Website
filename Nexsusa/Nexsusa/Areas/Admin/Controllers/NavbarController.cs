using Data.Dtos.NavBarDTOs;
using Microsoft.AspNetCore.Mvc;
using Services.HomePageServices.NavBarItemServices;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NavbarController(INavBarItemService _navBarItemService) : Controller
    {
        public async Task<IActionResult> Navbar()
        {
            //var currentLanguage = HttpContext.Session.GetString("CurrentLanguage");
            var result = (await _navBarItemService.GetList(1)).Data;
            return View(result);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _navBarItemService.GetById(id, 1);
            return View(model.Data);
        }
        public async Task<IActionResult> Save(NavBarItemDTO dto)
        {
            return Json(dto);
        }
    }
}
