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
using AionHR.Web.UI.Forms.ConstClasses;
using AionHR.Services.Messaging.CompanyStructure;
using AionHR.Model.Access_Control;
using AionHR.Services.Messaging.System;
using AionHR.Model.TaskSchedule;
using AionHR.Services.Messaging.TaskSchedule;

namespace AionHR.Web.UI.Forms
{
    public partial class AutomatedEmails : System.Web.UI.Page
    {


        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ITaskScheduleService _taskScheduleService = ServiceLocator.Current.GetInstance<ITaskScheduleService>();


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

        private void FillReportsComboStore()
        {
            ReportsListRequest request = new ReportsListRequest();
            request.moduleId = "80";
            request.Filter = "";
            ListResponse<ModuleClass> resp = _systemService.ChildGetAll<ModuleClass>(request);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
            }
            ReportComboStore.DataSource = resp.Items;
            ReportComboStore.DataBind();
        }

        private void FillSecGroupComboStore()
        {
            ListRequest request = new ListRequest();
            request.StartAt = "0";
            request.Filter = "";
            request.Size = "100";
            ListResponse<SecurityGroup> resp = _accessControlService.ChildGetAll<SecurityGroup>(request);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
            }
            sgStore.DataSource = resp.Items;
            sgStore.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!X.IsAjaxRequest && !IsPostBack)
            {

                SetExtLanguage();
                HideShowButtons();
                HideShowColumns();
                frequencyStore.DataSource = Common.XMLDictionaryList(_systemService, "46");
                frequencyStore.DataBind();

                receiverTypeStore.DataSource = Common.XMLDictionaryList(_systemService, "47");
                receiverTypeStore.DataBind();

                FillReportsComboStore();
                FillSecGroupComboStore();

                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(EntitlementDeduction), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
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
            //this.colAttach.Visible = false;
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



        protected void PoPuPReports(object sender, DirectEventArgs e)
        {

            
            string id = e.ExtraParams["id"];
            string reportId = e.ExtraParams["reportId"];
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id;

                    RecordResponse<Report> response = _taskScheduleService.ChildGetRecord<Report>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object

                    this.reportsForm.SetValues(response.result);
                   
                    this.reportsWindow.Title = Resources.Common.EditWindowsTitle;
                    this.reportsWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteReportRecord({0},{1})", id, reportId),
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


        protected void PoPuPReceiver(object sender, DirectEventArgs e)
        {


            string taskIdParamter = e.ExtraParams["taskId"];
            int seqNoParameter = Convert.ToInt32(e.ExtraParams["seqNo"]);
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    TaskReceiverListRequest r = new TaskReceiverListRequest();
                    r.taskId = taskIdParamter;
                    r.seqNo = seqNoParameter.ToString();
                   

                    RecordResponse<Receiver> response = _taskScheduleService.ChildGetRecord<Receiver>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.receiversForm.SetValues(response.result);
                    sgId.SetValue(response.result.sgId);
                    receiverType.SetValue(response.result.receiverType);


                    this.receiversWindow.Title = Resources.Common.EditWindowsTitle;
                    this.receiversWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteReceiverRecord({0},{1})", taskIdParamter, seqNoParameter),
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

        private void FillReportStore()
        {

            //GEtting the filter from the page
            string filter = string.Empty;




            //Fetching the corresponding list

            //in this test will take a list of News
            TaskReportsListRequest request = new TaskReportsListRequest();

            request.Filter = "";
            request.taskId = currentRuId.Text;
            ListResponse<Report> routers = _taskScheduleService.ChildGetAll<Report>(request);
            if (!routers.Success)
            {
                Common.errorMessage(routers);
                return;
            }

            this.reportsStore.DataSource = routers.Items;


            this.reportsStore.DataBind();
        }

        private void FillReceiverStore()
        {

            //GEtting the filter from the page
            string filter = string.Empty;




            //Fetching the corresponding list

            //in this test will take a list of News
            TaskReportsListRequest request = new TaskReportsListRequest();

            request.Filter = "";
            request.taskId = currentRuId.Text;
            ListResponse<Receiver> routers = _taskScheduleService.ChildGetAll<Receiver>(request);
            if (!routers.Success)
            {
                Common.errorMessage(routers);
                return;
            }

            if (routers.Items.Count > 0)
            {
                lineSeq.Text = (routers.Items.Count + 1).ToString();
            }
            else
            {
                lineSeq.Text = "1";
            }

            this.receiversStore.DataSource = routers.Items;
            

            this.receiversStore.DataBind();
        }

        protected void PoPuP(object sender, DirectEventArgs e)
        {

            panelRecordDetails.ActiveIndex = 0;
            reportsGrid.Disabled = false;
            ReceiversGrid.Disabled = false;
            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id;

                    RecordResponse<Service> response = _taskScheduleService.ChildGetRecord<Service>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.BasicInfoTab.SetValues(response.result);
                    frequency.SetValue(response.result.frequency);
                    currentRuId.Text = id;
                    FillReportStore();
                    FillReceiverStore();
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
        /// DeleteMessageRecord
        [DirectMethod]
        public void DeleteReceiverRecord(string taskIdParamter, int seqNoParameter)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                Receiver s = new Receiver();
                s.taskId = Convert.ToInt32(taskIdParamter);
                s.seqNo = seqNoParameter;


                PostRequest<Receiver> req = new PostRequest<Receiver>();
                req.entity = s;
                PostResponse<Receiver> r = _taskScheduleService.ChildDelete<Receiver>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(r);
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    receiversStore.Remove(taskIdParamter);

                    //Step 3 : Showing a notification for the user 
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordDeletedSucc
                    });

                    FillReceiverStore();
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
        public void DeleteReportRecord(string index, string rpt)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                Report s = new Report();
                s.taskId = Convert.ToInt32(index);
                s.reportId = Convert.ToInt32(rpt);


                PostRequest<Report> req = new PostRequest<Report>();
                req.entity = s;
                PostResponse<Report> r = _taskScheduleService.ChildDelete<Report>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(r);
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    reportsStore.Remove(index);

                    //Step 3 : Showing a notification for the user 
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordDeletedSucc
                    });

                    FillReportStore();
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
        public void DeleteRecord(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                Service s = new Service();
                s.recordId = index;

                s.name = "";

                PostRequest<Service> req = new PostRequest<Service>();
                req.entity = s;
                PostResponse<Service> r = _taskScheduleService.ChildDelete<Service>(req);
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

                    Store1.Reload();
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



        protected void ADDNewReportRecord(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            reportsForm.Reset();
            //moduleId.Hidden = false;
            //classId.Hidden = false;
            //keyName.ReadOnly = false;
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;
            this.reportsWindow.Show();
        }
        protected void ADDNewReceiverRecord(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            receiversForm.Reset();
            seqNo.Text = (Convert.ToInt16(string.IsNullOrEmpty(lineSeq.Text) ? "0" : lineSeq.Text) + 1).ToString();
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;
            this.receiversWindow.Show();
        }
        protected void ADDNewRecord(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            BasicInfoTab.Reset();
            currentRuId.Text = "";
            panelRecordDetails.ActiveIndex = 0;
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;
            reportsGrid.Disabled = true;
            ReceiversGrid.Disabled = true;
            //conditionsStore.DataSource = new List<Model.Company.Structure.RuleCondition>();
            //conditionsStore.DataBind();
            //messagesStore.DataSource = new List<Model.Company.Structure.RuleMessage>();
            //messagesStore.DataBind();
            this.EditRecordWindow.Show();
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
            ListResponse<Service> routers = _taskScheduleService.ChildGetAll<Service>(request);
            if (!routers.Success)
            {
                Common.errorMessage(routers);
                return;
            }

            this.Store1.DataSource = routers.Items;
            e.Total = routers.Items.Count; 

            this.Store1.DataBind();
        }



        protected void SaveNewReportRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.

            
            string obj = e.ExtraParams["values"];
            Report b = new Report();// JsonConvert.DeserializeObject<Report>(obj);

            string id = e.ExtraParams["id"];
            b.reportId = Convert.ToInt32(classId.SelectedItem.Value);

            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<Report> request = new PostRequest<Report>();

                    request.entity = b;
                    request.entity.taskId = Convert.ToInt32(currentRuId.Text);
                    //request.entity.value = string.IsNullOrEmpty(request.entity.value) ? null : request.entity.value;
                    //request.entity.expressionId = expressionCombo2.getExpression();
                    PostResponse<Report> r = _taskScheduleService.ChildAddOrUpdate<Report>(request);


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
                        //b.recordId = r.recordId;
                        //Add this record to the store 
                        FillReportStore();

                        this.reportsWindow.Close();
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
                    PostRequest<Report> request = new PostRequest<Report>();
                    request.entity = b;
                    request.entity.taskId = Convert.ToInt32(currentRuId.Text);
                    //request.entity.value = string.IsNullOrEmpty(request.entity.value) ? null : request.entity.value;
                    PostResponse<Report> r = _taskScheduleService.ChildAddOrUpdate<Report>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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

                        FillReportStore();
                        this.reportsWindow.Close();
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



        protected void SaveNewReceiverRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.

            if (!pdf.Checked && !xls.Checked && !csv.Checked)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("SelectPay")).Show();
                return;
            }

            string obj = e.ExtraParams["values"];
            Receiver b = new Receiver();//JsonConvert.DeserializeObject<Receiver>(obj);

            string id = e.ExtraParams["taskId"];
            string seqNo = e.ExtraParams["seqNo"];
            if (id != "")
            { b.taskId = Convert.ToInt32(id); }
            if (seqNo != "")
            { b.seqNo = Convert.ToInt32(seqNo); }
            
            b.receiverType = Convert.ToInt32(receiverType.SelectedItem.Value);
            if (Convert.ToInt32(sgId.SelectedItem.Value) == 0)
            { b.sgId = null; }
            else
            { b.sgId = Convert.ToInt32(sgId.SelectedItem.Value); }
            
            b.email = email.Text;
            
            if (languageId.SelectedItem.Value == null)
            { b.languageId = null; }
            else
            { b.languageId = Convert.ToInt32(languageId.SelectedItem.Value); }
            
            b.pdf = pdf.Checked;
            b.xls = xls.Checked;
            b.csv = csv.Checked;

            // Define the object to add or edit as null

            //if (string.IsNullOrEmpty(b.taskId.ToString()))// && string.IsNullOrEmpty(b.message))
            if (b.taskId.ToString() == "0")
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<Receiver> request = new PostRequest<Receiver> ();

                    request.entity = b;
                    request.entity.taskId = Convert.ToInt32(currentRuId.Text);
                    if (lineSeq.Text == "")
                    { request.entity.seqNo = 1; }
                    else
                    { request.entity.seqNo = Convert.ToInt32(lineSeq.Text); }
                    
                    //request.entity.ruleId = currentRuId.Text;
                    PostResponse<Receiver> r = _taskScheduleService.ChildAddOrUpdate<Receiver>(request);


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
                        FillReceiverStore();

                        this.receiversWindow.Close();
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
                    PostRequest<Receiver> request = new PostRequest<Receiver>();
                    request.entity = b;
                    //request.entity.ruleId = currentRuId.Text;

                    PostResponse<Receiver> r = _taskScheduleService.ChildAddOrUpdate<Receiver>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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

                        //FillMessageStore();
                        this.receiversWindow.Close();
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


        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.


            string obj = e.ExtraParams["values"];
            Service b = JsonConvert.DeserializeObject< Service>(obj);

            string id = e.ExtraParams["id"];

            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(currentRuId.Text))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<Service> request = new PostRequest<Service>();

                    request.entity = b;
                    //request.entity.expressionId = expressionCombo1.getExpression();
                    PostResponse<Service> r = _taskScheduleService.ChildAddOrUpdate<Service>(request);


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
                        Store1.Reload();
                        currentRuId.Text = b.recordId;
                        reportsGrid.Disabled = false;
                        ReceiversGrid.Disabled = false;

                        FillReportStore();
                        FillReceiverStore();

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
                    PostRequest<Service> request = new PostRequest<Service>();
                    request.entity = b;
                    request.entity.recordId = currentRuId.Text;
                    PostResponse<Service> r = _taskScheduleService.ChildAddOrUpdate<Service>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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
                        //currentRuId.Text = "";

                        FillReportStore();
                        FillReceiverStore();

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



        protected void CheckTime(object sender, DirectEventArgs e)
        {
            try
            {


                string timeId = e.ExtraParams["timeId"];
                if (string.IsNullOrEmpty(timeId))
                    return;


                if (!IsTime(timeId))
                {
                    X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorGettingLatenesses")).Show();
                    return;
                }
                
            }
            catch (Exception exp)
            {
                X.MessageBox.Alert(GetGlobalResourceObject("Common", "Error").ToString(), exp.Message);
            }


        }

        bool IsTime(string myValue)
        {

            bool Succeed = true;

            try
            {
                DateTime temp = Convert.ToDateTime(myValue);
            }
            catch (Exception ex)
            {
                Succeed = false;
            }

            return Succeed;
        }

        protected void LanguageStore_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            XMLDictionaryListRequest request = new XMLDictionaryListRequest();

            request.database = "23";
            ListResponse<XMLDictionary> resp = _systemService.ChildGetAll<XMLDictionary>(request);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            LanguageStore.DataSource = resp.Items;
            LanguageStore.DataBind();

        }


        protected void EnableLang(object sender, DirectEventArgs e)
        {
            try
            {
                string recType = receiverType.SelectedItem.Value;
                if (string.IsNullOrEmpty(recType))
                    return;

                if (recType == "2")
                {
                    languageId.AllowBlank = false;
                    languageId.Disabled = false;
                    sgId.Disabled = true;
                    sgId.SelectedItem.Value = null;
                    email.Disabled = false;
                }
                else
                {
                    languageId.AllowBlank = true;
                    languageId.Disabled = true;
                    languageId.Text = "";
                    languageId.SelectedItem.Value = null;
                    email.Text = "";
                    email.Disabled = true;
                    sgId.Disabled = false;
                }

                

            }
            catch (Exception exp)
            {
                X.MessageBox.Alert(GetGlobalResourceObject("Common", "Error").ToString(), exp.Message);
            }


        }



























    }
}