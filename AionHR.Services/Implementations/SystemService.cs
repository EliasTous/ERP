using AionHR.Infrastructure.Domain;
using AionHR.Infrastructure.Session;
using AionHR.Infrastructure.WebService;
using AionHR.Model.MasterModule;
using AionHR.Model.System;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
    /// <summary>
    /// Class responsible for all operation of the system.
    /// </summary>
    public class SystemService : BaseService, ISystemService
    {

      

        // public readonly SessionHelper _sessionHelper;

        public SystemService(ISystemRepository userRepository, SessionHelper sessionHelper) : base(sessionHelper)
        {
            this.childRepo = userRepository;
            // _sessionHelper = sessionHelper;
        }
        private ISystemRepository childRepo;

        /// <summary>
        /// The concrete method that authenticate a user request 
        /// </summary>
        /// <param name="request">holding the username, account an the password</param>
        /// <returns>Object AuthenticateResponse</returns>
        public AuthenticateResponse Authenticate(AuthenticateRequest request)
        {

            //Building the WebService request
            //First Step Request by Account >> Defining Header
            AuthenticateResponse response = new AuthenticateResponse();


            Dictionary<string, string> headers = SessionHelper.GetAuthorizationHeadersForUser();
            
            
            RecordWebServiceResponse<UserInfo> userRecord = childRepo.Authenticate(headers, request.Parameters);
            if (userRecord == null)
            {
                response.Success = false;
                response.Message = "RequestError"; //This message have to be read from resource, it indicate that there was a problem in the connection.
                return response;
            }
            if (userRecord.record == null)
            {
                response.Success = false;
                response.Message = "InvalidCredentials";
                return response;
            }
            //authentication Valid, set the session then return the response back


            SessionHelper.Set("UserId", userRecord.record.recordId);
            SessionHelper.Set("key", SessionHelper.GetToken(SessionHelper.Get("AccountId").ToString(), userRecord.record.recordId));
            response.User = userRecord.record;
            response.Success = true;
            return response;

        }

        public PasswordRecoveryResponse RequestPasswordRecovery(AccountRecoveryRequest request)
        {
            PasswordRecoveryResponse response = new PasswordRecoveryResponse();



            Dictionary<string, string> headers = SessionHelper.GetAuthorizationHeadersForUser();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            
            RecordWebServiceResponse<UserInfo> userRecord =childRepo.RequestPasswordRecovery( headers, request.Parameters);

            response = CreateServiceResponse<PasswordRecoveryResponse>(userRecord);
           // if (response.Success)
                return response;
           
        }

        public PasswordRecoveryResponse ResetPassword(ResetPasswordRequest request)
        {
            PasswordRecoveryResponse response;
            Dictionary<string, string> headers = SessionHelper.GetAuthorizationHeadersForUser();
            
            RecordWebServiceResponse<UserInfo> webResponse = childRepo.ResetPassword(headers, request.Parameters);


            response = CreateServiceResponse<PasswordRecoveryResponse>(webResponse);

            return response;
        }

        protected override dynamic GetRepository()
        {
            return childRepo;
        }
    }
}
