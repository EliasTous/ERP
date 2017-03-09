using AionHR.Infrastructure.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Infrastructure.Session
{
    /// <summary>
    /// It is a helper class very similar to UserManager of the old project, this class need to be structured again as there is a violation of the separation of Concern.
    /// </summary>
    public class SessionHelper
    {
       
        ISessionStorage _sessionStorage;
        ITokenGenerator _tokenGenerator;
        public SessionHelper(ISessionStorage sessionStorage, ITokenGenerator tokenGenerator)
        {
            _sessionStorage = sessionStorage;
            _tokenGenerator = tokenGenerator;

        }

        /// <summary>
        /// Check if a user is logged in
        /// </summary>
        /// <returns></returns>
        public bool CheckUserLoggedIn()
        {
            if (Get("key") == null || Get("AccountId") == null ||Get("UserId") == null)
                return false;
            else return true;
        }

        /// <summary>
        /// Check if the current session is arabic
        /// </summary>
        /// <returns></returns>
        public bool CheckIfArabicSession()
        {
            if (Get("Language") != null )
                if(Get("Language").ToString()=="ar")
                    return true;
             return false;
        }

        /// <summary>
        /// Clear session for the current storage
        /// </summary>
        public void ClearSession()
        {
            _sessionStorage.Clear();
        }


        public void SetLanguage(string language)
        {
            Set("Language", language);
        }

        public void Set(string key, object value)
        {
            _sessionStorage.Save(key, value);
        }
        public object Get(string key)
        {
            return _sessionStorage.Retrieve(key);
        }

        public Dictionary<string, string> GetAuthorizationHeadersForUser()
        {
            if (!CheckUserLoggedIn())
                return GetAuthorizationHeadersForApp();
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Basic " + Get("key"));
            headers.Add("AccountId", "" + Get("AccountId"));
            headers.Add("UserId", Get("UserId").ToString());
            return headers;
        }

        public Dictionary<string, string> GetAuthorizationHeadersForApp()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Basic " + _tokenGenerator.GetUserToken(Get("AccountId").ToString(), "0"));
            headers.Add("AccountId", Get("AccountId").ToString());
            headers.Add("UserId", "0");
            return headers;
        }

        public string GetToken(string accountID, string userID)
        {
            return _tokenGenerator.GetUserToken(accountID, userID);
        }

        //public void AddTimeZone(string timeZone)
        //{
        //    _sessionStorage.Save("TimeZone", timeZone);
        //}

        //public string GetTimeZone()
        //{
        //   object o= _sessionStorage.Retrieve("TimeZone");
        //    if ( o != null)
        //            return o.ToString();
        //        else return "";
            
            
        //}



    }
}
