using AionHR.Services.Messaging;
using System.Collections.Generic;

public class GroupUsersListRequest : ListRequest
{
    public string GroupId { get; set; }

    private Dictionary<string, string> parameters;

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_sgId", GroupId);
            return parameters;
        }
    }
}
public class AccessControlListRequest : ListRequest
{
    public string GroupId { get; set; }

    private Dictionary<string, string> parameters;

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_sgId", GroupId);
            return parameters;
        }
    }
}