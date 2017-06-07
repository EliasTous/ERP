﻿using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    [ClassIdentifier("31121", "31")]
    public class EmployeeEmergencyContact:ModelBase
    {
       

        public string name { get; set; }
        public int rtId { get; set; }

        public string rtName { get; set; }

        public string workPhone { get; set; }

        public string homePhone { get; set; }

        public string cellPhone { get; set; }

        public string email { get; set; }

        public AddressBook addressId { get; set; }

        public string naId { get; set; }

        public string stateId { get; set; }
        public int employeeId { get; set; }
    }
}
