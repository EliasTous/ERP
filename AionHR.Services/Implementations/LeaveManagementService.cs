using AionHR.Infrastructure.Session;
using AionHR.Model.LeaveManagement;
using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionHR.Model.Employees.Leaves;
using AionHR.Services.Messaging;

namespace AionHR.Services.Implementations
{
    public class LeaveManagementService : BaseService, ILeaveManagementService

    {
        private ILeaveManagementRepository _repository;
        public LeaveManagementService(SessionHelper helper, ILeaveManagementRepository repository):base(helper)
        {
            this._repository = repository;
        }

        public PostResponse<LeaveDay> DeleteLeaveDays(int leaveId)
        {
            PostResponse<LeaveDay> response;
            var headers = SessionHelper.GetAuthorizationHeadersForUser();

            LeaveDay breaks = new LeaveDay() { leaveId = leaveId, dayId = "0"};
            var webResponse = GetRepository().ChildDelete<LeaveDay>(breaks, headers);
            response = CreateServiceResponse<PostResponse<LeaveDay>>(webResponse);

            return response;
        }

        public PostResponse<VacationSchedulePeriod> DeleteVacationSchedulePeriods(int vacationScheduleId)
        {
            PostResponse<VacationSchedulePeriod> response;
            var headers = SessionHelper.GetAuthorizationHeadersForUser();

            VacationSchedulePeriod breaks = new VacationSchedulePeriod() {  vsId=vacationScheduleId, days=0, from=0, to=0, seqNo = 0 };
            var webResponse = GetRepository().ChildDelete<VacationSchedulePeriod>(breaks, headers);
            response = CreateServiceResponse<PostResponse<VacationSchedulePeriod>>(webResponse);

            return response;
        }

        protected override dynamic GetRepository()
        {
            return _repository;
        }
    }
}
