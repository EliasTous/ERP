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

namespace AionHR.Web.UI.Forms
{
    public partial class ImportAttendance : System.Web.UI.Page
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

                BatchStatusRequest req = new BatchStatusRequest();
                req.classId = ClassId.TAAS;
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


        protected void Prev_Click(object sender, DirectEventArgs e)
        {
            int index = int.Parse(e.ExtraParams["index"]);

            if ((index - 1) >= 0)
            {
                ResetPage();

            }


        }

        protected void SaveShift(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["recordId"];


            string obj = e.ExtraParams["values"];
            AttendanceShift b = JsonConvert.DeserializeObject<AttendanceShift>(obj);

            DateTime s;
            string dayId = "";
            if (DateTime.TryParse(b.dayId.ToString(), out s))
                dayId = s.ToString("ddMMyyyy");
            else
                dayId = b.dayId;

            b.dayId = dayId;
            // Define the object to add or edit as null
            b.employeeId = GetEmployeeId((EmployeeService)this._employeeService, b.employeeRef);


            //Update Mode




            ModelProxy record = this.attendanceShiftStore.GetById(id);

            EditShiftForm.UpdateRecord(record);
            record.Set("dayId", b.dayId);
            record.Set("employeeId", b.employeeId);
            record.Commit();
            Notification.Show(new NotificationConfig
            {
                Title = Resources.Common.Notification,
                Icon = Icon.Information,
                Html = Resources.Common.RecordUpdatedSucc
            });
            this.EditShiftWindow.Close();


            X.Call("setDisable");



        }
        //protected void SubmitFile(object sender, DirectEventArgs e)

        //{
        //    AttendanceImportingService service = null;
        //    try
        //    {


        //        string path = MapPath("~/Temp/" + fileUpload.FileName);
        //        fileUpload.PostedFile.SaveAs(path);
        //        service = new AttendanceImportingService(new CSVImporter(path), _employeeService);
        //    }
        //    catch(Exception exp)

        //    {
        //        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //        X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
        //        return;
        //    }
        //    try
        //    {
        //        List<AttendanceShift> shifts= service.ImportUnvalidated(service.FileName);


        //        Dictionary<string, string> ids = new Dictionary<string, string>();
        //        foreach (var item in shifts)
        //        {
        //            if (string.IsNullOrEmpty(item.employeeRef))
        //                continue;
        //            if (!ids.ContainsKey(item.employeeRef))
        //                ids.Add(item.employeeRef, GetEmployeeId(item.employeeRef));
        //            item.employeeId = ids[item.employeeRef];
        //        }
        //        foreach (var item in shifts)
        //        {
        //            PostRequest<AttendanceShift> req = new PostRequest<AttendanceShift>();
        //            req.entity = item;
        //            PostResponse<AttendanceShift> resp = _timeAttendanceService.ChildAddOrUpdate<AttendanceShift>(req);
        //            if (!resp.Success)
        //            {


        //                //X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //                //X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
        //                //loadingWindow.Close();
        //                //return;
        //            }

        //        }
        //        attendanceShiftStore.DataSource = shifts;
        //        attendanceShiftStore.DataBind();
        //        Viewport1.ActiveIndex = 1;
        //        X.Call("setDisable");
        //    }
        //    catch
        //    {
        //        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //        X.Msg.Alert(Resources.Common.Error, GetLocalResourceObject("ErrorImporting").ToString()).Show();

        //        return;
        //    }
        //}

        protected void SubmitFile(object sender, DirectEventArgs e)

