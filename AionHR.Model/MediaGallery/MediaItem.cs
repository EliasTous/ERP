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
        [PropertyID("23020_01")]
        [ApplySecurity]
        public int? departmentId { get; set; }
        [PropertyID("23020_02")]
        [ApplySecurity]
        public int mcId { get; set; }
        [PropertyID("23020_03")]
        [ApplySecurity]
        public short type { get; set; }
        [PropertyID("23020_04")]
        [ApplySecurity]
        public string description { get; set; }
        [PropertyID("23020_05")]
        [ApplySecurity]
        public DateTime date { get; set; }
        [PropertyID("23020_06")]
        [ApplySecurity]

        public string pictureUrl { get; set; }
        [PropertyID("23020_02")]
        [ApplySecurity]
        public string mcName { get; set; }
        [PropertyID("23020_01")]
        [ApplySecurity]
        public string departmentName { get; set; }

        

    }
}
