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
using AionHR.Model.Attributes;
using AionHR.Model.Access_Control;

namespace AionHR.Web.UI.Forms
{
    public partial class LeaveSchedules : System.Web.UI.Page
    {
        ILeaveManagementService _branchService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();

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
                HideShowColumns();
                if (_systemService.SessionHelper.CheckIfIsAdmin())
                    return;
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(VacationSchedule), BasicInfoTab, GridPanel1, btnAdd, SaveButton);

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
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(VacationSchedulePeriod), null, periodsGrid, addPeriod, null);

                }
                catch (AccessDeniedException exp)
                {

                    periodsGrid.Hidden = true;
                    
                }
                if ((bool)_systemService.SessionHelper.Get("IsAdmin"))
                    return;

                ApplySecurityOnVacationPeriods();
            }

         
        }

        private void ApplySecurityOnVacationPeriods()
        {
            ClassPermissionRecordRequest classReq = new ClassPermissionRecordRequest();
            classReq.ClassId = (typeof(VacationSchedulePeriod).GetCustomAttributes(typeof(ClassIdentifier), false).ToList()[0] as ClassIdentifier).ClassID;
            classReq.UserId = _systemService.SessionHelper.GetCurrentUserId();
            RecordResponse<ModuleClass> modClass = _accessControlService.ChildGetRecord<ModuleClass>(classReq);
            switch (modClass.result.accessLevel)
            {
                case 1: addPeriod.Disabled = true; editDisabled.Text = "1"; deleteDisabled.Text = "1"; break;
                case 2: addPeriod.Disabled = true; deleteDisabled.Text = "1"; break;
                default: break;
            }
           
            var properties = AccessControlApplier.GetPropertiesLevels(typeof(VacationSchedulePeriod));
            int i = 1;
            foreach (var item in properties)
            {
                if (item.accessLevel < 2 && periodsGrid.ColumnModel.Columns[i].Editor.Count > 0)
                    periodsGrid.ColumnModel.Columns[i].Editor[0].ReadOnly = true;
                if (item.accessLevel < 1 && periodsGrid.ColumnModel.Columns[i].Editor.Count > 0)
                    periodsGrid.ColumnModel.Columns[i].Editor[0].InputType = InputType.Password;

                i++;
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


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    panelRecordDetails.ActiveIndex = 0;
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id.ToString();
                    RecordResponse<LeaveSchedule> response = _branchService.ChildGetRecord<LeaveSchedule>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                         Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.BasicInfoTab.SetValues(response.result);

                    LeaveSchedulesListRequest req = new LeaveSchedulesListRequest();
                    req.LeaveScheduleId = r.RecordID;
                    ListResponse<LeaveSchedulePeriod> periods = _branchService.ChildGetAll<LeaveSchedulePeriod>(req);
                    if (!periods.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", periods.ErrorCode) != null ? GetGlobalResourceObject("Errors", periods.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + periods.LogId : periods.Summary).Show();
                        return;
                    }
                    periodsGrid.Store[0].DataSource = periods.Items;
                    periodsGrid.Store[0].DataBind();
                    periodsGrid.DataBind();
                    // InitCombos(response.result);
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
                LeaveSchedule s = new LeaveSchedule();
                s.recordId = index;
                s.name = "";

                PostRequest<LeaveSchedule> req = new PostRequest<LeaveSchedule>();
                req.entity = s;
                PostResponse<LeaveSchedule> r = _branchService.ChildDelete<LeaveSchedule>(req);
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
        public object FillParent(string action, Dictionary<string, object> extraParams)
        {
            StoreRequestParameters prms = new StoreRequestParameters(extraParams);



            List<VacationSchedule> data;
            ListRequest req = new ListRequest();

            ListResponse<VacationSchedule> response = _branchService.ChildGetAll<VacationSchedule>(req);
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

            //  return new
            // {
            return data;
            //};

        }

        private List<Employee> GetEmployeeByID(string id)
        {

            RecordRequest req = new RecordRequest();
            req.RecordID = id;



            List<Employee> emps = new List<Employee>();
            RecordResponse<Employee> emp = _employeeService.Get<Employee>(req);
            emps.Add(emp.result);
            return emps;
        }
        private List<Employee> GetEmployeesFiltered(string query)
        {
            

            EmployeeListRequest req = new EmployeeListRequest();
            req.DepartmentId = "0";
            req.BranchId = "0";
            req.IncludeIsInactive = 2;
            req.SortBy = "firstName";

            req.StartAt = "0";
            req.Size = "20";
            req.Filter = query;


            ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
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

            //Reset all values of the relative object
            BasicInfoTab.Reset();
            periodsGrid.Store[0].DataSource = new List<LeaveSchedulePeriod>();
            periodsGrid.Store[0].DataBind();
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;

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
            ListResponse<LeaveSchedule> resp = _branchService.ChildGetAll<LeaveSchedule>(request);
            if (!resp.Success)
                Common.errorMessage(resp);
            this.Store1.DataSource = resp.Items;
            e.Total = resp.count;

            this.Store1.DataBind();
        }




        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["schedule"];
            LeaveSchedule b = JsonConvert.DeserializeObject<LeaveSchedule>(obj);
            string pers = e.ExtraParams["periods"];
            b.recordId = id;
            // Define the object to add or edit as null
          
            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<LeaveSchedule> request = new PostRequest<LeaveSchedule>();
                    request.entity = b;
                    PostResponse<LeaveSchedule> r = _branchService.ChildAddOrUpdate<LeaveSchedule>(request);
                    b.recordId = r.recordId;

                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                         Common.errorMessage(r);
                        return;
                    }
                    List<LeaveSchedulePeriod> periods = JsonConvert.DeserializeObject<List<LeaveSchedulePeriod>>(pers);
                 bool Success =   AddPeriodsList(b.recordId, periods);
                                 

                   if (Success)
                    {

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
                    PostRequest<LeaveSchedule> modifyHeaderRequest = new PostRequest<LeaveSchedule>();
                    modifyHeaderRequest.entity = b;
                    PostResponse<LeaveSchedule> r = _branchService.ChildAddOrUpdate<LeaveSchedule>(modifyHeaderRequest);                   //Step 1 Selecting the object or building up the object for update purpose
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                        return;
                    }
                    LeaveSchedulesListRequest leaveScheduleReq = new LeaveSchedulesListRequest();
                    leaveScheduleReq.LeaveScheduleId = b.recordId;
                    ListResponse<LeaveSchedulePeriod> leaveScheduleResponse = _branchService.ChildGetAll<LeaveSchedulePeriod>(leaveScheduleReq);

                   leaveScheduleResponse.Items.ForEach(x =>
                   {
                       PostRequest<LeaveSchedulePeriod> LeaveScheduleDEleteRequest = new PostRequest<LeaveSchedulePeriod>();
                       LeaveScheduleDEleteRequest.entity = x;
                       PostResponse<LeaveSchedulePeriod> LeaveScheduleDEleteResponse = _branchService.ChildDelete<LeaveSchedulePeriod>(LeaveScheduleDEleteRequest);
                       if (!LeaveScheduleDEleteResponse.Success)
                       {
                           Common.errorMessage(LeaveScheduleDEleteResponse);
                           throw new Exception();
                       }
                   });
                   
                    List<LeaveSchedulePeriod> periods = JsonConvert.DeserializeObject<List<LeaveSchedulePeriod>>(pers);
                  bool result = AddPeriodsList(b.recordId, periods);

                    //Step 2 : saving to store

                    //Step 3 :  Check if request fails
                    if (result)
                    {


                        ModelProxy record = this.Store1.GetById(index);
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
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }
            }
        }
        private bool AddPeriodsList(string scheduleIdString, List<LeaveSchedulePeriod> periods)
        {
            short i = 1;
            int scheduleId = Convert.ToInt32(scheduleIdString);
            foreach (var period in periods)
            {
                period.seqNo = i++;
                period.lsId = scheduleId;
               
            }
            PostRequest<LeaveSchedulePeriod> periodRequest = new PostRequest<LeaveSchedulePeriod>();
            PostResponse<LeaveSchedulePeriod> response;
            periods.ForEach(x =>
            {
                periodRequest.entity = x;
                response = _branchService.ChildAddOrUpdate<LeaveSchedulePeriod>(periodRequest);
                if (!response.Success)
                {
                    Common.errorMessage(response);
                    throw new Exception();
                }
            });


            return true;
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