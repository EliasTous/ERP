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
using AionHR.Model.Payroll;

namespace AionHR.Web.UI.Forms
{
    public partial class PayrollTimeSchedule : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IPayrollService _PayrollService = ServiceLocator.Current.GetInstance<IPayrollService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
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
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(TimeSchedule), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
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
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(TimeCode), null, TimeCodeGrid, null, null);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    TimeCodeGrid.Disabled = true;
                    return;
                }


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

         
            string id = e.ExtraParams["id"];
            ScheduleID.Text = id;
            string type = e.ExtraParams["type"];
            TimeCodeGridStore.Reload();
            apIdStore.Reload();
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id;

                    RecordResponse<TimeSchedule> response = _PayrollService.ChildGetRecord<TimeSchedule>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() + "<br>Technical Error: " + response.ErrorCode + "<br> Summary: " + response.LogId : response.Summary).Show();
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.BasicInfoTab.SetValues(response.result);

                    recordId.Text = id;
                    this.EditRecordWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditRecordWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteRecord({0})", id),
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
                TimeSchedule s = new TimeSchedule();
                s.recordId = index;
                s.name = "";
                //s.intName = "";

                PostRequest<TimeSchedule> req = new PostRequest<TimeSchedule>();
                req.entity = s;
                PostResponse<TimeSchedule> r = _PayrollService.ChildDelete<TimeSchedule>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+ GetGlobalResourceObject("Errors", "ErrorLogId")+r.LogId : r.Summary).Show();
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
            ScheduleID.Text = "";
            TimeCodeGridStore.Reload();
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;


            this.EditRecordWindow.Show();
        }

        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;




            //Fetching the corresponding list

            //in this test will take a list of News
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<TimeSchedule> routers = _PayrollService.ChildGetAll<TimeSchedule>(request);
            if (!routers.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", routers.ErrorCode) != null ? GetGlobalResourceObject("Errors", routers.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + routers.LogId: routers.Summary).Show();
                return;
            }
            this.Store1.DataSource = routers.Items;
            e.Total = routers.Items.Count; ;

            this.Store1.DataBind();
        }



       
        
        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.


            string obj = e.ExtraParams["values"];
            TimeSchedule b = JsonConvert.DeserializeObject<TimeSchedule>(obj);
            List<TimeCode> codes = JsonConvert.DeserializeObject<List<TimeCode>>(e.ExtraParams["codes"]);
            string id = e.ExtraParams["id"];
            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<TimeSchedule> request = new PostRequest<TimeSchedule>();

                    request.entity = b;
                    PostResponse<TimeSchedule> r = _PayrollService.ChildAddOrUpdate<TimeSchedule>(request);

                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+ GetGlobalResourceObject("Errors", "ErrorLogId")+r.LogId : r.Summary).Show();
                        return;
                    }
                    else
                    {
                        codes.ForEach(x => x.tsId = Convert.ToInt32(r.recordId));
                        PostRequest<TimeCode[]> codesReq = new PostRequest<TimeCode[]>();
                        codesReq.entity = codes.ToArray();
                        
                        PostResponse<TimeCode[]> codesResp = _PayrollService.ChildAddOrUpdate<TimeCode[]>(codesReq);
                        if (!codesResp.Success)//it maybe be another condition
                        {
                            //Show an error saving...
                            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", codesResp.ErrorCode) != null ? GetGlobalResourceObject("Errors", codesResp.ErrorCode).ToString() : codesResp.Summary).Show();
                            return;
                        }
                        else
                        {
                            b.recordId = r.recordId;
                            //Add this record to the store 
                            this.Store1.Insert(0, b);

                            //Display successful notification
                            Notification.Show(new NotificationConfig
                            {
                                Title = Resources.Common.Notification,
                                Icon = Icon.Information,
                                Html = Resources.Common.RecordSavingSucc
                            });

                            this.EditRecordWindow.Close();
                            RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;
                            sm.DeselectAll();
                            sm.Select(b.recordId.ToString());

                        }

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
                    PostRequest<TimeSchedule> request = new PostRequest<TimeSchedule>();
                    request.entity = b;
                    PostResponse<TimeSchedule> r = _PayrollService.ChildAddOrUpdate<TimeSchedule>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                    //Step 2 : saving to store

                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+ GetGlobalResourceObject("Errors", "ErrorLogId")+r.LogId : r.Summary).Show();
                        return;
                    }
                    else
                    {

                        PostRequest<TimeCode[]> codesReq = new PostRequest<TimeCode[]>();
                        codes.ForEach(x => x.tsId = Convert.ToInt32(id));
                        codesReq.entity = codes.ToArray();
                        PostResponse<TimeCode[]> codesResp = _PayrollService.ChildAddOrUpdate<TimeCode[]>(codesReq);
                        if (!codesResp.Success)//it maybe be another condition
                        {
                            //Show an error saving...
                            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", codesResp.ErrorCode) != null ? GetGlobalResourceObject("Errors", codesResp.ErrorCode).ToString() : codesResp.Summary).Show();
                            return;
                        }
                        else
                        {
                            ModelProxy record = this.Store1.GetById(id);
                            BasicInfoTab.UpdateRecord(record);
                            record.Commit();
                            Notification.Show(new NotificationConfig
                            {
                                Title = Resources.Common.Notification,
                                Icon = Icon.Information,
                                Html = Resources.Common.RecordUpdatedSucc
                            });
                            this.EditRecordWindow.Close();


                        }
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
      


     

        protected void TimeCodeGridStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            List<TimeCode> TSL = new List<TimeCode>();
           // TSL.Add(new TimeCode { timeCode = "A" });
            TSL.Add(new TimeCode { timeCode = "D" });
            TSL.Add(new TimeCode { timeCode = "L" });
            TSL.Add(new TimeCode { timeCode = "O" });
            TSL.Add(new TimeCode { timeCode = "M" });
            PayrollTimeCodeRequest request = new PayrollTimeCodeRequest();
            if (!string.IsNullOrEmpty(ScheduleID.Text))
            {
                request.ScheduleId = Convert.ToInt32(ScheduleID.Text);


                request.Filter = "";
                ListResponse<TimeCode> r = _PayrollService.ChildGetAll<TimeCode>(request);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+ GetGlobalResourceObject("Errors", "ErrorLogId")+r.LogId : r.Summary).Show();
                    return;
                }

                //foreach (var local in TSL)
                //{
                //    foreach (var remote in r.Items)
                //    {
                //        if (remote.timeCode == local.timeCode)
                //        {
                //            local.tsId = remote.tsId;
                //            local.multiplier = remote.multiplier;
                //            local.isEnabled = remote.isEnabled;
                //            local.fullPeriod = remote.fullPeriod;
                //            local.deductPeriod = remote.deductPeriod;
                //            local.apId = remote.apId;
                //            local.apName = remote.apName;
                //            local.maxAllowed = remote.maxAllowed;
                //        }

                //    }

                //}
                TimeCode A;
                TSL.ForEach(x =>
                {
                    if (r.Items.Where(y => y.timeCode == x.timeCode).ToList().Count() != 0)
                    {
                        A = r.Items.Where(y => y.timeCode == x.timeCode).ToList().First();
                        x.apId = A.apId;
                        x.apName = A.apName;
                        x.deductPeriod = A.deductPeriod;
                        x.fullPeriod = A.fullPeriod;
                        x.isEnabled = A.isEnabled;
                        x.maxAllowed = A.maxAllowed;
                        x.multiplier = A.multiplier;
                        x.timeCode = A.timeCode;
                        x.tsId = A.tsId;
                    }
                });


            }

            this.TimeCodeGridStore.DataSource = TSL;
            e.Total = TSL.Count;

            this.TimeCodeGridStore.DataBind();

        }
        protected void printBtn_Click(object sender, EventArgs e)
        {
            PayrollTimeSchedulesReport p = GetReport();
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
            PayrollTimeSchedulesReport p = GetReport();
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
            PayrollTimeSchedulesReport p = GetReport();
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
        private PayrollTimeSchedulesReport GetReport()
        {

            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<TimeSchedule> routers = _PayrollService.ChildGetAll<TimeSchedule>(request);
            if (!routers.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", routers.ErrorCode) != null ? GetGlobalResourceObject("Errors", routers.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + routers.LogId: routers.Summary).Show();
                return null;
            }
            PayrollTimeSchedulesReport p = new PayrollTimeSchedulesReport();
            p.DataSource = routers.Items;
            p.Parameters["User"].Value = _systemService.SessionHelper.GetCurrentUser();
            p.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            p.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;

            return p;



        }


        protected void ApprovalStory_RefreshData(object sender, StoreReadDataEventArgs e)
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
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", routers.ErrorCode) != null ? GetGlobalResourceObject("Errors", routers.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + routers.LogId: routers.Summary).Show();
            this.apIdStore.DataSource = routers.Items;
            e.Total = routers.Items.Count; ;

            this.apIdStore.DataBind();
        }
    }
}
