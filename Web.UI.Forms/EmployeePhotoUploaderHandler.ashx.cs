using Infrastructure.Domain;
using Model.System;
using Services.Interfaces;
using Services.Messaging;
using Services.Messaging.System;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace Web.UI.Forms
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

            try
            {

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
                    if (context.Request.QueryString["classId"].ToString() != ClassId.SYDE.ToString())
                    {
                        EmployeeUploadPhotoRequest upreq = new EmployeeUploadPhotoRequest();
                        upreq.entity.fileName = context.Request.Files[0].FileName;
                        byte[] fileData = null;
                        for (int i = 0; i < context.Request.Files.Count; ++i)
                        {
                            HttpPostedFile f = context.Request.Files.Get(i);
                            fileData = new byte[Convert.ToInt32(f.InputStream.Length)];
                            if (f.InputStream.Length > 1435405312)
                            {

                                context.Response.Write("{'Error':'largImage'}");
                                return;
                            }
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
                            if (context.Request.Files.Count == 1)
                            
                                req.entity = new Model.System.Attachement() { date = DateTime.Now, classId = ClassId.SYDE, recordId = 1, fileName = context.Request.Files[0].FileName, seqNo = 0 };
                            else
                                req.entity = new Model.System.Attachement() { date = DateTime.Now, classId = ClassId.SYDE, recordId = 1, fileName = context.Request.Files[0].FileName, seqNo = null };
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
            catch(Exception exp)
            {
                context.Response.Write("{'Error':'Error'}");
            }

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