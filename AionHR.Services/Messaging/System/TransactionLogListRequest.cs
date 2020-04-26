using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.System
{
    public class TransactionLogListRequest:ListRequest
    {
        public int ClassId { get; set; }

        

        public int PrimaryKey { get; set; }

        public int UserId { get; set; }

        public int Type { get; set; }

        public string SoryBy { get; set; }

        private Dictionary<string, string> parameters;
        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_classId", ClassId.ToString());
                
                parameters.Add("_pk", PrimaryKey.ToString());
                parameters.Add("_userId", UserId.ToString());
                parameters.Add("_type", Type.ToString());
                parameters.Add("_sortBy", SoryBy);
                return parameters;
            }
        }
    }
}
