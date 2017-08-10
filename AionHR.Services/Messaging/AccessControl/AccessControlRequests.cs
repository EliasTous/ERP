using AionHR.Services.Messaging;
using System.Collections.Generic;

public class GroupUsersListRequest : ListRequest
{
    public string GroupId { get; set; }

    public string UserId { get; set; }

    private Dictionary<string, string> parameters;


    public GroupUsersListRequest()
    {
        GroupId = "0";
        UserId = "0";
    }
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_sgId", GroupId);
            parameters.Add("_userId", UserId);
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

public class PropertiesListRequest : ListRequest
{
    public string GroupId { get; set; }

    public string ClassId { get; set; }

    private Dictionary<string, string> parameters;

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_sgId", GroupId);
            parameters.Add("_classId", ClassId);
            return parameters;
        }
    }
}
public class ClassesListRequest : ListRequest
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
public class UserPropertiesPermissions : ListRequest
{
    public string UserId { get; set; }

    public string ClassId { get; set; }



    private Dictionary<string, string> parameters;

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_classId", ClassId);
            parameters.Add("_userId", UserId);

            return parameters;
        }
    }

    
}

public class ClassPermissionRecordRequest : RecordRequest
{
    public string UserId { get; set; }

    public string ClassId { get; set; }

    private Dictionary<string, string> parameters;
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_userId", UserId);
            parameters.Add("_classId", ClassId);
            return parameters;
        }
    }
}

public class SecurityGroupsListRequest : ListRequest
{
    public string UserId { get; set; }

    private Dictionary<string, string> parameters;

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters = new Dictionary<string, string>();
            parameters.Add("_userId", UserId);
            return parameters;
        }
    }


}

public class DataAccessListRequest:ListRequest
{
    public string sgId { get; set; }

    public string classId { get; set; }
    private Dictionary<string, string> parameters;

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.parameters;
            
            parameters.Add("_sgId", sgId);
            parameters.Add("_classId", classId);
            return parameters;
        }
    }

}
public class DataAccessRecordRequest:RecordRequest
{
    public string sgId { get; set; }

    public string classId { get; set; }
    private Dictionary<string, string> parameters;

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;

            parameters.Add("_sgId", sgId);
            parameters.Add("_classId", classId);
            return parameters;
        }
    }
}
