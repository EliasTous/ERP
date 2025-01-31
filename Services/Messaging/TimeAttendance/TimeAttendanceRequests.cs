﻿using System;
using System.Collections.Generic;
using Services.Messaging;

public class DayBreaksListRequest : ListRequest
{
    public string ScheduleId { get; set; }

    public string DayOfWeek { get; set; }
    private Dictionary<string, string> parameters;

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_scId", ScheduleId);
            parameters.Add("_dow", DayOfWeek);

            return parameters;
        }
    }
}

public class OvertimeSettingsListRequest : ListRequest
{
    public string DayId { get; set; }

    public int EmployeeId { get; set; }
    private Dictionary<string, string> parameters;

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_dayId", DayId);
            parameters.Add("_employeeId", EmployeeId.ToString());

            return parameters;
        }
    }
}

public class BranchScheduleRecordRequest : ListRequest
{
    public DateTime FromDayId { get; set; }

    public DateTime ToDayId { get; set; }

    public int EmployeeId { get; set; }
    public int BranchId { get; set; }
    
    private Dictionary<string, string> parameters;

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_fromDayId", FromDayId.ToString());
            parameters.Add("_toDayId", ToDayId.ToString());
            parameters.Add("_employeeId", EmployeeId.ToString());
            parameters.Add("_branchId", BranchId.ToString());

            return parameters;
        }
    }
}

public class FlatScheduleWorkingHoursRequest : ListRequest
{
    public DateTime startDate { get; set; }

    public DateTime endDate { get; set; }

    public int EmployeeId { get; set; }
   
    private Dictionary<string, string> parameters;

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_startDate", startDate.ToString());
            parameters.Add("_endDate", endDate.ToString());
            parameters.Add("_employeeId", EmployeeId.ToString());
     

            return parameters;
        }
    }
}
public class FlatScheduleImportEmployeeRequest 
{
    public int _fromEmployeeId { get; set; }
    public int _toEmployeeId { get; set; }
    public string _fromDayId { get; set; }
    public string _toDayId { get; set; }
  


}

public class BranchAvailabilityScheduleRecordRequest : ListRequest
{
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int BranchId { get; set; }

    public int departmentId { get; set;}
    private Dictionary<string, string> parameters;

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            if (StartDate==null)
                parameters.Add("_startDate", DateTime.Now.ToString());
            else
            parameters.Add("_startDate", StartDate.ToString());
            if (EndDate == null)
                parameters.Add("_endDate", DateTime.Now.ToString());
            else
                parameters.Add("_endDate", EndDate.ToString());
            parameters.Add("_branchId", BranchId.ToString());
            parameters.Add("_departmentId", departmentId.ToString());

            return parameters;
        }
    }
}


public class EmployeeCellScheduleRequest : ListRequest
{
    public string Time { get; set; }

    public string DayId { get; set; }

    public int BranchId { get; set; }

    public int departmentId { get; set; }
    private Dictionary<string, string> parameters;

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_time", Time);
            parameters.Add("_DayId", DayId);
            parameters.Add("_branchId", BranchId.ToString());
            parameters.Add("_departmentId", departmentId.ToString());

            return parameters;
        }
    }
}





public class AttendanceScheduleRecordRequest : RecordRequest
{
    public string ScheduleId { get; set; }
    public Dictionary<string, string> parameters;
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_scId", ScheduleId);
            return parameters;
        }
    }
}
public class AttendanceScheduleDayRecordRequest : RecordRequest
{
    public string ScheduleId { get; set; }
    public string DayOfWeek { get; set; }
    public Dictionary<string, string> parameters;
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_scId", ScheduleId);
            parameters.Add("_dow", DayOfWeek);
            return parameters;
        }
    }
}

public class AttendanceScheduleDayListRequest : ListRequest
{
    public string ScheduleId { get; set; }
    public Dictionary<string, string> parameters;
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_scId", ScheduleId);

            return parameters;
        }
    }
}
public class AttendanceDayBreaksListRequest : ListRequest
{
    public string ScheduleId { get; set; }
    public string DayOfWeek { get; set; }
    public Dictionary<string, string> parameters;
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_scId", ScheduleId);
            parameters.Add("_dow", DayOfWeek);
            return parameters;
        }
    }
}
public class DeleteDayBreaksRequest : RequestBase

