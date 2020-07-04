using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Admin.Models.Business
{
    public class ProductBus
    {
        //private const string ModelName = "/api/Product";

        //public static async Task<List<ProductMv>> GetAll()
        //{

        //    var res = await ServiceApi.GetData(ModelName);
        //    return res.IsSuccessStatusCode ? JsonConvert.DeserializeObject<List<ProductMv>>(res.Content.ReadAsStringAsync().Result.ToString()) : null;
        //}

        //public static async Task<ProductMv> GetById(Guid id)
        //{
        //    var res = await ServiceApi.GetDataById(ModelName, id);
        //    return res.IsSuccessStatusCode ? JsonConvert.DeserializeObject<ProductMv>(res.Content.ReadAsStringAsync().Result.ToString()) : null;
        //}

        //public static async Task<bool> Post(ProductMv product)
        //{

        //    var result = await ServiceApi.PostData(ModelName, product);
        //    return result.StatusCode == HttpStatusCode.Created;
        //}

        //public static async Task<bool> Update(object id, ProductMv product )
        //{
           
        //    var result = await  ServiceApi.Update(ModelName, product.Id, product);
        //    return  result.StatusCode == HttpStatusCode.OK;
        //}

        //public static async Task<bool> DisableAsync(Guid id)
        //{
        //    var result = await ServiceApi.GetDataById(ModelName+"/Disable", id);
        //    return result.StatusCode == HttpStatusCode.OK;
        //}
    }
}
