using Infrastructure.WebService;
using Model.MasterModule;
using Model.System;
using Services.Messaging;
using Services.Messaging.System;
using System.Collections.Generic;

namespace Services.Interfaces
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