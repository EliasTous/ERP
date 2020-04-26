using Infrastructure.Domain;
using Model.Employees.Profile;
using Model.System;
using Model.TimeAttendance;
using Services.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class OvertimeBatchRunner : ImportBatchRunner<OvertimeSetting>
    {
        EmployeeService employee;
        Dictionary<string, int> ids;
        public OvertimeBatchRunner(SystemService system, TimeAttendanceService time,EmployeeService emp):base(system,time)
        {
            employee = emp;
            ids = new Dictionary<string, int>();
            BatchStatus = new BatchOperationStatus() { classId = ClassId.TAOT, processed = 0, tableSize = 0, status = 0 };
        }
        protected override void PostProcessElements()
        {
            StringBuilder b = new StringBuilder();
            int i = 0;
            foreach (var error in errors)
            {
                b.AppendLine(error.reference + ","
                    + error.dayId + "," +
                    error.minOvertime+","+
                    error.maxOvertime + "," +
                   
                   errorMessages[i++].Replace('\r', ' ').Replace(',', ';')
                    );

            }

            string csv = b.ToString();
            string path = OutputPath + BatchStatus.classId.ToString() + ".csv";


            File.WriteAllText(path, csv.ToString());
        }
        protected override void PreProcessElement(OvertimeSetting item)
        {
            if (string.IsNullOrEmpty(item.reference))
                return;
            if (!ids.ContainsKey(item.reference))
                ids.Add(item.reference, Convert.ToInt32(GetEmployeeId(item.reference)));
            item.employeeId = ids[item.reference];
        }
        private string GetEmployeeId(string employeeRef)
        {
            EmployeeByReference req = new EmployeeByReference();
            req.Reference = employeeRef;
            RecordResponse<Employee> resp = employee.ChildGetRecord<Employee>(req);
            if (resp == null || resp.result == null)
                return employeeRef;
            else
                return resp.result.recordId;
        }
    

      
    }
}
