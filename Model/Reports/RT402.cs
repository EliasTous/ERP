﻿using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Reports
{
    [ClassIdentifier("80402", "80")]
    public class RT402
    {
     

        public DateTime date { get; set; }
        public string trxType { get; set; }
        public double amount { get; set; }
     
        public string reference { get; set; }
        public double balance { get; set; }
        public string dateStringFormat { get; set; }


    }
}
