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

namespace AionHR.Web.UI.Forms
{
    public partial class AssetManagementCategories : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IAssetManagementService _assetManagementService = ServiceLocator.Current.GetInstance<IAssetManagementService>();
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
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(AssetManagementCategory), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
                }
                FillParent();
                currentCategory.Text = "";

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
            currentCategory.Text = id.ToString();
            PropertiesStore.Reload();
            panelRecordDetails.ActiveIndex = 0;
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id.ToString();
                    RecordResponse<AssetManagementCategory> response = _assetManagementService.ChildGetRecord<AssetManagementCategory>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    FillParent();
                    ApprovalStore.Reload();
                    this.BasicInfoTab.SetValues(response.result);
                    
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


        protected void PoPuPCP(object sender, DirectEventArgs e)
        {


         
            string propertyId = e.ExtraParams["propertyId"];
            string type = e.ExtraParams["type"];
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    AssetManagementCategoryPropertyRecordRequest r = new AssetManagementCategoryPropertyRecordRequest();
                    r.categoryId = currentCategory.Text;
                    r.propertyId = propertyId;
                    RecordResponse<AssetManagementCategoryProperty> response = _assetManagementService.ChildGetRecord<AssetManagementCategoryProperty>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                   
                    this.PropertiesForm.SetValues(response.result);

                    this.EditPropertyWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditPropertyWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeletePropertyRecord({0})",propertyId),
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
                AssetManagementCategory n = new AssetManagementCategory();
                n.recordId = index;
                n.name = "";
                n.deliveryDuration = 0;



                PostRequest<AssetManagementCategory> req = new PostRequest<AssetManagementCategory>();
                req.entity = n;
                PostResponse<AssetManagementCategory> res = _assetManagementService.ChildDelete<AssetManagementCategory>(req);
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
        [DirectMethod]
        public void DeletePropertyRecord(string propertyId)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                AssetManagementCategoryProperty n = new AssetManagementCategoryProperty();
                n.categoryId = currentCategory.Text;
                n.propertyId = propertyId;



                PostRequest<AssetManagementCategoryProperty> req = new PostRequest<AssetManagementCategoryProperty>();
                req.entity = n;
                PostResponse<AssetManagementCategoryProperty> res = _assetManagementService.ChildDelete<AssetManagementCategoryProperty>(req);
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
                    PropertiesStore.Reload();

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
        /// 
        protected void ADDNewRecord(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            currentCategory.Text = "";
            BasicInfoTab.Reset();
            FillParent();
            panelRecordDetails.ActiveIndex = 0;
            PropertiesStore.Reload();
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;
         

            this.EditRecordWindow.Show();
        }
        protected void ADDNewPropertyRecord(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object

            PropertiesForm.Reset();
          
            this.EditPropertyWindow.Title = Resources.Common.AddNewRecord;


            this.EditPropertyWindow.Show();
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
            ListResponse<AssetManagementCategory> resp = _assetManagementService.ChildGetAll<AssetManagementCategory>(request);
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
            AssetManagementCategory b = JsonConvert.DeserializeObject<AssetManagementCategory>(obj);
           
            b.recordId = id;
            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<AssetManagementCategory> request = new PostRequest<AssetManagementCategory>();
                    request.entity = b;
                    PostResponse<AssetManagementCategory> r = _assetManagementService.ChildAddOrUpdate<AssetManagementCategory>(request);
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
                    PostRequest<AssetManagementCategory> request = new PostRequest<AssetManagementCategory>();
                    request.entity = b;
                    PostResponse<AssetManagementCategory> r = _assetManagementService.ChildAddOrUpdate<AssetManagementCategory>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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
        protected void SaveNewPropertyRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            
            string propertyId = e.ExtraParams["categoryId"];
            
            string obj = e.ExtraParams["values"];
            AssetManagementCategoryProperty b = JsonConvert.DeserializeObject<AssetManagementCategoryProperty>(obj);
            b.categoryId = currentCategory.Text;
            b.propertyId = propertyId;
          
            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(propertyId))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<AssetManagementCategoryProperty> request = new PostRequest<AssetManagementCategoryProperty>();
                    request.entity = b;
                    PostResponse<AssetManagementCategoryProperty> r = _assetManagementService.ChildAddOrUpdate<AssetManagementCategoryProperty>(request);
                   

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
                        this.PropertiesStore.Reload();

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        this.EditPropertyWindow.Close();
                       


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
                  
                    PostRequest<AssetManagementCategoryProperty> request = new PostRequest<AssetManagementCategoryProperty>();
                    request.entity = b;
                    PostResponse<AssetManagementCategoryProperty> r = _assetManagementService.ChildAddOrUpdate<AssetManagementCategoryProperty>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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


                        PropertiesStore.Reload();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });
                        this.EditPropertyWindow.Close();


                    }

                }
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }
            }
        }


        private void FillParent()
        {
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<AssetManagementCategory> resp = _assetManagementService.ChildGetAll<AssetManagementCategory>(request);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            this.parentStore.DataSource = resp.Items;
         

            this.parentStore.DataBind();

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
        protected void ApprovalStory_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<Approval> routers = _companyStructureService.ChildGetAll<Approval>(request);

            if (!routers.Success)
                Common.errorMessage(routers);
            this.ApprovalStore.DataSource = routers.Items;
            e.Total = routers.Items.Count; ;

            this.ApprovalStore.DataBind();
        }
        protected void PropertiesStore_ReadData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            AssetManagementCategoryPropertyListRequest request = new AssetManagementCategoryPropertyListRequest();
            if (string.IsNullOrEmpty(currentCategory.Text))
                return;
            request.categoryId = currentCategory.Text;

            request.Filter = "";
            ListResponse<AssetManagementCategoryProperty> routers = _assetManagementService.ChildGetAll<AssetManagementCategoryProperty>(request);

            if (!routers.Success)
                Common.errorMessage(routers);
           
                           routers.Items.ForEach(x =>
            {
                switch (x.mask)
                {
                    case 1 : x.maskString = GetLocalResourceObject("TEXT").ToString();
                        break;
                    case 2:
                        x.maskString = GetLocalResourceObject("NUMERIC").ToString();
                        break;
                    case 3:
                        x.maskString = GetLocalResourceObject("DATE").ToString();
                        break;
                    case 4:
                        x.maskString = GetLocalResourceObject("DATE_TIME").ToString();
                        break;
                    case 5:
                        x.maskString = GetLocalResourceObject("CHECKBOX").ToString();
                        break;
                    default: x.maskString = "";
                        break;



                }
            });
            this.PropertiesStore.DataSource = routers.Items;
            e.Total = routers.Items.Count; ;

            this.PropertiesStore.DataBind();
        }
        
    }
}