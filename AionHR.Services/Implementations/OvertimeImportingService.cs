﻿using AionHR.Infrastructure.Importers;
using AionHR.Model.TimeAttendance;
using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AionHR.Services.Implementations
{
  public  class OvertimeImportingService: ImportingServiceBase<OvertimeSetting>,IImportaingService
    {
        public OvertimeImportingService(IImporter importer):base(importer)
        {

        }

        public override List<OvertimeSetting> GetItem(DataRow row)
        {
            List<OvertimeSetting> result = new List<OvertimeSetting>();
            DateTime from = DateTime.Parse(row[1].ToString());
            DateTime to = DateTime.Parse(row[2].ToString());
            TimeSpan t =to.Subtract(from);
            int i = 0;
            while(i<=t.Days)
            {
                result.Add(new OvertimeSetting() { reference = row[0].ToString(), maxOvertime = Convert.ToInt32(row[3]), dayId = from.AddDays(i++).ToString("yyyyMMdd") });
            }

            return result;
        }
    }
}
