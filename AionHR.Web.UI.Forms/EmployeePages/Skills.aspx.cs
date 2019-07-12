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
using System.Net;
using AionHR.Web.UI.Forms.ConstClasses;

namespace AionHR.Web.UI.Forms.EmployeePages
{
    public partial class Skills : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
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
                if (string.IsNullOrEmpty(Request.QueryString["employeeId"]))
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorOperation).Show();
                CurrentEmployee.Text = Request.QueryString["employeeId"];
                dateFrom.Format = dateTo.Format = dateToColumn.Format = dateFromColumn.Format = _systemService.SessionHelper.GetDateformat();
                EmployeeTerminated.Text = Request.QueryString["terminated"];

                bool disabled = EmployeeTerminated.Text == "1";
                btnAdd.Disabled = SaveSkillButton.Disabled = disabled;
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(EmployeeCertificate), SkillsForm, skillsGrid, btnAdd, SaveSkillButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport11.Hidden = true;
                    return;
                }
                if (remarks.InputType == InputType.Password)
                {
                    remarks.Visible = false;
                    remarksField.Visible = true;
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
                this.Viewport11.RTL = true;

            }
        }



        protected void PoPuP(object sender, DirectEventArgs e)
        {


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string type = e.ExtraParams["type"];
            string path = e.ExtraParams["path"];
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    EmployeeCertificate entity = GetById(id.ToString());
                    //Step 2 : call setvalues with the retrieved object
                    this.SkillsForm.SetValues(entity);
                    FillCertificateLevels();
                    clId.Select(entity.clId.ToString());

                    this.EditSkillWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditSkillWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteSkill({0})", id),
                            Text = Resources.Common.Yes
                        },
                        No = new MessageBoxButtonConfig
                        {
                            Text = Resources.Common.No
                        }

                    }).Show();
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
        public void DeleteSkill(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                EmployeeCertificate n = new EmployeeCertificate();
                n.recordId = index;
                n.clId = 0;
                n.employeeId = Convert.ToInt32(CurrentEmployee.Text);
                n.dateFrom = DateTime.Now;
                n.dateTo = DateTime.Now;
                n.grade = "0";
                n.institution = "";
                n.major = "";
                


                PostRequest<EmployeeCertificate> req = new PostRequest<EmployeeCertificate>();
                req.entity = n;
                PostResponse<EmployeeCertificate> res = _employeeService.ChildDelete<EmployeeCertificate>(req);
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
                    skillStore.Remove(index);

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


        /// <summary>
        /// Direct method for removing multiple records
        /// </summary>


        /// <summary>
        /// Adding new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ADDNewRecord(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            SkillsForm.Reset();
            this.EditSkillWindow.Title = Resources.Common.AddNewRecord;
            FillCertificateLevels();

            this.EditSkillWindow.Show();
        }


        protected void skillsStore_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News

            EmployeeDocumentsListRequest request = new EmployeeDocumentsListRequest();
            request.EmployeeId = Convert.ToInt32(CurrentEmployee.Text);
            ListResponse<EmployeeCertificate> documents = _employeeService.ChildGetAll<EmployeeCertificate>(request);
            if (!documents.Success)
            {
                Common.errorMessage(documents);
                return;
            }
            this.skillStore.DataSource = documents.Items;
            e.Total = documents.count;

            this.skillStore.DataBind();
        }



        protected void SaveDocument(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];
            EmployeeCertificate b = JsonConvert.DeserializeObject<EmployeeCertificate>(obj);
            b.employeeId = Convert.ToInt32(CurrentEmployee.Text);
            b.recordId = id;
            // Define the object to add or edit as null
            b.clName = clId.SelectedItem.Text;
            if (b.dateFrom != null)
            {
                DateTime Date =(DateTime) b.dateFrom; 
                b.dateFrom = new DateTime(Date.Year, Date.Month, Date.Day, 14, 0, 0);
            }
            if (b.dateTo != null)
            {
                DateTime Date = (DateTime)b.dateTo;
                b.dateTo = new DateTime(Date.Year, Date.Month, Date.Day, 14, 0, 0);
            }

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    PostRequest<EmployeeCertificate> request = new PostRequest<EmployeeCertificate>();
                    request.entity = b;

                    PostResponse<EmployeeCertificate> r = _employeeService.ChildAddOrUpdate< EmployeeCertificate>(request);
                    b.recordId = r.recordId;


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
                        this.skillStore.Insert(0, b);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        this.EditSkillWindow.Close();
                        RowSelectionModel sm = this.skillsGrid.GetSelectionModel() as RowSelectionModel;
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
                    PostRequest<EmployeeCertificate> request = new PostRequest<EmployeeCertificate>();
                    request.entity = b; 



                    PostResponse<EmployeeCertificate> r = _employeeService.ChildAddOrUpdate<EmployeeCertificate>(request);


                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                         Common.errorMessage(r);
                        return;
                    }
                    else
                    {

                        
                        ModelProxy record = this.skillStore.GetById(index);

                        SkillsForm.UpdateRecord(record);
                        record.Set("clName", b.clName);
                        
                        record.Commit();
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });
                        this.EditSkillWindow.Close();


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

        private void FillCertificateLevels()
        {
            ListRequest documentTypes = new ListRequest();
            ListResponse<CertificateLevel> resp = _employeeService.ChildGetAll<CertificateLevel>(documentTypes);
            if (!resp.Success)
            {
               Common.errorMessage(resp);
                return;
            }
            clStore.DataSource = resp.Items;
            clStore.DataBind();

        }

        protected void addCl(object sender, DirectEventArgs e)
        {
            CertificateLevel dept = new CertificateLevel();
            dept.name = clId.Text;

            PostRequest<CertificateLevel> depReq = new PostRequest<CertificateLevel>();
            depReq.entity = dept;

            PostResponse<CertificateLevel> response = _employeeService.ChildAddOrUpdate<CertificateLevel>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillCertificateLevels();
                clId.Select(response.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return;
            }

        }

        private EmployeeCertificate GetById(string id)
        {
            RecordRequest r = new RecordRequest();
            r.RecordID = id.ToString();
            RecordResponse<EmployeeCertificate> response = _employeeService.ChildGetRecord<EmployeeCertificate>(r);
            if (!response.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                 Common.errorMessage(response);
                return null;
            }
            return response.result;
        }
    }
}