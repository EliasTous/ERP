using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.AdministrativeAffairs
{
  public  class DocumentNoteRecordRequest:RecordRequest
    {
        public string documentId { get; set; }
        public string seq { get; set; }


        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_documentId", documentId);
                parameters.Add("_seq", seq);
                return parameters;
            }
        }
    }
}
