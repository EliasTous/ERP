using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Infrastructure.Tokens
{
    /// <summary>
    /// Token Generator Class used to tokenize the key of the user
    /// </summary>
    public class APIKeyBasedTokenGenerator : ITokenGenerator
    {
        public  APIKeyBasedTokenGenerator()
        {
            APIKey = ConfigurationManager.AppSettings.Get("API_Token");
        }
        private string APIKey = "";
        public string UserId { get; set; }
        public string AccountId { get; set; }

    

        public string GetUserToken(string accountID, string userID)
        {
            AccountId = accountID;
            UserId = userID;

            return GetTripleToken(AccountId, UserId);

        }

        private string HashString(string input)
        {
            try
            {
                using (SHA512 sha = SHA512.Create())
                {
                    byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
                    string hashedKey = BitConverter.ToString(hash).Replace("-", "");
                    return hashedKey;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while generating hash key", ex);
            }
        }

        private byte[] Encode_SHA512(string updatedKey)
        {
            try
            {
                using (SHA512 sha = SHA512.Create())
                {
                    byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(updatedKey));
                    return hash;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while generating hash key", ex);
            }
        }

        private string GetTripleToken(string account, string user)
        {
            string updatedKey = account + APIKey + user;

            //byte[] hash = Encode_SHA512(updatedKey);

            //string hashedKey = BitConverter.ToString(hash).Replace("-", "");
            //return hashedKey;
            return HashString(updatedKey);
            
        }
    }
}