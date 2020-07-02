
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Common
{
    public class ConstString
    {
        //private static IConfiguration configuration { get; }
        public static readonly string BaseLocationImageLink = "https://localhost:44355/productImages/";
        //public static byte[] key = Encoding.UTF8.GetBytes(configuration["ApplicationSetting:JWT_Secret"].ToString());

    }
}
