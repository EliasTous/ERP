using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.UI.Forms.Utilities
{
    public class TimeSlot
    {
        public string ID { get; set; }
        public string Time { get; set; }
        
    }

    public class DaysSlot
    {
        public string ID { get;set;}
        public string Day { get; set; }
    }
    public class TimeSlotComparer : IEqualityComparer<TimeSlot>
    {
        // Products are equal if their names and product numbers are equal.
        public bool Equals(TimeSlot x, TimeSlot y)
        {

            if (x.Time == y.Time && x.ID == y.ID)
                return true;
            else
                return false;
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(TimeSlot product)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(product, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int ID = product.ID == null ? 0 : product.ID.GetHashCode();

            //Get hash code for the Code field.
            int Time = product.Time == null ? 0 : product.Time.GetHashCode();

            //Calculate the hash code for the product.
            return ID ^ Time;
        }

    }

}