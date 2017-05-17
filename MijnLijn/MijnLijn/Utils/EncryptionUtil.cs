using MijnLijn.Global;
using PCLCrypto;
using System.Linq;
using System.Text;

namespace MijnLijn.Utils
{
    public static class EncryptionUtil
    {
        private const string FieldValueSignaturePrefix = "android_ML_";
        private const string FieldValueSignatureVersionName = "1.0";  

        // Create api token: a SHA1 hash of api secret, the bodystring and app signature         
        public static string CreateToken(string bodyString)
        {
            string secret = Constants.ApiSecret;
            string signature = CreateSignature();
            string token = $"{secret}:{bodyString}:{signature}";
            string hashedToken = CalculateSha1Hash(token).ToUpper();
            return hashedToken;
        }

        private static string CreateSignature()
        {
            return FieldValueSignaturePrefix + FieldValueSignatureVersionName;
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
