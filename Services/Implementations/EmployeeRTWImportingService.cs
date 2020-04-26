using Infrastructure.Importers;
using Model.Employees.Profile;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
   public class EmployeeRTWImportingService : ImportingServiceBase<EmployeeRightToWork>, IImportaingService
    {
        public EmployeeRTWImportingService(IImporter imp) : base(imp)

        {

        }

        protected override List<EmployeeRightToWork> GetItem(DataRow row)
        {
            List<EmployeeRightToWork> depts = new List<EmployeeRightToWork>();
            try
            {
                EmployeeRightToWork rw = new EmployeeRightToWork();
                rw.employeeRef = row[0].ToString();
                rw.hijriCal = row[1].ToString() == "1";
                CultureInfo cu = rw.hijriCal? new CultureInfo("ar") : new CultureInfo("en");
                rw.documentRef = row[2].ToString();
                if (!string.IsNullOrEmpty(row[3].ToString()))
                    rw.issueDate = DateTime.ParseExact(row[3].ToString(),"yyyy/MM/dd", cu);

                rw.expiryDate = DateTime.ParseExact(row[4].ToString(), "yyyy/MM/dd", cu);
                rw.dtId = Convert.ToInt32(row[5].ToString());
                rw.remarks= row[6].ToString();
                depts.Add(rw);



            }
            catch { }

            return depts;
        }
    }
}
