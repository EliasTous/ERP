﻿using Infrastructure.Importers;
using Model.Company.Structure;
using Model.Payroll;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
  public  class PayCodeImportingService: ImportingServiceBase<PayCode>, IImportaingService
    {
        public PayCodeImportingService(IImporter imp):base(imp)

        {

        }

        protected override List<PayCode> GetItem(DataRow row)
        {
            List<PayCode> payCodes = new List<PayCode>();
            try
            {
                string payCode = row[0].ToString();
                string name = row[1].ToString();

                payCodes.Add(new PayCode() { payCode=payCode , name = name });



            }
            catch { }

            return payCodes;
        }
    }
}
