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

namespace AionHR.Web.UI.Forms
{
    public partial class Rules : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();
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
                actionTypeStore.DataSource = Common.XMLDictionaryList(_systemService, "31");
                actionTypeStore.DataBind();
                operStore.DataSource = Common.XMLDictionaryList(_systemService, "32");
                operStore.DataBind();
                languageStore.DataSource= Common.XMLDictionaryList(_systemService, "23");
                languageStore.DataBind();
                moduleStore.DataSource = Common.XMLDictionaryList(_systemService, "1");
                moduleStore.DataBind();
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


        

        protected void PoPuPCondition(object sender, DirectEventArgs e)
        {

           
            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id;

                    RecordResponse<Model.Company.Structure.RuleCondition> response = _companyStructureService.ChildGetRecord<Model.Company.Structure.RuleCondition>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.conditionForm.SetValues(response.result);
                    keyName.GetStore().Add(new object[]
                       {
                                new
                                {
                                   propertyName=response.result.keyName
                                }
                       });
                    keyName.SetValue(response.result.keyName);
                    moduleId.Hidden = true;
                    classId.Hidden = true;
                    keyName.ReadOnly = true;


                    this.conditionWindow.Title = Resources.Common.EditWindowsTitle;
                    this.conditionWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteConditionRecord({0})", id),
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


        protected void PoPuPMessage(object sender, DirectEventArgs e)
        {


            string languageIdParamter = e.ExtraParams["languageId"];
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RuleMessageRecordRequest r = new RuleMessageRecordRequest();
                    r.ruleId = currentRuId.Text;
                    r.languageId = languageIdParamter;

                    RecordResponse<Model.Company.Structure.RuleMessage> response = _companyStructureService.ChildGetRecord<Model.Company.Structure.RuleMessage>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.messageForm.SetValues(response.result);


                    this.messageWindow.Title = Resources.Common.EditWindowsTitle;
                    this.messageWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteMessageRecord({0})", languageIdParamter),
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

            panelRecordDetails.ActiveIndex = 0;
            conditionsGrid.Disabled = false;
            messagesGrid.Disabled = false;
            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id;

                    RecordResponse<Model.Company.Structure.Rule> response = _companyStructureService.ChildGetRecord<Model.Company.Structure.Rule>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.BasicInfoTab.SetValues(response.result);
                    currentRuId.Text = id;
                    FillConditionStore();
                    FillMessageStore();
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
        public void DeleteMessageRecord(string languageIdParamter)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                Model.Company.Structure.RuleMessage s = new Model.Company.Structure.RuleMessage();
                s.languageId = languageIdParamter;
                s.ruleId = currentRuId.Text;


                PostRequest<Model.Company.Structure.RuleMessage> req = new PostRequest<Model.Company.Structure.RuleMessage>();
                req.entity = s;
                PostResponse<Model.Company.Structure.RuleMessage> r = _companyStructureService.ChildDelete<Model.Company.Structure.RuleMessage>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(r);
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    FillMessageStore();

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
        public void DeleteConditionRecord(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                Model.Company.Structure.RuleCondition s = new Model.Company.Structure.RuleCondition();
                s.recordId = index;
                s.ruleId = currentRuId.Text;
              

                PostRequest<Model.Company.Structure.RuleCondition> req = new PostRequest<Model.Company.Structure.RuleCondition>();
                req.entity = s;
                PostResponse<Model.Company.Structure.RuleCondition> r = _companyStructureService.ChildDelete<Model.Company.Structure.RuleCondition>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(r);
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    FillConditionStore();

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
        public void DeleteRecord(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                Model.Company.Structure.Rule s = new Model.Company.Structure.Rule();
                s.recordId = index;

                s.name = "";
              
                PostRequest<Model.Company.Structure.Rule> req = new PostRequest<Model.Company.Structure.Rule>();
                req.entity = s;
                PostResponse<Model.Company.Structure.Rule> r = _companyStructureService.ChildDelete<Model.Company.Structure.Rule>(req);
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
        
        protected void ADDNewConditionRecord(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            conditionForm.Reset();
            moduleId.Hidden = false;
            classId.Hidden = false;
            keyName.ReadOnly = false;

            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;


            this.conditionWindow.Show();
        }
        protected void ADDNewMessageRecord(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            messageForm.Reset();


            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;


            this.messageWindow.Show();
        }
        protected void ADDNewRecord(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            BasicInfoTab.Reset();
            currentRuId.Text = "";
            panelRecordDetails.ActiveIndex = 0;
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;
            conditionsGrid.Disabled = true;
            messagesGrid.Disabled = true;
            conditionsStore.DataSource = new List<Model.Company.Structure.RuleCondition>();
            conditionsStore.DataBind(); 
            messagesStore.DataSource= new List<Model.Company.Structure.RuleMessage>();
            messagesStore.DataBind(); 
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
            ListResponse<Model.Company.Structure.Rule> routers = _companyStructureService.ChildGetAll<Model.Company.Structure.Rule>(request);
            if (!routers.Success)
            {
                Common.errorMessage(routers);
                return;
            }

            this.Store1.DataSource = routers.Items;
            e.Total = routers.Items.Count; ;

            this.Store1.DataBind();
        }




        private void FillConditionStore()
        {

            //GEtting the filter from the page
            string filter = string.Empty;
          



            //Fetching the corresponding list

            //in this test will take a list of News
            RuleConditionListRequest request = new RuleConditionListRequest();

            request.Filter = "";
            request.ruleId = currentRuId.Text;
            ListResponse<Model.Company.Structure.RuleCondition> routers = _companyStructureService.ChildGetAll<Model.Company.Structure.RuleCondition>(request);
            if (!routers.Success)
            {
                Common.errorMessage(routers);
                return;
            }

            this.conditionsStore.DataSource = routers.Items;
           

            this.conditionsStore.DataBind();
        }
        private void FillMessageStore()
        {

            //GEtting the filter from the page
            string filter = string.Empty;




            //Fetching the corresponding list

            //in this test will take a list of News
            RuleConditionListRequest request = new RuleConditionListRequest();

            request.Filter = "";
            request.ruleId = currentRuId.Text;
            ListResponse<Model.Company.Structure.RuleMessage> routers = _companyStructureService.ChildGetAll<Model.Company.Structure.RuleMessage>(request);
            if (!routers.Success)
            {
                Common.errorMessage(routers);
                return;
            }

            this.messagesStore.DataSource = routers.Items;


            this.messagesStore.DataBind();
        }

        
        protected void FillKeyName(object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(moduleId.SelectedItem.Value.ToString()) && !string.IsNullOrEmpty(classId.SelectedItem.Value.ToString()))
            {
                ClassPropertyListRequest req = new ClassPropertyListRequest();
                req.moduleId = moduleId.SelectedItem.Value.ToString();
                req.classId = classId.SelectedItem.Value.ToString();
                ListResponse<AionHR.Model.System.ClassProperty> resp = _systemService.ChildGetAll<AionHR.Model.System.ClassProperty>(req);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }
                keyNameStore.DataSource = resp.Items;
                keyNameStore.DataBind();
            }


        }
        protected void FillClassIdCombo(object sender, DirectEventArgs e)
        {

            if (!string.IsNullOrEmpty(moduleId.SelectedItem.Value.ToString()))
                {
                AccessControlListRequest req = new AccessControlListRequest();
                req.GroupId = "0";
                req.ModuleId = moduleId.SelectedItem.Value.ToString();
                ListResponse<ModuleClass> resp = _systemService.ChildGetAll<ModuleClass>(req);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }
                ClassStore.DataSource = resp.Items;
                ClassStore.DataBind();
            }



        }
        protected void SaveNewConditionRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.


            string obj = e.ExtraParams["values"];
            Model.Company.Structure.RuleCondition b = JsonConvert.DeserializeObject<Model.Company.Structure.RuleCondition>(obj);

            string id = e.ExtraParams["id"];

            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<Model.Company.Structure.RuleCondition> request = new PostRequest<Model.Company.Structure.RuleCondition>();

                    request.entity = b;
                    request.entity.ruleId = currentRuId.Text;
                    PostResponse<Model.Company.Structure.RuleCondition> r = _companyStructureService.ChildAddOrUpdate<Model.Company.Structure.RuleCondition>(request);


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
                        FillConditionStore();
                        
                        this.conditionWindow.Close();
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
                    PostRequest<Model.Company.Structure.RuleCondition> request = new PostRequest<Model.Company.Structure.RuleCondition>();
                    request.entity = b;
                    request.entity.ruleId = currentRuId.Text;

                    PostResponse<Model.Company.Structure.RuleCondition> r = _companyStructureService.ChildAddOrUpdate<Model.Company.Structure.RuleCondition>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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

                        FillConditionStore();
                        this.conditionWindow.Close();
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


        protected void SaveNewMessageRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.


            string obj = e.ExtraParams["values"];
            Model.Company.Structure.RuleMessage b = JsonConvert.DeserializeObject<Model.Company.Structure.RuleMessage>(obj);

           

            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(b.languageId)&& string.IsNullOrEmpty(b.message))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<Model.Company.Structure.RuleMessage> request = new PostRequest<Model.Company.Structure.RuleMessage>();

