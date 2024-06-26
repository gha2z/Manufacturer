using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Common
{
    public class Utility
    {
        public static string Encrypt(string password, string salt= "ED79C0BA-3E92-4151-8269-746E715CD339")
        {
            var provider = MD5.Create();
            byte[] bytes = provider.ComputeHash(Encoding.UTF32.GetBytes(salt + password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
