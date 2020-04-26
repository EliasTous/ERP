using Infrastructure.Domain;
using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.MediaGallery
{
    [ClassIdentifier("23010", "23")]
    public class MediaCategory:ModelBase
    {
        [PropertyID("23010_01")]
        [ApplySecurity]
        public string name { get; set; }
    }
}
