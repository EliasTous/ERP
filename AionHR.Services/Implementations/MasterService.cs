using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionHR.Infrastructure.Session;
using AionHR.Model.MasterModule;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.System;
using AionHR.Infrastructure.WebService;
using AionHR.Infrastructure.Domain;
using AionHR.Model.System;
using AionHR.Model.Employees.Profile;
using AionHR.Model.Company.Structure;
using AionHR.Model.MediaGallery;
using AionHR.Model.Company.News;
using AionHR.Model.TaskManagement;
using AionHR.Model.Attendance;
using AionHR.Model.TimeAttendance;
using AionHR.Model.LeaveManagement;
using AionHR.Model.Employees.Leaves;
using AionHR.Model.Payroll;
using AionHR.Model.LoadTracking;

namespace AionHR.Services.Implementations
{
    public class MasterService : BaseService,IMasterService
    {
        private IAccountRepository _accountRepository;

        public Dictionary<string, Type> ClassLookup
        {
            get; set;
        }

        public MasterService(IAccountRepository accountRepository, SessionHelper helper):base(helper)
        {
            _accountRepository = accountRepository;
            ClassLookup = new Dictionary<string, Type>();

            ClassLookup.Add(((int)ClassId.SYAA).ToString(), typeof(SystemAlert));
            ClassLookup.Add(((int)ClassId.SYAB).ToString(), typeof(AddressBook));
            ClassLookup.Add(((int)ClassId.SYAT).ToString(), typeof(Attachement));
            ClassLookup.Add(((int)ClassId.SYCU).ToString(), typeof(Currency));
            ClassLookup.Add(((int)ClassId.SYDE).ToString(), typeof(SystemDefault));
            ClassLookup.Add(((int)ClassId.SYDT).ToString(), typeof(CompanyDocumentType));
            ClassLookup.Add(((int)ClassId.SYFO).ToString(), typeof(SystemFolder));
            ClassLookup.Add(((int)ClassId.SYNA).ToString(), typeof(Nationality));
            ClassLookup.Add(((int)ClassId.SYRW).ToString(), typeof(CompanyRightToWork));
            ClassLookup.Add(((int)ClassId.SYTL).ToString(), typeof(TransactionLog));
            ClassLookup.Add(((int)ClassId.SYUS).ToString(), typeof(UserInfo));

            ClassLookup.Add(((int)ClassId.CSBR).ToString(), typeof(Branch));
            ClassLookup.Add(((int)ClassId.CSDE).ToString(), typeof(Department));
            ClassLookup.Add(((int)ClassId.CSDI).ToString(), typeof(Division));
            ClassLookup.Add(((int)ClassId.CSPO).ToString(), typeof(Position));

            
            ClassLookup.Add(((int)ClassId.DMDT).ToString(), typeof(CompanyDocumentType));

            ClassLookup.Add(((int)ClassId.MGMC).ToString(), typeof(MediaCategory));
            ClassLookup.Add(((int)ClassId.MGME).ToString(), typeof(MediaItem));

            ClassLookup.Add(((int)ClassId.CNNW).ToString(), typeof(News));

            ClassLookup.Add(((int)ClassId.EPAA).ToString(), typeof(AssetAllowance));
            ClassLookup.Add(((int)ClassId.EPAC).ToString(), typeof(AssetCategory));
            ClassLookup.Add(((int)ClassId.EPBC).ToString(), typeof(EmployeeBackgroundCheck));//error: Missing Translation
            ClassLookup.Add(((int)ClassId.EPBO).ToString(), typeof(Bonus));
            ClassLookup.Add(((int)ClassId.EPBT).ToString(), typeof(BonusType));
            ClassLookup.Add(((int)ClassId.EPCE).ToString(), typeof(EmployeeCertificate));
            ClassLookup.Add(((int)ClassId.EPCL).ToString(), typeof(CertificateLevel));
            ClassLookup.Add(((int)ClassId.EPCO).ToString(), typeof(EmployeeContact));
            ClassLookup.Add(((int)ClassId.EPCT).ToString(), typeof(CheckType));
            //ClassLookup.Add(((int)ClassId.EPDO).ToString(), typeof(News));
            ClassLookup.Add(((int)ClassId.EPDT).ToString(), typeof(DocumentType));
            ClassLookup.Add(((int)ClassId.EPEC).ToString(), typeof(EmployeeEmergencyContact));
            ClassLookup.Add(((int)ClassId.EPED).ToString(), typeof(EntitlementDeduction));
            ClassLookup.Add(((int)ClassId.EPEH).ToString(), typeof(EmploymentHistory));
            ClassLookup.Add(((int)ClassId.EPEM).ToString(), typeof(Employee));
            ClassLookup.Add(((int)ClassId.EPJI).ToString(), typeof(JobInfo));
            ClassLookup.Add(((int)ClassId.EPNO).ToString(), typeof(EmployeeNote));
            ClassLookup.Add(((int)ClassId.EPNP).ToString(), typeof(NoticePeriod));
            //ClassLookup.Add(((int)ClassId.EPRE).ToString(), typeof(Reqrue));
            ClassLookup.Add(((int)ClassId.EPRT).ToString(), typeof(RelationshipType));
            ClassLookup.Add(((int)ClassId.EPRW).ToString(), typeof(EmployeeRightToWork));
            ClassLookup.Add(((int)ClassId.EPSA).ToString(), typeof(EmployeeSalary));
            ClassLookup.Add(((int)ClassId.EPSC).ToString(), typeof(SalaryChangeReason));
            ClassLookup.Add(((int)ClassId.EPSD).ToString(), typeof(SalaryDetail));
            ClassLookup.Add(((int)ClassId.EPSH).ToString(), typeof(Sponsor));
            ClassLookup.Add(((int)ClassId.EPSP).ToString(), typeof(AddressBook));
            ClassLookup.Add(((int)ClassId.EPST).ToString(), typeof(State));
            ClassLookup.Add(((int)ClassId.EPTE).ToString(), typeof(EmployeeTermination));
            ClassLookup.Add(((int)ClassId.EPTR).ToString(), typeof(TerminationReason));

            ClassLookup.Add(((int)ClassId.TMTA).ToString(), typeof(Model.TaskManagement.Task));
            ClassLookup.Add(((int)ClassId.TMTC).ToString(), typeof(TaskComment));
            ClassLookup.Add(((int)ClassId.TMTT).ToString(), typeof(TaskType));

            ClassLookup.Add(((int)ClassId.TAAD).ToString(), typeof(AttendanceDay));
            ClassLookup.Add(((int)ClassId.TAAS).ToString(), typeof(AttendanceShift));
            ClassLookup.Add(((int)ClassId.TABM).ToString(), typeof(BiometricDevice));
            ClassLookup.Add(((int)ClassId.TACA).ToString(), typeof(WorkingCalendar));
            ClassLookup.Add(((int)ClassId.TACD).ToString(), typeof(CalendarDay));
            ClassLookup.Add(((int)ClassId.TACH).ToString(), typeof(Check));
            ClassLookup.Add(((int)ClassId.TACY).ToString(), typeof(CalendarYear));
            //ClassLookup.Add(((int)ClassId.TADE).ToString(), typeof(Device));
            ClassLookup.Add(((int)ClassId.TADT).ToString(), typeof(DayType));
            ClassLookup.Add(((int)ClassId.TAGF).ToString(), typeof(Geofence));
            ClassLookup.Add(((int)ClassId.TARO).ToString(), typeof(Router));
            ClassLookup.Add(((int)ClassId.TASB).ToString(), typeof(AttendanceBreak));
            ClassLookup.Add(((int)ClassId.TASC).ToString(), typeof(AttendanceSchedule));
            ClassLookup.Add(((int)ClassId.TASD).ToString(), typeof(AttendanceScheduleDay));

            ClassLookup.Add(((int)ClassId.LMLD).ToString(), typeof(LeaveDay));
            ClassLookup.Add(((int)ClassId.LMLR).ToString(), typeof(LeaveRequest));
            ClassLookup.Add(((int)ClassId.LMLT).ToString(), typeof(LeaveType));
            ClassLookup.Add(((int)ClassId.LMVP).ToString(), typeof(VacationSchedulePeriod));
            ClassLookup.Add(((int)ClassId.LMVS).ToString(), typeof(VacationSchedule));

            ClassLookup.Add(((int)ClassId.LTLC).ToString(), typeof(LoanComment));
            ClassLookup.Add(((int)ClassId.LTLR).ToString(), typeof(Loan));
            ClassLookup.Add(((int)ClassId.LTLT).ToString(), typeof(LoanType));


            ClassLookup.Add(((int)ClassId.PYED).ToString(), typeof(PayrollEntitlementDeduction));
            ClassLookup.Add(((int)ClassId.PYEM).ToString(), typeof(EmployeePayroll));
            ClassLookup.Add(((int)ClassId.PYHE).ToString(), typeof(GenerationHeader));
            ClassLookup.Add(((int)ClassId.PYPE).ToString(), typeof(FiscalPeriod));
            ClassLookup.Add(((int)ClassId.PYYE).ToString(), typeof(FiscalYear));


        }



