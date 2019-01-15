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
using AionHR.Model.LeaveManagement;
using AionHR.Services.Messaging.System;
using AionHR.Model.Company.Cases;
using System.Net;
using AionHR.Infrastructure.Domain;
using AionHR.Model.LoadTracking;
using AionHR.Services.Messaging.LoanManagment;
using AionHR.Model.Attributes;
using AionHR.Model.Payroll;
using Reports;

namespace AionHR.Web.UI.Forms
{
    public partial class LeavePayments : System.Web.UI.Page
    {
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
            //fill employee request 

            EmployeeListRequest req = new EmployeeListRequest();
            req.DepartmentId = "0";
            req.BranchId = "0";
            req.IncludeIsInactive = 0;
            req.SortBy = GetNameFormat();

            req.StartAt = "1";
            req.Size = "20";
            req.Filter = query;

            ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
            return response.Items;
        }

        private string GetNameFormat()
        {
            SystemDefaultRecordRequest req = new SystemDefaultRecordRequest();
            req.Key = "nameFormat";
            RecordResponse<KeyValuePair<string, string>> response = _systemService.ChildGetRecord<KeyValuePair<string, string>>(req);
            if (!response.Success)
            {

            }
            string paranthized = response.result.Value;
            paranthized = paranthized.Replace('{', ' ');
            paranthized = paranthized.Replace('}', ',');
            paranthized = paranthized.Substring(0, paranthized.Length - 1);
            paranthized = paranthized.Replace(" ", string.Empty);
            return paranthized;

        }

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();

        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();

        ILoanTrackingService _loanService = ServiceLocator.Current.GetInstance<ILoanTrackingService>();

        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();

        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();
        IPayrollService _payrollService = ServiceLocator.Current.GetInstance<IPayrollService>();

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
                HideShowColumns();
                Column6.Format = DateColumn1.Format = date.Format = effectiveDate.Format = _systemService.SessionHelper.GetDateformat();
                effectiveDate.MaxDate = DateTime.Now;

                //statusPref.Select("0");

                /*  c.Format = /cc.Format =*/

                //if (string.IsNullOrEmpty(Request.QueryString["employeeId"]))
                //    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorOperation).Show();
                //CurrentEmployee.Text = Request.QueryString["employeeId"];

