using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionHR.Model.Employees;
using AionHR.Services.Messaging;
using AionHR.Infrastructure.Session;
using AionHR.Model.Employees.Profile;
using AionHR.Infrastructure.Domain;
using AionHR.Infrastructure.WebService;

namespace AionHR.Services.Implementations
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;
        
        public EmployeeService(IEmployeeRepository employeeRepository, SessionHelper sessionHelper) : base(sessionHelper)
        {
            _employeeRepository = employeeRepository;
        }

        public PostResponse<Employee> AddOrUpdateEmployeeWithPhoto(EmployeeAddOrUpdateRequest req)
        {
            PostResponse<Employee> response;
            var headers = SessionHelper.GetAuthorizationHeadersForUser();
            PostWebServiceResponse webResponse = _employeeRepository.AddOrUpdateEmployeeWithImage(req.empData, req.fileName, req.imageData,headers);
            response = CreateServiceResponse<PostResponse<Employee>>(webResponse);
            response.recordId = webResponse.recordId;
            return response;
            
             
        }

        protected override dynamic GetRepository()
        {
            return _employeeRepository;
        }
    }
}
