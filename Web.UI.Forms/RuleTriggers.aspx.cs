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
using Model.Access_Control;
using Services.Messaging.CompanyStructure;

namespace Web.UI.Forms
{
    public partial class RuleTriggers : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();

        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();

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
                modulesCombo1.ADDHandler("change", "App.CurrentModule.setValue(this.value); App.classesStore.reload();");
                accessTypeStore.DataSource = Common.XMLDictionaryList(_systemService, "33");
                accessTypeStore.DataBind();
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(CertificateLevel), null, GridPanel1, btnAdd, SaveButton);
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



        protected void classesStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            if (!string.IsNullOrEmpty(CurrentModule.Text))
            {
                AccessControlListRequest req = new AccessControlListRequest();
                req.GroupId = "0";
                req.ModuleId = CurrentModule.Text;
                ListResponse<ModuleClass> resp = _systemService.ChildGetAll<ModuleClass>(req);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }


                classesStore.DataSource = resp.Items;
                classesStore.DataBind();
            }
        }
        protected void PoPuPClass(object sender, DirectEventArgs e)
        {

            ruleSelector.Disabled = true;
            string classId = e.ExtraParams["classId"];
            string ClassNameParam = e.ExtraParams["className"];
            classSelectedId.Text = classId;
            className.Text = ClassNameParam;
         //   ruleSelectorStore.Reload();
            TriggerWindow.Show();




        }
        protected void PoPuP(object sender, DirectEventArgs e)
        {


            string accessTypeParam = e.ExtraParams["accessType"];
            string ruleId = e.ExtraParams["ruleId"];
            string classId = e.ExtraParams["classId"];
            string ClassNameParam = e.ExtraParams["className"];
            string seqNo = e.ExtraParams["seqNo"];
            

            string type = e.ExtraParams["type"];
            CurrentRuleId.Text = ruleId;
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 

                    ruleSelector.Disabled = false;
                    accessType.Select(accessTypeParam);
                    classSelectedId.Text = classId;
                    className.Text = ClassNameParam;
                    ruleSelectorStore.Reload();
                    TriggerWindow.Show();

                    
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteRecord({0},{1},{2},{3})", ruleId, classId, accessTypeParam,seqNo),
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
        public void DeleteRecord(string ruleId, string classId,string accessType,string seqNo)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                RuleTrigger s = new RuleTrigger();
                s.ruleId = ruleId;
                s.classId = classId;
                s.accessType = accessType;
                s.seqNo = seqNo;
                PostRequest<RuleTrigger> req = new PostRequest<RuleTrigger>();
                req.entity = s;
                PostResponse<RuleTrigger> r = _companyStructureService.ChildDelete<RuleTrigger>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(r);
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    Store1.Reload();

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
            //BasicInfoTab.Reset();
            CurrentRuleId.Text = "0";
            EditRecordWindow.Show();
            accessType.Reset();
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;


            this.EditRecordWindow.Show();
        }
        protected void ruleSelectorStore_ReadData(object sender, StoreReadDataEventArgs e)
        {

            if (string.IsNullOrEmpty(accessType.SelectedItem.Value))
                return;


            ListRequest Listreq = new ListRequest();

            Listreq.Filter = "";
            ListResponse<Model.Company.Structure.Rule> allRulesTrigger = _companyStructureService.ChildGetAll<Model.Company.Structure.Rule>(Listreq);


           

           
            if (!allRulesTrigger.Success)
            {
                Common.errorMessage(allRulesTrigger);
                return;
            }

            RuleTriggerListRequset selectedRequest = new RuleTriggerListRequset();
            selectedRequest.ruleId = string.IsNullOrEmpty(CurrentRuleId.Text) ? "0" : CurrentRuleId.Text;
            selectedRequest.accessType = accessType.SelectedItem.Value;
            selectedRequest.classId = classSelectedId.Text;


            ListResponse<RuleTrigger> selectedRuleTrigger = _companyStructureService.ChildGetAll<RuleTrigger>(selectedRequest);

            if (!selectedRuleTrigger.Success)
            {
                Common.errorMessage(selectedRuleTrigger);
                return;
            }

            //selectedRuleTrigger.Items.ForEach(x =>
            //{
            //    allRulesTrigger.Items.Remove(x);

            //});

            ruleSelectorStore.DataSource = allRulesTrigger.Items;
            ruleSelectorStore.DataBind();

            this.ruleSelector.SelectedItems.Clear();
            selectedRuleTrigger.Items.ForEach(x =>
            {
                this.ruleSelector.SelectedItems.Add(new Ext.Net.ListItem() { Value = x.ruleId  });
            });

            this.ruleSelector.UpdateSelectedItems();
            this.ruleSelector.Update();


        }
        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            //string filter = string.Empty;
            //int totalCount = 1;



            ////Fetching the corresponding list

            ////in this test will take a list of News
            RuleTriggerListRequset Listreq = new RuleTriggerListRequset();
            Listreq.ruleId = "0";
            Listreq.accessType = "0";
            Listreq.classId = "0";
            ListResponse<RuleTrigger> resp = _companyStructureService.ChildGetAll<RuleTrigger>(Listreq);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }

            this.Store1.DataSource = resp.Items;
            e.Total = resp.Items.Count; ;

            this.Store1.DataBind();
        }



        protected void saveNewTrigger(object sender, DirectEventArgs e)
        {
            try
            {

                //Getting the id to check if it is an Add or an edit as they are managed within the same form.
                string classIdParameter = e.ExtraParams["classId"];
                string accessTypeParameter = e.ExtraParams["accessType"];


                List<Model.Company.Structure.RuleTrigger> selectedRules = new List<Model.Company.Structure.RuleTrigger>();
                int count = 1;
                foreach (var item in ruleSelector.SelectedItems)
                {

                    selectedRules.Add(new Model.Company.Structure.RuleTrigger() { ruleId = item.Value, classId = classIdParameter, accessType = accessTypeParameter, seqNo = count.ToString() });
                    count++;
                }

                RuleTriggerListRequset Listreq = new RuleTriggerListRequset();
                Listreq.ruleId = "0";
                Listreq.accessType = accessTypeParameter;
                Listreq.classId = classIdParameter;
                ListResponse<RuleTrigger> Listresp = _companyStructureService.ChildGetAll<RuleTrigger>(Listreq);
                if (!Listresp.Success)
                {
                    Common.errorMessage(Listresp);
                    return;
                }

                PostRequest<RuleTrigger> req = new PostRequest<RuleTrigger>();
                Listresp.Items.ForEach(x =>
                 {
                     req.entity = x;
                     PostResponse<RuleTrigger> delresp = _companyStructureService.ChildDelete<RuleTrigger>(req);
                     if (!delresp.Success)
                     {
                         Common.errorMessage(delresp);
                         throw new Exception();
                     }
                 });

                    selectedRules.ForEach(x =>
                    {
                        req.entity = x;
                        PostResponse<RuleTrigger> resp = _companyStructureService.ChildAddOrUpdate<RuleTrigger>(req);
                        if (!resp.Success)
                        {
                            Common.errorMessage(resp);
                            throw new Exception();
                        }
                    });






                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordSavingSucc
                    });


                Store1.Reload();
              
            }
            catch(Exception exp)
            {
                X.MessageBox.Alert(Resources.Common.Error,exp.Message).Show(); 
            }
            



        }
        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


          


            //string obj = e.ExtraParams["values"];
            //CertificateLevel b = JsonConvert.DeserializeObject<CertificateLevel>(obj);

            //string id = e.ExtraParams["id"];
            //// Define the object to add or edit as null

            //if (string.IsNullOrEmpty(id))
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
        protected void printBtn_Click(object sender, EventArgs e)
        {
            CertificateLevelsReport p = GetReport();
            string format = "Pdf";
            string fileName = String.Format("Report.{0}", format);

            MemoryStream ms = new MemoryStream();
            p.ExportToPdf(ms, new DevExpress.XtraPrinting.PdfExportOptions() { ShowPrintDialogOnOpen = true });
            Response.Clear();
            Response.Write("<script>");
            Response.Write("window.document.forms[0].target = '_blank';");
            Response.Write("setTimeout(function () { window.document.forms[0].target = ''; }, 0);");
            Response.Write("</script>");
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "inline", fileName));
            Response.BinaryWrite(ms.ToArray());
            Response.Flush();
            Response.Close();
            //Response.Redirect("Reports/RT301.aspx");
        }
        protected void ExportPdfBtn_Click(object sender, EventArgs e)
        {
            CertificateLevelsReport p = GetReport();
            string format = "Pdf";
            string fileName = String.Format("Report.{0}", format);

            MemoryStream ms = new MemoryStream();
            p.ExportToPdf(ms);
            Response.Clear();

            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "attachment", fileName));
            Response.BinaryWrite(ms.ToArray());
            Response.Flush();
            Response.Close();
            //Response.Redirect("Reports/RT301.aspx");
        }

        protected void ExportXLSBtn_Click(object sender, EventArgs e)
        {
            CertificateLevelsReport p = GetReport();
            string format = "xls";
            string fileName = String.Format("Report.{0}", format);

            MemoryStream ms = new MemoryStream();
            p.ExportToXls(ms);

            Response.Clear();

            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", String.Format("{0}; filename={1}", "attachment", fileName));
            Response.BinaryWrite(ms.ToArray());
            Response.Flush();
            Response.Close();
            //Response.Redirect("Reports/RT301.aspx");
        }
        private CertificateLevelsReport GetReport()
        {

            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<CertificateLevel> resp = _employeeService.ChildGetAll<CertificateLevel>(request);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(resp);
                return null;
            }
            CertificateLevelsReport p = new CertificateLevelsReport();
            p.DataSource = resp.Items;
            p.Parameters["User"].Value = _systemService.SessionHelper.GetCurrentUser();
            p.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            p.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;

            return p;



        }

    }
}