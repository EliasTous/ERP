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
using AionHR.Model.Employees.Profile;
using AionHR.Services.Messaging.System;
using Reports;
using AionHR.Model.Attendance;
using AionHR.Web.UI.Forms.ConstClasses;

namespace AionHR.Web.UI.Forms
{
    public partial class Departments : System.Web.UI.Page
    {

        ICompanyStructureService _branchService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ITimeAttendanceService _timeAttendanceService= ServiceLocator.Current.GetInstance<ITimeAttendanceService>();


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
                FillAttendanceScheduleStore();
                FillWorkingCalendarStore();

                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(Department), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
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


            int id = Convert.ToInt32(e.ExtraParams["id"]);
            string buttontype = e.ExtraParams["type"];
            CurrentDepartment.Text = id.ToString();

            switch (buttontype)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id.ToString();
                    RecordResponse<Department> response = _branchService.ChildGetRecord<Department>(r);
                    if (!response.Success)
                    {
                        Common.errorMessage(response);
                        return;
                    }
                    //FillParent();
                   
                    //Step 2 : call setvalues with the retrieved object


                    if (response.result.supervisorId.HasValue)
                    {

                        supervisorId.GetStore().Add(new object[]
                           {
                                new
                                {
                                    recordId = response.result.supervisorId,
                                    fullName =response.result.supervisorName
                                }
                           });
                        supervisorId.SetValue(response.result.supervisorId);
                       

                    }
                    this.BasicInfoTab.SetValues(response.result);
                    //if (!string.IsNullOrEmpty(response.result.scId))
                    //    scId.Select(response.result.scName);
                    //if (!string.IsNullOrEmpty(response.result.caId.ToString()))
                    //    caId.Select(response.result.caName);
                    //if (response.result.type != null)
                    //    type.Select(response.result.type); 
                 
                    // InitCombos(response.result);
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
                Department n = new Department();
                n.recordId = index;
                n.name = "";
                n.departmentRef = "";


                PostRequest<Department> req = new PostRequest<Department>();
                req.entity = n;
                PostResponse<Department> res = _branchService.ChildDelete<Department>(req);
               
                if (!res.Success)
                {



                    //Show an error saving...


                    Common.errorMessage(res);
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
                departmentStore.Reload();
            }

