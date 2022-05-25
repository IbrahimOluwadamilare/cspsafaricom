using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace cspv3.Helpers
{
    public static class GetHash
    {
        public static string SHA512Hash(string transactionref, string merchantsecret, string merchantcode)
        {
            var key = transactionref + merchantsecret + merchantcode;
            var data = System.Text.Encoding.UTF8.GetBytes(key);
            using (SHA512 shaM = new SHA512Managed())

            {
                var hash = shaM.ComputeHash(data);
                return GetStringFromHash(hash);
            }


        }
        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }

            return result.ToString().ToLower();
        }
    }
}
