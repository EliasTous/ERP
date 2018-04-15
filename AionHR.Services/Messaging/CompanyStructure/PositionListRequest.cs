using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.CompanyStructure
{
  public  class PositionListRequest :ListRequest
    {
        public string SortBy { get; set; }
      


        /// <summary>
        /// /// parameter list shipped with the web request
        /// </summary>
        public override Dictionary<string, string> Parameters
        {

            get
            {
                parameters = base.Parameters;

                parameters.Add("_sortBy", SortBy);
              

                return parameters;
            }
        }
    }
}
