using MijnLijn.Models;
using Newtonsoft.Json;
using PCLCrypto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
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
        }

        public async Task<ApiResponse> PostToGetLines(int[] stopNumbers)
        {

            client.DefaultRequestHeaders.Remove("x-token");

            string token = CreateToken(stopNumbers);
            client.DefaultRequestHeaders.Add("x-token", token);

            var uri = new Uri(string.Format(Constants.ApiUrl));
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                var keyValues = new List<KeyValuePair<string, string>>();
                foreach(int stopNumber in stopNumbers) {
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

        private static string FIELD_VALUE_SIGNATURE_PREFIX = "android_ML_";
        private static string FIELD_VALUE_SIGNATURE_VERSION_NAME = "1.0";
        private static string FIELD_KEY_STOP_ID = "stopID";
        private static string FIELD_KEY_OS = "os";
        private static string FIELD_VALUE_OS = "android";

        private static string CreateSignature()
        {
            return FIELD_VALUE_SIGNATURE_PREFIX + FIELD_VALUE_SIGNATURE_VERSION_NAME;
        }

        private static string CreateToken(int[] stopNrs)
        {
            string secret = Constants.ApiSecret;
            string signature = CreateSignature();
            string bodyString = CreateBodyString(stopNrs);
            string token = string.Format("{0}:{1}:{2}", secret, bodyString, signature);
            Debug.WriteLine("MijnLijn D: token = " + token);
            string hashedToken = CalculateSha1Hash(token).ToUpper();
            Debug.WriteLine("MijnLijn D: hashed token = " + hashedToken);
            return hashedToken;
        }

        private static string CalculateSha1Hash(string input)
        {
            var hasher = WinRTCrypto.HashAlgorithmProvider.OpenAlgorithm(HashAlgorithm.Sha1);
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = hasher.HashData(inputBytes);

            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < hash.Length; i++)
            //{
            //    sb.Append(hash[i].ToString("X2"));
            //}
            //return sb.ToString();

            return string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
        }

        private static string CreateBodyString(int[] stopNrs)
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
