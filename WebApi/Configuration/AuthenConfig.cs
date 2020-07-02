using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Configuration
{
    public static class AuthenConfig
    {
        public static void ConfigAuthen(this IServiceCollection services)
        {
   //        services.AddAuthentication(x=> x.DefaultAuthenticateScheme = JwtBearerDefault)
        }
    }
}
