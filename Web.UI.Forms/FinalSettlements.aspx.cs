﻿using System;
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
using Model.TimeAttendance;
using Reports;
using Model.Payroll;
using DevExpress.XtraReports.UI;
using Services.Messaging.LoanManagment;
using Model.LoadTracking;
using Services.Messaging.Employees;

namespace Web.UI.Forms
{
    public partial class FinalSettlements : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IPayrollService _payrollService = ServiceLocator.Current.GetInstance<IPayrollService>();

        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ILoanTrackingService _loanService = ServiceLocator.Current.GetInstance<ILoanTrackingService>();


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
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(FinalEntitlementsDeductions), ENForm, entitlementsGrid, Button11, SaveENButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
                }

                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(FinalEntitlementsDeductions), DEForm, deductionGrid, Button14, SaveDEButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
                }


                dateFormat.Text = _systemService.SessionHelper.GetDateformat().ToUpper(); ;
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
                FinalSettlement s = new FinalSettlement();
                s.recordId = id;
                
                //s.intName = "";

                PostRequest<FinalSettlement> req = new PostRequest<FinalSettlement>();
                req.entity = s;
                PostResponse<FinalSettlement> r = _payrollService.ChildDelete<FinalSettlement>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                     Common.errorMessage(r);
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

            panelRecordDetails.ActiveIndex = 0;
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;
            recordId.Text = "";
            deductionGrid.Disabled = true;
            entitlementsGrid.Disabled = true;
            finalSetlemntRecordId.Text = "";
            dateId.Value = DateTime.Now;
            this.setFillEmployeeInfoDisable(true);
            this.EditRecordWindow.Show();
        }

        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            List<EmployeeSnapShot> emp=   Common.GetEmployeesFiltered(prms.Query);



            return emp.Where(x => x.activeStatus == (short)ActiveStatus.INACTIVE).ToList();

        }


        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
          



            //Fetching the corresponding list

            //in this test will take a list of News
            ListRequest req = new ListRequest(); 


            ListResponse<FinalSettlement> routers = _payrollService.ChildGetAll<FinalSettlement>(req);
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


        private LoanManagementListRequest GetLoanManagementRequest( string employeeId)
        {
            LoanManagementListRequest req = new LoanManagementListRequest();

            req.BranchId = "0";
            req.DepartmentId = "0";
            req.DivisionId = "0";
            req.EmployeeId = Convert.ToInt32(employeeId);
            req.Status = "2";
            

          

            req.Size = "2000";
            req.StartAt = "0";
            req.Filter = "";
            req.SortBy = "employeeId";

            return req;
        }

        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {
            string employeeId = e.ExtraParams["employeeId"];
            //LoanManagementListRequest request1 = GetLoanManagementRequest(employeeId);
            
            //ListResponse<Loan> routers = _loanService.GetAll<Loan>(request1);
            //if (!routers.Success)
            //{
            //    Common.errorMessage(routers);
            //    return;
            //}
            //if (routers.count!=0)
            //{
            //    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", "FinalSettlementForEmployeeHaveLoans") ).Show();
            //    return;
            //}

            //Getting the id to check if it is an Add or an edit as they are managed within the same form.


            string obj = e.ExtraParams["values"];
            //string fsRefid = e.ExtraParams["fsRefid"];
            //string dateId = e.ExtraParams["dateId"];
            //string employeeId = e.ExtraParams["employeeId"];

            FinalSettlement b = JsonConvert.DeserializeObject<FinalSettlement>(obj);

            string id = e.ExtraParams["id"];
          
           
            // Define the object to add or edit as null
           

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<FinalSettlement> request = new PostRequest<FinalSettlement>();
                    
                    
                    request.entity = b;
                    request.entity.recordId = finalSetlemntRecordId.Text;
                    PostResponse<FinalSettlement> r = _payrollService.ChildAddOrUpdate<FinalSettlement>(request);


                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                         Common.errorMessage(r);
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


                        entitlementsGrid.Disabled = false;
                        deductionGrid.Disabled = false;

                        if (!string.IsNullOrEmpty(finalSetlemntRecordId.Text))
                        {
                          
                            this.EditRecordWindow.Close();
                            this.setFillEmployeeInfoDisable(true);
                        }
                        finalSetlemntRecordId.Text = r.recordId;
                        entitlementsStore.Reload();
                        deductionStore.Reload(); 
                        //RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;
                        //sm.DeselectAll();
                        //sm.Select(b.recordId.ToString());




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
                    PostRequest<FinalSettlement> request = new PostRequest<FinalSettlement>();
                   request.entity = b;
                    PostResponse<FinalSettlement> r = _payrollService.ChildAddOrUpdate<FinalSettlement>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                    //Step 2 : saving to store

                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                         Common.errorMessage(r);
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
            deductionGrid.Disabled = false;
            entitlementsGrid.Disabled = false;
            this.setFillEmployeeInfoDisable(true);
            string type = e.ExtraParams["type"];
            string id =e.ExtraParams["recordId"];
            finalSetlemntRecordId.Text = id;

            dateId.Value = DateTime.Now;
            string totalCount= e.ExtraParams["totalCount"];

            switch (type)
            {
                case "imgEdit":
                    entitlementsStore.Reload();
                    deductionStore.Reload();
                    setFillEmployeeInfoDisable(false);
                    
                    //Step 1 : get the object from the Web Service 
                    RecordRequest req = new RecordRequest();
                    req.RecordID = id;
                    RecordResponse<FinalSettlement> r = _payrollService.ChildGetRecord<FinalSettlement>(req);
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                         Common.errorMessage(r);
                        return;
                    }
                    else
                    {
                        //Step 2 : call setvalues with the retrieved object
                        employeeId.GetStore().Add(new object[]
                      {
                                new
                                {
                                    recordId = r.result.employeeId,
                                    fullName =r.result.employeeName
                                }
                      });
                        employeeId.SetValue(r.result.employeeId);
                        FillEmployeeInfo(sender,e);
                        this.BasicInfoTab.SetValues(r.result);
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
            try
            {
                EmployeeTerminationRecordRequest caRequest = new EmployeeTerminationRecordRequest();
                caRequest.employeeId = employeeId.Value.ToString();
                RecordResponse<EmployeeTermination> response = _employeeService.ChildGetRecord<EmployeeTermination>(caRequest);

                if (!response.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(response);
                    return;
                }




                EmployeeQuickViewRecordRequest req = new EmployeeQuickViewRecordRequest();
                req.RecordID = employeeId.Value.ToString();
                if (response.result != null)
                {
                    req.asOfDate = response.result.date ?? DateTime.Now;

                }
                else
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
                this.setFillEmployeeInfoDisable(false);
                if (routers.result == null)
                    return;

                branchNameTx.Text = routers.result.branchName;
                departmentNameTx.Text = routers.result.departmentName;
                positionNameTx.Text = routers.result.positionName;
                hireDateDf.Value = routers.result.hireDate;
                nationalityTx.Text = routers.result.countryName;
                divisionName.Text = routers.result.divisionName;
                reportToName.Text = routers.result.reportToName != null ? routers.result.reportToName : "";
                eosBalance.Text = routers.result.indemnity.ToString();
                lastLeaveStartDate.Value = routers.result.lastLeaveStartDate;
                lastLeaveEndDate.Value = routers.result.lastLeaveEndDate;
                leavesBalance.Text = routers.result.leaveBalance.ToString();
                allowedLeaveYtd.Text = routers.result.earnedLeavesLeg.ToString();
                serviceDuration.Text = routers.result.serviceDuration;
                esName.Text = routers.result.esName;
                paidLeavesYTD.Text = routers.result.usedLeavesLeg.ToString();
                loanBalance.Text = routers.result.loanBalance.ToString();
            }
            catch(Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();

            }


        }
        private void setFillEmployeeInfoDisable(bool YES)
        {
            if (!YES)
            {
                branchNameTx.Disabled = false;
                departmentNameTx.Disabled = false;
                positionNameTx.Disabled = false;
                hireDateDf.Disabled = false;
                nationalityTx.Disabled = false;
                divisionName.Disabled = false;
                reportToName.Disabled = false;
                eosBalance.Disabled = false;
                lastLeaveStartDate.Disabled = false;
                lastLeaveEndDate.Disabled = false;
                leavesBalance.Disabled = false;
                allowedLeaveYtd.Disabled = false;
                serviceDuration.Disabled = false;
                esName.Disabled = false;
                paidLeavesYTD.Disabled = false;
                loanBalance.Disabled = false;



            }
            else
            {
                branchNameTx.Disabled = true;
                departmentNameTx.Disabled = true;
                positionNameTx.Disabled = true;
                hireDateDf.Disabled = true;
                nationalityTx.Disabled = true;
                divisionName.Disabled = true;
                reportToName.Disabled = true;
                eosBalance.Disabled = true;
                lastLeaveStartDate.Disabled = true;
                lastLeaveEndDate.Disabled = true;
                leavesBalance.Disabled = true;
                allowedLeaveYtd.Disabled = true;
                serviceDuration.Disabled = true;
                esName.Disabled = true;
                paidLeavesYTD.Disabled = true;
                loanBalance.Disabled = true;

            }

        }
        protected void PoPuPEN(object sender, DirectEventArgs e)
        {
            
            isAddEn.Text = "";
            int fsId = Convert.ToInt32(e.ExtraParams["fsId"]);
            int seqNo = Convert.ToInt32(e.ExtraParams["seqNo"]);
            string entitlement = "";
            
            string type = e.ExtraParams["type"];
           
            switch (type)
            {


                case "imgEdit":

                    string record = e.ExtraParams["values"];
                    FinalEntitlementsDeductionsRecordRequest req = new FinalEntitlementsDeductionsRecordRequest(); 
                   
                    req.fsId = fsId;
                    req.seqNo = seqNo;
                    RecordResponse<FinalEntitlementsDeductions> res = _payrollService.ChildGetRecord<FinalEntitlementsDeductions>(req);
                    //entsStore.Reload();
                    if (!res.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", res.ErrorCode) != null ? GetGlobalResourceObject("Errors", res.ErrorCode).ToString() : res.Summary).Show();
                        return;
                    }
                   
                    this.ENForm.SetValues(res.result);

                    //  recordId.Text = ssId;
                    this.ENForm.Title = Resources.Common.EditWindowsTitle;
                    entEdId.GetStore().Add(new object[]
                      {
                                new
                                {
                                    recordId = res.result.edId,
                                    name =res.result.edName
                                }
                      });
                    entEdId.SetValue(res.result.edId);
                    
                    EditENWindow.Show();
                    break;
                case "imgDelete":
                    entitlement = e.ExtraParams["values"];
                   // FinalEntitlementsDeductions = JsonConvert.DeserializeObject<FinalEntitlementsDeductions>(entitlement)[0];
                 
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteEN({0},{1})", fsId, seqNo),
                            Text = Resources.Common.Yes
                        },
                        No = new MessageBoxButtonConfig
                        {
                            Text = Resources.Common.No
                        }

                    }).Show();
                  
                    break;

                default:
                    break;
            }


        }

        protected void PoPuPDE(object sender, DirectEventArgs e)
        {


            isAddEn.Text = "";
            int fsId = Convert.ToInt32(e.ExtraParams["fsId"]);
            int seqNo = Convert.ToInt32(e.ExtraParams["seqNo"]);
            string entitlement = "";

            string type = e.ExtraParams["type"];

            switch (type)
            {


                case "imgEdit":

                    string record = e.ExtraParams["values"];
                    FinalEntitlementsDeductionsRecordRequest req = new FinalEntitlementsDeductionsRecordRequest();

                    req.fsId = fsId;
                    req.seqNo = seqNo;
                    RecordResponse<FinalEntitlementsDeductions> res = _payrollService.ChildGetRecord<FinalEntitlementsDeductions>(req);
                    //entsStore.Reload();
                    if (!res.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", res.ErrorCode) != null ? GetGlobalResourceObject("Errors", res.ErrorCode).ToString() : res.Summary).Show();
                        return;
                    }
                    this.DEForm.SetValues(res.result);
                   
                    //  recordId.Text = ssId;
                    this.DEForm.Title = Resources.Common.EditWindowsTitle;
                    dedEdId.GetStore().Add(new object[]
                      {
                                new
                                {
                                    recordId = res.result.edId,
                                    name =res.result.edName
                                }
                      });
                    dedEdId.SetValue(res.result.edId);
                    EditDEWindow.Show();
                    break;
                case "imgDelete":
                    entitlement = e.ExtraParams["values"];
                    // FinalEntitlementsDeductions = JsonConvert.DeserializeObject<FinalEntitlementsDeductions>(entitlement)[0];

                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                             Handler = String.Format("App.direct.DeleteDE({0},{1})", fsId, seqNo),
                            Text = Resources.Common.Yes
                        },
                        No = new MessageBoxButtonConfig
                        {
                            Text = Resources.Common.No
                        }

                    }).Show();
                 
                    break;

                default:
                    break;
            }


        }
        [DirectMethod]
        public void DeleteEN(string fsId, string seqNo)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                FinalEntitlementsDeductions s = new FinalEntitlementsDeductions();
                s.fsId = Convert.ToInt32(fsId);
                s.seqNo= Convert.ToInt32(seqNo);

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
                    entitlementsStore.Reload();
                  
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
                   
                
                    deductionStore.Reload();
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
       
        protected void ADDNewEN(object sender, DirectEventArgs e)
        {
            isAddEn.Text = "1"; 
            //Reset all values of the relative object
            ENForm.Reset();
            this.EditENWindow.Title = Resources.Common.AddNewRecord;
            
            entsStore.Reload();
            entEdId.ReadOnly = false;

            this.EditENWindow.Show();
        }

        protected void ADDNewDE(object sender, DirectEventArgs e)
        {
            isAddEn.Text = "1";
            //Reset all values of the relative object
            DEForm.Reset();
            this.EditDEWindow.Title = Resources.Common.AddNewRecord;
            dedsStore.Reload();
            dedEdId.ReadOnly = false; 

            this.EditDEWindow.Show();
        }
        protected void SaveEN(object sender, DirectEventArgs e)
        {
          
            string obj = e.ExtraParams["values"];                   
            FinalEntitlementsDeductions b = JsonConvert.DeserializeObject<FinalEntitlementsDeductions>(obj);
            b.fsId =Convert.ToInt32( finalSetlemntRecordId.Text);                  
            if ( !string.IsNullOrEmpty(isAddEn.Text))
            {
                try
                {
                    ENSeq.Text = (Convert.ToUInt32(ENSeq.Text) + 1).ToString();
                    PostRequest<FinalEntitlementsDeductions> request = new PostRequest<FinalEntitlementsDeductions>();
                    request.entity = b;
                    request.entity.seqNo = Convert.ToInt32(ENSeq.Text);
                    PostResponse<FinalEntitlementsDeductions> r = _payrollService.ChildAddOrUpdate<FinalEntitlementsDeductions>(request);
                    if (!r.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                         Common.errorMessage(r);
                        return;

                    }
                    else
                    {
                      

                        //Add this record to the store 
                        entitlementsStore.Reload();
                        

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });
                        this.EditENWindow.Close();
                       
                        
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
                    PostRequest<FinalEntitlementsDeductions> request = new PostRequest<FinalEntitlementsDeductions>();
                    request.entity = b;
                    PostResponse<FinalEntitlementsDeductions> r = _payrollService.ChildAddOrUpdate<FinalEntitlementsDeductions>(request);
                    if (!r.Success)
                    {
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
                            Html = Resources.Common.RecordUpdatedSucc
                        });
                        entitlementsStore.Reload();
                      
                        this.EditENWindow.Close();
                      
                        //RefreshFinalForEntitlement(oldAmountDouble, oldInclude, b.includeInTotal.Value, amount);
                    }
                }
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }

              
            }
            FillEmployeeInfo(sender, e);

        }
        protected void SaveDE(object sender, DirectEventArgs e)
        {

            string obj = e.ExtraParams["values"];
            FinalEntitlementsDeductions b = JsonConvert.DeserializeObject<FinalEntitlementsDeductions>(obj);
            b.fsId = Convert.ToInt32(finalSetlemntRecordId.Text);
                
            if (!string.IsNullOrEmpty(isAddEn.Text))
            {
                try
                {
                    DESeq.Text = (Convert.ToUInt32(DESeq.Text) + 1).ToString();
                    PostRequest<FinalEntitlementsDeductions> request = new PostRequest<FinalEntitlementsDeductions>();
                    request.entity = b;
                    request.entity.seqNo = Convert.ToInt32(DESeq.Text);
                    PostResponse<FinalEntitlementsDeductions> r = _payrollService.ChildAddOrUpdate<FinalEntitlementsDeductions>(request);
                    if (!r.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                         Common.errorMessage(r);
                        return;

                    }
                    else
                    {
                     

                        //Add this record to the store 
                        deductionStore.Reload();

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });
                   
                        this.EditDEWindow.Close();

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
                    PostRequest<FinalEntitlementsDeductions> request = new PostRequest<FinalEntitlementsDeductions>();
                    request.entity = b;

                    PostResponse<FinalEntitlementsDeductions> r = _payrollService.ChildAddOrUpdate<FinalEntitlementsDeductions>(request);
                    if (!r.Success)
                    {
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
                            Html = Resources.Common.RecordUpdatedSucc
                        });
                       
                        deductionStore.Reload();
                  
                        this.EditDEWindow.Close();
                        //RefreshFinalForEntitlement(oldAmountDouble, oldInclude, b.includeInTotal.Value, amount);
                    }
                }
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }
            }
            FillEmployeeInfo(sender, e);
        }
       
        protected void ensStore_ReadData(object sender, StoreReadDataEventArgs e)
        {

            ListRequest req = new ListRequest();
            ListResponse<EntitlementDeduction> eds = _employeeService.ChildGetAll<EntitlementDeduction>(req);
          
            entsStore.DataSource = eds.Items.Where(s => s.type == 1).ToList();
            entsStore.DataBind();

        }
        protected void dedsStore_ReadData(object sender, StoreReadDataEventArgs e)
        {

            ListRequest req = new ListRequest();
            ListResponse<EntitlementDeduction> eds = _employeeService.ChildGetAll<EntitlementDeduction>(req);
           
            dedsStore.DataSource = eds.Items.Where(s => s.type == 2).ToList();
            dedsStore.DataBind();

        }


        protected void addEnt(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(entEdId.Text))
                return;
            EntitlementDeduction dept = new EntitlementDeduction();
            dept.name = entEdId.Text;
            dept.type = 1;

            PostRequest<EntitlementDeduction> depReq = new PostRequest<EntitlementDeduction>();
            depReq.entity = dept;
            PostResponse<EntitlementDeduction> response = _employeeService.ChildAddOrUpdate<EntitlementDeduction>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                entsStore.Reload();
                entEdId.Value = response.recordId;
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }

        protected void addDed(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(dedEdId.Text))
                return;
            EntitlementDeduction dept = new EntitlementDeduction();
            dept.name = dedEdId.Text;
            dept.type = 2;

            PostRequest<EntitlementDeduction> depReq = new PostRequest<EntitlementDeduction>();
            depReq.entity = dept;
            PostResponse<EntitlementDeduction> response = _employeeService.ChildAddOrUpdate<EntitlementDeduction>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                dedsStore.Reload();
                dedEdId.Value = dept.recordId;
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }

        protected void entitlementsStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            
            string filter = string.Empty;

            //Fetching the corresponding list

            //in this test will take a list of News
            FinalEntitlementsDeductionsListRequest req = new FinalEntitlementsDeductionsListRequest();
            req.type = 1;
            req.fsId = Convert.ToInt32( finalSetlemntRecordId.Text);
            req.sortBy = "seqNo";


            ListResponse<FinalEntitlementsDeductions> routers = _payrollService.ChildGetAll<FinalEntitlementsDeductions>(req);
            if (!routers.Success)
            {
                Common.errorMessage(routers);
                return;
            }
          
         
            this.entitlementsStore.DataSource = routers.Items;
            e.Total = routers.Items.Count;
            totalFsCount.Text = routers.Items.Count.ToString();
            if (routers.Items.Count != 0)
                ENSeq.Text = routers.Items[routers.Items.Count - 1].seqNo.ToString();
            else
                ENSeq.Text = "0";

            this.entitlementsStore.DataBind();
        }

       

        protected void deductionStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
           
            string filter = string.Empty;

            //Fetching the corresponding list

            //in this test will take a list of News
            FinalEntitlementsDeductionsListRequest req = new FinalEntitlementsDeductionsListRequest();
            req.type = 2;
            req.fsId = Convert.ToInt32(finalSetlemntRecordId.Text);
            req.sortBy = "seqNo";

            ListResponse<FinalEntitlementsDeductions> routers = _payrollService.ChildGetAll<FinalEntitlementsDeductions>(req);
            if (!routers.Success)
            {
                Common.errorMessage(routers);
                return;
            }
            if (routers.Items.Count != 0)
                DESeq.Text = routers.Items[routers.Items.Count - 1].seqNo.ToString();
            else
                DESeq.Text = "1000";
            this.deductionStore.DataSource = routers.Items;
            e.Total = routers.Items.Count;
          
                totalFsCount.Text = routers.Items.Count.ToString();
            this.deductionStore.DataBind();

        }
        [DirectMethod]
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
            try
            {

                double entitlementSubTotal = 0, deductionSubTotal = 0;

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
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", FinalSettlementEntitlementResponse.ErrorCode) != null ? GetGlobalResourceObject("Errors", FinalSettlementEntitlementResponse.ErrorCode).ToString() : FinalSettlementEntitlementResponse.Summary).Show();
                    return null;
                }
                FinalSettlementEntitlementResponse.Items.ForEach(x => entitlementSubTotal += x.amount);
            //    FinalSettlementEntitlementResponse.Items.Add(new FinalEntitlementsDeductions { edName = GetGlobalResourceObject("Common", "Total").ToString(), amount = entitlementSubTotal });
                FinalEntitlementsDeductionsListRequest FinalSettlementDeduction = new FinalEntitlementsDeductionsListRequest();
                FinalSettlementDeduction.type = 2;
                FinalSettlementDeduction.fsId = Convert.ToInt32(finalSetlemntRecordId.Text);
                FinalSettlementDeduction.sortBy = "seqNo";


                ListResponse<FinalEntitlementsDeductions> FinalSettlementDeductionResponse = _payrollService.ChildGetAll<FinalEntitlementsDeductions>(FinalSettlementDeduction);
                if (!FinalSettlementDeductionResponse.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", FinalSettlementDeductionResponse.ErrorCode) != null ? GetGlobalResourceObject("Errors", FinalSettlementDeductionResponse.ErrorCode).ToString() : FinalSettlementDeductionResponse.Summary).Show();
                    return null;
                }
                FinalSettlementDeductionResponse.Items.ForEach(x => deductionSubTotal += x.amount);
        //        FinalSettlementEntitlementResponse.Items.Add(new FinalEntitlementsDeductions { edName = GetGlobalResourceObject("Common", "Total").ToString(), amount = deductionSubTotal });


                List<FinalSettlement> l = new List<FinalSettlement>();
                l.Add(FinalSettlementResponse.result);
                FinalSettlementReport p = new FinalSettlementReport();

                DetailReportBand FinalSettlementDetail = p.Bands["DetailReport"] as DetailReportBand;
                FinalSettlementDetail.DataSource = l.ToList();
                DetailReportBand FinalSettlementEntitlementDetail = p.Bands["DetailReport1"] as DetailReportBand;
                FinalSettlementEntitlementDetail.DataSource = FinalSettlementEntitlementResponse.Items;
                DetailReportBand FinalSettlementDeductionDetail = p.Bands["DetailReport2"] as DetailReportBand;
                FinalSettlementDeductionDetail.DataSource = FinalSettlementDeductionResponse.Items;







                EmployeeTerminationRecordRequest caRequest = new EmployeeTerminationRecordRequest();
                caRequest.employeeId = FinalSettlementResponse.result.employeeId;
                RecordResponse<EmployeeTermination> response = _employeeService.ChildGetRecord<EmployeeTermination>(caRequest);

                if (!response.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(response);
                    throw new Exception();
                }




                EmployeeQuickViewRecordRequest req = new EmployeeQuickViewRecordRequest();
                req.RecordID = FinalSettlementResponse.result.employeeId;
                if (response.result != null)
                {
                    req.asOfDate = response.result.date ?? DateTime.Now;

                }
                else
                    req.asOfDate = DateTime.Now;

                RecordResponse<EmployeeQuickView> routers = _employeeService.ChildGetRecord<EmployeeQuickView>(req);
                if (!routers.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                     Common.errorMessage(routers);
                    return null;
                }
                p.Parameters["leaveBalance"].Value = routers.result.leaveBalance;
                p.Parameters["hireDate"].Value = routers.result.hireDate.Value.ToString(_systemService.SessionHelper.GetDateformat());

                p.Parameters["serviceDuration"].Value = routers.result.serviceDuration;

                p.Parameters["departmentName"].Value = routers.result.departmentName;

                p.Parameters["positionName"].Value = routers.result.positionName;
                p.Parameters["branchName"].Value = routers.result.branchName;
                p.Parameters["divisionName"].Value = routers.result.divisionName;
                p.Parameters["reportToName"].Value = routers.result.reportToName != null ? routers.result.reportToName : "";



                p.Parameters["countryName"].Value = routers.result.countryName;
                p.Parameters["esName"].Value = routers.result.esName;
                p.Parameters["paidLeaves"].Value = routers.result.paidLeaves;
                p.Parameters["totalAmount"].Value = entitlementSubTotal - deductionSubTotal;
                p.Parameters["TotalEntitlement"].Value = entitlementSubTotal;
                p.Parameters["TotalDeduction"].Value = deductionSubTotal;




                if (routers.result.lastLeaveStartDate != null)
                    p.Parameters["lastLeaveStartDate"].Value = routers.result.lastLeaveStartDate.Value.ToString(_systemService.SessionHelper.GetDateformat());
                if (routers.result.lastLeaveEndDate != null)
                    p.Parameters["lastLeaveEndDate"].Value = routers.result.lastLeaveEndDate.Value.ToString(_systemService.SessionHelper.GetDateformat());





                p.Parameters["User"].Value = _systemService.SessionHelper.GetCurrentUser();
                p.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
                p.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;

                return p;


            }
            catch(Exception exp)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
                return new FinalSettlementReport();
            }
        }

    }
}