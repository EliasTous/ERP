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
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", branches.ErrorCode) != null ? GetGlobalResourceObject("Errors", branches.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + branches.LogId : branches.Summary).Show();
                return;
            }
            this.branchStore.DataSource = branches.Items;
            this.branchStore.DataBind();
        }

              
        protected void Load_Click(object sender, DirectEventArgs e)
        {
            pnlTools.Hidden = true;
            if (branchId.Value == null || branchId.Value.ToString() == string.Empty)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("SelectBranch")).Show();
                return;
            }

            //Proceed to load

            BranchScheduleRecordRequest reqFS = new BranchScheduleRecordRequest();
            reqFS.EmployeeId = 0;
            reqFS.FromDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            reqFS.ToDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            reqFS.BranchId = Convert.ToInt32(branchId.SelectedItem.Value);
         
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
                             <table id = 'tbCalendar' cellpadding = '5' cellspacing = '0'  style='width:auto;'>";


            //CAlling the branch cvailability before proceeding

            string startAt, closeAt = string.Empty;
            GetBranchSchedule(out startAt, out closeAt);
            if(startAt=="00:00"&& closeAt=="00:00")
              
                {
                    X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("branchClosed")).Show();
                    return;
                }

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
            DateTime dtStart, dtEnd;
            dtStart = new DateTime(dateFrom.SelectedDate.Year, dateFrom.SelectedDate.Month,1, tsStart.Hours, tsStart.Minutes, 0);
            dtEnd = new DateTime(dateFrom.SelectedDate.Year, dateFrom.SelectedDate.Month,1, tsClose.Hours, tsClose.Minutes, 0);
            if (dtStart >= dtEnd)
            {
                dtEnd = dtEnd.AddDays(1);
            }



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
            List<string> listDn = new List<string>();
            List<string> listDS = new List<string>();
          
            Dictionary<string, int> dic = new Dictionary<string, int>();
            if (items.Count != 0 && items != null)
            {
                DateTime activeDate = DateTime.ParseExact(items[0].dayId, "yyyyMMdd", new CultureInfo("en"));
                DateTime fsfromDate, fsToDate;


                foreach (FlatSchedule fs in items)
                {

                    fsfromDate = new DateTime(activeDate.Year, activeDate.Month, activeDate.Day, Convert.ToInt32(fs.from.Split(':')[0]), Convert.ToInt32(fs.from.Split(':')[1]), 0);
                    fsToDate = new DateTime(activeDate.Year, activeDate.Month, activeDate.Day, Convert.ToInt32(fs.to.Split(':')[0]), Convert.ToInt32(fs.to.Split(':')[1]), 0);
                    if (fsfromDate >= fsToDate)
                    {
                        fsToDate = fsToDate.AddDays(1);
                    }

                    do
                    {
                        if (fsfromDate.ToString("HH:mm") == "00:00")
                        {
                            listIds.Add(fs.employeeId + "_" + fsfromDate.AddDays(-1).ToString("HH:mm"));
                            listDn.Add(fs.employeeId + "_" + fs.departmentId + "_" + fsfromDate.AddDays(-1).ToString("HH:mm"));
                            listDS.Add(fs.departmentId.ToString());
                            fsfromDate = fsfromDate.AddMinutes(30);
                            continue;

                        }
                        listIds.Add(fs.employeeId + "_" + fsfromDate.ToString("HH:mm"));
                        listDn.Add(fs.employeeId + "_" + fs.departmentId + "_" + fsfromDate.ToString("HH:mm"));
                        listDS.Add(fs.departmentId.ToString());

                        //if (!dic.ContainsKey(fs.departmentId + "-" + fsfromDate.ToString("HH:mm")))
                        //    dic.Add(fs.departmentId + "-" + fsfromDate.ToString("HH:mm"), 1);
                        //else
                        //    dic[fs.departmentId + "-" + fsfromDate.ToString("HH:mm")]++;
                        fsfromDate = fsfromDate.AddMinutes(30);
                    } while (fsToDate >= fsfromDate);
                }
                listDn = listDn.Distinct().ToList();
                listDS = listDS.Distinct().ToList();
                //listDS.ForEach(departmentID => dic.Add(departmentID+, listDn.Where(stringToCheck => stringToCheck.Contains(departmentID)).Count()));


                fsfromDate = new DateTime(activeDate.Year, activeDate.Month, activeDate.Day, Convert.ToInt32(startAt.Split(':')[0]), Convert.ToInt32(startAt.Split(':')[1]), 0);
                fsToDate = new DateTime(activeDate.Year, activeDate.Month, activeDate.Day, Convert.ToInt32(closeAt.Split(':')[0]), Convert.ToInt32(closeAt.Split(':')[1]), 0);
                if (fsfromDate >= fsToDate)
                {
                    fsToDate = fsToDate.AddDays(1);
                }
                do
                {
                    listDS.ForEach(departmentID => dic.Add(departmentID + "-" + fsfromDate.ToString("HH:mm"), listDn.Where(stringToCheck => stringToCheck.Contains(departmentID + "_" + fsfromDate.ToString("HH:mm"))).Count()));
                    fsfromDate = fsfromDate.AddMinutes(30);
                } while (fsToDate >= fsfromDate);


            }


            //var department= items.GroupBy(x => x.departmentId);
            //foreach (var de in department)
            //{

            //    fsfromDate = new DateTime(activeDate.Year, activeDate.Month, activeDate.Day, Convert.ToInt32(startAt.Split(':')[0]), Convert.ToInt32(startAt.Split(':')[1]), 0);
            //    fsToDate = new DateTime(activeDate.Year, activeDate.Month, activeDate.Day, Convert.ToInt32(closeAt.Split(':')[0]), Convert.ToInt32(closeAt.Split(':')[1]), 0);
            //    List<FlatSchedule> employee = de.ToList();


            //    do
            //    {
            //        listDn.Add(employee[0].departmentId + "-" + fsfromDate.ToString("HH:mm"));
            //        int sum = 0;
            //        employee.ForEach(x =>
            //        {
            //          int employeeFromHour=  Convert.ToInt32(x.from.Split(':')[0]);
            //          int employeeToHour = Convert.ToInt32(x.to.Split(':')[0]);

            //            int employeeFromMintues= Convert.ToInt32(x.from.Split(':')[1]);
            //            int employeeTMintues = Convert.ToInt32(x.to.Split(':')[1]);
            //            double employeeFromTotal = employeeFromHour * 100 + employeeFromMintues;
            //            double employeeToTotal = employeeToHour * 100 + employeeTMintues;
            //            double FsTotal = fsfromDate.Hour * 100 + fsfromDate.Minute;
            //            if (FsTotal >= employeeFromTotal && FsTotal <= employeeToTotal)
            //                sum++;

            //        }
            //        );
            //        listDS.Add(sum);
            //        fsfromDate = fsfromDate.AddMinutes(30);
            //    } while (fsToDate >= fsfromDate);


            //}



            var d = items.GroupBy(x => x.employeeId);
            List<string> totaldayId = new List<string>();
            List<string> totaldaySum = new List<string>();
            d.ToList().ForEach(x =>
            {
                totaldayId.Add(x.ToList()[0].employeeId + "_Total");
                totaldaySum.Add(x.ToList().Sum(y => Math.Round( Convert.ToDouble(y.duration) / 60)).ToString());
            });
            //List<string> employeeList = new List<string>();
            //items.ForEach(x => employeeList.Add(x.employeeId.ToString()));
           
            html = FillSummaryRow(html, timesList, dic);

            html += @"</table></div>";
            this.pnlSchedule.Html = html;
            X.Call("ColorifySchedule", JSON.JavaScriptSerialize(listIds));
           
            X.Call("filldaytotal", totaldayId, totaldaySum);
            //X.Call("filldepartmentTotal", listDn, listDS);
            X.Call("filldepartmentTotal", dic);
            X.Call("employeeClick", items);
            //X.Call("Init");
            //X.Call("DisableTools");
            X.Call("FixHeader");
        }


       

        private void GetBranchSchedule(out string startAt, out string closeAt)
        {
            //DateTime DF, DT;
            //DF = new DateTime(dateFrom.SelectedDate.Year, dateFrom.SelectedDate.Month, 1);
            //DT= DF.AddMonths(1).AddDays(-1);

            BranchWorkRecordRequest reqBS = new BranchWorkRecordRequest();
            reqBS.BranchId = branchId.Value.ToString();
            reqBS.FromDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            reqBS.ToDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            //reqBS.FromDayId = DF.ToString("yyyyMMdd");
            //reqBS.ToDayId = DT.ToString("yyyyMMdd");

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

          var department=  items.GroupBy(x => x.departmentId);
            foreach (var d in department)
            {
                var employee = d.ToList().GroupBy(x => x.employeeId);
                //int count = 0;
                foreach (var e in employee)
               
                {
                    //if (count > 1)
                    //    break;
                    
                    html += "<tr>";
                    html += "<td id=" +e.ToList()[0].employeeId + " class='Employee'>" + e.ToList()[0].employeeName.fullName + "</td><td id=" + e.ToList()[0].employeeId + "_Total></td>";
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
                    //count++;
                    for (int index = 0; index < timesList.Count; index++)
                     {
                        html += "<td id=" + e.ToList()[0].employeeId + "_" + timesList[index].ID + "></td>";
                    }
                    html += "</tr>";

                }
                html += "<td  id=" + d.ToList()[0].departmentId +"_"+ d.ToList()[0].departmentName + " class='department'>" + d.ToList()[0].departmentName + "</td><td  class='department' id=" + d.ToList()[0].departmentId + "-Total>"+ employee.ToList().Count()+"</td>";
                for (int index = 0; index < timesList.Count; index++)
                {
                    html += "<td class='department' id=" + d.ToList()[0].departmentId + "-" + timesList[index].ID + " ></td>";
                }
                html += "</tr>";
            }
            return html;
        }

        private string FillFirstRow(string html, List<TimeSlot> timesList)
        {
          

            html += "<thead><tr><th style='width:120px;'></th><th>" + GetLocalResourceObject("total") + "</th>";
            for (int index = 0; index < timesList.Count; index++)
            {
                html += "<th>" + timesList[index].Time + "</th>";
            }
            html += "</tr></thead>";
            return html;
        }
        private string FillSummaryRow(string html, List<TimeSlot> timesList,Dictionary<string,int> dic )
        {

            html += "<tfoot><tr class='department'><td style='width:95px;'></td><td>" + GetLocalResourceObject("total") + "</td>";
            for (int index = 0; index < timesList.Count; index++)
            {
              
                html += "<td>" + dic.Where(kvp => kvp.Key.Contains(timesList[index].Time)).Sum(kvp => kvp.Value) + "</td>";
            }
            html += "</tr></tfoot>";
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

        [DirectMethod]
        public void OpenCell(string EmployeeId)
        {
            pnlTools.Hidden = false;
            currentEmployee.Text = EmployeeId;
            string startAt=string.Empty, closeAt=string.Empty;
            BranchScheduleRecordRequest reqFS = new BranchScheduleRecordRequest();
            reqFS.EmployeeId = Convert.ToInt32(EmployeeId);
            reqFS.FromDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            reqFS.ToDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            reqFS.BranchId = 0;

            ListResponse<FlatSchedule> response = _timeAttendanceService.ChildGetAll<FlatSchedule>(reqFS);
            if (!response.Success)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorGettingSchedule")).Show();
                return;
            }
         
            this.pnlSchedule.Html = string.Empty;
            string html = @"<div style = 'margin: 5px auto; width: 99%; height: 98%; overflow:auto;' > 
                             <table id = 'tbCalendar' cellpadding = '5' cellspacing = '0' >";
            if (response.Items.Count == 0)
                Load_Click(new object(), new DirectEventArgs(null));
            else                
            GetBranchSchedule(out startAt, out closeAt);
           

            TimeSpan tsStart = TimeSpan.Parse(startAt);
            timeFrom.MinTime = tsStart;
            timeTo.MinTime = tsStart.Add(TimeSpan.FromMinutes(30));
            timeFrom.SelectedTime = tsStart;
            TimeSpan tsClose = TimeSpan.Parse(closeAt);
            timeTo.MaxTime = tsClose;
         


            //Filling The Times Slot
            List<TimeSlot> timesList = new List<TimeSlot>();
            DateTime dtStart, dtEnd;
            dtStart = new DateTime(dateFrom.SelectedDate.Year, dateFrom.SelectedDate.Month, 1, tsStart.Hours, tsStart.Minutes, 0);
            dtEnd = new DateTime(dateFrom.SelectedDate.Year, dateFrom.SelectedDate.Month, 1, tsClose.Hours, tsClose.Minutes, 0);

            if (dtStart >= dtEnd)
            {
                dtEnd = dtEnd.AddDays(1);
            }


            do
            {
                TimeSlot ts = new TimeSlot();
                ts.ID = dtStart.ToString("HH:mm");
                ts.Time = dtStart.ToString("HH:mm");
                timesList.Add(ts);
               
                dtStart = dtStart.AddMinutes(30);
            } while (dtStart <= dtEnd);
            
            html = FillFirstRow(html, timesList);

            html = FillOtherRow(html, timesList, response.Items);

            html += @"</table></div>";
            this.pnlSchedule.Html = html;
            List<string> listIds = new List<string>();
            foreach (FlatSchedule fs in response.Items)
            {
                DateTime activeDate = DateTime.ParseExact(fs.dayId, "yyyyMMdd", new CultureInfo("en"));
                DateTime fsfromDate = new DateTime(activeDate.Year, activeDate.Month, activeDate.Day, Convert.ToInt32(fs.from.Split(':')[0]), Convert.ToInt32(fs.from.Split(':')[1]), 0);
                DateTime fsToDate = new DateTime(activeDate.Year, activeDate.Month, activeDate.Day, Convert.ToInt32(fs.to.Split(':')[0]), Convert.ToInt32(fs.to.Split(':')[1]), 0);

                
                do
                {
                    listIds.Add(EmployeeId + "_" + fsfromDate.ToString("HH:mm"));
                    fsfromDate = fsfromDate.AddMinutes(30);
                } while (fsToDate >= fsfromDate);
            }
            var d = response.Items.GroupBy(x => x.employeeId);
            List<string> totaldayId = new List<string>();
            List<string> totaldaySum = new List<string>();
            d.ToList().ForEach(x =>
            {
                totaldayId.Add(x.ToList()[0].employeeId + "_Total");
                totaldaySum.Add(x.ToList().Sum(y => Convert.ToDouble(y.duration) / 60).ToString());
            });



            X.Call("ColorifySchedule", JSON.JavaScriptSerialize(listIds));

            X.Call("filldaytotal", totaldayId, totaldaySum);
        }

        protected void Save_Click(object sender, DirectEventArgs e)
        {
            FlatSchedule fs = new FlatSchedule();
            fs.from = timeFrom.SelectedTime.ToString().Substring(0,5);
            fs.to = timeTo.SelectedTime.ToString().Substring(0, 5);
            fs.employeeId = Convert.ToInt32(currentEmployee.Text);
            fs.dayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            PostRequest<FlatSchedule> request = new PostRequest<FlatSchedule>();

            request.entity = fs;
            PostResponse<FlatSchedule> r = _timeAttendanceService.ChildAddOrUpdate<FlatSchedule>(request);


            //check if the insert failed
            if (!r.Success)//it maybe be another condition
            {
                //Show an error saving...
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+GetGlobalResourceObject("Errors","ErrorLogId") + r.LogId : r.Summary).Show();
                return;
            }
            OpenCell(currentEmployee.Text);
        }
        protected void DeleteDay_Click(object sender, DirectEventArgs e)
        {
            X.Msg.Confirm(Resources.Common.Confirmation, (string)GetLocalResourceObject("ConfirmDeleteDay"), new MessageBoxButtonsConfig
            {
                Yes = new MessageBoxButtonConfig
                {
                    //We are call a direct request metho for deleting a record
                    Handler = String.Format("App.direct.DayDelete()"),
                    Text = Resources.Common.Yes
                },
                No = new MessageBoxButtonConfig
                {
                    Text = Resources.Common.No
                }

            }).Show();
        }
        [DirectMethod(ShowMask = true)]
        public void DayDelete()
        {
         


            FlatSchedule fs = new FlatSchedule();
            fs.employeeId = Convert.ToInt32(currentEmployee.Text);
            fs.dayId = dateFrom.SelectedDate.ToString("yyyyMMdd"); 
            fs.shiftId = 0;
            PostRequest<FlatSchedule> request = new PostRequest<FlatSchedule>();

            request.entity = fs;
            PostResponse<FlatSchedule> r = _timeAttendanceService.ChildDelete<FlatSchedule>(request);


            //check if the insert failed
            if (!r.Success)//it maybe be another condition
            {
                //Show an error saving...
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", r.ErrorCode) != null ? GetGlobalResourceObject("Errors", r.ErrorCode).ToString() + "<br>"+GetGlobalResourceObject("Errors","ErrorLogId") + r.LogId : r.Summary).Show();
                return;
            }

            else
            {
                Load_Click(new object(), new DirectEventArgs(null));
            }
        }
    


    }
}


    

  


