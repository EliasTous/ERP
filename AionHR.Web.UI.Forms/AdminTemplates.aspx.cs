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
using AionHR.Model.AdminTemplates;
using System.Text.RegularExpressions;
using AionHR.Web.UI.Forms.ConstClasses;

namespace AionHR.Web.UI.Forms
{
    public partial class AdminTemplates : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();


        IAdministrationService _administrationService = ServiceLocator.Current.GetInstance<IAdministrationService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
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
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(AdTemplate), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied, "closeCurrentTab()").Show();
                    Viewport1.Hidden = true;





                    return;
                }
                string s = File.ReadAllText(MapPath("~/Utilities/letters_meta_tags.txt"));
                List<TagGroup> groups = JsonConvert.DeserializeObject<List<TagGroup>>(s);
                List<string> empTags = null;
                List<string> adminAffairsTags = null;
                List<string> leaveTags = null;
                List<string> loanTags = null;
                List<string> scheduleTags = null;
                List<string> penaltyTags = null;
                List<string> tvar = null;
                foreach (var item in groups)
                {
                    switch(item.type)
                    {
                        case "employee": empTags= item.tags; break;
                        case "admin_affair": adminAffairsTags= item.tags; break;
                        case "leave": leaveTags = item.tags; break;
                        case "loan": loanTags = item.tags; break;
                        case "schedule": scheduleTags = item.tags; break;
                        case "penalty": penaltyTags = item.tags; break;
                        case "time_variations": tvar = item.tags; break;
                        default:break;
                    }
                }
                for(int i=0;i<empTags.Count;i++)
                {
                    try
                    {
                        empTags[i] += " (" + GetLocalResourceObject(empTags[i]).ToString() + ")";
                    }
                    catch { }
                }
                for (int i = 0; i < adminAffairsTags.Count; i++)
                {
                    try
                    {
                        adminAffairsTags[i] += " (" + GetLocalResourceObject(adminAffairsTags[i]).ToString() + ")";
                    }
                    catch { }
                }
                for (int i = 0; i < leaveTags.Count; i++)
                {
                    try
                    {
                        leaveTags[i] += " (" + GetLocalResourceObject(leaveTags[i]).ToString() + ")";
                    }
                    catch { }
                }
                for (int i = 0; i < loanTags.Count; i++)
                {
                    try
                    {
                        loanTags[i] += " (" + GetLocalResourceObject(loanTags[i]).ToString() + ")";
                    }
                    catch { }
                }
                for (int i = 0; i < scheduleTags.Count; i++)
                {
                    try
                    {
                        scheduleTags[i] += " (" + GetLocalResourceObject(scheduleTags[i]).ToString() + ")";
                    }
                    catch { }
                }
                for (int i = 0; i < penaltyTags.Count; i++)
                {
                    try
                    {
                        penaltyTags[i] += " (" + GetLocalResourceObject(penaltyTags[i]).ToString() + ")";
                    }
                    catch { }
                }
                for (int i = 0; i < tvar.Count; i++)
                {
                    try
                    {
                        tvar[i] += " (" + GetLocalResourceObject(tvar[i]).ToString() + ")";
                    }
                    catch { }
                }
                X.Call("InitTags", empTags, adminAffairsTags, leaveTags, loanTags,scheduleTags,penaltyTags,tvar);
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


            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id;

                    RecordResponse<AdTemplate> response = _administrationService.ChildGetRecord<AdTemplate>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.BasicInfoTab.SetValues(response.result);
                    recordId.Text = id;
                    Store2.Reload();

                    templateUsage.Text = response.result.usage.ToString();

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
                AdTemplate s = new AdTemplate();
                s.recordId = index;
                s.name = "";
                //s.intName = "";

                PostRequest<AdTemplate> req = new PostRequest<AdTemplate>();
                req.entity = s;
                PostResponse<AdTemplate> r = _administrationService.ChildDelete<AdTemplate>(req);
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
        [DirectMethod]
        public void DeleteBody(string te, string language)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                TemplateBody s = new TemplateBody();
                s.teId = Convert.ToInt32(te);
                s.languageId = Convert.ToInt32(language);
                //s.intName = "";

                PostRequest<TemplateBody> req = new PostRequest<TemplateBody>();
                req.entity = s;
                PostResponse<TemplateBody> r = _administrationService.ChildDelete<TemplateBody>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                   Common.errorMessage(r);;
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    Store2.Remove(language);

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
            Store2.DataSource = new List<TemplateBody>();
            Store2.DataBind();
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
            ListResponse<AdTemplate> routers = _administrationService.ChildGetAll<AdTemplate>(request);
            if (!routers.Success)
            {
                 Common.errorMessage(routers);
                return;
            }
            this.Store1.DataSource = routers.Items;
            e.Total = routers.Items.Count;

            this.Store1.DataBind();
        }




        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.


            string obj = e.ExtraParams["values"];
            AdTemplate b = JsonConvert.DeserializeObject<AdTemplate>(obj);

            string id = e.ExtraParams["id"];
            string text = e.ExtraParams["html_text"];
            // Define the object to add or edit as null
            //b.body = text;
            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<AdTemplate> request = new PostRequest<AdTemplate>();

                    request.entity = b;

                    PostResponse<AdTemplate> respons = _administrationService.ChildAddOrUpdate<AdTemplate>(request);


                    //check if the insert failed
                    if (!respons.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(respons);
                        return;
                    }
                    else
                    {
                        b.recordId = respons.recordId;
                        recordId.Text = respons.recordId;
                        currentUsage.Text = b.usage.ToString();
                        //Add this record to the store 
                        this.Store1.Insert(0, b);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });


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
                    //getting the id of the record
                    PostRequest<AdTemplate> request = new PostRequest<AdTemplate>();
                    request.entity = b;
                    PostResponse<AdTemplate> r = _administrationService.ChildAddOrUpdate<AdTemplate>(request);                      //Step 1 Selecting the object or building up the object for update purpose

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


                        ModelProxy record = this.Store1.GetById(id);
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


        protected void SaveTemplateBody(object sender, DirectEventArgs e)
        {
            string obj = e.ExtraParams["values"];
            TemplateBody b = JsonConvert.DeserializeObject<TemplateBody>(obj);
            b.textBody = e.ExtraParams["html_text"];
            
            b.teId = Convert.ToInt32(e.ExtraParams["teId"]);
            PostRequest<TemplateBody> req = new PostRequest<TemplateBody>();
            req.entity = b;
            b.textBody = b.textBody.Substring(1, b.textBody.Length - 2); ;
            PostResponse<TemplateBody> r = _administrationService.ChildAddOrUpdate<TemplateBody>(req);                      //Step 1 Selecting the object or building up the object for update purpose
            UpdateTags(b.teId, Server.UrlDecode(b.textBody));
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


                
                Store2.Reload();

                
                Store2.DataBind();


                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordUpdatedSucc
                });

                
                this.TemplateBodyWindow.Close();
                if(e.ExtraParams["isPreview"]!="")
                {
                    if (e.ExtraParams["templateUsage"] == "2")
                    {
                        selectEmpForm.Reset();
                        selectEmpWindow.Show();
                    }
                }

            }

        }

        protected void ShowEmployeePreview(object sender, DirectEventArgs e)
        {
            string languageId = e.ExtraParams["languageId"];
            string teId = e.ExtraParams["teId"];
            string empId = e.ExtraParams["empId"];

            EmployeeTemplatePreviewRecordRequest req = new EmployeeTemplatePreviewRecordRequest();
            req.EmployeeId = Convert.ToInt32(empId);
            req.LanguageId = Convert.ToInt32(languageId);
            req.TemplateId = Convert.ToInt32(teId);

            RecordResponse<EmployeeTemplatePreview> r = _administrationService.ChildGetRecord<EmployeeTemplatePreview>(req);

            if (!r.Success)//it maybe another check
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
               Common.errorMessage(r);;
                return;
            }
            selectEmpWindow.Hide();
            
            templatePreviewWindow.Show();
            Panel1.Html = r.result.textBody;
        }


        protected void ClosePreview(object sender, DirectEventArgs e)
        {
            string languageId = e.ExtraParams["languageId"];
            string teId = e.ExtraParams["teId"];


            TemplateBody body = new TemplateBody() { languageId = Convert.ToInt32(languageId), teId = Convert.ToInt32(teId) };
            TemplateBodyRecordRequest req = new TemplateBodyRecordRequest() { LanguageId = Convert.ToInt32(languageId), TemplateId = Convert.ToInt32(teId) };
            RecordResponse<TemplateBody> r = _administrationService.ChildGetRecord<TemplateBody>(req);                      //Step 1 Selecting the object or building up the object for update purpose

            //Step 2 : saving to store

            //Step 3 :  Check if request fails
            if (!r.Success || r.result == null)//it maybe another check
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
               Common.errorMessage(r);;
                return;
            }

            bodyText.Text = r.result.textBody;
            body.textBody = r.result.textBody;
            //Step 2 : call setvalues with the retrieved object
            this.TemplateBodyForm.SetValues(body);



            this.TemplateBodyWindow.Show();
            templatePreviewWindow.Hide();

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

        protected void AddNewBody(object sender, DirectEventArgs e)
        {

            TemplateBodyForm.Reset();
            bodyText.Text = "";
            //Reset all values of the relative object
            if (e.ExtraParams["teId"] != "")
                teId.Text = e.ExtraParams["teId"];
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetLocalResourceObject("SaveTemplateFirst").ToString()).Show();
                return;
            }




            this.TemplateBodyWindow.Show();
        }
        protected void PoPuPBody(object sender, DirectEventArgs e)
        {


            string teId = e.ExtraParams["teId"];
            string languageId = e.ExtraParams["languageId"];

            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    TemplateBody body = new TemplateBody() { languageId = Convert.ToInt32(languageId), teId = Convert.ToInt32(teId) };
                    TemplateBodyRecordRequest req = new TemplateBodyRecordRequest() { LanguageId = Convert.ToInt32(languageId), TemplateId = Convert.ToInt32(teId) };
                    RecordResponse<TemplateBody> r = _administrationService.ChildGetRecord<TemplateBody>(req);                      //Step 1 Selecting the object or building up the object for update purpose

                    //Step 2 : saving to store

                    //Step 3 :  Check if request fails
                    if (!r.Success || r.result==null)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                       Common.errorMessage(r);;
                        return;
                    }
                    
                    bodyText.Text = r.result.textBody;
                    body.textBody = r.result.textBody; 
                    //Step 2 : call setvalues with the retrieved object
                    this.TemplateBodyForm.SetValues(body);



                    this.TemplateBodyWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteBody({0},{1})", teId, languageId),
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

        protected void Store2_ReadData(object sender, StoreReadDataEventArgs e)
        {
            TemplateBodyListReuqest req = new TemplateBodyListReuqest();
            req.TemplateId = Convert.ToInt32(recordId.Text);
            ListResponse<TemplateBody> bodies = _administrationService.ChildGetAll<TemplateBody>(req);
            if (!bodies.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(bodies);
                return;
            }
            this.Store2.DataSource = bodies.Items;
            Store2.DataBind();
        }

        private void UpdateTags(int templateId,string plainHtml)
        {

            PostRequest<TemplateTag> dellAll = new PostRequest<TemplateTag>();
            dellAll.entity = new TemplateTag() { teId = templateId, tag = String.Empty };
            PostResponse<TemplateTag> resp = _administrationService.ChildDelete<TemplateTag>(dellAll);
            if (!resp.Success)//it maybe another check
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(resp);
                return;
            }
            MatchCollection c = Regex.Matches(plainHtml,@"\#(?<word>\w+)#");
            List<TemplateTag> tags = new List<TemplateTag>();
            for(int i=0;i<c.Count;i++)       
            {

                tags.Add(new TemplateTag() { teId = templateId, tag = c[i].Value.Substring(1,c[i].Value.Length-2) });
            }

            PostRequest<TemplateTag[]> req = new PostRequest<TemplateTag[]>();
            req.entity = tags.ToArray();
            PostResponse<TemplateTag[]> r = _administrationService.ChildAddOrUpdate<TemplateTag[]>(req);                    

            //Step 2 : saving to store

            //Step 3 :  Check if request fails
            if (!r.Success)//it maybe another check
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
               Common.errorMessage(r);;
                return;
            }

        }

        private string GetNameFormat()
        {
            return _systemService.SessionHelper.Get("nameFormat").ToString();
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
    }
}