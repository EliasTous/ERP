﻿using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AssetManagementRepository
{
   public interface IAssetManagementRepository : IRepository<IEntity, string>
    {
    }
}
