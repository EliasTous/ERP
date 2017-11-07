using AionHR.Infrastructure.Importers;
using AionHR.Model.Company.Structure;
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
    public class JobInfoImportingService : ImportingServiceBase<JobInfo>, IImportaingService
    {
        public JobInfoImportingService(IImporter imp):base(imp)

        {

        }
        protected override List<JobInfo> GetItem(DataRow row)
        {
            List<JobInfo> depts = new List<JobInfo>();
            try
            {
                JobInfo j = new JobInfo();
                j.employeeRef = row[0].ToString();
                j.departmentName = row[1].ToString().Trim('\r', '\n').Trim();
                j.branchName = row[2].ToString().Trim('\r', '\n').Trim(); ;
                j.positionName = row[3].ToString().Trim('\r', '\n').Trim(); ;
                j.divisionName = row[4].ToString().Trim('\r', '\n').Trim(); ;

                if (!string.IsNullOrEmpty(row[5].ToString()))
                    j.reportToRef = row[5].ToString();
                else
                    j.reportToId = null;
                j.date = DateTime.Parse(row[6].ToString());
                depts.Add(j);
            }
            catch { }

            return depts;
        }
    }
}
