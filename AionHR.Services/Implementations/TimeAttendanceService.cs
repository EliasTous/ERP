using AionHR.Infrastructure.Session;
using AionHR.Model.TimeAttendance;
using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionHR.Model.Attendance;
using AionHR.Services.Messaging;

namespace AionHR.Services.Implementations
{
    public class TimeAttendanceService : BaseService,ITimeAttendanceService
    {
        private ITimeAttendanceRepository _repository;
        public TimeAttendanceService(SessionHelper helper, ITimeAttendanceRepository repository):base(helper)
        {
            this._repository = repository;
        }

        public PostResponse<AttendanceBreak> DeleteDayBreaks(int ScheduleId, short dow)
        {
            PostResponse<AttendanceBreak> response;
            var headers = SessionHelper.GetAuthorizationHeadersForUser();

            AttendanceBreak breaks = new AttendanceBreak() { scId = ScheduleId, dow = dow, start = "00:00", end = "00:00", name = " ", seqNo = 0 };
            var webResponse = GetRepository().ChildDelete<AttendanceBreak>(breaks, headers);
            response = CreateServiceResponse<PostResponse<AttendanceBreak>>(webResponse);

            return response;
        }

        protected override dynamic GetRepository()
        {
            return _repository;
        }
    }
}
