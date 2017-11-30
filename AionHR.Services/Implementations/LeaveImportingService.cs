using AionHR.Infrastructure.Importers;
using AionHR.Model.LeaveManagement;
using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
    public class LeaveImportingService : ImportingServiceBase<LeaveRequest>, IImportaingService
    {
        public LeaveImportingService(IImporter imp):base(imp)
        {

        }

        protected override List<LeaveRequest> GetItem(DataRow row)
        {
            List<LeaveRequest> leaves = new List<LeaveRequest>();
            try
            {
                string employeeRef = row[0].ToString();
                
                DateTime date = DateTime.Parse(row[1].ToString());
                DateTime effectiveDate = DateTime.Parse(row[2].ToString());
                int bulk;
                int? ltId;
                if (int.TryParse(row[3].ToString(), out bulk))
                    ltId = bulk;
                else
                    ltId = null;
                string destination = row[4].ToString();
                string justifcation = row[5].ToString();
                string status = row[6].ToString();

                leaves.Add(new LeaveRequest() { employeeRef = employeeRef, startDate = date, endDate = effectiveDate, ltId=ltId, destination = string.IsNullOrEmpty(destination)?" ":destination, justification = justifcation, status = Convert.ToInt16(status) });
                
            }
            catch { }

            return leaves;
        }

    }
}
