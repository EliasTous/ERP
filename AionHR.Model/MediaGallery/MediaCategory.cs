using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.MediaGallery
{
    [ClassIdentifier("23010", "23")]
    public class MediaCategory:ModelBase
    {
        public string name { get; set; }
    }
}
