using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging
{
   public class PostResponse<T>:ResponseBase
    {
        public string recordId { get; set; }
    }
}