                    request.entity = b;
                    request.entity.ruleId = currentRuId.Text;
                    PostResponse<Model.Company.Structure.RuleMessage> r = _companyStructureService.ChildAddOrUpdate<Model.Company.Structure.RuleMessage>(request);


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
                        FillMessageStore();

                        this.messageWindow.Close();
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
                    PostRequest<Model.Company.Structure.RuleMessage> request = new PostRequest<Model.Company.Structure.RuleMessage>();
                    request.entity = b;
                    request.entity.ruleId = currentRuId.Text;

                    PostResponse<Model.Company.Structure.RuleMessage> r = _companyStructureService.ChildAddOrUpdate<Model.Company.Structure.RuleMessage>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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

                        FillMessageStore();
                        this.messageWindow.Close();
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
            Model.Company.Structure.Rule b = JsonConvert.DeserializeObject<Model.Company.Structure.Rule>(obj);

            string id = e.ExtraParams["id"];
            
            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(currentRuId.Text))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<Model.Company.Structure.Rule> request = new PostRequest<Model.Company.Structure.Rule>();

                    request.entity = b;
                    PostResponse<Model.Company.Structure.Rule> r = _companyStructureService.ChildAddOrUpdate<Model.Company.Structure.Rule>(request);


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
                        conditionsGrid.Disabled = false;
                        messagesGrid.Disabled = false;
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
                    PostRequest<Model.Company.Structure.Rule> request = new PostRequest<Model.Company.Structure.Rule>();
                    request.entity = b;
                    request.entity.recordId = currentRuId.Text;
                    PostResponse<Model.Company.Structure.Rule> r = _companyStructureService.ChildAddOrUpdate<Model.Company.Structure.Rule>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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
                        currentRuId.Text = "";

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

        protected void Unnamed_Event()
        {

        }
    }
}