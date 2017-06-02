﻿using AionHR.Services.Messaging;
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