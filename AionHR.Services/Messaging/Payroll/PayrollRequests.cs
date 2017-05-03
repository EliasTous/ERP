using System.Collections.Generic;
using AionHR.Model.Employees.Profile;
using AionHR.Services.Messaging;

public class FiscalPeriodsListRequest:ListRequest
{
    public string Year { get; set; }

    public PaymentFrequency PeriodType { get; set; }

    public short Status { get; set; }

    private Dictionary<string, string> parameters;
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_year", Year);
            parameters.Add("_periodType", ((int)PeriodType).ToString());
            parameters.Add("_status", Status.ToString());
            return parameters;
        }
    }


}