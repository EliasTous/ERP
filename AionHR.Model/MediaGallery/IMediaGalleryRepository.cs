using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.WebService;

namespace Model.MediaGallery
{
   public  interface IMediaGalleryRepository : IRepository<IEntity, string>, ICommonRepository
    {
        
    }
}
