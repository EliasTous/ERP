using AionHR.Infrastructure.Domain;
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
        public string fullName { get; set; }
        public string email { get; set; }
        public bool isInactive { get; set; }
        public bool isAdmin { get; set; }
        public string employeeId { get; set; }
        public int languageId { get; set; }
        public string accountId
        {
            get; set;
        }
        public string password { get; set; }

        public string recordId { get; set; }

        
        
        
        public string enableHijriCalendar { get; set; }

        

        
        public string companyName { get; set; }

        
       
    }
}