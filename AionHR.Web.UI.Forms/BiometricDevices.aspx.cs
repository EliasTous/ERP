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
using AionHR.Model.TimeAttendance;

namespace AionHR.Web.UI.Forms
{
    public partial class BiometricDevices : System.Web.UI.Page
    {

        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
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
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id.ToString();
                    RecordResponse<BiometricDevice> response = _timeAttendanceService.ChildGetRecord<BiometricDevice>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).ToString() : response.Summary).Show();
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.BasicInfoTab.SetValues(response.result);
                    FillBranch();
                    if (response.result.divisionId != null)
                        divisionId.Select(response.result.divisionId);
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
                BiometricDevice n = new BiometricDevice();
                n.recordId = index;
                n.name = "";
                n.reference = "";
                n.divisionId = "0";

                PostRequest<BiometricDevice> req = new PostRequest<BiometricDevice>();
                req.entity = n;
                PostResponse<BiometricDevice> res = _timeAttendanceService.ChildDelete<BiometricDevice>(req);
                if (!res.Success)
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", res.ErrorCode) != null ? GetGlobalResourceObject("Errors", res.ErrorCode).ToString() : res.Summary).Show();
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
            BasicInfoTab.Reset();
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;

            FillBranch();
            this.EditRecordWindow.Show();
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
            ListResponse<BiometricDevice> nationalities = _timeAttendanceService.ChildGetAll<BiometricDevice>(request);
            if (!nationalities.Success)
            {
                X.Msg.Alert(Resources.Common.ErrorSavingRecord, GetGlobalResourceObject("Errors", nationalities.ErrorCode) != null ? GetGlobalResourceObject("Errors", nationalities.ErrorCode).ToString() : nationalities.Summary).Show();
                return;
            }
            this.Store1.DataSource = nationalities.Items;
            e.Total = nationalities.count;

            this.Store1.DataBind();
        }




        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];
            BiometricDevice b = JsonConvert.DeserializeObject<BiometricDevice>(obj);

            b.recordId = id;
            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<BiometricDevice> request = new PostRequest<BiometricDevice>();
                    request.entity = b;
                    PostResponse<BiometricDevice> r = _timeAttendanceService.ChildAddOrUpdate<BiometricDevice>(request);
                    b.recordId = r.recordId;

                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.ErrorSavingRecord, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() : r.Summary).Show();
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
                    X.Msg.Alert(Resources.Common.ErrorSavingRecord,ex.Message ).Show();
                }


            }
            else
            {
                //Update Mode

                try
                {
                    int index = Convert.ToInt32(id);//getting the id of the record
                    PostRequest<BiometricDevice> request = new PostRequest<BiometricDevice>();
                    request.entity = b;
                    PostResponse<BiometricDevice> r = _timeAttendanceService.ChildAddOrUpdate<BiometricDevice>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                    //Step 2 : saving to store

                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.ErrorUpdatingRecord, r.Message).Show();
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
                    X.Msg.Alert(Resources.Common.ErrorUpdatingRecord, ex.Message ).Show();
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

      

        private void FillBranch()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Division> resp = _companyStructureService.ChildGetAll<Division>(branchesRequest);
            divisionStore.DataSource = resp.Items;
            divisionStore.DataBind();
        }
        protected void addDivision(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(divisionId.Text))
                return;
            Division dept = new Division();
            dept.name = divisionId.Text;
            dept.isInactive = false;
            PostRequest<Division> depReq = new PostRequest<Division>();
            depReq.entity = dept;

            PostResponse<Division> response = _companyStructureService.ChildAddOrUpdate<Division>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                divisionStore.Insert(0, dept);
                divisionId.Select(0);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).ToString() : response.Summary).Show();
                return;
            }

        }
    }
}