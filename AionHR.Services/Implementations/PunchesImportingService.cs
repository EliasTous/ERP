using AionHR.Infrastructure.Importers;
using AionHR.Model.Employees.Profile;
using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Globalization;
using AionHR.Model.Attendance;

namespace AionHR.Services.Implementations
{
   public class PunchesImportingService: ImportingServiceBase<Check>, IImportaingService
    {
        public PunchesImportingService(IImporter imp):base(imp)
        {

        }
        protected override List<Check> GetItem(DataRow row)
        {
            List<Check> result = new List<Check>();
            try
            {

                Check em = new Check();
                em.employeeRef = row[0].ToString();
                em.clockStamp = DateTime.ParseExact(row[1].ToString(), "yyyy-MM-dd HH:mm:ss", new CultureInfo("en"));




                result.Add(em);
            }
            catch (Exception exp)
            {

            }
            return result;
        }
    }
}
