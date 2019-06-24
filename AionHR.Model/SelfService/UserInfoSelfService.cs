using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.SelfService
{
      [ClassIdentifier("60113", "60")]
    
    public class UserInfoSelfService : IEntity
    {


        public string fullName { get; set; }

        public string email { get; set; }

        public short activeStatus { get; set; }


        public bool isAdmin
        {
            get
            {
                if ((int)this.userType == 1)
                    return true;
                else return false;
            }
            set { }

        }

        public string employeeId { get; set; }

        public int languageId { get; set; }


        public string password { get; set; }


        public string recordId { get; set; }

        public string accountId
        {
            get; set;
        }


        public string enableHijriCalendar { get; set; }




        public string companyName { get; set; }


        public int userType { get; set; }
        public string userTypeString { get; set; }

    }
}