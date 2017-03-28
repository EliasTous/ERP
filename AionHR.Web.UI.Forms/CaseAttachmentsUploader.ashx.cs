using AionHR.Model.System;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.System;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace AionHR.Web.UI.Forms
{
    /// <summary>
    /// Summary description for CaseAttachmentsUploader
    /// </summary>
    public class CaseAttachmentsUploader : IHttpHandler, IRequiresSessionState
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();

        public void ProcessRequest(HttpContext context)
        {
           
            SystemAttachmentsPostRequest req = new SystemAttachmentsPostRequest();
            req.entity = new Model.System.Attachement() { classRef = "CMCA ", recordId = Convert.ToInt32(context.Request.QueryString["caseId"]) };
            //write your handler implementation here.
            if (context.Request.Files.Count <= 0)
            {
                context.Response.Write("No file uploaded");
            }
            else
            {
                byte[] fileData = null;
                for (int i = 0; i < context.Request.Files.Count; ++i)
                {
                    HttpPostedFile f = context.Request.Files.Get(i);
                    fileData = new byte[f.ContentLength];
                    f.InputStream.Read(fileData, 0, f.ContentLength);
                    req.FilesData.Add(fileData);
                    req.FileNames.Add(f.FileName);


                }
                PostResponse<Attachement> resp = _systemService.UploadMultipleAttachments(req);
            }
            context.Response.Write("{}");
            context.Response.ContentType = "application/javascript";
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}