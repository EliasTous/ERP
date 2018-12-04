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
using AionHR.Model.TimeAttendance;
using Reports;
using AionHR.Model.Payroll;
using DevExpress.XtraReports.UI;
using AionHR.Services.Messaging.LoanManagment;
using AionHR.Model.LoadTracking;
using AionHR.Services.Implementations;
using AionHR.Model.Benefits;
using AionHR.Services.Messaging.Benefits;
using AionHR.Services.Messaging.HelpFunction;
using AionHR.Model.HelpFunction;
using AionHR.Web.UI.Forms.ConstClasses;

namespace AionHR.Web.UI.Forms
{
    public partial class BenefitAcquisitions : System.Web.UI.Page
    {

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IPayrollService _payrollService = ServiceLocator.Current.GetInstance<IPayrollService>();

        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ILoanTrackingService _loanService = ServiceLocator.Current.GetInstance<ILoanTrackingService>();
        IBenefitsService _benefitsService = ServiceLocator.Current.GetInstance<IBenefitsService>();
        IHelpFunctionService _helpFunctionService = ServiceLocator.Current.GetInstance<IHelpFunctionService>();
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
                HideShowColumns();
                //       BuildQuickViewTemplate();
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(FinalSettlement), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
                }
                //try
                //{
                //    AccessControlApplier.ApplyAccessControlOnPage(typeof(FinalEntitlementsDeductions), ENForm, entitlementsGrid, Button11, SaveENButton);
                //}
                //catch (AccessDeniedException exp)
                //{
                //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                //    Viewport1.Hidden = true;
                //    return;
                //}

                //try
                //{
                //    AccessControlApplier.ApplyAccessControlOnPage(typeof(FinalEntitlementsDeductions), DEForm, deductionGrid, Button14, SaveDEButton);
                //}
                //catch (AccessDeniedException exp)
                //{
                //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                //    Viewport1.Hidden = true;
                //    return;
                //}