                //cc.Format = _systemService.SessionHelper.GetDateformat();
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(Loan), BasicInfoTab, GridPanel1, btnAdd, SaveButton);

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




        //private void ApplyAccessControlOnLoanComments()
        //{
        //    var properties = AccessControlApplier.GetPropertiesLevels(typeof(LoanComment));
        //    foreach (var item in properties)
        //    {
        //        if (item.propertyId == "4501103")
        //        {
        //            if (item.accessLevel < 2)
        //                loanCommentGrid.ColumnModel.Columns[loanCommentGrid.ColumnModel.Columns.Count - 1].Renderer.Handler = " return '';";
        //        }

        //        if (item.accessLevel == 0)
        //        {
        //            if (item.propertyId == "4501102")
        //            {
        //                loanCommentGrid.ColumnModel.Columns[1].Renderer.Handler = loanCommentGrid.ColumnModel.Columns[1].Renderer.Handler.Replace("s.calendar()", "'***** '");
        //            }
        //            else
        //            {
        //                var indices = typeof(LoanComment).GetProperties().Where(x =>
        //                {
        //                    var d = x.GetCustomAttributes(typeof(PropertyID), false);
        //                    if (d.Count() == 0)
        //                        return false;
        //                    return (x.GetCustomAttributes(typeof(PropertyID), false).ToList()[0] as PropertyID).ID == item.propertyId;
        //                }).ToList();

        //                indices.ForEach(x =>
        //                {
        //                    loanCommentGrid.ColumnModel.Columns[1].Renderer.Handler = loanCommentGrid.ColumnModel.Columns[1].Renderer.Handler.Replace("record.data['" + x.Name + "']", "'***** '");
        //                });
        //            }

        //        }
        //    }

        //}

        protected void addSACurrency(object sender, DirectEventArgs e)
        {
            Currency obj = new Currency();
            //obj.name = currencyId.Text;
            //obj.reference = currencyId.Text;
            PostRequest<Currency> req = new PostRequest<Currency>();
            req.entity = obj;
            PostResponse<Currency> response = _systemService.ChildAddOrUpdate<Currency>(req);
            if (response.Success)
            {
                obj.recordId = response.recordId;


                //currencyId.Select(obj.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
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
                CurrentLanguage.Text = "ar";
            }
            else
            {
                CurrentLanguage.Text = "en";
            }
        }


        protected void PoPuP(object sender, DirectEventArgs e)
        {


            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];
            currentLeavePayment.Text = id;

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id;

                    RecordResponse<LeavePayment> response = _payrollService.ChildGetRecord<LeavePayment>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                         Common.errorMessage(response);
                        return;
                    }
                   


                    //employeeId.SuspendEvent("Change"); 
                    employeeId.GetStore().Add(new object[]
                       {
                                new
                                {
                                    recordId = response.result.employeeId,
                                    fullName =response.result.employeeName.fullName
                                }
                       });
                    employeeId.SetValue(response.result.employeeId);
                    //employeeId.ResumeEvent("Change"); 
                    //effectiveDate.SuspendEvent("Change");
                    effectiveDate.SetValue(response.result.effectiveDate);
                    //effectiveDate.ResumeEvent("Change");
                    leaveBalance.Text = response.result.leaveBalance.ToString();
           
                    this.BasicInfoTab.SetValues(response.result);
                    updateLeaveBalance.Text = "true";
                      








                    //if (!response.result.effectiveDate.HasValue)
                    //    effectiveDate.SelectedDate = DateTime.Now;
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

        //protected void PoPuPCase(object sender, DirectEventArgs e)
        //{


        //    string id = e.ExtraParams["id"];
        //    string type = e.ExtraParams["type"];
        //    string index = e.ExtraParams["index"];
        //    switch (type)
        //    {
        //        case "imgEdit":
        //            //Step 1 : get the object from the Web Service 
        //            X.Call("App.loanCommentGrid.editingPlugin.startEdit", Convert.ToInt32(index), 0);
        //            break;


        //        case "imgDelete":
        //            X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
        //            {
        //                Yes = new MessageBoxButtonConfig
        //                {
        //                    //We are call a direct request metho for deleting a record
        //                    Handler = String.Format("App.direct.DeleteCase({0})", id),
        //                    Text = Resources.Common.Yes
        //                },
        //                No = new MessageBoxButtonConfig
        //                {
        //                    Text = Resources.Common.No
        //                }

        //            }).Show();
        //            break;

        //        case "colAttach":

        //            //Here will show up a winow relatice to attachement depending on the case we are working on
        //            break;
        //        default:
        //            break;
        //    }


        //}


        [DirectMethod]
        public void DeleteRecord(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                LeavePayment s = new LeavePayment();
                s.recordId = index;
             
           
                s.date = DateTime.Now;
                s.effectiveDate = DateTime.Now;
              
                s.amount = 0;

                

                PostRequest<LeavePayment> req = new PostRequest<LeavePayment>();
                req.entity = s;
                PostResponse<LeavePayment> r = _payrollService.ChildDelete<LeavePayment>(req);
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


        //[DirectMethod]
        //public void DeleteCase(string index)
        //{
        //    try
        //    {
        //        //Step 1 Code to delete the object from the database 
        //        LoanComment s = new LoanComment();
        //        s.loanId = Convert.ToInt32(currentCase.Text);
        //        s.comment = "";
        //        s.seqNo = Convert.ToInt16(index);
        //        s.userId = 0;
        //        s.userName = "";
        //        s.date = DateTime.Now;



        //        PostRequest<LoanComment> req = new PostRequest<LoanComment>();
        //        req.entity = s;
        //        PostResponse<LoanComment> r = _loanService.ChildDelete<LoanComment>(req);
        //        if (!r.Success)
        //        {
        //            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //             Common.errorMessage(r);
        //            return;
        //        }
        //        else
        //        {
        //            //Step 2 :  remove the object from the store


        //            //Step 3 : Showing a notification for the user 
        //            Notification.Show(new NotificationConfig
        //            {
        //                Title = Resources.Common.Notification,
        //                Icon = Icon.Information,
        //                Html = Resources.Common.RecordDeletedSucc
        //            });
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //In case of error, showing a message box to the user
        //        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorDeletingRecord).Show();

        //    }

        //}



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

       

        protected void ADDNewRecord(object sender, DirectEventArgs e)
        {
            this.BasicInfoTab.Reset();

            //ListRequest req = new ListRequest();
            //ListResponse<KeyValuePair<string, string>> defaults = _systemService.ChildGetAll<KeyValuePair<string, string>>(req);
            //if (!defaults.Success)
            //{
            //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
            //    X.Msg.Alert(Resources.Common.Error, defaults.Summary).Show();
            //    return;
            //}
            //if (defaults.Items.Where(s => s.Key == "ldMethod").Count() != 0)
            //    ldMethod.Select(defaults.Items.Where(s => s.Key == "ldMethod").First().Value);
            //if (defaults.Items.Where(s => s.Key == "ldValue").Count() != 0)
            //    ldValue.Text = defaults.Items.Where(s => s.Key == "ldValue").First().Value.ToString();
            //caseCommentStore.DataSource = new List<CaseComment>();
            //caseCommentStore.DataBind();
            //Reset all values of the relative object

            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;
            date.SelectedDate = DateTime.Now;
            effectiveDate.SelectedDate = DateTime.Now;
            this.EditRecordWindow.Show();
            updateLeaveBalance.Text = "false";
        }





        private LeavePaymentsListRequest GetLeavePaymentRequest()
        {
            LeavePaymentsListRequest req = new LeavePaymentsListRequest();


            if (!string.IsNullOrEmpty(employeeFilter.Text) && employeeFilter.Value.ToString() != "0")
            {
                req.EmployeeId = Convert.ToInt32(employeeFilter.Value);
            }
            else
            {
                req.EmployeeId = 0;
            }

            req.Size = "2000";
            req.StartAt = "1";
            req.Filter = "";
            return req;
        }


        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;


            //Fetching the corresponding list

            //in this test will take a list of News
            //ListRequest request = new ListRequest();

            LeavePaymentsListRequest request = GetLeavePaymentRequest();
            request.Filter = "";
            request.Size = e.Limit.ToString();
            request.StartAt = e.Start.ToString();
            ListResponse<LeavePayment> routers = _payrollService.ChildGetAll<LeavePayment>(request);
            if (!routers.Success)
            {
                Common.errorMessage(routers);
                return;
            }
            this.Store1.DataSource = routers.Items;
            e.Total = routers.count;

            this.Store1.DataBind();
        }

        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.


            string obj = e.ExtraParams["values"];
            LeavePayment b = JsonConvert.DeserializeObject<LeavePayment>(obj);

            string id = e.ExtraParams["id"];
            // Define the object to add or edit as null

            b.employeeName = new EmployeeName();
            //if (ldMethodCom.SelectedItem != null)
            //    b.ldMethod = ldMethodCom.SelectedItem.Value; 
            if (employeeId.SelectedItem != null)
                b.employeeName.fullName = employeeId.SelectedItem.Text;

            if (date.ReadOnly)
                b.date = DateTime.Now;
            //b.effectiveDate = new DateTime(b.effectiveDate.Year, b.effectiveDate.Month, b.effectiveDate.Day, 14, 0, 0);


            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<LeavePayment> request = new PostRequest<LeavePayment>();
                    request.entity = b;
                    PostResponse<LeavePayment> r = _payrollService.ChildAddOrUpdate<LeavePayment>(request);
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
                        //this.Store1.Insert(0, b);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });
                        recordId.Text = b.recordId;
                      
                        currentCase.Text = b.recordId;

                        //RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;
                        //sm.DeselectAll();
                        //sm.Select(b.recordId.ToString());
                        Store1.Reload();
                        this.EditRecordWindow.Close();



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
                    PostRequest<LeavePayment> request = new PostRequest<LeavePayment>();
                    request.entity = b;
                    PostResponse<LeavePayment> r = _payrollService.ChildAddOrUpdate<LeavePayment>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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

                        if (date.ReadOnly)
                            record.Set("date", null);

                        record.Set("employeeName", b.employeeName);



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

        //public object ValidateSave(bool isPhantom, string obj, JsonObject values)
        //{


        //    if (!values.ContainsKey("comment"))
        //    {
        //        return new { valid = false, msg = "Error in call" };
        //    }

        //    PostRequest<LoanComment> req = new PostRequest<LoanComment>();
        //    LoanComment note = JsonConvert.DeserializeObject<List<LoanComment>>(obj)[0];
        //    //note.recordId = id;
        //    note.loanId = Convert.ToInt32(currentCase.Text);
        //    note.comment = values["comment"].ToString();
        //    int bulk;

        //    req.entity = note;

        //    PostResponse<LoanComment> resp = _loanService.ChildAddOrUpdate<LoanComment>(req);
        //    if (!resp.Success)
        //    {
        //       Common.errorMessage(resp);
        //        return new { valid = false };
        //    }

        //    return new { valid = true };
        protected void FillEmployeeInfo(object sender, DirectEventArgs e)
        {
            try
            {
                string effectiveDate = e.ExtraParams["effectiveDate"];
                string employeeId = e.ExtraParams["employeeId"];
                if (string.IsNullOrEmpty(employeeId))
                    return;
                if (!string.IsNullOrEmpty(effectiveDate))
                {
                    EmployeeQuickViewRecordRequest req = new EmployeeQuickViewRecordRequest();
                   
                    req.RecordID = employeeId;


                    req.asOfDate = Convert.ToDateTime(effectiveDate);

                    RecordResponse<EmployeeQuickView> routers = _employeeService.ChildGetRecord<EmployeeQuickView>(req);
                    if (!routers.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, routers.Summary).Show();

                    }
                    //RecordRequest req = new RecordRequest();
                    //req.RecordID = employeeId.Value.ToString();
                    //RecordResponse<Employee> routers = _employeeService.Get<Employee>(req);
                    //if (!routers.Success)
                    //{
                    //    Common.errorMessage(routers);
                    //    return;
                    //}
                    //this.setFillEmployeeInfoDisable(false);
                    if (routers.result == null)
                        return;

                    branchNameTx.Text = routers.result.branchName;
                    departmentNameTx.Text = routers.result.departmentName;
                    positionNameTx.Text = routers.result.positionName;
                    hireDateDf.Value = routers.result.hireDate;
                    nationalityTx.Text = routers.result.countryName;
                    if (updateLeaveBalance.Text == "false")
                    {
                        earnedLeaves.Text = routers.result.earnedLeaves.ToString();
                        leaveBalance.Text = routers.result.leaveBalance.ToString();
                    }
                    usedLeaves.Text = routers.result.usedLeaves.ToString();
                    paidLeaves.Text = routers.result.paidLeaves.ToString();


                    lastLeaveStartDate.Value = routers.result.lastLeaveStartDate;
                    lastLeaveEndDate.Value = routers.result.lastLeaveEndDate;
                    salary.Text = routers.result.salary.ToString();
                    //days.Text = "0";


                    serviceDuration.Text = routers.result.serviceDuration;
                    updateLeaveBalance.Text = "false";

                }
            }
            catch(Exception exp)
            {
                X.MessageBox.Alert(GetGlobalResourceObject("Common","Error").ToString(), exp.Message);
            }
           

        }
        //private void setFillEmployeeInfoDisable(bool YES)
        //{
        //    if (!YES)
        //    {
        //        branchNameTx.Disabled = false;
        //        departmentNameTx.Disabled = false;
        //        positionNameTx.Disabled = false;
        //        hireDateDf.Disabled = false;
        //        nationalityTx.Disabled = false;

        //        lastLeaveStartDate.Disabled = false;
        //        lastLeaveEndDate.Disabled = false;

        //        serviceDuration.Disabled = false;




        //    }
        //    else
        //    {
        //        branchNameTx.Disabled = true;
        //        departmentNameTx.Disabled = true;
        //        positionNameTx.Disabled = true;
        //        hireDateDf.Disabled = true;
        //        nationalityTx.Disabled = true;

        //        lastLeaveStartDate.Disabled = true;
        //        lastLeaveEndDate.Disabled = true;

        //        serviceDuration.Disabled = true;


        //    }

        //}
        protected void printBtn_Click(object sender, EventArgs e)
        {
            LeavePaymentsReport p = GetReport();
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
            LeavePaymentsReport p = GetReport();
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
            LeavePaymentsReport p = GetReport();
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
        private LeavePaymentsReport GetReport()
        {
            if (String.IsNullOrEmpty(currentLeavePayment.Text))
                return new LeavePaymentsReport();
            RecordRequest r = new RecordRequest();
            r.RecordID = currentLeavePayment.Text;

            RecordResponse<LeavePayment> response = _payrollService.ChildGetRecord<LeavePayment>(r);
            if (!response.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
               Common.errorMessage(response);
                return null;
            }
            EmployeeQuickViewRecordRequest req = new EmployeeQuickViewRecordRequest();
            req.RecordID = response.result.employeeId.ToString();
            req.asOfDate = response.result.effectiveDate;

            RecordResponse<EmployeeQuickView> routers = _employeeService.ChildGetRecord<EmployeeQuickView>(req);
            if (!routers.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", routers.ErrorCode) != null ? GetGlobalResourceObject("Errors", routers.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") +  routers.LogId: routers.Summary).Show();
                return null;
            }
           response.result.dateString = response.result.date.ToString(_systemService.SessionHelper.GetDateformat());
            response.result.effectiveDateString=response.result.effectiveDate.ToString(_systemService.SessionHelper.GetDateformat());
            List<LeavePayment> l = new List<LeavePayment>();
            l.Add(response.result);
            LeavePaymentsReport p = new LeavePaymentsReport();
            p.DataSource = l;
            p.Parameters["leaveBalance"].Value = routers.result.leaveBalance;
            p.Parameters["hireDate"].Value = routers.result.hireDate.Value.ToString(_systemService.SessionHelper.GetDateformat());

            p.Parameters["serviceDuration"].Value = routers.result.serviceDuration;

            p.Parameters["departmentName"].Value = routers.result.departmentName;

            p.Parameters["positionName"].Value = routers.result.positionName;
            p.Parameters["branchName"].Value = routers.result.branchName;

            p.Parameters["countryName"].Value = routers.result.countryName;
            if (routers.result.lastLeaveStartDate!=null)
            p.Parameters["lastLeaveStartDate"].Value = routers.result.lastLeaveStartDate.Value.ToString(_systemService.SessionHelper.GetDateformat()); ;
            if (routers.result.lastLeaveEndDate != null)
                p.Parameters["lastLeaveEndDate"].Value = routers.result.lastLeaveEndDate.Value.ToString(_systemService.SessionHelper.GetDateformat()); ;
        




            p.Parameters["User"].Value = _systemService.SessionHelper.GetCurrentUser();
            p.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            p.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;

            return p;



        }

    }
}