using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingFormApp.Helpers
{
    public static class Utilities
    {
        public static bool IsFull(string[] arr)
        {
            foreach (var a in arr)
            {
                if (string.IsNullOrWhiteSpace(a))
                {
                    return false;
                }
            }
            return true;
        }

        public static string HashPassword(string password)
        {
            byte[] byteArr = Encoding.UTF8.GetBytes(password);
            var hashPass = SHA256Managed.Create().ComputeHash(byteArr);
            string passString = Encoding.UTF8.GetString(hashPass);

            return passString;
        }
    }
}
