using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Attendance
{
    [ClassIdentifier("41020", "41")]
    public class Geofence:ModelBase
    {
        public string name
        {
            get; set;
        }
        public int branchId
        {
            get; set;
        }
           
       
        public string branchName
        {
            get; set;
        }
        public double lat
        {
            get; set;
        }
        public double lon
        {
            get; set;
        }
        public double? lat2
        {
            get; set;
        }
        public double? lon2
        {
            get; set;
        }
        public short shape
        {
            get; set;
        }
        public double? radius
        {
            get; set;
        }
    }
}
