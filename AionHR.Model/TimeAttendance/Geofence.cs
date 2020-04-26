using Infrastructure.Domain;
using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Attendance
{
    [ClassIdentifier("41020", "41")]
    public class Geofence:ModelBase
    {
        [PropertyID("41020_01")]
        [ApplySecurity]
        public string name
        {
            get; set;
        }
        [PropertyID("41020_02")]
        [ApplySecurity]
        public int branchId
        {
            get; set;
        }

        [PropertyID("41020_02")]
        [ApplySecurity]
        public string branchName
        {
            get; set;
        }
        [PropertyID("41020_03")]
        [ApplySecurity]
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
