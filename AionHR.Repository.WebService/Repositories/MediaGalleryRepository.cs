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
            ChildGetLookup.Add(typeof(MediaCategory), "getMC");
            ChildGetAllLookup.Add(typeof(MediaCategory), "qryMC");
            ChildAddOrUpdateLookup.Add(typeof(MediaCategory), "setMC");
            ChildDeleteLookup.Add(typeof(MediaCategory), "delMC");


            ChildGetLookup.Add(typeof(MediaItem), "getME");
            ChildGetAllLookup.Add(typeof(MediaItem), "qryME");
            ChildAddOrUpdateLookup.Add(typeof(MediaItem), "setME");
            ChildDeleteLookup.Add(typeof(MediaItem), "delME");

        }
    }
}
