using System;
using System.Security.Cryptography;
using System.Text;

namespace MySoft.Common
{
    public class EncryptHelper
    {
        public static string GetMd5(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);

            var md = new MD5CryptoServiceProvider();
            var hashkey = md.ComputeHash(bytes);

            var md5String = new StringBuilder();

            foreach (byte b in hashkey)
            {
                md5String.AppendFormat("{0:x2}", b);
            }

            return md5String.ToString();
        }

        public static string GetSha1(string str)
        {
            var enc = Encoding.GetEncoding(0);

            byte[] buffer = enc.GetBytes(str);
            var sha1 = SHA1.Create();
            var hash = BitConverter.ToString(sha1.ComputeHash(buffer)).Replace("-", "");
            return hash;
        }
    }
}
