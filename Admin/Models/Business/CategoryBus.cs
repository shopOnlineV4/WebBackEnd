using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Models.ApiService;
using ModelViews;
using Newtonsoft.Json;

namespace Admin.Models.Business
{
    public class CategoryBus
    {
        private const string ModelName = "/api/category";

        public static async Task<List<CategoryMv>> GetAll()
        {
            var res = await ServiceApi.GetData(ModelName);
            return res.IsSuccessStatusCode ? JsonConvert.DeserializeObject<List<CategoryMv>>(res.Content.ReadAsStringAsync().Result.ToString()) : null;
        }
    }
}
