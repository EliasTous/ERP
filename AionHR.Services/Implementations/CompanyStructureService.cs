using AionHR.Infrastructure.Domain;
using AionHR.Infrastructure.Session;
using AionHR.Infrastructure.WebService;
using AionHR.Model.Company.Structure;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
    public class CompanyStructureService:BaseService,ICompanyStructureService
    {
        ICompanyStructureRepository _companyRepository;
        public enum CompanyStructureErrors
        {
            Company_Department_50005,
        }
        public CompanyStructureService(ICompanyStructureRepository companyStructureRepository, SessionHelper sessionHelper) : base(sessionHelper)
        {
            _companyRepository = companyStructureRepository;
           
        }
       

        protected override dynamic GetRepository()
        {
            return _companyRepository;
        }

        public RecordResponse<Department> GetDepartmentByReference(DepartmentByReference request)
        {
            RecordResponse<Department> response;
            var headers = SessionHelper.GetAuthorizationHeadersForUser();
            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            

            RecordWebServiceResponse<Department> webResponse = _companyRepository.GetDepartmentByReference(headers, request.Parameters);
            response = CreateServiceResponse<RecordResponse<Department>>(webResponse);
            if (response.Success)
                response.result = webResponse.record;

            return response;
        }
    }
}

