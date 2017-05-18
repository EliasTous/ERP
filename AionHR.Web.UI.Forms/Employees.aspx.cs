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
using AionHR.Infrastructure.Session;
using AionHR.Model.Employees.Profile;
using AionHR.Model.System;
using AionHR.Model.Employees.Leaves;
using AionHR.Model.Attendance;
using AionHR.Services.Messaging.System;
using AionHR.Infrastructure.Domain;

namespace AionHR.Web.UI.Forms
{
    public partial class Employees : System.Web.UI.Page
    {

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ILeaveManagementService _leaveManagementService = ServiceLocator.Current.GetInstance<ILeaveManagementService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();


        protected override void InitializeCulture()
        {

            bool rtl = true;
            if (!_systemService.SessionHelper.CheckIfArabicSession())
            {
                rtl = false;
                base.InitializeCulture();
                LocalisationManager.Instance.SetEnglishLocalisation();
                Culture = "en";
            }

            if (rtl)
            {
                base.InitializeCulture();
                LocalisationManager.Instance.SetArabicLocalisation();
                Culture = "ar-eg";
            }

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            employeeControl1.Store1 = Store1;

        }

        protected void Page_Load(object sender, EventArgs e)
        {



            if (!X.IsAjaxRequest && !IsPostBack)
            {

                SetExtLanguage();
                HideShowButtons();
                HideShowColumns();
                ColHireDate.Format = _systemService.SessionHelper.GetDateformat();

                inactivePref.Select("0");
                CurrentClassId.Text = ClassId.EPEM.ToString();
                InitFilters();
                BuildQuickViewTemplate();
            }


        }

        private void BuildQuickViewTemplate()
        {
            string html = "<table width='50%' style='font-weight:bold;'><tr><td> ";
            html += GetLocalResourceObject("FieldReportsTo").ToString() + " {reportsTo}</td><td>";
            html += GetLocalResourceObject("eosBalanceTitle").ToString() + " {eosBalance}</td></tr><tr><td>";

            html += GetLocalResourceObject("lastLeaveStartDateTitle").ToString() + " {lastLeave}</td><td>";
            html += GetLocalResourceObject("paidLeavesYTDTitle").ToString() + " {paidLeavesYTD}</td></tr><tr><td>";

            html += GetLocalResourceObject("leavesBalanceTitle").ToString() + " {leavesBalance}</td><td>";
            html += GetLocalResourceObject("allowedLeaveYtdTitle").ToString() + " {allowedLeaveYtd}</td></tr></table>";
            RowExpander1.Template.Html = html;
        }
        private List<Department> GetDepartments()
        {
            ListRequest departmentsRequest = new ListRequest();
            ListResponse<Department> resp = _companyStructureService.ChildGetAll<Department>(departmentsRequest);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return new List<Department>();
            }
            return resp.Items;
        }
        private List<Division> GetDivisions()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Division> resp = _companyStructureService.ChildGetAll<Division>(branchesRequest);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return new List<Division>();
            }
            return resp.Items;
        }
        private List<Branch> GetBranches()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Branch> resp = _companyStructureService.ChildGetAll<Branch>(branchesRequest);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return new List<Branch>();
            }

            return resp.Items;
        }
        private List<Model.Company.Structure.Position> GetPositions()
        {
            ListRequest positionsRequest = new ListRequest();
            ListResponse<Model.Company.Structure.Position> resp = _companyStructureService.ChildGetAll<Model.Company.Structure.Position>(positionsRequest);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return new List<Model.Company.Structure.Position>();
            }
            return resp.Items;
        }
        private void InitFilters()
        {
            filterBranchStore.DataSource = GetBranches();
            filterBranchStore.DataBind();
            filterDepartmentStore.DataSource = GetDepartments();
            filterDepartmentStore.DataBind();
            filterPositionStore.DataSource = GetPositions();
            filterPositionStore.DataBind();
            filterDivisionStore.DataSource = GetDivisions();
            filterDivisionStore.DataBind();
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
                BuildQuickViewTemplate();
            }
            pRTL.Text = rtl.ToString();
        }



        protected void PoPuP(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":

                    employeeControl1.Update(id.ToString());


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

                case "colAttach":

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
            employeeControl1.Add();
        }

        private EmployeeListRequest GetListRequest(StoreReadDataEventArgs e)
        {
            EmployeeListRequest empRequest = new EmployeeListRequest();
            if (!string.IsNullOrEmpty(inactivePref.Text) && inactivePref.Value.ToString() != "")
            {
                empRequest.IncludeIsInactive = Convert.ToInt32(inactivePref.Value);
            }
            else
            {
                empRequest.IncludeIsInactive = 2;
            }

            if (!string.IsNullOrEmpty(filterDepartment.Text) && filterDepartment.Value.ToString() != "")
            {
                empRequest.DepartmentId = (filterDepartment.Value).ToString();
            }
            else
            {
                empRequest.DepartmentId = "0";
            }
            if (!string.IsNullOrEmpty(filterBranch.Text) && filterBranch.Value.ToString() != "")
            {
                empRequest.BranchId = (filterBranch.Value).ToString();
            }
            else
            {
                empRequest.BranchId = "0";
            }

            if (!string.IsNullOrEmpty(filterPosition.Text) && filterPosition.Value.ToString() != "")
            {
                empRequest.PositionId = (filterPosition.Value).ToString();
            }
            else
            {
                empRequest.PositionId = "0";
            }

            if (e.Sort[0].Property == "name.fullName")
                empRequest.SortBy = GetNameFormat();
            else if (e.Sort[0].Property == "name.reference")
                empRequest.SortBy = "reference";
            else
                empRequest.SortBy = e.Sort[0].Property;
            empRequest.Size = e.Limit.ToString();
            empRequest.StartAt = e.Start.ToString();
            empRequest.Filter = searchTrigger.Text;

            return empRequest;
        }
        private string GetNameFormat()
        {
            return _systemService.SessionHelper.Get("nameFormat").ToString();

        }
        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page

            EmployeeListRequest empRequest = GetListRequest(e);





            ListResponse<Employee> emps = _employeeService.GetAll<Employee>(empRequest);
            if (!emps.Success)
            {
                X.Msg.Alert(Resources.Common.Error, emps.Summary).Show();
                return;
            }
            e.Total = emps.count;
            if (emps.Items != null)
            {
                this.Store1.DataSource = emps.Items;
                this.Store1.DataBind();
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


        protected void Unnamed_Load(object sender, EventArgs e)
        {
        }

        [DirectMethod]
        public object GetQuickView(Dictionary<string, string> parameters)
        {
            RecordRequest req = new RecordRequest();
            req.RecordID = parameters["id"];
            RecordResponse<EmployeeQuickView> qv = _employeeService.ChildGetRecord<EmployeeQuickView>(req);
            if (!qv.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, qv.Summary).Show();
                return null;
            }
            return new
            {
                reportsTo = qv.result.reportToName.fullName,
                eosBalance = qv.result.eosBalance,
                paidLeavesYTD = qv.result.paidLeavesYTD,
                leavesBalance = qv.result.leavesBalance,
                allowedLeaveYtd = qv.result.allowedLeaveYtd,
                lastleave = qv.result.LastLeave(_systemService.SessionHelper.GetDateformat())


            };

        }

    }
}