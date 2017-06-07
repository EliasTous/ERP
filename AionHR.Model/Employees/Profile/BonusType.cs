﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;

namespace AionHR.Model.Employees.Profile
{
    [ClassIdentifier("31030", "31")]
    public class BonusType : ModelBase
    {
        public string name { get; set; }
    }
}