{
    public string ScheduleId
    { get; set; }
    public string Dow
    { get; set; }
    public Dictionary<string, string> parameters;

    /// <summary>
    /// parameter list shipped with the web request
    /// </summary>
    public override Dictionary<string, string> Parameters
    {

        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_scId", ScheduleId);
            parameters.Add("_dow", Dow);
            parameters.Add("_seqNo", "0");

            return parameters;
        }
    }
}
public class CalendarYearsListRequest : ListRequest
{
    public string CalendarId { get; set; }

    public Dictionary<string, string> parameters;
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_caId", CalendarId);

            return parameters;
        }
    }
}

public class CalendarDayListRequest : ListRequest
{
    public string CalendarId { get; set; }
    public string Year { get; set; }

    public Dictionary<string, string> parameters;
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_caId", CalendarId);
            parameters.Add("_year", Year);

            return parameters;
        }
    }
}


public class CalendarDayRecordRequest : RecordRequest
{
    public string CaId { get; set; }

    public string year { get; set; }

    public string DayId { get; set; }



    public override Dictionary<string, string> Parameters
    {

        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_caId", CaId);
            parameters.Add("_dayId", DayId);
            parameters.Add("_year", year);


            return parameters;
        }
    }
}
public class AttendnanceDayListRequest : ListRequest
{
    public string EmployeeId { get; set; }
    public string StartDayId { get; set; }
    public string EndDayId { get; set; }
    public string Month { get; set; }
    public string Year { get; set; }

    public string BranchId { get; set; }
    public string DepartmentId { get; set; }
    public string DivisionId { get; set; }
 //   public int apStatus { get; set; }
    public string SortBy { get; set; }

    public override Dictionary<string, string> Parameters
    {

        get
        {
            parameters = base.Parameters;
            parameters.Add("_employeeId", EmployeeId);
            parameters.Add("_startDayId", StartDayId);
            parameters.Add("_endDayId", EndDayId);
            parameters.Add("_month", Month);
            parameters.Add("_year", Year);
            parameters.Add("_branchId", BranchId);
            parameters.Add("_departmentId", DepartmentId);
            parameters.Add("_divisionId", DivisionId);
            parameters.Add("_sortBy", SortBy);
        //    parameters.Add("_apStatus", apStatus.ToString());



            return parameters;
        }
    }
}

public class ActiveAttendanceRequest : ListRequest
{
    public string   DepartmentId { get; set; }

    public string BranchId { get; set; }

    public string PositionId { get; set; }
    public string StatusId { get; set; }
    public string DivisionId { get; set; }

    public int? DayStatus { get; set; }

   

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_departmentId", DepartmentId.ToString());
            parameters.Add("_branchId", BranchId.ToString());
            parameters.Add("_positionId", PositionId.ToString());
            parameters.Add("_divisionId", DivisionId.ToString());
            parameters.Add("_esId", StatusId.ToString());

          
            if (DayStatus.HasValue)
                parameters.Add("_dayStatus", DayStatus.Value.ToString());
            return parameters;
        }
    }

   
}
public class TimeBoundedActiveAttendanceRequest :ListRequest
{

    public string paramString { get; set; }
    public string startingDayId { get; set; }
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_params", paramString);
            parameters.Add("_startingDayId", startingDayId);
            return parameters;
        }
    }

    
}

public class GetRouterRequest : RecordRequest
{
    public string RouterRef { get; set; }

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_routerRef", RouterRef);
            return parameters;
        }
    }
}

public class AttendanceShiftListRequest : ListRequest
{
    public int EmployeeId { get; set; }

    public string DayId { get; set; }



    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_employeeId", EmployeeId.ToString());
            parameters.Add("_dayId", DayId.ToString());

            return parameters;
        }
    }
}
public class TimeAttendanceViewListRequest : ListRequest
{
    public string paramString { get; set; }
    public string sortBy { get; set; }

    



    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_params", paramString);
            parameters.Add("_sortBy", sortBy);

            return parameters;
        }
    }
}