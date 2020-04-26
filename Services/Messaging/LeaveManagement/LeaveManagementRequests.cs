using Services.Messaging;
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



public class LeaveSchedulesListRequest : ListRequest
{
    public string LeaveScheduleId { get; set; }


    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_lsId", LeaveScheduleId);


            return parameters;
        }
    }
}
public class LeaveRequestListRequest : ListRequest
{
    //public int status { get; set; }
    //public int BranchId { get; set; }
    //public int DepartmentId { get; set; }
    //public int EmployeeId { get; set; }
    //public int ApproverId { get; set; }

    public string Params { get; set; }

    public string SortBy { get; set; }
    //public int raEmployeeId { get; set; }
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            //parameters.Add("_status", status.ToString());
            //parameters.Add("_branchId", BranchId.ToString());
            //parameters.Add("_departmentId", DepartmentId.ToString());
            //parameters.Add("_employeeId", EmployeeId.ToString());
            parameters.Add("_sortBy", SortBy.ToString());
            //parameters.Add("_raEmployeeId", raEmployeeId.ToString());
            parameters.Add("_params", Params);
            //parameters.Add("_approverId", ApproverId.ToString());




            return parameters;
        }
    }
}

public class LeaveCalendarDayListRequest : ListRequest
{
    public string employeeId { get; set; }

    public string StartDayId { get; set; }

    public string EndDayId { get; set; }

    public bool IsWorkingDay { get; set; }

    public int caId { get; set; }

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_employeeId", employeeId);
            parameters.Add("_fromDayId", StartDayId.ToString());
            parameters.Add("_toDayId", EndDayId.ToString());
            parameters.Add("_isWorkingDay", IsWorkingDay ? "1" : "0");
            parameters.Add("_caId", caId.ToString());


            return parameters;
        }
    }
}

public class WorkingDayListRequest : ListRequest
{
    public string employeeId { get; set; }

    public string StartDayId { get; set; }

    public string EndDayId { get; set; }

    public bool IsWorkingDay { get; set; }

    public int caId { get; set; }

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_employeeId", employeeId);
            parameters.Add("_startDayId", StartDayId.ToString());
            parameters.Add("_endDayId", EndDayId.ToString());
            parameters.Add("_isWorkingDay", IsWorkingDay ? "1" : "0");
            parameters.Add("_caId", caId.ToString());


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
            parameters.Add("_leaveId", LeaveId);




            return parameters;
        }
    }
}
public class LeaveRequestTamplateRequest : ListRequest
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



