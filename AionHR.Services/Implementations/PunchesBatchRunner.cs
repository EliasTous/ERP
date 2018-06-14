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
        Dictionary<string, string> arabicErrors;

        public PunchesBatchRunner(ISessionStorage store, IEmployeeService employee, ISystemService system, ITimeAttendanceService timeAttendance,Dictionary<string,string> arabicErrors) :base(system, employee)
        {
            this.timeAttendance = timeAttendance;
            this.SessionStore = store;
            SessionHelper h = new SessionHelper(store, new APIKeyBasedTokenGenerator());


            BatchStatus = new BatchOperationStatus() { classId = ClassId.TACH, processed = 0, tableSize = 0, status = 0 };
            errors = new List<Check>();
            udId = new Dictionary<string, int>();
            this.arabicErrors = arabicErrors;
            FillUdId();

            //FillCaId();

        }

        protected override void PostProcessElements()
        {
            StringBuilder b = new StringBuilder();
            int i = 0;
            foreach (var error in errors)
            {
                b.AppendLine(error.employeeRef + "," + error.clockStamp + "," + error.udId + "," + errorMessages[i++].Replace('\r', ' ').Replace(',', ';'));

            }


            string csv = b.ToString();
            string path = OutputPath + BatchStatus.classId.ToString() + ".txt";


            File.WriteAllText(path, csv.ToString(),Encoding.UTF8);
        }

        protected override void PreProcessElement(Check item)
        {
            //if (udId.ContainsKey(item.udIdRef))
            //    item.udId = udId[item.udIdRef].ToString();
            item.udIdRef = null;
            item.authMode = 3;
            item.lon = 0;
            item.lat = 0;
            item.hasImage = 0;
            item.ip = "0.0.0.0";

         
          
        }

        protected override void ProcessElement(Check item)
        {
            PostRequest<Check> req = new PostRequest<Check>();
            req.entity = item;

            PostResponse<Check> resp =timeAttendance.ChildAddOrUpdate<Check>(req);
            if (!resp.Success)
            {
                if (arabicErrors.ContainsKey(resp.ErrorCode))
                {
                    errorMessages.Add(arabicErrors[resp.ErrorCode]);
                }
                else
                    errorMessages.Add(resp.Summary);

                errors.Add(item);
              
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
