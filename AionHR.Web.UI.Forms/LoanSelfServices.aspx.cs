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
using AionHR.Model.SelfService;
using AionHR.Web.UI.Forms.ConstClasses;

namespace AionHR.Web.UI.Forms
{
    public partial class LoanSelfServices : System.Web.UI.Page
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
        ISelfServiceService _selfServiceService = ServiceLocator.Current.GetInstance<ISelfServiceService>();




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


              
                ldMethod.Select("0");
               cc.Format = date.Format = effectiveDate.Format = _systemService.SessionHelper.GetDateformat();
                //if (string.IsNullOrEmpty(Request.QueryString["employeeId"]))
                //    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorOperation).Show();
                //CurrentEmployee.Text = Request.QueryString["employeeId"];

                //cc.Format = _systemService.SessionHelper.GetDateformat();
                if (!string.IsNullOrEmpty(Request.QueryString["_employeeId"]) && !string.IsNullOrEmpty(Request.QueryString["_loanId"]))
                {
                    var p1 = new Ext.Net.Parameter("id", Request.QueryString["_loanId"]);
                    var p2 = new Ext.Net.Parameter("type", "imgEdit");
                    var col = new Ext.Net.ParameterCollection();
                    col.Add(p1);
                    col.Add(p2);
                    PoPuP(null, new DirectEventArgs(col));

                }
                //try
                //{
                //    AccessControlApplier.ApplyAccessControlOnPage(typeof(loanSelfService), BasicInfoTab, GridPanel1, btnAdd, SaveButton);

                //}
                //catch (AccessDeniedException exp)
                //{
                //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied, "closeCurrentTab()").Show();
                //    Viewport1.Hidden = true;
                //    return;
                //}
                //try

                //{
                //    AccessControlApplier.ApplyAccessControlOnPage(typeof(LoanComment), null, loanCommentGrid, null, Button1);
                //    ApplyAccessControlOnLoanComments();
                //}
                //catch (AccessDeniedException exp)
                //{

                //    caseCommentsTab.Hidden = true;

                //}
                if (purpose.InputType == InputType.Password)
                {
                    purpose.Visible = false;
                    purposeField.Visible = true;
                }
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
            SetTabPanelEnable(true);
            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    SelfServiceLoanRecordRequest r = new SelfServiceLoanRecordRequest();
                    r.LoanId = Convert.ToInt32(id);

