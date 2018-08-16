using AionHR.Infrastructure.Domain;
using AionHR.Model.Employees.Profile;
using AionHR.Model.System;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.System;
using System;
using System.Collections.Generic;
using System.Globalization;

public class EmployeeListRequest:ListRequest
{
    public string DepartmentId { get; set; }
    public string BranchId { get; set; }

    public int IncludeIsInactive { get; set; }

    public string SortBy { get; set; }
    public string PositionId { get; set; }
    public string DivisionId { get; set; }
   
    public string filterField
    {
        get; set;

    }



    /// <summary>
    /// /// parameter list shipped with the web request
    /// </summary>
    public override Dictionary<string, string> Parameters
    {

        get
        {
            parameters = base.Parameters;
           
            parameters.Add("_departmentId", DepartmentId);
            parameters.Add("_branchId", BranchId);
            parameters.Add("_positionId", PositionId);
            parameters.Add("_includeInactive", IncludeIsInactive.ToString());
            parameters.Add("_sortBy", SortBy);
            parameters.Add("_divisionId", DivisionId);
            parameters.Add("_filterField", (string.IsNullOrEmpty(filterField)?"0":filterField));


          

            return parameters;
        }
    }

   
}

public class EmployeeAddOrUpdateRequest
{
    public Employee empData { get; set; }
    public byte[] imageData { get; set; }

    public string fileName { get; set; }
}

public class EmployeementHistoryListRequest:ListRequest
{
    public string employeeId { get; set; }
    public string DepartmentId { get; set; }
    public string BranchId { get; set; }

    public string positionId { get; set; }

    public string divisionId { get; set; }

    public string SortBy { get; set; }



    /// <summary>
    /// /// parameter list shipped with the web request
    /// </summary>
    public override Dictionary<string, string> Parameters
    {

        get
        {
            parameters = base.Parameters;

            parameters.Add("_departmentId", DepartmentId);
            parameters.Add("_branchId", BranchId);
            parameters.Add("_positionId", positionId);
            parameters.Add("_divisionId", divisionId);
            parameters.Add("_employeeId", employeeId);
            parameters.Add("_sortBy", SortBy);

            return parameters;
        }
    }
}
public class JobInfoListRequest:EmployeementHistoryListRequest
{

}

public class EmployeeSalaryListRequest :ListRequest
{
    public string EmployeeId { get; set; }




    /// <summary>
    /// /// parameter list shipped with the web request
    /// </summary>
    public override Dictionary<string, string> Parameters
    {

        get
        {
            parameters = base.Parameters;

            parameters.Add("_employeeId", EmployeeId);
          

            return parameters;
        }
    }
}
public class EmployeeBonusListRequest:ListRequest
{
    public int EmployeeId { get; set; }

    public int BonusTypeId { get; set; }

    public int CurrencyId { get; set; }




    /// <summary>
    /// /// parameter list shipped with the web request
    /// </summary>
    public override Dictionary<string, string> Parameters
    {

        get
        {
            parameters = base.Parameters;

            parameters.Add("_employeeId", EmployeeId.ToString());
            parameters.Add("_btId", BonusTypeId.ToString());
            parameters.Add("_currencyId", CurrencyId.ToString());


            return parameters;
        }
    }
}
public class SalaryDetailsListRequest : ListRequest
{
    public int SalaryID { get; set; }

    public int Type { get; set; }






    /// <summary>
    /// /// parameter list shipped with the web request
    /// </summary>
    public override Dictionary<string, string> Parameters
    {

        get
        {
            parameters = base.Parameters;

            parameters.Add("_salaryId", SalaryID.ToString());
            parameters.Add("_type", Type.ToString());
            


            return parameters;
        }
    }
    
}
public class EmployeeNotesListRequest:ListRequest
{
    public int EmployeeId { get; set; }

    public string SortBy { get; set; }






    /// <summary>
    /// /// parameter list shipped with the web request
    /// </summary>
    public override Dictionary<string, string> Parameters
    {

        get
        {
            parameters = base.Parameters;

            parameters.Add("_employeeId", EmployeeId.ToString());
            parameters.Add("_sortBy", SortBy);
            


            return parameters;
        }
    }
}

public class EmployeeDocumentsListRequest:ListRequest
{
    public int EmployeeId { get; set; }

    public string SortBy { get; set; }






    /// <summary>
    /// /// parameter list shipped with the web request
    /// </summary>
    public override Dictionary<string, string> Parameters
    {

        get
        {
            parameters = base.Parameters;

            parameters.Add("_employeeId", EmployeeId.ToString());
            parameters.Add("_sortBy", SortBy);



            return parameters;
        }
    }
}


public class EmployeeRightToWorkListRequest : ListRequest
{
    public string EmployeeId { get; set; }

    /// <summary>
    /// /// parameter list shipped with the web request
    /// </summary>
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


public class EmployeeBackgroundCheckListRequest : ListRequest
{
    public string EmployeeId { get; set; }

    /// <summary>
    /// /// parameter list shipped with the web request
    /// </summary>
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


public class EmployeeDocumentAddOrUpdateRequest
{
    public EmployeeDocument documentData { get; set; }
    public byte[] fileData { get; set; }

