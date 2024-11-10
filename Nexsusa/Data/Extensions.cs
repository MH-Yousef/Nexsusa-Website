using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Data
{
    public static class Extensions
    {
        public static void Dependencies(this IServiceCollection services,IConfiguration configuration)
        {
          
            services.AddSingleton<ConnectionStrings>();

            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                var connectionStrings = serviceProvider.GetRequiredService<IOptions<ConnectionStrings>>().Value;
                options.UseSqlServer(connectionStrings.DefaultConnection);
            });
            
            //services.AddIdentityCore<IdentityUser<string>>().AddEntityFrameworkStores<AppDbContext>();


        }
    }
}
