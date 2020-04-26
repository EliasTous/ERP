using Infrastructure.Importers;
using Model.LeaveManagement;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
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
               
                //int bulk;
                //int? ltId;
                //if (int.TryParse(row[4].ToString(), out bulk))
                //    ltId = bulk;
                //else
                //    ltId = null;
                string ltRef = row[3].ToString();
                string destination = row[4].ToString();
                string justifcation = row[5].ToString();
             

                leaves.Add(new LeaveRequest() { employeeRef = employeeRef, startDate = date, endDate = effectiveDate, ltRef = ltRef, destination = string.IsNullOrEmpty(destination)?" ":destination, justification = justifcation ,apStatus=1,leaveRef=""});
                
            }
            catch { }

            return leaves;
        }

    }
}
