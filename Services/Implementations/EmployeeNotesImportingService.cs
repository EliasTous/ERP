using Infrastructure.Importers;
using Model.Employees.Profile;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class EmployeeNotesImportingService : ImportingServiceBase<EmployeeNote>, IImportaingService
    {
        IEmployeeService service;
        public EmployeeNotesImportingService(IImporter m) : base(m)
        {
            dict = new Dictionary<string, string>();
            this.service = service;
        }
        Dictionary<string, string> dict;


        protected override List<EmployeeNote> GetItem(DataRow row)
        {
            List<EmployeeNote> notes = new List<EmployeeNote>();
            try
            {
                string employeeRef = row[0].ToString();
                string note = row[1].ToString();
                string username = row[2].ToString();


                notes.Add(new EmployeeNote() { employeeRef = employeeRef, note = note, userName = username, date = DateTime.Now });



            }
            catch { }

            return notes;
        }


    }
}
