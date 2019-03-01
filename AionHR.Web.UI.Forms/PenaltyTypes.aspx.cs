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
using AionHR.Model.Payroll;
using System.Reflection;
using AionHR.Web.UI.Forms.ConstClasses;

namespace AionHR.Web.UI.Forms
{
    public partial class PenaltyTypes : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IPayrollService _PayrollService = ServiceLocator.Current.GetInstance<IPayrollService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();

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
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(TimeSchedule), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
                }

                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(TimeCode), null, penaltyDetailGrid, null, null);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    penaltyDetailGrid.Disabled = true;
                    return;
                }


            }

        }
        private void FilltimeCodeStore()
        {
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<TimeCode> response = _PayrollService.ChildGetAll<TimeCode>(request);
            if (!response.Success)
            {
                Common.errorMessage(response);
                return;

            }
            timeVariationStore.DataSource = response.Items;
            timeVariationStore.DataBind();

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
            FilltimeCodeStore();
            penaltyDetailStoreCount.Text = "0";
            panelRecordDetails.ActiveIndex = 0;
            penaltyDetailGrid.Disabled = false;
            string id = e.ExtraParams["id"];
            currentPenaltyType.Text = id;
            string type = e.ExtraParams["type"];
            this.BasicInfoTab.Reset();

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id;

                    RecordResponse<PenaltyType> response = _PayrollService.ChildGetRecord<PenaltyType>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                   
                    this.BasicInfoTab.SetValues(response.result);
                  
                   
                    X.Call("ChangeReason", response.result.reason, GetGlobalResourceObject("ComboBoxValues", "Reason_ATTENDANCE").ToString(), GetGlobalResourceObject("ComboBoxValues", "TimeBasee_DAYS").ToString());


                    recordId.Text = id;
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
                PenaltyType s = new PenaltyType();
                s.recordId = index;
                s.name = "";
                //s.intName = "";

                PostRequest<PenaltyType> req = new PostRequest<PenaltyType>();
                req.entity = s;
                PostResponse<PenaltyType> r = _PayrollService.ChildDelete<PenaltyType>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                   Common.errorMessage(r);;
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
            FilltimeCodeStore();
            BasicInfoTab.Reset();
            currentPenaltyType.Text = "";
            panelRecordDetails.ActiveIndex = 0;
            penaltyDetailGrid.Disabled = true;
            damage.Select("0");
            penaltyDetailStoreCount.Text = "0";
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;


            this.EditRecordWindow.Show();
        }

        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;




            //Fetching the corresponding list

            //in this test will take a list of News
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<PenaltyType> response = _PayrollService.ChildGetAll<PenaltyType>(request);
            if (!response.Success)
            {
                Common.errorMessage(response);
                return;
            }
            List<XMLDictionary> timeCode = ConstTimeVariationType.TimeCodeList(_systemService);
           
            response.Items.ForEach(x =>
           {
               x.reasonString = FillReasonString(x.reason);
               x.timeBaseString = FillTimeBaseString(x.timeBase);

          
              
                   x.timeCodeString = timeCode.Where(y => y.key == x.timeCode).Count() != 0 ? timeCode.Where(y => y.key == Convert.ToInt32(x.timeCode)).First().value : string.Empty;
               

           });
            this.Store1.DataSource = response.Items;
            e.Total = response.Items.Count; ;

            this.Store1.DataBind();
        }





        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.

            
            string obj = e.ExtraParams["values"];
            PenaltyType b = JsonConvert.DeserializeObject<PenaltyType>(obj);
            List<PenaltyDetail> PD = JsonConvert.DeserializeObject<List<PenaltyDetail>>(e.ExtraParams["codes"]);

          
            string id = e.ExtraParams["id"];
            // Define the object to add or edit as null
            if (!string.IsNullOrEmpty(currentPenaltyType.Text))
            {
                id = currentPenaltyType.Text;
                b.recordId= currentPenaltyType.Text;
            }
            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<PenaltyType> request = new PostRequest<PenaltyType>();

                    request.entity = b;
                    PostResponse<PenaltyType> r = _PayrollService.ChildAddOrUpdate<PenaltyType>(request);
                   
                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                       Common.errorMessage(r);;
                        return;
                    }
                    else
                    {
                        currentPenaltyType.Text = r.recordId;

                 

                            //PostRequest<PenaltyDetail> codesReq = new PostRequest<PenaltyDetail>();

                            //PD.ForEach(x =>

                            //{
                            //    DeletePenaltyDetailsRecord(x);
                            //    codesReq.entity = x;
                            //    codesReq.entity.recordId = null;
                            //    PostResponse<PenaltyDetail> codesResp = _PayrollService.ChildAddOrUpdate<PenaltyDetail>(codesReq);
                            //    if (!codesResp.Success)//it maybe be another condition
                            //{
                            //    //Show an error saving...
                            //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            //        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", codesResp.ErrorCode) != null ? GetGlobalResourceObject("Errors", codesResp.ErrorCode).ToString() : codesResp.Summary).Show();
                            //        throw new Exception();
                            //    }
                            //});

                        
                        Store1.Reload();
                        penaltyDetailGrid.Disabled = false;

                            //Display successful notification
                            Notification.Show(new NotificationConfig
                            {
                                Title = Resources.Common.Notification,
                                Icon = Icon.Information,
                                Html = Resources.Common.RecordSavingSucc
                            });

                          
                            //RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;
                            //sm.DeselectAll();
                            //sm.Select(b.recordId.ToString());

                        }

                    
                }
                catch (Exception ex)
                {
                    if (ex.Message != null)
                    {
                        //Error exception displaying a messsage box
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        X.Msg.Alert(Resources.Common.Error, ex.Message).Show();
                    }
                }


            }
            else
            {
                penaltyDetailGrid.Disabled = false;
                //Update Mode

                try
                {
                    //getting the id of the record
                    PostRequest<PenaltyType> request = new PostRequest<PenaltyType>();
                    request.entity = b;
                    PostResponse<PenaltyType> r = _PayrollService.ChildAddOrUpdate<PenaltyType>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                    //Step 2 : saving to store

                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                       Common.errorMessage(r);;
                        return;
                    }
                    else
                    {
                        DeleteAllPenaltyDetailsRecords(damage.Value.ToString());
                        PostRequest<PenaltyDetail> codesReq = new PostRequest<PenaltyDetail>();
                        PD.ForEach(x =>
                        {
                           
                            codesReq.entity = x;
                            codesReq.entity.recordId = null;
                          
                        PostResponse<PenaltyDetail> codesResp = _PayrollService.ChildAddOrUpdate<PenaltyDetail>(codesReq);

                        if (!codesResp.Success)//it maybe be another condition
                        {
                            //Show an error saving...
                            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", codesResp.ErrorCode) != null ? GetGlobalResourceObject("Errors", codesResp.ErrorCode).ToString() : codesResp.Summary).Show();
                                throw new Exception();
                            }
                    });

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





        protected void penaltyDetailStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            try
            {
                PenaltyDetailListRequest request = new PenaltyDetailListRequest();
                request.ptId = currentPenaltyType.Text;
                request.damage = damage.Value.ToString();

                ListResponse<PenaltyDetail> response = _PayrollService.ChildGetAll<PenaltyDetail>(request);
                if (!response.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(response);
                    return;
                }

                //foreach (var local in TSL)
                //{
                //    foreach (var remote in r.Items)
                //    {
                //        if (remote.timeCode == local.timeCode)
                //        {
                //            local.tsId = remote.tsId;
                //            local.multiplier = remote.multiplier;
                //            local.isEnabled = remote.isEnabled;
                //            local.fullPeriod = remote.fullPeriod;
                //            local.deductPeriod = remote.deductPeriod;
                //            local.apId = remote.apId;
                //            local.apName = remote.apName;
                //            local.maxAllowed = remote.maxAllowed;
                //        }

                //    }

                //}

                List<PenaltyDetail> emptyPenaltyDetail = new List<PenaltyDetail>();
                if (response.Items.Count == 0)
                {
                    for (int i = 1; i <= 4; i++)
                    {
                        PostRequest<PenaltyDetail> PD = new PostRequest<PenaltyDetail>();

                        PD.entity = new PenaltyDetail { sequence = i, ptId = currentPenaltyType.Text,damage=Convert.ToInt16( damage.Value.ToString()), action = 2 };
                        PostResponse<PenaltyDetail> codesResp = _PayrollService.ChildAddOrUpdate<PenaltyDetail>(PD);
                        if (!codesResp.Success)
                        {
                            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                            Common.errorMessage(response);
                            break;
                        }

                        emptyPenaltyDetail.Add(PD.entity);
                    }
                    this.penaltyDetailStore.DataSource = emptyPenaltyDetail;
                    e.Total = emptyPenaltyDetail.Count;
                }
                else
                {

                    this.penaltyDetailStore.DataSource = response.Items;
                    e.Total = response.Items.Count;
                }
               

                this.penaltyDetailStore.DataBind();


                List<object> action = new List<object>();
                action.Add(new { name = GetLocalResourceObject("ActionWARNING").ToString(), recordId = GetGlobalResourceObject("ComboBoxValues", "Action_WARNING").ToString() });
                action.Add(new { name = GetLocalResourceObject("ActionSALARY_DEDUCTION").ToString(), recordId = GetGlobalResourceObject("ComboBoxValues", "Action_SALARY_DEDUCTION").ToString() });
                action.Add(new { name = GetLocalResourceObject("ActionRAISE_SUSPENSION").ToString(), recordId = GetGlobalResourceObject("ComboBoxValues", "Action_RAISE_SUSPENSION").ToString() });

                action.Add(new { name = GetLocalResourceObject("ActionTERMINATION_WITH_INDEMNITY").ToString(), recordId = GetGlobalResourceObject("ComboBoxValues", "Action_TERMINATION_WITH_INDEMNITY").ToString() });

                action.Add(new { name = GetLocalResourceObject("ActionTERMINATION_WITHOUT_INDEMNITY").ToString(), recordId = GetGlobalResourceObject("ComboBoxValues", "Action_TERMINATION_WITHOUT_INDEMNITY").ToString() });

                actionStore.DataSource = action;
                actionStore.DataBind();


            }
            catch (Exception exp)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }

        }
        
       

      
        private string FillReasonString(short Reason)
        {
            string R="";

            try
            {
               
                switch (Reason)
                {
                    case 1: R= GetLocalResourceObject("ReasonATTENDANCE").ToString();
                        break;
                    case 2:
                        R = GetLocalResourceObject("ResonLAW_VIOLATION").ToString();
                        break;
                }

                return R;   
            }
            catch { return string.Empty; }
        }
        private string FillTimeBaseString(short? timeBaseString)
        {
            string R = "";

            try
            {

                switch (timeBaseString)
                {
                    case 1:
                        R = GetLocalResourceObject("TimeBaseMINUTES").ToString();
                        break;
                    case 2:
                        R = GetLocalResourceObject("TimeBaseDAYS").ToString();
                        break;
                }

                return R;
            }
            catch { return string.Empty; }
        }
      
        private void DeleteAllPenaltyDetailsRecords(string damage)
        {

            if (string.IsNullOrEmpty(damage))
                return; 

            PenaltyDetailListRequest request = new PenaltyDetailListRequest();
            request.ptId = currentPenaltyType.Text;
            request.damage = damage;
        
            ListResponse<PenaltyDetail> response = _PayrollService.ChildGetAll<PenaltyDetail>(request);
            if (!response.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(response);
                return;
            }




            PostRequest<PenaltyDetail> deleteRequest = new PostRequest<PenaltyDetail>();

            response.Items.ForEach(x =>
            {
                deleteRequest.entity = x;
                PostResponse<PenaltyDetail> r = _PayrollService.ChildDelete<PenaltyDetail>(deleteRequest);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                   Common.errorMessage(r);;
                    return;
                }

            });
               
             

         

        }
    }
}
