using AionHR.Services.Messaging;
using System.Collections.Generic;

public class CaseManagementListRequest : ListRequest
{

    public int EmployeeId { get; set; }

    public int DepartmentId { get; set; }

    public int BranchId { get; set; }

    public int DivisionId { get; set; }


    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_employeeId", EmployeeId.ToString());
            parameters.Add("_departmentId", DepartmentId.ToString());
            parameters.Add("_branchId", BranchId.ToString());
            parameters.Add("_divisionId", DivisionId.ToString());

            return parameters;
        }
    }
}


public class CaseCommentsListRequest : ListRequest
{
    public int caseId { get; set; }

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_caseId", caseId.ToString());           

            return parameters;
        }
    }

}