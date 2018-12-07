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
                case 60108: return "AssetAllowanceSelfService.aspx";
                case 60107: return "EmployeeComplaintsSelfService.aspx";

                default:return "Default.aspx";
            }
        }
    }
}