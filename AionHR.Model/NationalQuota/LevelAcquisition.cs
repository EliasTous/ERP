﻿using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.NationalQuota
{
    [ClassIdentifier("10105", "10")]
    public  class LevelAcquisition
    {
        public string inName { get; set; }
        public string leName { get; set; }
        public int inId { get; set; }
        public int bsId { get; set; }
        public int leId { get; set; }
        public short pct { get; set; }
       
   
    
     
    }
}
