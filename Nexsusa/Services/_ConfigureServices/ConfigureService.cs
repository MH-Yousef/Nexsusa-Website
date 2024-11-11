using Microsoft.Extensions.DependencyInjection;
using Services._GenericServices;
using Services.HomePageServices.NavBarItemServices;
using Services.LanguageServices;

namespace Services._ConfigureServices
{
    public static class ConfigureService
    {
        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            #region Language

            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<INavBarItemService, NavBarItemService>();

            #endregion
            services.AddScoped(typeof(GenericService<>));
        }
    }
}
