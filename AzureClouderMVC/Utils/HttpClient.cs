using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace AzureClouderMVC.Utils
{
    public class GameHttpClient
    {
        public static async Task<IList<T>> GetAsync<T>(string uri, Dictionary<string, string> headers, Dictionary<string, string> parameters)
        {
            string responseString;

            using (var client = new HttpClient())
            {
                foreach (var header in headers)
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);

                var completeUri = QueryHelpers.AddQueryString(uri, parameters);
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
