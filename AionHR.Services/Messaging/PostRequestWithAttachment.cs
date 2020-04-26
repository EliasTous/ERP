using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging
{
    public class PostRequestWithAttachment<T>:PostRequest<T>
    {
        public string FileName { get; set; }

        public byte[] FileData { get; set; }
    }
}
