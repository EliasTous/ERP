﻿using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Benefits
{
   public interface IBenefitsRepository : IRepository<IEntity, string>
    {
    }
}
