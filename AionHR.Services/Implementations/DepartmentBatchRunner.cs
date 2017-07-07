using AionHR.Infrastructure.Domain;
using AionHR.Infrastructure.Session;
using AionHR.Infrastructure.Tokens;
using AionHR.Model.Company.Structure;
using AionHR.Model.Employees.Profile;
using AionHR.Model.System;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
    public class DepartmentBatchRunner : ImportBatchRunner<Department>
    {
        private IEmployeeService _employeeService;
        private Dictionary<string, string> empIds;
        private Dictionary<string, string> departmentsIds;
        public DepartmentBatchRunner(ISessionStorage store, ISystemService system, ICompanyStructureService main, IEmployeeService employeeService) : base(system, main)
        {
            this.SessionStore = store;
            SessionHelper h = new SessionHelper(store, new APIKeyBasedTokenGenerator());



            BatchStatus = new BatchOperationStatus() { classId = ClassId.CSDE, processed = 0, tableSize = 0, status = 0 };
            _employeeService = employeeService;
            departmentsIds = new Dictionary<string, string>();
            empIds = new Dictionary<string, string>();

        }

        protected override void PostProcessElements()
        {
            StringBuilder b = new StringBuilder();
            int i = 0;
            foreach (var error in errors)
            {
                b.AppendLine(error.departmentRef + ","
                    + error.name + "," +

                    errorMessages[i++].Replace('\r', ' ').Replace(',', ';')

                    );

            }
            string csv = b.ToString();
            string path = OutputPath + BatchStatus.classId.ToString() + ".csv";


            File.WriteAllText(path, csv.ToString());
        }

        protected override void PreProcessElement(Department item)
        {
            if (!string.IsNullOrEmpty(item.supervisorRef))
            {
                if (!empIds.ContainsKey(item.supervisorRef))
                    empIds.Add(item.supervisorRef, GetEmployeeId(item.supervisorRef));

                if (string.IsNullOrEmpty(empIds[item.supervisorRef]))
                    item.supervisorId = null;
                else
                    item.supervisorId = Convert.ToInt32(empIds[item.supervisorRef]);
            }

            if (!string.IsNullOrEmpty(item.parentRef))
            {
                if (!departmentsIds.ContainsKey(item.parentRef))
                {
                    string id = GetParentId(item.parentRef);
                    if (id != null)
                    {
                        departmentsIds.Add(item.parentRef, id);
                        item.parentId = Convert.ToInt32(id);
                    }
                }

            }
        }

        private string GetEmployeeId(string employeeRef)
        {
            EmployeeByReference req = new EmployeeByReference();
            req.Reference = employeeRef;
            RecordResponse<Employee> resp = _employeeService.ChildGetRecord<Employee>(req);
            if (resp == null || resp.result == null)
                return null;
            else
                return resp.result.recordId;
        }
        private string GetParentId(string departmentRef)
        {
            DepartmentByReference req = new DepartmentByReference();
            req.Reference = departmentRef;
            RecordResponse<Department> resp = (service as ICompanyStructureService).GetDepartmentByReference(req);
            if (resp == null || resp.result == null)
                return null;
            else
                return resp.result.recordId;
        }

        protected override void ProcessElement(Department item)
        {
            PostRequest<Department> req = new PostRequest<Department>();

            req.entity = item;

            if (!departmentsIds.ContainsKey(item.parentRef) || string.IsNullOrEmpty(item.parentRef))
                item.parentId = null;
            else
                item.parentId = Convert.ToInt32(departmentsIds[item.parentRef]);

            PostResponse<Department> resp = service.ChildAddOrUpdate<Department>(req);

            if (!resp.Success)
            {
                errors.Add(item);
                errorMessages.Add(resp.Summary);
                return;
            }
            if (!departmentsIds.ContainsKey(item.departmentRef))
                departmentsIds.Add(item.departmentRef, resp.recordId);
        }

    }

}
