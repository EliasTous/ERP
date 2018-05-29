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
   public class PunchesBatchRunner: ImportBatchRunner<Check>
    {
        ITimeAttendanceService timeAttendance;
     
        public PunchesBatchRunner(ISessionStorage store, IEmployeeService employee, ISystemService system, ITimeAttendanceService timeAttendance) :base(system, employee)
        {
            this.timeAttendance = timeAttendance;
            this.SessionStore = store;
            SessionHelper h = new SessionHelper(store, new APIKeyBasedTokenGenerator());


            BatchStatus = new BatchOperationStatus() { classId = ClassId.TACH, processed = 0, tableSize = 0, status = 0 };
            errors = new List<Check>();
          
            //FillCaId();
           
        }

        protected override void PostProcessElements()
        {
            StringBuilder b = new StringBuilder();
            int i = 0;
            foreach (var error in errors)
            {
                b.AppendLine(error.employeeRef + "," + error.clockStamp + "," + errorMessages[i++].Replace('\r', ' ').Replace(',', ';'));

            }
            string csv = b.ToString();
            string path = OutputPath + BatchStatus.classId.ToString() + ".csv";


            File.WriteAllText(path, csv.ToString());
        }

        protected override void PreProcessElement(Check item)
        {

            item.authMode = 1;
            item.lon = 0;
            item.lat = 0;
            item.hasImage = 0;
          
        }

        protected override void ProcessElement(Check item)
        {
            PostRequest<Check> req = new PostRequest<Check>();
            req.entity = item;

            PostResponse<Check> resp =timeAttendance.ChildAddOrUpdate<Check>(req);
            if (!resp.Success)
            {
                errors.Add(item);
                errorMessages.Add(resp.Summary);
            }
        }
        //private void FillCaId()
        //{
        //    ListRequest caRequest = new ListRequest();
        //    ListResponse<WorkingCalendar> resp = timeAttendance.ChildGetAll<WorkingCalendar>(caRequest);
        //    if (resp.Success && resp.Items != null)
        //    {
        //        resp.Items.ForEach(x => this.caId.Add(x.name,Convert.ToInt32( x.recordId )));
        //    }
        //}
        

    }
}
