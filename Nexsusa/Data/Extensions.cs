using Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            services.AddIdentityCore<IdentityUser<string>>().AddEntityFrameworkStores<AppDbContext>();
            
            
        }
    }
}
