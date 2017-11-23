using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AionHR.Model.System
{
    public enum UserType
    {
        SUPER_USER=1, SUPERVISOR=2, OPERATOR=3, SELF_SERVICE=4
    };

    /// <summary>
    /// User Entity
    /// </summary>
    /// 
    [ClassIdentifier("20010","20")]
    public class UserInfo : IEntity
    {
        
        [PropertyID("20010_01")]
        [ApplySecurity(false)]
        public string fullName { get; set; }
        [PropertyID("20010_02")]
        [ApplySecurity]
        public string email { get; set; }
        [PropertyID("20010_03")]
        [ApplySecurity]
        public bool isInactive { get; set; }

        [PropertyID("20010_04")]
        [ApplySecurity]
        public bool isAdmin {
            get { if ((int)this.userType == 1)
                    return true;
                     else return false; 
                    }
            set { }

        }
        [PropertyID("20010_01")]
        [ApplySecurity]
        public string employeeId { get; set; }
        [PropertyID("20010_06")]
        [ApplySecurity]
        public int languageId { get; set; }

        [PropertyID("20010_07")]
        [ApplySecurity]
        public string password { get; set; }
       

        public string recordId { get; set; }

        public string accountId
        {
            get; set;
        }


        public string enableHijriCalendar { get; set; }

        

        
        public string companyName { get; set; }


        public UserType userType { get; set; }
        
       
    }
}