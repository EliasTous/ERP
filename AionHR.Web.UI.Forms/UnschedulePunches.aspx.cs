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
using AionHR.Model.System;
using AionHR.Model.Attendance;
using AionHR.Model.Employees.Leaves;
using AionHR.Model.Employees.Profile;
using AionHR.Web.UI.Forms.ConstClasses;
using AionHR.Services.Messaging.Reports;
using AionHR.Model.TimeAttendance;
using AionHR.Model.Reports;

namespace AionHR.Web.UI.Forms
{
    public partial class UnschedulePunches : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();

        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        IReportsService _reportsService = ServiceLocator.Current.GetInstance<IReportsService>();

        protected override void InitializeCulture()
        {

            switch (_systemService.SessionHelper.getLangauge())
            {
                case "ar":
                    {
                        base.InitializeCulture();
                        LocalisationManager.Instance.SetArabicLocalisation();
                    }
                    break;
                case "en":
                    {
                        base.InitializeCulture();
                        LocalisationManager.Instance.SetEnglishLocalisation();
                    }
                    break;

                case "fr":
                    {
                        base.InitializeCulture();
                        LocalisationManager.Instance.SetFrenchLocalisation();
                    }
                    break;
                case "de":
                    {
                        base.InitializeCulture();
                        LocalisationManager.Instance.SetGermanyLocalisation();
                    }
                    break;
                default:
                    {


                        base.InitializeCulture();
                        LocalisationManager.Instance.SetEnglishLocalisation();
                    }
                    break;
            }
        }
        [DirectMethod]
        public void SetLabels(string labels)
        {
            this.labels.Text = labels;
        }

        [DirectMethod]
        public void SetVals(string labels)
        {
            this.vals.Text = labels;
        }

