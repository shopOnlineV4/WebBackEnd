using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.EF;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Config
{
    public static class IdentityConfig 
    {
        public static void ConfigIdentity(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
                {
                    options.User.RequireUniqueEmail = false;
                })
                .AddEntityFrameworkStores<WebOnlineDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequireDigit = true;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequiredLength = 8;
                config.Password.RequireUppercase = false;
                
            });
        }
    }
}
