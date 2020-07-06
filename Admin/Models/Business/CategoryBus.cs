using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Admin.Models.ApiService;
using ModelViews;
using ModelViews.Enum;
using Newtonsoft.Json;

namespace Admin.Models.Business
{
    public class CategoryBus
    {
        private const string ModelName = "/api/category";

        public static async Task<List<CategoryMv>> GetAll()
        {
            var res = await ServiceApi.GetData(ModelName);
            return res.StatusCode == HttpStatusCode.OK ?
                JsonConvert.DeserializeObject<List<CategoryMv>>(res.Content.ReadAsStringAsync().Result.ToString()) : null;
        }

        //Get Category byId
        public static async Task<CategoryMv> GetById(Guid id)
        {
            var res = await ServiceApi.GetDataById(ModelName, id);
            return res.StatusCode == HttpStatusCode.OK ?
                JsonConvert.DeserializeObject<CategoryMv>(res.Content.ReadAsStringAsync().Result.ToString()) : null;
        }

        //Get Category byId
        public static async Task<List<CategoryMv>> GetByParrentId(Guid id)
        {
            var res = await ServiceApi.GetData(ModelName);
            return res.StatusCode == HttpStatusCode.OK ?
                JsonConvert.DeserializeObject<List<CategoryMv>>(res.Content.ReadAsStringAsync().Result.ToString()).Where(x => x.SubCategoryId == id).ToList() : null;
        }
        //Update
        public static bool UpdateCategory(Guid id, CategoryInput category)
        {
            category.CategoryParent = TypeCategories.Child;
            var res = ServiceApi.Update<CategoryInput>(ModelName, id, category).Result;
            return res.StatusCode == HttpStatusCode.OK;
        }

        public static bool CreateChild(CategoryInput category)
        {
            category.CategoryParent = TypeCategories.Child;
            var res = ServiceApi.PostData<CategoryInput>(ModelName, category).Result;
            return res.StatusCode == HttpStatusCode.Created;
        }
    }
}
