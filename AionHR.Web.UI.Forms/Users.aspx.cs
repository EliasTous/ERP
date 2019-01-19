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
using Ext.Net.Utilities;
using Newtonsoft.Json;
using AionHR.Services.Interfaces;
using Microsoft.Practices.ServiceLocation;
using AionHR.Web.UI.Forms.Utilities;
using AionHR.Model.Company.News;
using AionHR.Services.Messaging;
using AionHR.Model.Company.Structure;
using AionHR.Model.Employees.Profile;
using AionHR.Model.System;
using AionHR.Services.Messaging.System;
using AionHR.Infrastructure.Domain;
using AionHR.Model.Access_Control;
using AionHR.Infrastructure;

namespace AionHR.Web.UI.Forms
{
    public partial class Users : System.Web.UI.Page
    {

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();
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

                this.rtl.Text = _systemService.SessionHelper.CheckIfArabicSession() ? "True" : "False";
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(UserInfo), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
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
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(SecurityGroupUser), null, groupsGrid, addToGroupButton, addToGroupButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
                }

                //userSelector.ButtonsText = new ItemSelectorButtonsText();
                //userSelector.ButtonsText.Add = GetLocalResourceObject("Add").ToString();
                //userSelector.ButtonsText.Remove = GetLocalResourceObject("Remove").ToString();
                //userSelector.Buttons = new ItemSelectorButton[2];
                //userSelector.Buttons[0] = ItemSelectorButton.Add;
                //userSelector.Buttons[1] = ItemSelectorButton.Remove;
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


