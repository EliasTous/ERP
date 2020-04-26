using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging
{
    public class RequestBase
    {
        
        public virtual Dictionary<string,string> Parameters
        {
            get
            {
                return new Dictionary<string, string>();
            }
        }
    }
}
