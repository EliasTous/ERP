﻿using AionHR.Infrastructure.Domain;
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
   public class EmployeeRTWBatchRunner: ImportBatchRunner<EmployeeRightToWork>
    {
        Dictionary<string, int> ids;
        public EmployeeRTWBatchRunner(SystemService system, IEmployeeService time):base(system,time)
        {
            ids = new Dictionary<string, int>();
            BatchStatus = new BatchOperationStatus() { classId = ClassId.EPRW, processed = 0, tableSize = 0, status = 0 };
        }
        protected override void PostProcessElements()
        {
            StringBuilder b = new StringBuilder();
            int i = 0;
            foreach (var error in errors)
            {
                b.AppendLine(error.employeeRef + ","
                    + error.hijriCal.ToString() + "," 
                    + error.documentRef + "," +
                    (error.issueDate.HasValue?error.issueDate.Value.ToShortDateString():" ") + "," +
                    error.expiryDate.ToShortDateString() + "," +
                     error.dtId+ "," +
                   errorMessages[i++].Replace('\r', ' ').Replace(',', ';')
                    );

            }

            string csv = b.ToString();
            string path = OutputPath + BatchStatus.classId.ToString() + ".csv";


            File.WriteAllText(path, csv.ToString());
        }
        protected override void PreProcessElement(EmployeeRightToWork item)
        {
            if (string.IsNullOrEmpty(item.employeeRef))
                return;
            if (!ids.ContainsKey(item.employeeRef))
                ids.Add(item.employeeRef, Convert.ToInt32(GetEmployeeId(item.employeeRef)));
            item.employeeId = ids[item.employeeRef];
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
