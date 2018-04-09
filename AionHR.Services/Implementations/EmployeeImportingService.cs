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
    public class EmployeeImportingService : ImportingServiceBase<Employee>, IImportaingService
    {
        private char nameDelimiter;
        public EmployeeImportingService(IImporter m, char nameDelimiter) : base(m)
        {
            this.nameDelimiter = nameDelimiter;

        }
        protected override List<Employee> GetItem(DataRow row)
        {
            List<Employee> result = new List<Employee>();
            try
            {
                Employee em = new Employee();
                em.reference = row[0].ToString();
                em.name = new EmployeeName();
                em.name.firstName = row[1].ToString();
                em.name.middleName= row[2].ToString();
                em.name.lastName = row[3].ToString();
                em.name.familyName = row[4].ToString();
                //var parts = row[1].ToString().Split(nameDelimiter);
                //em.name.firstName = row[1].ToString().Split(nameDelimiter)[0];
                //if (parts.Count() < 4)
                //{
                //    em.name.lastName = em.name.firstName;
                //}
                //else
                //{
                //    em.name.lastName = row[1].ToString().Split(nameDelimiter)[3];
                //    em.name.middleName = row[1].ToString().Split(nameDelimiter)[1];
                //    em.name.familyName = row[1].ToString().Split(nameDelimiter)[2];
                //}
                em.name.reference = row[0].ToString();
                //int ca;
                //if (int.TryParse(row[2].ToString(), out ca))
                //    em.caId = ca;
                //else
                //    em.caId = null;
            

                em.hireDate = DateTime.Parse(row[5].ToString());
                em.idRef= row[6].ToString();
                em.nationalityName = row[7].ToString();
                em.gender = Convert.ToInt16(row[8]);
                em.religion = Convert.ToInt16(row[9]);
                em.birthDate = DateTime.Parse(row[10].ToString());
                em.mobile = row[11].ToString();


                result.Add(em);
            }
            catch (Exception exp)
            {

            }
            return result;
        }
    }
}
