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
                DateTime returnDate= DateTime.Parse(row[3].ToString());
                //int bulk;
                //int? ltId;
                //if (int.TryParse(row[4].ToString(), out bulk))
                //    ltId = bulk;
                //else
                //    ltId = null;
                string ltRef = row[4].ToString();
                string destination = row[5].ToString();
                string justifcation = row[6].ToString();
                string status = row[7].ToString();

                leaves.Add(new LeaveRequest() { employeeRef = employeeRef, startDate = date, endDate = effectiveDate,returnDate=returnDate, ltRef = ltRef, destination = string.IsNullOrEmpty(destination)?" ":destination, justification = justifcation, status = Convert.ToInt16(status) });
                
            }
            catch { }

            return leaves;
        }

    }
}
