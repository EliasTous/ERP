using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Xsl;
using Ext.Net;
using Newtonsoft.Json;
using AionHR.Services.Interfaces;
using Microsoft.Practices.ServiceLocation;
using AionHR.Web.UI.Forms.Utilities;
using AionHR.Model.Company.News;
using AionHR.Services.Messaging;
using AionHR.Model.Company.Structure;
using AionHR.Model.Employees.Profile;
using AionHR.Model.Employees.Leaves;
using AionHR.Model.Attendance;
using AionHR.Model.TimeAttendance;
using AionHR.Services.Messaging.Reports;
using AionHR.Model.Reports;
using AionHR.Services.Implementations;
using AionHR.Infrastructure.Importers;
using System.Threading;
using AionHR.Infrastructure.Session;
using AionHR.Infrastructure.Tokens;
using AionHR.Repository.WebService.Repositories;
using AionHR.Services.Messaging.System;
using AionHR.Infrastructure.Domain;
using AionHR.Model.System;
using AionHR.Model.LoadTracking;
using AionHR.Model.LeaveManagement;


namespace AionHR.Web.UI.Forms
{
    public partial class ImportEmployees : System.Web.UI.Page
    {
        ITimeAttendanceService _branchService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        IReportsService _reportService = ServiceLocator.Current.GetInstance<IReportsService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        protected override void InitializeCulture()
        {

            bool rtl = true;
            if (!_systemService.SessionHelper.CheckIfArabicSession())
            {
                rtl = false;
                base.InitializeCulture();
                LocalisationManager.Instance.SetEnglishLocalisation();
            }

            if (rtl)
            {
                base.InitializeCulture();
                LocalisationManager.Instance.SetArabicLocalisation();
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {




            if (!X.IsAjaxRequest && !IsPostBack)
            {

                SetExtLanguage();
                HideShowButtons();
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(AionHR.Model.Employees.Profile.ImportEmployees), null, null, null, null);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
                }
                BatchStatusRequest req = new BatchStatusRequest();
                req.classId = ClassId.EPEM;
                RecordResponse<BatchOperationStatus> resp = _systemService.ChildGetRecord<BatchOperationStatus>(req);
                if (!resp.Success || resp.result == null)
                {
                    return;
                }
                switch (resp.result.status)
                {
                    case 0:
                        Viewport1.ActiveIndex = 0;
                        break;
                    case 1:


                        Viewport1.ActiveIndex = 1;
                        this.ResourceManager1.AddScript("{0}.startTask('longactionprogress');", this.TaskManager1.ClientID); break;
                    case 2:
                        Viewport1.ActiveIndex = 1;
                        this.ResourceManager1.AddScript("{0}.startTask('longactionprogress');", this.TaskManager1.ClientID); break;
                    case 3:
                        Viewport1.ActiveIndex = 2;
                        break;
                    default: Viewport1.ActiveIndex = 0; break;

                }


            }


        }

        protected void Page_Init(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// the detailed tabs for the edit form. I put two tabs by default so hide unecessary or add addional
        /// </summary>
        private void HideShowTabs()
        {
            //this.OtherInfoTab.Visible = false;
        }



        private void HideShowButtons()
        {

        }


        /// <summary>
        /// hiding uncessary column in the grid. 
        /// </summary>


        private void SetExtLanguage()
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            if (rtl)
            {
                this.ResourceManager1.RTL = true;
                this.Viewport1.RTL = true;

            }
        }







        protected void SubmitFile(object sender, DirectEventArgs e)

        {

            string path = "";
            try
            {


                path = MapPath("~/Imports/" + _systemService.SessionHelper.Get("AccountId") + "/" + fileUpload.FileName);
                Directory.CreateDirectory(MapPath("~/Imports/" + _systemService.SessionHelper.Get("AccountId") + "/"));
                fileUpload.PostedFile.SaveAs(path);
                fileUpload.Reset();
            }
            catch (Exception exp)

            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
                return;
            }
            CurrentPath.Text = path;
            Viewport1.ActiveIndex = 1;
            X.Call("importButton", true);

        }

