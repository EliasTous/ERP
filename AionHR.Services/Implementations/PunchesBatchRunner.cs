using AionHR.Infrastructure.Domain;
using AionHR.Infrastructure.Session;
using AionHR.Infrastructure.Tokens;
using AionHR.Model.Attendance;
using AionHR.Model.Employees.Profile;
using AionHR.Model.System;
using AionHR.Model.TimeAttendance;
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
        Dictionary<string, int> udId;
        public PunchesBatchRunner(ISessionStorage store, IEmployeeService employee, ISystemService system, ITimeAttendanceService timeAttendance) :base(system, employee)
        {
            this.timeAttendance = timeAttendance;
            this.SessionStore = store;
            SessionHelper h = new SessionHelper(store, new APIKeyBasedTokenGenerator());


            BatchStatus = new BatchOperationStatus() { classId = ClassId.TACH, processed = 0, tableSize = 0, status = 0 };
            errors = new List<Check>();
            udId = new Dictionary<string, int>();
            FillUdId();

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
            if (udId.ContainsKey(item.udIdRef))
                item.udId = udId[item.udIdRef].ToString();
            item.udIdRef = null;
            item.authMode = 3;
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
        private void FillUdId()
        {
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<BiometricDevice> BdResponse = timeAttendance.ChildGetAll<BiometricDevice>(request);
            BdResponse.Items.ForEach(x => this.udId.Add(x.reference, Convert.ToInt32(x.recordId)));
        }


    }
}
