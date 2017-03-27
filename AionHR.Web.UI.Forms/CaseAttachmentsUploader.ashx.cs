using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AionHR.Web.UI.Forms
{
    /// <summary>
    /// Summary description for CaseAttachmentsUploader
    /// </summary>
    public class CaseAttachmentsUploader : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string filePath = "Uploads//";

            //write your handler implementation here.
            if (context.Request.Files.Count <= 0)
            {
                context.Response.Write("No file uploaded");
            }
            else
            {
                for (int i = 0; i < context.Request.Files.Count; ++i)
                {
                    HttpPostedFile file = context.Request.Files[i];
                    file.SaveAs(context.Server.MapPath(filePath + file.FileName));
                    
                }
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