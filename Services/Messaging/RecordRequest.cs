using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging
{
    /// <summary>
    /// Generic class used to fill a request to be used by a record retrieval service
    /// </summary>
    public class RecordRequest : RequestBase
    {
        /// <summary>
        /// Generic record request class
        /// </summary>
        public string RecordID { get; set; }
        /// <summary>
        /// parameter list shipped with the web request
        /// </summary>
        public Dictionary<string, string> parameters;


        
        /// <summary>
        /// parameter list shipped with the web request
        /// </summary>
        public override Dictionary<string, string> Parameters
        {

            get
            {
                parameters = base.Parameters;
                if (!string.IsNullOrEmpty(RecordID))
                    parameters.Add("_recordId", RecordID);
             
                return parameters;
            }
        }
    }
}
