﻿using Infrastructure.Importers;
using Model.LoadTracking;
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
   public class LoanImportingService: ImportingServiceBase<Loan>,IImportaingService
    {
        public LoanImportingService(IImporter imp):base(imp)

        {

        }
        protected override List<Loan> GetItem(DataRow row)
        {
            CultureInfo cu = new CultureInfo("en");
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
                string reason = row[5].ToString();
                string status = row[6].ToString();
                DateTime date = DateTime.ParseExact(row[7].ToString(), "dd-MM-yy", cu);
                // DateTime date = DateTime.Parse(row[7].ToString());
        //        DateTime effectiveDate = DateTime.Parse(row[8].ToString());
               DateTime effectiveDate = DateTime.ParseExact(row[8].ToString(), "dd-MM-yy", cu);
                string loanRef = row[9].ToString();
               

                string ldMe= row[10].ToString(); ;
                string ldVal= row[11].ToString(); ;

                loans.Add(new Loan() { employeeRef = employeeRef, amount = Convert.ToDouble(amount), ltId =ltId, branchId = branch.ToLower()=="null"?null:branch, currencyRef = currRef, date = date, effectiveDate = effectiveDate, loanRef = loanRef, apStatus= Convert.ToInt16(status), purpose=reason ,ldMethod=Convert.ToInt16(ldMe),ldValue = ldVal});
                
          

            }
            catch { }

            return loans;
        }
    }
}