    public string fileName { get; set; }
}


public class AssetAllowanceListRequest : ListRequest
{
    public int BranchId { get; set; }
    public int DepartmentId { get; set; }
    public int EmployeeId { get; set; }

    public int AcId { get; set; }

    public string SortBy { get; set; }
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_branchId", BranchId.ToString());
            parameters.Add("_departmentId", DepartmentId.ToString());
            parameters.Add("_employeeId", EmployeeId.ToString());
            parameters.Add("_acId", AcId.ToString());
            parameters.Add("_sortBy", SortBy.ToString());


            return parameters;
        }
    }
}


public class EmployeeComplaintListRequest : ListRequest
{

    public int EmployeeId { get; set; }

    public int DepartmentId { get; set; }

    public int BranchId { get; set; }

    public int Status { get; set; }
    public int DivisionId { get; set; }


    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_employeeId", EmployeeId.ToString());
            parameters.Add("_departmentId", DepartmentId.ToString());
            parameters.Add("_branchId", BranchId.ToString());
            parameters.Add("_divisionId", DivisionId.ToString());
            parameters.Add("_status", Status.ToString());
            parameters.Add("_sortBy", SortBy.ToString());

            return parameters;
        }
    }

    public string SortBy { get; set; }
}
public class EmployeeAttachmentsListRequest : SystemAttachmentsListRequest
{
    public EmployeeAttachmentsListRequest()
    {
        base.classId = ClassId.EPDO;
    }
}

public class EmployeeUploadPhotoRequest : PostRequest<Attachement>
{
    public EmployeeUploadPhotoRequest()
    {
        entity = new Attachement();
        entity.classId = ClassId.EPEM;
        entity.folderId = null;
        entity.seqNo = 0;
        entity.folderId = null;
    }

    public string photoName { get; set; }

    public byte[] photoData { get; set; }

}


public class TeamMembersListRequest : ListRequest
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

public class EmployeeContactsListRequest : ListRequest
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
public class HireInfoRecordRequest:RecordRequest
{
    public string EmployeeId { get; set; }

    private Dictionary<string, string> parameters;

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_employeeId", EmployeeId);
            return parameters;
        }
    }


}
public class GetDependantRequest : RecordRequest
{
    public string EmployeeId { get; set; }

    public string SeqNo { get; set; }

    private Dictionary<string, string> parameters;

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_employeeId", EmployeeId);
            parameters.Add("_seqNo", SeqNo);
            return parameters;
        }
    }


}

public class EmployeeByReference:RecordRequest
{
    public string Reference { get; set; }
    private Dictionary<string, string> parameters;

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_reference", Reference);
            
            return parameters;
        }
    }
}
public class StatusByReference : RecordRequest
{
    public string Reference { get; set; }
    private Dictionary<string, string> parameters;

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_esRef", Reference);

            return parameters;
        }
    }
}

public class EmployeeCountRequest:RecordRequest
{
    public int DepartmentId { get; set; }

    public int BranchId { get; set; }

    public int PositionId { get; set; }

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_departmentId", DepartmentId.ToString());
            parameters.Add("_branchId", BranchId.ToString());
            parameters.Add("_positionId", PositionId.ToString());
            parameters.Add("_divisionId", StatusId.ToString());
            parameters.Add("_esId", DivisionId.ToString());
            return parameters;
        }
    }

    public int StatusId { get; set; }
    public int DivisionId { get; set; }

}
public class EmployeeCalendarRequest : ListRequest
{
    public int employeeId { get; set; }
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_employeeId", employeeId.ToString());
            return parameters;
        }
    }



}
public class EmployeeCalendarRecordRequest:RecordRequest
{
    public int employeeId { get; set; }
    public string dayId { set; get; }
    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>(); 
            parameters.Add("_employeeId", employeeId.ToString());
            parameters.Add("_dayId", dayId);

            return parameters;
        }
    }


}
public class DeleteEmployeeCalenderRequest : RequestBase

{
    public int employeeId
    { get; set; }
    public string dayId
    { get; set; }
    public Dictionary<string, string> parameters;

    /// <summary>
    /// parameter list shipped with the web request
    /// </summary>
    public override Dictionary<string, string> Parameters
    {

        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_employeeId", employeeId.ToString());
            parameters.Add("_dayId", dayId);


            return parameters;
        }
    }
    
}

public class EmployeeQuickViewRecordRequest : RecordRequest
{
   

   

    public DateTime asOfDate { get; set; }

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_asOfDate", asOfDate.ToString("MM/dd/yyyy", new CultureInfo("en-US")));
            
            return parameters;
        }
    }

   

}

public class EmployeeRecruitmentRecordRequest : RecordRequest
{
    public string EmployeeId { get; set; }




    /// <summary>
    /// /// parameter list shipped with the web request
    /// </summary>
    public override Dictionary<string, string> Parameters
    {

        get
        {
            parameters = base.Parameters;

            parameters.Add("_employeeId", EmployeeId);


            return parameters;
        }
    }
}
