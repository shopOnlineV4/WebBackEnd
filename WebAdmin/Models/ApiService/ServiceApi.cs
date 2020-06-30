using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebAdmin.Common;

namespace WebAdmin.Models.ApiService
{
    public class ServiceApi
    {
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri(ConstKey.Base_URL)
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

        public static async Task<HttpResponseMessage> PostData<T>(string url, T data)
        {
            var res = await _client.PostAsJsonAsync(url, data);
            return res;
        }

        public static async Task<HttpResponseMessage> GetDataById(string url, object id)
        {
            var res = await _client.GetAsync(url + "/" + id.ToString());
            return res;
        }

        public static async Task<HttpResponseMessage> DeleteDataById(string url, object id)
        {
            var res = await _client.DeleteAsync(url + "/" + id.ToString());
            return res;
        }

        public static async Task<HttpResponseMessage> Update<T>(string url, object id, T data)
        {
            var res = await _client.PutAsJsonAsync(url + "/" + id, data);
            return res;
        }

        
    }
}