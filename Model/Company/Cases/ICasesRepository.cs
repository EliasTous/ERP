using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Company.Cases
{
    public interface ICasesRepository:IRepository<Case, string>,ICommonRepository
    {

    }
}
