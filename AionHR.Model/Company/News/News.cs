using Infrastructure.Domain;
using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Company.News
{
    [ClassIdentifier("24010", "24")]
    public class News : ModelBase,IEntity
    {
        [PropertyID("24010_01")]
        [ApplySecurity]
        public string subject { get; set; }
        [PropertyID("24010_02")]
        [ApplySecurity]
        public string newsText { get; set; }
        [PropertyID("24010_03")]
        [ApplySecurity]
        public bool notifyViaEmail { get; set; }
        [PropertyID("24010_04")]
        [ApplySecurity]
        public bool allowComments { get; set; }
        [PropertyID("24010_05")]
        [ApplySecurity]
        public bool isPublished { get; set; }

    }
}
