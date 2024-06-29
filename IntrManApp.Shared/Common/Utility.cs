using IntrManApp.Shared.Contract;
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
        public static string Encrypt(string password, string salt = "ED79C0BA-3E92-4151-8269-746E715CD339")
        {
            var provider = MD5.Create();
            byte[] bytes = provider.ComputeHash(Encoding.UTF32.GetBytes(salt + password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }

    public class AppUser
    {
        public static Guid Token { get; set; } = Guid.Empty;
        public static Guid UserId { get; set; } = Guid.Empty;
        public static string Username { get; set; } = string.Empty;
        public static string Role { get; set; } = string.Empty;
        public static List<FeatureAccessResponse> FeatureAccesses { get; set; } = [];

        public static void Init(Guid token, Guid userId, string username, string role, List<FeatureAccessResponse> featureAccesses)
        {
            Token = token;
            UserId = userId;
            Username = username;
            Role = role;
            FeatureAccesses = featureAccesses;
        }

        public static void Reset()
        {
            Token = Guid.Empty;
            UserId = Guid.Empty;
            Username = string.Empty;
            Role = string.Empty;
            FeatureAccesses = [];
        }

        public static bool CanView(string featureName)
        {
            var feature = FeatureAccesses
                .Where(x => x.Name.ToLower().Trim().Equals(featureName.Trim().ToLower())).FirstOrDefault();
            return feature?.CanView ?? false;
        }
    }
}
