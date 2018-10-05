using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Attendance
{
    [ClassIdentifier("41060", "41")]
    public class Check
    {
        //public int authMode { get; set; }
        //public int? employeeId { get; set; }

        public string employeeRef { get; set; }
        //public int? checkStatus { get; set; }
        //public string ip { get; set; }
        public string serialNo { get; set; }

        public DateTime clockStamp { get; set; }

        public string udId { get; set; }
        //public int? lat { get; set; }
        //public int? lon { get; set; }

        //public string routerRef { get; set; }
        //public short hasImage; 
        //public string udIdRef { get; set; }

    }
  public  class CheckComparer : IEqualityComparer<Check>
    {
        // Products are equal if their names and product numbers are equal.
        public bool Equals(Check x, Check y)
        {

            if (x.employeeRef == y.employeeRef && x.clockStamp == y.clockStamp)
                return true;
            else
                return false;
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(Check product)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(product, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashEmployeeRef = product.employeeRef == null ? 0 : product.employeeRef.GetHashCode();

            //Get hash code for the Code field.
            int hashClockStamp = product.clockStamp.GetHashCode();

            //Calculate the hash code for the product.
            return hashEmployeeRef ^ hashClockStamp;
        }

    }
}
