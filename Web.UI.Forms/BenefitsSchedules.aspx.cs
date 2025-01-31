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
using Model.Benefits;
using Services.Messaging.Benefits;
using Web.UI.Forms.ConstClasses;

namespace Web.UI.Forms
{
    public partial class BenefitsSchedules : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();

        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        IBenefitsService _benefitsService = ServiceLocator.Current.GetInstance<IBenefitsService>();


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
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(CertificateLevel), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
                }

                bsId.Text = "";
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


        
        protected void PoPuPBESC(object sender, DirectEventArgs e)
        {


            string benId = e.ExtraParams["benefitId"];
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 

                    ScheduleBenefitsRecordRequest r = new ScheduleBenefitsRecordRequest();
                    r.bsId = bsId.Text;
                    r.benefitId = benId; 

                    RecordResponse<ScheduleBenefits> response = _benefitsService.ChildGetRecord<ScheduleBenefits>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                       Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.ScheduleBenefitFormPanel.Reset();
                    this.ScheduleBenefitFormPanel.SetValues(response.result);
                 
                        benefitId.Select(response.result.benefitId.ToString());

                      
                    //if (!string.IsNullOrEmpty(response.result.aqType.ToString()))
                    //    aqType.Select(response.result.aqType.ToString());
                    if (string.IsNullOrEmpty(response.result.defaultAmount.ToString()))
                    defaultAmount.Text = "0";
                    if (string.IsNullOrEmpty(response.result.intervalDays.ToString()))
                     intervalDays.Text = "0";



                        this.ScheduleBenefitWindow.Title = Resources.Common.EditWindowsTitle;
                    this.ScheduleBenefitWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteScheduleBenefitRecord({0},{1})", benId,bsId.Text),
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
        protected void PoPuP(object sender, DirectEventArgs e)
        {


            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];
            bsId.Text = id; 


            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id;

                    RecordResponse<BenefitsSchedule> response = _benefitsService.ChildGetRecord<BenefitsSchedule>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                       Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.BasicInfoTab.SetValues(response.result);

                    ScheduleBenefitsGrid.Disabled = false;
                    this.EditRecordWindow.Title = Resources.Common.EditWindowsTitle;
                    panelRecordDetails.ActiveIndex = 0;
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
                BenefitsSchedule s = new BenefitsSchedule();
                s.recordId = index;
                //s.reference = "";

                s.name = "";
                PostRequest<BenefitsSchedule> req = new PostRequest<BenefitsSchedule>();
                req.entity = s;
                PostResponse<BenefitsSchedule> r = _benefitsService.ChildDelete<BenefitsSchedule>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                  Common.errorMessage(r);
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
        [DirectMethod]
        public void DeleteScheduleBenefitRecord(string benefitId,string bsId)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                ScheduleBenefits s = new ScheduleBenefits();
                s.bsId =Convert.ToInt32(bsId);
                s.benefitId = Convert.ToInt32(benefitId); 




                PostRequest<ScheduleBenefits> req = new PostRequest<ScheduleBenefits>();
                req.entity = s;
                PostResponse<ScheduleBenefits> r = _benefitsService.ChildDelete<ScheduleBenefits>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                  Common.errorMessage(r);
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    ScheduleBenefitsStore.Reload();

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
        /// 

        
        protected void ADDNewBenefitsScheduleRecord(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            ScheduleBenefitFormPanel.Reset();


            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;

        
            this.ScheduleBenefitWindow.Show();
        }
        protected void ADDNewRecord(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            BasicInfoTab.Reset();


            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;

            panelRecordDetails.ActiveIndex = 0;
            this.EditRecordWindow.Show();
            bsId.Text = "";
            ScheduleBenefitsGrid.Disabled = true;
        }
       

        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<BenefitsSchedule> routers = _benefitsService.ChildGetAll<BenefitsSchedule>(request);
            if (!routers.Success)
                return;
            this.Store1.DataSource = routers.Items;
            e.Total = routers.Items.Count; ;

            this.Store1.DataBind();
        }
        protected void ScheduleBenefitsStore_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            ScheduleBenefitsListRequest request = new ScheduleBenefitsListRequest();

            request.Filter = "";
            request.bsId = bsId.Text;
            ListResponse<ScheduleBenefits> routers = _benefitsService.ChildGetAll<ScheduleBenefits>(request);
            if (!routers.Success)
                return;
            this.ScheduleBenefitsStore.DataSource = routers.Items;
            e.Total = routers.Items.Count; ;

            this.ScheduleBenefitsStore.DataBind();
        }


        
        protected void SaveNewScheduleBenefitRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.


            string obj = e.ExtraParams["values"];
            ScheduleBenefits b = JsonConvert.DeserializeObject<ScheduleBenefits>(obj);
            //if (!b.isChecked)
            //{
            //    DeleteScheduleBenefitRecord(b.benefitId.ToString(), bsId.Text);
            //    this.ScheduleBenefitWindow.Close();
            //    return; 
            //}

            
            string id = e.ExtraParams["id"];
            // Define the object to add or edit as null

            //if (string.IsNullOrEmpty(b.benefitId.ToString()))
            //{

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<ScheduleBenefits> request = new PostRequest<ScheduleBenefits>();

                    request.entity = b;
                    request.entity.bsId = Convert.ToInt32(bsId.Text);
                    PostResponse<ScheduleBenefits> r = _benefitsService.ChildAddOrUpdate<ScheduleBenefits>(request);


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
                        ScheduleBenefitsStore.Reload(); 

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        this.ScheduleBenefitWindow.Close();
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


            
            //else
            //{
            //    //Update Mode

            //    try
            //    {
            //        //getting the id of the record
            //        PostRequest<BenefitsSchedule> request = new PostRequest<BenefitsSchedule>();
            //        request.entity = b;
            //        PostResponse<BenefitsSchedule> r = _benefitsService.ChildAddOrUpdate<BenefitsSchedule>(request);                      //Step 1 Selecting the object or building up the object for update purpose

            //        //Step 2 : saving to store

            //        //Step 3 :  Check if request fails
            //        if (!r.Success)//it maybe another check
            //        {
            //            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
            //          Common.errorMessage(r);
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
        }

        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.


            string obj = e.ExtraParams["values"];
            BenefitsSchedule b = JsonConvert.DeserializeObject<BenefitsSchedule>(obj);

            string id = e.ExtraParams["id"];
            // Define the object to add or edit as null
           
            if (string.IsNullOrEmpty(id)&& string.IsNullOrEmpty(bsId.Text))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<BenefitsSchedule> request = new PostRequest<BenefitsSchedule>();

                    
                    request.entity = b;
                    PostResponse<BenefitsSchedule> r = _benefitsService.ChildAddOrUpdate<BenefitsSchedule>(request);


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

                       
                        RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;
                        sm.DeselectAll();
                        sm.Select(b.recordId.ToString());
                        bsId.Text = r.recordId;
                        ScheduleBenefitsGrid.Disabled = false;




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
                    PostRequest<BenefitsSchedule> request = new PostRequest<BenefitsSchedule>();
                    if (string.IsNullOrEmpty(id))
                        b.recordId = bsId.Text;
                        request.entity = b;
                    PostResponse<BenefitsSchedule> r = _benefitsService.ChildAddOrUpdate<BenefitsSchedule>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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
        //protected void printBtn_Click(object sender, EventArgs e)
        //{
        //    CertificateLevelsReport p = GetReport();
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
        //    CertificateLevelsReport p = GetReport();
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
        //    CertificateLevelsReport p = GetReport();
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
        //private CertificateLevelsReport GetReport()
        //{

        //    ListRequest request = new ListRequest();

        //    request.Filter = "";
        //    ListResponse<CertificateLevel> resp = _employeeService.ChildGetAll<CertificateLevel>(request);
        //    if (!resp.Success)
        //    {
        //        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //       Common.errorMessage(resp);
        //        return null;
        //    }
        //    CertificateLevelsReport p = new CertificateLevelsReport();
        //    p.DataSource = resp.Items;
        //    p.Parameters["User"].Value = _systemService.SessionHelper.GetCurrentUser();
        //    p.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
        //    p.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;

        //    return p;



        //}

        private void FillBenefits()
        {
            ListRequest req = new ListRequest();
          

            ListResponse<Benefit> response = _benefitsService.ChildGetAll<Benefit>(req);
            if (!response.Success)
            {
               Common.errorMessage(response);
                benefitsStore.DataSource = new List<Benefit>();
            }
            benefitsStore.DataSource = response.Items;
            benefitsStore.DataBind();
        }
        [DirectMethod]
        public object FillBenefits(string action, Dictionary<string, object> extraParams)
        {
            StoreRequestParameters prms = new StoreRequestParameters(extraParams);



            List<Benefit> data;
            ListRequest req = new ListRequest();
           

            ListResponse<Benefit> response = _benefitsService.ChildGetAll<Benefit>(req);
            if (!response.Success)
            {
               Common.errorMessage(response);
                return new List<Department>();
            }
            data = response.Items;
            return new
            {
                data
            };

        }
    }
}