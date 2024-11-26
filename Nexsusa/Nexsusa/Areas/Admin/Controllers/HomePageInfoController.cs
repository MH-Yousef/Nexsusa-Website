using AutoMapper;
using Core.HomePage;
using Data.Context;
using Data.Dtos.HomePageDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services.HomePageServices;
using Services.HomePageServices.HomePageInfoServices;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomePageInfoController : Controller
    {
        private readonly IHomePageServices _homePageServices;
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;
        private readonly IHomePageInfoService _homePageInfoService;

        public HomePageInfoController(IHomePageServices homePageServices, AppDbContext appDbContext = null, IMapper mapper = null, IHomePageInfoService homePageInfoService = null)
        {
            _homePageServices = homePageServices;
            _appDbContext = appDbContext;
            _mapper = mapper;
            _homePageInfoService = homePageInfoService;
        }

        public async Task<IActionResult> Manage()
        {
            ViewBag.Languages=await _appDbContext.Languages.ToListAsync();
            var response = await _homePageServices.GetHomePageInfo();
            if (response.IsSuccess)
            {
                return View(response.Data);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Save(HomePageInfoDTO dto)
        {
            try
            {
                bool isInputsNull = dto.MetaPublisher == null || dto.PhoneNumber == null || dto.Title == null || dto.MetaDescription == null || dto.MetaKeywords == null || dto.MetaAuthor == null || dto.Email == null;
                if (isInputsNull)
                {
                    return Json(new { success = false, message = "All Fields must be filled." });
                }
                var response=  await _homePageInfoService.ManageAsync(dto);
                if (response.IsSuccess)
                {
                return Json(new { success = true });

                }
                else
                {
                    return Json(new { success = false, message = "Home Page Info doesn't updated!!!!" });
                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.GetError() });

            }


        }

        [HttpGet]
        public async Task<IActionResult> GetHomePageInfo(int langId)
        {
            try
            {
                var info = await _homePageInfoService.GetHomePageInfoAsync(langId);
                return Json(new { success = true, data = info.Data });
            }
            catch (Exception ex)
            {

                return Json(new { success = false , message=ex.GetError()});
            }
         
        }

    }
}
