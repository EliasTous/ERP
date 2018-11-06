using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.AdminTemplates
{
    public class AdminDocument : ModelBase
    {
        public string bpName { get; set; }
        public string dcName { get; set; }
        public DateTime issueDate { get; set; }
        public DateTime expiryDate { get; set; }

        public int bpId { get; set; }
        public string docRef { get; set; }
        public string binNo { get; set; }
        public int dcId { get; set; }
        public string notes { get; set; }
        public string oDocRef { get; set; }
        public int? languageId { get; set; }



    }
}
