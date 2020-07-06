using Admin.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace Admin.Models.ApiService
{
    public class ServiceApi
    {
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri(ConstKey.Base_URL),

        };

        public static HttpClient ApiClient()
        {
            return _client;
        }

        public static async Task<HttpResponseMessage> GetData(string url)
        {

            var res = await _client.GetAsync(url);
            return res;
        }

        public static async Task<HttpResponseMessage> PostData<T>(string url, T data, string token = null)
        {
            if (token != null) _client.DefaultRequestHeaders.Authorization
                 = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var res = await _client.PostAsync(url, content);
            return res;
        }

        public static async Task<HttpResponseMessage> GetDataById(string url, object id)
        {
            var res = await _client.GetAsync(url + "/" + id.ToString());
            return res;
        }

        public static async Task<HttpResponseMessage> DeleteDataById(string url, object id, string token = null)
        {
            if (token != null) _client.DefaultRequestHeaders.Authorization
                 = new AuthenticationHeaderValue("Bearer", token);
            var res = await _client.DeleteAsync(url + "/" + id.ToString());
            return res;
        }

        public static async Task<HttpResponseMessage> Update<T>(string url, object id, T data, string token = null)
        {
            if (token != null) _client.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", token);
            StringContent content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var res = await _client.PutAsync(url + "/" + id, content);
            return res;
        }

    }
}