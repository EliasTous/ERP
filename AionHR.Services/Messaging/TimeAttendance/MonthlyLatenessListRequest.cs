using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.TimeAttendance
{
    public class MonthlyLatenessListRequest : ListRequest
    {
        public string PayId { get; set; }


        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.parameters;

                parameters.Add("_payId", PayId);


                return parameters;
            }
        }
    }

    public class MLDListRequest : ListRequest
    {
        public string PayId { get; set; }
        public string EmployeeId { get; set; }


        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.parameters;

                parameters.Add("_payId", PayId);
                parameters.Add("_employeeId", EmployeeId);


                return parameters;
            }
        }
    }

    public class MonthlyLatenessPeriodListRequest : RecordRequest
    {
        public string PayID { get; set; }

        

        public override Dictionary<String, String> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_payId", PayID);

                return parameters;
            }
        }
    }

}
