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
using AionHR.Infrastructure.Session;
using AionHR.Model.System;
using AionHR.Model.Employees.Profile;
using AionHR.Infrastructure.JSON;
using AionHR.Model.Attributes;
using AionHR.Model.Access_Control;
using AionHR.Model.Attendance;
using AionHR.Web.UI.Forms.ConstClasses;
using AionHR.Model.AssetManagement;
using AionHR.Services.Messaging.Asset_Management;
using AionHR.Services.Messaging.System;

namespace AionHR.Web.UI.Forms
{
    public partial class AssetManagementPurchaseOrders : System.Web.UI.Page
    {
        IAssetManagementService _assetManagementService = ServiceLocator.Current.GetInstance<IAssetManagementService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService= ServiceLocator.Current.GetInstance<ICompanyStructureService>();


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
            try {

                if (!X.IsAjaxRequest && !IsPostBack)
                {

                    SetExtLanguage();
                    HideShowButtons();
                    HideShowColumns();

                    //releaseDate.Format = expiryDate.Format = releaseDateDF.Format = expiryDateDF.Format = _systemService.SessionHelper.GetDateformat();

                    WidgetColumn1.Format= WidgetColumn2.Format= depreciationDate.Format=warrantyEndDate.Format= date.Format = Coldate.Format = _systemService.SessionHelper.GetDateformat();
                    if (_systemService.SessionHelper.CheckIfIsAdmin())
                        return;
                    try
                    {
                        AccessControlApplier.ApplyAccessControlOnPage(typeof(AssetManagementPurchaseOrder), BasicInfoTab, GridPanel1, btnAdd, SaveButton);

                    }
                    catch (AccessDeniedException exp)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                        Viewport1.Hidden = true;
                        return;
                    }
                    FillBranch();
                    FillDepartment();
                    currentPurchaseOrderId.Text = "";

                }
            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }


        }

        private void FillBranch()
        {

            ListRequest branchesRequest = new ListRequest();
            ListResponse<Branch> resp = _companyStructureService.ChildGetAll<Branch>(branchesRequest);
            if (!resp.Success)
                Common.errorMessage(resp);
            branchStore.DataSource = resp.Items;
            branchStore.DataBind();
            if (_systemService.SessionHelper.CheckIfIsAdmin())
                return;

        }
        private void FillDepartment()
        {
            DepartmentListRequest departmentsRequest = new DepartmentListRequest();
            departmentsRequest.type = 0;
            ListResponse<Department> resp = _companyStructureService.ChildGetAll<Department>(departmentsRequest);
            if (!resp.Success)
                Common.errorMessage(resp);
            departmentStore.DataSource = resp.Items;
            departmentStore.DataBind();
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

        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);



            List<Employee> data = GetEmployeesFiltered(prms.Query);
            if (data == null)
                data = new List<Employee>();
            data.ForEach(s => { s.fullName = s.name.fullName; });
            //  return new
            // {
            return data;
            //};

        }
        private List<Employee> GetEmployeesFiltered(string query)
        {

            EmployeeListRequest req = new EmployeeListRequest();

            req.DepartmentId = "0";

            req.BranchId = "0";
            req.IncludeIsInactive = 0;
            req.SortBy = _systemService.SessionHelper.GetNameformat();

            req.StartAt = "0";
            req.Size = "20";
            req.Filter = query;




            ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
            return response.Items;
        }


        protected void PoPuP(object sender, DirectEventArgs e)
        {
            FillBranch();
            FillDepartment();
      
            BasicInfoTab.Reset();
            panelRecordDetails.ActiveIndex = 0;
            int id = Convert.ToInt32(e.ExtraParams["id"]);
            currentPurchaseOrderId.Text = id.ToString();
          
         
            string type = e.ExtraParams["type"];
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id.ToString();
                    RecordResponse<AssetManagementPurchaseOrder> response = _assetManagementService.ChildGetRecord<AssetManagementPurchaseOrder>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object



                    employeeId.GetStore().Add(new object[]
                         {
                                        new
                                        {
                                            recordId =response.result.employeeId,
                                            fullName =response.result.employeeName

                                        }
                         });

                    employeeId.SetValue(response.result.employeeId);
                    this.BasicInfoTab.SetValues(response.result);
                    supplierId.setSupplier(response.result.supplierId);
                    categoryId.setCategory(response.result.categoryId);
                  
                       if(!string.IsNullOrEmpty(response.result.currencyId))
                    CurrencyControl.setCurrency(response.result.currencyId);


                    //if (response.result.status == null)
                    //{
                    //    status.Select("1");
                    //    status.SetValue("1");
                    //}
                    apStatus.setApprovalStatus("1");

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
                AssetManagementPurchaseOrder n = new AssetManagementPurchaseOrder();
                n.recordId = index;
               

                PostRequest<AssetManagementPurchaseOrder> req = new PostRequest<AssetManagementPurchaseOrder>();
                req.entity = n;
                PostResponse<AssetManagementPurchaseOrder> res = _assetManagementService.ChildDelete<AssetManagementPurchaseOrder>(req);
                if (!res.Success)
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(res);
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
        protected void ADDNewRecord(object sender, DirectEventArgs e)
        {
            
            panelRecordDetails.ActiveIndex = 0;
            //Reset all values of the relative object
            BasicInfoTab.Reset();
            CurrencyControl.setCurrency(_systemService.SessionHelper.GetDefaultCurrency());
            FillBranch();
            FillDepartment();
            if (!string.IsNullOrEmpty(_systemService.SessionHelper.GetEmployeeId()))
            {
                EmployeeQuickViewRecordRequest req = new EmployeeQuickViewRecordRequest();
                req.RecordID = _systemService.SessionHelper.GetEmployeeId();
                req.asOfDate = DateTime.Now;
                RecordResponse<EmployeeQuickView> resp = _employeeService.ChildGetRecord<EmployeeQuickView>(req);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }
                branchId.Select(resp.result.branchId);
                departmentId.Select(resp.result.departmentId);
                employeeId.GetStore().Add(new object[]
                      {
                                        new
                                        {
                                            recordId =_systemService.SessionHelper.GetEmployeeId(),
                                            fullName =resp.result.name

                                        }
                      });


            }
            employeeId.SetValue(_systemService.SessionHelper.GetEmployeeId());
            status.Select("1");
            status.SetValue("1");
            qty.SetValue(1);
            currentPurchaseOrderId.Text = "";
            date.SelectedDate  = DateTime.Now;
            apStatus.setApprovalStatus("1");
            status.SetValue("1");
            status.Select("1");
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;
           

            //timeZoneCombo.Select(timeZoneOffset.Text);
            this.EditRecordWindow.Show();
        }
       
        private AssetManagementPurchaseOrdersListRequest getRequest()
        {
            var d = jobInfo1.GetJobInfo();
            AssetManagementPurchaseOrdersListRequest request = new AssetManagementPurchaseOrdersListRequest();
            request.apStatus = apStatusFilter.GetApprovalStatus();
            request.branchId = d.BranchId.HasValue ? d.BranchId.Value.ToString() : "0";
            request.status = string.IsNullOrEmpty(statusFilter.Value.ToString())? "0" : statusFilter.Value.ToString() ;
            request.supplierId = supplierIdFilter.GetSupplierId();
            request.categoryId = categoryIdFilter.GetCategoryId();
            return request;
        }


        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            AssetManagementPurchaseOrdersListRequest request = getRequest(); 

            request.Filter = "";
            ListResponse<AssetManagementPurchaseOrder> resp = _assetManagementService.ChildGetAll<AssetManagementPurchaseOrder>(request);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            this.Store1.DataSource = resp.Items;
            e.Total = resp.count;

            this.Store1.DataBind();
        }




        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];

            AssetManagementPurchaseOrder b = JsonConvert.DeserializeObject<AssetManagementPurchaseOrder>(obj);
            b.supplierId = supplierId.GetSupplierId() == "0" ? null : supplierId.GetSupplierId();
            b.categoryId = categoryId.GetCategoryId();
            b.currencyId = CurrencyControl.getCurrency()=="0"?null : CurrencyControl.getCurrency();
            b.apStatus = apStatus.GetApprovalStatus() == "0" ? null : apStatus.GetApprovalStatus();

            b.recordId = id;
            // Define the object to add or edit as null
           
           
         
              
            

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<AssetManagementPurchaseOrder> request = new PostRequest<AssetManagementPurchaseOrder>();
                    request.entity = b;
                    PostResponse<AssetManagementPurchaseOrder> resp = _assetManagementService.ChildAddOrUpdate<AssetManagementPurchaseOrder>(request);
                    b.recordId = resp.recordId;

                    //check if the insert failed
                    if (!resp.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(resp);
                        return;
                    }
                    else
                    {
                        Store1.Reload();
                        this.EditRecordWindow.Close();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });
                        //Add this record to the store 
                       
                       


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
                  
                    PostRequest<AssetManagementPurchaseOrder> request = new PostRequest<AssetManagementPurchaseOrder>();
                    b.recordId = id;
                    request.entity = b;
                    PostResponse<AssetManagementPurchaseOrder> r = _assetManagementService.ChildAddOrUpdate<AssetManagementPurchaseOrder>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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

                        //    this.EditRecordWindow.Close();

                    }
                    AssetPOReception POReception = JsonConvert.DeserializeObject<AssetPOReception>(obj);
                    POReception.recordId = id;
                    POReception.supplierId = supplierId.GetSupplierId() == "0" ? null : supplierId.GetSupplierId();
                    POReception.categoryId = categoryId.GetCategoryId();
                    POReception.currencyId = CurrencyControl.getCurrency() == "0" ? null : CurrencyControl.getCurrency();
                    POReception.apStatus = apStatus.GetApprovalStatus() == "0" ? null : apStatus.GetApprovalStatus();
                    if (b.status == 2)
                    {
                        PostRequest<AssetPOReception> req = new PostRequest<AssetPOReception>();
                        req.entity = POReception;
                        PostResponse<AssetPOReception> resp = _assetManagementService.ChildAddOrUpdate<AssetPOReception>(req);
                        if (!resp.Success)//it maybe another check
                        {

                            Common.errorMessage(resp);
                            return;
                        }
                        AssetManagementAssetListRequest request1 = new AssetManagementAssetListRequest();
                       
                        request1.branchId ="0";
                        request1.departmentId = "0";
                        request1.positionId =  "0";
                        request1.categoryId = "0";
                        request1.employeeId = "0";
                        request1.supplierId = "0";
                        request1.PurchaseOrderId = b.poRef;
                        request1.Filter = "";
                        ListResponse<AssetManagementAsset> resp1 = _assetManagementService.ChildGetAll<AssetManagementAsset>(request1);
                        if (!resp1.Success)//it maybe another check
                        {

                            Common.errorMessage(resp1);
                            return;
                        }
                        FillCondition();
                        AssetPOReceptionStore.DataSource = resp1.Items;
                        AssetPOReceptionStore.DataBind();
                        
                        AssetPOReceptionWindow.Show();
                    }

                }
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }
            }
           


        }


        private void FillCondition()
        {
            conditionStore.DataSource = Common.XMLDictionaryList(_systemService, "10");
            conditionStore.DataBind();
        }
        protected void SaveAssetPOReception(object sender, DirectEventArgs e)
        {
             try
            {
                List<AssetManagementAsset> values = JsonConvert.DeserializeObject<List<AssetManagementAsset>>(e.ExtraParams["values"]);
                PostRequest<AssetManagementAsset> request = new PostRequest<AssetManagementAsset>();
                values.ForEach(x =>
                {

                    request.entity = x;
                    PostResponse<AssetManagementAsset> resp = _assetManagementService.ChildAddOrUpdate<AssetManagementAsset>(request);
                    if (!resp.Success)
                    {
                        Common.errorMessage(resp);
                       

                    }
                });
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordSavingSucc
                });
            }
            catch(Exception exp)
            {
                if (!string.IsNullOrEmpty( exp.Message))
                X.MessageBox.Alert(GetGlobalResourceObject("Common","Error").ToString(), exp.Message); 

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





        protected void ApprovalsStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            AssetManagementPurchaseOrderApprovalListRequest req = new AssetManagementPurchaseOrderApprovalListRequest();
            req.poId = currentPurchaseOrderId.Text;
          
            if (string.IsNullOrEmpty(req.poId))
            {
                ApprovalStore.DataSource = new List<AssetManagementPurchaseOrderApproval>();
                ApprovalStore.DataBind();
            }
            req.approverId = 0;
            req.BranchId = "0";
            req.DepartmentId = "0";

            req.Status = 0;
            req.Filter = "";
          

         
            ListResponse<AssetManagementPurchaseOrderApproval> response = _assetManagementService.ChildGetAll<AssetManagementPurchaseOrderApproval>(req);

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