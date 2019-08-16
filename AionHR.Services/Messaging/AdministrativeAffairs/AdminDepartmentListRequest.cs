using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.AdministrativeAffairs
{
   public class AdminDepartmentListRequest : ListRequest
    {
        public int status { get; set; }



        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_status", status.ToString());
              

                return parameters;
            }
        }
    }
}

