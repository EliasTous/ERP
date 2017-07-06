using AionHR.Infrastructure.Importers;
using AionHR.Model.Company.Structure;
using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
   public class DepartmentImportingService: ImportingServiceBase<Department>, IImportaingService
    {
        public DepartmentImportingService(IImporter imp):base(imp)

        {

        }

        public override List<Department> GetItem(DataRow row)
        {
            List<Department> depts = new List<Department>();
            try
            {
                string deptRef = row[0].ToString();
                string name = row[0].ToString();

                depts.Add(new Department() { departmentRef = deptRef, name = name});



            }
            catch { }

            return depts;
        }
    }
}
