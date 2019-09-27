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
using AionHR.Web.UI.Forms.ConstClasses;
using AionHR.Model.Payroll;
using AionHR.Services.Messaging.Payroll;

namespace AionHR.Web.UI.Forms
{

    

    public partial class PayrollFunctions : System.Web.UI.Page
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


            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];
            CurrentFunctionId.Text = id;

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id;

                    RecordResponse<PayrollFunction> response = _payrollService.ChildGetRecord<PayrollFunction>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.BasicInfoTab.SetValues(response.result);

                    FillFUNConstStore(e.ExtraParams["id"]);

                    FunConstGridPanel.Enable();

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
                PayrollFunction s = new PayrollFunction();
                s.recordId = index;
                //s.reference = "";

                s.name = "";
                PostRequest<PayrollFunction> req = new PostRequest<PayrollFunction>();
                req.entity = s;
                PostResponse<PayrollFunction> r = _payrollService.ChildDelete<PayrollFunction>(req);
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


            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;

            FunConstGridPanel.Disable();

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
            ListResponse<PayrollFunction> resp = _payrollService.ChildGetAll<PayrollFunction>(request);
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
            PayrollFunction b = JsonConvert.DeserializeObject<PayrollFunction>(obj);

            string id = e.ExtraParams["id"];
            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<PayrollFunction> request = new PostRequest<PayrollFunction>();

                    request.entity = b;
                    PostResponse<PayrollFunction> r = _payrollService.ChildAddOrUpdate<PayrollFunction>(request);


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
                        b.recordId = r.recordId;
                        //Add this record to the store 
                        Store1.Reload();

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });


                        FunConstGridPanel.Enable();

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
                    PostRequest<PayrollFunction> request = new PostRequest<PayrollFunction>();
                    request.entity = b;
                    PostResponse<PayrollFunction> r = _payrollService.ChildAddOrUpdate<PayrollFunction>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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



        protected void checkRecordExpression(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.

            string id = e.ExtraParams["id"];
            string obj = e.ExtraParams["values"];
            string name = e.ExtraParams["name"];
            string functionBody = e.ExtraParams["functionBody"];
            CheckExpression b = JsonConvert.DeserializeObject<CheckExpression>(obj);

            //CheckExpressionRecordRequest req = new CheckExpressionRecordRequest();
            PostRequest<CheckExpression> request = new PostRequest<CheckExpression>();
            request.entity = new CheckExpression();
            request.entity.recordId = id;
            request.entity.expression = functionBody;            
            PostResponse<CheckExpression> resp = _payrollService.ChildAddOrUpdate<CheckExpression>(request);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            //if (resp.result.success)
            //    X.MessageBox.Alert(Resources.Common.Notification, GetLocalResourceObject("trueEX").ToString()).Show();
            //else
            //    X.MessageBox.Alert(Resources.Common.Error, resp.result.returnMessage).Show();

            if (resp.Success)
                X.MessageBox.Alert(Resources.Common.Notification, GetLocalResourceObject("trueEX").ToString()).Show();
            else
                X.MessageBox.Alert(Resources.Common.Error, resp.Message).Show();
        }



        private void FillFUNConstStore(string FUNConstID)
        {
            //GenericParametersRequest request = new GenericParametersRequest();
            //request.paramString = "1|" + CustID;
            //request.Filter = "";
            //ListResponse<Address> Measurements = _saleService.ChildGetAll<Address>(request);
            //if (!Measurements.Success)
            //{
            //    Common.errorMessage(Measurements);
            //    return;
            //}
            //this.ClientAddressStore.DataSource = Measurements.Items;

            //this.AddressGridPanel.DataBind();

            PayrollFunConstCodeRequest request = new PayrollFunConstCodeRequest();
            request.FunctionId = FUNConstID;
            request.Filter = "";
            ListResponse<PayrollFunConst> resp = _payrollService.ChildGetAll<PayrollFunConst>(request);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
            }
            this.FunctionConstStore.DataSource = resp.Items;

            this.FunConstGridPanel.DataBind();
        }


        protected void SaveNewFUNConstRecord(object sender, DirectEventArgs e)
        {
            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = CurrentFunctionId.Text; //e.ExtraParams["expressionId"];
            string functionId = e.ExtraParams["functionId"];
            string constant = e.ExtraParams["constant"];

            string obj = e.ExtraParams["values"];
            PayrollFunConst b = JsonConvert.DeserializeObject<PayrollFunConst>(obj);
            b.functionId = id;
            // Define the object to add or edit as null

            //if (string.IsNullOrEmpty(id))
            //{

            try
            {
                //New Mode
                //Step 1 : Fill The object and insert in the store 
                PostRequest<PayrollFunConst> request = new PostRequest<PayrollFunConst>();
                request.entity = new PayrollFunConst();                
                request.entity.functionId = id;
                request.entity.constant = constant;
                PostResponse<PayrollFunConst> r = _payrollService.ChildAddOrUpdate<PayrollFunConst>(request);
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
                    //this.ClientAddressStore.Insert(0, b);

                    //Display successful notification
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordSavingSucc
                    });
                    FunctionConstStore.Reload();


                    this.EditFUNConstWindow.Close();
                    //RowSelectionModel sm = this.ClientGridPanel.GetSelectionModel() as RowSelectionModel;
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


            //}
            //else
            //{
            //    //Update Mode

            //    try
            //    {
            //        PostRequest<ClientAddress> request = new PostRequest<ClientAddress>();
            //        request.entity = new ClientAddress();
            //        request.entity.address = b;
            //        request.entity.clientId = CurrentClientId.Text;
            //        request.entity.addressId = id;
            //        PostResponse<ClientAddress> r = _saleService.ChildAddOrUpdate<ClientAddress>(request);
            //        b.recordId = r.recordId;

            //        //Step 2 : saving to store

            //        //Step 3 :  Check if request fails
            //        if (!r.Success)//it maybe another check
            //        {
            //            Common.errorMessage(r);
            //            return;
            //        }
            //        else
            //        {


            //            //ModelProxy record = this.ClientStore.GetById(index);
            //            //BasicInfoTab.UpdateRecord(record);
            //            //record.Commit();

            //            ClientAddressStore.Reload();


            //            Notification.Show(new NotificationConfig
            //            {
            //                Title = Resources.Common.Notification,
            //                Icon = Icon.Information,
            //                Html = Resources.Common.RecordUpdatedSucc
            //            });
            //            this.EditAddressWindow.Close();


            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
            //        X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
            //    }
            //}
        }


        protected void FUNConstStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            string filter = string.Empty;
            int totalCount = 1;

            if (string.IsNullOrEmpty(CurrentFunctionId.Text))
                return;

            PayrollFunConstCodeRequest request = new PayrollFunConstCodeRequest();
            request.FunctionId = CurrentFunctionId.Text;
            request.Filter = "";
            ListResponse<PayrollFunConst> resp = _payrollService.ChildGetAll<PayrollFunConst>(request);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
            }
            this.FunctionConstStore.DataSource = resp.Items;

            this.FunConstGridPanel.DataBind();

        }

        protected void ADDFunConstNewRecord(object sender, DirectEventArgs e)
        {
            FUNConstForm.Reset();

            FillConstantStore();


            this.EditFUNConstWindow.Show();
        }

        private void FillConstantStore()
        {
            ListRequest request = new ListRequest();
            request.Filter = "";

            ListResponse<PayrollConstant> Items = _payrollService.ChildGetAll<PayrollConstant>(request);
            if (!Items.Success)
            {
                Common.errorMessage(Items);
                return;
            }
            this.ConstantStore.DataSource = Items.Items;

            this.ConstantStore.DataBind();
        }

        protected void PoPuPFUNConst(object sender, DirectEventArgs e)
        {
            FillConstantStore();

            string id = e.ExtraParams["constant"];
            CurrentDelFuncID.Text = e.ExtraParams["constant"];
            int fnid = Convert.ToInt32(e.ExtraParams["functionId"]);
            string type = e.ExtraParams["type"];
            //addressId.Text = e.ExtraParams["addressId"];
            switch (type)
            {
                //case "imgEdit":
                //    //Step 1 : get the object from the Web Service 
                //    AddressRequest r = new AddressRequest();
                //    r.AddressId = id.ToString();


                //    RecordResponse<Address> response = _systemService.ChildGetRecord<Address>(r);
                //    if (!response.Success)
                //    {
                //        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //        Common.errorMessage(response);
                //        return;
                //    }
                //    //Step 2 : call setvalues with the retrieved object

                //    this.AddressForm.SetValues(response.result);

                //    this.EditAddressWindow.Title = Resources.Common.EditWindowsTitle;
                //    this.EditAddressWindow.Show();
                //    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteFUNConstRecord({0})", fnid),
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

        [DirectMethod]
        public void DeleteFUNConstRecord(int fnId)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                PayrollFunConst n = new PayrollFunConst();
                n.constant = CurrentDelFuncID.Text;//exId.ToString();
                n.functionId = fnId.ToString();

                PostRequest<PayrollFunConst> req = new PostRequest<PayrollFunConst>();
                req.entity = n;
                PostResponse<PayrollFunConst> res = _payrollService.ChildDelete<PayrollFunConst>(req);
                if (!res.Success)
                {
                    //Show an error saving...
                    Common.errorMessage(res);
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    FunctionConstStore.Remove(CurrentDelFuncID.Text);

                    FunctionConstStore.Reload();
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









    }
}