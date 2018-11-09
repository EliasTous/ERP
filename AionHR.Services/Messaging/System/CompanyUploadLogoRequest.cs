using AionHR.Infrastructure.Domain;
using AionHR.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.System
{
  public  class CompanyUploadLogoRequest : PostRequest<Attachement>
    {
        public CompanyUploadLogoRequest()
        {
            entity = new Attachement();
            entity.classId = ClassId.SYDE;
            entity.folderId = null;
            entity.seqNo = 1;
            entity.recordId = 1; 
            
        }

        public string photoName { get; set; }

        public byte[] photoData { get; set; }

    }
}
