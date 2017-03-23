using AionHR.Infrastructure.Configuration;
using AionHR.Infrastructure.Domain;
using AionHR.Model.MediaGallery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Repository.WebService.Repositories
{
    public class MediaGalleryRepository: Repository<IEntity, string>, IMediaGalleryRepository
    {

        private string serviceName = "MG.asmx/";
        public MediaGalleryRepository()
        {
            base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;
            
        }
    }
}
