using AionHR.Services.Messaging.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionHR.Services.Messaging;
using AionHR.Infrastructure.Domain;

namespace AionHR.Services.Messaging.CompanyStructure
{
  public class CompanyFilesListRequest: SystemAttachmentsListRequest
    {
        public CompanyFilesListRequest()
        {
            base.classId = ClassId.DMDO;
        }
    }
}

public class MediaItemsListRequest : ListRequest
{

    public string SortBy { get; set; }
    public int MCId { get; set; }
    public int DepartmentId { get; set; }
    public short Type { get; set; }


    /// <summary>
    /// /// parameter list shipped with the web request
    /// </summary>
    public override Dictionary<string, string> Parameters
    {

        get
        {
            parameters = base.Parameters;

            parameters.Add("_sortBy", SortBy);
            parameters.Add("_mcId", MCId.ToString());
            parameters.Add("_departmentId", DepartmentId.ToString());
            parameters.Add("_type", Type.ToString());

            return parameters;
        }
    }
}


public class CompanyRightToworkListRequest : ListRequest
{

    public string SortBy { get; set; }
    public int DTid { get; set; }
    public int BranchId { get; set; }
    

    /// <summary>
    /// /// parameter list shipped with the web request
    /// </summary>
    public override Dictionary<string, string> Parameters
    {

        get
        {
            parameters = base.Parameters;

            parameters.Add("_sortBy", SortBy);
            parameters.Add("_dtId", DTid.ToString());
            parameters.Add("_branchId", BranchId.ToString());
            
            return parameters;
        }
    }
}

public class DepartmentByReference:RecordRequest
{
    public string Reference { get; set; }

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_departmentRef", Reference);
            return parameters;
        }
    }
}

public class BranchWorkRecordRequest : ListRequest
{
    public string FromDayId { get; set; }

    public string ToDayId { get; set; }

    public string BranchId { get; set; }
    

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_fromDayId", FromDayId);
            parameters.Add("_toDayId", ToDayId);
            parameters.Add("_branchId", BranchId.ToString());

            return parameters;
        }
    }
}
public class LegalReferenceListRequest : ListRequest
{
    public string branchId  { get; set; }

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_branchId", branchId);
            return parameters;
        }
    }
}
public class LegalReferenceRecordRequest :RecordRequest
{
    
    public string branchId { get; set; }
    public string goId { get; set; }

    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("_branchId", branchId);
            parameters.Add("_goId", goId);
            return parameters;
        }
    }
}
public class DepartmentListRequest : ListRequest
{

   public int type { get; set; }
    public int? isInactive { get; set; }


    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;
            parameters.Add("_type", type.ToString());
            if (isInactive == null)
                isInactive = 0; 
            parameters.Add("_activeStatus", isInactive.ToString()  );


            return parameters;
        }
    }
}