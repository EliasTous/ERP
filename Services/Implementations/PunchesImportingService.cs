using Infrastructure.Importers;
using Model.Employees.Profile;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Globalization;
using Model.Attendance;

namespace Services.Implementations
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
                DateTime clockStamp;
                //if (DateTime.TryParse(row[1].ToString(), out clockStamp))
                //    em.clockStamp = clockStamp;
                //else
                  em.clockStamp = DateTime.Parse(row[1].ToString());
                 //em.clockStamp = DateTime.ParseExact(row[1].ToString(), "yyyy-MM-dd HH:mm:ss", new CultureInfo("en"));
                 em.udId = row[2].ToString();
                em.serialNo = row[3].ToString();




                result.Add(em);
            }
            catch (Exception exp)
            {



                exp.Source = row[0].ToString() + ";" + row[1].ToString() + ";" + row[2].ToString() + ";" + row[3].ToString() +";" + exp.Message;
                throw exp;
            }
       
            return result;
        }
    }
}
