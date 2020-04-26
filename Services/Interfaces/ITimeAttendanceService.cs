using Model.Attendance;
using Services.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITimeAttendanceService:IBaseService
    {

        PostResponse<AttendanceBreak> DeleteDayBreaks(int ScheduleId, short dow);
    }
}
