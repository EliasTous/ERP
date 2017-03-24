using AionHR.Infrastructure.Session;
using AionHR.Model.MediaGallery;
using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
   public class MediaGalleryService : BaseService, IMediaGalleryService
    {
        IMediaGalleryRepository _mediaGalleryRepository;
        protected override dynamic GetRepository()
        {
            return _mediaGalleryRepository;
        }

        public MediaGalleryService(IMediaGalleryRepository mediaRepo, SessionHelper sessionHelper) : base(sessionHelper)
        {
            _mediaGalleryRepository = mediaRepo;

        }
    }
}