        protected void ProcessData(object sender, DirectEventArgs e)
        {
            try
            {

                EmployeeImportingService service = null;
                service = new EmployeeImportingService(new CSVImporter(CurrentPath.Text),' ');
                List<Employee> shifts = service.ImportUnvalidated(CurrentPath.Text);

                File.Delete(CurrentPath.Text);





                DictionarySessionStorage storage = new DictionarySessionStorage();
                storage.Save("AccountId", _systemService.SessionHelper.Get("AccountId"));
                storage.Save("UserId", _systemService.SessionHelper.Get("UserId"));
                storage.Save("key", _systemService.SessionHelper.Get("Key"));
                SessionHelper h = new SessionHelper(storage, new APIKeyBasedTokenGenerator());
                IEmployeeService emp = new EmployeeService(new EmployeeRepository(), h);
                SystemService _system = new SystemService(new SystemRepository(), h);
                EmployeeBatchRunner runner = new EmployeeBatchRunner(storage, emp, _system) { Items = shifts, OutputPath = MapPath("~/Imports/" + _systemService.SessionHelper.Get("AccountId") + "/") };
                runner.Process();
                this.ResourceManager1.AddScript("{0}.startTask('longactionprogress');", this.TaskManager1.ClientID);


            }
            catch
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorOperation).Show();
                this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);

            }
        }




        protected void RefreshProgress(object sender, DirectEventArgs e)
        {
            //object progress = _systemService.SessionHelper.Get("LongActionProgress");
            double progress = 0;

            //object prep = _systemService.SessionHelper.Get("Preporcessing");
            BatchStatusRequest req = new BatchStatusRequest();
            req.classId = ClassId.EPEM;
            RecordResponse<BatchOperationStatus> resp = _systemService.ChildGetRecord<BatchOperationStatus>(req);
            if (resp.result == null)
                return;
            if (resp.result.status == 1 || resp.result.status == 2)
            {
                if (resp.result.tableSize == 0)
                    progress = 0;
                else
                    progress = (double)resp.result.processed / resp.result.tableSize;
                string prog = (float.Parse(progress.ToString()) * 100).ToString();
                string message = resp.result.status == 2 ? GetLocalResourceObject("working").ToString() : GetLocalResourceObject("preprocessing").ToString();
                this.Progress1.UpdateProgress(float.Parse(progress.ToString()), string.Format(message + " {0}%", (int)(float.Parse(progress.ToString()) * 100)));

            }



            else if (resp.result.status == 3)
            {
                //this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);
                Viewport1.ActiveIndex = 2;


            }
            else if (resp.result.status == 0)
            {
                this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);
                Viewport1.ActiveIndex = 0;
            }
        }

        protected void DownloadResult(object sender, DirectEventArgs e)
        {
            string attachment = "attachment; filename=MyCsvLol.csv";
            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.ClearHeaders();
            //HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Pragma", "public");
            string content;
            try
            {
                content = File.ReadAllText(MapPath("~/Imports/" + _systemService.SessionHelper.Get("AccountId") + "/" + ClassId.EPEM.ToString() + ".csv"));
            }

            catch (Exception exp)
            {

                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
                return;

            }
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.Write(content);
            PostRequest<BatchOperationStatus> req = new PostRequest<BatchOperationStatus>();
            BatchOperationStatus batch = new BatchOperationStatus();
            batch.classId = ClassId.EPEM;
            batch.status = 0;
            batch.processed = 0;
            batch.tableSize = 0;
            req.entity = batch;
            PostResponse<BatchOperationStatus> resp = _systemService.ChildAddOrUpdate<BatchOperationStatus>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                return;
            }
            Viewport1.ActiveIndex = 0;
            HttpContext.Current.Response.Flush();

            Response.Close();
        }


        [DirectMethod]
        public string CheckSession()
        {
            if (!_systemService.SessionHelper.CheckUserLoggedIn())
            {
                return "0";
            }
            else return "1";
        }

        protected void Unnamed_Event(object sender, DirectEventArgs e)
        {
            PostRequest<BatchOperationStatus> req = new PostRequest<BatchOperationStatus>();
            BatchOperationStatus batch = new BatchOperationStatus();
            batch.classId = ClassId.EPEM;
            batch.status = 0;
            batch.processed = 0;
            batch.tableSize = 0;
            req.entity = batch;
            PostResponse<BatchOperationStatus> resp = _systemService.ChildAddOrUpdate<BatchOperationStatus>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();
                return;
            }
        }
    }
}