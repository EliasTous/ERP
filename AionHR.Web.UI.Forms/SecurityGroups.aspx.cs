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
using AionHR.Model.Employees.Profile;
using AionHR.Model.Employees.Leaves;
using AionHR.Model.Attendance;
using AionHR.Model.TimeAttendance;
using AionHR.Services.Messaging.Reports;
using AionHR.Model.Reports;
using AionHR.Model.Access_Control;
using AionHR.Model.System;
using AionHR.Services.Messaging.System;

namespace AionHR.Web.UI.Forms
{
    public partial class SecurityGroups : System.Web.UI.Page
    {
        //ITimeAttendanceService _branchService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        //IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
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


                groupsStore.Reload();
                userSelector.ButtonsText = new ItemSelectorButtonsText();
                userSelector.ButtonsText.Add = GetLocalResourceObject("Add").ToString();
                userSelector.ButtonsText.Remove = GetLocalResourceObject("Remove").ToString();
                userSelector.Buttons = new ItemSelectorButton[2];
                userSelector.Buttons[0] = ItemSelectorButton.Add;
                userSelector.Buttons[1] = ItemSelectorButton.Remove;

            }


        }

        protected void Page_Init(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// the detailed tabs for the edit form. I put two tabs by default so hide unecessary or add addional
        /// </summary>
        private void HideShowTabs()
        {
            //this.OtherInfoTab.Visible = false;
        }

        protected void SaveGroup(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];
            SecurityGroup b = JsonConvert.DeserializeObject<SecurityGroup>(obj);

            b.recordId = id;
            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<SecurityGroup> request = new PostRequest<SecurityGroup>();
                    request.entity = b;
                    PostResponse<SecurityGroup> r = _accessControlService.ChildAddOrUpdate<SecurityGroup>(request);
                    b.recordId = r.recordId;

                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, r.Summary).Show();
                        return;
                    }
                    else
                    {

                        //Add this record to the store 
                        this.groupsStore.Insert(0, b);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                       
                        RowSelectionModel sm = this.groupsGrid.GetSelectionModel() as RowSelectionModel;
                        sm.DeselectAll();
                        sm.Select(b.recordId.ToString());
                        SetTabPanelActivated(true);
                        CurrentGroup.Text = b.recordId;

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
                    int index = Convert.ToInt32(id);//getting the id of the record
                    PostRequest<SecurityGroup> request = new PostRequest<SecurityGroup>();
                    request.entity = b;
                    PostResponse<SecurityGroup> r = _accessControlService.ChildAddOrUpdate<SecurityGroup>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                    //Step 2 : saving to store

                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                        return;
                    }
                    else
                    {


                        ModelProxy record = this.groupsStore.GetById(index);
                        GroupForm.UpdateRecord(record);
                        record.Commit();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });
                        this.GroupWindow.Close();


                    }

                }
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }
            }
        }

        protected void SaveGroupUsers(object sender, DirectEventArgs e)
        {
            

            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];
            
            List<SecurityGroupUser> selectedUsers = new List<SecurityGroupUser>();
            foreach (var item in userSelector.SelectedItems)
            {
                selectedUsers.Add(new SecurityGroupUser() { userId = item.Value, fullName = item.Text, sgId=CurrentGroup.Text });
            }

            PostRequest<SecurityGroupUser> req = new PostRequest<SecurityGroupUser>();
            PostResponse<SecurityGroupUser> resp = new PostResponse<SecurityGroupUser>();
            req.entity = new SecurityGroupUser() { userId = "0", sgId = CurrentGroup.Text };
            resp = _accessControlService.ChildDelete<SecurityGroupUser>(req);
            if(!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return;
            }
            foreach (var item in selectedUsers)
            {
                req.entity = item;
                req.entity.sgId = CurrentGroup.Text;
                resp = _accessControlService.ChildAddOrUpdate<SecurityGroupUser>(req);
                if (!resp.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                    return;
                }

            }
            Notification.Show(new NotificationConfig
            {
                Title = Resources.Common.Notification,
                Icon = Icon.Information,
                Html = Resources.Common.RecordSavingSucc
            });
            groupUsersWindow.Close();
            usersStore.Reload();
            
          
        }

        private void SetTabPanelActivated(bool isActive)
        {
            foreach (var item in panelRecordDetails.Items)
            {
                if (item.ID == "GroupForm")
                    continue;
                item.Disabled = !isActive;
            }
        }

        protected void ADDNewGroup(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            GroupForm.Reset();
            this.GroupWindow.Title = Resources.Common.AddNewRecord;
            usersStore.DataSource = new List<SecurityGroupUser>();
            usersStore.DataBind();
            SetTabPanelActivated(false);
            this.GroupWindow.Show();
        }

        protected void ADDUsers(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            
        

            UsersListRequest req = new UsersListRequest();
            req.Size = "100";
            req.StartAt = "1";
            req.Filter = "";

            var s = jobInfo1.GetJobInfo();
            req.DepartmentId = s.DepartmentId.HasValue ? s.DepartmentId.ToString() : "0";
            req.PositionId = s.PositionId.HasValue ? s.PositionId.ToString() : "0";
            ListResponse<UserInfo> groups = _systemService.ChildGetAll<UserInfo>(req);
            if (!groups.Success)
            {
                X.Msg.Alert(Resources.Common.Error, groups.Summary).Show();
                return;
            }
            List<SecurityGroupUser> all = new List<SecurityGroupUser>();
            groups.Items.ForEach(x => all.Add(new SecurityGroupUser() { fullName = x.fullName, userId = x.recordId }));
            
            userSelectorStore.DataSource = all;
            userSelectorStore.DataBind();
            
            this.groupUsersWindow.Show();
            X.Call("show");
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

            }
        }


        protected void Prev_Click(object sender, DirectEventArgs e)
        {
            int index = int.Parse(e.ExtraParams["index"]);

            if ((index - 1) >= 0)
            {
                this.Viewport1.ActiveIndex = index - 1;
                groupsStore.Reload();
            }


        }
        protected void PoPuPGroup(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
            CurrentGroup.Text = id.ToString();
            usersStore.Reload();
            switch (type)
            {


                case "imgAttach":
                    Viewport1.ActiveIndex = 1;
                    
                    
                    
                    break;


                case "imgEdit":
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id.ToString();
                    RecordResponse<SecurityGroup> response = _accessControlService.ChildGetRecord<SecurityGroup>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, response.Summary).Show();
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.GroupForm.SetValues(response.result);

                    this.GroupWindow.Title = Resources.Common.EditWindowsTitle;
                    this.GroupWindow.Show();
                    SetTabPanelActivated(true);
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteGroup({0})", id),
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

        protected void PoPuPUser(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
            switch (type)
            {


              

               

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteUser({0})", id),
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
        public void DeleteGroup(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                SecurityGroup n = new SecurityGroup();
                n.recordId = index;
                n.name = "";
                


                PostRequest<SecurityGroup> req = new PostRequest<SecurityGroup>();
                req.entity = n;
                PostResponse<SecurityGroup> res = _accessControlService.ChildDelete<SecurityGroup>(req);
                if (!res.Success)
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, res.Summary).Show();
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    groupsStore.Remove(index);

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
        public void DeleteUser(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                SecurityGroupUser  n = new SecurityGroupUser();
                n.userId = index;
                n.sgId = CurrentGroup.Text;



                PostRequest<SecurityGroupUser> req = new PostRequest<SecurityGroupUser>();
                req.entity = n;
                PostResponse<SecurityGroupUser> res = _accessControlService.ChildDelete<SecurityGroupUser>(req);
                if (!res.Success)
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, res.Summary).Show();
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    usersStore.Remove(index);

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

        protected void groupsStore_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
           

            ListRequest req = new ListRequest();
            req.Size = "100";
            req.StartAt = "1";
            req.Filter = "";
                


            

            //Fetching the corresponding list

            //in this test will take a list of News


            ListResponse<SecurityGroup> groups = _accessControlService.ChildGetAll<SecurityGroup>(req);
            if (!groups.Success)
            {
                X.Msg.Alert(Resources.Common.Error, groups.Summary).Show();
                return;
            }
            this.groupsStore.DataSource = groups.Items;
            e.Total = groups.count;

            this.groupsStore.DataBind();
        }

        protected void usersStore_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;


            GroupUsersListRequest req = new GroupUsersListRequest();
            req.Size = "100";
            req.StartAt = "1";
            req.Filter = "";
            req.GroupId = CurrentGroup.Text;




            //Fetching the corresponding list

            //in this test will take a list of News


            ListResponse<SecurityGroupUser> groups = _accessControlService.ChildGetAll<SecurityGroupUser>(req);
            if (!groups.Success)
            {
                X.Msg.Alert(Resources.Common.Error, groups.Summary).Show();
                return;
            }
            this.usersStore.DataSource = groups.Items;
            e.Total = groups.count;

            this.usersStore.DataBind();
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

        protected void userSelectorStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            UsersListRequest req = new UsersListRequest();
            req.Size = "100";
            req.StartAt = "1";
            req.Filter = "";

            var s = jobInfo1.GetJobInfo();
            req.DepartmentId = s.DepartmentId.HasValue ? s.DepartmentId.ToString() : "0";
            req.PositionId = s.PositionId.HasValue ? s.PositionId.ToString() : "0";
            ListResponse<UserInfo> groups = _systemService.ChildGetAll<UserInfo>(req);
            if (!groups.Success)
            {
                X.Msg.Alert(Resources.Common.Error, groups.Summary).Show();
                return;
            }
            List<SecurityGroupUser> all = new List<SecurityGroupUser>();
            groups.Items.ForEach(x => all.Add(new SecurityGroupUser() { fullName = x.fullName, userId = x.recordId }));

            X.Call("AddSource", all);
            
        }
        [DirectMethod]
        public void GetFilteredUsers()
        {
            UsersListRequest req = new UsersListRequest();
            req.Size = "100";
            req.StartAt = "1";
            req.Filter = "";

            var s = jobInfo1.GetJobInfo();
            req.DepartmentId = s.DepartmentId.HasValue ? s.DepartmentId.ToString() : "0";
            req.PositionId = s.PositionId.HasValue ? s.PositionId.ToString() : "0";
            ListResponse<UserInfo> groups = _systemService.ChildGetAll<UserInfo>(req);
            if (!groups.Success)
            {
                X.Msg.Alert(Resources.Common.Error, groups.Summary).Show();
                return;
            }
            List<SecurityGroupUser> all = new List<SecurityGroupUser>();
            groups.Items.ForEach(x => all.Add(new SecurityGroupUser() { fullName = x.fullName, userId = x.recordId }));

            X.Call("AddSource", all);

        }
    }
}