using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionHR.Infrastructure.WebService;

namespace AionHR.Model.MediaGallery
{
   public  interface IMediaGalleryRepository : IRepository<IEntity, string>, ICommonRepository
    {
        
    }
}
