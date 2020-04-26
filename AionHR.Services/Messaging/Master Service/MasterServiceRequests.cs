using Services.Messaging;
using System.Collections.Generic;

public class GetAccountRequest:RequestBase
{
    public string Account { get; set; }

    private  Dictionary<string, string> parameters;

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_accountName", Account);
            return parameters;
        }
    }

}

public class AccountRecoveryRequest : RequestBase
{
    public string Email { get; set; }

    private Dictionary<string, string> parameters;

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_email", Email);
            return parameters;
        }
    }

}
