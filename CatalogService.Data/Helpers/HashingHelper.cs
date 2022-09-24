using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace CatalogService.Data.Helpers
{
    public class HashingHelper
    {      
        public static void CreatePasswordHashAndSalt(string password, out string passwordHash, out string passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var passwordSaltByteArray = hmac.Key;
                var passwordHashByteArray = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                passwordHash = Convert.ToBase64String(passwordHashByteArray);
                passwordSalt = Convert.ToBase64String(passwordSaltByteArray);
            }
        }

        public static bool VerifyPasswordHashAndSalt(string password, string passwordHash, string passwordSalt)
        {
            byte[] passwordHashByteArray = Convert.FromBase64String(passwordHash);
            byte[] passwordSaltByteArray = Convert.FromBase64String(passwordSalt);

            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSaltByteArray))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHashByteArray);

                //for (int i = 0; i < computedHash.Length; i++)
                //{
                //    if (computedHash[i] != passwordHashByteArray[i])
                //    {
                //        return false;
                //    }
                //}

                //return true;
            }
        }
    }
}
