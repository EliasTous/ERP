using Infrastructure.Domain;
using Infrastructure.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.MasterModule
{
    public interface IAccountRepository:IRepository<Account,string>,ICommonRepository
    {
        RecordWebServiceResponse<Account> RequestAccountRecovery(Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null);
    }
}
