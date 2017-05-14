using PCLCrypto;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MijnLijn.Utils
{
    public static class EncryptionUtil
    {
        private const string FIELD_VALUE_SIGNATURE_PREFIX = "android_ML_";
        private const string FIELD_VALUE_SIGNATURE_VERSION_NAME = "1.0";
      

        public static string CreateSignature()
        {
            return FIELD_VALUE_SIGNATURE_PREFIX + FIELD_VALUE_SIGNATURE_VERSION_NAME;
        }

        public static string CreateToken(string bodyString)
        {
            string secret = Constants.ApiSecret;
            string signature = CreateSignature();
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

            return string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
        }
    }
}
