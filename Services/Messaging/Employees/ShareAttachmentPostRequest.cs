using Model.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.Employees
{
  public  class ShareAttachmentPostRequest: PostRequest<ShareAttachment>
    {
       
            public List<byte[]> FilesData { get; set; }
            public List<string> FileNames { get; set; }

        public ShareAttachmentPostRequest()
            {

            FileNames = new List<string>();
            FilesData = new List<byte[]>();
        }
        }
    }

