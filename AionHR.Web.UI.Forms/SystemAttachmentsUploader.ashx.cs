using AionHR.Infrastructure.Domain;
using AionHR.Model.System;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.System;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace AionHR.Web.UI.Forms
{
    /// <summary>
    /// Summary description for CaseAttachmentsUploader
    /// </summary>
    public class SystemAttachmentsUploader : IHttpHandler, IRequiresSessionState
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/javascript";
            SystemAttachmentsPostRequest req = new SystemAttachmentsPostRequest();
            req.entity = new Model.System.Attachement() { classId = Convert.ToInt32(context.Request.QueryString["classId"]), recordId = Convert.ToInt32(context.Request.QueryString["recordId"]), folderId= Convert.ToInt32(context.Request.Form["id"]), fileName = context.Request.Files[0].FileName  };
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
                    fileData = new byte[Convert.ToInt32(f.InputStream.Length)];
                    f.InputStream.Seek(0, SeekOrigin.Begin);
                    f.InputStream.Read(fileData, 0, Convert.ToInt32(f.InputStream.Length));
                    f.InputStream.Close();
                    
                    req.FilesData.Add((byte[])fileData.Clone());
                    req.FileNames.Add(f.FileName);


                }
                
                PostResponse<Attachement> resp = _systemService.UploadMultipleAttachments(req);
                if (!resp.Success)
                {
                    context.Response.Write("{'Error':'Error'}");
                    return;
                }
                
            }
            context.Response.Write("{}");
            
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