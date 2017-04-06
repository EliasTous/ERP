using AionHR.Services.Messaging.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionHR.Services.Messaging;

namespace AionHR.Services.Messaging.CompanyStructure
{
  public class CompanyFilesListRequest: SystemAttachmentsListRequest
    {
        public CompanyFilesListRequest()
        {
            base.classId = 20080;
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