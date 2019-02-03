using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AionHR.Web.UI.Forms.ConstClasses
{
    public static class ConstDashboardItem
    {
        // head count (total of pie 1)
        public const short TA_HEAD_COUNT = 100;

        // attendance (pie 1 data)

        // active
        public const short TA_CHECKED = 101;                // qryCH (employee info + first punch)
        // absent
        public const short TA_PENDING = 102;                // qryPE (employee day info)
        public const short TA_NO_SHOW_UP = 103;             // qryNS (employee day info)
        public const short TA_LEAVE_WITHOUT_EXCUSE = 104;   // qryLW (employee day info)
        public const short TA_LEAVE = 105;                  // qryLE (employee day info)
        public const short TA_DAY_OFF = 106;                // qryDO (Employee day info)

        // time variation (pie 2 data) = distribution of TA_CHECKED
        // call qryTV for all 5 services (employee info + first punch). parameter 1 is the TimeVariationType

        // lateness (TimeVariationType = 3x)
        public const short TA_LATE_CHECKIN = 211;
        public const short DURING_SHIFT_LEAVE = 212;
        public const short TA_EARLY_LEAVE = 213;
        // overtime (TimeVariationType = 4x)
        public const short TA_EARLY_CHECKIN = 221;
        public const short TA_OVERTIME = 222;

        public const short TA_PAID_LEAVE = 311;

        public const short TA_UNPAID_LEAVE = 312;


        // summary figures
        public const short SALARY_CHANGE_DUE = 901;
        public const short END_OF_PROBATION = 902;
        public const short EMPLOYMENT_REVIEW_DATE = 903;
        public const short TERM_END_DATE = 904;
        public const short BIRTHDAY = 905;
        public const short RETIREMENT = 906;
        public const short WORK_ANNIVERSARY = 907;
        public const short COMPANY_RIGHT_TO_WORK = 908;
        public const short EMPLOYEE_RIGHT_TO_WORK = 909;
        public const short LOANS = 910;
        public const short VACATIONS= 911;


        public const short APPROVAL_TIME = 401;
        public const short APPROVAL_LEAVE = 402;
        public const short APPROVAL_LOAN = 403;
        public const short APPROVAL_PENALTY = 404;

    }
}