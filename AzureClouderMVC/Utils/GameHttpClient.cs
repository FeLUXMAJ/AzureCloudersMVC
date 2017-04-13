using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace AzureClouderMVC.Utils
{
    public static class GameHttpClient
    {
        public static async Task<IList<T>> GetAsync<T>(string uri, string name, string value)
        {
            string responseString;

            using (var client = new HttpClient())
            {
                var completeUri = QueryHelpers.AddQueryString(uri, name, value);
                var response = await client.GetAsync(completeUri);
                responseString = await response.Content.ReadAsStringAsync();
            }

            var responseJson = JsonConvert.DeserializeObject<IList<T>>(
                responseString,
                    new JsonSerializerSettings());

            return responseJson;
        }
    }
}
