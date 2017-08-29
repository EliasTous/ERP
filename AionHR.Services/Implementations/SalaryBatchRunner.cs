using AionHR.Infrastructure.Domain;
using AionHR.Infrastructure.Session;
using AionHR.Infrastructure.Tokens;
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
   public class SalaryBatchRunner: ImportBatchRunner<SalaryTree>
    {
        private Dictionary<string, int> empRef;
        private Dictionary<string, int> eds;
        
        public SalaryBatchRunner(ISessionStorage store, IEmployeeService employee, ISystemService system):base(system, employee)
        {
            empRef = new Dictionary<string, int>();
            eds = new Dictionary<string, int>();
            FillEDS(); 
            this.SessionStore = store;
            SessionHelper h = new SessionHelper(store, new APIKeyBasedTokenGenerator());


            BatchStatus = new BatchOperationStatus() { classId = ClassId.EPSA, processed = 0, tableSize = 0, status = 0 };
            errors = new List<SalaryTree>();
        }

        private void FillEDS()
        {
            ListRequest req = new ListRequest();
            ListResponse<EntitlementDeduction> branches = service.ChildGetAll<EntitlementDeduction>(req);
            if (branches.Success && branches.Items != null)
            {
                foreach (var item in branches.Items)
                {
                    this.eds.Add(item.name.Trim('\r', '\n'), Convert.ToInt32(item.recordId));
                }
            }
        }

        protected override void PostProcessElements()
        {
            StringBuilder b = new StringBuilder();
            int i = 0;
            foreach (var error in errors)
            {
                //b.AppendLine(error.reference + "," + error.firstName + "," + error.lastName + "," + error.hireDate + "," + error.caId + "," + errorMessages[i++].Replace('\r', ' ').Replace(',', ';'));
                b.AppendLine(errorMessages[i]);
            }
            string csv = b.ToString();
            string path = OutputPath + BatchStatus.classId.ToString() + ".csv";


            File.WriteAllText(path, csv.ToString());
        }

        protected override void PreProcessElement(SalaryTree item)
        {
            if (!empRef.ContainsKey(item.Basic.EmpRef))
            {
                empRef.Add(item.Basic.EmpRef, Convert.ToInt32(GetEmployeeId(item.Basic.EmpRef.ToString())));

            }

            item.Basic.employeeId = empRef[item.Basic.EmpRef];
            item.Details.ForEach(x => x.edId = eds[x.edName.Trim()]);


        }

        protected override void ProcessElement(SalaryTree item)
        {
            PostRequest<EmployeeSalary> saReq = new PostRequest<EmployeeSalary>();
            saReq.entity = item.Basic;
            PostResponse<EmployeeSalary> saResp = service.ChildAddOrUpdate<EmployeeSalary>(saReq);
            if(!saResp.Success)
            {
                errorMessages.Add(saResp.Summary);
                return;
            }
            
            item.Details.ForEach(x => x.salaryId = Convert.ToInt32(saResp.recordId));
            PostRequest<SalaryDetail[]> detReq = new PostRequest<SalaryDetail[]>();
            detReq.entity = item.Details.ToArray();
            PostResponse<SalaryDetail[]> detResp = service.ChildAddOrUpdate<SalaryDetail[]>(detReq);
            if(!detResp.Success)
            {
                errorMessages.Add(detResp.Summary);
                return;
            }

        }

        private string GetEmployeeId(string employeeRef)
        {
            EmployeeByReference req = new EmployeeByReference();
            req.Reference = employeeRef;
            RecordResponse<Employee> resp = service.ChildGetRecord<Employee>(req);
            if (resp == null || resp.result == null)
                return employeeRef;
            else
                return resp.result.recordId;
        }
    }
}
