﻿using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    [ClassIdentifier("31110", "31")]
    public class Sponsor : ModelBase
    {
        public string name { get; set; }

        public string idRef { get; set; }
        public string rtwRef { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string mobile { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string fax { get; set; }
       

        

       
    }
}