                    RecordResponse<loanSelfService> response = _selfServiceService.ChildGetRecord<loanSelfService>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                         Common.errorMessage(response);
                        return;
                    }

                    currentCase.Text = id;

                    employeeId.GetStore().Add(new object[]
                       {
                                new
                                {
                                    recordId = response.result.employeeId,
                                    fullName =response.result.employeeName.fullName
                                }
                       });
                    employeeId.SetValue(response.result.employeeId);
                    effectiveDate.Disabled = response.result.status != 3;
                    //FillFilesStore(Convert.ToInt32(id));

                    //Step 2 : call setvalues with the retrieved object
                    this.BasicInfoTab.SetValues(response.result);
                    FillCurrency();
                    //FillBranchField();
                    FillLoanType();
                    ltId.Select(response.result.ltId.ToString());
                    CurrentAmountCurrency.Text = response.result.currencyRef;
                    currencyId.Select(response.result.currencyId.ToString());
                    status.Select(response.result.status.ToString());
                    ldMethod.Select(response.result.ldMethod.ToString());
                    //if (!string.IsNullOrEmpty(response.result.branchId))
                    //    branchId.Select(response.result.branchId);
                    //loanComments_RefreshData(Convert.ToInt32(id));
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


        [DirectMethod]
        public void DeleteRecord(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                loanSelfService s = new loanSelfService();
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

                PostRequest<loanSelfService> req = new PostRequest<loanSelfService>();
                req.entity = s;
                PostResponse<loanSelfService> r = _selfServiceService.ChildDelete<loanSelfService>(req);
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
                    //caseCommentStore.Remove(index);

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

        private void SetTabPanelEnable(bool isEnable)
        {
            foreach (var item in panelRecordDetails.Items)
            {
                if (item.ID == "BasicInfoTab")
                    continue;
                item.Disabled = !isEnable;
            }

        }

        protected void ADDNewRecord(object sender, DirectEventArgs e)
        {
            BasicInfoTab.Reset();
            SystemDefaultRecordRequest req = new SystemDefaultRecordRequest();
            req.Key = "ldMethod";
            RecordResponse<KeyValuePair<string, string>> defaults = _systemService.ChildGetRecord<KeyValuePair<string, string>>(req);
            if (!defaults.Success)
            {
                Common.errorMessage(defaults);
                return;
            }
          
                ldMethod.Select(defaults.result.Value);
          
            req.Key = "ldValue";
            RecordResponse<KeyValuePair<string, string>> ldValueResponse = _systemService.ChildGetRecord<KeyValuePair<string, string>>(req);
            if (!ldValueResponse.Success)
            {
                Common.errorMessage(ldValueResponse);
                return;
            }
           
                ldValue.Text = ldValueResponse.result.Value;

            //Reset all values of the relative object

            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;
            date.SelectedDate=effectiveDate.SelectedDate = DateTime.Now;
            panelRecordDetails.ActiveIndex = 0;
            SetTabPanelEnable(false);
            FillLoanType();
            //FillBranchField();
            FillCurrency();
          
            RecordRequest req1 = new RecordRequest();
            req1.RecordID = _systemService.SessionHelper.GetEmployeeId(); 
            RecordResponse<MyInfo> r = _selfServiceService.ChildGetRecord<MyInfo>(req1);
            if (!r.Success)//it maybe be another condition
            {
                //Show an error saving...
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
              Common.errorMessage(r);
                return;
            }

            branchId.Select(r.result.branchId);
            effectiveDate.Disabled = false;
            this.EditRecordWindow.Show();
        }

     

    
      


        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            //ListRequest request = new ListRequest();
            LoanManagementListRequest request = new LoanManagementListRequest();
            request.BranchId = 0;
            request.DepartmentId = 0;
            request.DivisionId = 0;
            request.EmployeeId =Convert.ToInt32( _systemService.SessionHelper.GetEmployeeId());
            request.Status = 0;
            request.Filter = "";

            request.SortBy = e.Sort[0].Property;
            if (e.Sort[0].Property == "employeeName")
                request.SortBy = _systemService.SessionHelper.GetNameformat();


          
            request.Size = e.Limit.ToString();
            request.StartAt = e.Start.ToString();
            ListResponse<loanSelfService> routers = _selfServiceService.ChildGetAll<loanSelfService>(request);
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
            loanSelfService b = JsonConvert.DeserializeObject<loanSelfService>(obj);
             string   ldMethod= e.ExtraParams["ldMethod"];
            string id = e.ExtraParams["id"];
            if (b.ldMethod==null)
            {
                X.MessageBox.Alert(GetGlobalResourceObject("Common", "Error").ToString() , GetGlobalResourceObject("Errors", "emptyLdMethod").ToString()).Show();
                return;
            }

            //if (string.IsNullOrEmpty(ldMethod)
            //b.ldMethod =Convert.ToInt16( ldMethod);
            // Define the object to add or edit as null

         
            //if (ldMethodCom.SelectedItem != null)
            //    b.ldMethod = ldMethodCom.SelectedItem.Value; 
           
            if (date.ReadOnly)
                b.date = DateTime.Now;
            //b.effectiveDate = new DateTime(b.effectiveDate.Year, b.effectiveDate.Month, b.effectiveDate.Day, 14, 0, 0);
          
            if (branchId.SelectedItem != null)
            {
                b.branchName = branchId.SelectedItem.Text;
            }
           

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<loanSelfService> request = new PostRequest<loanSelfService>();
                    request.entity = b;
                    request.entity.employeeId = _systemService.SessionHelper.GetEmployeeId();
                    request.entity.employeeName = new EmployeeName() { fullName="" };
                    request.entity.date = DateTime.Now;
                    //request.entity.ltName = "";
                    //request.entity.currencyRef = "";
                  
               
                
                    request.entity.status = 1; 
                    PostResponse<loanSelfService> r = _selfServiceService.ChildAddOrUpdate<loanSelfService>(request);
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
                        Store1.Reload();
                        //b.recordId = r.recordId;
                        
                        ////Add this record to the store 
                        //this.Store1.Insert(0, b);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });
                        recordId.Text = b.recordId;
                        SetTabPanelEnable(true);
                        currentCase.Text = b.recordId;

                        RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;
                        sm.DeselectAll();
                        sm.Select(b.recordId.ToString());

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
                    PostRequest<loanSelfService> request = new PostRequest<loanSelfService>();
                    request.entity = b;
                    request.entity.employeeId = _systemService.SessionHelper.GetEmployeeId();
                 
                   
                   
                    PostResponse<loanSelfService> r = _selfServiceService.ChildAddOrUpdate<loanSelfService>(request);                    //Step 1 Selecting the object or building up the object for update purpose

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

                       // ModelProxy record = this.Store1.GetById(id);
                       // BasicInfoTab.UpdateRecord(record);
                       // record.Set("currencyRef", b.currencyRef);
                       // if (date.ReadOnly)
                       //     record.Set("date", null);

                       // record.Set("employeeName", b.employeeName);

                       //// record.Set("branchName", b.branchName);

                       // record.Commit();
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
            int bulk;

            req.entity = note;

            PostResponse<LoanComment> resp = _loanService.ChildAddOrUpdate<LoanComment>(req);
            if (!resp.Success)
            {
               Common.errorMessage(resp);
                return new { valid = false };
            }
            //loanComments_RefreshData(note.loanId);
            return new { valid = true };
        }


    }
}