using AionHR.Services.Messaging.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.CompanyStructure
{
  public class CompanyFilesListRequest: SystemAttachmentsListRequest
    {
        public CompanyFilesListRequest()
        {
            base.classId = 20080;
        }
    }
}
