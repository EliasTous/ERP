using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.LoadTracking
{
    public interface ILoanTrackingRepository:IRepository<Loan,string>, ICommonRepository
    {


    }
}
