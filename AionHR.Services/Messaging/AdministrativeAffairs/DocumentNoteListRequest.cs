using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.AdministrativeAffairs
{
   public class DocumentNoteListRequest:ListRequest
    {
        public int documentId { get; set; }

     

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_doId", documentId.ToString());
                parameters.Add("_filter", Filter);

                return parameters;
            }
        }
    }
}
