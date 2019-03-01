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
using AionHR.Model.Payroll;
using AionHR.Model.AdminTemplates;

namespace AionHR.Web.UI.Forms
{
    public partial class ProcessNotifications : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();

        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        IPayrollService _payrollService = ServiceLocator.Current.GetInstance<IPayrollService>();
        IAdministrationService _administrationService = ServiceLocator.Current.GetInstance<IAdministrationService>();

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
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!X.IsAjaxRequest && !IsPostBack)
            {

                SetExtLanguage();
                HideShowButtons();
                ProcessNotificationStore.Reload();
                fillStore1();
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(ProcessNotification), null, ProcessNotificationGrid, null, SaveButton);
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
        //private void HideShowColumns()
        //{
        //    this.colAttach.Visible = false;
        //}


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

        
            string payCode = e.ExtraParams["payCode"];
            string name = e.ExtraParams["name"];
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    PayCodeRecordRequest r = new PayCodeRecordRequest();
                    r.payCode = payCode;

                    RecordResponse<PayCode> response = _payrollService.ChildGetRecord<PayCode>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                  
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteRecord('{0}','{1}')", payCode, name),
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
        public void DeleteRecord(string paycode, string name)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                PayCode s = new PayCode();
                s.payCode = paycode;
                s.name = name;
                //s.intName = "";

                PostRequest<PayCode> req = new PostRequest<PayCode>();
                req.entity = s;
                PostResponse<PayCode> r = _payrollService.ChildDelete<PayCode>(req);
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
    

        /// <summary>
        /// Direct method for removing multiple records
        /// </summary>
      
        /// <summary>
        /// Adding new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       

        protected void ProcessNotificationStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            ListRequest req = new ListRequest();
            req.Filter = "";
            ListResponse<ProcessNotification> res = _administrationService.ChildGetAll<ProcessNotification>(req);
            if (!res.Success)//it maybe be another condition
            {
                //Show an error saving...
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(res);
                return;
            }

            List<ProcessNotification> PN = new List<ProcessNotification>();
            PN.Add(new ProcessNotification { prName = GetLocalResourceObject("PENALTY_NEW").ToString(), processId = ProcessNotificationTypes.PENALTY_NEW });
            PN.Add(new ProcessNotification { prName = GetLocalResourceObject("PENALTY_APPROVED").ToString(), processId = ProcessNotificationTypes.PENALTY_APPROVED,  });
            PN.Add(new ProcessNotification { prName = GetLocalResourceObject("PENALTY_REJECTED").ToString(), processId = ProcessNotificationTypes.PENALTY_REJECTED });
            PN.Add(new ProcessNotification { prName = GetLocalResourceObject("TIME_SCHEDULE").ToString(), processId = ProcessNotificationTypes.TIME_SCHEDULE, });
            PN.Add(new ProcessNotification { prName = GetLocalResourceObject("LEAVE_REQUEST_NEW").ToString(), processId = ProcessNotificationTypes.LEAVE_REQUEST_NEW, });
            PN.Add(new ProcessNotification { prName = GetLocalResourceObject("LEAVE_REQUEST_APPROVED").ToString(), processId = ProcessNotificationTypes.LEAVE_REQUEST_APPROVED,  });
            PN.Add(new ProcessNotification { prName = GetLocalResourceObject("LEAVE_REQUEST_REJECTED").ToString(), processId = ProcessNotificationTypes.LEAVE_REQUEST_REJECTED,  });
            PN.Add(new ProcessNotification { prName = GetLocalResourceObject("LOAN_REQUEST_NEW").ToString(), processId = ProcessNotificationTypes.LOAN_REQUEST_NEW,  });

            PN.Add(new ProcessNotification { prName = GetLocalResourceObject("LOAN_REQUEST_APPROVED").ToString(), processId = ProcessNotificationTypes.LOAN_REQUEST_APPROVED,  });
            PN.Add(new ProcessNotification { prName = GetLocalResourceObject("LOAN_REQUEST_REJECTED").ToString(), processId = ProcessNotificationTypes.LOAN_REQUEST_REJECTED });
            PN.Add(new ProcessNotification { prName = GetLocalResourceObject("PAYROLL_PAYSLIP").ToString(), processId = ProcessNotificationTypes.PAYROLL_PAYSLIP,  });


            PN.Add(new ProcessNotification { prName = GetLocalResourceObject("TIME_VARIATION_NEW").ToString(), processId = ProcessNotificationTypes.TIME_VARIATION_NEW });
            PN.Add(new ProcessNotification { prName = GetLocalResourceObject("TIME_VARIATION_APPROVED").ToString(), processId = ProcessNotificationTypes.TIME_VARIATION_APPROVED });
            PN.Add(new ProcessNotification { prName = GetLocalResourceObject("TIME_VARIATION_REJECTED").ToString(), processId = ProcessNotificationTypes.TIME_VARIATION_REJECTED });
            res.Items.ForEach(x =>
            {
               if ( (PN.Where(y => y.processId == x.processId).First()!=null ) )
                    PN.Where(y => y.processId == x.processId).First().templateId = x.templateId;
            });
            

            ProcessNotificationStore.DataSource = PN;
            ProcessNotificationStore.DataBind();
        }
        private void fillStore1()
        {
            ListRequest req = new ListRequest();
            req.Filter = "";
            ListResponse<AdTemplate> res = _administrationService.ChildGetAll<AdTemplate>(req);
            if (!res.Success)//it maybe be another condition
            {
                //Show an error saving...
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(res);
                return;
            }
            Store1.DataSource = res.Items;
            Store1.DataBind(); 
           
        
        }

        protected void SubmitData(object sender, StoreSubmitDataEventArgs e)
        {
            try
            {
                PostRequest<ProcessNotification> request = new PostRequest<ProcessNotification>();
                PostResponse<ProcessNotification> r;
                List<ProcessNotification> PN = e.Object<ProcessNotification>();
                PN.ForEach(x =>
                {
                    if (x.templateId != null)
                    {


                        request.entity = x;
                        r = _administrationService.ChildAddOrUpdate<ProcessNotification>(request);
                    }
                    else
                    {


                        request.entity = x;
                        r = _administrationService.ChildDelete<ProcessNotification>(request);
                    }
              
                        if (!r.Success)//it maybe be another condition
                          {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            Common.errorMessage(r);
                          
                            throw new Exception();
                        }
                    
                    
                       

                    

                });
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordSavingSucc
                });
            }
            catch
            {

            }

        }

            protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.


            string obj = e.ExtraParams["values"];
            PayCode b = JsonConvert.DeserializeObject<PayCode>(obj);


            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(b.payCode))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<PayCode> request = new PostRequest<PayCode>();

                    request.entity = b;
                    PostResponse<PayCode> r = _payrollService.ChildAddOrUpdate<PayCode>(request);


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


                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        Store1.Reload();



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
                    PostRequest<PayCode> request = new PostRequest<PayCode>();
                    request.entity = b;
                    PostResponse<PayCode> r = _payrollService.ChildAddOrUpdate<PayCode>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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
      
      

      

    }
}