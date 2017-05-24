using AionHR.Infrastructure.Importers;
using AionHR.Model.Attendance;
using AionHR.Model.Employees.Profile;
using AionHR.Model.TimeAttendance;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
    public class AttendanceImportingService : ImportingServiceBase<AttendanceShift>, IImportaingService
    {
        IEmployeeService service;
        public AttendanceImportingService(IImporter m, IEmployeeService service) : base(m)
        {
            dict = new Dictionary<string, string>();
            this.service = service;
        }
        Dictionary<string, string> dict;


        public override AttendanceShift GetItem(DataRow row)
        {
            AttendanceShift c = new AttendanceShift();
            c.dayId = row[0].ToString();
            c.checkIn = row[1].ToString();
            c.checkOut = row[2].ToString();
            //if (!dict.ContainsKey(row[3].ToString()))
            //{

            //    EmployeeByReference req2 = new EmployeeByReference();
            //    req2.Reference = row[3].ToString();
            //    RecordResponse<Employee> resp = service.ChildGetRecord<Employee>(req2);
            //    if (!resp.Success || resp.result == null)
            //        dict.Add(row[3].ToString(), "0");
            //    else
            //        dict.Add(row[3].ToString(), resp.result.recordId);
            //}
            //c.employeeId = dict[row[3].ToString()].ToString();
            c.employeeRef = row[3].ToString();

            return c;
        }

    }
}
