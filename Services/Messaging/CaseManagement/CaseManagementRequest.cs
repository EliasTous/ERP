using Infrastructure.Domain;
using Services.Messaging;
using Services.Messaging.System;
using System.Collections.Generic;

public class CaseManagementListRequest : ListRequest
{

    public int EmployeeId { get; set; }

    public int DepartmentId { get; set; }

    public int BranchId { get; set; }

    public int Status { get; set; }
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
            parameters.Add("_status", Status.ToString());
            parameters.Add("_sortBy", SortBy.ToString());

            return parameters;
        }
    }

    public string SortBy { get; set; }
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

public class CaseAttachmentsListRequest: SystemAttachmentsListRequest
{
    public CaseAttachmentsListRequest()
    {
        base.classId = ClassId.CMCA;
    }
}