        public Response<Account> GetAccount(GetAccountRequest request)
        {
            Response<Account> response = new Response<Account>();
            SessionHelper.ClearSession();
            SessionHelper.Set("AccountId", "0"); //To be checked as it is a strange behavior ( simulated from old code)
            Dictionary<string, string> headers = SessionHelper.GetAuthorizationHeadersForUser();
            
            var accountRecord = _accountRepository.GetRecord(headers, request.Parameters);
            response =CreateServiceResponse<Response<Account>>(accountRecord);
            if (accountRecord == null)
            {
                response.Success = false;
                response.Message = "RequestError"; //This message have to be read from resource, it indicate that there was a problem in the connection.
                return response;
            }
            if (accountRecord.record == null)
            {
                response.Success = false;
                response.Message = "InvalidAccount";
                return response;
            }
            response.result = (Account)accountRecord.record;
            SessionHelper.Set("AccountId", response.result.accountId);
            response.Success = true;
            return response;
        }

        public Response<Account> RequestAccountRecovery(AccountRecoveryRequest request)
        {
            Response<Account> response;
            SessionHelper.ClearSession();
            SessionHelper.Set("AccountId", "0");
            Dictionary<string, string> headers = SessionHelper.GetAuthorizationHeadersForUser();
            
            var webResponse=  _accountRepository.GetRecord(headers, request.Parameters);
            response = CreateServiceResponse<Response<Account>>(webResponse);
            if (!response.Success)
                response.Message = "";
            return response;
        }

