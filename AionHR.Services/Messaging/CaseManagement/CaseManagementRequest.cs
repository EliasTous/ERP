using AionHR.Services.Messaging;
using System.Collections.Generic;

public class CaseManagementListRequest : ListRequest
{

    public int EmployeeId { get; set; }


    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_employeeId", EmployeeId.ToString());

            return parameters;
        }
    }
}