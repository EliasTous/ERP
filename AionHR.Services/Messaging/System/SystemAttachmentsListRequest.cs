using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.System
{
   public class SystemAttachmentsListRequest:ListRequest
    {
        protected string classRef { get; set; }

        public int recordId { get; set; }

        private Dictionary<string, string> parameters;

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_classRef", classRef);
                parameters.Add("_recordId", recordId.ToString());
                return parameters;
            }
        }
    }
}