        public PostResponse<Registration> AddRegistration(Registration r)
        {
            PostResponse<Registration> response = new PostResponse<Registration>();
            SessionHelper.ClearSession();
            SessionHelper.Set("AccountId", "0"); //To be checked as it is a strange behavior ( simulated from old code)
            Dictionary<string, string> headers = SessionHelper.GetAuthorizationHeadersForUser();
            var accountRecord = _accountRepository.ChildAddOrUpdate<Registration>(r, headers);
            response = base.CreateServiceResponse<PostResponse<Registration>>(accountRecord);

            if (accountRecord != null)
                response.recordId = accountRecord.recordId;
            return response;

        }
        public PostResponse<Account> AddAccount(Account r)
        {
            PostResponse<Account> response = new PostResponse<Account>();
            SessionHelper.ClearSession();
            SessionHelper.Set("AccountId", "0"); //To be checked as it is a strange behavior ( simulated from old code)
            Dictionary<string, string> headers = SessionHelper.GetAuthorizationHeadersForUser();
            var accountRecord = _accountRepository.ChildAddOrUpdate<Account>(r, headers);
            response = base.CreateServiceResponse<PostResponse<Account>>(accountRecord);

            if (accountRecord != null)
                response.recordId = accountRecord.recordId;
            return response;

        }
        public PostResponse<DbSetup> CreateDB(DbSetup r)
        {
            PostResponse<DbSetup> response = new PostResponse<DbSetup>();
            SessionHelper.ClearSession();
            SessionHelper.Set("AccountId", "0");
            SessionHelper.Set("UserId", "0");
            Dictionary<string, string> headers = SessionHelper.GetAuthorizationHeadersForUser();
            var accountRecord = _accountRepository.ChildAddOrUpdate<DbSetup>(r, headers);
            response = base.CreateServiceResponse<PostResponse<DbSetup>>(accountRecord);
           
          
            SessionHelper.ClearSession();
            SessionHelper.Set("AccountId", r.accountId);
            SessionHelper.Set("UserId", "0");
         
            if (accountRecord != null)
                response.recordId = accountRecord.recordId;
            return response;

        }
        protected override dynamic GetRepository()
        {
            return _accountRepository;
        }
    }
}
