using Infrastructure.Domain;
using Infrastructure.Session;
using Model.Company.Structure;
using Model.Employees;
using Model.Employees.Profile;
using Model.System;
using Services.Interfaces;
using Services.Messaging;
using Services.Messaging.CompanyStructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
   public class EmploymentHistoryBatchRunner : ImportBatchRunner<EmploymentHistory>
    {


        IEmployeeService _employeeService;
         Dictionary<string, int> emps;
        Dictionary<string, int> status;


        public EmploymentHistoryBatchRunner(ISessionStorage store, ISystemService system, IEmployeeService employeeService) : base(system, employeeService)
        {
            _employeeService = employeeService;

            BatchStatus = new BatchOperationStatus() { classId = ClassId.EPST, processed = 0, tableSize = 0, status = 0 };
            errors = new List<EmploymentHistory>();
         
           
            emps = new Dictionary<string, int>();
            status = new Dictionary<string, int>();
           

           

        }
        protected override void PostProcessElements()
        {
            StringBuilder b = new StringBuilder();
            int i = 0;
            foreach (var error in errors)
            {
                b.AppendLine(error.employeeId + "," +
                    error.statusId + "," +
                     error.date + "," +

                    error.comment + "," +
                 
                   
                   


                   errorMessages[i++].Replace('\r', ' ').Replace(',', ';')
                    );

            }

            string csv = b.ToString();
            string path = OutputPath + BatchStatus.classId.ToString() + ".csv";


            File.WriteAllText(path, csv.ToString());
        }

        protected override void PreProcessElement(EmploymentHistory item)
        {

            try
            {
                if (!string.IsNullOrEmpty(item.employeeRef))
                {
                    if (!emps.ContainsKey(item.employeeRef))
                    {
                        emps.Add(item.employeeRef, Convert.ToInt32(GetEmployeeId(item.employeeRef)));

                    }

                    item.employeeId = emps[item.employeeRef];
                }
                //if (!emps.ContainsKey(item.employeeRef))
                //{
                //    emps.Add(item.employeeRef, Convert.ToInt32(GetEmployeeId(item.employeeRef)));

                //}

                //item.employeeId = emps[item.employeeRef];

                if (!string.IsNullOrEmpty(item.statusRef))
                {
                    if (!status.ContainsKey(item.statusRef))
                    {
                        status.Add(item.statusRef, Convert.ToInt32(GetStatusId(item.statusRef)));

                    }

                    item.statusId = status[item.statusRef];
                }
                //if (!status.ContainsKey(item.statusRef))
                //{
                //    status.Add(item.statusRef, Convert.ToInt32(GetStatusId(item.statusRef)));

                //}

                //item.statusId = emps[item.statusRef];

                //if (status.ContainsKey(item.statusRef))
                //    item.statusId = status[item.statusRef];
            }
            catch(Exception e)
            { }
            

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
        private string GetStatusId(string statusRef)
        {
            StatusByReference req = new StatusByReference();
            req.Reference = statusRef;
            RecordResponse<EmploymentStatusByReferance> resp = service.ChildGetRecord<EmploymentStatusByReferance>(req);
            if (resp == null || resp.result == null)
                return statusRef;
            else
                return resp.result.recordId;
        }
        //private void FillStatus()
        //{
        //    ListRequest request = new ListRequest();

        //    ListResponse<EmploymentStatus> routers = _employeeService.ChildGetAll<EmploymentStatus>(request);
        //    if (routers.Success && routers.Items != null)
        //    {
        //        routers.Items.ForEach(x => this.status.Add(x.esRef, Convert.ToInt32(x.recordId)));
        //    }
        //}
        protected override void ProcessElement(EmploymentHistory item)
        {
            bool okToGo = true;
            if (item.employeeId == 0)
            {
                errorMessages.Add("Employee Not Found");
                okToGo = false;
            }
            else if (item.statusId == 0)
            {
                errorMessages.Add("Status Not Found");
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
