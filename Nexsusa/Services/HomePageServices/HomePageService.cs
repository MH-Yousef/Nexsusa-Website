using AutoMapper;
using Core.HomePage;
using Data.Context;
using Data.Dtos.HomePageDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services.HomePageServices.ChooseUsServices;
using Services.HomePageServices.ClientSaysServices;
using Services.HomePageServices.FooterServices;
using Services.HomePageServices.HomePageInfoServices;
using Services.HomePageServices.NavBarItemServices;
using Services.HomePageServices.OurCompanyServices;
using Services.HomePageServices.OurEmployeeServices;
using Services.HomePageServices.RegularBlogsServices;
using Services.HomePageServices.ServiceServices;
using Services.HomePageServices.SliderServices;
using Services.HomePageServices.WhoWeAreServices;
using Services.HomePageServices.WorkingProcessServices;
using Services.HomePageServices.WorkShowCaseServices;

namespace Services.HomePageServices
{
    public class HomePageService : BaseService, IHomePageServices
    {
        private readonly INavBarItemService _navBarItemService;
        private readonly ISliderService _sliderService;
        private readonly IServiceService _serviceService;
        private readonly IOurCompanyService _ourCompanyService;
        private readonly IChooseUsService _chooseUsService;
        private readonly IWorkingProcessService _workingProcessService;
        private readonly IWorkShowCaseService _workShowCaseService;
        private readonly IWhoWeAreService _whoWeAreService;
        private readonly IOurEmployeesService _ourEmployeesService;
        private readonly IClientSaysService _clientSaysService;
        private readonly IRegularBlogsService _regularBlogsService;
        private readonly IFooterService _footerService;
        private readonly IHomePageInfoService _homePageInfoService;
        public HomePageService(AppDbContext dbContext, IMapper mapper, INavBarItemService navBarItemService, ISliderService sliderService, IOurCompanyService ourCompanyService, IWorkingProcessService workingProcessService, IWorkShowCaseService workShowCaseService, IWhoWeAreService whoWeAreService, IOurEmployeesService ourEmployeesService, IClientSaysService clientSaysService, IRegularBlogsService regularBlogsService, IFooterService footerService, IServiceService serviceService, IChooseUsService chooseUsService, IHomePageInfoService homePageInfoService = null) : base(dbContext, mapper)
        {
            _navBarItemService = navBarItemService;
            _sliderService = sliderService;
            _ourCompanyService = ourCompanyService;
            _workingProcessService = workingProcessService;
            _workShowCaseService = workShowCaseService;
            _whoWeAreService = whoWeAreService;
            _ourEmployeesService = ourEmployeesService;
            _clientSaysService = clientSaysService;
            _regularBlogsService = regularBlogsService;
            _footerService = footerService;
            _serviceService = serviceService;
            _chooseUsService = chooseUsService;
            _homePageInfoService = homePageInfoService;
        }

        public async Task<ResponseResult<HomePageDTO>> GetHomePage(int languageId)
        {
            try
            {
                var model = new HomePageDTO
                {
                    HomePageInfo = (await _homePageInfoService.GetHomePageInfoAsync(languageId)).Data,
                    NavBarItems = (await _navBarItemService.GetList(languageId)).Data,
                    Slider = (await _sliderService.GetList(languageId)).Data.FirstOrDefault(),
                    Services = (await _serviceService.GetList(languageId)).Data.FirstOrDefault(),
                    OurCompany = (await _ourCompanyService.GetList(languageId)).Data.FirstOrDefault(),
                    ChooseUs = (await _chooseUsService.GetList(languageId)).Data.FirstOrDefault(),
                    WorkingProcess = (await _workingProcessService.GetList(languageId)).Data.FirstOrDefault(),
                    WorkShowCase = (await _workShowCaseService.GetList(languageId)).Data.FirstOrDefault(),
                    WhoWeAre = (await _whoWeAreService.GetList(languageId)).Data.FirstOrDefault(),
                    OurEmployees = (await _ourEmployeesService.GetList(languageId)).Data.FirstOrDefault(),
                    ClientSays = (await _clientSaysService.GetList(languageId)).Data.FirstOrDefault(),
                    RegularBlogs = (await _regularBlogsService.GetList(languageId)).Data.FirstOrDefault(),
                    Footer = (await _footerService.GetList(languageId)).Data.FirstOrDefault()
                };
                return Success(model);
            }
            catch (Exception ex)
            {
                return Error<HomePageDTO>(ex);
            }
        }
       // get Homne page info
        public async Task<ResponseResult<HomePageInfoDTO>> GetHomePageInfo()
        {
            try
            {
                var model = await _dbContext.HomePageInfos.FirstOrDefaultAsync();
                var dto = _mapper.Map<HomePageInfoDTO>(model);
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<HomePageInfoDTO>(ex);
            }
        }
        public async Task<ResponseResult<HomePageInfo>> Create(HomePageInfo model)
        {
            try
            {
                await _dbContext.HomePageInfos.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return Success(model);
            }
            catch (Exception ex)
            {
                return Error<HomePageInfo>(ex);
            }
        }
        public async Task<ResponseResult<HomePageInfo>> Update(HomePageInfo model)
        {
            try
            {
                _dbContext.HomePageInfos.Update(model);
                await _dbContext.SaveChangesAsync();
                return Success(model);
            }
            catch (Exception ex)
            {
                return Error<HomePageInfo>(ex);
            }
        }
    }
}
