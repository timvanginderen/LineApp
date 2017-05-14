using MijnLijn.Models;
using MijnLijn.Utils;
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
        private const string FIELD_KEY_STOP_ID = "stopID";
        private const string FIELD_KEY_OS = "os";
        private const string FIELD_VALUE_OS = "android";

        HttpClient client;

        public List<Line> Lines { get; private set; }

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Add("x-signature", Constants.ApiSignature);
        }

        public async Task<ApiResponse> PostToGetLines(int[] stopNumbers)
        {
            AddRequestToken(stopNumbers);

            var uri = new Uri(string.Format(Constants.ApiUrl));
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                var keyValues = new List<KeyValuePair<string, string>>();
                foreach (int stopNumber in stopNumbers)
                {
                    keyValues.Add(new KeyValuePair<string, string>("stopID", stopNumber.ToString()));
                }
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

        private void AddRequestToken(int[] stopNumbers)
        {
            string bodyString = CreateBodyString(stopNumbers);
            string token = EncryptionUtil.CreateToken(bodyString);

            client.DefaultRequestHeaders.Remove("x-token");
            client.DefaultRequestHeaders.Add("x-token", token);
        }

        private string CreateBodyString(int[] stopNrs)
        {
            string bodyString = "";
            foreach(int nr in stopNrs)
            {
                bodyString += string.Format("{0}={1}&", FIELD_KEY_STOP_ID, nr);
            }
            bodyString += string.Format("{0}={1}", FIELD_KEY_OS, FIELD_VALUE_OS);
            return bodyString;
        }
    }
}
