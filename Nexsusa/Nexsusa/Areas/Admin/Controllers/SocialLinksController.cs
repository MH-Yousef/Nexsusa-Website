using Core.SocialLinks;
using Microsoft.AspNetCore.Mvc;
using Services.SocialLinkServices;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SocialLinksController(ISocialLinkService _socialLinkService) : Controller
    {
        public async Task<IActionResult> List()
        {
            var response = await _socialLinkService.Get();
            return View(response.Data);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _socialLinkService.GetById(id);
            return Json(response.Data);
        }
        [HttpPost]
        public async Task<IActionResult> Manage(SocialLink socialLink)
        {
            if (socialLink.Id == 0)
            {
                var response = await _socialLinkService.Create(socialLink);
                return Json(response);
            }
            else
            {
                var response = await _socialLinkService.Update(socialLink);
                return Json(response);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _socialLinkService.Delete(id);
            return Json(response);
        }
    }
}
