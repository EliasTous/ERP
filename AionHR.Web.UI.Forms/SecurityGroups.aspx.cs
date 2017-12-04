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
using AionHR.Model.Employees.Profile;
using AionHR.Model.Employees.Leaves;
using AionHR.Model.Attendance;
using AionHR.Model.TimeAttendance;
using AionHR.Services.Messaging.Reports;
using AionHR.Model.Reports;
using AionHR.Model.Access_Control;
using AionHR.Model.System;
using AionHR.Services.Messaging.System;
using AionHR.Infrastructure.Domain;
using System.Reflection;
using Newtonsoft.Json.Linq;
using AionHR.Model.Attributes;

namespace AionHR.Web.UI.Forms
{
    public partial class SecurityGroups : System.Web.UI.Page
    {
        //ITimeAttendanceService _branchService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        //IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();
        IMasterService _masterService = ServiceLocator.Current.GetInstance<IMasterService>();
        ICompanyStructureService _company = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
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

                if ((bool)_systemService.SessionHelper.Get("IsAdmin"))
                    return;
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(SecurityGroup), GroupForm, groupsGrid, groupAddButton, SaveGroupButton);
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
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(SecurityGroupUser), null, usersGrid, Button1, null);
                }
                catch (AccessDeniedException exp)
                {

                    usersGrid.Hidden = true;

                }
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(ModuleClass), EditClassLevelForm, classesGrid, null, SaveClassLevelButton);
                }
                catch (AccessDeniedException exp)
                {

                    AccessLevelsForm.Hidden = true;

                }
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(ModuleClass), ApplyModuleLevelWindow, classesGrid, openModuleLevelForm, ApplyModuleButton);
                }
                catch (AccessDeniedException exp)
                {

                    AccessLevelsForm.Hidden = true;

                }
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(ClassProperty), EditClassPropertiesForm, propertiesGrid, null, Button6);
                }
                catch (AccessDeniedException exp)
                {

                    var c = classesGrid.ColumnModel.Columns[classesGrid.ColumnModel.Columns.Count - 1];
                    c.Renderer.Handler = c.Renderer.Handler.Replace("propertiesRender()", "' '");
                    return;
                }
                try
                {
                    var properties = AccessControlApplier.GetPropertiesLevels(typeof(ClassProperty));

                    var result = propertiesGrid.ColumnModel.Columns[propertiesGrid.ColumnModel.Columns.Count - 1];
                    var item = properties.Where(x => x.index == "accessLevel").ToList()[0];
                    switch (item.accessLevel)
                    {
                        case 0:

                            ((result as WidgetColumn).Widget[0] as Field).InputType = InputType.Password;
                            (result as WidgetColumn).Widget[0].Disabled = true;

                            break;
                        case 1:



                            (result as WidgetColumn).Widget[0].Disabled = true;

                            break;
                        default: break;

                    }
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
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>Technical Error: "+r.ErrorCode + "<br> Summary: "+r.Summary : r.Summary).Show();
                        return;
                    }
                    else
                    {

                        //Add this record to the store 
                        this.groupsStore.Insert(0, b);
                     
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
                        recordId.Text = b.recordId;
                        this.GroupWindow.Title = b.name;
                        modulesCombo.Select(0);
                        classesStore.Reload();
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
                selectedUsers.Add(new SecurityGroupUser() { userId = item.Value, fullName = item.Text, sgId = CurrentGroup.Text });
            }

            PostRequest<SecurityGroupUser> req = new PostRequest<SecurityGroupUser>();
            PostResponse<SecurityGroupUser> resp = new PostResponse<SecurityGroupUser>();
            req.entity = new SecurityGroupUser() { userId = "0", sgId = CurrentGroup.Text };
            resp = _accessControlService.ChildDelete<SecurityGroupUser>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
                return;
            }
            foreach (var item in selectedUsers)
            {
                req.entity = item;
                req.entity.sgId = CurrentGroup.Text;
                resp = _accessControlService.ChildAddOrUpdate<SecurityGroupUser>(req);
                if (!resp.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
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

        protected void SaveClassLevel(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string classId = e.ExtraParams["classId"];

            ModuleClass m = JsonConvert.DeserializeObject<ModuleClass>(e.ExtraParams["values"]);

            PostRequest<ModuleClass> req = new PostRequest<ModuleClass>();
            req.entity = m;
            m.classId = CurrentClass.Text; ;
            m.sgId = CurrentGroup.Text;
            PostResponse<ModuleClass> resp = _accessControlService.ChildAddOrUpdate<ModuleClass>(req);

            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
                return;
            }



            Notification.Show(new NotificationConfig
            {
                Title = Resources.Common.Notification,
                Icon = Icon.Information,
                Html = Resources.Common.RecordSavingSucc
            });
            classesStore.Reload();
            EditClassLevelWindow.Close();

        }

        protected void SaveModuleLevel(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string moduleId = modulesCombo.SelectedItem.Value;
            List<Type> types = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("AionHR.Model")).ToList()[0].GetTypes().Where(x => { var d = x.GetCustomAttribute<ClassIdentifier>(); if (d != null && d.ModuleId == moduleId) return true; else return false; }).ToList();

            int level = Convert.ToInt32(moduleAccessLevel.SelectedItem.Value);
            PostRequest<ModuleClass[]> batch = new PostRequest<ModuleClass[]>();
            List<ModuleClass> allClasses = new List<ModuleClass>();
            types.ForEach(x => { allClasses.Add(new ModuleClass() { classId = x.GetCustomAttribute<ClassIdentifier>().ClassID, accessLevel = level, id = x.GetCustomAttribute<ClassIdentifier>().ClassID, sgId = CurrentGroup.Text }); });
            allClasses.Where(x => x.classId.StartsWith("80") || x.classId.EndsWith("99")).ToList().ForEach(y => y.accessLevel = Math.Min(1, y.accessLevel));
            batch.entity = allClasses.ToArray();
            PostResponse<ModuleClass[]> batResp = _accessControlService.ChildAddOrUpdate<ModuleClass[]>(batch);
            if (!batResp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, batResp.Summary).Show();
                return;
            }





            Notification.Show(new NotificationConfig
            {
                Title = Resources.Common.Notification,
                Icon = Icon.Information,
                Html = Resources.Common.RecordSavingSucc
            });
            classesStore.Reload();
            ApplyModuleLevelWindow.Close();

        }

        protected void SaveClassProperties(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string classId = e.ExtraParams["classId"];

            List<ClassProperty> properties = JsonConvert.DeserializeObject<List<ClassProperty>>(e.ExtraParams["values"]);
            PostRequest<ClassProperty> req = new PostRequest<ClassProperty>();
            PostResponse<ClassProperty> resp = null;
            foreach (var item in properties)
            {
                item.classId = CurrentClass.Text;
                item.sgId = CurrentGroup.Text;
                req.entity = item;
                resp = _accessControlService.ChildAddOrUpdate<ClassProperty>(req);
                if (!resp.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();
                    return;
                }
            }





            Notification.Show(new NotificationConfig
            {
                Title = Resources.Common.Notification,
                Icon = Icon.Information,
                Html = Resources.Common.RecordSavingSucc
            });
            classesStore.Reload();
            EditClassPropertiesWindow.Close();

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
            panelRecordDetails.ActiveIndex = 0;

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
                isRTL.Text = "1";

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
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).   ToString() + "<br>Technical Error: " + response.ErrorCode + "<br> Summary: " + response.Summary : response.Summary).Show();
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.GroupForm.SetValues(response.result);

                    this.GroupWindow.Title = response.result.name;
                    this.GroupWindow.Show();
                    SetTabPanelActivated(true);
                    panelRecordDetails.ActiveIndex = 0;
                    modulesCombo.Select(0);
                    classesStore.Reload();
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

        protected void PoPuPClass(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
            string level = e.ExtraParams["accessLevel"];
            CurrentClass.Text = id.ToString();
            CurrentClassLevel.Text = level;
            switch (type)
            {


                case "imgAttach":

                    PostRequest<ModuleClass> req = new PostRequest<ModuleClass>();
                    req.entity = new ModuleClass() { accessLevel = Convert.ToInt32(level), classId = id.ToString(), sgId = CurrentGroup.Text, id = id.ToString() };

                    PostResponse<ModuleClass> resp = _accessControlService.ChildAddOrUpdate<ModuleClass>(req);

                    List<PropertyAccessLevel> levels = new List<PropertyAccessLevel>();
                    levels.Add(new PropertyAccessLevel(GetLocalResourceObject("NoAccess").ToString(), "0"));
                    levels.Add(new PropertyAccessLevel(GetLocalResourceObject("Read").ToString(), "1"));
                    if (level == "2" || level == "3")
                        levels.Add(new PropertyAccessLevel(GetLocalResourceObject("WriteProperty").ToString(), "2"));

                    accessLevelsStore.DataSource = levels;
                    accessLevelsStore.DataBind();

                    EditClassPropertiesWindow.Show();
                    propertiesStore.Reload();



                    break;


                case "imgEdit":
                    List<PropertyAccessLevel> masterLevels = new List<PropertyAccessLevel>();
                    masterLevels.Add(new PropertyAccessLevel(GetLocalResourceObject("NoAccess").ToString(), "0"));
                    masterLevels.Add(new PropertyAccessLevel(GetLocalResourceObject("Read").ToString(), "1"));
                    int maxLevel = 1;
                    if (modulesCombo.SelectedItem.Value != "80" && !id.ToString().EndsWith("99") && !id.ToString().EndsWith("98") && !id.ToString().EndsWith("97") && !id.ToString().EndsWith("96") && !id.ToString().EndsWith("95"))
                    {

                        masterLevels.Add(new PropertyAccessLevel(GetLocalResourceObject("WriteClass").ToString(), "2"));
                        masterLevels.Add(new PropertyAccessLevel(GetLocalResourceObject("FullControl").ToString(), "3"));
                        maxLevel = 3;
                    }
                    classAccessLevelsStore.DataSource = masterLevels;
                    classAccessLevelsStore.DataBind();
                    EditClassLevelForm.Reset();
                    int levelInt = Convert.ToInt32(level);
                    accessLevel.Select(Math.Min(levelInt, maxLevel).ToString());


                    EditClassLevelWindow.Show();
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
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", res.ErrorCode) != null ? GetGlobalResourceObject("Errors", res.ErrorCode).ToString() : res.Summary).Show();
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
                SecurityGroupUser n = new SecurityGroupUser();
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

        protected void modulesStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            //List<Type> types = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("AionHR.Model")).ToList()[0].GetTypes().ToList();
            //HashSet<string> mods = new HashSet<string>();
            //types.ForEach(x => { var d = x.GetCustomAttribute<ClassIdentifier>(); if(d!= null) mods.Add(x.GetCustomAttribute<ClassIdentifier>().ModuleId); });
            //List<Model.Access_Control.Module> modules = new List<Model.Access_Control.Module>();

            //mods.ToList().ForEach(x => modules.Add(new Model.Access_Control.Module() { id = x, name = GetGlobalResourceObject("Common", "Mod" + x).ToString() }));
            ////preDefined.ForEach(x => x.name = GetGlobalResourceObject("Common", x.name).ToString());
            //modulesStore.DataSource = modules;
            //modulesStore.DataBind();
        }

        protected void classesStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            List<ModuleClassDefinition> classes = new List<ModuleClassDefinition>();
            List<Type> types = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("AionHR.Model")).ToList()[0].GetTypes().Where(x => { var d = x.GetCustomAttribute<ClassIdentifier>(); if (d != null && d.ModuleId == modulesCombo.SelectedItem.Value) return true; else return false; }).ToList();

            types.ForEach(x => classes.Add(new ModuleClassDefinition() { classId = x.GetCustomAttribute<ClassIdentifier>().ClassID, id = x.GetCustomAttribute<ClassIdentifier>().ClassID, name = "Class" + x.GetCustomAttribute<ClassIdentifier>().ClassID }));

            classes.ForEach(x => { x.name = GetGlobalResourceObject("Classes", x.name).ToString(); x.classId = x.id; });
            AccessControlListRequest req = new AccessControlListRequest();
            req.GroupId = CurrentGroup.Text;
            ListResponse<ModuleClass> stored = _accessControlService.ChildGetAll<ModuleClass>(req);
            if (!stored.Success)
            {
                X.Msg.Alert(Resources.Common.Error, stored.Summary).Show();
                return;
            }
            List<ModuleClass> finalClasses = new List<ModuleClass>();
            classes.ForEach(x => { List<ModuleClass> match = stored.Items.Where(y => y.classId == x.classId).ToList(); ModuleClass temp = new ModuleClass(x); if (match.Count > 0) temp.accessLevel = match[0].accessLevel; else temp.accessLevel = 0; finalClasses.Add(temp); });


            classesStore.DataSource = finalClasses;
            classesStore.DataBind();
        }

        protected void propertyStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            Type t = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("AionHR.Model")).ToList()[0].GetTypes().Where(x => { var d = x.GetCustomAttribute<ClassIdentifier>(); return (d != null && d.ClassID == CurrentClass.Text); }).ToList()[0];
            PropertyInfo[] props = t.GetProperties();

            List<ClassPropertyDefinition> properites = new List<ClassPropertyDefinition>();
            props.ToList().ForEach(x => { var d = x.GetCustomAttribute<PropertyID>(); if (d != null) properites.Add(new ClassPropertyDefinition() { propertyId = d.ID, index = x.Name, name = "Property" + d.ID }); });
            PropertiesListRequest req = new PropertiesListRequest();
            req.GroupId = CurrentGroup.Text;
            req.ClassId = CurrentClass.Text;
            ListResponse<ClassProperty> stored = _accessControlService.ChildGetAll<ClassProperty>(req);
            if (!stored.Success)
            {
                X.Msg.Alert(Resources.Common.Error, stored.Summary).Show();
                return;
            }


            List<ClassProperty> final = new List<ClassProperty>();
            foreach (var item in properites)
            {
                if (GetGlobalResourceObject("AccessControl", item.name) == null)
                    continue;
                else
                {
                    item.name = GetGlobalResourceObject("AccessControl", item.name).ToString();
                    final.Add(new ClassProperty() { index = item.index, propertyId = item.propertyId, name = item.name, accessLevel = 2 });
                }
            }
            JsonSerializerSettings settings = new JsonSerializerSettings();

            stored.Items.ForEach(y => { final.Where(f => y.propertyId == f.propertyId).ToList().ForEach(x => x.accessLevel = Math.Min(y.accessLevel, Convert.ToInt32(CurrentClassLevel.Text))); });
            properites.Clear();
            final.ForEach(z => z.accessLevel = Math.Min(z.accessLevel, Convert.ToInt32(CurrentClassLevel.Text)));

            propertiesStore.DataSource = final;
            propertiesStore.DataBind();
        }

        protected void dataStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            string classId = "";
            if (classIdCombo.SelectedItem == null || string.IsNullOrEmpty(classIdCombo.SelectedItem.Value))
                classId = "21010";
            else
                classId = classIdCombo.SelectedItem.Value;
            DataAccessRecordRequest recordReq = new DataAccessRecordRequest();
            recordReq.RecordID = "0";
            recordReq.sgId = CurrentGroup.Text;
            recordReq.classId = classId;
            RecordResponse<DataAccessItemView> recordRes = _accessControlService.ChildGetRecord<DataAccessItemView>(recordReq);
            if (recordRes.result != null)
            {
                superUserCheck.Checked = true;
                dataStore.DataSource = new List<DataAccessItemView>();
                dataStore.DataBind();
            }
            else
            {


                DataAccessListRequest req = new DataAccessListRequest();


                req.classId = classId;
                req.sgId = CurrentGroup.Text;
                ListResponse<DataAccessItemView> stored = _accessControlService.ChildGetAll<DataAccessItemView>(req);
                dataStore.DataSource = stored.Items;
                dataStore.DataBind();
                superUserCheck.Checked = false;
            }


        }


        [DirectMethod]
        public void onChangeActive_Event(string recordId, string isChecked)
        {
            DataAccessItemView item = new DataAccessItemView();
            item.sgId = CurrentGroup.Text;
            item.classId = classIdCombo.SelectedItem.Value;
            item.recordId = recordId;

            PostRequest<DataAccessItemView> req = new PostRequest<DataAccessItemView>();
            req.entity = item;
            PostResponse<DataAccessItemView> resp = null;
            if (isChecked == "true")
            {
                req.entity.hasAccess = true;
                resp = _accessControlService.ChildAddOrUpdate<DataAccessItemView>(req);
            }
            else
            {
                req.entity.hasAccess = false;
                resp = _accessControlService.ChildDelete<DataAccessItemView>(req);
            }

        }

        protected void PropmptSave(object sender, DirectEventArgs e)
        {
            X.Msg.Confirm(GetLocalResourceObject("DaSaveTitle").ToString(), GetLocalResourceObject("DaSaveText").ToString(), new MessageBoxButtonsConfig
            {
                Yes = new MessageBoxButtonConfig
                {
                    //We are call a direct request metho for deleting a record
                    Handler = "App.dataStore.reload()",

                    Text = Resources.Common.Yes
                },
                No = new MessageBoxButtonConfig
                {
                    Handler = "App.classIdCombo.select('" + e.ExtraParams["current"] + "')",
                    Text = Resources.Common.No
                }

            }).Show();
        }
        protected void SaveDA(object sender, DirectEventArgs e)
        {
            List<DataAccessItemView> changed = JsonConvert.DeserializeObject<List<DataAccessItemView>>(e.ExtraParams["values"]);

            PostRequest<DataAccessItemView> req = new PostRequest<DataAccessItemView>();

            changed.ForEach(
                x =>
                {
                    x.classId = classIdCombo.SelectedItem.Value;
                    x.sgId = CurrentGroup.Text;
                    req.entity = x;
                    PostResponse<DataAccessItemView> resp;
                    if (x.hasAccess)
                        resp = _accessControlService.ChildAddOrUpdate<DataAccessItemView>(req);
                    else
                        resp = _accessControlService.ChildDelete<DataAccessItemView>(req);
                });
            Notification.Show(new NotificationConfig
            {
                Title = Resources.Common.Notification,
                Icon = Icon.Information,
                Html = Resources.Common.RecordSavingSucc
            });
            X.Call("clearDirty");
        }

        protected void ToggleSuperuser(object sender, DirectEventArgs e)
        {
            if (superUserCheck.Checked)
            {
                PostRequest<DataAccessItemView> req = new PostRequest<DataAccessItemView>();
                DataAccessItemView x = new DataAccessItemView();
                x.classId = classIdCombo.SelectedItem.Value;
                x.sgId = CurrentGroup.Text;
                x.hasAccess = true;
                x.recordId = "0";
                req.entity = x;
                PostResponse<DataAccessItemView> resp = _accessControlService.ChildAddOrUpdate<DataAccessItemView>(req);
            }
            else
            {
                PostRequest<DataAccessItemView> req = new PostRequest<DataAccessItemView>();
                DataAccessItemView x = new DataAccessItemView();
                x.classId = classIdCombo.SelectedItem.Value;
                x.sgId = CurrentGroup.Text;
                x.hasAccess = false;
                x.recordId = "0";
                req.entity = x;
                PostResponse<DataAccessItemView> resp = _accessControlService.ChildDelete<DataAccessItemView>(req);
            }
            dataStore.Reload();
        }
    }
}