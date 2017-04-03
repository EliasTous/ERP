using AionHR.Model.Employees.Profile;
using AionHR.Services.Messaging;
using System.Collections.Generic;

public class EmployeeListRequest:ListRequest
{
    public string DepartmentId { get; set; }
    public string BranchId { get; set; }

    public int IncludeIsInactive { get; set; }

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

public class EmployeementHistoryListRequest:ListRequest
{
    public string employeeId { get; set; }
    public string DepartmentId { get; set; }
    public string BranchId { get; set; }

    public string positionId { get; set; }

    public string divisionId { get; set; }

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
            parameters.Add("_positionId", positionId);
            parameters.Add("_divisionId", divisionId);
            parameters.Add("_employeeId", employeeId);
            parameters.Add("_sortBy", SortBy);

            return parameters;
        }
    }
}
public class JobInfoListRequest:EmployeementHistoryListRequest
{

}

public class EmployeeSalaryListRequest :ListRequest
{
    public string EmployeeId { get; set; }




    /// <summary>
    /// /// parameter list shipped with the web request
    /// </summary>
    public override Dictionary<string, string> Parameters
    {

        get
        {
            parameters = base.Parameters;

            parameters.Add("_employeeId", EmployeeId);
          

            return parameters;
        }
    }
}
public class EmployeeBonusListRequest:ListRequest
{
    public int EmployeeId { get; set; }

    public int BonusTypeId { get; set; }

    public int CurrencyId { get; set; }




    /// <summary>
    /// /// parameter list shipped with the web request
    /// </summary>
    public override Dictionary<string, string> Parameters
    {

        get
        {
            parameters = base.Parameters;

            parameters.Add("_employeeId", EmployeeId.ToString());
            parameters.Add("_btId", BonusTypeId.ToString());
            parameters.Add("_currencyId", CurrencyId.ToString());


            return parameters;
        }
    }
}
public class SalaryDetailsListRequest : ListRequest
{
    public int SalaryID { get; set; }

    public int Type { get; set; }






    /// <summary>
    /// /// parameter list shipped with the web request
    /// </summary>
    public override Dictionary<string, string> Parameters
    {

        get
        {
            parameters = base.Parameters;

            parameters.Add("_salaryId", SalaryID.ToString());
            parameters.Add("_type", Type.ToString());
            


            return parameters;
        }
    }
    
}
public class EmployeeNotesListRequest:ListRequest
{
    public int EmployeeId { get; set; }

    public string SortBy { get; set; }






    /// <summary>
    /// /// parameter list shipped with the web request
    /// </summary>
    public override Dictionary<string, string> Parameters
    {

        get
        {
            parameters = base.Parameters;

            parameters.Add("_employeeId", EmployeeId.ToString());
            parameters.Add("_sortBy", SortBy);
            


            return parameters;
        }
    }
}

public class EmployeeDocumentsListRequest:ListRequest
{
    public int EmployeeId { get; set; }

    public string SortBy { get; set; }






    /// <summary>
    /// /// parameter list shipped with the web request
    /// </summary>
    public override Dictionary<string, string> Parameters
    {

        get
        {
            parameters = base.Parameters;

            parameters.Add("_employeeId", EmployeeId.ToString());
            parameters.Add("_sortBy", SortBy);



            return parameters;
        }
    }
}


public class EmployeeRightToWorkListRequest : ListRequest
{
    public string EmployeeId { get; set; }

    /// <summary>
    /// /// parameter list shipped with the web request
    /// </summary>
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


public class EmployeeBackgroundCheckListRequest : ListRequest
{
    public string EmployeeId { get; set; }

    /// <summary>
    /// /// parameter list shipped with the web request
    /// </summary>
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


public class EmployeeDocumentAddOrUpdateRequest
{
    public EmployeeDocument documentData { get; set; }
    public byte[] fileData { get; set; }

    public string fileName { get; set; }
}


public class AssetAllowanceListRequest : ListRequest
{
    public int BranchId { get; set; }
    public int DepartmentId { get; set; }
    public int EmployeeId { get; set; }

    public int AcId { get; set; }

    public string SortBy { get; set; }
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_branchId", BranchId.ToString());
            parameters.Add("_departmentId", DepartmentId.ToString());
            parameters.Add("_employeeId", EmployeeId.ToString());
            parameters.Add("_acId", AcId.ToString());
            parameters.Add("_sortBy", SortBy.ToString());


            return parameters;
        }
    }
}