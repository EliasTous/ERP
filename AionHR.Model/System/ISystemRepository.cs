using AionHR.Infrastructure.Domain;
using AionHR.Infrastructure.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.System
{
    /// <summary>
    /// Interface for user repository
    /// </summary>
    public interface ISystemRepository:IRepository<UserInfo,string>,ICommonRepository
    {
         RecordWebServiceResponse<UserInfo> Authenticate(Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null);
        RecordWebServiceResponse<UserInfo> ResetPassword(Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null);
        RecordWebServiceResponse<UserInfo> RequestPasswordRecovery(Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null);

        PostWebServiceResponse UploadMultipleAttachments(Attachement at, List<string> fileNames, List<byte[]> filesData, Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null);
        PostWebServiceResponse UploadCompanyLogo(Attachement at, string fileName, byte[] fileData, Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null);
    }
}
