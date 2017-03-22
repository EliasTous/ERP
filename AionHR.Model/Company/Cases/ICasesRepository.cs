using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Company.Cases
{
    public interface ICasesRepository:IRepository<Case, string>,ICommonRepository
    {

    }
}
