using AionHR.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.System
{
   public class SystemAttachmentsListRequest:ListRequest
    {
        protected int classId { get; set; }

        public int recordId { get; set; }

        private Dictionary<string, string> parameters;

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_classId", classId.ToString());
                parameters.Add("_recordId", recordId.ToString());
                return parameters;
            }
        }
    }
    public class SystemAttachmentsPostRequest : PostRequest<Attachement>
    {
       public List<string> FileNames { get; set; }

       public List<byte[]> FilesData { get; set; }

        public SystemAttachmentsPostRequest()
        {
            FileNames = new List<string>();
            FilesData = new List<byte[]>();
                
        }
    }
}
