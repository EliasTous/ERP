using AionHR.Infrastructure.Domain;
using AionHR.Infrastructure.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.MasterModule
{
    public interface IAccountRepository:IRepository<Account,string>,ICommonRepository
    {
        RecordWebServiceResponse<Account> RequestAccountRecovery(Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null);
    }
}
