using AionHR.Services.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class SelfServiceLoanRecordRequest:RecordRequest
{
    public int LoanId { get; set; }
    private Dictionary<string, string> parameters;

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_loanId", LoanId.ToString());
            return parameters;
        }
    }
}
public class SelfServiceLeaveRecordRequest : RecordRequest
{
    public int LeaveId { get; set; }
    private Dictionary<string, string> parameters;

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_leaveId", LeaveId.ToString());
            return parameters;
        }
    }
}