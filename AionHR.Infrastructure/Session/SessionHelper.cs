using AionHR.Infrastructure.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            if (Get("AccountId") == null || Get("UserId") == null)
                return false;
            else return true;
        }

        /// <summary>
        /// Check if the current session is arabic
        /// </summary>
        /// <returns></returns>
        public bool CheckIfArabicSession()
        {
            if (Get("Language") != null)
                if (Get("Language").ToString() == "ar")
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
        #region Sets

        public void SetLanguage(string language)
        {
            Set("Language", language);
        }
        public void SetDateformat(string format)
        {
            Set("dateFormat", format);
        }
        public void SetNameFormat(string format)
        {
            string commad = format.Replace('}', ',');
            string removedBrace = commad.Replace('{', Char.MinValue);
            string lastLetterRemoved = removedBrace.Substring(0, removedBrace.Length - 1);
            Set("nameFormat", lastLetterRemoved);
        }
        public void SetCurrencyId(string value)
        {
            Set("currencyId", value);
        }

        public void SetCalendarId(string value)
        {
            if (string.IsNullOrEmpty(value))
                value = "0";
            Set("caId", value);
        }
        public void SetVacationScheduleId(string value)
        {
            if (string.IsNullOrEmpty(value))
                value = "0";
            Set("vsId", value);
        }
        public void SetDefaultTimeZone(int value)
        {
            Set("timeZone", value);
        }
        public void SetDefaultCountry(string format)
        {
            Set("countryId", format);
        }

        public void SetHijriSupport(bool value)
        {
            Set("enableHijri", value);
        }

        public void SetStartDate(DateTime date)
        {
            Set("StartDate", date.ToString("dd/MM/yyy"));
        }

        #endregion

        #region Gets

        public string GetCurrentUser()
        {
            return Get("CurrentUserName").ToString();
        }

        public string GetCurrentUserId()
        {
            return Get("UserId").ToString();
        }
        public string GetDateformat()
        {
            object dateFormat = Get("dateFormat");
            if (dateFormat == null)
                return "MMM dd,yyyy";

            return dateFormat.ToString();
        }
        public string GetNameformat()
        {
            object nameFormat = Get("nameFormat");
            if (nameFormat == null)
                return "{firstName} {lastName}";

            return nameFormat.ToString();
        }

        public int GetDefaultTimeZone()
        {
            return Convert.ToInt32(Get("timeZone").ToString());
        }
        public DateTime GetStartDate()
        {
            return DateTime.ParseExact(Get("StartDate").ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        public int GetCalendarId()
        {
            return Convert.ToInt32(Get("caId").ToString());
        }
        public int GetVacationScheduleId()
        {
            return Convert.ToInt32(Get("vsId").ToString());
        }
        public string GetDefaultCountry()
        {
            object nameFormat = Get("countryId");
            if (nameFormat == null)
                return "Lebanon";

            return nameFormat.ToString();
        }
        public string GetDefaultCurrency()
        {
            object nameFormat = Get("currencyId");
            if (nameFormat == null)
                return "0";

            return nameFormat.ToString();
        }

        public void SetUserType(int userType)
        {
            Set("UserType", userType);
        }

        public int GetUserType()
        {
            return (int)Get("UserType");
        }
        public void SetEmployeeId(string EmployeeId)
        {
            Set("EmployeeId", EmployeeId);
        }

        public string GetEmployeeId()
        {
            return (string)Get("EmployeeId");
        }
        public bool CheckIfIsAdmin()
        {
            return (bool)Get("IsAdmin");
        }

        public bool GetHijriSupport()
        {
            try
            {
                return Convert.ToBoolean(Get("enableHijri"));
            }
            catch
            {
                return false;
            }
        }

        #endregion
        public object Get(string key)
        {
            return _sessionStorage.Retrieve(key);
        }
        public void Set(string key, object value)
        {
            _sessionStorage.Save(key, value);
        }
        public Dictionary<string, string> GetAuthorizationHeadersForUser()
        {
            if (!CheckUserLoggedIn())
                return GetAuthorizationHeadersForApp();
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Basic " + Get("key"));
            headers.Add("AccountId", "" + Get("AccountId"));
            headers.Add("UserId", Get("UserId").ToString());
           
                if (Get("Language").ToString() == "en")
                    headers.Add("languagId", "1");
                else
                    headers.Add("languagId", "2");
            
            
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
