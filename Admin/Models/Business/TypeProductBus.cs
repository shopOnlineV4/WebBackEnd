using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Admin.Models.ApiService;
using ModelViews;
using Newtonsoft.Json;


namespace Admin.Models.Business
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

        public static async Task<List<ColorCodeMv>> GetAllColorCodesAsync()
        {
            var res = await ServiceApi.GetData(ModelName + "/Color");
            return res.StatusCode == HttpStatusCode.OK
                ? JsonConvert.DeserializeObject<List<ColorCodeMv>>(res.Content.ReadAsStringAsync().Result) : null;
        }

        public static async Task<bool> CreateNewColorProductAsync(ColorCodeMv color)
        {
            var res = await ServiceApi.PostData<ColorCodeMv>(ModelName + "/Color", color);
            return res.StatusCode == HttpStatusCode.Created;
        }

        public static async Task<bool> DeleteColorAsync(int id)
        {
            var res = await ServiceApi.DeleteDataById(ModelName + "/Color", id);
            return res.StatusCode == HttpStatusCode.OK;

        }

        public static async Task<List<SizeMv>> GetAllSizeCodesAsync()
        {
            var res = await ServiceApi.GetData(ModelName + "/Size");
            return res.StatusCode == HttpStatusCode.OK
                ? JsonConvert.DeserializeObject<List<SizeMv>>(res.Content.ReadAsStringAsync().Result) : null;
        }

        public static async Task<bool> CreateNewSizeProductAsync(SizeMv size)
        {
            var res = await ServiceApi.PostData<SizeMv>(ModelName + "/Size", size);
            return res.StatusCode == HttpStatusCode.Created;
        }

        public static async Task<bool> DeleteSizeAsync(int id)
        {
            var res = await ServiceApi.DeleteDataById(ModelName + "/Size", id);
            return res.StatusCode == HttpStatusCode.OK;
        }

        public static async Task<ProductMv> GetByProductIdAsync(Guid id)
        {
            var res = await ServiceApi.GetDataById(ModelName + "/GetTypeProductByProductId", id);
            return res.StatusCode == HttpStatusCode.OK ? JsonConvert.DeserializeObject<ProductMv>(res.Content.ReadAsStringAsync().Result) : null;
        }

        public static async Task<bool> CreateNewTypeProductBusAsync(TypeProductInput typeProduct)
        {
            var res = await ServiceApi.PostData<TypeProductInput>(ModelName, typeProduct);
            return res.StatusCode == HttpStatusCode.Created;
        }

        public static async Task<bool> DeleteTypeProductBusAsync(Guid typeProductId)
        {
            var res = await ServiceApi.DeleteDataById(ModelName, typeProductId);
            return res.StatusCode == HttpStatusCode.OK;
        }


    }
}
