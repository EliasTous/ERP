﻿using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Company.News
{
    public interface ICompanyNewsRepository:IRepository<News, string>,ICommonRepository
    {

    }
}
