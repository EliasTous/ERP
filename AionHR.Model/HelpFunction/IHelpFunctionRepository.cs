﻿using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.HelpFunction
{
  public  interface IHelpFunctionRepository : IRepository<IEntity, string>
    {
    }
}
