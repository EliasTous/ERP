using AionHR.Infrastructure.Importers;
using AionHR.Model.Employees.Profile;
using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AionHR.Services.Implementations
{
    public class SalaryImportingService : ImportingServiceBase<SalaryTree>, IImportaingService
    {

        public SalaryImportingService(IImporter m) : base(m)
        {


        }
        protected override List<SalaryTree> GetItem(DataRow row)
        {
            List<SalaryTree> result = new List<SalaryTree>();
            try
            {
                SalaryTree sT = new SalaryTree();
                sT.Basic = new EmployeeSalary();
                sT.Details = new List<SalaryDetail>();
                sT.Basic.EmpRef = row[0].ToString();
                sT.Basic.currencyRef = row[1].ToString();
                //int bulk;

                //if (int.TryParse(row[2].ToString(), out bulk))
                //    sT.Basic.scrId = bulk;
                sT.Basic.scrName= row[2].ToString();
                sT.Basic.effectiveDate = DateTime.Parse(row[3].ToString());
                sT.Basic.salaryType = Convert.ToInt16(row[4].ToString());
                sT.Basic.paymentFrequency = Convert.ToInt16(row[5].ToString());
                sT.Basic.paymentMethod = Convert.ToInt16(row[6].ToString());
                
                if (sT.Basic.paymentMethod == 2)
                {
                    sT.Basic.bankName = row[7].ToString();
                    sT.Basic.accountNumber = row[8].ToString();
                }
                sT.Basic.basicAmount = Convert.ToDouble(row[9].ToString());
                int i = 10;
                double ens = 0, deds = 0;
                while (i < row.Table.Columns.Count)
                {
                    SalaryDetail det = new SalaryDetail();
                    det.edName = row[i].ToString();
                    det.fixedAmount = Convert.ToDouble(row[i + 1].ToString());
                    det.isTaxable = row[i + 2].ToString() == "1";

                    det.type = (row[i + 3]).ToString() == "1" ? 1 : 2;

                    if (det.type == 1)
                        ens += det.fixedAmount;
                    else
                        deds += det.fixedAmount;

                    det.includeInTotal = true;
                    sT.Details.Add(det);
                    i += 4;
                }
                sT.Basic.eAmount = ens;
                sT.Basic.dAmount = deds;
                sT.Basic.finalAmount = sT.Basic.basicAmount + sT.Basic.eAmount - sT.Basic.dAmount;
                result.Add(sT);
                return result;
            }
            catch (Exception exp)
            {

            }
            return result;
        }
    }
}
