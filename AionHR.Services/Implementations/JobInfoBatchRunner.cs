using AionHR.Infrastructure.Domain;
using AionHR.Infrastructure.Session;
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
    public class JobInfoBatchRunner : ImportBatchRunner<JobInfo>
    {
        ICompanyStructureService companyStructure;
        Dictionary<string, int> branches;
        Dictionary<string, int> positions;
        Dictionary<int, int> emps;
        Dictionary<string, int> divisions;
        Dictionary<string, int> departments;
        public JobInfoBatchRunner(ISessionStorage store, ISystemService system, ICompanyStructureService main, IEmployeeService employeeService) : base(system, employeeService)
        {

            companyStructure = main;
            BatchStatus = new BatchOperationStatus() { classId = ClassId.EPJI, processed = 0, tableSize = 0, status = 0 };
            errors = new List<JobInfo>();
            branches = new Dictionary<string, int>();
            positions = new Dictionary<string, int>();
            emps = new Dictionary<int, int>();
            divisions = new Dictionary<string, int>();
            departments = new Dictionary<string, int>();
            FillDepartments();
            FillDivisions();
            FillBranches();
            FillPosition();

        }
        protected override void PostProcessElements()
        {
            StringBuilder b = new StringBuilder();
            int i = 0;
            foreach (var error in errors)
            {
                b.AppendLine(error.employeeId + ","+
                    error.departmentName + "," +
                     error.branchName + "," +
                    
                    error.positionName + "," +
                    error.divisionName + "," +
                    error.reportToId + "," +
                    error.date + "," +
                    

                   errorMessages[i++].Replace('\r', ' ').Replace(',', ';')
                    );

            }

            string csv = b.ToString();
            string path = OutputPath + BatchStatus.classId.ToString() + ".csv";


            File.WriteAllText(path, csv.ToString());
        }

        protected override void PreProcessElement(JobInfo item)
        {
            if (branches.ContainsKey(item.branchName))
                item.branchId = branches[item.branchName];
            if (departments.ContainsKey(item.departmentName))
                item.departmentId = departments[item.departmentName];
            if (positions.ContainsKey(item.positionName))
                item.positionId = positions[item.positionName];
            if (divisions.ContainsKey(item.divisionName))
                item.divisionId = divisions[item.divisionName];
            if (item.reportToId.HasValue)
            {
                if (!emps.ContainsKey(item.reportToId.Value))
                {
                    emps.Add(item.reportToId.Value, Convert.ToInt32(GetEmployeeId(item.reportToId.ToString())));

                }

                item.reportToId = emps[item.reportToId.Value];
            }
            if (!emps.ContainsKey(item.employeeId))
            {
                emps.Add(item.employeeId, Convert.ToInt32(GetEmployeeId(item.employeeId.ToString())));

            }

            item.employeeId = emps[item.employeeId];

        }
        private void FillBranches()
        {
            ListRequest req = new ListRequest();
            ListResponse<Branch> branches = companyStructure.ChildGetAll<Branch>(req);
            if (branches.Success && branches.Items != null)
            {
                branches.Items.ForEach(x => this.branches.Add(x.name.Trim('\r', '\n'), Convert.ToInt32(x.recordId)));
            }
        }
        private void FillDepartments()
        {
            ListRequest req = new ListRequest();
            ListResponse<Department> branches = companyStructure.ChildGetAll<Department>(req);
            if (branches.Success && branches.Items != null)
            {
                foreach (var item in branches.Items)
                {
                    this.departments.Add(item.name.Trim('\r','\n'), Convert.ToInt32(item.recordId));
                }
            }
        }
        private void FillDivisions()
        {
            ListRequest req = new ListRequest();
            ListResponse<Division> branches = companyStructure.ChildGetAll<Division>(req);
            if (branches.Success && branches.Items != null)
            {
                branches.Items.ForEach(x => this.divisions.Add(x.name.Trim('\r', '\n'), Convert.ToInt32(x.recordId)));
            }
        }
        private void FillPosition()
        {
            ListRequest req = new ListRequest();
            ListResponse<Position> branches = companyStructure.ChildGetAll<Position>(req);
            if (branches.Success && branches.Items != null)
            {
                branches.Items.ForEach(x => this.positions.Add(x.name.Trim('\r', '\n'), Convert.ToInt32(x.recordId)));
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
