using AionHR.Services.Messaging;
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

public class VacationPeriodsListRequest:ListRequest
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