﻿using System.Collections.Generic;
using Model.Employees.Profile;
using Services.Messaging;

public class FiscalPeriodsListRequest:ListRequest
{
    public string Year { get; set; }

    public int PeriodType { get; set; }

    public string Status { get; set; }

    private Dictionary<string, string> parameters;
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_year", Year);
            parameters.Add("_salaryType", (PeriodType).ToString());
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
           // parameters.Add("_positionId", Position);
            
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
public class PayrollTimeCodeRequest : ListRequest
{
    public int ScheduleId { set; get; }
    private Dictionary<string, string> parameters;
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_tsId", ScheduleId.ToString());
        

            return parameters;
        }
    }

}
public class SocialSecurityScheduleSetupRequest:RecordRequest
{
    public int ssId { set; get; }
    public int seqNo { set; get; }

    private Dictionary<string, string> parameters;
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_ssId", ssId.ToString());
            parameters.Add("_seqNo", seqNo.ToString());


            return parameters;
        }
    }

}
public class SocialSecurityScheduleSetupListRequest : ListRequest
{
    public int ssId { set; get; }
   

    private Dictionary<string, string> parameters;
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_ssId", ssId.ToString());
           


            return parameters;
        }
    }

}
public class FinalEntitlementsDeductionsListRequest:ListRequest
{
    
   
        public int fsId { set; get; }
    public int type { get; set; }
    public string sortBy { set; get; }


    private Dictionary<string, string> parameters;
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_fsId", fsId.ToString());
            parameters.Add("_type", type.ToString());
            parameters.Add("_sortBy", type.ToString());



            return parameters;
        }
    }

}
public class FinalEntitlementsDeductionsRecordRequest : RecordRequest
{


    public int fsId { set; get; }
    public int seqNo { get; set; }


    private Dictionary<string, string> parameters;
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_fsId", fsId.ToString());
            parameters.Add("_seqNo", seqNo.ToString());



            return parameters;
        }
    }

}
public class PayCodeRecordRequest : RecordRequest
{


   
    public string payCode { get; set; }


    private Dictionary<string, string> parameters;
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_payCode", payCode);
            



            return parameters;
        }
    }

}
public class PayrollSocialSecurityListRequest:ListRequest
{ 
    public string payId { get; set; }

    public string seqNo { get; set; }
            

    private Dictionary<string, string> parameters;
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_payId", payId);
            parameters.Add("_seqNo", seqNo);
                       
            return parameters;
        }
    }

}
public class PayrollIndemnityDetailsListRequest : ListRequest
{
    public string inId { get; set; }


    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_inId", inId);


            return parameters;
        }
    }

}
public class LeavePaymentsListRequest : ListRequest
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

public class PenaltyDetailListRequest :ListRequest
{
    public string ptId { set; get; }
    public string damage { get; set; }


    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_ptId", ptId.ToString());
            parameters.Add("_damage", damage.ToString());


            return parameters;
        }
    }
}

public class FiscalPeriodRecordRequest : RecordRequest
{



    public string year { get; set; }
    public string salaryType { get; set; }
    public string periodId { get; set; }
    


    private Dictionary<string, string> parameters;
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_year", year);
            parameters.Add("_salaryType", salaryType);
            parameters.Add("_periodId", periodId);




            return parameters;
        }
    }

    

}

public class PayrollExFunCodeRequest : ListRequest
{
    public string ExpressionId { set; get; }
    private Dictionary<string, string> parameters;
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_expressionId", ExpressionId.ToString());


            return parameters;
        }
    }

}


public class PayrollFunConstCodeRequest : ListRequest
{
    public string FunctionId { set; get; }
    private Dictionary<string, string> parameters;
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_functionId", FunctionId.ToString());


            return parameters;
        }
    }

}
