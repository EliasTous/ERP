using Infrastructure.Domain;
using Infrastructure.Session;
using Infrastructure.WebService;
using Model.MasterModule;
using Model.System;
using Services.Interfaces;
using Services.Messaging;
using Services.Messaging.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
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
            response =CreateServiceResponse<AuthenticateResponse>(userRecord);
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

        public PostResponse<Attachement> UploadMultipleAttachments(SystemAttachmentsPostRequest request)
        {
            PostResponse<Attachement> response;
            var headers = SessionHelper.GetAuthorizationHeadersForUser();
            PostWebServiceResponse webResponse = childRepo.UploadMultipleAttachments(request.entity, request.FileNames, request.FilesData, headers);
            response = CreateServiceResponse<PostResponse<Attachement>>(webResponse);
            if (webResponse != null)
                response.recordId = webResponse.recordId;
            return response;
        }
        public PostResponse<Attachement> UploadCompanyLogo(CompanyUploadLogoRequest request)
        {
            PostResponse<Attachement> response;
            var headers = SessionHelper.GetAuthorizationHeadersForUser();
            PostWebServiceResponse webResponse = childRepo.UploadCompanyLogo(request.entity, request.photoName, request.photoData, headers);
            response = CreateServiceResponse<PostResponse<Attachement>>(webResponse);
            if (webResponse != null)
                response.recordId = webResponse.recordId;
            return response;
        }

     
        public PostResponse<BatchSql> RunSqlBatch(BatchSql r)
        {
            PostResponse<BatchSql> response = new PostResponse<BatchSql>();

            
            Dictionary<string, string> headers = SessionHelper.GetAuthorizationHeadersForUser();
            var accountRecord = childRepo.ChildAddOrUpdate<BatchSql>(r, headers);
            response = base.CreateServiceResponse<PostResponse<BatchSql>>(accountRecord);
          
            return response;

        }
    }
}
