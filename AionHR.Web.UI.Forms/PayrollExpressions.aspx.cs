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
    public partial class PayrollExpressions : System.Web.UI.Page
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


        private void FillEXFUNStore(string EXFUNID)
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

            PayrollExFunCodeRequest request = new PayrollExFunCodeRequest();
            request.ExpressionId = EXFUNID;
            request.Filter = "";
            ListResponse<PayrollExpFunc> resp = _payrollService.ChildGetAll<PayrollExpFunc>(request);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
            }
            this.ExpressionFunctionStore.DataSource = resp.Items;

            this.ExFunGridPanel.DataBind();
        }


        protected void PoPuP(object sender, DirectEventArgs e)
        {


            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];
            CurrentExpressionId.Text = id;


            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id;

                    RecordResponse<PayrollExpression> response = _payrollService.ChildGetRecord<PayrollExpression>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.BasicInfoTab.SetValues(response.result);

                    FillEXFUNStore(e.ExtraParams["id"]);

                    ExFunGridPanel.Enable();

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
                PayrollExpression s = new PayrollExpression();
                s.recordId = index;
                //s.reference = "";

                s.name = "";
                PostRequest<PayrollExpression> req = new PostRequest<PayrollExpression>();
                req.entity = s;
                PostResponse<PayrollExpression> r = _payrollService.ChildDelete<PayrollExpression>(req);
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

            ExFunGridPanel.Disable();

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
            ListResponse<PayrollExpression> resp = _payrollService.ChildGetAll<PayrollExpression>(request);
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
            PayrollExpression b = JsonConvert.DeserializeObject<PayrollExpression>(obj);

            string id = e.ExtraParams["id"];
            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<PayrollExpression> request = new PostRequest<PayrollExpression>();

                    request.entity = b;
                    PostResponse<PayrollExpression> r = _payrollService.ChildAddOrUpdate<PayrollExpression>(request);


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

                        this.EditRecordWindow.Close();

                        ExFunGridPanel.Enable();



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
                    PostRequest<PayrollExpression> request = new PostRequest<PayrollExpression>();
                    request.entity = b;
                    PostResponse<PayrollExpression> r = _payrollService.ChildAddOrUpdate<PayrollExpression>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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
        protected void checkRecordExpression(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.


            //string obj = e.ExtraParams["values"];
            //PayrollExpression b = JsonConvert.DeserializeObject<PayrollExpression>(obj);

            //CheckExpressionRecordRequest req = new CheckExpressionRecordRequest();
            //req.expression = b.expression;
            //RecordResponse<CheckExpression> resp = _payrollService.ChildGetRecord<CheckExpression>(req);
            //if (!resp.Success)
            //{
            //    Common.errorMessage(resp);
            //    return; 
            //}
            //if (resp.result.success)
            //    X.MessageBox.Alert(Resources.Common.Notification, GetLocalResourceObject("trueEX").ToString()).Show();
            //else
            //    X.MessageBox.Alert(Resources.Common.Error, resp.result.returnMessage).Show();



            string id = e.ExtraParams["id"];
            string obj = e.ExtraParams["values"];
            string name = e.ExtraParams["name"];
            string expression = e.ExtraParams["expression"];
            CheckExpression b = JsonConvert.DeserializeObject<CheckExpression>(obj);

            //CheckExpressionRecordRequest req = new CheckExpressionRecordRequest();
            PostRequest<CheckExpression> request = new PostRequest<CheckExpression>();
            request.entity = new CheckExpression();
            request.entity.recordId = id;
            request.entity.expression = expression;
            
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



        protected void SaveNewEXFUNRecord(object sender, DirectEventArgs e)
        {
            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = CurrentExpressionId.Text; //e.ExtraParams["expressionId"];
            string functionId = e.ExtraParams["functionId"];

            string obj = e.ExtraParams["values"];
            PayrollExpFunc b = JsonConvert.DeserializeObject<PayrollExpFunc>(obj);
            b.expressionId = id;
            // Define the object to add or edit as null

            //if (string.IsNullOrEmpty(id))
            //{

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<PayrollExpFunc> request = new PostRequest<PayrollExpFunc>();
                    request.entity = new PayrollExpFunc();
                    request.entity.expressionId = id;
                    request.entity.functionId = functionId;
                    PostResponse<PayrollExpFunc> r = _payrollService.ChildAddOrUpdate<PayrollExpFunc>(request);
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
                        ExpressionFunctionStore.Reload();


                        this.EditEXFUNWindow.Close();
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


        protected void EXFUNStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            string filter = string.Empty;
            int totalCount = 1;

            if (string.IsNullOrEmpty(CurrentExpressionId.Text))
                return;

            PayrollExFunCodeRequest request = new PayrollExFunCodeRequest();
            request.ExpressionId = CurrentExpressionId.Text;
            request.Filter = "";
            ListResponse<PayrollExpFunc> resp = _payrollService.ChildGetAll<PayrollExpFunc>(request);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
            }
            this.ExpressionFunctionStore.DataSource = resp.Items;

            this.ExFunGridPanel.DataBind();

        }

        protected void ADDExFunNewRecord(object sender, DirectEventArgs e)
        {
            EXFUNForm.Reset();

            FillFunctionStore();            
            

            this.EditEXFUNWindow.Show();
        }

        private void FillFunctionStore()
        {
            ListRequest request = new ListRequest();
            request.Filter = "";

            ListResponse<PayrollFunction> Items = _payrollService.ChildGetAll<PayrollFunction>(request);
            if (!Items.Success)
            {
                Common.errorMessage(Items);
                return;
            }
            this.FunctionStore.DataSource = Items.Items;

            this.FunctionStore.DataBind();
        }

        protected void PoPuPEXPFUN(object sender, DirectEventArgs e)
        {
            FillFunctionStore();

            int id = Convert.ToInt32(e.ExtraParams["expressionId"]);
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
                            Handler = String.Format("App.direct.DeleteEXFUNRecord({0},{1})", id, fnid),
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
        public void DeleteEXFUNRecord(int exId, int fnId)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                PayrollExpFunc n = new PayrollExpFunc();
                n.expressionId = exId.ToString();
                n.functionId = fnId.ToString();
   
                PostRequest<PayrollExpFunc> req = new PostRequest<PayrollExpFunc>();
                req.entity = n;
                PostResponse<PayrollExpFunc> res = _payrollService.ChildDelete<PayrollExpFunc>(req);
                if (!res.Success)
                {
                    //Show an error saving...
                    Common.errorMessage(res);
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    ExpressionFunctionStore.Remove(exId);

                    ExpressionFunctionStore.Reload();
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