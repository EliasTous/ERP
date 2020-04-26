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
using Services.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Web.UI.Forms.Utilities;
using Model.Company.News;
using Services.Messaging;
using Model.Company.Structure;
using Model.System;
using Infrastructure.Domain;
using Model.Access_Control;
using Web.UI.Forms.ConstClasses;
using Model.AssetManagement;
using Services.Messaging.Asset_Management;
using Model.Employees.Profile;

namespace Web.UI.Forms
{
    public partial class AssetManagementAssets : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IAssetManagementService _assetManagementService = ServiceLocator.Current.GetInstance<IAssetManagementService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();



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
                FillCurrency();
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(AssetManagementAsset), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
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

        protected void addCurrency(object sender, DirectEventArgs e)
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
        private void FillStatus()
        {
            statusStore.DataSource = Common.XMLDictionaryList(_systemService, "9");
            statusStore.DataBind();
        }
        private void FillCondition()
        {
            conditionStore.DataSource = Common.XMLDictionaryList(_systemService, "10");
            condition.DataBind();
        }
        protected void PoPuP(object sender, DirectEventArgs e)
        {
            branchId.ReadOnly = true;
            disposedDate.Hidden = true;
            BasicInfoTab.Reset();
            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
            FillCurrency();
            FillBranch();
            Panel8.Disabled = false;
            panelRecordDetails.ActiveIndex = 0;
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id.ToString();
                    RecordResponse<AssetManagementAsset> response = _assetManagementService.ChildGetRecord<AssetManagementAsset>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    FillStatus();
                    FillCondition();
                    FillCategories();
                    this.BasicInfoTab.SetValues(response.result);
                    if (response.result.status == 4)
                    {
                        SetBasicInfoFormEnable(false);
                        disposedDate.Hidden = false;
                    }
                    else
                    {
                        SetBasicInfoFormEnable(true);
                        disposedDate.Hidden = true;
                    }
                    
                   
                  //  status.Select( XMLStatus.Where(x => x.key == response.result.status).Count() != 0 ? XMLStatus.Where(x => x.key == response.result.status).First().value.ToString() : string.Empty);
                    supplierId.setSupplier(response.result.supplierId);
                   
                    employeeFullName.Text = response.result.employeeName;
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
                AssetManagementAsset n = new AssetManagementAsset();
                n.recordId = index;
             



                PostRequest<AssetManagementAsset> req = new PostRequest<AssetManagementAsset>();
                req.entity = n;
                PostResponse<AssetManagementAsset> res = _assetManagementService.ChildDelete<AssetManagementAsset>(req);
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

            //Reset all values of the relative object
            branchId.ReadOnly = false;
            disposedDate.Hidden = true;
            FillCurrency();
            FillBranch();
            FillStatus();
            FillCondition();
            FillCategories();
            Panel8.Disabled = true;
            SetBasicInfoFormEnable(true);
            BasicInfoTab.Reset();
            panelRecordDetails.ActiveIndex = 0;
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;


            this.EditRecordWindow.Show();
        }
      
        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            return Common.GetEmployeesFiltered(prms.Query);

        }
        private string GetNameFormat()
        {
            return _systemService.SessionHelper.Get("nameFormat").ToString();
        }

        private void SetBasicInfoFormEnable(bool isEnable)
        {
            foreach (var item in BasicInfoTab.Items)
            {
                if (item.GetType() == typeof(FieldSet))
                    continue;
                item.Disabled = !isEnable;
                
            }
           
            categoryId.Disabled= !isEnable;
            supplierId.disabled= !isEnable;

            SaveButton.Disabled = !isEnable;
        }

        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            AssetManagementAssetListRequest request = new AssetManagementAssetListRequest();
            var d = jobInfo1.GetJobInfo();
            request.branchId = d.BranchId.HasValue ? d.BranchId.Value.ToString() : "0";
            request.departmentId = d.DepartmentId.HasValue ? d.DepartmentId.Value.ToString() : "0";
            request.positionId = d.PositionId.HasValue ? d.PositionId.Value.ToString() : "0";
            request.categoryId = categoryIdFilter.GetCategoryId();
            request.employeeId = employeeFilter.GetEmployee().employeeId.ToString();
            request.supplierId = supplierIdFilter.GetSupplierId();
            request.Filter = "";
            ListResponse<AssetManagementAsset> resp = _assetManagementService.ChildGetAll<AssetManagementAsset>(request);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            this.Store1.DataSource = resp.Items;
            e.Total = resp.count;

            this.Store1.DataBind();
        }

        private void FillCategories()
        {
            ListRequest req = new ListRequest();
            ListResponse<AssetManagementCategory> resp = _assetManagementService.ChildGetAll<AssetManagementCategory>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(resp);
                return;
            }

            categoryIdStore.DataSource = resp.Items;
            categoryIdStore.DataBind();
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
        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {

            try
            {
                //Getting the id to check if it is an Add or an edit as they are managed within the same form.
                string id = e.ExtraParams["id"];
                string disposedDate = e.ExtraParams["disposedDate"];

                string obj = e.ExtraParams["values"];
                AssetManagementAsset b = JsonConvert.DeserializeObject<AssetManagementAsset>(obj);
                if (!string.IsNullOrEmpty(disposedDate))
                    b.depreciationDate = DateTime.Parse(disposedDate);
                b.recordId = id;
                b.supplierId = supplierId.GetSupplierId()=="0"?null: supplierId.GetSupplierId();
                

                // Define the object to add or edit as null

                if (string.IsNullOrEmpty(id))
                {

                    try
                    {
                        //New Mode
                        //Step 1 : Fill The object and insert in the store 
                        PostRequest<AssetManagementAsset> request = new PostRequest<AssetManagementAsset>();
                        request.entity = b;
                        PostResponse<AssetManagementAsset> r = _assetManagementService.ChildAddOrUpdate<AssetManagementAsset>(request);
                        b.recordId = r.recordId;

                        //check if the insert failed
                        if (!r.Success)//it maybe be another condition
                        {
                            //Show an error saving...

                            Common.errorMessage(r);
                            return;
                        }
                        else
                        {

                            //Add this record to the store 
                        

                            //Display successful notification
                            Notification.Show(new NotificationConfig
                            {
                                Title = Resources.Common.Notification,
                                Icon = Icon.Information,
                                Html = Resources.Common.RecordSavingSucc
                            });

                            
                            Store1.Reload();
                            Panel8.Disabled = false;
                            currentAsset.Text = r.recordId;
                            X.Call("setUrlAfterNewAsset");
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
                        PostRequest<AssetManagementAsset> request = new PostRequest<AssetManagementAsset>();
                        request.entity = b;
                        PostResponse<AssetManagementAsset> r = _assetManagementService.ChildAddOrUpdate<AssetManagementAsset>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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
                            Store1.Reload();


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
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
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

        


    }
}