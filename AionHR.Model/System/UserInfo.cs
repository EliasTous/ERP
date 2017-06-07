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
    /// 
    [ClassIdentifier("20010","20")]
    public class UserInfo : IEntity
    {
        [PropertyID("20010_01")]
        public string fullName { get; set; }
        [PropertyID("20010_02")]
        public string email { get; set; }
        [PropertyID("20010_03")]
        public bool isInactive { get; set; }

        [PropertyID("20010_04")]
        public bool isAdmin { get; set; }
        [PropertyID("20010_05")]
        public string employeeId { get; set; }
        [PropertyID("20010_06")]
        public int languageId { get; set; }

        [PropertyID("20010_07")]
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