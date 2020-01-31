using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.TimeAttendance
{



    public class FlatPunchesListRequest : ListRequest
    {

        public string shiftId { get; set; }
        public string sortBy { get; set; }





        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_shiftId", shiftId);
                parameters.Add("_sortBy", sortBy);



                return parameters;
            }
        }
    }


    public class TVListRequest : ListRequest
    {

        public string paramString { get; set; }
        public string startAt { get; set; }
        public string size { get; set; }
        public string sortBy { get; set; }


        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_params", paramString);                
                parameters.Add("_startAt", startAt);
                parameters.Add("_size", size);
                parameters.Add("_sortBy", sortBy);
                return parameters;
            }
        }
    }


}
