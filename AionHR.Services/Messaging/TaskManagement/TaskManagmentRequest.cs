using Infrastructure.Domain;
using Services.Messaging;
using Services.Messaging.System;
using System.Collections.Generic;

namespace Services.Messaging.TaskManagement
{
    public class TaskManagementListRequest : ListRequest
    {
        public int DepartmentId { get; set; }

        public int BranchId { get; set; }

        public int DivisionId { get; set; }

        public int AssignToId { get; set; }

        public int InRelationToId { get; set; }

        public int Completed { get; set; }

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_departmentId", DepartmentId.ToString());
                parameters.Add("_branchId", BranchId.ToString());
                parameters.Add("_divisionId", DivisionId.ToString());
                parameters.Add("_assignToId", AssignToId.ToString());
                parameters.Add("_inRelationToId", InRelationToId.ToString());
                parameters.Add("_completed", Completed.ToString());
                parameters.Add("_sortBy", SortBy.ToString());

                return parameters;
            }
        }

        public string SortBy { get; set; }
    }
}


public class TaskCommentsListRequest : ListRequest
{
    public int taskId { get; set; }

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_taskId", taskId.ToString());

            return parameters;
        }
    }

}

public class TaskAttachmentsListRequest : SystemAttachmentsListRequest
{
    public TaskAttachmentsListRequest()
    {
        base.classId = ClassId.TMTA;
    }
}

