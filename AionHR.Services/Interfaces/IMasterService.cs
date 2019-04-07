using AionHR.Model.MasterModule;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Interfaces
{
    public interface IMasterService:IBaseService
    {
        Response<Account> GetAccount(GetAccountRequest request);

        Response<Account> RequestAccountRecovery(AccountRecoveryRequest request);
        PostResponse<DbSetup> CreateDB(DbSetup r);

        PostResponse<Account> AddAccount(Account r);

      //  PostResponse<Registration> AddRegistration(Registration r);

        Dictionary<string, Type> ClassLookup
        {
            get; set;
        }

    }
}
