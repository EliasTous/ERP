﻿using System;
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
using Model.Attendance;
using Model.Employees.Leaves;
using Model.Employees.Profile;
using Web.UI.Forms.ConstClasses;
using Model.Payroll;

namespace Web.UI.Forms
{
   
    public partial class PayrollConstants : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();

        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        IMathematicalService _payrollService = ServiceLocator.Current.GetInstance<IMathematicalService>();

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
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(CertificateLevel), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
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

            constant.ReadOnly = true;
            string id = e.ExtraParams["id"];
            string valueParam = e.ExtraParams["value"];
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 

                    //Step 2 : call setvalues with the retrieved object
                    constant.Text = id;
                    value.Value = valueParam; 


                    this.EditRecordWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditRecordWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = "App.direct.DeleteRecord('" + id+"')",

                     

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
        /// 

       

        [DirectMethod]
        public void DeleteRecord(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                PayrollConstant s = new PayrollConstant();
                s.constant = index;
                //s.reference = "";

             
                PostRequest<PayrollConstant> req = new PostRequest<PayrollConstant>();
                req.entity = s;
                PostResponse<PayrollConstant> r = _payrollService.ChildDelete<PayrollConstant>(req);
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
            constant.ReadOnly = false;

            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;


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
            ListResponse<PayrollConstant> resp = _payrollService.ChildGetAll<PayrollConstant>(request);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            this.Store1.DataSource = resp.Items;
            e.Total = resp.Items.Count; ;

            this.Store1.DataBind();
        }




        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.


            string obj = e.ExtraParams["values"];
            PayrollConstant b = JsonConvert.DeserializeObject<PayrollConstant>(obj);

            PostRequest<PayrollConstant> request = new PostRequest<PayrollConstant>();

            request.entity = b;
            PostResponse<PayrollConstant> r = _payrollService.ChildAddOrUpdate<PayrollConstant>(request);


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
                Store1.Reload();

                //Display successful notification
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordSavingSucc
                });

                this.EditRecordWindow.Close();
                // Define the object to add or edit as null

                //    if (string.IsNullOrEmpty(id))
                //{

                //    try
                //    {
                //        //New Mode
                //        //Step 1 : Fill The object and insert in the store 
                //        PostRequest<CertificateLevel> request = new PostRequest<CertificateLevel>();

                //        request.entity = b;
                //        PostResponse<CertificateLevel> r = _employeeService.ChildAddOrUpdate<CertificateLevel>(request);


                //        //check if the insert failed
                //        if (!r.Success)//it maybe be another condition
                //        {
                //            //Show an error saving...
                //            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //            Common.errorMessage(r);
                //            return;
                //        }
                //        else
                //        {
                //            b.recordId = r.recordId;
                //            //Add this record to the store 
                //            this.Store1.Insert(0, b);

                //            //Display successful notification
                //            Notification.Show(new NotificationConfig
                //            {
                //                Title = Resources.Common.Notification,
                //                Icon = Icon.Information,
                //                Html = Resources.Common.RecordSavingSucc
                //            });

                //            this.EditRecordWindow.Close();
                //            RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;
                //            sm.DeselectAll();
                //            sm.Select(b.recordId.ToString());



                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        //Error exception displaying a messsage box
                //        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorSavingRecord).Show();
                //    }


                //}
                //else
                //{
                //    //Update Mode

                //    try
                //    {
                //        //getting the id of the record
                //        PostRequest<CertificateLevel> request = new PostRequest<CertificateLevel>();
                //        request.entity = b;
                //        PostResponse<CertificateLevel> r = _employeeService.ChildAddOrUpdate<CertificateLevel>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                //        //Step 2 : saving to store

                //        //Step 3 :  Check if request fails
                //        if (!r.Success)//it maybe another check
                //        {
                //            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //            Common.errorMessage(r);
                //            return;
                //        }
                //        else
                //        {


                //            ModelProxy record = this.Store1.GetById(id);
                //            BasicInfoTab.UpdateRecord(record);
                //            record.Commit();
                //            Notification.Show(new NotificationConfig
                //            {
                //                Title = Resources.Common.Notification,
                //                Icon = Icon.Information,
                //                Html = Resources.Common.RecordUpdatedSucc
                //            });
                //            this.EditRecordWindow.Close();


                //        }

                //    }
                //    catch (Exception ex)
                //    {
                //        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                //    }
                //}
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