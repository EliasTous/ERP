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
using Model.Payroll;
namespace Web.UI.Forms
{
    public partial class SocialSecuritySchedules : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IPayrollService _PayrollService = ServiceLocator.Current.GetInstance<IPayrollService>();

        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();




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
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(SocialSecuritySchedule), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
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
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(SocialSecurityScheduleSetup), socialSetupForm, socialSetupGrid, ADDNewsocialSetupBtn, saveSocialbutton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
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
            this.colAttachSetupGrid.Visible = false;
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

            panelRecordDetails.ActiveIndex = 0;
            string id = e.ExtraParams["id"];
            SocailSecurityrecordId.Text = id;
            string type = e.ExtraParams["type"];
            socialSetupStore.Reload();
          
            switch (type)
            {
                case "imgEdit":

                   
                        //Step 1 : get the object from the Web Service 
                        RecordRequest r = new RecordRequest();
                    r.RecordID = id;

                    RecordResponse<SocialSecuritySchedule> response = _PayrollService.ChildGetRecord<SocialSecuritySchedule>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                         Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.BasicInfoTab.SetValues(response.result);

                    recordId.Text = id;
                    socialSetupGrid.Disabled = false;
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
        protected void PoPuPSocialSetup(object sender, DirectEventArgs e)
        {


            string ssId = e.ExtraParams["id"];
            string seqNo = e.ExtraParams["seqNo"];
       
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    SocialSecurityScheduleSetupRequest r = new SocialSecurityScheduleSetupRequest();
                    r.ssId = Convert.ToInt32(ssId);
                    r.seqNo = Convert.ToInt32(seqNo);

                    RecordResponse<SocialSecurityScheduleSetup> response = _PayrollService.ChildGetRecord<SocialSecurityScheduleSetup>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                         Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.socialSetupForm.SetValues(response.result);

                  //  recordId.Text = ssId;
                    this.socialSetupForm.Title = Resources.Common.EditWindowsTitle;
                   
                    this.EditSocialSecuritySetupWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteSocialRecord({0},{1})", ssId,seqNo),
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
                SocialSecuritySchedule s = new SocialSecuritySchedule();
                s.recordId = index;
              
                   //s.intName = "";

                PostRequest<SocialSecuritySchedule> req = new PostRequest<SocialSecuritySchedule>();
                req.entity = s;
                PostResponse<SocialSecuritySchedule> r = _PayrollService.ChildDelete<SocialSecuritySchedule>(req);
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
        public void DeleteSocialRecord(string ssId,string seqNo)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                SocialSecurityScheduleSetup s = new SocialSecurityScheduleSetup();
                s.ssId =Convert.ToInt32( ssId);
                s.seqNo = Convert.ToInt32(seqNo);


                //s.intName = "";

                PostRequest<SocialSecurityScheduleSetup> req = new PostRequest<SocialSecurityScheduleSetup>();
                req.entity = s;
                PostResponse<SocialSecurityScheduleSetup> r = _PayrollService.ChildDelete<SocialSecurityScheduleSetup>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                     Common.errorMessage(r);
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    socialSetupStore.Reload(); 

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
            SocailSecurityrecordId.Text = "";
            panelRecordDetails.ActiveIndex = 0;
            
            



            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;
         
            
                socialSetupGrid.Disabled = true;
           
        

            this.EditRecordWindow.Show();
        }
        protected void ADDNewsocialSetupRecord(object sender, DirectEventArgs e)
        {

          

            //Reset all values of the relative object
            socialSetupForm.Reset();


            this.EditSocialSecuritySetupWindow.Title = Resources.Common.AddNewRecord;


            this.EditSocialSecuritySetupWindow.Show();
        }


        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;




            //Fetching the corresponding list

            //in this test will take a list of News
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<SocialSecuritySchedule> routers = _PayrollService.ChildGetAll<SocialSecuritySchedule>(request);
            if (!routers.Success)
            {
                Common.errorMessage(routers);
                return;
            }
            this.Store1.DataSource = routers.Items;
            e.Total = routers.Items.Count; ;

            this.Store1.DataBind();
        }



       
        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {

            socialSetupStore.Reload();


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.

            string obj = e.ExtraParams["values"];
            SocialSecuritySchedule b = JsonConvert.DeserializeObject<SocialSecuritySchedule>(obj);
           
            string id = e.ExtraParams["id"];
            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<SocialSecuritySchedule> request = new PostRequest<SocialSecuritySchedule>();

                    request.entity = b;
                    request.entity.recordId = SocailSecurityrecordId.Text; 


                    PostResponse<SocialSecuritySchedule> r = _PayrollService.ChildAddOrUpdate<SocialSecuritySchedule>(request);
                   
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
                        if (!string.IsNullOrEmpty(SocailSecurityrecordId.Text))
                        {
                            b.recordId = r.recordId;
                            //Add this record to the store 
                            this.Store1.Insert(0, b);

                            this.EditRecordWindow.Close();
                            RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;
                            sm.DeselectAll();
                            sm.Select(b.recordId.ToString());
                            
                            socialSetupStore.Reload();
                          
                        }

                        Store1.Reload();
                        SocailSecurityrecordId.Text = r.recordId;
                        
                        socialSetupGrid.Disabled = false;

                      

                            //Display successful notification
                            Notification.Show(new NotificationConfig
                            {
                                Title = Resources.Common.Notification,
                                Icon = Icon.Information,
                                Html = Resources.Common.RecordSavingSucc
                            });

                     
                           
                           
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
                    PostRequest<SocialSecuritySchedule> request = new PostRequest<SocialSecuritySchedule>();
                    request.entity = b;
                    PostResponse<SocialSecuritySchedule> r = _PayrollService.ChildAddOrUpdate<SocialSecuritySchedule>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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
                            ModelProxy record = this.Store1.GetById(id);
                            BasicInfoTab.UpdateRecord(record);
                            record.Commit();
                            Notification.Show(new NotificationConfig
                            {
                                Title = Resources.Common.Notification,
                                Icon = Icon.Information,
                                Html = Resources.Common.RecordUpdatedSucc
                            });
                        Store1.Reload();
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
        protected void saveSocialButton(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.

            string obj = e.ExtraParams["values"];
            SocialSecurityScheduleSetup b = JsonConvert.DeserializeObject<SocialSecurityScheduleSetup>(obj);

            string id = e.ExtraParams["id"];
            
            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    SocailSecurityseqId.Text = Convert.ToString(Convert.ToInt32(SocailSecurityseqId.Text) + 1); 
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<SocialSecurityScheduleSetup> request = new PostRequest<SocialSecurityScheduleSetup>();

                    request.entity = b;
                    request.entity.ssId =Convert.ToInt32( SocailSecurityrecordId.Text); 
                    request.entity.seqNo= Convert.ToInt32(SocailSecurityseqId.Text);


                    PostResponse<SocialSecurityScheduleSetup> r = _PayrollService.ChildAddOrUpdate<SocialSecurityScheduleSetup>(request);
                 
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

                        socialSetupStore.Reload();
                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });
                       
