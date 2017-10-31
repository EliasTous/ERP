using AionHR.Services.Messaging;
using System.Collections.Generic;

public class DeleteVacationPeriodsRequest : RequestBase

{
    public string ScheduleId
    { get; set; }

    private Dictionary<string, string> parameters;

    /// <summary>
    /// parameter list shipped with the web request
    /// </summary>
    public override Dictionary<string, string> Parameters
    {

        get
        {
            parameters = base.Parameters;
            parameters.Add("_vsId", ScheduleId);
            parameters.Add("_seqNo", "0");

            return parameters;
        }
    }
}

public class VacationPeriodsListRequest : ListRequest
{
    public string VacationScheduleId { get; set; }


    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_vsId", VacationScheduleId);


            return parameters;
        }
    }
}
public class LeaveRequestListRequest : ListRequest
{
    public int OpenRequests { get; set; }
    public int BranchId { get; set; }
    public int DepartmentId { get; set; }
    public int EmployeeId { get; set; }

    public string SortBy { get; set; }
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_openRequests", OpenRequests.ToString());
            parameters.Add("_branchId", BranchId.ToString());
            parameters.Add("_departmentId", DepartmentId.ToString());
            parameters.Add("_employeeId", EmployeeId.ToString());
            parameters.Add("_sortBy", SortBy.ToString());


            return parameters;
        }
    }
}

public class LeaveCalendarDayListRequest : ListRequest
{
    public string CaId { get; set; }

    public string StartDayId { get; set; }

    public string EndDayId { get; set; }

    public bool IsWorkingDay { get; set; }

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_caId", CaId.ToString());
            parameters.Add("_startDayId", StartDayId.ToString());
            parameters.Add("_endDayId", EndDayId.ToString());
            parameters.Add("_isWorkingDay", IsWorkingDay ? "1" : "0");



            return parameters;
        }
    }
}

public class LeaveDayListRequest : ListRequest
{
    public string LeaveId { get; set; }
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_leaveId", LeaveId.ToString());




            return parameters;
        }
    }
}


