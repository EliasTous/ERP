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
using AionHR.Model.Employees.Profile;
using AionHR.Model.Attributes;
using AionHR.Model.Access_Control;

namespace AionHR.Web.UI.Forms.EmployeePages
{
    public partial class Notes : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();
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
                X.Msg.Alert(GetGlobalResourceObject("Common", "SystemAlerts").ToString(), GetLocalResourceObject("FillNote")).Show();
               
                if (string.IsNullOrEmpty(Request.QueryString["employeeId"]))
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorOperation).Show();
                CurrentEmployee.Text = Request.QueryString["employeeId"];
                EmployeeTerminated.Text = Request.QueryString["terminated"];

                bool disabled = EmployeeTerminated.Text == "1";
                btnAdd.Disabled = disabled;
                if ((bool)_systemService.SessionHelper.Get("IsAdmin"))
                    return;
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(EmployeeNote), null, employeementHistoryGrid, null, btnAdd);
                    ApplyAccessControlOnCaseComments();
                    //AccessControlApplier.ApplyAccessControlOnPage(typeof(CaseComment), null, caseCommentGrid, null, Button1);
                   
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport11.Hidden = true;
                    return;
                }
               
            }

        }
        private void ApplyAccessControlOnCaseComments()
        {
            UserPropertiesPermissions req = new UserPropertiesPermissions();
            req.ClassId = (typeof(EmployeeNote).GetCustomAttributes(typeof(ClassIdentifier), false).ToList()[0] as ClassIdentifier).ClassID;
            req.UserId = _systemService.SessionHelper.GetCurrentUserId();
            ListResponse<UC> resp = _accessControlService.ChildGetAll<UC>(req);

            foreach (var item in resp.Items)
            {
                if (item.propertyId == "3109003")
                {
                    if (item.accessLevel < 2)
                    {
                        employeementHistoryGrid.ColumnModel.Columns[employeementHistoryGrid.ColumnModel.Columns.Count - 1].Renderer.Handler = " return '';";
                        newNoteText.ReadOnly = true;
                    }

                }

                if (item.accessLevel == 0)
                {
                    if (item.propertyId == "3109002")
                    {
                        employeementHistoryGrid.ColumnModel.Columns[1].Renderer.Handler = employeementHistoryGrid.ColumnModel.Columns[1].Renderer.Handler.Replace("s.calendar()", "'***** '");
                    }
                    else
                    {
                        var indices = typeof(EmployeeNote).GetProperties().Where(x =>
                        {
                            var d = x.GetCustomAttributes(typeof(PropertyID), false);
                            if (d.Count() == 0)
                                return false;
                            return (x.GetCustomAttributes(typeof(PropertyID), false).ToList()[0] as PropertyID).ID == item.propertyId;
                        }).ToList();

                        indices.ForEach(x =>
                        {
                            employeementHistoryGrid.ColumnModel.Columns[1].Renderer.Handler = employeementHistoryGrid.ColumnModel.Columns[1].Renderer.Handler.Replace("record.data['" + x.Name + "']", "'***** '");
                        });
                    }

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
       
        protected void ADDNewRecord(object sender, DirectEventArgs e)
        {
            string noteText = e.ExtraParams["noteText"];
            X.Call("ClearNoteText");
            PostRequest<EmployeeNote> req = new PostRequest<EmployeeNote>();
            EmployeeNote note = new EmployeeNote();
            //note.recordId = id;
            note.employeeId =CurrentEmployee.Text;
            note.note = noteText;
            note.recordId = "";
            note.date = DateTime.Now;
            req.entity = note;

            
            PostResponse<EmployeeNote> resp = _employeeService.ChildAddOrUpdate<EmployeeNote>(req);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.LogId : resp.Summary).Show();
                
            }
            employeementHistoryStore.Reload();
            
            //Reset all values of the relative object

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
                this.Viewport11.RTL = true;

            }
        }



        protected void PoPuP(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
            string index = e.ExtraParams["index"];
            switch (type)
            {
               

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteEH({0})", id),
                            Text = Resources.Common.Yes
                        },
                        No = new MessageBoxButtonConfig
                        {
                            Text = Resources.Common.No
                        }

                    }).Show();
                    break;
                case "imgEdit":
                    X.Call("App.employeementHistoryGrid.editingPlugin.startEdit",Convert.ToInt32(index),0);
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
        public void DeleteEH(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                EmployeeNote n = new EmployeeNote();
                n.recordId = index;
                n.note = "";
                n.date = DateTime.Now;
                n.employeeId =CurrentEmployee.Text;
                
                PostRequest<EmployeeNote> req = new PostRequest<EmployeeNote>();
                req.entity = n;
                PostResponse<EmployeeNote> res = _employeeService.ChildDelete<EmployeeNote>(req);
                if (!res.Success)
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, res.Summary).Show();
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    employeementHistoryStore.Remove(index);

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
     

        protected void employeementHistory_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News

            EmployeeNotesListRequest req = new EmployeeNotesListRequest();
            req.EmployeeId = Convert.ToInt32(CurrentEmployee.Text);
            req.SortBy = "firstName";
            ListResponse<EmployeeNote> notes = _employeeService.ChildGetAll<EmployeeNote>(req);
            if (!notes.Success)
                X.Msg.Alert(Resources.Common.Error, notes.Summary).Show();
            this.employeementHistoryStore.DataSource = notes.Items;
            e.Total = notes.count;

            this.employeementHistoryStore.DataBind();
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

        

        [DirectMethod]
        public object ValidateSave(bool isPhantom,string obj, JsonObject values)
        {
            

            if (!values.ContainsKey("note") )
            {
                return new { valid = false, msg = "Salary must be >=1000 for new employee" };
            }

            PostRequest<EmployeeNote> req = new PostRequest<EmployeeNote>();
            EmployeeNote note = JsonConvert.DeserializeObject<List<EmployeeNote>>(obj)[0];
            //note.recordId = id;
            note.employeeId = CurrentEmployee.Text;
            note.note = values["note"].ToString();
            int bulk;
          
            req.entity = note;
            
            PostResponse<EmployeeNote> resp = _employeeService.ChildAddOrUpdate<EmployeeNote>(req);
            if(!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.LogId : resp.Summary).Show();
                return new { valid = false };
            }
            employeementHistoryStore.Reload();
            return new { valid = true };
        }

    }
}