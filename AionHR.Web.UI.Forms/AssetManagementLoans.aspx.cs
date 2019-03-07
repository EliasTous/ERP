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
using AionHR.Infrastructure.Domain;
using AionHR.Model.Access_Control;
using AionHR.Web.UI.Forms.ConstClasses;
using AionHR.Model.AssetManagement;
using AionHR.Services.Messaging.Asset_Management;
using AionHR.Model.Employees.Profile;

namespace AionHR.Web.UI.Forms
{
    public partial class AssetManagementLoans : System.Web.UI.Page
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
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!X.IsAjaxRequest && !IsPostBack)
            {

                SetExtLanguage();
                HideShowButtons();
                HideShowColumns();
             
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
                ColDate.Format = date.Format = _systemService.SessionHelper.GetDateformat();

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

     

      
       
        
        protected void PoPuP(object sender, DirectEventArgs e)
        {
           
            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
          
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id.ToString();
                    RecordResponse<AssetManagementLoan> response = _assetManagementService.ChildGetRecord<AssetManagementLoan>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                  
                    this.BasicInfoTab.SetValues(response.result);
                    apId.setApprovalStatus(response.result.status.ToString());
                    assetId.setAsset(response.result.assetId);
                    employeeId.GetStore().Add(new object[]
                         {
                                        new
                                        {
                                            recordId = response.result.employeeId,
                                            fullName =response.result.employeeName.fullName

                                        }
                         });

                    employeeId.SetValue(response.result.employeeId);



                    //  status.Select( XMLStatus.Where(x => x.key == response.result.status).Count() != 0 ? XMLStatus.Where(x => x.key == response.result.status).First().value.ToString() : string.Empty);

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
                AssetManagementLoan n = new AssetManagementLoan();
                n.recordId = index;




                PostRequest<AssetManagementLoan> req = new PostRequest<AssetManagementLoan>();
                req.entity = n;
                PostResponse<AssetManagementLoan> res = _assetManagementService.ChildDelete<AssetManagementLoan>(req);
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
      

        /// <summary>
        /// Adding new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ADDNewRecord(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
           
            BasicInfoTab.Reset();
            apId.setApprovalStatus("1");
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;


            this.EditRecordWindow.Show();
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

      

        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            AssetManagementLoanListRequest request = new AssetManagementLoanListRequest();
            var d = jobInfo1.GetJobInfo();
            request.branchId = d.BranchId.HasValue ? d.BranchId.Value.ToString() : "0";
            request.departmentId = d.DepartmentId.HasValue ? d.DepartmentId.Value.ToString() : "0";
            request.positionId = d.PositionId.HasValue ? d.PositionId.Value.ToString() : "0";
            request.categoryId = categoryIdFilter.GetCategoryId();
            request.employeeId = employeeFilter.GetEmployee().employeeId.ToString();
            request.assetId = AssetFilter.GetAssetId();
            request.Filter = "";
            ListResponse<AssetManagementLoan> resp = _assetManagementService.ChildGetAll<AssetManagementLoan>(request);
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

            try
            {
                //Getting the id to check if it is an Add or an edit as they are managed within the same form.
                string id = e.ExtraParams["id"];
               

                string obj = e.ExtraParams["values"];
                AssetManagementLoan b = JsonConvert.DeserializeObject<AssetManagementLoan>(obj);
                if (!string.IsNullOrEmpty(apId.GetApprovalStatus()))
                    b.status = Convert.ToInt16(apId.GetApprovalStatus());
                b.assetId = assetId.GetAssetId();
             
             

                // Define the object to add or edit as null

                if (string.IsNullOrEmpty(id))
                {

                    try
                    {
                        //New Mode
                        //Step 1 : Fill The object and insert in the store 
                        PostRequest<AssetManagementLoan> request = new PostRequest<AssetManagementLoan>();
                        request.entity = b;
                        PostResponse<AssetManagementLoan> r = _assetManagementService.ChildAddOrUpdate<AssetManagementLoan>(request);
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

                            this.EditRecordWindow.Close();
                            Store1.Reload();



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
                        PostRequest<AssetManagementLoan> request = new PostRequest<AssetManagementLoan>();
                        request.entity = b;
                        PostResponse<AssetManagementLoan> r = _assetManagementService.ChildAddOrUpdate<AssetManagementLoan>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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
                        


                        }

                    }
                    catch (Exception ex)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                    }
                }
            }
            catch (Exception exp)
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