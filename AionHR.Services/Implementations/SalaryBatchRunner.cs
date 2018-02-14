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
    public class SalaryBatchRunner : ImportBatchRunner<SalaryTree>
    {
        IEmployeeService _employeeService;
        private Dictionary<string, int> empRef;
        private Dictionary<string, int> ents;
        private Dictionary<string, int> deds;
        private Dictionary<string, int> currencyId;
        private Dictionary<string, string> scrId;


        public SalaryBatchRunner(ISessionStorage store, IEmployeeService employee, ISystemService system,IEmployeeService _employeeService) : base(system, employee)
        {
            this._employeeService = _employeeService;
            empRef = new Dictionary<string, int>();
            ents = new Dictionary<string, int>();
            deds = new Dictionary<string, int>();
            currencyId = new Dictionary<string, int>();
            scrId = new Dictionary<string, string>();
            FillEDS();
            fillcurrency();
            fillscrId();
            //   this.SessionStore = store;
            //   SessionHelper h = new SessionHelper(store, new APIKeyBasedTokenGenerator());


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
                    if (item.type == 1)
                        this.ents.Add(item.name.Trim('\r', '\n'), Convert.ToInt32(item.recordId));
                    else
                        this.deds.Add(item.name.Trim('\r', '\n'), Convert.ToInt32(item.recordId));
                }
            }
        }
        private void fillcurrency()
        {

            ListRequest branchesRequest = new ListRequest();
            ListResponse<Currency> resp = _systemService.ChildGetAll<Currency>(branchesRequest);
            if (resp.Success && resp.Items != null)
            {
                resp.Items.ForEach(x => this.currencyId.Add(x.reference, Convert.ToInt32(x.recordId)));
            }

        }
        private void fillscrId()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<SalaryChangeReason> resp = _employeeService.ChildGetAll<SalaryChangeReason>(branchesRequest);
           
            if (resp.Success && resp.Items != null)
            {
                resp.Items.ForEach(x => this.scrId.Add(x.name, x.recordId));
            }

        }


        protected override void PostProcessElements()
        {
            StringBuilder b = new StringBuilder();
            int i = 0;
            foreach (var error in errors)
            {
                b.AppendLine(error.Basic.employeeId.ToString()+","+error.Basic.EmpRef+","+errorMessages[i++].Replace('\r', ' ').Replace(',', ';'));
             
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
            item.Details.ForEach(x =>
            {
                if(ents.ContainsKey(x.edName))
                {
                    x.edId = ents[x.edName];
                    x.type = 1;
                }
                else if(deds.ContainsKey(x.edName))
                {
                    x.edId = deds[x.edName];
                    x.type = 2;

                }
            });
            if (currencyId.ContainsKey(item.Basic.currencyRef))
                item.Basic.currencyId = currencyId[item.Basic.currencyRef];
            if (scrId.ContainsKey(item.Basic.scrName))
                item.Basic.scrId =Convert.ToInt32( scrId[item.Basic.scrName]);


        }

        protected override void ProcessElement(SalaryTree item)
        {
            PostRequest<EmployeeSalary> saReq = new PostRequest<EmployeeSalary>();
            item.Basic.eAmount = item.Details.Where(x => x.type == 1).Sum(x => x.fixedAmount);
            item.Basic.dAmount = item.Details.Where(x => x.type == 2).Sum(x => x.fixedAmount);
            saReq.entity = item.Basic;
            saReq.entity.recordId = "0";
            PostResponse<EmployeeSalary> saResp = service.ChildAddOrUpdate<EmployeeSalary>(saReq);
            if (!saResp.Success)
            {
                errorMessages.Add(saResp.Summary);
                errors.Add(item);
                return;
            }
            short i = 0;
            foreach (var detail in item.Details)
            {
                detail.salaryId = Convert.ToInt32(saResp.recordId);
                detail.seqNo = i++;
            }
            
            PostRequest<SalaryDetail[]> detReq = new PostRequest<SalaryDetail[]>();
            detReq.entity = item.Details.ToArray();
            PostResponse<SalaryDetail[]> detResp = service.ChildAddOrUpdate<SalaryDetail[]>(detReq);
            if (!detResp.Success)
            {
                errorMessages.Add(detResp.Summary);
                errors.Add(item);
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