                        this.EditSocialSecuritySetupWindow.Close();
                      
                           

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
                    PostRequest<SocialSecurityScheduleSetup> request = new PostRequest<SocialSecurityScheduleSetup>();
                    request.entity = b;
                    request.entity.ssId = b.ssId;
                    request.entity.seqNo = b.seqNo;
                    PostResponse<SocialSecurityScheduleSetup> r = _PayrollService.ChildAddOrUpdate<SocialSecurityScheduleSetup>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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
                        socialSetupStore.Reload();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });
                        this.EditSocialSecuritySetupWindow.Close();


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
                Common.errorMessage(routers);
                return null;
            }
            PayrollTimeSchedulesReport p = new PayrollTimeSchedulesReport();
            p.DataSource = routers.Items;
            p.Parameters["User"].Value = _systemService.SessionHelper.GetCurrentUser();
            p.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            p.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;

            return p;



        }
        public void socialSetupStore_RefreshData(object sender, StoreReadDataEventArgs e)
        {
            

            if (!string.IsNullOrEmpty(SocailSecurityrecordId.Text))
            {
                SocialSecurityScheduleSetupListRequest request = new SocialSecurityScheduleSetupListRequest();

                request.Filter = "";
                request.ssId = Convert.ToInt32(SocailSecurityrecordId.Text);


                ListResponse<SocialSecurityScheduleSetup> routers = _PayrollService.ChildGetAll<SocialSecurityScheduleSetup>(request);
                if (!routers.Success)
                {
                    Common.errorMessage(routers);
                    return;
                }
                this.socialSetupStore.DataSource = routers.Items;
                e.Total = routers.Items.Count; ;

                this.socialSetupStore.DataBind();
            }
        }

        protected void PayCode_ReadData(object sender, StoreReadDataEventArgs e)
        {
            ListRequest req = new ListRequest();
            ListResponse<PayCode> eds = _PayrollService.ChildGetAll<PayCode>(req);

            Store2.DataSource = eds.Items;
            Store2.DataBind();

        }
    }
}
