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
using AionHR.Services.Messaging.Asset_Management;
using AionHR.Model.AssetManagement;

namespace AionHR.Web.UI.Forms
{
    public partial class AssetManagementOnboardings : System.Web.UI.Page
    {

        ICompanyStructureService _branchService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        IPayrollService _payrollService = ServiceLocator.Current.GetInstance<IPayrollService>();
        IAssetManagementService _assetManagementService = ServiceLocator.Current.GetInstance<IAssetManagementService>();


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
                try
                {
                    //AccessControlApplier.ApplyAccessControlOnPage(), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
                }
                CurrentPositionId.Text = "";
                categoryIdFilter.ADDHandler("FocusLeave", "App.OnboardingStore.reload();");
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

            deliveryDuration.Hidden = false;
            int id = Convert.ToInt32(e.ExtraParams["id"]);
            CurrentPositionId.Text = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    OnboardingStore.Reload();
                    this.EditRecordWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditRecordWindow.Show();
                    break;

                //case "imgDelete":
                //    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                //    {
                //        Yes = new MessageBoxButtonConfig
                //        {
                //            //We are call a direct request metho for deleting a record
                //            Handler = String.Format("App.direct.DeleteRecord({0})", id),
                //            Text = Resources.Common.Yes
                //        },
                //        No = new MessageBoxButtonConfig
                //        {
                //            Text = Resources.Common.No
                //        }

                //    }).Show();
                //    break;

                //case "colAttach":

                //    //Here will show up a winow relatice to attachement depending on the case we are working on
                //    break;
                default:
                    break;
            }


        }
        protected void PoPuPOB(object sender, DirectEventArgs e)
        {

           
                          
            string positionId = e.ExtraParams["positionId"];
            string categoryId = e.ExtraParams["categoryId"];
            string delivery = e.ExtraParams["deliveryDuration"];
            string type = e.ExtraParams["type"];
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 

                    //Step 2 : call setvalues with the retrieved object
                    AssetCategoryControl.setCategory(categoryId);
                    if (!string.IsNullOrEmpty(delivery))
                        deliveryDuration.Value = Convert.ToInt32(delivery);
                    this.EditRecordWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditOnBoardingRecordWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteRecord({0})", categoryId),
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
                AssetManagementOnBoarding s = new AssetManagementOnBoarding();
                s.categoryId = index;
                s.positionId = CurrentPositionId.Text;
                PostRequest<AssetManagementOnBoarding> req = new PostRequest<AssetManagementOnBoarding>();
                req.entity = s;
                PostResponse<AssetManagementOnBoarding> r = _assetManagementService.ChildDelete<AssetManagementOnBoarding>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(r); ;
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    OnboardingStore.Reload();

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
         //   CurrentPositionId.Text = "";
            //Reset all values of the relative object
            OnBoardingTab.Reset();
            deliveryDuration.Hidden = true;
            this.EditOnBoardingRecordWindow.Title = Resources.Common.AddNewRecord;

            this.EditOnBoardingRecordWindow.Show();
        }
      
         protected void OnboardingStore_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            AssetManagementOnBoardingListRequest request = new AssetManagementOnBoardingListRequest();
            // request.Filter = "";
            request.positionId = string.IsNullOrEmpty(CurrentPositionId.Text) ? "0" : CurrentPositionId.Text;
            request.categoryId = categoryIdFilter.GetCategoryId();
            request.Size = e.Limit.ToString();
            request.StartAt = e.Start.ToString();
        
            ListResponse<AssetManagementOnBoarding> resp = _assetManagementService.ChildGetAll<AssetManagementOnBoarding>(request);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            e.Total = resp.count;
            this.OnboardingStore.DataSource = resp.Items;


            this.OnboardingStore.DataBind();
        }
        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            PositionListRequest request = new PositionListRequest();
            // request.Filter = "";
            request.SortBy = "positionRef";
            request.Size = e.Limit.ToString();
            request.StartAt = e.Start.ToString();
           
            ListResponse<Model.Company.Structure.Position> branches = _branchService.ChildGetAll<Model.Company.Structure.Position>(request);
            if (!branches.Success)
                return;
            e.Total = branches.count;
            this.Store1.DataSource = branches.Items;


            this.Store1.DataBind();
        }




        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
           

            string obj = e.ExtraParams["values"];
           AssetManagementOnBoarding b = JsonConvert.DeserializeObject<AssetManagementOnBoarding>(obj);
            if (string.IsNullOrEmpty(CurrentPositionId.Text))
                return;
            //if (tsId.SelectedItem != null)
            //    b.tsName = tsId.SelectedItem.Text;
            b.categoryId = AssetCategoryControl.GetCategoryId();
            b.positionId = CurrentPositionId.Text;
            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(b.categoryId))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<AssetManagementOnBoarding> request = new PostRequest<AssetManagementOnBoarding>();
                    request.entity = b;
                    PostResponse<AssetManagementOnBoarding> r = _branchService.ChildAddOrUpdate<AssetManagementOnBoarding>(request);
                

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
                        this.OnboardingStore.Reload();

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        this.EditOnBoardingRecordWindow.Close();
                      



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
                  
                    PostRequest<AssetManagementOnBoarding> request = new PostRequest<AssetManagementOnBoarding>();
                    request.entity = b;
                    PostResponse<AssetManagementOnBoarding> r = _assetManagementService.ChildAddOrUpdate<AssetManagementOnBoarding>(request);                   //Step 1 Selecting the object or building up the object for update purpose

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


                        OnboardingStore.Reload(); 
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });
                        this.EditOnBoardingRecordWindow.Close();


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

      

      
    }
}