        {
            AttendanceImportingService service = null;
            string path = "";
            try
            {


                path = MapPath("~/Imports/" + _systemService.SessionHelper.Get("AccountId") + "/" + fileUpload.FileName);
                Directory.CreateDirectory(MapPath("~/Imports/" + _systemService.SessionHelper.Get("AccountId") + "/"));
                fileUpload.PostedFile.SaveAs(path);
                service = new AttendanceImportingService(new CSVImporter(path), _employeeService);
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

                AttendanceImportingService service = null;
                service = new AttendanceImportingService(new CSVImporter(CurrentPath.Text), _employeeService);
                List<AttendanceShift> shifts = service.ImportUnvalidated(CurrentPath.Text);

                File.Delete(CurrentPath.Text);





                DictionarySessionStorage storage = new DictionarySessionStorage();
                storage.Save("AccountId", _systemService.SessionHelper.Get("AccountId"));
                storage.Save("UserId", _systemService.SessionHelper.Get("UserId"));
                storage.Save("key", _systemService.SessionHelper.Get("Key"));
                SessionHelper h = new SessionHelper(storage, new APIKeyBasedTokenGenerator());
                EmployeeService emp = new EmployeeService(new EmployeeRepository(), h);
                TimeAttendanceService _timeAtt = new TimeAttendanceService(h, new TimeAttendanceRepository());
                SystemService _system = new SystemService(new SystemRepository(), h);
                AttendanceBatchRunner runner = new AttendanceBatchRunner(storage, _timeAtt, _system, emp) { Items = shifts, OutputPath= MapPath("~/Imports/" + _systemService.SessionHelper.Get("AccountId") + "/") };
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

        private string GetEmployeeId(EmployeeService serv, string employeeRef)
        {
            EmployeeByReference req = new EmployeeByReference();
            req.Reference = employeeRef;
            RecordResponse<Employee> resp = serv.ChildGetRecord<Employee>(req);
            if (resp == null || resp.result == null)
                return "";
            else
                return resp.result.recordId;
        }

        private void ResetPage()
        {
            Viewport1.ActiveIndex = 0;
            attendanceShiftStore.DataSource = new List<AttendanceShift>();
            attendanceShiftStore.DataBind();
            fileUpload.Reset();
        }
        protected void PoPuPShift(object sender, DirectEventArgs e)
        {


            string id = e.ExtraParams["id"];
            string dayId = e.ExtraParams["dayId"];
            string checkIn = e.ExtraParams["checkIn"];
            string checkOut = e.ExtraParams["checkOut"];
            string type = e.ExtraParams["type"];
            string employeeRef = e.ExtraParams["employeeId"];
            switch (type)
            {
                case "imgEdit":
                    recordId.Text = id;
                    this.dayId.Text = dayId;
                    this.checkIn.Text = checkIn;
                    this.checkOut.Text = checkOut;
                    this.employeeRef.Text = employeeRef;
                    EditShiftWindow.Show();



                    break;
                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteRecord('{0}')", id),
                            Text = Resources.Common.Yes
                        },
                        No = new MessageBoxButtonConfig
                        {
                            Text = Resources.Common.No
                        }

                    }).Show();
                    break;
                default: break;
            }


        }
        [DirectMethod]
        public void DeleteRecord(string index)
        {

            //Step 1 Code to delete the object from the database 

            //Step 2 :  remove the object from the store
            attendanceShiftStore.Remove(index);

            //Step 3 : Showing a notification for the user 
            Notification.Show(new NotificationConfig
            {
                Title = Resources.Common.Notification,
                Icon = Icon.Information,
                Html = Resources.Common.RecordDeletedSucc
            });

            X.Call("setDisable");



        }
        protected void UploadAttendances(object sender, DirectEventArgs e)
        {
            string atts = e.ExtraParams["attendances"];
            List<AttendanceShift> periods = JsonConvert.DeserializeObject<List<AttendanceShift>>(atts);


            int steps = periods.Count;
            int current = 0;
            int failed = 0;
            foreach (var item in periods)
            {
                PostRequest<AttendanceShift> req = new PostRequest<AttendanceShift>();
                req.entity = item;
                PostResponse<AttendanceShift> resp = _timeAttendanceService.ChildAddOrUpdate<AttendanceShift>(req);
                if (!resp.Success)
                {
                    failed++;
                    //X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    //X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                    //loadingWindow.Close();
                    //return;
                }
                progressBar.UpdateProgress((current++) / steps);
            }

            loadingWindow.Close();
            string msg = string.Format(GetLocalResourceObject("saved").ToString(), periods.Count - failed, failed);
            X.Msg.Alert(Resources.Common.Error, msg).Show();
            ResetPage();


        }


        private void LongAction(object state)
        {
            BackgroundWork<AttendanceShift> bw = (BackgroundWork<AttendanceShift>)state;
            List<AttendanceShift> shifts = bw.Items;
            if (shifts == null)
                return;
            int i = 0;
            SessionHelper h = new SessionHelper(bw.SessionStorage, new APIKeyBasedTokenGenerator());
            EmployeeService emp = new EmployeeService(new EmployeeRepository(), h);
            Dictionary<string, string> ids = new Dictionary<string, string>();
            Session.Add("Preporcessing", 0);
            foreach (var item in shifts)
            {
                if (string.IsNullOrEmpty(item.employeeRef))
                    continue;
                if (!ids.ContainsKey(item.employeeRef))
                    ids.Add(item.employeeRef, GetEmployeeId(emp, item.employeeRef));
                item.employeeId = ids[item.employeeRef];
                i++;
                float percentage = ((float)i / shifts.Count);

                this.Session["Preporcessing"] = (percentage);
            }
            this.Session["Preporcessing"] = null;
            this.Session["LongActionProgress"] = 0;
            List<AttendanceShift> errors = new List<AttendanceShift>();
            TimeAttendanceService service = new TimeAttendanceService(h, new TimeAttendanceRepository());
            i = 0;
            foreach (var item in shifts)
            {
                PostRequest<AttendanceShift> req = new PostRequest<AttendanceShift>();
                req.entity = item;
                //Thread.Sleep(10);
                PostResponse<AttendanceShift> resp = _timeAttendanceService.ChildAddOrUpdate<AttendanceShift>(req);
                if (!resp.Success)
                {
                    errors.Add(item);
                }
                i++;
                float percentage = ((float)i / shifts.Count);

                this.Session["LongActionProgress"] = (percentage);

            }
            StringBuilder b = new StringBuilder();
            foreach (var error in errors)
            {
                b.Append(error.employeeRef + "," + error.dayId + "," + error.checkIn + "," + error.checkOut + "\n");

            }
            string csv = b.ToString();
            this.Session.Add("result", csv);
            this.Session.Remove("LongActionProgress");

        }
        protected void RefreshProgress(object sender, DirectEventArgs e)
        {
            //object progress = _systemService.SessionHelper.Get("LongActionProgress");
            double progress = 0;

            //object prep = _systemService.SessionHelper.Get("Preporcessing");
            BatchStatusRequest req = new BatchStatusRequest();
            req.classId = ClassId.TAAS;
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



            else
            {
                this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);
                Viewport1.ActiveIndex = 2;


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
                content = File.ReadAllText(MapPath("~/Imports/" + _systemService.SessionHelper.Get("AccountId") + "/" + ClassId.TAAS.ToString() + ".csv"));
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
            batch.classId = ClassId.TAAS;
            batch.status = 0;
            batch.processed = 0;
            batch.tableSize = 0;
            req.entity = batch;
            PostResponse<BatchOperationStatus> resp = _systemService.ChildAddOrUpdate<BatchOperationStatus>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
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
            batch.classId = ClassId.TAAS;
            batch.status = 0;
            batch.processed = 0;
            batch.tableSize = 0;
            req.entity = batch;
            PostResponse<BatchOperationStatus> resp = _systemService.ChildAddOrUpdate<BatchOperationStatus>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return;
            }
        }
    }
}