﻿using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.System
{

    public class KeyId:ModelBase
    {
        public string keyId { get; set; }
        public DateTime expiryDate { get; set; }
    }
}
