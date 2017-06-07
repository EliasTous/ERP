﻿using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Company.News
{
    [ClassIdentifier("24010", "24")]
    public class News : ModelBase,IEntity
    {
        public string subject { get; set; }
        public string newsText { get; set; }
        public bool notifyViaEmail { get; set; }
        public bool allowComments { get; set; }
        public bool isPublished { get; set; }

    }
}
