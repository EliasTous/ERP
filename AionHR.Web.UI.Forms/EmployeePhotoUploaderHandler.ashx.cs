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
    /// Summary description for EmployeePhotoUploaderHandler
    /// </summary>
    public class EmployeePhotoUploaderHandler : IHttpHandler, IRequiresSessionState
    {

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/javascript";



            //write your handler implementation here.

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
            else
            {
                if (context.Request.QueryString["classId"].ToString() != "20030")
                {
                    EmployeeUploadPhotoRequest upreq = new EmployeeUploadPhotoRequest();
                    upreq.entity.fileName = context.Request.Files[0].FileName;
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
                        upreq.entity.date = DateTime.Now;

                    }

                    PostResponse<Attachement> resp = _employeeService.UploadEmployeePhoto(upreq);
                    if (!resp.Success)
                    {
                        context.Response.Write("{'Error':'Error'}");
                        return;
                    }
                }
                else
                {
                    
                    

                        byte[] fileData = null;
                        HttpPostedFile f = context.Request.Files.Get(0);
                        if (f.InputStream.Length > 0)
                        {

                            fileData = new byte[Convert.ToInt32(f.InputStream.Length)];
                            f.InputStream.Seek(0, SeekOrigin.Begin);
                            f.InputStream.Read(fileData, 0, Convert.ToInt32(f.InputStream.Length));
                            f.InputStream.Close();



                        }
                        else
                        {
                            fileData = null;
                        }
                       


                        //check if the insert failed
                      
                            if (fileData != null)
                            {
                                SystemAttachmentsPostRequest req = new SystemAttachmentsPostRequest();
                                req.entity = new Model.System.Attachement() { date = DateTime.Now, classId = ClassId.SYDE, recordId = 1, fileName = context.Request.Files[0].FileName, seqNo = 1 };
                                req.FileNames.Add(context.Request.Files[0].FileName);
                                req.FilesData.Add(fileData);
                                PostResponse<Attachement> resp = _systemService.UploadMultipleAttachments(req);
                                if (!resp.Success)
                                {
                                    context.Response.Write("{'Error':'Error'}");
                                    return;
                                }

                            }
                           
                     
                      

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