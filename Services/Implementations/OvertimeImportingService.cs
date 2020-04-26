using Infrastructure.Importers;
using Model.TimeAttendance;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Services.Implementations
{
  public  class OvertimeImportingService: ImportingServiceBase<OvertimeSetting>,IImportaingService
    {
        public OvertimeImportingService(IImporter importer):base(importer)
        {

        }

        protected override List<OvertimeSetting> GetItem(DataRow row)
        {
            List<OvertimeSetting> result = new List<OvertimeSetting>();
            DateTime from = DateTime.Parse(row[1].ToString());
            DateTime to = DateTime.Parse(row[2].ToString());
            TimeSpan t =to.Subtract(from);
            int i = 0;
            while(i<=t.Days)
            {
                result.Add(new OvertimeSetting() { reference = row[0].ToString(), maxOvertime = Convert.ToInt32(row[4]),minOvertime=Convert.ToInt32(row[3]), dayId = from.AddDays(i++).ToString("yyyyMMdd") });
            }

            return result;
        }
    }
}
