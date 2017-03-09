using AionHR.Model.Employees.Profile;
using AionHR.Services.Messaging;
using System.Collections.Generic;

public class EmployeeListRequest:ListRequest
{
    public string DepartmentId { get; set; }
    public string BranchId { get; set; }

    public bool IncludeIsInactive { get; set; }

    public string SortBy { get; set; }
    

    
    /// <summary>
    /// /// parameter list shipped with the web request
    /// </summary>
    public override Dictionary<string, string> Parameters
    {

        get
        {
            parameters = base.Parameters;
           
            parameters.Add("_departmentId", DepartmentId);
            parameters.Add("_branchId", BranchId);
            parameters.Add("_includeInactive", IncludeIsInactive.ToString());
            parameters.Add("_sortBy", SortBy);
           
            return parameters;
        }
    }
}

public class EmployeeAddOrUpdateRequest
{
    public Employee empData { get; set; }
    public byte[] imageData { get; set; }

    public string fileName { get; set; }
}