               ColdateFrom.Format= ColDateTo.Format= ColAqDate.Format= lastLeaveStartDate.Format=lastLeaveEndDate.Format = hireDateDf.Format= dateFrom.Format = dateTo.Format = aqDate.Format = ColAqDate.Format = ColdateFrom.Format = ColDateTo.Format = dateFormat.Text = _systemService.SessionHelper.GetDateformat();
                //bsIdHidden.Text = "0";
                //FillBenefits();
                EditMode.Text = "false";
                benefitsFilterStore.Reload();
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
                //    BuildQuickViewTemplate();

            }
        }





        /// <summary>
        /// This direct method will be called after confirming the delete
        /// </summary>
        /// <param name="index">the ID of the object to delete</param>
        [DirectMethod]
        public void DeleteRecord(string id)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                BenefitAcquisition s = new BenefitAcquisition();
                s.recordId = id;

                //s.intName = "";

                PostRequest<BenefitAcquisition> req = new PostRequest<BenefitAcquisition>();
                req.entity = s;
                PostResponse<BenefitAcquisition> r = _benefitsService.ChildDelete<BenefitAcquisition>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                   Common.errorMessage(r);;
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    Store1.Reload();

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
           
            dateFrom.Disabled = true;
            dateTo.Disabled = true;
            EditMode.Text = "false";
            panelRecordDetails.ActiveIndex = 0;
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;
            recordId.Text = "";
            bsIdHidden.Text = "";
            Store3.DataSource = new List<ScheduleBenefits>();
            Store3.DataBind();
            //deductionGrid.Disabled = true;
            //entitlementsGrid.Disabled = true;
            //finalSetlemntRecordId.Text = "";
            //dateId.Value = DateTime.Now;
            //      this.setFillEmployeeInfoDisable(true);
            aqDate.SelectedDate = DateTime.Now;
            this.EditRecordWindow.Show();
        }

        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);



            List<Employee> data = GetEmployeesFiltered(prms.Query);

            data.ForEach(s => s.fullName = s.name.fullName);
            //  return new
            // {
            return data;
            //};

        }



        private List<Employee> GetEmployeesFiltered(string query)
        {

            EmployeeListRequest req = new EmployeeListRequest();
            req.DepartmentId = "0";
            req.BranchId = "0";
            req.IncludeIsInactive = 0;
            req.SortBy = "firstName";

            req.StartAt = "1";
            req.Size = "20";
            req.Filter = query;




            ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
            if (!response.Success)
                Common.errorMessage(response);
            return response.Items;
        }


        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;


            

            //Fetching the corresponding list

            //in this test will take a list of News
            BenefitAcquisitionsListRequest req = new BenefitAcquisitionsListRequest();
            req.StartAt = "0";
            req.Size = "50";
            
            if (string.IsNullOrEmpty(employeeFilter.SelectedItem.Value))
                req.employeeId = 0;
            else
                req.employeeId = Convert.ToInt32(employeeFilter.SelectedItem.Value);

            if (string.IsNullOrEmpty(benefitFilter.SelectedItem.Value))
                req.benefitId = 0;
            else
                req.benefitId = Convert.ToInt32(benefitFilter.SelectedItem.Value);

            ListResponse<BenefitAcquisition> routers = _benefitsService.ChildGetAll<BenefitAcquisition>(req);
            if (!routers.Success)
            {
                 Common.errorMessage(routers);
                return;
            }
            this.Store1.DataSource = routers.Items;
            e.Total = routers.Items.Count;
            totalFsCount.Text = routers.Items.Count.ToString();

            this.Store1.DataBind();
        }


        private void FillBenefits()
        {
                    


            ScheduleBenefitsListRequest request = new ScheduleBenefitsListRequest();

            request.Filter = "";
            request.bsId = bsIdHidden.Text;

            ListResponse<ScheduleBenefits> response = _benefitsService.ChildGetAll<ScheduleBenefits>(request);

            if (!response.Success)
            {
                Common.errorMessage(response);
                return;
            }
            response.Items.RemoveAll(x => x.isChecked == false);
                      
                Store3.DataSource = response.Items;
                Store3.DataBind();
            
         
        }


        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {




            string obj = e.ExtraParams["values"];

            string employeeId = e.ExtraParams["employeeId"];
            string CoBenefitId = e.ExtraParams["ComboBenefitId"];

            BenefitAcquisition b = JsonConvert.DeserializeObject<BenefitAcquisition>(obj);
            b.benefitId = Convert.ToInt32(CoBenefitId);
            b.bsId = Convert.ToInt32(bsIdHidden.Text);
            string id = e.ExtraParams["id"];


            // Define the object to add or edit as null


            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<BenefitAcquisition> request = new PostRequest<BenefitAcquisition>();


                    request.entity = b;

                    PostResponse<BenefitAcquisition> r = _benefitsService.ChildAddOrUpdate<BenefitAcquisition>(request);


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


                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });
                        Store1.Reload();
                        this.EditRecordWindow.Close();

                        //entitlementsGrid.Disabled = false;
                        //deductionGrid.Disabled = false;

                      




                    }
                }
                catch (Exception ex)
                {
                    //Error exception displaying a messsage box
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorSavingRecord).Show();
                }


            }
            else
            {
                //Update Mode

                try
                {
                    //getting the id of the record
                    PostRequest<BenefitAcquisition> request = new PostRequest<BenefitAcquisition>();
                    request.entity = b;
                    PostResponse<BenefitAcquisition> r = _benefitsService.ChildAddOrUpdate<BenefitAcquisition>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                    //Step 2 : saving to store

                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                       Common.errorMessage(r);;
                        return;
                    }
                    else
                    {



                        Store1.Reload();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });
                        this.EditRecordWindow.Close();


                    }

                }
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }
            }
            EditMode.Text = "false";
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

        protected void PoPuP(object sender, DirectEventArgs e)
        {
            panelRecordDetails.ActiveIndex = 0;
            EditMode.Text = "true";
            dateFrom.Disabled = false;
            dateTo.Disabled = false;

            //deductionGrid.Disabled = false;
            //entitlementsGrid.Disabled = false;
       //     this.setFillEmployeeInfoDisable(true);

            string type = e.ExtraParams["type"];
            string id = e.ExtraParams["recordId"];


            switch (type)
            {
                case "imgEdit":
                    //entitlementsStore.Reload();
                    //deductionStore.Reload();
              //      setFillEmployeeInfoDisable(false);

                    //Step 1 : get the object from the Web Service 
                    RecordRequest req = new RecordRequest();
                    req.RecordID = id;
                    RecordResponse<BenefitAcquisition> r = _benefitsService.ChildGetRecord<BenefitAcquisition>(req);
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                       Common.errorMessage(r);;
                        return;
                    }
                    else
                    {

                        //Step 2 : call setvalues with the retrieved object
                        r.result.period = returnPeriodDateFriendly(r.result.period);
                        employeeId.GetStore().Add(new object[]
                         {
                                new
                                {
                                    recordId = r.result.employeeId,
                                    fullName =r.result.employeeName.fullName
                                }
                         });

                        employeeId.SetValue(r.result.employeeId);
                        FillEmployeeInfo(sender, e);
                        this.BasicInfoTab.SetValues(r.result);
                        bsIdHidden.Text = r.result.bsId.ToString();
                        FillBenefits();
                        ComboBenefitId.SetValue(r.result.benefitId);
                        deliveryType.SetValue(r.result.deliveryType.ToString()); 
                        this.EditRecordWindow.Title = Resources.Common.EditWindowsTitle;
                        this.EditRecordWindow.Show();
                        break;
                    }
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

                case "imgAttach":

                    //Here will show up a winow relatice to attachement depending on the case we are working on
                    break;
                default:
                    break;
            }


        }

        //protected void printBtn_Click(object sender, EventArgs e)
        //{
        //    OvertimeSettingsReport p = GetReport();
        //    string format = "Pdf";
        //    string fileName = String.Format("Report.{0}", format);

        //    MemoryStream ms = new MemoryStream();
        //    p.ExportToPdf(ms, new DevExpress.XtraPrinting.PdfExportOptions() { ShowPrintDialogOnOpen = true });
        //    Response.Clear();
        //    Response.Write("<script>");
        //    Response.Write("window.document.forms[0].target = '_blank';");
        //    Response.Write("setTimeout(function () { window.document.forms[0].target = ''; }, 0);");
        //    Response.Write("</script>");
        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "inline", fileName));
        //    Response.BinaryWrite(ms.ToArray());
        //    Response.Flush();
        //    Response.Close();
        //    //Response.Redirect("Reports/RT301.aspx");
        //}
        //protected void ExportPdfBtn_Click(object sender, EventArgs e)
        //{
        //    OvertimeSettingsReport p = GetReport();
        //    string format = "Pdf";
        //    string fileName = String.Format("Report.{0}", format);

        //    MemoryStream ms = new MemoryStream();
        //    p.ExportToPdf(ms);
        //    Response.Clear();

        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "attachment", fileName));
        //    Response.BinaryWrite(ms.ToArray());
        //    Response.Flush();
        //    Response.Close();
        //    //Response.Redirect("Reports/RT301.aspx");
        //}

        //protected void ExportXLSBtn_Click(object sender, EventArgs e)
        //{
        //    OvertimeSettingsReport p = GetReport();
        //    string format = "xls";
        //    string fileName = String.Format("Report.{0}", format);

        //    MemoryStream ms = new MemoryStream();
        //    p.ExportToXls(ms);

        //    Response.Clear();

        //    Response.ContentType = "application/vnd.ms-excel";
        //    Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "attachment", fileName));
        //    Response.BinaryWrite(ms.ToArray());
        //    Response.Flush();
        //    Response.Close();
        //    //Response.Redirect("Reports/RT301.aspx");
        //}
        //private OvertimeSettingsReport GetReport()
        //{

        //    ListRequest request = new ListRequest();
        //    request.Filter = "" + "&_employeeId=0&_dayId=";

        //    ListResponse<OvertimeSetting> resp = _timeAttendanceService.ChildGetAll<OvertimeSetting>(request);
        //    if (!resp.Success)
        //    {
        //        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //       Common.errorMessage(resp);
        //        return null;
        //    }
        //    OvertimeSettingsReport p = new OvertimeSettingsReport();
        //    p.DataSource = resp.Items;
        //    p.Parameters["User"].Value = _systemService.SessionHelper.GetCurrentUser();
        //    p.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
        //    p.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
        //    //p.Parameters["Yes"].Value = GetGlobalResourceObject("Common", "Yes").ToString();
        //    //p.Parameters["No"].Value = GetGlobalResourceObject("Common", "No").ToString();
        //    return p;



        //}

        protected void FillEmployeeInfo(object sender, DirectEventArgs e)
        {
            string empId = e.ExtraParams["EmpId"];
         
            if (string.IsNullOrEmpty(empId))
                empId = employeeId.Value.ToString();
            if (EditMode.Text == "false")
                bsIdHidden.Text = ""; 

            EmployeeQuickViewRecordRequest req = new EmployeeQuickViewRecordRequest();
            req.RecordID = empId;
            req.asOfDate = DateTime.Now;
            RecordResponse<EmployeeQuickView> routers = _employeeService.ChildGetRecord<EmployeeQuickView>(req);
            if (!routers.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(routers);
                return;
            }
            //RecordRequest req = new RecordRequest();
            //req.RecordID = employeeId.Value.ToString();
            //RecordResponse<Employee> routers = _employeeService.Get<Employee>(req);
            //if (!routers.Success)
            //{
            //    Common.errorMessage(routers);
            //    return;
            //}
       //     this.setFillEmployeeInfoDisable(false);

            if (string.IsNullOrEmpty(bsIdHidden.Text))
            {

                EmployeeRecruitmentRecordRequest recReq = new EmployeeRecruitmentRecordRequest();
                recReq.EmployeeId = empId;
                RecordResponse<HireInfo> recRes = _employeeService.ChildGetRecord<HireInfo>(recReq);
                if (!recRes.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", recRes.ErrorCode) != null ? GetGlobalResourceObject("Errors", recRes.ErrorCode).ToString()+"<br>"+GetGlobalResourceObject("Errors","ErrorLogId") +recRes.LogId : recRes.Summary).Show();
                    return;
                }
                if (recRes.result != null)
                {
                    if (!string.IsNullOrEmpty(recRes.result.bsId.ToString()))
                    {
                        bsIdHidden.Text = recRes.result.bsId.ToString();
                    }
                }

                if (string.IsNullOrEmpty(bsIdHidden.Text))
                {
                    RecordRequest empReq = new RecordRequest();
                    empReq.RecordID = empId;
                    RecordResponse<Employee> empRes = _employeeService.Get<Employee>(empReq);
                    if (!empRes.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", empRes.ErrorCode) != null ? GetGlobalResourceObject("Errors", empRes.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + empRes.LogId : empRes.Summary).Show();
                        return;
                    }
                   
                    if (empRes.result != null&& empRes.result.nationalityId!=null)
                    {
                        RecordRequest naReq = new RecordRequest();
                        naReq.RecordID = empRes.result.nationalityId.ToString();
                        RecordResponse<Nationality> naRes = _systemService.ChildGetRecord<Nationality>(naReq);
                        if (!naRes.Success)
                        {
                            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", naRes.ErrorCode) != null ? GetGlobalResourceObject("Errors", naRes.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + naRes.LogId : naRes.Summary).Show();
                            return;
                        }
                        if (naRes.result != null)
                        {
                            if (!string.IsNullOrEmpty(naRes.result.bsId))
                            {
                                bsIdHidden.Text = naRes.result.bsId.ToString();

                            }
                        }
                    }


                }
                if (string.IsNullOrEmpty(bsIdHidden.Text))
                {
                    ListRequest deReq = new ListRequest();
                    ListResponse<KeyValuePair<string, string>> defRes = _systemService.ChildGetAll<KeyValuePair<string, string>>(deReq);
                    if (!defRes.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", defRes.ErrorCode) != null ? GetGlobalResourceObject("Errors", defRes.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + defRes.LogId : defRes.Summary).Show();
                        return;
                    }
                    if (defRes.Items.Where(x => x.Key == "bsId").Count() != 0)
                        bsIdHidden.Text = defRes.Items.Where(x => x.Key == "bsId").First().Value.ToString();

                }









                if (!string.IsNullOrEmpty(bsIdHidden.Text))
                {
                    FillBenefits();
                
                }
                else
                    bsIdHidden.Text = "";
                ListRequest request = new ListRequest();

                request.Filter = "";
                ListResponse<BenefitsSchedule> benefitsList = _benefitsService.ChildGetAll<BenefitsSchedule>(request);

                if (!benefitsList.Success)
                    return;
                if (!string.IsNullOrEmpty(bsIdHidden.Text) && benefitsList.Items.Where(x => x.recordId == bsIdHidden.Text).ToList().Count!=0)
                   bsName.Text = benefitsList.Items.Where(x => x.recordId == bsIdHidden.Text).ToList().First().name;

            }

            if (routers.result == null)
                return;

            branchNameTx.Text = routers.result.branchName;
            departmentNameTx.Text = routers.result.departmentName;
            positionNameTx.Text = routers.result.positionName;
            hireDateDf.Value = routers.result.hireDate;
            nationalityTx.Text = routers.result.countryName;
            divisionName.Text = routers.result.divisionName;
            reportToName.Text = routers.result.reportToName.fullName;
            eosBalance.Text = routers.result.indemnity.ToString();
            lastLeaveStartDate.Value = routers.result.lastLeaveStartDate;
            lastLeaveEndDate.Value = routers.result.lastLeaveEndDate;
            leavesBalanceNF.Text = routers.result.leaveBalance.ToString();
            allowedLeaveYtd.Text = routers.result.earnedLeavesLeg.ToString();
            serviceDuration.Text = routers.result.serviceDuration;
            esName.Text = routers.result.esName;
            paidLeavesYTD.Text = routers.result.usedLeavesLeg.ToString();
            loanBalance.Text = routers.result.loanBalance.ToString();







        }
        //private void setFillEmployeeInfoDisable(bool YES)
        //{
        //    if (!YES)
        //    {
        //        branchNameTx.Disabled = false;
        //        departmentNameTx.Disabled = false;
        //        positionNameTx.Disabled = false;
        //        hireDateDf.Disabled = false;
        //        nationalityTx.Disabled = false;
        //        divisionName.Disabled = false;
        //        reportToName.Disabled = false;
        //        eosBalance.Disabled = false;
        //        lastLeaveStartDate.Disabled = false;
        //        lastLeaveEndDate.Disabled = false;
        //        leavesBalance.Disabled = false;
        //        allowedLeaveYtd.Disabled = false;
        //        serviceDuration.Disabled = false;
        //        esName.Disabled = false;
        //        paidLeavesYTD.Disabled = false;
        //        loanBalance.Disabled = false;



        //    }
        //    else
        //    {
        //        branchNameTx.Disabled = true;
        //        departmentNameTx.Disabled = true;
        //        positionNameTx.Disabled = true;
        //        hireDateDf.Disabled = true;
        //        nationalityTx.Disabled = true;
        //        divisionName.Disabled = true;
        //        reportToName.Disabled = true;
        //        eosBalance.Disabled = true;
        //        lastLeaveStartDate.Disabled = true;
        //        lastLeaveEndDate.Disabled = true;
        //        leavesBalance.Disabled = true;
        //        allowedLeaveYtd.Disabled = true;
        //        serviceDuration.Disabled = true;
        //        esName.Disabled = true;
        //        paidLeavesYTD.Disabled = true;
        //        loanBalance.Disabled = true;

        //    }

        //}
        //protected void PoPuPEN(object sender, DirectEventArgs e)
        //{

        //    isAddEn.Text = "";
        //    int fsId = Convert.ToInt32(e.ExtraParams["fsId"]);
        //    int seqNo = Convert.ToInt32(e.ExtraParams["seqNo"]);
        //    string entitlement = "";

        //    string type = e.ExtraParams["type"];

        //    switch (type)
        //    {


        //        case "imgEdit":

        //            string record = e.ExtraParams["values"];
        //            FinalEntitlementsDeductionsRecordRequest req = new FinalEntitlementsDeductionsRecordRequest();

        //            req.fsId = fsId;
        //            req.seqNo = seqNo;
        //            RecordResponse<FinalEntitlementsDeductions> res = _payrollService.ChildGetRecord<FinalEntitlementsDeductions>(req);
        //            //entsStore.Reload();
        //            if (!res.Success)
        //            {
        //                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", res.ErrorCode) != null ? GetGlobalResourceObject("Errors", res.ErrorCode).ToString() : res.Summary).Show();
        //                return;
        //            }

        //            this.ENForm.SetValues(res.result);

        //            //  recordId.Text = ssId;
        //            this.ENForm.Title = Resources.Common.EditWindowsTitle;
        //            entEdId.GetStore().Add(new object[]
        //              {
        //                        new
        //                        {
        //                            recordId = res.result.edId,
        //                            name =res.result.edName
        //                        }
        //              });
        //            entEdId.SetValue(res.result.edId);

        //            EditENWindow.Show();
        //            break;
        //        case "imgDelete":
        //            entitlement = e.ExtraParams["values"];
        //            // FinalEntitlementsDeductions = JsonConvert.DeserializeObject<FinalEntitlementsDeductions>(entitlement)[0];

        //            X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
        //            {
        //                Yes = new MessageBoxButtonConfig
        //                {
        //                    //We are call a direct request metho for deleting a record
        //                    Handler = String.Format("App.direct.DeleteEN({0},{1})", fsId, seqNo),
        //                    Text = Resources.Common.Yes
        //                },
        //                No = new MessageBoxButtonConfig
        //                {
        //                    Text = Resources.Common.No
        //                }

        //            }).Show();

        //            break;

        //        default:
        //            break;
        //    }


        //}

        //protected void PoPuPDE(object sender, DirectEventArgs e)
        //{


        //    isAddEn.Text = "";
        //    int fsId = Convert.ToInt32(e.ExtraParams["fsId"]);
        //    int seqNo = Convert.ToInt32(e.ExtraParams["seqNo"]);
        //    string entitlement = "";

        //    string type = e.ExtraParams["type"];

        //    switch (type)
        //    {


        //        case "imgEdit":

        //            string record = e.ExtraParams["values"];
        //            FinalEntitlementsDeductionsRecordRequest req = new FinalEntitlementsDeductionsRecordRequest();

        //            req.fsId = fsId;
        //            req.seqNo = seqNo;
        //            RecordResponse<FinalEntitlementsDeductions> res = _payrollService.ChildGetRecord<FinalEntitlementsDeductions>(req);
        //            //entsStore.Reload();
        //            if (!res.Success)
        //            {
        //                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", res.ErrorCode) != null ? GetGlobalResourceObject("Errors", res.ErrorCode).ToString() : res.Summary).Show();
        //                return;
        //            }
        //            this.DEForm.SetValues(res.result);

        //            //  recordId.Text = ssId;
        //            this.DEForm.Title = Resources.Common.EditWindowsTitle;
        //            dedEdId.GetStore().Add(new object[]
        //              {
        //                        new
        //                        {
        //                            recordId = res.result.edId,
        //                            name =res.result.edName
        //                        }
        //              });
        //            dedEdId.SetValue(res.result.edId);
        //            EditDEWindow.Show();
        //            break;
        //        case "imgDelete":
        //            entitlement = e.ExtraParams["values"];
        //            // FinalEntitlementsDeductions = JsonConvert.DeserializeObject<FinalEntitlementsDeductions>(entitlement)[0];

        //            X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
        //            {
        //                Yes = new MessageBoxButtonConfig
        //                {
        //                    //We are call a direct request metho for deleting a record
        //                    Handler = String.Format("App.direct.DeleteDE({0},{1})", fsId, seqNo),
        //                    Text = Resources.Common.Yes
        //                },
        //                No = new MessageBoxButtonConfig
        //                {
        //                    Text = Resources.Common.No
        //                }

        //            }).Show();

        //            break;

        //        default:
        //            break;
        //    }


        //}
        [DirectMethod]
        public void DeleteEN(string fsId, string seqNo)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                FinalEntitlementsDeductions s = new FinalEntitlementsDeductions();
                s.fsId = Convert.ToInt32(fsId);
                s.seqNo = Convert.ToInt32(seqNo);

                PostRequest<FinalEntitlementsDeductions> req = new PostRequest<FinalEntitlementsDeductions>();
                req.entity = s;
                PostResponse<FinalEntitlementsDeductions> r = _payrollService.ChildDelete<FinalEntitlementsDeductions>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                  Common.errorMessage(r);
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    // ENSeq.Text = (Convert.ToInt32(ENSeq.Text) - 1).ToString(); 
                    //entitlementsStore.Reload();

                    //Step 3 : Showing a notification for the user 
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordDeletedSucc
                    });
                    FillEmployeeInfo(new object(), new DirectEventArgs(new Ext.Net.ParameterCollection()));
                }

            }

            catch (Exception ex)
            {
                //In case of error, showing a message box to the user
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorDeletingRecord).Show();

            }

        }
        [DirectMethod]
        public void DeleteDE(string fsId, string seqNo)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                FinalEntitlementsDeductions s = new FinalEntitlementsDeductions();
                s.fsId = Convert.ToInt32(fsId);
                s.seqNo = Convert.ToInt32(seqNo);

                PostRequest<FinalEntitlementsDeductions> req = new PostRequest<FinalEntitlementsDeductions>();
                req.entity = s;
                PostResponse<FinalEntitlementsDeductions> r = _payrollService.ChildDelete<FinalEntitlementsDeductions>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                  Common.errorMessage(r);
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store


                    //  deductionStore.Reload();
                    //Step 3 : Showing a notification for the user 
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordDeletedSucc
                    });
                }
                FillEmployeeInfo(new object(), new DirectEventArgs(new Ext.Net.ParameterCollection()));
            }
            catch (Exception ex)
            {
                //In case of error, showing a message box to the user
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorDeletingRecord).Show();

            }

        }

        //protected void ADDNewEN(object sender, DirectEventArgs e)
        //{
        //    isAddEn.Text = "1";
        //    //Reset all values of the relative object
        //    ENForm.Reset();
        //    this.EditENWindow.Title = Resources.Common.AddNewRecord;

        //    entsStore.Reload();
        //    entEdId.ReadOnly = false;

        //    this.EditENWindow.Show();
        //}

        //protected void ADDNewDE(object sender, DirectEventArgs e)
        //{
        //    isAddEn.Text = "1";
        //    //Reset all values of the relative object
        //    DEForm.Reset();
        //    this.EditDEWindow.Title = Resources.Common.AddNewRecord;
        //    dedsStore.Reload();
        //    dedEdId.ReadOnly = false;

        //    this.EditDEWindow.Show();
        //}
        //protected void SaveEN(object sender, DirectEventArgs e)
        //{

        //    string obj = e.ExtraParams["values"];
        //    FinalEntitlementsDeductions b = JsonConvert.DeserializeObject<FinalEntitlementsDeductions>(obj);
        //    b.fsId = Convert.ToInt32(finalSetlemntRecordId.Text);
        //    if (!string.IsNullOrEmpty(isAddEn.Text))
        //    {
        //        try
        //        {
        //            ENSeq.Text = (Convert.ToUInt32(ENSeq.Text) + 1).ToString();
        //            PostRequest<FinalEntitlementsDeductions> request = new PostRequest<FinalEntitlementsDeductions>();
        //            request.entity = b;
        //            request.entity.seqNo = Convert.ToInt32(ENSeq.Text);
        //            PostResponse<FinalEntitlementsDeductions> r = _payrollService.ChildAddOrUpdate<FinalEntitlementsDeductions>(request);
        //            if (!r.Success)
        //            {
        //                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //              Common.errorMessage(r);
        //                return;

        //            }
        //            else
        //            {


        //                //Add this record to the store 
        //                entitlementsStore.Reload();


        //                //Display successful notification
        //                Notification.Show(new NotificationConfig
        //                {
        //                    Title = Resources.Common.Notification,
        //                    Icon = Icon.Information,
        //                    Html = Resources.Common.RecordSavingSucc
        //                });
        //                this.EditENWindow.Close();


        //            }

        //        }

        //        catch (Exception ex)
        //        {
        //            //Error exception displaying a messsage box
        //            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //              Common.errorMessage(r);
        //        }


        //    }
        //    else
        //    {
        //        //Update Mode

        //        try
        //        {
        //            PostRequest<FinalEntitlementsDeductions> request = new PostRequest<FinalEntitlementsDeductions>();
        //            request.entity = b;
        //            PostResponse<FinalEntitlementsDeductions> r = _payrollService.ChildAddOrUpdate<FinalEntitlementsDeductions>(request);
        //            if (!r.Success)
        //            {
        //                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //              Common.errorMessage(r);
        //                return;

        //            }
        //            else
        //            {


        //                Notification.Show(new NotificationConfig
        //                {
        //                    Title = Resources.Common.Notification,
        //                    Icon = Icon.Information,
        //                    Html = Resources.Common.RecordUpdatedSucc
        //                });
        //                entitlementsStore.Reload();

        //                this.EditENWindow.Close();

        //                //RefreshFinalForEntitlement(oldAmountDouble, oldInclude, b.includeInTotal.Value, amount);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //            X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
        //        }


        //    }
        //    FillEmployeeInfo(sender, e);

        //}
        //protected void SaveDE(object sender, DirectEventArgs e)
        //{

        //    string obj = e.ExtraParams["values"];
        //    FinalEntitlementsDeductions b = JsonConvert.DeserializeObject<FinalEntitlementsDeductions>(obj);
        //    b.fsId = Convert.ToInt32(finalSetlemntRecordId.Text);

        //    if (!string.IsNullOrEmpty(isAddEn.Text))
        //    {
        //        try
        //        {
        //            DESeq.Text = (Convert.ToUInt32(DESeq.Text) + 1).ToString();
        //            PostRequest<FinalEntitlementsDeductions> request = new PostRequest<FinalEntitlementsDeductions>();
        //            request.entity = b;
        //            request.entity.seqNo = Convert.ToInt32(DESeq.Text);
        //            PostResponse<FinalEntitlementsDeductions> r = _payrollService.ChildAddOrUpdate<FinalEntitlementsDeductions>(request);
        //            if (!r.Success)
        //            {
        //                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //              Common.errorMessage(r);
        //                return;

        //            }
        //            else
        //            {


        //                //Add this record to the store 
        //                deductionStore.Reload();

        //                //Display successful notification
        //                Notification.Show(new NotificationConfig
        //                {
        //                    Title = Resources.Common.Notification,
        //                    Icon = Icon.Information,
        //                    Html = Resources.Common.RecordSavingSucc
        //                });

        //                this.EditDEWindow.Close();

        //            }

        //        }

        //        catch (Exception ex)
        //        {
        //            //Error exception displaying a messsage box
        //            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //              Common.errorMessage(r);
        //        }


        //    }
        //    else
        //    {
        //        //Update Mode

        //        try
        //        {
        //            PostRequest<FinalEntitlementsDeductions> request = new PostRequest<FinalEntitlementsDeductions>();
        //            request.entity = b;

        //            PostResponse<FinalEntitlementsDeductions> r = _payrollService.ChildAddOrUpdate<FinalEntitlementsDeductions>(request);
        //            if (!r.Success)
        //            {
        //                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //              Common.errorMessage(r);
        //                return;

        //            }
        //            else
        //            {

        //                Notification.Show(new NotificationConfig
        //                {
        //                    Title = Resources.Common.Notification,
        //                    Icon = Icon.Information,
        //                    Html = Resources.Common.RecordUpdatedSucc
        //                });

        //                deductionStore.Reload();

        //                this.EditDEWindow.Close();
        //                //RefreshFinalForEntitlement(oldAmountDouble, oldInclude, b.includeInTotal.Value, amount);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //            X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
        //        }
        //    }
        //    FillEmployeeInfo(sender, e);
        //}

        //protected void ensStore_ReadData(object sender, StoreReadDataEventArgs e)
        //{

        //    ListRequest req = new ListRequest();
        //    ListResponse<EntitlementDeduction> eds = _employeeService.ChildGetAll<EntitlementDeduction>(req);

        //    entsStore.DataSource = eds.Items.Where(s => s.type == 1).ToList();
        //    entsStore.DataBind();

        //}
        //protected void dedsStore_ReadData(object sender, StoreReadDataEventArgs e)
        //{

        //    ListRequest req = new ListRequest();
        //    ListResponse<EntitlementDeduction> eds = _employeeService.ChildGetAll<EntitlementDeduction>(req);

        //    dedsStore.DataSource = eds.Items.Where(s => s.type == 2).ToList();
        //    dedsStore.DataBind();

        //}


        //protected void addEnt(object sender, DirectEventArgs e)
        //{
        //    if (string.IsNullOrEmpty(entEdId.Text))
        //        return;
        //    EntitlementDeduction dept = new EntitlementDeduction();
        //    dept.name = entEdId.Text;
        //    dept.type = 1;

        //    PostRequest<EntitlementDeduction> depReq = new PostRequest<EntitlementDeduction>();
        //    depReq.entity = dept;
        //    PostResponse<EntitlementDeduction> response = _employeeService.ChildAddOrUpdate<EntitlementDeduction>(depReq);
        //    if (response.Success)
        //    {
        //        dept.recordId = response.recordId;
        //        entsStore.Reload();
        //        entEdId.Value = response.recordId;
        //    }
        //    else
        //    {
        //        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //       Common.errorMessage(response);
        //        return;
        //    }

        //}

        //protected void addDed(object sender, DirectEventArgs e)
        //{
        //    if (string.IsNullOrEmpty(dedEdId.Text))
        //        return;
        //    EntitlementDeduction dept = new EntitlementDeduction();
        //    dept.name = dedEdId.Text;
        //    dept.type = 2;

        //    PostRequest<EntitlementDeduction> depReq = new PostRequest<EntitlementDeduction>();
        //    depReq.entity = dept;
        //    PostResponse<EntitlementDeduction> response = _employeeService.ChildAddOrUpdate<EntitlementDeduction>(depReq);
        //    if (response.Success)
        //    {
        //        dept.recordId = response.recordId;
        //        dedsStore.Reload();
        //        dedEdId.Value = dept.recordId;
        //    }
        //    else
        //    {
        //        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //       Common.errorMessage(response);
        //        return;
        //    }

        //}

        //protected void entitlementsStore_ReadData(object sender, StoreReadDataEventArgs e)
        //{

        //    string filter = string.Empty;

        //    //Fetching the corresponding list

        //    //in this test will take a list of News
        //    FinalEntitlementsDeductionsListRequest req = new FinalEntitlementsDeductionsListRequest();
        //    req.type = 1;
        //    req.fsId = Convert.ToInt32(finalSetlemntRecordId.Text);
        //    req.sortBy = "seqNo";


        //    ListResponse<FinalEntitlementsDeductions> routers = _payrollService.ChildGetAll<FinalEntitlementsDeductions>(req);
        //    if (!routers.Success)
        //    {
        //        Common.errorMessage(routers);
        //        return;
        //    }


        //    this.entitlementsStore.DataSource = routers.Items;
        //    e.Total = routers.Items.Count;
        //    totalFsCount.Text = routers.Items.Count.ToString();
        //    if (routers.Items.Count != 0)
        //        ENSeq.Text = routers.Items[routers.Items.Count - 1].seqNo.ToString();
        //    else
        //        ENSeq.Text = "0";

        //    this.entitlementsStore.DataBind();
        //}



        //protected void deductionStore_ReadData(object sender, StoreReadDataEventArgs e)
        //{

        //    string filter = string.Empty;

        //    //Fetching the corresponding list

        //    //in this test will take a list of News
        //    FinalEntitlementsDeductionsListRequest req = new FinalEntitlementsDeductionsListRequest();
        //    req.type = 2;
        //    req.fsId = Convert.ToInt32(finalSetlemntRecordId.Text);
        //    req.sortBy = "seqNo";

        //    ListResponse<FinalEntitlementsDeductions> routers = _payrollService.ChildGetAll<FinalEntitlementsDeductions>(req);
        //    if (!routers.Success)
        //    {
        //        Common.errorMessage(routers);
        //        return;
        //    }
        //    if (routers.Items.Count != 0)
        //        DESeq.Text = routers.Items[routers.Items.Count - 1].seqNo.ToString();
        //    else
        //        DESeq.Text = "1000";
        //    this.deductionStore.DataSource = routers.Items;
        //    e.Total = routers.Items.Count;

        //    totalFsCount.Text = routers.Items.Count.ToString();
        //    this.deductionStore.DataBind();

        //}
        //[DirectMethod]
        //public object GetQuickView(Dictionary<string, string> parameters)
        //{
        //    RecordRequest req = new RecordRequest();
        //    req.RecordID = parameters["id"];
        //    RecordResponse<EmployeeQuickView> qv = _employeeService.ChildGetRecord<EmployeeQuickView>(req);
        //    if (!qv.Success)
        //    {
        //        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //        X.Msg.Alert(Resources.Common.Error, qv.Summary).Show();
        //        return null;
        //    }
        //    if (qv.result != null)
        //    {
        //        return new
        //        {
        //            reportsTo = qv.result.reportToName.fullName,
        //            eosBalance = qv.result.indemnity,
        //            paidLeavesYTD = qv.result.usedLeavesLeg,
        //            leavesBalance = qv.result.leaveBalance,
        //            allowedLeaveYtd = qv.result.earnedLeavesLeg,
        //            lastleave = qv.result.LastLeave(_systemService.SessionHelper.GetDateformat()),
        //            departmentName = qv.result.departmentName,
        //            branchName = qv.result.branchName,
        //            divisionName = qv.result.divisionName,
        //            positionName = qv.result.positionName,
        //            serviceDuration = qv.result.serviceDuration,
        //            esName = qv.result.esName


        //        };
        //    }
        //    else
        //        return new { };

        //}

        //private void BuildQuickViewTemplate()
        //{
        //    string html = "<table width='80%' style='font-weight:bold;'><tr><td> ";
        //    html += GetLocalResourceObject("FieldReportsTo").ToString() + " {reportsTo}</td><td>";
        //    html += GetLocalResourceObject("eosBalanceTitle").ToString() + " {eosBalance}</td><td>";

        //    html += GetLocalResourceObject("lastLeaveStartDateTitle").ToString() + " {lastLeave}</td><td>";
        //    html += GetLocalResourceObject("paidLeavesYTDTitle").ToString() + " {paidLeavesYTD}</td></tr><tr><td>";

        //    html += GetLocalResourceObject("leavesBalanceTitle").ToString() + " {leavesBalance}</td><td>";
        //    html += GetLocalResourceObject("allowedLeaveYtdTitle").ToString() + " {allowedLeaveYtd}</td><td>";


        //    html += GetLocalResourceObject("FieldDepartment").ToString() + "{departmentName}</td><td>";
        //    html += GetLocalResourceObject("FieldBranch").ToString() + " {branchName}</td><td></tr><tr><td>";
        //    html += GetLocalResourceObject("FieldDivision").ToString() + " {divisionName}</td><td>";

        //    html += GetLocalResourceObject("FieldPosition").ToString() + " {positionName}</td><td>";
        //    html += GetLocalResourceObject("serviceDuration").ToString() + " {serviceDuration}</td><td>";
        //    html += GetLocalResourceObject("Status").ToString() + " {esName}</td></tr></table>";
        //    //RowExpander1.Template.Html = html;
        //}
        protected void printBtn_Click(object sender, EventArgs e)
        {
            FinalSettlementReport p = GetReport();
            string format = "Pdf";
            string fileName = String.Format("Report.{0}", format);

            MemoryStream ms = new MemoryStream();
            p.CreateDocument(false);
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
            FinalSettlementReport p = GetReport();
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
            FinalSettlementReport p = GetReport();
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
        private FinalSettlementReport GetReport()
        {
            if (string.IsNullOrEmpty(finalSetlemntRecordId.Text))
            {
                return new FinalSettlementReport();
            }
            RecordRequest FinalSettlementReq = new RecordRequest();
            FinalSettlementReq.RecordID = finalSetlemntRecordId.Text;
            RecordResponse<FinalSettlement> FinalSettlementResponse = _payrollService.ChildGetRecord<FinalSettlement>(FinalSettlementReq);
            if (!FinalSettlementResponse.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", FinalSettlementResponse.ErrorCode) != null ? GetGlobalResourceObject("Errors", FinalSettlementResponse.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId").ToString() + FinalSettlementResponse.LogId : FinalSettlementResponse.Summary).Show();
                return null;
            }
            FinalSettlementResponse.result.dateStringFormat = FinalSettlementResponse.result.date.ToString(_systemService.SessionHelper.GetDateformat());
            FinalEntitlementsDeductionsListRequest FinalSettlementEntitlement = new FinalEntitlementsDeductionsListRequest();
            FinalSettlementEntitlement.type = 1;
            FinalSettlementEntitlement.fsId = Convert.ToInt32(finalSetlemntRecordId.Text);
            FinalSettlementEntitlement.sortBy = "seqNo";


            ListResponse<FinalEntitlementsDeductions> FinalSettlementEntitlementResponse = _payrollService.ChildGetAll<FinalEntitlementsDeductions>(FinalSettlementEntitlement);
            if (!FinalSettlementEntitlementResponse.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", FinalSettlementEntitlementResponse.ErrorCode) != null ? GetGlobalResourceObject("Errors", FinalSettlementEntitlementResponse.ErrorCode).ToString()+"<br>"+ GetGlobalResourceObject("Errors","ErrorLogId") : FinalSettlementEntitlementResponse.Summary).Show();
                return null;
            }
            FinalEntitlementsDeductionsListRequest FinalSettlementDeduction = new FinalEntitlementsDeductionsListRequest();
            FinalSettlementDeduction.type = 2;
            FinalSettlementDeduction.fsId = Convert.ToInt32(finalSetlemntRecordId.Text);
            FinalSettlementDeduction.sortBy = "seqNo";


            ListResponse<FinalEntitlementsDeductions> FinalSettlementDeductionResponse = _payrollService.ChildGetAll<FinalEntitlementsDeductions>(FinalSettlementDeduction);
            if (!FinalSettlementDeductionResponse.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", FinalSettlementDeductionResponse.ErrorCode) != null ? GetGlobalResourceObject("Errors", FinalSettlementDeductionResponse.ErrorCode).ToString()+"<br>"+GetGlobalResourceObject("Errors","ErrorLogId") + FinalSettlementDeductionResponse.LogId : FinalSettlementDeductionResponse.Summary).Show();
                return null;
            }


            List<FinalSettlement> l = new List<FinalSettlement>();
            l.Add(FinalSettlementResponse.result);
            FinalSettlementReport p = new FinalSettlementReport();

            DetailReportBand FinalSettlementDetail = p.Bands["DetailReport"] as DetailReportBand;
            FinalSettlementDetail.DataSource = l.ToList();
            DetailReportBand FinalSettlementEntitlementDetail = p.Bands["DetailReport1"] as DetailReportBand;
            FinalSettlementEntitlementDetail.DataSource = FinalSettlementEntitlementResponse.Items;
            DetailReportBand FinalSettlementDeductionDetail = p.Bands["DetailReport2"] as DetailReportBand;
            FinalSettlementDeductionDetail.DataSource = FinalSettlementDeductionResponse.Items;

            EmployeeQuickViewRecordRequest req = new EmployeeQuickViewRecordRequest();
            req.RecordID = FinalSettlementResponse.result.employeeId.ToString();
            req.asOfDate = DateTime.Now;

            RecordResponse<EmployeeQuickView> routers = _employeeService.ChildGetRecord<EmployeeQuickView>(req);
            if (!routers.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", routers.ErrorCode) != null ? GetGlobalResourceObject("Errors", routers.ErrorCode).ToString()+ "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + routers.LogId : routers.Summary).Show();
                return null;
            }
            p.Parameters["leaveBalance"].Value = routers.result.leaveBalance;
            p.Parameters["hireDate"].Value = routers.result.hireDate.Value.ToString(_systemService.SessionHelper.GetDateformat());

            p.Parameters["serviceDuration"].Value = routers.result.serviceDuration;

            p.Parameters["departmentName"].Value = routers.result.departmentName;

            p.Parameters["positionName"].Value = routers.result.positionName;
            p.Parameters["branchName"].Value = routers.result.branchName;
            p.Parameters["divisionName"].Value = routers.result.divisionName;
            p.Parameters["reportToName"].Value = routers.result.reportToName.fullName;



            p.Parameters["countryName"].Value = routers.result.countryName;
            p.Parameters["esName"].Value = routers.result.esName;
            p.Parameters["paidLeaves"].Value = routers.result.paidLeaves;



            if (routers.result.lastLeaveStartDate != null)
                p.Parameters["lastLeaveStartDate"].Value = routers.result.lastLeaveStartDate.Value.ToString(_systemService.SessionHelper.GetDateformat());
            if (routers.result.lastLeaveEndDate != null)
                p.Parameters["lastLeaveEndDate"].Value = routers.result.lastLeaveEndDate.Value.ToString(_systemService.SessionHelper.GetDateformat());





            p.Parameters["User"].Value = _systemService.SessionHelper.GetCurrentUser();
            p.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            p.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;

            return p;



        }

        protected void selectAqType(object sender, DirectEventArgs e)
        {
            string benefitId = e.ExtraParams["benefitId"];
            ScheduleBenefitsRecordRequest r = new ScheduleBenefitsRecordRequest();
            r.bsId = bsIdHidden.Text;
            r.benefitId = benefitId;

            RecordResponse<ScheduleBenefits> response = _benefitsService.ChildGetRecord<ScheduleBenefits>(r);
            if (!response.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
               Common.errorMessage(response);
                return;
            }
            aqType.Select(response.result.aqType.ToString());
            amount.Text = response.result.defaultAmount.ToString();

        }

        protected void getAqRatio(object sender, DirectEventArgs e)
        {
            string dateFrom = e.ExtraParams["dateFrom"];

            //new
            //DateTime DF = new DateTime();
            //DF = DateTime.Parse(dateFrom);
            //dateFrom = DF.ToString("yyyyMMdd");



            string employeeId = e.ExtraParams["employeeId"];
            string benefitId = e.ExtraParams["benefitId"];
            AcquisitionRateListRequest request = new AcquisitionRateListRequest();
            request.employeeId = employeeId;
            request.benefitId = benefitId;
            request.bsId = bsIdHidden.Text;
            //request.asOfDate = dateFrom;
            request.asOfDate = dateFrom;
            if (string.IsNullOrEmpty(employeeId) || string.IsNullOrEmpty(benefitId) || string.IsNullOrEmpty(bsIdHidden.Text) || string.IsNullOrEmpty(dateFrom))
                return; 
            RecordResponse< BenefitAcquisitionAcquisitionRate>  response = _helpFunctionService.ChildGetRecord<BenefitAcquisitionAcquisitionRate>(request);
            if (!response.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
               Common.errorMessage(response);
                return;
            }
            if (response.result != null)
                aqRatio.Text = response.result.aqRatio.ToString();
            else
                aqRatio.Text = "";


            returnPeriodDate(sender, e);

        }

        protected void returnPeriodDate(object sender, DirectEventArgs e)
        {
            string dateFrom = e.ExtraParams["dateFrom"];
            string dateTo = e.ExtraParams["dateTo"];
            if (string.IsNullOrEmpty(dateFrom) || string.IsNullOrEmpty(dateTo))
                return; 
            

            
          



         
            PeriodOfTheDateRangeRequest request = new PeriodOfTheDateRangeRequest();
            request.startDate = dateFrom;
            request.endDate = dateTo;
                      
            RecordResponse<PeriodOfTheDate> response = _helpFunctionService.ChildGetRecord<PeriodOfTheDate>(request);
            if (!response.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors","ErrorLogId")  + response.LogId : response.Summary).Show();
                return;
            }
            if (response.result != null)
            {
            
                period.Text = returnPeriodDateFriendly( response.result.periodText.ToString());
            }


        }

        private string returnPeriodDateFriendly(string days)
        {
            
         
             days = days.Replace("y", GetGlobalResourceObject("Common","year").ToString());
            days = days.Replace("m", GetGlobalResourceObject("Common", "Month").ToString());
            days = days.Replace("d", GetGlobalResourceObject("Common", "day").ToString());
            return days;

        }

        protected void benefitsFilterStore_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<Benefit> routers = _benefitsService.ChildGetAll<Benefit>(request);
            if (!routers.Success)
                return;
            this.benefitsFilterStore.DataSource = routers.Items;
            e.Total = routers.Items.Count; 

            this.benefitsFilterStore.DataBind();
        }
    }
}