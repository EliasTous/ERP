using System.Collections.Generic;
using AionHR.Model.Employees.Profile;
using AionHR.Services.Messaging;

public class FiscalPeriodsListRequest:ListRequest
{
    public string Year { get; set; }

    public SalaryType PeriodType { get; set; }

    public string Status { get; set; }

    private Dictionary<string, string> parameters;
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_year", Year);
            parameters.Add("_salaryType", ((int)PeriodType).ToString());
            parameters.Add("_status", Status.ToString());
            return parameters;
        }
    }


}

public class EmployeePayrollListRequest : ListRequest
{
    public string DepartmentId { get; set; }

    public string BranchId { get; set; }

    public string EmployeeId { get; set; }

    public string PayId { get; set; }

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_payId", PayId);
            parameters.Add("_departmentId", DepartmentId);
            parameters.Add("_branchId", BranchId);
            parameters.Add("_employeeId", EmployeeId);
            return parameters;
        }
    }
}

public class PayrollListRequest:ListRequest
{
    public string Year { get; set; }

    public string PeriodType { get; set; }

    public string Status { get; set; }

    private Dictionary<string, string> parameters;
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_year", Year);
            parameters.Add("_salaryType", PeriodType);
            parameters.Add("_status", Status);
            return parameters;
        }
    }
}
public class PayrollEntitlementsDeductionListRequest : ListRequest
{
    public string PayId { get; set; }

    public string SeqNo { get; set; }

   

    private Dictionary<string, string> parameters;
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_payId", PayId);
            parameters.Add("_seqNo", SeqNo);
            
            return parameters;
        }
    }
}