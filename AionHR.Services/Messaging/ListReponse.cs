using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging
{
   public class ListResponse<T>:ResponseBase
    {
        public List<T> Items { get; set;}

        public int count { get; set; }
    }
}
