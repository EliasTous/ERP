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
using AionHR.Services.Messaging.System;
using AionHR.Services.Messaging.Reports;

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
                FillBranches();
                dateFrom.SelectedDate = DateTime.Now;


            }
            try
            {
                //AccessControlApplier.ApplyAccessControlOnPage(typeof(DailySchedule), null, null, null, null);
            }
            catch (AccessDeniedException exp)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied, "closeCurrentTab()").Show();
                Viewport1.Hidden = true;





                return;
            }



        }

        private void FillBranches()
        {
            ListRequest request = new ListRequest();
            request.Filter = string.Empty;
            ListResponse<Branch> branches = _branchService.ChildGetAll<Branch>(request);
            if (!branches.Success)
            {
                Common.errorMessage(branches);
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

            //BranchScheduleRecordRequest reqFS = new BranchScheduleRecordRequest();
            //reqFS.EmployeeId = 0;
            //reqFS.FromDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            //reqFS.ToDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            //reqFS.BranchId = Convert.ToInt32(branchId.SelectedItem.Value);
            string rep_params = "";
            Dictionary<string, string> parameters = new Dictionary<string, string>();




           
                parameters.Add("2", dateFrom.SelectedDate.ToString("yyyyMMdd"));
          
                parameters.Add("3", dateFrom.SelectedDate.ToString("yyyyMMdd"));
           
             
            parameters.Add("4", branchId.SelectedItem.Value);

            foreach (KeyValuePair<string, string> entry in parameters)
            {
                rep_params += entry.Key.ToString() + "|" + entry.Value + "^";
            }
            if (rep_params.Length > 0)
            {
                if (rep_params[rep_params.Length - 1] == '^')
                    rep_params = rep_params.Remove(rep_params.Length - 1);
            }



           
            ReportGenericRequest reqFS = new ReportGenericRequest();
            reqFS.paramString = rep_params;


            ListResponse<FlatSchedule> response = _timeAttendanceService.ChildGetAll<FlatSchedule>(reqFS);
            if (!response.Success)
            {
                X.Msg.Alert(Resources.Common.Error, (string)GetLocalResourceObject("ErrorGettingSchedule")).Show();
                return;
            }
            this.dayId.Value = string.Empty;
            BuildSchedule(response.Items);

        

        }

        public static string timeConverter(int _minutes, bool _signed)
        {
            if (_minutes == 0)
                return "00:00";

            bool isNegative = _minutes < 0 ? true : false;

            _minutes = Math.Abs(_minutes);

            string hours = (_minutes / 60).ToString(), minutes = (_minutes % 60).ToString(), formattedTime;

            if (hours.Length == 1)
                hours = "0" + hours;

            if (minutes.Length == 1)
                minutes = "0" + minutes;

            formattedTime = hours + ':' + minutes;

            if (isNegative && _signed)
                formattedTime = "-" + formattedTime;

            return formattedTime;
        }
        private void BuildSchedule(List<FlatSchedule> items)
        {
            string dailyScheduleVariation;
            SystemDefaultRecordRequest req = new SystemDefaultRecordRequest();
            req.Key = "dailySchedule";
            RecordResponse<KeyValuePair<string, string>> SystemDefaultResponse = _systemService.ChildGetRecord<KeyValuePair<string, string>>(req);
            if (!SystemDefaultResponse.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", SystemDefaultResponse.ErrorCode) != null ? GetGlobalResourceObject("Errors", SystemDefaultResponse.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + SystemDefaultResponse.LogId : SystemDefaultResponse.Summary).Show();
                return;
            }
            if (string.IsNullOrEmpty(SystemDefaultResponse.result.Value))
                dailyScheduleVariation = "60";
            else
                dailyScheduleVariation = SystemDefaultResponse.result.Value;



            string html = @"<div style = 'margin: 5px auto; width: 99%; height: 98%; overflow:auto;' > 
                             <table id = 'tbCalendar' cellpadding = '5' cellspacing = '0'  style='width:auto;'>";




            //string startAt, closeAt = string.Empty;
            //GetBranchSchedule(out startAt, out closeAt);
            string startAt = items.Count != 0 ? items.Min(x => x.dtFrom.TimeOfDay).ToString() : "00:00";
            string closeAt = items.Count != 0 ? items.Max(x => x.dtTo.TimeOfDay).ToString() : "00:00";

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

            TimeSpan EmployeeTsStart, EmployeeTsEnd;


            //items.ForEach(x =>
            //{
            //    string newFrom = "";
            //    string newTo = "";
            //    List<string> fromList = x.dtFrom.TimeOfDay.Split(':').ToList();
            //    if (fromList.Count==2)
            //    {
            //        newFrom = (Convert.ToInt32(fromList.First()) % 24).ToString();
            //        newFrom += ":" + fromList.ElementAt(1);
            //        EmployeeTsStart = TimeSpan.Parse(newFrom);
            //    }
            //    else
            //        EmployeeTsStart = TimeSpan.Parse(x.from);



            //    List<string> ToList = x.to.Split(':').ToList();
            //    if (ToList.Count == 2)
            //    {
            //        newTo = (Convert.ToInt32(ToList.First()) % 24).ToString();
            //        newTo += ":" + ToList.ElementAt(1);
            //        EmployeeTsEnd = TimeSpan.Parse(newTo);
            //    }
            //    else
            //        EmployeeTsEnd = TimeSpan.Parse(x.to); 

            //    if (EmployeeTsStart < tsStart || EmployeeTsEnd > tsClose)
            //    {
            //        html += @"</table></div>";
            //        this.pnlSchedule.Html = html;
            //        X.Call("DisableTools");
            //        X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", "ErrorEmployeeTimeOutside").ToString() + x.employeeName).Show();
            //        return;
            //    }
            //});

            //Filling The Times Slot
            List<TimeSlot> timesList = new List<TimeSlot>();
            DateTime dtStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, tsStart.Hours, tsStart.Minutes, 0);
            DateTime dtEnd;
            if (tsStart > tsClose)
            {
                DateTime endDate = DateTime.Now;
                endDate = endDate.AddDays(1);
                dtEnd = new DateTime(endDate.Year, endDate.Month, endDate.Day, tsClose.Hours, tsClose.Minutes, 0);
            }
            else
                dtEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, tsClose.Hours, tsClose.Minutes, 0);

            if (dtEnd == dtStart)
            {
                dtEnd = dtEnd.AddDays(1).AddMinutes(-Convert.ToInt32(dailyScheduleVariation));
            }
            int counter = 0;
            do
            {
                TimeSlot ts = new TimeSlot();
                ts.ID = dtStart.ToString("HH:mm");
                ts.Time = dtStart.ToString("HH:mm");
                timesList.Add(ts);
                dtStart = dtStart.AddMinutes(Convert.ToInt32(dailyScheduleVariation));
                counter++;
            } while (dtStart <= dtEnd && !string.IsNullOrEmpty(dailyScheduleVariation) && counter != 3000);

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
                DateTime activeDate = items[0].dtFrom;
                DateTime fsfromDate, fsToDate;

                foreach (FlatSchedule fs in items)
                {


                    DateTime from = fs.dtFrom;
                    DateTime to = fs.dtTo;
                    counter = 0;
                    while (to > from && counter != 3000)
                    {

                        if (from.ToString("HH:mm") == "00:00")
                        {
                            listIds.Add(fs.employeeId + "_" + from.AddDays(-1).ToString("HH:mm"));
                            listDn.Add(fs.employeeId + "_" + fs.departmentId + "_" + from.AddDays(-1).ToString("HH:mm"));
                            listDS.Add(fs.departmentId.ToString());
                            from = from.AddMinutes(Convert.ToInt32(SystemDefaultResponse.result.Value));
                            counter++;
                            continue;

                        }
                        listIds.Add(fs.employeeId + "_" + from.ToString("HH:mm"));
                        listDn.Add(fs.employeeId + "_" + fs.departmentId + "_" + from.ToString("HH:mm"));
                        listDS.Add(fs.departmentId.ToString());

                        from = from.AddMinutes(Convert.ToInt32(SystemDefaultResponse.result.Value));
                        counter++;

                    }
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
                counter = 0;
                do
                {
                    listDS.ForEach(departmentID =>
                    {
                        if (!dic.ContainsKey(departmentID + "-" + fsfromDate.ToString("HH:mm")))
                            dic.Add(departmentID + "-" + fsfromDate.ToString("HH:mm"), listDn.Where(stringToCheck => stringToCheck.Contains(departmentID + "_" + fsfromDate.ToString("HH:mm"))).Count());
                    });
                    fsfromDate = fsfromDate.AddMinutes(Convert.ToInt32(SystemDefaultResponse.result.Value));

                    counter++;
                } while (fsToDate >= fsfromDate && counter!=3000);


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
                totaldaySum.Add(timeConverter( x.ToList().Sum(y =>  Convert.ToInt32(y.duration)),true));
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
                    html += "<td id=" +e.ToList()[0].employeeId + " class='Employee'>" + e.ToList()[0].employeeName + "</td><td id=" + e.ToList()[0].employeeId + "_Total></td>";
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
            string rep_params = "";
            Dictionary<string, string> parameters = new Dictionary<string, string>();




            
                parameters.Add("1", dateFrom.SelectedDate.ToString("yyyyMMdd"));
         
                parameters.Add("2", dateFrom.SelectedDate.ToString("yyyyMMdd"));
                if (!string.IsNullOrEmpty(employeeID))
                parameters.Add("3", employeeID);
                parameters.Add("4", "0");

            foreach (KeyValuePair<string, string> entry in parameters)
            {
                rep_params += entry.Key.ToString() + "|" + entry.Value + "^";
            }
            if (rep_params.Length > 0)
            {
                if (rep_params[rep_params.Length - 1] == '^')
                    rep_params = rep_params.Remove(rep_params.Length - 1);
            }



           
            ReportGenericRequest reqFS = new ReportGenericRequest();
            reqFS.paramString = rep_params;



            //BranchScheduleRecordRequest reqFS = new BranchScheduleRecordRequest();
            //reqFS.EmployeeId = Convert.ToInt32(employeeID);
            //reqFS.FromDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            //reqFS.ToDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            //reqFS.BranchId = 0;
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
            SystemDefaultRecordRequest req = new SystemDefaultRecordRequest();
            req.Key = "dailySchedule";
            RecordResponse<KeyValuePair<string, string>> SystemDefaultResponse = _systemService.ChildGetRecord<KeyValuePair<string, string>>(req);
            if (!SystemDefaultResponse.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", SystemDefaultResponse.ErrorCode) != null ? GetGlobalResourceObject("Errors", SystemDefaultResponse.ErrorCode).ToString() + "<br>" + GetGlobalResourceObject("Errors", "ErrorLogId") + SystemDefaultResponse.LogId : SystemDefaultResponse.Summary).Show();
                return;
            }
            pnlTools.Hidden = false;
            currentEmployee.Text = EmployeeId;
        



            string rep_params = "";
            Dictionary<string, string> parameters = new Dictionary<string, string>();




          
                parameters.Add("2", dateFrom.SelectedDate.ToString("yyyyMMdd"));
          
                parameters.Add("3", dateFrom.SelectedDate.ToString("yyyyMMdd"));
         
                parameters.Add("1", EmployeeId);
               

            foreach (KeyValuePair<string, string> entry in parameters)
            {
                rep_params += entry.Key.ToString() + "|" + entry.Value + "^";
            }
            if (rep_params.Length > 0)
            {
                if (rep_params[rep_params.Length - 1] == '^')
                    rep_params = rep_params.Remove(rep_params.Length - 1);
            }



         
            ReportGenericRequest reqFS = new ReportGenericRequest();
            reqFS.paramString = rep_params;

            //BranchScheduleRecordRequest reqFS = new BranchScheduleRecordRequest();
            //reqFS.EmployeeId = Convert.ToInt32(EmployeeId);
            //reqFS.FromDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            //reqFS.ToDayId = dateFrom.SelectedDate.ToString("yyyyMMdd");
            //reqFS.BranchId = 0;

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

            //  GetBranchSchedule(out startAt, out closeAt);

            string startAt = response.Items.Count != 0 ? response.Items.Min(x => x.dtFrom.TimeOfDay).ToString() : "00:00";
            string closeAt = response.Items.Count != 0 ? response.Items.Max(x => x.dtTo.TimeOfDay).ToString() : "00:00";
            TimeSpan tsStart = TimeSpan.Parse(startAt);
            //timeFrom.MinTime = tsStart;
            //timeTo.MinTime = tsStart.Add(TimeSpan.FromMinutes(Convert.ToInt32(SystemDefaultResponse.result.Value)));
            timeFrom.SelectedTime = tsStart;
            TimeSpan tsClose = TimeSpan.Parse(closeAt);
           //timeTo.MaxTime = tsClose;
         


            //Filling The Times Slot
            List<TimeSlot> timesList = new List<TimeSlot>();
            DateTime dtStart, dtEnd;
            dtStart = new DateTime(dateFrom.SelectedDate.Year, dateFrom.SelectedDate.Month, 1, tsStart.Hours, tsStart.Minutes, 0);
            dtEnd = new DateTime(dateFrom.SelectedDate.Year, dateFrom.SelectedDate.Month, 1, tsClose.Hours, tsClose.Minutes, 0);

            if (dtStart >= dtEnd)
            {
                dtEnd = dtEnd.AddDays(1);
            }

            int counter = 0;
            do
            {
                TimeSlot ts = new TimeSlot();
                ts.ID = dtStart.ToString("HH:mm");
                ts.Time = dtStart.ToString("HH:mm");
                timesList.Add(ts);
               
                dtStart = dtStart.AddMinutes(Convert.ToInt32(SystemDefaultResponse.result.Value));
                counter++;
            } while (dtStart <= dtEnd && counter!=3000);
            
            html = FillFirstRow(html, timesList);

            html = FillOtherRow(html, timesList, response.Items);

            html += @"</table></div>";
            this.pnlSchedule.Html = html;
            List<string> listIds = new List<string>();
            foreach (FlatSchedule fs in response.Items)
            {
                DateTime activeDate = fs.dtFrom;
                DateTime fsfromDate = fs.dtFrom;
                DateTime fsToDate = fs.dtTo;

               counter = 0;
                do
                {
                    listIds.Add(EmployeeId + "_" + fsfromDate.ToString("HH:mm"));
                    fsfromDate = fsfromDate.AddMinutes(Convert.ToInt32(SystemDefaultResponse.result.Value));
                    counter++;
                } while (fsToDate > fsfromDate &&counter!=3000);
            }
            var d = response.Items.GroupBy(x => x.employeeId);
            List<string> totaldayId = new List<string>();
            List<string> totaldaySum = new List<string>();
            d.ToList().ForEach(x =>
            {
                totaldayId.Add(x.ToList()[0].employeeId + "_Total");
                totaldaySum.Add(timeConverter( x.ToList().Sum(y => Convert.ToInt32(y.duration)),true));
            });



            X.Call("ColorifySchedule", JSON.JavaScriptSerialize(listIds));

            X.Call("filldaytotal", totaldayId, totaldaySum);
        }

        protected void Save_Click(object sender, DirectEventArgs e)
        {
            List<FlatSchedule> currentFlat;
            string rep_params = "";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("1", currentEmployee.Text);
            parameters.Add("2", dateFrom.SelectedDate.ToString("yyyyMMdd") );
            parameters.Add("3", dateFrom.SelectedDate.ToString("yyyyMMdd"));
            foreach (KeyValuePair<string, string> entry in parameters)
            {
                rep_params += entry.Key.ToString() + "|" + entry.Value + "^";
            }
            if (rep_params.Length > 0)
            {
                if (rep_params[rep_params.Length - 1] == '^')
                    rep_params = rep_params.Remove(rep_params.Length - 1);
            }







            ReportGenericRequest reqSaveFS = new ReportGenericRequest();
            reqSaveFS.paramString = rep_params;
            //

            ListResponse<FlatSchedule> respSaveFS = _timeAttendanceService.ChildGetAll<FlatSchedule>(reqSaveFS);
            if (!respSaveFS.Success)
            {
                Common.errorMessage(respSaveFS);
                return;
            }

            currentFlat = respSaveFS.Items;


            List<FlatSchedule> listToDelete = new List<FlatSchedule>();
            DateTime fromday = dateFrom.SelectedDate; 
            DateTime today = dateFrom.SelectedDate;
            DateTimeRange fixRange = new DateTimeRange();
            DateTimeRange compareRange = new DateTimeRange();
            List<FlatSchedule> temp = new List<FlatSchedule>();
         int    counter = 0;
            do
            {
                temp = currentFlat.Where(x => x.dtFrom.Date == fromday.Date).ToList();

                foreach (FlatSchedule f in temp)
                {
                    fixRange.Start = f.dtFrom;
                    fixRange.End = f.dtTo;
                    compareRange.Start = f.dtFrom.Date + new TimeSpan(timeFrom.SelectedTime.Hours, timeFrom.SelectedTime.Minutes, 0);
                    compareRange.End = f.dtFrom.Date + new TimeSpan(timeTo.SelectedTime.Hours, timeTo.SelectedTime.Minutes, 0);
                    if (compareRange.Intersects(fixRange))
                        listToDelete.Add(f);
                }

                fromday = fromday.AddDays(1);
                counter++;
            } while (fromday <= today && counter != 3000);


            PostRequest<FlatSchedule> deleteRequest = new PostRequest<FlatSchedule>();
            listToDelete.ForEach(x =>
            {
                deleteRequest.entity = x;
                PostResponse<FlatSchedule> deleteResp = _timeAttendanceService.ChildDelete<FlatSchedule>(deleteRequest);
                if (!deleteResp.Success)
                    Common.errorMessage(deleteResp);

            });


            FlatSchedule fs = new FlatSchedule();
            fs.dtFrom = new DateTime(dateFrom.SelectedDate.Year, dateFrom.SelectedDate.Month, dateFrom.SelectedDate.Day, timeFrom.SelectedTime.Hours, timeFrom.SelectedTime.Minutes, 0);
            fs.dtTo = new DateTime(dateFrom.SelectedDate.Year, dateFrom.SelectedDate.Month, dateFrom.SelectedDate.Day, timeTo.SelectedTime.Hours, timeTo.SelectedTime.Minutes, 0);
            fs.employeeId = Convert.ToInt32(currentEmployee.Text);


            PostRequest<FlatSchedule> request = new PostRequest<FlatSchedule>();

            request.entity = fs;
            PostResponse<FlatSchedule> r = _timeAttendanceService.ChildAddOrUpdate<FlatSchedule>(request);


            //check if the insert failed
            if (!r.Success)//it maybe be another condition
            {
                //Show an error saving...
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
              Common.errorMessage(r);
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
              Common.errorMessage(r);
                return;
            }

            else
            {
                Load_Click(new object(), new DirectEventArgs(null));
            }
        }
    


    }
}


    

  


