using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.MediaGallery
{
    [ClassIdentifier("23020", "23")]
    public class MediaItem : ModelBase
    {
        //public string departmentId { get; set; }
        
        public int departmentId { get; set; }

        public int mcId { get; set; }
        public short type { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }

        public string pictureUrl { get; set; }
        public string mcName { get; set; }
        public string departmentName { get; set; }

        

    }
}