            catch (Exception ex)
            {
                //In case of error, showing a message box to the user
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorDeletingRecord).Show();

            }

        }

        private void FillParent()
        {
            DepartmentListRequest req = new DepartmentListRequest();
            req.type = 0;

            ListResponse<Department> response = _branchService.ChildGetAll<Department>(req);
            if (!response.Success)
            {
                Common.errorMessage(response);
                departmentStore.DataSource = new List<Department>();
            }
            departmentStore.DataSource = response.Items;
            departmentStore.DataBind();
        }
        [DirectMethod]
        public object FillParent(string action, Dictionary<string, object> extraParams)
        {
            StoreRequestParameters prms = new StoreRequestParameters(extraParams);



            List<Department> data;
            DepartmentListRequest req = new DepartmentListRequest();
            req.type = 0;

            ListResponse<Department> response = _branchService.ChildGetAll<Department>(req);
            if (!response.Success)
            {
                Common.errorMessage(response);
                return new List<Department>();
            }
            data = response.Items;
            return new
            {
                data
            };

        }
       
        [DirectMethod]
        public object FillSupervisor(string action, Dictionary<string, object> extraParams)
        {

            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            return Common.GetEmployeesFiltered(prms.Query);

        }
       

        private List<Employee> GetEmployeeByID(string id)
        {

            RecordRequest req = new RecordRequest();
            req.RecordID = id;



            List<Employee> emps = new List<Employee>();
            RecordResponse<Employee> emp = _employeeService.Get<Employee>(req);
            emps.Add(emp.result);
            return emps;
        }
        //private List<Employee> GetEmployeesFiltered(string query)
        //{

        //    EmployeeListRequest req = new EmployeeListRequest();
        //    if (string.IsNullOrEmpty(CurrentDepartment.Text))
        //      req.DepartmentId = "0";
        //    else
        //    req.DepartmentId = CurrentDepartment.Text;
        //    req.BranchId = "0";
        //    req.IncludeIsInactive = 0;
        //    req.SortBy = GetNameFormat();

        //    req.StartAt = "0";
        //    req.Size = "20";
        //    req.Filter = query;




        //    ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
        //    return response.Items;
        //}


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
           // FillParent();
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
            DepartmentListRequest request = new DepartmentListRequest();
            request.Filter = searchTrigger.Text;
            request.type = 0;
            request.isInactive = 2; 
            ListResponse<Department> branches = _branchService.ChildGetAll<Department>(request);
            if (!branches.Success)
            {
                Common.errorMessage(branches);
                return;
            }
            this.Store1.DataSource = branches.Items;
            e.Total = branches.count;

            this.Store1.DataBind();
        }




        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];

            string obj = e.ExtraParams["values"];

            Department b = JsonConvert.DeserializeObject<Department>(obj);
            b.scName = scId.SelectedItem.Text;
            b.recordId = id;
            
            b.caName=caId.SelectedItem.Text;
            // Define the object to add or edit as null
            if (supervisorId.SelectedItem.Text != null)

                b.supervisorName = supervisorId.SelectedItem.Text;
            if (parentId.SelectedItem != null)
                b.parentName = parentId.SelectedItem.Text;
            if (!b.isInactive.HasValue)
                b.isInactive = false;
            if (scId.SelectedItem != null)
                b.scId = scId.SelectedItem.Value;
           
            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<Department> request = new PostRequest<Department>();
                    request.entity = b;
                    PostResponse<Department> r = _branchService.ChildAddOrUpdate<Department>(request);
                    b.recordId = r.recordId;

                    //check if the insert failed
                    if (!r.Success)//it maybe be another condition
                    {
                        //Show an error saving...
                       
                        Common.errorMessage(r);;
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
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorSavingRecord).Show();
                }


            }
            else
            {
                //Update Mode

                try
                {
                    int index = Convert.ToInt32(id);//getting the id of the record
                    PostRequest<Department> request = new PostRequest<Department>();
                    request.entity = b;
                    PostResponse<Department> r = _branchService.ChildAddOrUpdate<Department>(request);                   //Step 1 Selecting the object or building up the object for update purpose

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


                        ModelProxy record = this.Store1.GetById(index);
                        BasicInfoTab.UpdateRecord(record);

                        record.Set("supervisorName", b.supervisorName);
                        record.Set("parentName", b.parentName);
                        record.Set("caName", b.caName);
                        record.Set("scName", b.scName);


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
            departmentStore.Reload();
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
        private string GetNameFormat()
        {
            string format="";
            SystemDefaultRecordRequest req = new SystemDefaultRecordRequest();
            req.Key = "nameFormat";
            RecordResponse<KeyValuePair<string, string>> r = _systemService.ChildGetRecord<KeyValuePair<string, string>>(req);
            if (!r.Success)
            {
                Common.errorMessage(r);;
                return null;
            }
            format = r.result.Value;
            if (string.IsNullOrEmpty( r.result.Value))
            {
              
                PostRequest<KeyValuePair<string, string>> request = new PostRequest<KeyValuePair<string, string>>();
                request.entity = new KeyValuePair<string, string>("nameFormat", "{firstName} {lastName}");
                PostResponse <KeyValuePair<string, string>> resp = _systemService.ChildAddOrUpdate<KeyValuePair<string, string>>(request);
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

        protected void addDepartment(object sender, DirectEventArgs e)
        {
            Department dept = new Department();
            if (string.IsNullOrEmpty(parentId.Text))
                return;
            dept.name = parentId.Text;
            
            PostRequest<Department> depReq = new PostRequest<Department>();
            depReq.entity = dept;
            PostResponse<Department> response = _branchService.ChildAddOrUpdate<Department>(depReq);
            if (response.Success)
            {
                dept.recordId = response.recordId;
                departmentStore.Reload();
                parentId.Select(dept.recordId);
                Store1.Insert(0, dept);
            }
            else
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(response);
                return;
            }

        }



        protected void printBtn_Click(object sender, EventArgs e)
        {
            DepartmentsReport p = GetReport();
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
            DepartmentsReport p = GetReport();
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
            DepartmentsReport p = GetReport();
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
        private DepartmentsReport GetReport()
        {

            DepartmentListRequest request = new DepartmentListRequest();

            request.Filter = "";
            request.type = 0;
            ListResponse<Department> resp = _branchService.ChildGetAll<Department>(request);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
               Common.errorMessage(resp);
                return null;
            }
            DepartmentsReport p = new DepartmentsReport();
            p.DataSource = resp.Items;
            p.Parameters["User"].Value = _systemService.SessionHelper.GetCurrentUser();
            p.RightToLeft = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeft.Yes : DevExpress.XtraReports.UI.RightToLeft.No;
            p.RightToLeftLayout = _systemService.SessionHelper.CheckIfArabicSession() ? DevExpress.XtraReports.UI.RightToLeftLayout.Yes : DevExpress.XtraReports.UI.RightToLeftLayout.No;
            p.Parameters["Yes"].Value = GetGlobalResourceObject("Common", "Yes").ToString();
            p.Parameters["No"].Value = GetGlobalResourceObject("Common", "No").ToString();
            return p;



        }
      
        public void FillAttendanceScheduleStore()
        {
            ListRequest req = new ListRequest();

            ListResponse<AttendanceSchedule> response = _timeAttendanceService.ChildGetAll<AttendanceSchedule>(req);
            if (!response.Success)
            {
                Common.errorMessage(response);
                departmentStore.DataSource = new List<Department>();
            }
            Store2.DataSource = response.Items;
            Store2.DataBind();
        }
      
        public void FillWorkingCalendarStore()
        {
            ListRequest req = new ListRequest();

            ListResponse<WorkingCalendar> response = _timeAttendanceService.ChildGetAll<WorkingCalendar>(req);
            if (!response.Success)
            {
                Common.errorMessage(response);
                departmentStore.DataSource = new List<Department>();
            }
            Store4.DataSource = response.Items;
            Store4.DataBind();
        }

    }
}
