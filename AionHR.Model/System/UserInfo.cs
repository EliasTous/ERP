using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AionHR.Model.System
{
    /// <summary>
    /// User Entity
    /// </summary>
    public class UserInfo : IEntity
    {
        [PropertyID("2001001")]
        public string fullName { get; set; }
        [PropertyID("2001002")]
        public string email { get; set; }
        [PropertyID("2001003")]
        public bool isInactive { get; set; }
        [PropertyID("2001004")]
        public bool isAdmin { get; set; }
        [PropertyID("2001005")]
        public string employeeId { get; set; }
        [PropertyID("2001006")]
        public int languageId { get; set; }
        
        [PropertyID("2001007")]
        public string password { get; set; }

        public string recordId { get; set; }

        public string accountId
        {
            get; set;
        }


        public string enableHijriCalendar { get; set; }

        

        
        public string companyName { get; set; }

        
       
    }
}