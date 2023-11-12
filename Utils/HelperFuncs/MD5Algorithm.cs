using System.Security.Cryptography;
using System.Text;

namespace Utils.HelperFuncs
{
    public static class MD5Algorithm
    {
        public static string HashMd5(string input)
        {
            byte[] data = MD5.HashData(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new();

            for (int i = 0 ; i < data.Length ; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            string result = sBuilder.ToString();

            return result;
        }

        public static bool VerifyMd5Hash(string input, string hash)
        {
            string hashOfInput = HashMd5(input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return 0 == comparer.Compare(hashOfInput, hash);
        }
    }
}
