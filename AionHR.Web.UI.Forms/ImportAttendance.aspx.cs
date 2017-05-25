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
            b.employeeId = GetEmployeeId(b.employeeRef);


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
        protected void SubmitFile(object sender, DirectEventArgs e)

        {
            AttendanceImportingService service = null;
            try
            {


                string path = MapPath("~/Temp/" + fileUpload.FileName);
                fileUpload.PostedFile.SaveAs(path);
                service = new AttendanceImportingService(new CSVImporter(path), _employeeService);
            }
            catch(Exception exp)

            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
                return;
            }
            try
            {
                List<AttendanceShift> shifts= service.ImportUnvalidated(service.FileName);


                Dictionary<string, string> ids = new Dictionary<string, string>();
                foreach (var item in shifts)
                {
                    if (string.IsNullOrEmpty(item.employeeRef))
                        continue;
                    if (!ids.ContainsKey(item.employeeRef))
                        ids.Add(item.employeeRef, GetEmployeeId(item.employeeRef));
                    item.employeeId = ids[item.employeeRef];
                }
                foreach (var item in shifts)
                {
                    PostRequest<AttendanceShift> req = new PostRequest<AttendanceShift>();
                    req.entity = item;
                    PostResponse<AttendanceShift> resp = _timeAttendanceService.ChildAddOrUpdate<AttendanceShift>(req);
                    if (!resp.Success)
                    {
                        
                        
                        //X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        //X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                        //loadingWindow.Close();
                        //return;
                    }
                 
                }
                attendanceShiftStore.DataSource = shifts;
                attendanceShiftStore.DataBind();
                Viewport1.ActiveIndex = 1;
                X.Call("setDisable");
            }
            catch
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetLocalResourceObject("ErrorImporting").ToString()).Show();

                return;
            }
        }
        
        private string GetEmployeeId(string employeeRef)
        {
            EmployeeByReference req = new EmployeeByReference();
            req.Reference = employeeRef;
            RecordResponse<Employee> resp = _employeeService.ChildGetRecord<Employee>(req);
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
            string msg = string.Format(GetLocalResourceObject("saved").ToString(), periods.Count-failed, failed);
            X.Msg.Alert(Resources.Common.Error, msg).Show();
            ResetPage();


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
    }
}