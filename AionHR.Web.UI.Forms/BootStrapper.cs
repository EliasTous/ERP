using AionHR.Infrastructure.Configuration;
using AionHR.Infrastructure.Logging;
using AionHR.Infrastructure.Session;
using AionHR.Infrastructure.Tokens;
using AionHR.Model.Company.Cases;
using AionHR.Model.Company.News;
using AionHR.Model.Company.Structure;
using AionHR.Model.EmployeeComplaints;
using AionHR.Model.Employees;
using AionHR.Model.LeaveManagement;
using AionHR.Model.LoadTracking;
using AionHR.Model.MasterModule;
using AionHR.Model.MediaGallery;
using AionHR.Model.Reports;
using AionHR.Model.System;
using AionHR.Model.TimeAttendance;
using AionHR.Repository.WebService.Repositories;
using AionHR.Services.Implementations;
using AionHR.Services.Interfaces;
using Microsoft.Practices.ServiceLocation;
using StructureMap;
using StructureMap.Configuration.DSL;
using AionHR.Model.TaskManagement;
using AionHR.Model.Payroll;
using AionHR.Model.Access_Control;
using AionHR.Web.UI.Forms.Utilities;
using AionHR.Model.SelfService;

namespace AionHR.Web.UI.Forms
{
    public class BootStrapper
    {
        public static void ConfigureStructureMap()
        {
            // Initialize the registry
            var container = new Container(c => { c.AddRegistry<ModelRegistry>(); });
            var smServiceLocator = new StructureMapServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => smServiceLocator);
        }

        public class ModelRegistry : Registry
        {
            public ModelRegistry()
            {
                //Infrastructure

                For<IApplicationSettings>().Use<WebConfigApplicationSettings>();
                For<ISessionStorage>().Use<SessionStorage>();
                For<ITokenGenerator>().Use<APIKeyBasedTokenGenerator>();

                // Application Settings                 
                For<IApplicationSettings>().Use<WebConfigApplicationSettings>();

                // Logger
                For<ILogger>().Use<Log4NetAdapter>();

                //Repositories
                For<IEmployeeRepository>().Use<EmployeeRepository>();
                For<ISystemRepository>().Use<SystemRepository>();
                For<IAccountRepository>().Use<AccountRepository>();
                For<ILeaveManagementRepository>().Use<LeaveManagementRepository>();
                For<ICompanyStructureRepository>().Use<CompanyStructureRepository>();
                For<ITimeAttendanceRepository>().Use<TimeAttendanceRepository>();
                For<ICasesRepository>().Use<CasesRepository>();
                For<IMediaGalleryRepository>().Use<MediaGalleryRepository>();
                For<IComplaintsRepository>().Use<ComplaintsRepository>();
                For<ICompanyNewsRepository>().Use<CompanyNewsRepository>();
                For<ILoanTrackingRepository>().Use<LoanTrackingRepository>();
                For<IReportsRepository>().Use<ReportsRepository>();
                For<ITaskManagementRepository>().Use<TaskManagementRepository>();
                For<IPayrollRepository>().Use<PayrollRepository>();
                For<IAccessControlRepository>().Use<AccessControlRepository>();

                //Services
                For<ISystemService>().Use<SystemService>();
                For<IEmployeeService>().Use<EmployeeService>();
                For<IMasterService>().Use<MasterService>();
                For<ICompanyStructureService>().Use<CompanyStructureService>();
                For<ILeaveManagementService>().Use<LeaveManagementService>();
                For<ITimeAttendanceService>().Use<TimeAttendanceService>();
                For<ICaseService>().Use<CaseService>();
                For<IMediaGalleryService>().Use<MediaGalleryService>();
                For<IComplaintsService>().Use<ComplaintsService>();
                For<ICompanyNewsService>().Use<CompanyNewsService>();
                For<ILoanTrackingService>().Use<LoanTrackingService>();
                For<IReportsService>().Use<ReportsService>();
                For<ITaskManagementService>().Use<TaskManagementService>();
                For<IPayrollService>().Use<PayrollService>();
                For<IAccessControlService>().Use<AccessControlService>();
                For<ISelfServiceService>().Use<SelfServiceService>();
                For<ISelfServiceRepository>().Use<SelfServiceRepository>();

            }
        }
    }
}