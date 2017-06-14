using AionHR.Infrastructure.Domain;
using AionHR.Infrastructure.Session;
using AionHR.Infrastructure.Tokens;
using AionHR.Model.Employees.Profile;
using AionHR.Model.System;
using AionHR.Model.TimeAttendance;
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
    public class AttendanceBatchRunner : ImportBatchRunner<AttendanceShift>

    {
 
       
        IEmployeeService _employeeService;
        Dictionary<string, string> ids;


        public AttendanceBatchRunner(ISessionStorage store, ITimeAttendanceService attendance, ISystemService system, IEmployeeService employee):base(system,attendance)
        {
            this.SessionStore = store;
            SessionHelper h = new SessionHelper(store, new APIKeyBasedTokenGenerator());

            ids = new Dictionary<string, string>();
            this._employeeService = employee;
            BatchStatus = new BatchOperationStatus() { classId = ClassId.TAAS, processed = 0, tableSize = 0, status = 0 };
            errors = new List<AttendanceShift>();
        }
       

        protected override void PreProcessElement(AttendanceShift item)
        {
            if (string.IsNullOrEmpty(item.employeeRef))
                return;
            if (!ids.ContainsKey(item.employeeRef))
                ids.Add(item.employeeRef, GetEmployeeId(item.employeeRef));
            item.employeeId = ids[item.employeeRef];
        }
        protected override void PostProcessElements()
        {
            StringBuilder b = new StringBuilder();
            int i = 0;
            foreach (var error in  errors)
            {
                b.AppendLine(error.employeeRef + "," + error.dayId + "," + error.checkIn + "," + error.checkOut + ","+errorMessages[i++].Replace('\r',' ').Replace(',',';') );

            }
            string csv = b.ToString();
            string path = OutputPath + BatchStatus.classId.ToString() + ".csv";
          
            
            File.WriteAllText(path, csv.ToString());
        }
      
        private string GetEmployeeId( string employeeRef)
        {
            EmployeeByReference req = new EmployeeByReference();
            req.Reference = employeeRef;
            RecordResponse<Employee> resp = _employeeService.ChildGetRecord<Employee>(req);
            if (resp == null || resp.result == null)
                return employeeRef;
            else
                return resp.result.recordId;
        }


    }
}
