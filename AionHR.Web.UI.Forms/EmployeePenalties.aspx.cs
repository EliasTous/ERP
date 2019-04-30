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
using AionHR.Model.Payroll;
using AionHR.Services.Messaging.CompanyStructure;
using AionHR.Model.Employees;
using AionHR.Services.Messaging.LoanManagment;
using AionHR.Services.Messaging.Employees;
using AionHR.Web.UI.Forms.ConstClasses;

namespace AionHR.Web.UI.Forms
{
    public partial class EmployeePenalties : System.Web.UI.Page
    {

        ICompanyStructureService _branchService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        IPayrollService _payrollService = ServiceLocator.Current.GetInstance<IPayrollService>();


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
                //try
                //{
                //    AccessControlApplier.ApplyAccessControlOnPage(typeof(Model.Company.Structure.Position), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
                //}
                //catch (AccessDeniedException exp)
                //{
                //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                //    Viewport1.Hidden = true;
                //    return;
                //}
                ColDate.Format=date.Format = _systemService.SessionHelper.GetDateformat();
                FillPenalty();

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
            CurrentpenaltyId.Text = id.ToString();
            FillPenalty();
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id.ToString();
                    RecordResponse<EmployeePenalty> response = _employeeService.ChildGetRecord<EmployeePenalty>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.BasicInfoTab.SetValues(response.result);
                    employeeId.GetStore().Add(new object[]
                   {
                                new
                                {
                                    recordId = response.result.employeeId,
                                    fullName =response.result.employeeName.fullName
                                }
                   });
                    employeeId.SetValue(response.result.employeeId);

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
        [DirectMethod]
        public void DeleteRecord(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                PostRequest<EmployeePenalty> req = new PostRequest<EmployeePenalty>();
                EmployeePenalty p = new EmployeePenalty();
                p.recordId = index;
                req.entity = p;
                PostResponse<EmployeePenalty> resp = _employeeService.ChildDelete<EmployeePenalty>(req);
                if (!resp.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(resp);
                    return;

                }
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
            FillPenalty();
            CurrentpenaltyId.Text = "";
            date.SelectedDate = DateTime.Now; 
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;

            this.EditRecordWindow.Show();
        }

        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                //GEtting the filter from the page
                string filter = string.Empty;
                int totalCount = 1;



                //Fetching the corresponding list

                //in this test will take a list of News
                EmployeePenaltyListRequest request = new EmployeePenaltyListRequest();
                // request.Filter = "";
                request.SortBy = "date";
                request.Size = e.Limit.ToString();
                request.StartAt = e.Start.ToString();

                request.employeeId = employeeFilter.GetEmployee().employeeId.ToString();
                if (string.IsNullOrEmpty(PenaltyFilter.Value.ToString()))
                    request.penaltyId = "0";
                else
                    request.penaltyId = PenaltyFilter.SelectedItem.Value.ToString();
                if (string.IsNullOrEmpty(apStatus.Value.ToString()))
                    request.apStatus = "0";
                else
                    request.apStatus = apStatus.Value.ToString();

                ListResponse<EmployeePenalty> resp = _employeeService.ChildGetAll<EmployeePenalty>(request);
                if (!resp.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(resp);
                    return;
                }

                e.Total = resp.count;
                this.Store1.DataSource = resp.Items;


                this.Store1.DataBind();
            }
            catch(Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error,exp.Message).Show();
            }
        }




        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];
            EmployeePenalty b = JsonConvert.DeserializeObject<EmployeePenalty>(obj);
            //if (referToPositionId.SelectedItem != null)
            //    b.referToPositionName = referToPositionId.SelectedItem.Text;
            //if (tsId.SelectedItem != null)
            //    b.tsName = tsId.SelectedItem.Text;
            b.recordId = CurrentpenaltyId.Text;
            // Define the object to add or edit as null
            b.apStatus = string.IsNullOrEmpty(b.apStatus) ? "1" : b.apStatus;
            if (string.IsNullOrEmpty(CurrentpenaltyId.Text))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<EmployeePenalty> request = new PostRequest<EmployeePenalty>();
                    request.entity = b;
                    PostResponse<EmployeePenalty> r = _employeeService.ChildAddOrUpdate<EmployeePenalty>(request);
                   

                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                       Common.errorMessage(r);;
                        return;
                    }
                    else
                    {
                        b.recordId = r.recordId;
                        //Add this record to the store 
                        Store1.Reload();
                        CurrentpenaltyId.Text = b.recordId; 

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
                    PostRequest<EmployeePenalty> request = new PostRequest<EmployeePenalty>();
                    request.entity = b;
                    b.recordId = CurrentpenaltyId.Text;
                    PostResponse<EmployeePenalty> r = _employeeService.ChildAddOrUpdate<EmployeePenalty>(request);                   //Step 1 Selecting the object or building up the object for update purpose

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
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {
            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            List<Employee> data = GetEmployeesFiltered(prms.Query);
            data.ForEach(s => { s.fullName = s.name.fullName; });
            //  return new
            // {
            return data;
        }

        private List<Employee> GetEmployeesFiltered(string query)
        {

            EmployeeListRequest req = new EmployeeListRequest();
            req.DepartmentId = "0";
            req.BranchId = "0";
            req.IncludeIsInactive = 2;
            req.SortBy = GetNameFormat();

            req.StartAt = "0";
            req.Size = "20";
            req.Filter = query;

            ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
            return response.Items;
        }


        private string GetNameFormat()
        {
            return _systemService.SessionHelper.Get("nameFormat").ToString();
        }

        protected void ApprovalsStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            EmployeePenaltyApprovalListRequest req = new EmployeePenaltyApprovalListRequest();

            req.apStatus = "0";
            req.penaltyId = CurrentpenaltyId.Text;
            req.approverId = "0";
            req.PositionId = "0";
            req.DepartmentId = "0";
            req.DivisionId = "0";
            req.BranchId = "0";
            req.EsId = "0";

            if (string.IsNullOrEmpty(req.penaltyId))
            {
                ApprovalStore.DataSource = new List<EmployeePenaltyApproval>();
                ApprovalStore.DataBind();
                return;
            }
            ListResponse<EmployeePenaltyApproval> response = _employeeService.ChildGetAll<EmployeePenaltyApproval>(req);

            if (!response.Success)
            {
                Common.errorMessage(response);
                return;
            }
            response.Items.ForEach(x =>
            {

                switch (x.status)
                {
                    case 1:
                        x.statusString = StatusNew.Text;
                        break;
                    case 2:
                        x.statusString = StatusInProcess.Text;
                        ;
                        break;
                    case 3:
                        x.statusString = StatusApproved.Text;
                        ;
                        break;
                    case -1:
                        x.statusString = StatusRejected.Text;

                        break;
                }
            }
          );
            ApprovalStore.DataSource = response.Items; 
            ApprovalStore.DataBind();
        }

        private void FillPenalty()
        {
            string filter = string.Empty;




            //Fetching the corresponding list

            //in this test will take a list of News
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<PenaltyType> response = _payrollService.ChildGetAll<PenaltyType>(request);
            if (!response.Success)
            {
                Common.errorMessage(response);
                return;
            }


            PenaltyStore.DataSource = response.Items;
            PenaltyStore.DataBind();
        }




    }
}