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
using AionHR.Model.Employees.Leaves;
using AionHR.Model.Attendance;
using AionHR.Model.TimeAttendance;

namespace AionHR.Web.UI.Forms
{
    public partial class DaysAvailability : System.Web.UI.Page
    {
        ICompanyStructureService _branchService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ITimeAttendanceService _timeAttendanceService = ServiceLocator.Current.GetInstance<ITimeAttendanceService>();
        IHelpFunctionService _helpFunctionService = ServiceLocator.Current.GetInstance<IHelpFunctionService>();
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
                FillBranches();
              
             
            }


        }

        private void FillBranches()
        {
            ListRequest request = new ListRequest();
            request.Filter = string.Empty;
            ListResponse<Branch> branches = _branchService.ChildGetAll<Branch>(request);
            if (!branches.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", branches.ErrorCode) != null ? GetGlobalResourceObject("Errors", branches.ErrorCode).ToString() : branches.Summary).Show();
                return;
            }
            this.branchStore.DataSource = branches.Items;
            this.branchStore.DataBind();
        }

              
        protected void Load_Click(object sender, DirectEventArgs e)
        {
         
         
            //Proceed to load

            BranchScheduleRecordRequest reqFS = new BranchScheduleRecordRequest();
            reqFS.EmployeeId = 0;
            reqFS.FromDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            reqFS.ToDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            reqFS.BranchId = 0;
            ListResponse<FlatSchedule> response = _timeAttendanceService.ChildGetAll<FlatSchedule>(reqFS);
            if (!response.Success)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorGettingSchedule")).Show();
                return;
            }
            this.dayId.Value = string.Empty;
            BuildSchedule(response.Items);

        

        }
      

        private void BuildSchedule(List<FlatSchedule> items)
        {
          
               string html = @"<div style = 'margin: 5px auto; width: 99%; height: 98%; overflow:auto;' > 
                             <table id = 'tbCalendar' cellpadding = '5' cellspacing = '0' >";

            //CAlling the branch cvailability before proceeding

            string startAt, closeAt = string.Empty;
            GetBranchSchedule(out startAt, out closeAt);
            if (string.IsNullOrEmpty(startAt) || string.IsNullOrEmpty(closeAt))
            {
                html += @"</table></div>";
                this.pnlSchedule.Html = html;
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorBranchWorkingHours")).Show();
                return;
            }

            TimeSpan tsStart = TimeSpan.Parse(startAt);
            //timeFrom.MinTime = tsStart;
            //timeTo.MinTime = tsStart.Add(TimeSpan.FromMinutes(30));
            TimeSpan tsClose = TimeSpan.Parse(closeAt);
            //timeTo.MaxTime = tsClose;



            //Filling The Times Slot
            List<TimeSlot> timesList = new List<TimeSlot>();
            DateTime dtStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, tsStart.Hours, tsStart.Minutes, 0);
            DateTime dtEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, tsClose.Hours, tsClose.Minutes, 0);
            do
            {
                TimeSlot ts = new TimeSlot();
                ts.ID = dtStart.ToString("HH:mm");
                ts.Time = dtStart.ToString("HH:mm");
                timesList.Add(ts);
                dtStart = dtStart.AddMinutes(30);
            } while (dtStart <= dtEnd);

            //filling the Day slots
            //int totalDays = Convert.ToInt32(Math.Ceiling((dateTo.SelectedDate - dateFrom.SelectedDate).TotalDays));

            html = FillFirstRow(html, timesList);

            html = FillOtherRow(html, timesList, items);
       

            //Preparing the ids to get colorified
            List<string> listIds = new List<string>();
            foreach (FlatSchedule fs in items)
            {
                DateTime activeDate = DateTime.ParseExact(fs.dayId, "yyyyMMdd", new CultureInfo("en"));
                DateTime fsfromDate = new DateTime(activeDate.Year, activeDate.Month, activeDate.Day, Convert.ToInt32(fs.from.Split(':')[0]), Convert.ToInt32(fs.from.Split(':')[1]), 0);
                DateTime fsToDate = new DateTime(activeDate.Year, activeDate.Month, activeDate.Day, Convert.ToInt32(fs.to.Split(':')[0]), Convert.ToInt32(fs.to.Split(':')[1]), 0);

                do
                {
                    listIds.Add(fsfromDate.ToString("yyyyMMdd") + "_" + fsfromDate.ToString("HH:mm"));
                    fsfromDate = fsfromDate.AddMinutes(30);
                } while (fsToDate >= fsfromDate);

            }

            var d = items.GroupBy(x => x.dayId);
            List<string> totaldayId = new List<string>();
            List<string> totaldaySum = new List<string>();
            d.ToList().ForEach(x =>
            {
                totaldayId.Add(x.ToList()[0].dayId + "_Total");
                totaldaySum.Add(x.ToList().Sum(y => Convert.ToDouble(y.duration) / 60).ToString());
            });




            html += @"</table></div>";
            this.pnlSchedule.Html = html;
            X.Call("ColorifySchedule", JSON.JavaScriptSerialize(listIds));
            X.Call("filldaytotal", totaldayId, totaldaySum);
            X.Call("Init");
            X.Call("DisableTools");
        }


       

        private void GetBranchSchedule(out string startAt, out string closeAt)
        {
            DateTime DF, DT;
            DF = new DateTime(dateFrom.SelectedDate.Year, dateFrom.SelectedDate.Month, 1);
            DT= DF.AddMonths(1).AddDays(-1);

            BranchWorkRecordRequest reqBS = new BranchWorkRecordRequest();
            reqBS.BranchId = branchId.Value.ToString();
            //reqBS.FromDayId =dateFrom.SelectedDate.ToString("yyyyMMdd");
            //reqBS.ToDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            reqBS.FromDayId = DF.ToString("yyyyMMdd");
            reqBS.ToDayId = DT.ToString("yyyyMMdd");

            ListResponse<BranchSchedule> response = _helpFunctionService.ChildGetAll<BranchSchedule>(reqBS);
            if (response.Success)
            {
                startAt = response.Items[0].openAt;
                closeAt = response.Items[0].closeAt;
            }
            else
            {
                startAt = string.Empty;
                closeAt = string.Empty;
            }
        }

        private string FillOtherRow(string html, List<TimeSlot> timesList,  List<FlatSchedule> items)
        {

          var d=  items.GroupBy(x => x.departmentId);
            foreach (var c in d)
            {
                for (int count = 0; count < c.ToList().GroupBy(x => x.employeeId).Count(); count++)
                {
                    if (count != 0 && items[count].employeeId == items[count - 1].employeeId)
                    {
                        continue;

                    }
                    html += "<tr>";
                    html += "<td id=" + items[count] + " class='Employee'>" + items[count].employeeName.firstName + "</td><td id=" + items[count].employeeName.firstName + "_Total></td>";
                    //if (!_systemService.SessionHelper.CheckIfArabicSession())
                    //{

                    //    html += "<td id=" + items[count] + " class='Employee'>" + items[count].employeeName.fullName + "</td><td id=" + firstDate.ToString("yyyyMMdd") + "_Total></td>";
                    //    // html += "<td id=" + firstDate.ToString("yyyyMMdd") + " class='day'>" + firstDate.ToString("ddd, MMM d") + "</td>";
                    //}
                    //else
                    //{
                    //    string day = firstDate.ToString("ddd");
                    //    string dayNumber = firstDate.ToString("dd");
                    //    string month = firstDate.ToString("MM");
                    //    html += "<td id=" + firstDate.ToString("yyyyMMdd") + " class='day'>" + string.Format("<div style='width:43px;display:inline-block'>{0}</div> {1} - {2}", (string)GetLocalResourceObject(day), dayNumber, month) + "</td><td id=" + firstDate.ToString("yyyyMMdd") + "_Total></td>";
                    //}

                    for (int index = 0; index < timesList.Count; index++)
                    {
                        html += "<td id=" + items[count].employeeName.firstName + "_" + timesList[index].ID + "></td>";
                    }
                    html += "</tr>";

                }
            }
            return html;
        }

        private string FillFirstRow(string html, List<TimeSlot> timesList)
        {

            html += "<tr><th style='width:95px;'></th><th>" + GetLocalResourceObject("total") + "</th>";
            for (int index = 0; index < timesList.Count; index++)
            {
                html += "<th>" + timesList[index].Time + "</th>";
            }
            html += "</tr>";
            return html;
        }

     

        private void SetExtLanguage()
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            if (rtl)
            {
                this.ResourceManager1.RTL = true;
                this.Viewport1.RTL = true;
                isRTL.Text = "1";

            }
        }


        protected void Prev_Click(object sender, DirectEventArgs e)
        {
            int index = int.Parse(e.ExtraParams["index"]);

            if ((index - 1) >= 0)
            {
                this.Viewport1.ActiveIndex = index - 1;
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


        [DirectMethod(ShowMask = true, CustomTarget = "employeeScheduleWindow")]
      

        protected void LoadEmployeeSchedule(object sender, DirectEventArgs e)
        {
            string employeeID = e.ExtraParams["employeeID"];
            string Name = e.ExtraParams["employeeName"];
            //getting the employee to push it to the Combo

            Employee emp = new Employee();
            emp.recordId = employeeID;
            emp.fullName = Name;



            BranchScheduleRecordRequest reqFS = new BranchScheduleRecordRequest();
            reqFS.EmployeeId = Convert.ToInt32(employeeID);
            reqFS.FromDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            reqFS.ToDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            reqFS.BranchId = 0;
            ListResponse<FlatSchedule> response = _timeAttendanceService.ChildGetAll<FlatSchedule>(reqFS);
            if (!response.Success)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorGettingSchedule")).Show();

                return;
            }
            this.dayId.Value = string.Empty;
            BuildSchedule(response.Items);





         
                                                   // this.employeeId.SelectedItem.Value = emp.recordId;
                                                   //   this.employeeId.Update();
            this.employeeScheduleWindow.Hide();
        }
     

        //[DirectMethod]
        //public void GetFilteredUsers()
        //{
        //    EmployeeListRequest req = new EmployeeListRequest();
        //    if (branchId.Value == null || branchId.Value.ToString() == string.Empty)
        //    {
        //        req.BranchId = "0";
        //    }
        //    else
        //        req.BranchId = branchId.Value.ToString();
        //    if (departmentId.Value == null || departmentId.Value.ToString() == string.Empty)
        //    {
        //        req.DepartmentId = "0";
        //    }
        //    else
        //        req.DepartmentId = departmentId.Value.ToString();


        //    req.IncludeIsInactive = 2;
        //    req.SortBy = "firstName";

        //    req.StartAt = "1";
        //    req.Size = "20";
        //    req.Filter = "";
        //    ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
        //    if (!response.Success)
        //        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", response.ErrorCode) != null ? GetGlobalResourceObject("Errors", response.ErrorCode).ToString() + "<br>Technical Error: " + response.ErrorCode + "<br> Summary: " + response.Summary : response.Summary).Show();

        //    else
        //        response.Items.ForEach(x => x.fullName = x.name.fullName);
        //    X.Call("AddSource", response.Items);

        //}
      

    }
}


    

  


