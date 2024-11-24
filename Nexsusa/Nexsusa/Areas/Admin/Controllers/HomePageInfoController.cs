using AutoMapper;
using Core.HomePage;
using Data.Context;
using Data.Dtos.HomePageDTOs;
using Microsoft.AspNetCore.Mvc;
using Services.HomePageServices;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomePageInfoController : Controller
    {
        private readonly IHomePageServices _homePageServices;
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public HomePageInfoController(IHomePageServices homePageServices, AppDbContext appDbContext = null, IMapper mapper = null)
        {
            _homePageServices = homePageServices;
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<IActionResult> Manage()
        {
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
                dto.UpdatedDate = DateTime.Now;
                var entity = _mapper.Map<HomePageInfoDTO, HomePageInfo>(dto);
                _appDbContext.Update(entity);
                await _appDbContext.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {

                throw ex;
            }








        }
    }
}
