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


        protected override List<AttendanceShift> GetItem(DataRow row)
        {
            List<AttendanceShift> shifts = new List<AttendanceShift>();
            try
            {
                string employeeRef = row[0].ToString();
                DateTime s;
                string dayId = "";
                if (DateTime.TryParse(row[1].ToString(), out s))
                    dayId = s.ToString("yyyyMMdd");
                else
                    dayId = row[1].ToString();
                for (int i = 0; i < (row.Table.Columns.Count - 2); i += 2)
                {
                    string cIn = row[2 + i].ToString();
                    string cOut = row[2 + i + 1].ToString();
                    if (string.IsNullOrEmpty(cIn) || string.IsNullOrEmpty(cOut))
                        continue;
                    if (cIn.Length < 5)
                    {
                        cIn = PadTime(cIn);
                    }
                    if (cOut.Length < 5)
                    {
                        cOut = PadTime(cOut);
                    }
                    if (cIn.Length == cOut.Length && cOut.Length== 5)
                        shifts.Add(new AttendanceShift() { checkIn = cIn, checkOut = cOut, dayId = dayId, employeeRef = employeeRef });
                }


            }
            catch { }

            return shifts;
        }

        private string PadTime(string cOut)
        {
            string[] parts = cOut.Split(':');
            if (parts.Length < 2)
                return cOut;
            return parts[0].PadLeft(2, '0') + ":" + parts[1].PadLeft(2, '0');
        }
    }
}
