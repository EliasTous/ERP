using Infrastructure.Importers;
using Model.Company.Structure;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
   public class DepartmentImportingService: ImportingServiceBase<Department>, IImportaingService
    {
        public DepartmentImportingService(IImporter imp):base(imp)

        {

        }

        protected override List<Department> GetItem(DataRow row)
        {
            List<Department> depts = new List<Department>();
            try
            {
                string deptRef = row[0].ToString();
                string name = row[1].ToString();
                string parentRef = row[2].ToString();
                string supervisorRef = row[3].ToString();
                depts.Add(new Department() { departmentRef = deptRef, name = name, parentRef=parentRef, supervisorRef=supervisorRef });



            }
            catch { }

            return depts;
        }
    }
}
