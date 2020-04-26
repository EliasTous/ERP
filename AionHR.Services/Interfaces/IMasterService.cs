using Model.MasterModule;
using Services.Messaging;
using Services.Messaging.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
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
