using Microsoft.Extensions.DependencyInjection;
using Services._GenericServices;
using Services.AboutServices;
using Services.ContactUsPageServices;
using Services.HomePageServices;
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
using Services.ImageServices;
using Services.LanguageServices;
using Services.ServicePageServices;
using Services.SocialLinkServices;

namespace Services._ConfigureServices
{
    public static class ConfigureService
    {
        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            #region Language

            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IHomePageServices, HomePageService>();
            services.AddScoped<IAboutService, AboutService>();
            services.AddScoped<IServicePageService, ServicePageService>();

            services.AddScoped<ISocialLinkService, SocialLinkService>();
            

            services.AddScoped<INavBarItemService, NavBarItemService>();
            services.AddScoped<IChooseUsService, ChooseUsService>();
            services.AddScoped<IClientSaysService, ClientSaysService>();
            services.AddScoped<IFooterService, FooterService>();
            services.AddScoped<IOurCompanyService, OurCompanyService>();
            services.AddScoped<IOurEmployeesService, OurEmployeesService>();
            services.AddScoped<IRegularBlogsService, RegularBlogsService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<IWhoWeAreService, WhoWeAreService>();
            services.AddScoped<IWorkingProcessService, WorkingProcessService>();
            services.AddScoped<IWorkShowCaseService, WorkShowCaseService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IContactUsService, ContactUsService>();
            services.AddScoped<IHomePageInfoService,HomePageInfoService>();
            #endregion
            services.AddScoped(typeof(GenericService<>));
        }
    }
}
