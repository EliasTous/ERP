using AionHR.Infrastructure.Importers;
using AionHR.Model.Employees.Profile;
using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
  public  class EmploymentHistoryImportingService: ImportingServiceBase<EmploymentHistory>, IImportaingService
    {
        public EmploymentHistoryImportingService(IImporter imp):base(imp)

        {

        }
        protected override List<EmploymentHistory> GetItem(DataRow row)
        {
            List<EmploymentHistory> EH = new List<EmploymentHistory>();
            try
            {
                EmploymentHistory j = new EmploymentHistory();
                j.employeeRef = row[0].ToString();
                j.statusRef = row[1].ToString().Trim('\r', '\n').Trim();
                j.date = DateTime.Parse(row[2].ToString().Trim('\r', '\n').Trim()); 
                if (!string.IsNullOrEmpty(row[3].ToString()))
                j.comment = row[3].ToString().Trim('\r', '\n').Trim(); ;



                EH.Add(j);
            }
            catch { }

            return EH;
        }
    }
}