        [DirectMethod]
        public void SetTexts(string labels)
        {
            this.texts.Text = labels;
        }
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!X.IsAjaxRequest && !IsPostBack)
            {

                SetExtLanguage();
                HideShowButtons();
                HideShowColumns();
                //try
                //{
                //    AccessControlApplier.ApplyAccessControlOnPage(typeof(CertificateLevel), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
                //}
                //catch (AccessDeniedException exp)
                //{
                //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                //    Viewport1.Hidden = true;
                //    return;
                //}
                currentEmployee.Text = "";
                Column8.Format = _systemService.SessionHelper.GetDateformat();


            }

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
        private void HideShowColumns()
        {
            this.colAttach.Visible = false;
        }


        private void SetExtLanguage()
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            if (rtl)
            {
                this.ResourceManager1.RTL = true;
                this.Viewport1.RTL = true;

            }
        }



        protected void PoPuP(object sender, DirectEventArgs e)
        {
            string employeeId = e.ExtraParams["employeeId"];
            currentEmployee.Text = employeeId;
            string startDate = ""
                , endDate="", branchId="";

            //Dictionary<string, string> parameters = Common.FetchParametersAsDictionary(vals.Text);
            //if (parameters.ContainsKey("1"))
            //    startDate = parameters["1"];
            //if (parameters.ContainsKey("2"))
            //    endDate= parameters["2"];
            //if (parameters.ContainsKey("4"))
            //    branchId= parameters["4"];








            //      reportPanel.LoadContent(new ComponentLoader { Url = "RT309.aspx?_fromUP=true&_employeeId=" + employeeId + "&_startDate=" + startDate + "&_endDate=" + endDate + "&_branchId=" + branchId, Mode = LoadMode.Frame, DisableCaching = true });

            FillStore2(employeeId);

            EditRecordWindow.Show();


        }

        /// <summary>
        /// This direct method will be called after confirming the delete
        /// </summary>
        /// <param name="index">the ID of the object to delete</param>
       




        /// <summary>
        /// Deleting all selected record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteAll(object sender, DirectEventArgs e)
        {


            RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;
            if (sm.SelectedRows.Count() <= 0)
                return;
            X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteManyRecord, new MessageBoxButtonsConfig
            {
                //Calling DoYes the direct method for removing selecte record
                Yes = new MessageBoxButtonConfig
                {
                    Handler = "App.direct.DoYes()",
                    Text = Resources.Common.Yes
                },
                No = new MessageBoxButtonConfig
                {
                    Text = Resources.Common.No
                }

            }).Show();
        }

        /// <summary>
        /// Direct method for removing multiple records
        /// </summary>
        [DirectMethod(ShowMask = true)]
        public void DoYes()
        {
            try
            {
                RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;

                foreach (SelectedRow row in sm.SelectedRows)
                {
                    //Step 1 :Getting the id of the selected record: it maybe string 
                    int id = int.Parse(row.RecordID);


                    //Step 2 : removing the record from the store
                    //To do add code here 

                    //Step 3 :  remove the record from the store
                    Store1.Remove(id);

                }
                //Showing successful notification
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.ManyRecordDeletedSucc
                });

            }
            catch (Exception ex)
            {
                //Alert in case of any failure
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorDeletingRecord).Show();

            }
        }

        /// <summary>
        /// Adding new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      

        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            ReportGenericRequest req = new ReportGenericRequest();
            req.paramString = vals.Text;

           
            ListResponse<UnschedulePunch> resp = _timeAttendanceService.ChildGetAll<UnschedulePunch>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            this.Store1.DataSource = resp.Items;
            e.Total = resp.Items.Count; ;

            this.Store1.DataBind();
        }




        protected void processPunches(object sender, DirectEventArgs e)
        {

            processUnscheduledPunch PUP = new processUnscheduledPunch();
            Dictionary<string, string> parameters = Common.FetchParametersAsDictionary(vals.Text);
            if (parameters.ContainsKey("1"))
                PUP.startDate =DateTime.ParseExact( parameters["1"],"yyyyMMdd", new CultureInfo("en")).ToString("yyyy-MM-dd");
            if (parameters.ContainsKey("2"))
                PUP.endDate = DateTime.ParseExact(parameters["2"], "yyyyMMdd", new CultureInfo("en")).ToString("yyyy-MM-dd");
            
            if (parameters.ContainsKey("4"))
                PUP.branchId = parameters["4"];
            if (parameters.ContainsKey("3"))
                PUP.employeeId = parameters["3"];
           
            PostRequest<processUnscheduledPunch> request = new PostRequest<processUnscheduledPunch>();

            request.entity = PUP;
            PostResponse<processUnscheduledPunch> r = _timeAttendanceService.ChildAddOrUpdate<processUnscheduledPunch>(request);
            if (!r.Success)//it maybe be another condition
            {
                //Show an error saving...
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(r);
                return;
            }
            else
            {
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordSavingSucc
                });

              //  this.EditRecordWindow.Close();
            }

            
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

        protected void BasicInfoTab_Load(object sender, EventArgs e)
        {

        }

        private void FillStore2(string employeeId)
        {
            ReportGenericRequest req = new ReportGenericRequest();
            req.paramString = vals.Text;
           if (string.IsNullOrEmpty( vals.Text))
            {
                req.paramString = "3|" + employeeId; 

            }
           else
            {
                req.paramString = vals.Text+ "^3|" + employeeId;
            }
            ListResponse<AionHR.Model.Reports.RT309> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT309>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            DateTime temp;
            resp.Items.ForEach(x =>
            {
                x.shiftId = buildShiftValue(x.shiftLog);
                if (DateTime.TryParseExact(x.dayId, "yyyyMMdd", new CultureInfo("en"), DateTimeStyles.AdjustToUniversal, out temp))
                x.dayIdDateTime = temp;
            }
 
            );
            store2.DataSource = resp.Items;
            store2.DataBind(); 

        }

        private string buildShiftValue(List<ShiftLog> shifts)
        {
            try
            {
                string shift = "";

                shifts.ForEach(x => {

                    if (string.IsNullOrEmpty(x.start))
                        shift += x.end;
                    else
                    {
                        if (string.IsNullOrEmpty(x.end))
                        {
                            shift += x.start;
                        }
                        else
                            shift += x.start + " - " + x.end;
                    }

                    shift += Environment.NewLine;

                });

                //if (shifts.Count != 0)
                //{
                //    if (string.IsNullOrEmpty(shifts[0].start))
                //        shift = shifts[0].end;
                //    else
                //    {
                //        if (string.IsNullOrEmpty(shifts[0].end))
                //        {
                //            shift = shifts[0].start;
                //        }
                //        else
                //            shift = shifts[0].start + " - " + shifts[0].end;
                //    }

                //    shift += Environment.NewLine;

                //}
                //if (shifts.Count==2)
                //{
                //    shift += Environment.NewLine;
                //    if (string.IsNullOrEmpty(shifts[1].start))
                //        shift += shifts[1].end;
                //    else
                //    {
                //        if (string.IsNullOrEmpty(shifts[1].end))
                //        {
                //            shift += shifts[1].start;
                //        }
                //        else
                //            shift += shifts[1].start + " - " + shifts[1].end;
                //    }
                //}
                return shift;
            }
            catch { return ""; }
        }

        protected void Unnamed_Event()
        {

        }
    }
}