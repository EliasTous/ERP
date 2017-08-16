using System.Collections.Generic;
using AionHR.Services.Messaging;

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



            return parameters;
        }
    }
}

public class ActiveAttendanceRequest : ListRequest
{
    public int DepartmentId { get; set; }

    public int BranchId { get; set; }

    public int PositionId { get; set; }

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_departmentId", DepartmentId.ToString());
            parameters.Add("_branchId", BranchId.ToString());
            parameters.Add("_positionId", PositionId.ToString());
            parameters.Add("_divisionId", StatusId.ToString());
            parameters.Add("_esId", DivisionId.ToString());
            if (DayStatus.HasValue)
                parameters.Add("_dayStatus", DayStatus.Value.ToString());
            return parameters;
        }
    }

    public int StatusId { get; set; }
    public int DivisionId { get; set; }

    public int? DayStatus { get; set; }
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

    public int DayId { get; set; }



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