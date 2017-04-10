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
    /// Summary description for EmployeePhotoUploaderHandler
    /// </summary>
    public class EmployeePhotoUploaderHandler : IHttpHandler, IRequiresSessionState
    {

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/javascript";
           
            EmployeeUploadPhotoRequest upreq = new EmployeeUploadPhotoRequest();
            upreq.entity.fileName = context.Request.Files[0].FileName;
           
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

                    upreq.photoName = context.Request.Files[0].FileName;
                    upreq.photoData = fileData;
                    upreq.entity.recordId = Convert.ToInt32(context.Request.QueryString["recordId"]);


                }

                PostResponse<Attachement> resp = _employeeService.UploadEmployeePhoto(upreq);
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