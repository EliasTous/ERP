using AionHR.Infrastructure.Importers;
using AionHR.Model.LoadTracking;
using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
   public class LoanImportingService: ImportingServiceBase<Loan>,IImportaingService
    {
        public LoanImportingService(IImporter imp):base(imp)

        {

        }
        public override List<Loan> GetItem(DataRow row)
        {
            List<Loan> loans = new List<Loan>();
            try
            {
                string employeeRef = row[0].ToString();
                string amount = row[1].ToString();
                int bulk;
                int? ltId;
                if (int.TryParse(row[2].ToString(), out bulk))
                    ltId = bulk;
                else
                    ltId = null;
                string branch = row[3].ToString();
                string currRef = row[4].ToString();
                DateTime date = DateTime.Parse(row[7].ToString());
                DateTime effectiveDate = DateTime.Parse(row[8].ToString());
                string reason = row[5].ToString();
                
                string loanRef = row[10].ToString();
                string status = row[6].ToString();

                loans.Add(new Loan() { employeeRef = employeeRef, amount = Convert.ToDouble(amount), ltId =1 , branchId = branch.ToLower()=="null"?null:branch, currencyId=1, currencyRef = currRef, date = date, effectiveDate = effectiveDate, loanRef = loanRef, status= Convert.ToInt16(status), purpose=reason });
                
          

            }
            catch { }

            return loans;
        }
    }
}
