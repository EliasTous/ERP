using AionHR.Infrastructure.WebService;
using AionHR.Model.MasterModule;
using AionHR.Model.System;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.System;
using System.Collections.Generic;

namespace AionHR.Services.Interfaces
{
    /// <summary>
    /// Interface that hold the methods of a service
    /// </summary>
    public interface ISystemService : IBaseService
    {

        /// <summary>
        /// Authenticate a request
        /// </summary>
        /// <param name="request">holding the account, user, and password</param>
        /// <returns>AuthenticateResponse object</returns>
        AuthenticateResponse Authenticate(AuthenticateRequest request);

        PasswordRecoveryResponse RequestPasswordRecovery(AccountRecoveryRequest request);


        PasswordRecoveryResponse ResetPassword(ResetPasswordRequest request);

        PostResponse<Attachement> UploadMultipleAttachments(SystemAttachmentsPostRequest req);
        PostResponse<Attachement> UploadCompanyLogo(CompanyUploadLogoRequest req);
      
        PostResponse<BatchSql> RunSqlBatch(BatchSql r);
    }
}