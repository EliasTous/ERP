using AionHR.Model.Company.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionHR.Infrastructure.WebService;
using AionHR.Infrastructure.Configuration;
using AionHR.Infrastructure.Domain;

namespace AionHR.Repository.WebService.Repositories
{
    public class CompanyStructureRepository : Repository<IEntity,string>, ICompanyStructureRepository
    {
        private string serviceName = "CS.asmx/";
        public CompanyStructureRepository()
        {

            base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;
            
            base.ChildGetLookup.Add(typeof(Branch), "getBR");
            base.ChildGetLookup.Add(typeof(Department), "getDE");
            base.ChildGetLookup.Add(typeof(Position), "getPO");
            base.ChildGetLookup.Add(typeof(Division), "getDI");
            base.ChildGetLookup.Add(typeof(LegalReference), "getBL");
            base.ChildGetLookup.Add(typeof(Approval), "getAP");
            base.ChildGetLookup.Add(typeof(ApprovelDepartment), "getAD");

            base.ChildGetAllLookup.Add(typeof(Branch), "qryBR");
            base.ChildGetAllLookup.Add(typeof(Department), "qryDE");
            base.ChildGetAllLookup.Add(typeof(Position), "qryPO");
            base.ChildGetAllLookup.Add(typeof(Division), "qryDI");
            base.ChildGetAllLookup.Add(typeof(LegalReference), "qryBL");
            base.ChildGetAllLookup.Add(typeof(Approval), "qryAP");
            base.ChildGetAllLookup.Add(typeof(ApprovelDepartment), "qryAD");


            base.ChildAddOrUpdateLookup.Add(typeof(Branch), "setBR");
            base.ChildAddOrUpdateLookup.Add(typeof(Department), "setDE");
            base.ChildAddOrUpdateLookup.Add(typeof(Position), "setPO");
            base.ChildAddOrUpdateLookup.Add(typeof(Division), "setDI");
            base.ChildAddOrUpdateLookup.Add(typeof(LegalReference), "setBL");
            base.ChildAddOrUpdateLookup.Add(typeof(Approval), "setAP");
            base.ChildAddOrUpdateLookup.Add(typeof(ApprovelDepartment), "setAD");

            ChildDeleteLookup.Add(typeof(Branch), "delBR");
            ChildDeleteLookup.Add(typeof(Department), "delDE");
            ChildDeleteLookup.Add(typeof(Position), "delPO");
            ChildDeleteLookup.Add(typeof(Division), "delDI");
            ChildDeleteLookup.Add(typeof(Approval), "delAP");
            ChildDeleteLookup.Add(typeof(ApprovelDepartment), "delAD");
            ChildDeleteLookup.Add(typeof(LegalReference), "delBL");

            

            base.GetAllMethodName = "";
            base.GetRecordMethodName = "";
            base.DeleteMethodName = "";
            base.AddOrUpdateMethodName = "";
        }

        public RecordWebServiceResponse<Department> GetDepartmentByReference(Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null)
        {
            var request = new HTTPWebServiceRequest();
            request.MethodType = "GET";
            request.URL = ServiceURL + "getDE2";
            if (Headers != null)
                request.Headers = Headers;
            if (QueryStringParams != null)
                request.QueryStringParams = QueryStringParams;

            return request.GetAsync<RecordWebServiceResponse<Department>>();
        }
    }
}
