using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.AdminTemplates
{
public   class ProcessNotificationTypes
    {
        public const short PENALTY_NEW = ModuleId.EP * 100 + 1;
        public const short PENALTY_APPROVED = ModuleId.EP * 100 + 2;
        public const short PENALTY_REJECTED = ModuleId.EP * 100 + 3;

        public const short TIME_SCHEDULE = ModuleId.TA * 100 + 1;

        public const short LEAVE_REQUEST_NEW = ModuleId.LM * 100 + 1;
        public const short LEAVE_REQUEST_APPROVED = ModuleId.LM * 100 + 2;
        public const short LEAVE_REQUEST_REJECTED = ModuleId.LM * 100 + 3;

        public const short LOAN_REQUEST_NEW = ModuleId.LT * 100 + 1;
        public const short LOAN_REQUEST_APPROVED = ModuleId.LT * 100 + 2;
        public const short LOAN_REQUEST_REJECTED = ModuleId.LT * 100 + 3;

        public const short PAYROLL_PAYSLIP = ModuleId.PY * 100 + 1;

    }
}
