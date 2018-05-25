using AionHR.Infrastructure.Domain;
using AionHR.Infrastructure.Session;
using AionHR.Model.Company.Structure;
using AionHR.Model.Employees.Profile;
using AionHR.Model.System;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.CompanyStructure;
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
        Dictionary<string, int> emps;
        Dictionary<string, int> divisions;
        Dictionary<string, int> departments;
        public JobInfoBatchRunner(ISessionStorage store, ISystemService system, ICompanyStructureService main, IEmployeeService employeeService) : base(system, employeeService)
        {

            companyStructure = main;
            BatchStatus = new BatchOperationStatus() { classId = ClassId.EPJI, processed = 0, tableSize = 0, status = 0 };
            errors = new List<JobInfo>();
            branches = new Dictionary<string, int>();
            positions = new Dictionary<string, int>();
            emps = new Dictionary<string, int>();
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
            if (!string.IsNullOrEmpty(item.reportToRef))
            {
                if (!emps.ContainsKey(item.reportToRef))
                {
                    int result;
                    if (int.TryParse(GetEmployeeId(item.reportToRef),out  result)){
                        emps.Add(item.reportToRef, result);
                    }
                   else {
                        return;                       
                      }
                   

                }

                item.reportToId = emps[item.reportToRef];
            }
            if (!emps.ContainsKey(item.employeeRef))
            {
                int result;
                if (int.TryParse(GetEmployeeId(item.employeeRef), out result))
                {
                    emps.Add(item.employeeRef, result);
                }
                else
                {
                    return;
                }


            }

            item.employeeId = emps[item.employeeRef];

        }
        private void FillBranches()
        {
            ListRequest req = new ListRequest();
            ListResponse<Branch> branches = companyStructure.ChildGetAll<Branch>(req);
            if (branches.Success && branches.Items != null)
            {
                branches.Items.ForEach(x => this.branches.Add(x.name.Trim('\r', '\n').ToLower(), Convert.ToInt32(x.recordId)));
            }
        }
        private void FillDepartments()
        {
            DepartmentListRequest request = new DepartmentListRequest();
            request.Filter = "";
            request.type = 0;
            ListResponse<Department> branches = companyStructure.ChildGetAll<Department>(request);
            if (branches.Success && branches.Items != null)
            {
                foreach (var item in branches.Items)
                {
                    this.departments.Add(item.name.Trim('\r','\n').ToLower(), Convert.ToInt32(item.recordId));
                }
            }
        }
        private void FillDivisions()
        {
            ListRequest req = new ListRequest();
            ListResponse<Division> branches = companyStructure.ChildGetAll<Division>(req);
            if (branches.Success && branches.Items != null)
            {
                branches.Items.ForEach(x => this.divisions.Add(x.name.Trim('\r', '\n').ToLower(), Convert.ToInt32(x.recordId)));
            }
        }
        private void FillPosition()
        {
            PositionListRequest request = new PositionListRequest();
            // request.Filter = "";
            request.SortBy = "positionRef";
            request.Size = "2000";
            request.StartAt = "0";
            request.Filter = "";
            ListResponse<Position> branches = companyStructure.ChildGetAll<Position>(request);
            if (branches.Success && branches.Items != null)
            {
                branches.Items.ForEach(x => this.positions.Add(x.name.Trim('\r', '\n').ToLower(), Convert.ToInt32(x.recordId)));
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

        protected override void ProcessElement(JobInfo item)
        {
            bool okToGo = true;
            if (item.employeeId == 0)
            {
                errorMessages.Add("Employee Not Found");
                okToGo = false;
            }
         
            else if (item.departmentId == 0)
            {
                errorMessages.Add("Department Not Found");
                okToGo = false;
            }
            else if (item.divisionId == 0 )
            {
                errorMessages.Add("Division Not Found");
                okToGo = false;
            }
            else  if (item.branchId == 0)
            {
                errorMessages.Add("Branch Not Found");
                okToGo = false;
            }
            else if (item.positionId == 0)
            {
                errorMessages.Add("Position Not Found");
                okToGo = false;
            }

            if (okToGo)
                base.ProcessElement(item);
            else
            {
                errors.Add(item);
            }
        }
    }
}
