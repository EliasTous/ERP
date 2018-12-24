using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AionHR.Web.UI.Forms
{
    public class PageLookup
    {
        public static string GetPageUrlByClassId(int classId)
        {
            switch (classId)
            {
                case 60108: return "TimeVariationSelfServices.aspx";
                case 60109: return "TimeAttendanceViewSelfServices.aspx";
                case 60110: return "PayrollGenerationSelfServices.aspx";
                case 60104: return "LeaveRequestsSelfServices.aspx";
                case 60105: return "LoanSelfServices.aspx";
                case 60112: return "EmployeePenalties.aspx";

                default:return "Default.aspx";
            }
        }
    }
}