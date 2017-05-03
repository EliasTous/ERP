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

            if (context.Request.Files.Count <= 0)
            {
                PostRequest<Attachement> request = new PostRequest<Attachement>();

                Attachement at = new Attachement();
                at.classId = ClassId.EPEM;
                at.recordId = Convert.ToInt32(context.Request.QueryString["recordId"]);
                at.seqNo = 0;
                at.folderId = null;

                at.fileName = context.Request.Form["oldUrl"];
                request.entity = at;
                PostResponse<Attachement> response = _systemService.ChildDelete<Attachement>(request);
                if (response.Success)
                    context.Response.Write("{}");
                else
                    context.Response.Write("{Error}");

            }
            SystemAttachmentsPostRequest req = new SystemAttachmentsPostRequest();
            req.entity = new Model.System.Attachement() { date=DateTime.Now, classId = Convert.ToInt32(context.Request.QueryString["classId"]), recordId = Convert.ToInt32(context.Request.QueryString["recordId"]), fileName = context.Request.Files[0].FileName, seqNo = null };
            //write your handler implementation here.
            int bulk;
            if (int.TryParse(context.Request.Form["id"], out bulk))
            {
                req.entity.folderId = bulk;
            }
            else
                req.entity.folderId = null;


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