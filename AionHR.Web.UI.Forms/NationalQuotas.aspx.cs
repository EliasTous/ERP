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
using AionHR.Model.NationalQuota;
using AionHR.Services.Messaging.NationalQuotas;

namespace AionHR.Web.UI.Forms
{
    public partial class NationalQuotas : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();

        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        INationalQuotaService _nationalQuotaService= ServiceLocator.Current.GetInstance<INationalQuotaService>();
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
                PointAcquisitionStore.Reload();
                FillBusinessSize();
                FillIndustry();
                bsId.Select(0);
                inId.Select(0);
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(Industry), IndustryFrom, IndustryGrid, btnIndustryAdd, SaveIndustryButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied, "closeCurrentTab()").Show();
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



        protected void IndustryPoPuP(object sender, DirectEventArgs e)
        {


            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id;

                    RecordResponse<Industry> response = _nationalQuotaService.ChildGetRecord<Industry>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).ToString() +"<br>"+GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary).Show();
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.IndustryFrom.SetValues(response.result);


                    this.EditIndustryWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditIndustryWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteIndustryRecord({0})", id),
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
        protected void BusinessPoPuP(object sender, DirectEventArgs e)
        {


            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id;

                    RecordResponse<BusinessSize> response = _nationalQuotaService.ChildGetRecord<BusinessSize>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).ToString() +"<br>"+GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary).Show();
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.businessForm.SetValues(response.result);


                    this.EditBusinessWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditBusinessWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteBusinessRecord({0})", id),
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
        protected void levelPoPuP(object sender, DirectEventArgs e)
        {


            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id;

                    RecordResponse<Level> response = _nationalQuotaService.ChildGetRecord<Level>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).ToString() +"<br>"+GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary).Show();
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.levelForm.SetValues(response.result);


                    this.EditLevelWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditLevelWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteLevelRecord({0})", id),
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
        protected void CitizenshipPoPuP(object sender, DirectEventArgs e)
        {


            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id;

                    RecordResponse<Citizenship> response = _nationalQuotaService.ChildGetRecord<Citizenship>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).ToString() +"<br>"+GetGlobalResourceObject("Errors", "ErrorLogId") + response.LogId : response.Summary).Show();
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.CitizenshipForm.SetValues(response.result);


                    this.EditCitizenshipWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditCitizenshipWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteCitizenshipRecord({0})", id),
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
        public void DeleteIndustryRecord(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                Industry s = new Industry();
                s.recordId = index;
                s.name = "";
                //s.intName = "";

                PostRequest<Industry> req = new PostRequest<Industry>();
                req.entity = s;
                PostResponse<Industry> r = _nationalQuotaService.ChildDelete<Industry>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+GetGlobalResourceObject("Errors","ErrorLogId") + r.LogId : r.Summary).Show();
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    IndustryStore.Remove(index);

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
        public void DeleteBusinessRecord(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                BusinessSize s = new BusinessSize();
                s.recordId = index;
                s.name = "";
                //s.intName = "";

                PostRequest<BusinessSize> req = new PostRequest<BusinessSize>();
                req.entity = s;
                PostResponse<BusinessSize> r = _nationalQuotaService.ChildDelete<BusinessSize>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+GetGlobalResourceObject("Errors","ErrorLogId") + r.LogId : r.Summary).Show();
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    BusinessStore.Remove(index);

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
        public void DeleteLevelRecord(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                Level s = new Level();
                s.recordId = index;
                s.name = "";
                //s.intName = "";

                PostRequest<Level> req = new PostRequest<Level>();
                req.entity = s;
                PostResponse<Level> r = _nationalQuotaService.ChildDelete<Level>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+GetGlobalResourceObject("Errors","ErrorLogId") + r.LogId : r.Summary).Show();
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    levelStore.Remove(index);

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
        public void DeleteCitizenshipRecord(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                Citizenship s = new Citizenship();
                s.recordId = index;
                s.name = "";
                //s.intName = "";

                PostRequest<Citizenship> req = new PostRequest<Citizenship>();
                req.entity = s;
                PostResponse<Citizenship> r = _nationalQuotaService.ChildDelete<Citizenship>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+GetGlobalResourceObject("Errors","ErrorLogId") + r.LogId : r.Summary).Show();
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    CitizenshipStore.Remove(index);

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
        public void DeletePointAcquisitionRecord(PointAcquisition p)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
               
                PostRequest<PointAcquisition> req = new PostRequest<PointAcquisition>();
                req.entity = p;
                PostResponse<PointAcquisition> r = _nationalQuotaService.ChildDelete<PointAcquisition>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+GetGlobalResourceObject("Errors","ErrorLogId") + r.LogId : r.Summary).Show();
                    return;
                }
                //else
                //{
                //    //Step 2 :  remove the object from the store
                //    citizenshipStore.Remove(index);

                //    //Step 3 : Showing a notification for the user 
                //    if (serverSide)
                //    {
                //        Notification.Show(new NotificationConfig
                //        {
                //            Title = Resources.Common.Notification,
                //            Icon = Icon.Information,
                //            Html = Resources.Common.RecordDeletedSucc
                //        });
                //    }
                //}

            }
            catch (Exception ex)
            {
                //In case of error, showing a message box to the user
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorDeletingRecord).Show();

            }

        }
        public void DeleteLevelAcquisitionRecord(LevelAcquisition p)
        {
            try
            {
                //Step 1 Code to delete the object from the database 

                PostRequest<LevelAcquisition> req = new PostRequest<LevelAcquisition>();
                req.entity = p;
                PostResponse<LevelAcquisition> r = _nationalQuotaService.ChildDelete<LevelAcquisition>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+GetGlobalResourceObject("Errors","ErrorLogId") + r.LogId : r.Summary).Show();
                    return;
                }
                //else
                //{
                //    //Step 2 :  remove the object from the store
                //    citizenshipStore.Remove(index);

                //    //Step 3 : Showing a notification for the user 
                //    if (serverSide)
                //    {
                //        Notification.Show(new NotificationConfig
                //        {
                //            Title = Resources.Common.Notification,
                //            Icon = Icon.Information,
                //            Html = Resources.Common.RecordDeletedSucc
                //        });
                //    }
                //}

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


            RowSelectionModel sm = this.IndustryGrid.GetSelectionModel() as RowSelectionModel;
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
                RowSelectionModel sm = this.IndustryGrid.GetSelectionModel() as RowSelectionModel;

                foreach (SelectedRow row in sm.SelectedRows)
                {
                    //Step 1 :Getting the id of the selected record: it maybe string 
                    int id = int.Parse(row.RecordID);


                    //Step 2 : removing the record from the store
                    //To do add code here 

                    //Step 3 :  remove the record from the store
                    IndustryStore.Remove(id);

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
        protected void ADDNewIndustryRecord(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            IndustryFrom.Reset();


            this.EditIndustryWindow.Title = Resources.Common.AddNewRecord;


            this.EditIndustryWindow.Show();
        }
        protected void ADDNewBusinessRecord(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            businessForm.Reset();


            this.EditBusinessWindow.Title = Resources.Common.AddNewRecord;


            this.EditBusinessWindow.Show();
        }
        protected void ADDNewCitizenshipRecord(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            CitizenshipForm.Reset();


            this.EditCitizenshipWindow.Title = Resources.Common.AddNewRecord;


            this.EditCitizenshipWindow.Show();
        }
        protected void ADDNewLevelRecord(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            levelForm.Reset();


            this.EditLevelWindow.Title = Resources.Common.AddNewRecord;


            this.EditLevelWindow.Show();
        }

      


        protected void IndustryStore_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<Industry> routers = _nationalQuotaService.ChildGetAll<Industry>(request);
            if (!routers.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", routers.ErrorCode) != null ? GetGlobalResourceObject("Errors", routers.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + routers.LogId: routers.Summary).Show();
                return;
            }
            this.IndustryStore.DataSource = routers.Items;
            e.Total = routers.Items.Count; ;

            this.IndustryStore.DataBind();
        }
        protected void BusinessStore_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<BusinessSize> routers = _nationalQuotaService.ChildGetAll<BusinessSize>(request);
            if (!routers.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", routers.ErrorCode) != null ? GetGlobalResourceObject("Errors", routers.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + routers.LogId: routers.Summary).Show();
                return;
            }
            this.BusinessStore.DataSource = routers.Items;
            e.Total = routers.Items.Count; ;

            this.BusinessStore.DataBind();
        }
        protected void levelStore_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<Level> routers = _nationalQuotaService.ChildGetAll<Level>(request);
            if (!routers.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", routers.ErrorCode) != null ? GetGlobalResourceObject("Errors", routers.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + routers.LogId: routers.Summary).Show();
                return;
            }
            this.levelStore.DataSource = routers.Items;
            e.Total = routers.Items.Count; ;

            this.levelStore.DataBind();
        }
        protected void CitizenshipStore_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<Citizenship> routers = _nationalQuotaService.ChildGetAll<Citizenship>(request);
            if (!routers.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", routers.ErrorCode) != null ? GetGlobalResourceObject("Errors", routers.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + routers.LogId: routers.Summary).Show();
                return;
            }
            this.CitizenshipStore.DataSource = routers.Items;
            e.Total = routers.Items.Count; ;

            this.CitizenshipStore.DataBind();
        }
        protected void PointAcquisitionStore_ReadData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<PointAcquisition> routers = _nationalQuotaService.ChildGetAll<PointAcquisition>(request);
            if (!routers.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", routers.ErrorCode) != null ? GetGlobalResourceObject("Errors", routers.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + routers.LogId: routers.Summary).Show();
                return;
            }
            this.PointAcquisitionStore.DataSource = routers.Items;
            e.Total = routers.Items.Count; ;

            this.PointAcquisitionStore.DataBind();
            //if(routers.Items.Count==0)
            //{
            //    List<Citizenship> L = new List<Citizenship>(); 
            //    Citizenship c = new Citizenship();
            //    c.ceiling = 0;
            //    c.points = 0.0;
            //    c.name = "";
            //    c.recordId = "";
            //    L.Add(c);
            //    this.citizenshipStore.DataSource =L ;
            //    this.citizenshipStore.DataBind();
            //}

        }
        protected void LevelAcquisition_ReadData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<LevelAcquisition> routers = _nationalQuotaService.ChildGetAll<LevelAcquisition>(request);
            if (!routers.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", routers.ErrorCode) != null ? GetGlobalResourceObject("Errors", routers.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + routers.LogId: routers.Summary).Show();
                return;
            }
            this.LevelAcquisitionStore.DataSource = routers.Items;
            e.Total = routers.Items.Count; ;

            this.LevelAcquisitionStore.DataBind();
            //if(routers.Items.Count==0)
            //{
            //    List<Citizenship> L = new List<Citizenship>(); 
            //    Citizenship c = new Citizenship();
            //    c.ceiling = 0;
            //    c.points = 0.0;
            //    c.name = "";
            //    c.recordId = "";
            //    L.Add(c);
            //    this.citizenshipStore.DataSource =L ;
            //    this.citizenshipStore.DataBind();
            //}

        }





        protected void SaveNewIndustry(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.


            string obj = e.ExtraParams["values"];
            Industry b = JsonConvert.DeserializeObject<Industry>(obj);

            string id = e.ExtraParams["id"];
            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<Industry> request = new PostRequest<Industry>();

                    request.entity = b;
                    PostResponse<Industry> r = _nationalQuotaService.ChildAddOrUpdate<Industry>(request);


                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+GetGlobalResourceObject("Errors","ErrorLogId") + r.LogId : r.Summary).Show();
                        return;
                    }
                    else
                    {
                        b.recordId = r.recordId;
                        //Add this record to the store 
                        this.IndustryStore.Insert(0, b);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        this.EditIndustryWindow.Close();
                        RowSelectionModel sm = this.IndustryGrid.GetSelectionModel() as RowSelectionModel;
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
                    PostRequest<Industry> request = new PostRequest<Industry>();
                    request.entity = b;
                    PostResponse<Industry> r = _nationalQuotaService.ChildAddOrUpdate<Industry>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                    //Step 2 : saving to store

                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+GetGlobalResourceObject("Errors","ErrorLogId") + r.LogId : r.Summary).Show();
                        return;
                    }
                    else
                    {


                        ModelProxy record = this.IndustryStore.GetById(id);
                        IndustryFrom.UpdateRecord(record);
                        record.Commit();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });
                        this.EditIndustryWindow.Close();


                    }

                }
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }
            }
        }
        protected void SaveNewBusiness(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.


            string obj = e.ExtraParams["values"];
            BusinessSize b = JsonConvert.DeserializeObject<BusinessSize>(obj);

            string id = e.ExtraParams["id"];
            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<BusinessSize> request = new PostRequest<BusinessSize>();

                    request.entity = b;
                    PostResponse<BusinessSize> r = _nationalQuotaService.ChildAddOrUpdate<BusinessSize>(request);


                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+GetGlobalResourceObject("Errors","ErrorLogId") + r.LogId : r.Summary).Show();
                        return;
                    }
                    else
                    {
                        b.recordId = r.recordId;
                        //Add this record to the store 
                        this.BusinessStore.Insert(0, b);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        this.EditBusinessWindow.Close();
                        RowSelectionModel sm = this.BusinessGrid.GetSelectionModel() as RowSelectionModel;
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
                    PostRequest<BusinessSize> request = new PostRequest<BusinessSize>();
                    request.entity = b;
                    PostResponse<BusinessSize> r = _nationalQuotaService.ChildAddOrUpdate<BusinessSize>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                    //Step 2 : saving to store

                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+GetGlobalResourceObject("Errors","ErrorLogId") + r.LogId : r.Summary).Show();
                        return;
                    }
                    else
                    {


                        ModelProxy record = this.BusinessStore.GetById(id);
                        businessForm.UpdateRecord(record);
                        record.Commit();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });
                        this.EditBusinessWindow.Close();


                    }

                }
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }
            }
        }
        protected void SaveNewLevel(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.


            string obj = e.ExtraParams["values"];
            Level b = JsonConvert.DeserializeObject<Level>(obj);

            string id = e.ExtraParams["id"];
            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<Level> request = new PostRequest<Level>();

                    request.entity = b;
                    PostResponse<Level> r = _nationalQuotaService.ChildAddOrUpdate<Level>(request);


                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+GetGlobalResourceObject("Errors","ErrorLogId") + r.LogId : r.Summary).Show();
                        return;
                    }
                    else
                    {
                        b.recordId = r.recordId;
                        //Add this record to the store 
                        this.levelStore.Insert(0, b);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        this.EditLevelWindow.Close();
                        RowSelectionModel sm = this.levelGrid.GetSelectionModel() as RowSelectionModel;
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
                    PostRequest<Level> request = new PostRequest<Level>();
                    request.entity = b;
                    PostResponse<Level> r = _nationalQuotaService.ChildAddOrUpdate<Level>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                    //Step 2 : saving to store

                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+GetGlobalResourceObject("Errors","ErrorLogId") + r.LogId : r.Summary).Show();
                        return;
                    }
                    else
                    {


                        ModelProxy record = this.levelStore.GetById(id);
                        levelForm.UpdateRecord(record);
                        record.Commit();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });
                        this.EditLevelWindow.Close();


                    }

                }
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }
            }
        }
        protected void SaveNewCitizenship(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.


            string obj = e.ExtraParams["values"];
            Citizenship b = JsonConvert.DeserializeObject<Citizenship>(obj);

            string id = e.ExtraParams["id"];
            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<Citizenship> request = new PostRequest<Citizenship>();

                    request.entity = b;
                    PostResponse<Citizenship> r = _nationalQuotaService.ChildAddOrUpdate<Citizenship>(request);


                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+GetGlobalResourceObject("Errors","ErrorLogId") + r.LogId : r.Summary).Show();
                        return;
                    }
                    else
                    {
                        b.recordId = r.recordId;
                        //Add this record to the store 
                        this.CitizenshipStore.Insert(0, b);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        this.EditCitizenshipWindow.Close();
                        RowSelectionModel sm = this.CitizenshipGrid.GetSelectionModel() as RowSelectionModel;
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
                    PostRequest<Citizenship> request = new PostRequest<Citizenship>();
                    request.entity = b;
                    PostResponse<Citizenship> r = _nationalQuotaService.ChildAddOrUpdate<Citizenship>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                    //Step 2 : saving to store

                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+GetGlobalResourceObject("Errors","ErrorLogId") + r.LogId : r.Summary).Show();
                        return;
                    }
                    else
                    {


                        ModelProxy record = this.CitizenshipStore.GetById(id);
                        CitizenshipForm.UpdateRecord(record);
                        record.Commit();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });
                        this.EditCitizenshipWindow.Close();


                    }

                }
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }
            }
        }
        protected void SaveNewPointAcquisition(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.

           
            string obj = e.ExtraParams["values"];
            
            List<PointAcquisition> codes = JsonConvert.DeserializeObject<List<PointAcquisition>>(e.ExtraParams["codes"]);

            // Define the object to add or edit as null

           

                try
                {
                //New Mode
                //Step 1 : Fill The object and insert in the store 

                                ListRequest request = new ListRequest();

                                request.Filter = "";
                                ListResponse<PointAcquisition> routers = _nationalQuotaService.ChildGetAll<PointAcquisition>(request);
                                if (!routers.Success)
                                {
                                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", routers.ErrorCode) != null ? GetGlobalResourceObject("Errors", routers.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + routers.LogId: routers.Summary).Show();
                                    return;
                                }

                              routers.Items.ForEach(x => DeletePointAcquisitionRecord(x));

                        PostRequest<PointAcquisition> codesReq = new PostRequest<PointAcquisition>();
                       foreach(PointAcquisition C in codes)
                         {
                                codesReq.entity = C;
                         
                  if (C.days<=0|| C.hiredPct<0 ||C.hiredPct>100 || C.terminatedPct<0 || C.terminatedPct>100)
                    { continue; }
                                    PostResponse<PointAcquisition> codesResp = _nationalQuotaService.ChildAddOrUpdate<PointAcquisition>(codesReq);
                                if (!codesResp.Success)//it maybe be another condition
                                {
                                    //Show an error saving...
                                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", codesResp.ErrorCode) != null ? GetGlobalResourceObject("Errors", codesResp.ErrorCode).ToString() : codesResp.Summary).Show();
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

                                }  
                          
                        

                    }
                }
                catch (Exception ex)
                {
                    //Error exception displaying a messsage box
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorSavingRecord).Show();
                }
            PointAcquisitionStore.Reload();

            
          
        }
        protected void SaveNewLevelAcquisition(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.


            string obj = e.ExtraParams["values"];

            List<LevelAcquisition> codes = JsonConvert.DeserializeObject<List<LevelAcquisition>>(e.ExtraParams["codes"]);

            // Define the object to add or edit as null



            try
            {
                //New Mode
                //Step 1 : Fill The object and insert in the store 

                LevelAcquisitionListRequest request = getLevelAcquisitionList(); 

                request.Filter = "";
                ListResponse<LevelAcquisition> routers = _nationalQuotaService.ChildGetAll<LevelAcquisition>(request);
                if (!routers.Success)
                {
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", routers.ErrorCode) != null ? GetGlobalResourceObject("Errors", routers.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + routers.LogId: routers.Summary).Show();
                    return;
                }

                routers.Items.ForEach(x => DeleteLevelAcquisitionRecord(x));

                PostRequest<LevelAcquisition> codesReq = new PostRequest<LevelAcquisition>();
                foreach (LevelAcquisition C in codes)
                {
                    codesReq.entity = C;

                    if (C.leId==0)
                    { continue; }
                    PostResponse<LevelAcquisition> codesResp = _nationalQuotaService.ChildAddOrUpdate<LevelAcquisition>(codesReq);
                    if (!codesResp.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", codesResp.ErrorCode) != null ? GetGlobalResourceObject("Errors", codesResp.ErrorCode).ToString() : codesResp.Summary).Show();
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

                    }



                }
            }
            catch (Exception ex)
            {
                //Error exception displaying a messsage box
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorSavingRecord).Show();
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
      
       private void FillIndustry ()
        {
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<Industry> routers = _nationalQuotaService.ChildGetAll<Industry>(request);
            if (!routers.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", routers.ErrorCode) != null ? GetGlobalResourceObject("Errors", routers.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + routers.LogId: routers.Summary).Show();
                return;
            }
            this.inIdStore.DataSource = routers.Items;
            
            this.inIdStore.DataBind();
        }
        private void FillBusinessSize()
        {
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<BusinessSize> routers = _nationalQuotaService.ChildGetAll<BusinessSize>(request);
            if (!routers.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", routers.ErrorCode) != null ? GetGlobalResourceObject("Errors", routers.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + routers.LogId: routers.Summary).Show();
                return;
            }
            this.bsIdStore.DataSource = routers.Items;
          

            this.bsIdStore.DataBind();
        }

        protected void FillLevelAcquisition(object sender, DirectEventArgs e)
        {

            LevelAcquisitionListRequest request = getLevelAcquisitionList(); 
            if (!string.IsNullOrEmpty(bsId.Text) && bsId.Value.ToString() != "0")      
                request.BusinessSizeId = Convert.ToInt32(bsId.Value);      
            else          
                request.BusinessSizeId = 0;            
            if (!string.IsNullOrEmpty(inId.Text) && inId.Value.ToString() != "0")           
                request.industryId = Convert.ToInt32(inId.Value);           
            else            
                request.industryId = 0;

            

            request.Filter = "";
            ListResponse<LevelAcquisition> routers = _nationalQuotaService.ChildGetAll<LevelAcquisition>(request);
            if (!routers.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", routers.ErrorCode) != null ? GetGlobalResourceObject("Errors", routers.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + routers.LogId: routers.Summary).Show();
                return;
            }
            this.LevelAcquisitionStore.DataSource = routers.Items;
          
            this.LevelAcquisitionStore.DataBind();

        }
        private LevelAcquisitionListRequest getLevelAcquisitionList()
        {
            LevelAcquisitionListRequest request = new LevelAcquisitionListRequest();
            if (!string.IsNullOrEmpty(bsId.Text) && bsId.Value.ToString() != "0")
                request.BusinessSizeId = Convert.ToInt32(bsId.Value);
            else
                request.BusinessSizeId = 0;
            if (!string.IsNullOrEmpty(inId.Text) && inId.Value.ToString() != "0")
                request.industryId = Convert.ToInt32(inId.Value);
            else
                request.industryId = 0;



            request.Filter = "";
            return request;
        }
    }
}