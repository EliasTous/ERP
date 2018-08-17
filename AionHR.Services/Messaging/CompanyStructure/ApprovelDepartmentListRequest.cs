using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.CompanyStructure
{
  public  class ApprovelDepartmentListRequest:ListRequest
    {
        public string apId { get; set; }



        /// <summary>
        /// /// parameter list shipped with the web request
        /// </summary>
        public override Dictionary<string, string> Parameters
        {

            get
            {
                parameters = base.Parameters;

                parameters.Add("_apId", apId.ToString());


                return parameters;
            }
        }
    }
}
