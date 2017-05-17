using MijnLijn.Models;
using MijnLijn.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using MijnLijn.Global;

namespace MijnLijn.Data.Remote
{
    public class RestService : IRestService
    {
        private const string FieldKeyStopId = "stopID";
        private const string FieldKeyOs = "os";
        private const string FieldValueOs = "android";

        readonly HttpClient _client;

        public RestService()
        {
            _client = new HttpClient { MaxResponseContentBufferSize = 256000 };
            _client.DefaultRequestHeaders.Add("x-signature", Constants.ApiSignature);
        }

        public async Task<ApiResponse> PostToGetLines(int[] stopNumbers)
        {
            AddRequestToken(stopNumbers);

            var uri = new Uri(string.Format(Constants.ApiUrl));
            var apiResponse = new ApiResponse();

            try
            {
                var keyValues = new List<KeyValuePair<string, string>>();
                foreach (int stopNumber in stopNumbers)
                {
                    keyValues.Add(new KeyValuePair<string, string>("stopID", stopNumber.ToString()));
                }
                keyValues.Add(new KeyValuePair<string, string>("os", "android"));

                HttpContent content = new FormUrlEncodedContent(keyValues);

                var response = await _client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(@"				Success, content: " + responseContent);
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

            _client.DefaultRequestHeaders.Remove("x-token");
            _client.DefaultRequestHeaders.Add("x-token", token);
        }

        private string CreateBodyString(int[] stopNrs)
        {
            string bodyString = "";
            foreach(int nr in stopNrs)
            {
                bodyString += $"{FieldKeyStopId}={nr}&";
            }
            bodyString += $"{FieldKeyOs}={FieldValueOs}";
            return bodyString;
        }
    }
}
