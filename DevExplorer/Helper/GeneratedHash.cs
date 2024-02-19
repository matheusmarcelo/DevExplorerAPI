using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DevExplorerAPI.DevExplorer.Helper
{
    public static class GeneratedHash
    {
        public static string GeneratePasswordHash(this string password)
        {
            var hash = SHA256.Create();
            var encoding = new ASCIIEncoding();
            var array = encoding.GetBytes(password);

            array = hash.ComputeHash(array);

            StringBuilder strHexa = new StringBuilder();

            foreach (var item in array)
            {
                strHexa.Append(item.ToString("x2"));
            }

            return strHexa.ToString();
        }
    }
}