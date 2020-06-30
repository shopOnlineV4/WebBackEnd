using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebAdmin.Models.ApiService;
using WebAdmin.Models.ModelView;

namespace WebAdmin.Models.Business
{
    public class TypeProductBus
    {
        private const string ModelName = "/api/TypeProduct";

        public static async Task<List<TypeProductMv>> GetAll()
        {
            var res = await ServiceApi.GetData(ModelName);
            return res.StatusCode == HttpStatusCode.OK
                ? JsonConvert.DeserializeObject<List<TypeProductMv>>(res.Content.ReadAsStringAsync().Result) : null;
        }

        internal static async Task<List<ColorCodeMv>> GetAllColorCodesAsync()
        {
            var res = await ServiceApi.GetData(ModelName + "/GetListColor");
            return res.StatusCode == HttpStatusCode.OK
                ? JsonConvert.DeserializeObject<List<ColorCodeMv>>(res.Content.ReadAsStringAsync().Result) : null;
        }

        internal static async Task<bool> CreateNewColorProductAsync(ColorCodeMv color)
        {
            var res = await ServiceApi.PostData<ColorCodeMv>(ModelName + "/AddNewColor", color);
            return res.StatusCode == HttpStatusCode.Created;
        }

        internal static async Task<bool> DeleteColorAsync(int id)
        {
            var res = await ServiceApi.DeleteDataById(ModelName + "/DeleteColor", id);
            return res.StatusCode == HttpStatusCode.OK;

        }

        internal static async Task<List<SizeMv>> GetAllSizeCodesAsync()
        {
            var res = await ServiceApi.GetData(ModelName + "/GetListSize");
            return res.StatusCode == HttpStatusCode.OK
                ? JsonConvert.DeserializeObject<List<SizeMv>>(res.Content.ReadAsStringAsync().Result) : null;
        }

        internal static async Task<bool> CreateNewSizeProductAsync(SizeMv size)
        {
            var res = await ServiceApi.PostData<SizeMv>(ModelName + "/AddNewSize", size);
            return res.StatusCode == HttpStatusCode.Created;
        }

        internal static async Task<bool> DeleteSizeAsync(int id)
        {
            var res = await ServiceApi.DeleteDataById(ModelName + "/DeleteSize", id);
            return res.StatusCode == HttpStatusCode.OK;
        }

        internal static async Task<ProductMv> GetByProductIdAsync(Guid id)
        {
            var res = await ServiceApi.GetDataById(ModelName + "/GetByProductId", id);
            return res.StatusCode == HttpStatusCode.OK ? JsonConvert.DeserializeObject<ProductMv>(res.Content.ReadAsStringAsync().Result) : null;
        }

        internal static async Task<bool> CreateNewTypeProductBusAsync(TypeProductMv typeProduct)
        {
            var res = await ServiceApi.PostData<TypeProductMv>(ModelName, typeProduct);
            return res.StatusCode == HttpStatusCode.Created;
        }

        internal static async Task<bool> DeleteTypeProductBusAsync(Guid typeProductId)
        {
            var res = await ServiceApi.DeleteDataById(ModelName, typeProductId);
            return res.StatusCode == HttpStatusCode.OK;
        }


    }
}
