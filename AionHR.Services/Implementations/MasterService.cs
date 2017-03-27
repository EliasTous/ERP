using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionHR.Infrastructure.Session;
using AionHR.Model.MasterModule;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.System;
using AionHR.Infrastructure.WebService;
using AionHR.Infrastructure.Domain;

namespace AionHR.Services.Implementations
{
    public class MasterService : BaseService,IMasterService
    {
        private IAccountRepository _accountRepository;
        public MasterService(IAccountRepository accountRepository, SessionHelper helper):base(helper)
        {
            _accountRepository = accountRepository;
        }



        public Response<Account> GetAccount(GetAccountRequest request)
        {
            Response<Account> response = new Response<Account>();
            SessionHelper.ClearSession();
            SessionHelper.Set("AccountId", "0"); //To be checked as it is a strange behavior ( simulated from old code)
            Dictionary<string, string> headers = SessionHelper.GetAuthorizationHeadersForUser();
            
            var accountRecord = _accountRepository.GetRecord(headers, request.Parameters);
            response =CreateServiceResponse<Response<Account>>(accountRecord);
            if (accountRecord == null)
            {
                response.Success = false;
                response.Message = "RequestError"; //This message have to be read from resource, it indicate that there was a problem in the connection.
                return response;
            }
            if (accountRecord.record == null)
            {
                response.Success = false;
                response.Message = "InvalidAccount";
                return response;
            }
            response.result = (Account)accountRecord.record;
            SessionHelper.Set("AccountId", response.result.accountId);
            response.Success = true;
            return response;
        }

        public Response<Account> RequestAccountRecovery(AccountRecoveryRequest request)
        {
            Response<Account> response;
            SessionHelper.ClearSession();
            SessionHelper.Set("AccountId", "0");
            Dictionary<string, string> headers = SessionHelper.GetAuthorizationHeadersForUser();
            
            var webResponse=  _accountRepository.GetRecord(headers, request.Parameters);
            response = CreateServiceResponse<Response<Account>>(webResponse);
            if (!response.Success)
                response.Message = "";
            return response;
        }

        public PostResponse<Registration> AddRegistration(Registration r)
        {
            PostResponse<Registration> response = new PostResponse<Registration>();
            SessionHelper.ClearSession();
            SessionHelper.Set("AccountId", "0"); //To be checked as it is a strange behavior ( simulated from old code)
            Dictionary<string, string> headers = SessionHelper.GetAuthorizationHeadersForUser();
            var accountRecord = _accountRepository.ChildAddOrUpdate<Registration>(r, headers);
            response = base.CreateServiceResponse<PostResponse<Registration>>(accountRecord);

            if (accountRecord != null)
                response.recordId = accountRecord.recordId;
            return response;

        }
        public PostResponse<Account> AddAccount(Account r)
        {
            PostResponse<Account> response = new PostResponse<Account>();
            SessionHelper.ClearSession();
            SessionHelper.Set("AccountId", "0"); //To be checked as it is a strange behavior ( simulated from old code)
            Dictionary<string, string> headers = SessionHelper.GetAuthorizationHeadersForUser();
            var accountRecord = _accountRepository.ChildAddOrUpdate<Account>(r, headers);
            response = base.CreateServiceResponse<PostResponse<Account>>(accountRecord);

            if (accountRecord != null)
                response.recordId = accountRecord.recordId;
            return response;

        }
        public PostResponse<DbSetup> CreateDB(DbSetup r)
        {
            PostResponse<DbSetup> response = new PostResponse<DbSetup>();
            SessionHelper.ClearSession();
            SessionHelper.Set("AccountId", "0"); //To be checked as it is a strange behavior ( simulated from old code)
            Dictionary<string, string> headers = SessionHelper.GetAuthorizationHeadersForUser();
            var accountRecord = _accountRepository.ChildAddOrUpdate<DbSetup>(r, headers);
            response = base.CreateServiceResponse<PostResponse<DbSetup>>(accountRecord);

            if (accountRecord != null)
                response.recordId = accountRecord.recordId;
            return response;

        }
        protected override dynamic GetRepository()
        {
            return _accountRepository;
        }
    }
}
