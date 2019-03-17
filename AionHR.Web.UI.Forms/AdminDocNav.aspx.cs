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
using AionHR.Model.AdminTemplates;
using AionHR.Services.Messaging.AdministrativeAffairs;
using AionHR.Web.UI.Forms.ConstClasses;
using AionHR.Model;
using AionHR.Services.Messaging.System;

namespace AionHR.Web.UI.Forms
{
    public partial class AdminDocNav : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();

        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        IAdministrationService _administrationService = ServiceLocator.Current.GetInstance<IAdministrationService>();
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
                currentEmployeeId.Text = _systemService.SessionHelper.GetEmployeeId();
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(AdminDocument), BasicInfoTab1, GridPanel1, null, null);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied, "closeCurrentTab()").Show();
                    Viewport1.Hidden = true;


                    


                    return;
                }
                FillDepartment();



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

        /// <summary>
        /// the detailed tabs for the edit form. I put two tabs by default so hide unecessary or add addional
        /// </summary>
        private void HideShowTabs()
        {
            //this.OtherInfoTab.Visible = false;
        }

        protected void PoPuP(object sender, DirectEventArgs e)
        {
            

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
        protected void saveDocumentTransfer(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            try
            {


                string obj = e.ExtraParams["values"];

                AdminDocTransfer b = JsonConvert.DeserializeObject<AdminDocTransfer>(obj);



                
                    b.apStatus = "1";
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<AdminDocTransfer> request = new PostRequest<AdminDocTransfer>();

                    request.entity = b;
                    PostResponse<AdminDocTransfer> r = _administrationService.ChildAddOrUpdate<AdminDocTransfer>(request);


                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(r); ;
                        return;
                    }
                    else
                    {


                    //Add this record to the store 
                    documentsStore.Reload();

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });



                        DocumentTransferWindow.Close();

                    }
                }



              
            
            catch (Exception ex)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
            }
        }
        protected void documentsStore_ReadData(object sender, StoreReadDataEventArgs e)
        {
            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            DocumentListRequest request = new DocumentListRequest();
            request.Status = 0;
            request.Filter = "";

            ListResponse<AdminDocument> routers = _administrationService.ChildGetAll<AdminDocument>(request);
            if (!routers.Success)
            {
                Common.errorMessage(routers);
                return;
            }
            this.documentsStore.DataSource = routers.Items;
            e.Total = routers.Items.Count; ;

            this.documentsStore.DataBind();
        }

        #region comboboxFilling
        private void FillDepartment()
        {
            DepartmentListRequest departmentsRequest = new DepartmentListRequest();
            departmentsRequest.type = 0;
            ListResponse<Department> resp = _companyStructureService.ChildGetAll<Department>(departmentsRequest);
            if (!resp.Success)
                Common.errorMessage(resp);
            departmentStore.DataSource = resp.Items;
            departmentStore.DataBind();
        }
        private string GetNameFormat()
        {
            string format = "";
            SystemDefaultRecordRequest req = new SystemDefaultRecordRequest();
            req.Key = "nameFormat";
            RecordResponse<KeyValuePair<string, string>> r = _systemService.ChildGetRecord<KeyValuePair<string, string>>(req);
            if (!r.Success)
            {
                Common.errorMessage(r); ;
                return null;
            }
            format = r.result.Value;
            if (string.IsNullOrEmpty(r.result.Value))
            {

                PostRequest<KeyValuePair<string, string>> request = new PostRequest<KeyValuePair<string, string>>();
                request.entity = new KeyValuePair<string, string>("nameFormat", "{firstName} {lastName}");
                PostResponse<KeyValuePair<string, string>> resp = _systemService.ChildAddOrUpdate<KeyValuePair<string, string>>(request);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return null;
                }
                format = "{firstName} {lastName}";

            }


            string paranthized = format;
            paranthized = paranthized.Replace('{', ' ');
            paranthized = paranthized.Replace('}', ',');
            paranthized = paranthized.Substring(0, paranthized.Length - 1);
            paranthized = paranthized.Replace(" ", string.Empty);
            return paranthized;

        }
        private List<Employee> GetEmployeesFiltered(string query)
        {

            EmployeeListRequest req = new EmployeeListRequest();

            req.DepartmentId = "0";

            req.BranchId = "0";
            req.IncludeIsInactive = 0;
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
            if (data == null)
                data = new List<Employee>();
            data.ForEach(s => { s.fullName = s.name.fullName; });
            //  return new
            // {
            return data;
            //};

        }
        protected void addDepartment(object sender, DirectEventArgs e)
        {
            Department dept = new Department();
            dept.name = departmentId.Text;

            PostRequest<Department> depReq = new PostRequest<Department>();
            depReq.entity = dept;
            PostResponse<Department> response = _companyStructureService.ChildAddOrUpdate<Department>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                FillDepartment();
                departmentId.Select(dept.recordId);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(response);
                return;
            }

        }

        #endregion

    }
}