using Admin.Models.ApiService;
using ModelViews;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Admin.Models.Business
{
    public class ImageBus
    {
        private const string ModelName = "/api/ImageProduct";

        internal async static Task<bool> PostImage(ImageInputMv imageProduct)
        {
            var res = await ServiceApi.PostData<ImageInputMv>(ModelName, imageProduct);
            return res.StatusCode == HttpStatusCode.Created;
        }

        internal static async Task<ProductMv> GetByProductId(Guid productId)
        {
            var res = await ServiceApi.GetDataById(ModelName + "/GetByProductId", productId);
            return res.StatusCode == HttpStatusCode.OK
                ? JsonConvert.DeserializeObject<ProductMv>(res.Content.ReadAsStringAsync().Result.ToString()) : null;
        }

        internal static async Task<bool> DeleteImage(Guid id)
        {
            var res = await ServiceApi.DeleteDataById(ModelName, id);
            return res.StatusCode == HttpStatusCode.OK;
        }
    }

}

