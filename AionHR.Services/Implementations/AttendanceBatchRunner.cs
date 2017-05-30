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
    public class AttendanceBatchRunner : BatchRunner<AttendanceShift>

    {
        ITimeAttendanceService _timeAttendanceService;
       
        IEmployeeService _employeeService;

        private List<AttendanceShift> errors;

        public AttendanceBatchRunner(ISessionStorage store, ITimeAttendanceService attendance, ISystemService system, IEmployeeService employee):base(system)
        {
            this.SessionStore = store;
            SessionHelper h = new SessionHelper(store, new APIKeyBasedTokenGenerator());
            this._timeAttendanceService = attendance;
            
            this._employeeService = employee;
            BatchStatus = new BatchOperationStatus() { classId = ClassId.TAAS, processed = 0, tableSize = 0, status = 0 };
            errors = new List<AttendanceShift>();
        }
        protected override void PreProcessElements()
        {
            Dictionary<string, string> ids = new Dictionary<string, string>();
            foreach (var item in Items)
            {
                if (string.IsNullOrEmpty(item.employeeRef))
                    continue;
                if (!ids.ContainsKey(item.employeeRef))
                    ids.Add(item.employeeRef, GetEmployeeId(item.employeeRef));
                item.employeeId = ids[item.employeeRef];


            }
        }

        protected override void PostProcessElements()
        {
            StringBuilder b = new StringBuilder();
            foreach (var error in errors)
            {
                b.Append(error.employeeRef + "," + error.dayId + "," + error.checkIn + "," + error.checkOut + "\n");

            }
            string csv = b.ToString();
            string path = "C:/BatchOutput/" + BatchStatus.classId.ToString() + ".csv";
          
            
            File.WriteAllText(path, csv.ToString());
        }
        protected override void ProcessElement(AttendanceShift item)
        {
            PostRequest<AttendanceShift> req = new PostRequest<AttendanceShift>();
            req.entity = item;
            //Thread.Sleep(10);
            PostResponse<AttendanceShift> resp = _timeAttendanceService.ChildAddOrUpdate<AttendanceShift>(req);
            if (!resp.Success)
            {
                errors.Add(item);
            }
        }
        private string GetEmployeeId( string employeeRef)
        {
            EmployeeByReference req = new EmployeeByReference();
            req.Reference = employeeRef;
            RecordResponse<Employee> resp = _employeeService.ChildGetRecord<Employee>(req);
            if (resp == null || resp.result == null)
                return "";
            else
                return resp.result.recordId;
        }


    }
}
