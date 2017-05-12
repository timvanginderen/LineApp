using MijnLijn.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace MijnLijn.Data.Remote
{
    public class RestService : IRestService
    {
        HttpClient client;

        public List<Line> Lines { get; private set; }

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Add("x-signature", Constants.ApiSignature);
            client.DefaultRequestHeaders.Add("x-token", Constants.ApiToken);
        }

        public async Task<ApiResponse> PostToGetLines()
        {
            var uri = new Uri(string.Format(Constants.ApiUrl));
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                var keyValues = new List<KeyValuePair<string, string>>();
                keyValues.Add(new KeyValuePair<string, string>("stopID", "105694"));
                keyValues.Add(new KeyValuePair<string, string>("os", "android"));

                HttpContent content = new FormUrlEncodedContent(keyValues);

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(@"				Success, content: " + responseContent.ToString());
                    apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return apiResponse;
        }
    }
}