        private void SetExtLanguage()
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            if (rtl)
            {
                this.ResourceManager1.RTL = true;
                this.Viewport1.RTL = true;
                this.rtl.Text = "1";
                isRTL.Text = "1";
            }
            else
            {
                this.rtl.Text = "0";

            }
        }


        private void DeactivatePassword(bool deactivate)
        {
            PasswordField.Hidden = deactivate;
            PasswordConfirmation.Hidden = deactivate;
            PasswordField.AllowBlank = deactivate;
            PasswordConfirmation.AllowBlank = deactivate;
        }
        protected void PoPuP(object sender, DirectEventArgs e)
        {
            panelRecordDetails.ActiveIndex = 0;
            DeactivatePassword(true);
            userGroups.Disabled = false;
            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
            CurrentUser.Text = id.ToString();
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id.ToString();

                    RecordResponse<UserInfo> response = _systemService.ChildGetRecord<UserInfo>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                         Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object

                    PasswordConfirmation.Text = response.result.password;
                    this.BasicInfoTab.SetValues(response.result);



                    if (!String.IsNullOrEmpty(response.result.employeeId))
                    {

                        RecordRequest empRecord = new RecordRequest();
                        empRecord.RecordID = response.result.employeeId;
                        RecordResponse<Employee> empResponse = _employeeService.Get<Employee>(empRecord);

                        RecordRequest req = new RecordRequest();

                        employeeId.GetStore().Add(new object[]
                           {
                                new
                                {
                                    recordId = response.result.employeeId,
                                    fullName =empResponse.result.name.fullName
                                }
                           });
                        employeeId.SetValue(response.result.employeeId);

                        X.Call("SetNameEnabled", false, empResponse.result.name.fullName);
                    }
                    pro.Hidden = true;

                    // InitCombos(response.result);
                    this.EditRecordWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditRecordWindow.Show();


                    AllGroupsStore.Reload();
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

        protected void PoPuPGroup(object sender, DirectEventArgs e)
        {

            
            string id =e.ExtraParams["id"];
            string type = e.ExtraParams["type"];
            switch (type)
            {

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.LeaveGroup({0})", id),
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
                UserInfo s = new UserInfo();
                s.recordId = index;
                s.accountId = "";
                s.companyName = "";
                s.email = "";
                s.fullName = "";
                s.languageId = 0;

                PostRequest<UserInfo> req = new PostRequest<UserInfo>();
                req.entity = s;
                PostResponse<UserInfo> r = _systemService.ChildDelete<UserInfo>(req);
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
        public void LeaveGroup(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                SecurityGroupUser user = new SecurityGroupUser();
                user.userId = CurrentUser.Text;
                user.sgId = index;
                PostRequest<SecurityGroupUser> req = new PostRequest<SecurityGroupUser>();
                req.entity = user;
                PostResponse<SecurityGroupUser> r = _accessControlService.ChildDelete<SecurityGroupUser>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                     Common.errorMessage(r);
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    UserGroupsStore.Reload();
                    AllGroupsStore.Reload();

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
        public object FillParent(string action, Dictionary<string, object> extraParams)
        {
            StoreRequestParameters prms = new StoreRequestParameters(extraParams);



            List<UserInfo> data;
            UsersListRequest req = new UsersListRequest();
            req.Size = "1000";
            req.StartAt = "1";
            req.Filter = "";


            req.DepartmentId = "0";
            req.PositionId = "0";
            req.BranchId = "0";


            ListResponse<UserInfo> response = _systemService.ChildGetAll<UserInfo>(req);
            data = response.Items;
            return new
            {
                data
            };

        }
        [DirectMethod]
        public object FillSupervisor(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);



            List<Employee> data = GetEmployeesFiltered(prms.Query);

            data.ForEach(s => s.fullName = s.name.fullName);
            //  return new
            // {

            if (data.Count == 0)
            {
                X.Call("SetNameEnabled", true, " ");
            }

            return data;
            //};

        }

        private List<Employee> GetEmployeeByID(string id)
        {

            RecordRequest req = new RecordRequest();
            req.RecordID = id;



            List<Employee> emps = new List<Employee>();
            RecordResponse<Employee> emp = _employeeService.Get<Employee>(req);
            if (!emp.Success)
                X.Msg.Alert(Resources.Common.Error,GetGlobalResourceObject("Errors", emp.ErrorCode) != null ? GetGlobalResourceObject("Errors", emp.ErrorCode).ToString()+ "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") +emp.LogId : emp.Summary).Show();
            emps.Add(emp.result);
            return emps;
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
            CurrentUser.Text = "";
            panelRecordDetails.ActiveIndex = 0;
            //Reset all values of the relative object
            BasicInfoTab.Reset();
            fullName.Disabled = false;

            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;
            DeactivatePassword(false);
            this.EditRecordWindow.Show();
            userGroups.Disabled = true;
            pro.Hidden = false;
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
            request.StartAt = e.Start.ToString();
            request.Size = "30";
            var s = jobInfo1.GetJobInfo();
            UsersListRequest req = new UsersListRequest();
            req.Filter = searchTrigger.Text;
            req.StartAt = e.Start.ToString();
            req.Size = "30";
            req.DepartmentId = s.DepartmentId.HasValue ? s.DepartmentId.Value.ToString() : "0";
            req.PositionId = s.PositionId.HasValue ? s.PositionId.Value.ToString() : "0";
            req.BranchId = s.BranchId.HasValue ? s.BranchId.Value.ToString() : "0";

            req.SortBy = "employeeId";
            ListResponse<UserInfo> branches = _systemService.ChildGetAll<UserInfo>(req);
            if (!branches.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", branches.ErrorCode) != null ? GetGlobalResourceObject("Errors", branches.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") +branches.LogId: branches.Summary).Show();
                return;
            }
            branches.Items.ForEach(x =>
            {
                switch (x.userType)
                {
                    case 1:x.userTypeString = GetLocalResourceObject("FieldSuperUser").ToString();
                        break;
                    case 2:x.userTypeString = GetLocalResourceObject("FieldAdministrator").ToString();
                        break;
                    case 3:x.userTypeString = GetLocalResourceObject("FieldOperator").ToString();
                        break;
                    case 4:x.userTypeString = GetLocalResourceObject("FieldSelfService").ToString();
                        break;
                    default:
                        break;


                }
            }

            );
            this.Store1.DataSource = branches.Items;
            e.Total = branches.count;

            this.Store1.DataBind();
        }



        [DirectMethod]
        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];
            UserInfo b = JsonConvert.DeserializeObject<UserInfo>(obj);

            b.recordId = id;
            // Define the object to add or edit as null
            if (employeeId.SelectedItem != null && employeeId.SelectedItem.Value != null)
            {
                b.employeeId = employeeId.SelectedItem.Value;
                b.fullName = employeeId.SelectedItem.Text;
            }
           
            if (string.IsNullOrEmpty(CurrentUser.Text))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<UserInfo> request = new PostRequest<UserInfo>();
                    b.password = EncryptionHelper.encrypt(b.password);
                    request.entity = b;
                    PostResponse<UserInfo> r = _systemService.ChildAddOrUpdate<UserInfo>(request);
                   
                    if (!r.Success)
                    //check if the insert failed
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                         Common.errorMessage(r);
                        return;
                    }
                    b.recordId = r.recordId;
                    //elias
                    AccountRecoveryRequest req = new AccountRecoveryRequest();
                    req.Email = b.email;
                    //PasswordRecoveryResponse response = _systemService.RequestPasswordRecovery(req);
                    //if (!response.Success)
                  
                    //{
                    //    //Show an error saving...
                    //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    //     Common.errorMessage(response);
                    //    return;
                    //}
                    //else
                    //{

                        //Add this record to the store 
                        Store1.Reload();
                        CurrentUser.Text = r.recordId;
                        userGroups.Disabled = false;
                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        //this.EditRecordWindow.Close();
                        //RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;
                        //sm.DeselectAll();
                        //sm.Select(b.recordId.ToString());



                    
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
                    int index = Convert.ToInt32(id);//getting the id of the record
                    PostRequest<UserInfo> request = new PostRequest<UserInfo>();



                    request.entity = b;
                    PostResponse<UserInfo> r = _systemService.ChildAddOrUpdate<UserInfo>(request);                   //Step 1 Selecting the object or building up the object for update purpose

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

        [DirectMethod]
        public void SetFullName()
        {
            X.Call("SetNameEnabled", false, employeeId.SelectedItem.Text);
        }


        protected void AllGroupsStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            ListRequest groupsReq = new ListRequest();
            groupsReq.Size = "100";
            groupsReq.StartAt = "1";
            groupsReq.Filter = "";

            //Fetching the corresponding list

            //in this test will take a list of News


            ListResponse<SecurityGroup> groups = _accessControlService.ChildGetAll<SecurityGroup>(groupsReq);
            if (!groups.Success)
            {
                X.Msg.Alert(Resources.Common.Error,GetGlobalResourceObject("Errors", groups.ErrorCode) != null ? GetGlobalResourceObject("Errors", groups.ErrorCode).ToString()+ "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") +groups.LogId : groups.Summary).Show();
                return;
            }

            GroupUsersListRequest request = new GroupUsersListRequest();
            request.UserId = CurrentUser.Text;
            ListResponse<SecurityGroupUser> userGroups = _accessControlService.ChildGetAll<SecurityGroupUser>(request);
            if (!groups.Success)
            {
                X.Msg.Alert(Resources.Common.Error,GetGlobalResourceObject("Errors", groups.ErrorCode) != null ? GetGlobalResourceObject("Errors", groups.ErrorCode).ToString() : groups.Summary).Show();
                return;
            }
            UserGroupsStore.DataSource = userGroups.Items;
            UserGroupsStore.DataBind();

            List<SecurityGroup> availableGroups = new List<SecurityGroup>();
            groups.Items.ForEach(x => { if (userGroups.Items.Where(y => y.sgId == x.recordId).Count() == 0) availableGroups.Add(x); });

            AllGroupsStore.DataSource = availableGroups;
            AllGroupsStore.DataBind();
            GroupsCombo.Select(0);
        }

        protected void addUserToGroup(object sender, DirectEventArgs e)
        {
            PostRequest<SecurityGroupUser> user = new PostRequest<SecurityGroupUser>();
            SecurityGroupUser en = new SecurityGroupUser();
            en.userId = CurrentUser.Text;
            if (GroupsCombo.SelectedItem == null)
            {
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorSavingRecord).Show();
                return;
            }
            en.sgId = GroupsCombo.SelectedItem.Value;
            user.entity = en;
            PostResponse<SecurityGroupUser> resp = _accessControlService.ChildAddOrUpdate<SecurityGroupUser>(user);
            if(!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }

            AllGroupsStore.Reload();
        }

        protected void groupSelectorGroup_ReadData(object sender, StoreReadDataEventArgs e)
        {
            
            
        }

        protected void UserGroupsStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            string filter = string.Empty;


            GroupUsersListRequest req = new GroupUsersListRequest();
            req.Size = "100";
            req.StartAt = "1";
            req.Filter = "";
            req.UserId = CurrentUser.Text;




            //Fetching the corresponding list

            //in this test will take a list of News


            ListResponse<SecurityGroupUser> groups = _accessControlService.ChildGetAll<SecurityGroupUser>(req);
            if (!groups.Success)
            {
                X.Msg.Alert(Resources.Common.Error, groups.Summary).Show();
                return;
            }
            this.UserGroupsStore.DataSource = groups.Items;
            e.Total = groups.count;

            this.UserGroupsStore.DataBind();
        }

        protected void SaveGroupUsers(object sender, DirectEventArgs e)
        {
            try
            {

                //Getting the id to check if it is an Add or an edit as they are managed within the same form.
                string id = e.ExtraParams["id"];

                List<SecurityGroupUser> selectedUsers = new List<SecurityGroupUser>();
                foreach (var item in userSelector.SelectedItems)
                {
                    selectedUsers.Add(new SecurityGroupUser() { userId = CurrentUser.Text, sgName = item.Text, sgId = item.Value });
                }



            
                GroupUsersListRequest request = new GroupUsersListRequest();
                request.UserId = CurrentUser.Text;
                ListResponse<SecurityGroupUser> userGroups = _accessControlService.ChildGetAll<SecurityGroupUser>(request);
                if (!userGroups.Success)
                {
                    Common.errorMessage(userGroups);
                    return;
                }

                PostRequest<SecurityGroupUser> req = new PostRequest<SecurityGroupUser>();
                PostResponse<SecurityGroupUser> resp = new PostResponse<SecurityGroupUser>();
                userGroups.Items.ForEach(x =>
                {
                    req.entity = x;
                    resp = _accessControlService.ChildDelete<SecurityGroupUser>(req);
                    if (!resp.Success)
                    {
                        Common.errorMessage(resp);
                        throw new Exception();
                    }

                });


                //req.entity = new SecurityGroupUser() { sgId = "0", userId = CurrentUser.Text };
                //resp = _accessControlService.ChildDelete<SecurityGroupUser>(req);
              
              
                foreach (var item in selectedUsers)
                {
                    req.entity = item;
                    req.entity.userId = CurrentUser.Text;
                    resp = _accessControlService.ChildAddOrUpdate<SecurityGroupUser>(req);
                    if (!resp.Success)
                    {
                        Common.errorMessage(resp);
                        throw new Exception();
                    }

                }
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordSavingSucc
                });
                groupUsersWindow.Close();
                UserGroupsStore.Reload();
            }
            catch(Exception exp)
            {
                X.MessageBox.Alert(GetGlobalResourceObject("Common", "Error").ToString(), exp.Message);
            }

        }

        protected void ADDGroups(object sender, DirectEventArgs e)
        {

            GroupUsersListRequest request = new GroupUsersListRequest();
            request.UserId = CurrentUser.Text;
            ListResponse<SecurityGroup> userGroups = _accessControlService.ChildGetAll<SecurityGroup>(request);
            if (!userGroups.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", userGroups.ErrorCode) != null ? GetGlobalResourceObject("Errors", userGroups.ErrorCode).ToString()+ "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") +userGroups.LogId : userGroups.Summary).Show();
                return;
            }
            List<SecurityGroupUser> list = new List<SecurityGroupUser>();
            userGroups.Items.ForEach(x => { list.Add(new SecurityGroupUser() { sgName = x.name, sgId = x.recordId, userId = CurrentUser.Text }); });
            groupSelectorGroup.DataSource = list;
            groupSelectorGroup.DataBind();
            this.groupUsersWindow.Show();
            X.Call("show");
        }
    }
}