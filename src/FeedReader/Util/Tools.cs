using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace FeedReader.Util
{
    public class Tools
    {
        public static string CreateIDHash(string title, string url)
        {
            return SHA256FromString(title + "|" + url);

        }

        public static string SHA256FromString(string data)
        {
            SHA256 sha = SHA256.Create();

            return Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(data)));

        }
    }
}
