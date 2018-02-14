using AionHR.Infrastructure.Domain;
using AionHR.Infrastructure.Session;
using AionHR.Infrastructure.Tokens;
using AionHR.Model.Attendance;
using AionHR.Model.Employees.Profile;
using AionHR.Model.System;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
   public class EmployeeBatchRunner:ImportBatchRunner<Employee>
    {
        ITimeAttendanceService timeAttendance;
        Dictionary<string, int> caId;
        Dictionary<string, int> nationalityId;
        public EmployeeBatchRunner(ISessionStorage store, IEmployeeService employee, ISystemService system,ITimeAttendanceService timeAttendance) :base(system, employee)
        {
            this.timeAttendance = timeAttendance;
            this.SessionStore = store;
            SessionHelper h = new SessionHelper(store, new APIKeyBasedTokenGenerator());

            
            BatchStatus = new BatchOperationStatus() { classId = ClassId.EPEM, processed = 0, tableSize = 0, status = 0 };
            errors = new List<Employee>();
            caId = new Dictionary<string, int>();
            nationalityId = new Dictionary<string, int>();
            FillCaId();
            FillNationalityId();
        }

        protected override void PostProcessElements()
        {
            StringBuilder b = new StringBuilder();
            int i = 0;
            foreach (var error in errors)
            {
              b.AppendLine(error.reference + "," + error.firstName + "," + error.lastName+ "," + error.caId + "," + error.hireDate + "," + error.idRef + "," +error.nationalityName + "," +error.gender + "," +error.religion + "," +error.birthDate + "," +error.mobile + "," + errorMessages[i++].Replace('\r', ' ').Replace(',', ';'));

            }
            string csv = b.ToString();
            string path = OutputPath + BatchStatus.classId.ToString() + ".csv";


            File.WriteAllText(path, csv.ToString());
        }

        protected override void PreProcessElement(Employee item)
        {

            if (caId.ContainsKey(item.caName))
                item.caId = caId[item.caName];
            if (nationalityId.ContainsKey(item.nationalityName))
                item.nationalityId = nationalityId[item.nationalityName];
            item.bdHijriCal = false;
        }

        protected override void ProcessElement(Employee item)
        {
            PostRequest<Employee> req = new PostRequest<Employee>();
            req.entity = item;

            PostResponse<Employee> resp = service.AddOrUpdate<Employee>(req);
            if (!resp.Success)
            {
                errors.Add(item);
                errorMessages.Add(resp.Summary);
            }
        }
        private void FillCaId()
        {
            ListRequest caRequest = new ListRequest();
            ListResponse<WorkingCalendar> resp = timeAttendance.ChildGetAll<WorkingCalendar>(caRequest);
            if (resp.Success && resp.Items != null)
            {
                resp.Items.ForEach(x => this.caId.Add(x.name,Convert.ToInt32( x.recordId )));
            }
        }
        private void FillNationalityId()
        {
            ListRequest nationalityRequest = new ListRequest();
            ListResponse<Nationality> resp = _systemService.ChildGetAll<Nationality>(nationalityRequest);
            if (resp.Success && resp.Items != null)
            {
                resp.Items.ForEach(x => this.nationalityId.Add(x.name, Convert.ToInt32(x.recordId)));
            }

        }

    }
}
