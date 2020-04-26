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
using Services.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Web.UI.Forms.Utilities;
using Model.Company.News;
using Services.Messaging;
using Model.Company.Structure;
using Model.System;
using Model.Attendance;
using Model.Employees.Leaves;
using Model.Employees.Profile;
using Model.Payroll;
using Services.Messaging.Reports;
using Web.UI.Forms.ConstClasses;

namespace Web.UI.Forms
{
    public partial class TimeCodes : System.Web.UI.Page
    {

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();

        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IPayrollService _payrollService = ServiceLocator.Current.GetInstance<IPayrollService>();

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
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!X.IsAjaxRequest && !IsPostBack)
            {

                SetExtLanguage();
                HideShowButtons();
                HideShowColumns();
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(DocumentType), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
                }

                timeCodeStore.DataSource = Common.XMLDictionaryList(_systemService, "3");
                timeCodeStore.DataBind();
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




            try {
               

                string type = e.ExtraParams["type"];
                string edTypeP = e.ExtraParams["edType"];
                string timeCodeP = e.ExtraParams["timeCode"];
                string edIdP = e.ExtraParams["edId"];
                string apIdP = e.ExtraParams["apId"];
                string gracePeriodP = e.ExtraParams["gracePeriod"];
                currentEDtype.Text = edTypeP;
                switch (type)
                {
                    case "imgEdit":
                        //Step 1 : get the object from the Web Service 

                        //Step 2 : call setvalues with the retrieved object

                        TimeCodeRecordRequest r = new TimeCodeRecordRequest();
                        r.timeCode = timeCodeP;
                        RecordResponse<TimeCode> response = _payrollService.ChildGetRecord<TimeCode>(r);
                        if (!response.Success)
                        {
                            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            Common.errorMessage(response);
                            return;
                        }
                        FillApprovalStory();
                        FillensStore();
                        timeCodeStore.DataSource = Common.XMLDictionaryList(_systemService, "3");
                        timeCodeStore.DataBind();
                        this.BasicInfoTab.Reset();
                        //Step 2 : call setvalues with the retrieved object
                        this.BasicInfoTab.SetValues(response.result);
                      if (!String.IsNullOrEmpty(timeCodeP))
                        timecode.Select(timeCodeP);
                        this.EditRecordWindow.Title = Resources.Common.EditWindowsTitle;
                        this.EditRecordWindow.Show();
                        break;

                    case "imgDelete":
                        X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                        {
                            Yes = new MessageBoxButtonConfig
                            {
                                //We are call a direct request metho for deleting a record
                                // Handler = String.Format("App.direct.DeleteRecord({0})", ),
                                Text = Resources.Common.Yes
                            },
                            No = new MessageBoxButtonConfig
                            {
                                Text = Resources.Common.No
                            }

                        }).Show();
                        break;

                    case "imgAttach":

                        //Here will show up a winow relatice to attachement depending on the case we are working on
                        break;
                    default:
                        break;
                }
            }
            catch(Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }


        }

        /// <summary>
        /// This direct method will be called after confirming the delete
        /// </summary>
        /// <param name="index">the ID of the object to delete</param>
        [DirectMethod]
        public void DeleteRecord(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                EmploymentStatus s = new EmploymentStatus();
                s.recordId = index;
                s.name = "";
                //s.intName = "";

                PostRequest<EmploymentStatus> req = new PostRequest<EmploymentStatus>();
                req.entity = s;
                PostResponse<EmploymentStatus> r = _employeeService.ChildDelete<EmploymentStatus>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                   Common.errorMessage(r);;
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    Store1.Remove(index);

                    //Step 3 : Showing a notification for the user 
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordDeletedSucc
                    });
                }

            }
            catch (Exception ex)
            {
                //In case of error, showing a message box to the user
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorDeletingRecord).Show();

            }

        }





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
        protected void ADDNewRecord(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            BasicInfoTab.Reset();
            currentEDtype.Text = "";
            FillApprovalStory();
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;


            this.EditRecordWindow.Show();
        }

        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {


            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;

            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<TimeCode> response = _payrollService.ChildGetAll<TimeCode>(request);
            if (!response.Success)
            {
                Common.errorMessage(response);
                return;

            }
            response.Items.ForEach(x => x.edTypeString = FillEdType(x.edType));

            Store1.DataSource = response.Items;
            e.Total = response.Items.Count;
            Store1.DataBind();

            //List<TimeCode> routers = new List<TimeCode>();
            //routers.Add(new TimeCode { timeCode = ConstTimeVariationType.Day_Bonus });
            //routers.Add(new TimeCode { timeCode = ConstTimeVariationType.DAY_LEAVE_WITHOUT_EXCUSE });
            //routers.Add(new TimeCode { timeCode = ConstTimeVariationType.DURING_SHIFT_LEAVE });
            //routers.Add(new TimeCode { timeCode = ConstTimeVariationType.EARLY_CHECKIN });
            //routers.Add(new TimeCode { timeCode = ConstTimeVariationType.EARLY_LEAVE });
            //routers.Add(new TimeCode { timeCode = ConstTimeVariationType.LATE_CHECKIN });
            //routers.Add(new TimeCode { timeCode = ConstTimeVariationType.MISSED_PUNCH });
            //routers.Add(new TimeCode { timeCode = ConstTimeVariationType.OVERTIME });
            //routers.Add(new TimeCode { timeCode = ConstTimeVariationType.PAID_LEAVE });
            //routers.Add(new TimeCode { timeCode = ConstTimeVariationType.SHIFT_LEAVE_WITHOUT_EXCUSE });
            //routers.Add(new TimeCode { timeCode = ConstTimeVariationType.UNPAID_LEAVE });


            //Fetching the corresponding list

            //in this test will take a list of News
            //ListRequest request = new ListRequest();

            //ListResponse<TimeCode> routers = _payrollService.ChildGetAll<TimeCode>(request);
            //if (!routers.Success)
            //{
            //     Common.errorMessage(routers);
            //    return;
            //}
            
        }




        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.


            string obj = e.ExtraParams["values"];
            TimeCode b = JsonConvert.DeserializeObject<TimeCode>(obj);
            //if (!string.IsNullOrEmpty(timeCodeCombo.Value.ToString()))
            //    b.timeCode = Convert.ToInt16(timeCodeCombo.Value.ToString());

            // Define the object to add or edit as null
            try
            {

                //New Mode
                //Step 1 : Fill The object and insert in the store 
                PostRequest<TimeCode> request = new PostRequest<TimeCode>();

                request.entity = b;
                PostResponse<TimeCode> r = _payrollService.ChildAddOrUpdate<TimeCode>(request);


                //check if the insert failed
                if (!r.Success)//it maybe be another condition
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                   Common.errorMessage(r);;
                    return;
                }
                else
                {

                    //Add this record to the store 
                    Store1.Reload();

                    //Display successful notification
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordSavingSucc
                    });

                    this.EditRecordWindow.Close();




                }
            }
            catch (Exception ex)
            {
                //Error exception displaying a messsage box
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorSavingRecord).Show();
            }


        }
        //else
        //{
        //    //Update Mode

        //    try
        //    {
        //        //getting the id of the record
        //        PostRequest<EmploymentStatus> request = new PostRequest<EmploymentStatus>();
        //        request.entity = b;
        //        PostResponse<EmploymentStatus> r = _employeeService.ChildAddOrUpdate<EmploymentStatus>(request);                      //Step 1 Selecting the object or building up the object for update purpose

        //        //Step 2 : saving to store

        //        //Step 3 :  Check if request fails
        //        if (!r.Success)//it maybe another check
        //        {
        //            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //           Common.errorMessage(r);;
        //            return;
        //        }
        //        else
        //        {


        //            ModelProxy record = this.Store1.GetById(id);
        //            BasicInfoTab.UpdateRecord(record);
        //            record.Commit();
        //            Notification.Show(new NotificationConfig
        //            {
        //                Title = Resources.Common.Notification,
        //                Icon = Icon.Information,
        //                Html = Resources.Common.RecordUpdatedSucc
        //            });
        //            this.EditRecordWindow.Close();


        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
        //    }
        //}


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
        protected void printBtn_Click(object sender, EventArgs e)
        {
            DocumentTypesReport p = GetReport();
            string format = "Pdf";
            string fileName = String.Format("Report.{0}", format);

            MemoryStream ms = new MemoryStream();
            p.ExportToPdf(ms, new DevExpress.XtraPrinting.PdfExportOptions() { ShowPrintDialogOnOpen = true });
            Response.Clear();
            Response.Write("<script>");
            Response.Write("window.document.forms[0].target = '_blank';");
            Response.Write("setTimeout(function () { window.document.forms[0].target = ''; }, 0);");
            Response.Write("</script>");
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "inline", fileName));
            Response.BinaryWrite(ms.ToArray());
            Response.Flush();
            Response.Close();
            //Response.Redirect("Reports/RT301.aspx");
        }
        protected void ExportPdfBtn_Click(object sender, EventArgs e)
        {
            DocumentTypesReport p = GetReport();
            string format = "Pdf";
            string fileName = String.Format("Report.{0}", format);

            MemoryStream ms = new MemoryStream();
            p.ExportToPdf(ms);
            Response.Clear();

            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "attachment", fileName));
            Response.BinaryWrite(ms.ToArray());
            Response.Flush();
            Response.Close();
            //Response.Redirect("Reports/RT301.aspx");
        }

        protected void ExportXLSBtn_Click(object sender, EventArgs e)
        {
            DocumentTypesReport p = GetReport();
            string format = "xls";
            string fileName = String.Format("Report.{0}", format);

            MemoryStream ms = new MemoryStream();
            p.ExportToXls(ms);

            Response.Clear();

            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "attachment", fileName));
            Response.BinaryWrite(ms.ToArray());
            Response.Flush();
            Response.Close();
            //Response.Redirect("Reports/RT301.aspx");
        }
        private DocumentTypesReport GetReport()
        {

            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<DocumentType> resp = _employeeService.ChildGetAll<DocumentType>(request);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(resp);
                return null;
            }
            DocumentTypesReport p = new DocumentTypesReport();
            p.DataSource = resp.Items;
            p.Parameters["User"].Value = _systemService.SessionHelper.GetCurrentUser();
            p.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            p.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;

            return p;



        }
        [DirectMethod]
      private void FillensStore()
        {

            ListRequest req = new ListRequest();
            ListResponse<EntitlementDeduction> eds = _employeeService.ChildGetAll<EntitlementDeduction>(req);
            if (!string.IsNullOrEmpty(currentEDtype.Text))
                entsStore.DataSource = eds.Items.Where(s => s.type == Convert.ToInt16(currentEDtype.Text)).ToList();
            else
                entsStore.DataSource = eds.Items;
            entsStore.DataBind();

        }
        private void FillApprovalStory()
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<Approval> routers = _companyStructureService.ChildGetAll<Approval>(request);

            if (!routers.Success)
                 Common.errorMessage(routers);
            this.ApprovalStore.DataSource = routers.Items;
          

            this.ApprovalStore.DataBind();
        }
      
        private string FillEdType(int? edType)
        {
            string R = "";


            // Retrieve the value of the string resource named "welcome".
            // The resource manager will retrieve the value of the  
            // localized resource using the caller's current culture setting.


            try
            {

                switch (edType)
                {
                    case 1:
                        R = GetLocalResourceObject("Entitlement").ToString();
                        break;
                    case 2:
                        R = GetLocalResourceObject("Deduction").ToString();
                        break;
                    default:R = string.Empty;
                        break;

                }

                return R;
            }
            catch { return string.Empty; }

        }

        protected void Unnamed_Event(object sender, DirectEventArgs e)
        {
            FillensStore();
        }
    }
}
    