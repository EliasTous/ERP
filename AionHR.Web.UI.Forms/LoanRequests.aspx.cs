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
using AionHR.Web.UI.Forms.ConstClasses;
using System.Text.RegularExpressions;

namespace AionHR.Web.UI.Forms
{
    public partial class LoanRequests : System.Web.UI.Page
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

            req.StartAt = "0";
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

                SetBasicInfoFormEnable(true);
                caseCommentsAddButton.Disabled = false;
                addDeduction.Disabled = false;
                effectiveDate.Disabled = false;
                statusPref.Select("0");
                ldMethod.Select("0");
                currentLoanId.Text = "0";
                LoanAmount.Text = "0";
                X.GetCmp<DateField>("deductionDate").MinDate = new DateTime();
                c.Format =deductionDate.Format= dateCol.Format= /*cc.Format =*/ date.Format = effectiveDate.Format = _systemService.SessionHelper.GetDateformat();
                
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
                try

                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(LoanComment), null, loanCommentGrid, null, caseCommentsAddButton);
                    ApplyAccessControlOnLoanComments();
                }
                catch (AccessDeniedException exp)
                {

                    caseCommentsTab.Hidden = true;

                }
                if (purpose.InputType == InputType.Password)
                {
                    purpose.Visible = false;
                    purposeField.Visible = true;
                }
            }

        }

        protected void addLoanType(object sender, DirectEventArgs e)
        {
            LoanType obj = new LoanType();
            obj.name = ltId.Text;

            PostRequest<LoanType> req = new PostRequest<LoanType>();
            req.entity = obj;
            PostResponse<LoanType> response = _loanService.ChildAddOrUpdate<LoanType>(req);
            if (response.Success)
            {
                obj.recordId = response.recordId;
                FillLoanType();
                ltId.Select(obj.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }


        }
        protected void addBranch(object sender, DirectEventArgs e)
        {
            Branch obj = new Branch();
            obj.name = branchId.Text;

            PostRequest<Branch> req = new PostRequest<Branch>();
            req.entity = obj;
            PostResponse<Branch> response = _companyStructureService.ChildAddOrUpdate<Branch>(req);
            if (response.Success)
            {
                obj.recordId = response.recordId;
                FillBranchField();
                branchId.Select(obj.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }


        }

        private void FillLoanType()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<LoanType> resp = _loanService.ChildGetAll<LoanType>(branchesRequest);
            if (!resp.Success)
               Common.errorMessage(resp);
            ltStore.DataSource = resp.Items;
            ltStore.DataBind();
        }
        private void ApplyAccessControlOnLoanComments()
        {
            var properties = AccessControlApplier.GetPropertiesLevels(typeof(LoanComment));
            foreach (var item in properties)
            {
                if (item.propertyId == "4501103")
                {
                    if (item.accessLevel < 2)
                        loanCommentGrid.ColumnModel.Columns[loanCommentGrid.ColumnModel.Columns.Count - 1].Renderer.Handler = " return '';";
                }

                if (item.accessLevel == 0)
                {
                    if (item.propertyId == "4501102")
                    {
                        loanCommentGrid.ColumnModel.Columns[1].Renderer.Handler = loanCommentGrid.ColumnModel.Columns[1].Renderer.Handler.Replace("s.calendar()", "'***** '");
                    }
                    else
                    {
                        var indices = typeof(LoanComment).GetProperties().Where(x =>
                        {
                            var d = x.GetCustomAttributes(typeof(PropertyID), false);
                            if (d.Count() == 0)
                                return false;
                            return (x.GetCustomAttributes(typeof(PropertyID), false).ToList()[0] as PropertyID).ID == item.propertyId;
                        }).ToList();

                        indices.ForEach(x =>
                        {
                            loanCommentGrid.ColumnModel.Columns[1].Renderer.Handler = loanCommentGrid.ColumnModel.Columns[1].Renderer.Handler.Replace("record.data['" + x.Name + "']", "'***** '");
                        });
                    }

                }
            }

        }

        protected void addSACurrency(object sender, DirectEventArgs e)
        {
            Currency obj = new Currency();
            obj.name = currencyId.Text;
            obj.reference = currencyId.Text;
            PostRequest<Currency> req = new PostRequest<Currency>();
            req.entity = obj;
            PostResponse<Currency> response = _systemService.ChildAddOrUpdate<Currency>(req);
            if (response.Success)
            {
                obj.recordId = response.recordId;
                FillCurrency();

                currencyId.Select(obj.recordId);
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

            panelRecordDetails.ActiveIndex = 0;
            //SetTabPanelEnable(true);
            SetBasicInfoFormEnable(true);
            caseCommentsAddButton.Disabled = false;
            addDeduction.Disabled = false;
            effectiveDate.Disabled = false;
            DeductionGridPanel.Disabled = false;
            string id = e.ExtraParams["id"];
            currentLoanId.Text = id;
            Store3.Reload();
            ApprovalStore.Reload();

            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id;

                    RecordResponse<Loan> response = _loanService.Get<Loan>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                         Common.errorMessage(response);
                        return;
                    }

                    LoanAmount.Text = response.result.amount.ToString();
                    currentCase.Text = id;
                    if (response.result.effectiveDate.HasValue)
                        X.GetCmp<DateField>("deductionDate").MinDate =Convert.ToDateTime(response.result.effectiveDate);



                    employeeId.GetStore().Add(new object[]
                       {
                                new
                                {
                                    recordId = response.result.employeeId,
                                    fullName =response.result.employeeName.fullName
                                }
                       });
                    employeeId.SetValue(response.result.employeeId);
                //    effectiveDate.Disabled = response.result.status != 3;
                    //FillFilesStore(Convert.ToInt32(id));

                    //Step 2 : call setvalues with the retrieved object
                    this.BasicInfoTab.SetValues(response.result);
                    FillCurrency();
                    FillBranchField();
                    FillLoanType();
                    ltId.Select(response.result.ltId.ToString());
                    CurrentAmountCurrency.Text = response.result.currencyRef;
                    currencyId.Select(response.result.currencyId.ToString());
                    status.Select(response.result.status.ToString());
                    ldMethod.Select(response.result.ldMethod.ToString());
                    if (!string.IsNullOrEmpty(response.result.branchId))
                        branchId.Select(response.result.branchId);
                    loanComments_RefreshData(Convert.ToInt32(id));
                    //if (!response.result.effectiveDate.HasValue)
                    //    effectiveDate.SelectedDate = DateTime.Now;
                    if (response.result.status == 2 || response.result.status==-1)
                    {


                        SetBasicInfoFormEnable(false);
                        effectiveDate.Disabled = true;
                        caseCommentsAddButton.Disabled = true;
                        addDeduction.Disabled = true;

                    }
                    else
                    {
                       
                        SetBasicInfoFormEnable(true);
                        caseCommentsAddButton.Disabled = false;
                        addDeduction.Disabled = false;
                        effectiveDate.Disabled = false;
                    }
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

        protected void PoPuPCase(object sender, DirectEventArgs e)
        {


            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];
            string index = e.ExtraParams["index"];
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    X.Call("App.loanCommentGrid.editingPlugin.startEdit", Convert.ToInt32(index), 0);
                    break;


                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteCase({0})", id),
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
        protected void PoPuPDed(object sender, DirectEventArgs e)
        {


            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];
         
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id.ToString();
                    RecordResponse<LoanDeduction> response = _loanService.ChildGetRecord<LoanDeduction>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                       Common.errorMessage(response);
                        return;
                    }
                   

                    //Step 2 : call setvalues with the retrieved object


                 
                    this.deductionInfoTab.SetValues(response.result);
                   
                    this.EditDeductionWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditDeductionWindow.Show();
                    break;
                   


                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteDeduction({0})", id),
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


        [DirectMethod]
        public void DeleteRecord(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                Loan s = new Loan();
                s.recordId = index;
                s.employeeId = "0";
                s.purpose = "";
                s.date = DateTime.Now;
                s.effectiveDate = DateTime.Now;
                s.status = 0;
                s.ltId = 0;
                s.ltName = "";
                s.amount = 0;

                s.currencyId = 0;

                PostRequest<Loan> req = new PostRequest<Loan>();
                req.entity = s;
                PostResponse<Loan> r = _loanService.Delete<Loan>(req);
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
        public void DeleteDeduction(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                LoanDeduction s = new LoanDeduction();
                s.recordId = index;

                s.notes = "";
                s.date = DateTime.Now;
                s.loanId = 0;
               
                s.amount = 0;
                s.payrollDeduction = false;
                s.type = 1;
               

                PostRequest<LoanDeduction> req = new PostRequest<LoanDeduction>();
                req.entity = s;
                PostResponse<LoanDeduction> r = _loanService.ChildDelete<LoanDeduction>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                  Common.errorMessage(r);
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    Store3.Remove(index);

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
        public void DeleteCase(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                LoanComment s = new LoanComment();
                s.loanId = Convert.ToInt32(currentCase.Text);
                s.comment = "";
                s.seqNo = Convert.ToInt16(index);
                s.userId = 0;
                s.userName = "";
                s.date = DateTime.Now;



                PostRequest<LoanComment> req = new PostRequest<LoanComment>();
                req.entity = s;
                PostResponse<LoanComment> r = _loanService.ChildDelete<LoanComment>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                     Common.errorMessage(r);
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    caseCommentStore.Remove(index);

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
        public void DeleteDed(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                LoanComment s = new LoanComment();
                s.loanId = Convert.ToInt32(currentCase.Text);
                s.comment = "";
                s.seqNo = Convert.ToInt16(index);
                s.userId = 0;
                s.userName = "";
                s.date = DateTime.Now;



                PostRequest<LoanComment> req = new PostRequest<LoanComment>();
                req.entity = s;
                PostResponse<LoanComment> r = _loanService.ChildDelete<LoanComment>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                  Common.errorMessage(r);
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    caseCommentStore.Remove(index);

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




        private void FillBranchField()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Branch> resp = _companyStructureService.ChildGetAll<Branch>(branchesRequest);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            branchStore.DataSource = resp.Items;
            branchStore.DataBind();
        }


        private void FillCurrency()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Currency> resp = _systemService.ChildGetAll<Currency>(branchesRequest);
            if (!resp.Success)
                Common.errorMessage(resp);
            currencyStore.DataSource = resp.Items;
            currencyStore.DataBind();
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

        //private void SetTabPanelEnable(bool isEnable)
        //{
        //    foreach (var item in panelRecordDetails.Items)
        //    {
        //        if (item.ID == "BasicInfoTab")
        //            continue;
        //        item.Disabled = !isEnable;
        //    }

        //}
        private void SetBasicInfoFormEnable(bool isEnable)
        {
            foreach (var item in BasicInfoTab.Items)
            {
               
                item.Disabled = !isEnable;
              
            }
            effectiveDate.Disabled = !isEnable;


            SaveButton.Disabled = !isEnable;
        }

        protected void ADDNewRecord(object sender, DirectEventArgs e)
        {
            BasicInfoTab.Reset();
           
            effectiveDate.Disabled = false;
            caseCommentsAddButton.Disabled = false;
            addDeduction.Disabled = false;
            SetBasicInfoFormEnable(true);
            ListRequest req = new ListRequest();
            ListResponse<KeyValuePair<string, string>> defaults = _systemService.ChildGetAll<KeyValuePair<string, string>>(req);
            if (!defaults.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, defaults.Summary).Show();
                return;
            }
            if(defaults.Items.Where(s => s.Key == "ldMethod").Count()!=0)
             ldMethod.Select(defaults.Items.Where(s => s.Key == "ldMethod").First().Value);
            if (defaults.Items.Where(s => s.Key == "ldValue").Count() != 0)
                ldValue.Text = defaults.Items.Where(s => s.Key == "ldValue").First().Value.ToString();
            caseCommentStore.DataSource = new List<CaseComment>();
            caseCommentStore.DataBind();
            //Reset all values of the relative object

            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;
            date.SelectedDate = DateTime.Now;
            //effectiveDate.SelectedDate= DateTime.Now;
            date.MaxDate = new DateTime();
            DeductionGridPanel.Disabled = true;
            panelRecordDetails.ActiveIndex = 0;
            effectiveDate.SelectedDate = DateTime.Now;
            //SetTabPanelEnable(false);
            FillLoanType();
            FillBranchField();
            FillCurrency();

            currencyId.Select(defaults.Items.Where(s => s.Key == "currencyId").First().Value.ToString());
          //  effectiveDate.Disabled = true;
            this.EditRecordWindow.Show();
        }
        protected void ADDNewDeductionRecord(object sender, DirectEventArgs e)
        {
            deductionInfoTab.Reset();
          
           
            //Reset all values of the relative object

            this.EditDeductionWindow.Title = Resources.Common.AddNewRecord;
            deductionDate.SelectedDate = DateTime.Now;
           
            //SetTabPanelEnable(false);
          
            //  effectiveDate.Disabled = true;
            this.EditDeductionWindow.Show();
        }

        protected void loanComments_RefreshData(int cId)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News

            LoanCommentsListRequest req = new LoanCommentsListRequest();
            req.loanId = cId;
            ListResponse<LoanComment> notes = _loanService.ChildGetAll<LoanComment>(req);
            if (!notes.Success)
            {
                //   X.Msg.Alert(Resources.Common.Error, notes.Summary).Show();
            }
            this.caseCommentStore.DataSource = notes.Items;
            this.caseCommentStore.DataBind();
        }

        protected void ADDNewRecordComments(object sender, DirectEventArgs e)
        {
            string noteText = e.ExtraParams["noteText"];
            X.Call("ClearNoteText");
            PostRequest<LoanComment> req = new PostRequest<LoanComment>();
            LoanComment note = new LoanComment();
            note.recordId = null;
            note.comment = noteText;
            note.date = DateTime.Now;
            note.loanId = Convert.ToInt32(currentCase.Text);
            req.entity = note;
          


            PostResponse<LoanComment> resp = _loanService.ChildAddOrUpdate<LoanComment>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId")+ resp.LogId : resp.Summary).Show();

            }
            loanComments_RefreshData(Convert.ToInt32(currentCase.Text));

            //Reset all values of the relative object

        }

        private LoanManagementListRequest GetLoanManagementRequest()
        {
            LoanManagementListRequest req = new LoanManagementListRequest();
            var d = jobInfo1.GetJobInfo();
            req.BranchId = d.BranchId.HasValue ? d.BranchId.Value : 0;
            req.DepartmentId = d.DepartmentId.HasValue ? d.DepartmentId.Value : 0;
            req.DivisionId = d.DivisionId.HasValue ? d.DivisionId.Value : 0;

            if (!string.IsNullOrEmpty(employeeFilter.Text) && employeeFilter.Value.ToString() != "0")
            {
                req.EmployeeId = Convert.ToInt32(employeeFilter.Value);
            }
            else
            {
                req.EmployeeId = 0;
            }
            if (!string.IsNullOrEmpty(statusPref.Text) && statusPref.Value.ToString() != "")
            {
                req.Status = Convert.ToInt32(statusPref.Value);
            }
            else
            {
                req.Status = 0;
            }


            req.Size = "2000";
            req.StartAt = "0";
            req.Filter = "";
            req.SortBy = "employeeId";

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
            LoanManagementListRequest request = GetLoanManagementRequest();
            request.Filter = "";

            request.SortBy = e.Sort[0].Property;
            if (e.Sort[0].Property == "employeeName")
                request.SortBy = _systemService.SessionHelper.GetNameformat();


            request.Filter = "";

            request.Size = e.Limit.ToString();
            request.StartAt = e.Start.ToString();
            request.LoanId = "0";
            ListResponse<Loan> routers = _loanService.GetAll<Loan>(request);
            if (!routers.Success)
            {
                Common.errorMessage(routers);
                return;
            }
            this.Store1.DataSource = routers.Items;
            e.Total = routers.count;

            this.Store1.DataBind();
        }
        protected void Store3_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            //ListRequest request = new ListRequest();
            LoanDeductionListRequest request =new  LoanDeductionListRequest();
            request.LoanId = currentLoanId.Text;

          
           


           

           
            ListResponse<LoanDeduction> response = _loanService.ChildGetAll<LoanDeduction>(request);
            if (!response.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).ToString() : response.Summary).Show();
                return;
            }
            this.Store3.DataSource = response.Items;
       

            this.Store3.DataBind();
        }

        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {

            try
            {
                //Getting the id to check if it is an Add or an edit as they are managed within the same form.


                string obj = e.ExtraParams["values"];
                Loan b = JsonConvert.DeserializeObject<Loan>(obj);
                b.ldValue = Regex.Replace(b.ldValue, "[^.0-9]", "");
                b.amount = Convert.ToDouble(Regex.Replace(b.amount.ToString(), "[^.0-9]", ""));

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
                if (currencyId.SelectedItem != null)
                    b.currencyRef = currencyId.SelectedItem.Text;
                if (branchId.SelectedItem != null)
                {
                    b.branchName = branchId.SelectedItem.Text;
                }
                if (ltId.SelectedItem != null)
                    b.ltName = ltId.SelectedItem.Text;

                if (string.IsNullOrEmpty(id))
                {

                    try
                    {
                        //New Mode
                        //Step 1 : Fill The object and insert in the store 
                        PostRequest<Loan> request = new PostRequest<Loan>();
                        request.entity = b;
                        PostResponse<Loan> r = _loanService.AddOrUpdate<Loan>(request);
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
                            LoanAmount.Text = b.amount.ToString();
                            DeductionGridPanel.Disabled = false;
                            b.recordId = r.recordId;
                            if (b.effectiveDate != null)
                                X.GetCmp<DateField>("deductionDate").MinDate = Convert.ToDateTime(b.effectiveDate);

                            //Add this record to the store 
                            this.Store1.Insert(0, b);

                            //Display successful notification
                            Notification.Show(new NotificationConfig
                            {
                                Title = Resources.Common.Notification,
                                Icon = Icon.Information,
                                Html = Resources.Common.RecordSavingSucc
                            });
                            recordId.Text = b.recordId;
                            //SetTabPanelEnable(true);
                            currentCase.Text = b.recordId;

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
                        PostRequest<Loan> request = new PostRequest<Loan>();
                        request.entity = b;
                        PostResponse<Loan> r = _loanService.AddOrUpdate<Loan>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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
                            DeductionGridPanel.Disabled = false;
                            if (b.effectiveDate != null)
                                X.GetCmp<DateField>("deductionDate").MinDate = Convert.ToDateTime(b.effectiveDate);
                            LoanAmount.Text = b.amount.ToString();
                            ModelProxy record = this.Store1.GetById(id);
                            BasicInfoTab.UpdateRecord(record);
                            record.Set("currencyRef", b.currencyRef);
                            if (date.ReadOnly)
                                record.Set("date", null);

                            record.Set("employeeName", b.employeeName);

                            record.Set("branchName", b.branchName);

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
            catch(Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }
        protected void SaveNewDeductionRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];

            LoanDeduction b = JsonConvert.DeserializeObject<LoanDeduction>(obj);
            b.loanId =Convert.ToInt32( currentLoanId.Text);
            b.employeeId =Convert.ToInt32( CurrentEmployee.Text);
            b.recordId = id;
        
            // Define the object to add or edit as null
          
           

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<LoanDeduction> request = new PostRequest<LoanDeduction>();
                    request.entity = b;
                    PostResponse<LoanDeduction> r = _loanService.ChildAddOrUpdate<LoanDeduction>(request);
                    b.recordId = r.recordId;

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
                        Store3.Reload();

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        this.EditDeductionWindow.Close();
                        //RowSelectionModel sm = this.DeductionGridPanel.GetSelectionModel() as RowSelectionModel;
                        //sm.DeselectAll();
                        //sm.Select(b.recordId.ToString());



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
                    PostRequest<LoanDeduction> request = new PostRequest<LoanDeduction>();
                    request.entity = b;
                    PostResponse<LoanDeduction> r = _loanService.ChildAddOrUpdate<LoanDeduction>(request);                   //Step 1 Selecting the object or building up the object for update purpose

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


                        //ModelProxy record = this.Store3.GetById(index);
                        //deductionInfoTab.UpdateRecord(record);


                        //record.Commit();
                        Store3.Reload();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });

                        this.EditDeductionWindow.Close();

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
        protected void DeductionInfoTab_Load(object sender, EventArgs e)
        {

        }
        [DirectMethod]
        public object ValidateSave(bool isPhantom, string obj, JsonObject values)
        {


            if (!values.ContainsKey("comment"))
            {
                return new { valid = false, msg = "Error in call" };
            }

            PostRequest<LoanComment> req = new PostRequest<LoanComment>();
            LoanComment note = JsonConvert.DeserializeObject<List<LoanComment>>(obj)[0];
            //note.recordId = id;
            note.loanId = Convert.ToInt32(currentCase.Text);
            note.comment = values["comment"].ToString();
            note.employeeId = Convert.ToInt32(CurrentEmployee.Text);
            int bulk;

            req.entity = note;

            PostResponse<LoanComment> resp = _loanService.ChildAddOrUpdate<LoanComment>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return new { valid = false };
            }
            loanComments_RefreshData(note.loanId);
            return new { valid = true };
        }

        protected void LoanTypeChanged(object sender, DirectEventArgs e)
        {
            RecordResponse<LoanType> resp = _loanService.ChildGetRecord<LoanType>(new RecordRequest() { RecordID = e.ExtraParams["id"] });
            if(resp.Success)
            {
                ldMethod.Select(resp.result.ldMethod.ToString());
                ldValue.Text = resp.result.ldValue.ToString();
                ldMethod.ReadOnly = resp.result.disableEditing;
                ldValue.ReadOnly= resp.result.disableEditing;

            }
            else
                Common.errorMessage(resp);

        }

        protected void ApprovalsStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            LoanManagementListRequest req = new LoanManagementListRequest();
            req.LoanId = currentLoanId.Text;
            if(string.IsNullOrEmpty(req.LoanId))
            {
                ApprovalStore.DataSource = new List<LoanApproval>();
                ApprovalStore.DataBind();
            }
            req.approverId = 0;
            req.BranchId = 0;
            req.DepartmentId = 0;
            req.DivisionId = 0;
            req.EmployeeId = 0;
            req.Status = 0;
            req.Filter = "";
            req.PositionId = 0;
            req.EsId = 0;
            
            req.SortBy = "recordId";





            req.Size = "1000";
            req.StartAt = "0";
            ListResponse<LoanApproval> response = _loanService.ChildGetAll<LoanApproval>(req);
          
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
    }
}