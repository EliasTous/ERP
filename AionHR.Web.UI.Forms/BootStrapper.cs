using Infrastructure.Configuration;
using Infrastructure.Logging;
using Infrastructure.Session;
using Infrastructure.Tokens;
using Model.Company.Cases;
using Model.Company.News;
using Model.Company.Structure;
using Model.EmployeeComplaints;
using Model.Employees;
using Model.LeaveManagement;
using Model.LoadTracking;
using Model.MasterModule;
using Model.MediaGallery;
using Model.Reports;
using Model.System;
using Model.TimeAttendance;
using Repository.WebService.Repositories;
using Services.Implementations;
using Services.Interfaces;
using Microsoft.Practices.ServiceLocation;
using StructureMap;
using StructureMap.Configuration.DSL;
using Model.TaskManagement;
using Model.Payroll;
using Model.Access_Control;
using Web.UI.Forms.Utilities;
using Model.SelfService;
using Model.NationalQuota;
using Model.HelpFunction;
using Model.Benefits;
using Model.Dashboard;
using Model.AdminTemplates;
using Model.AssetManagementRepository;
using Model.TaskSchedule;
using System.Configuration;

namespace Web.UI.Forms
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
                For<ISelfServiceRepository>().Use<SelfServiceRepository>();
                For<INationalQuotaRepository>().Use<NationalQuotaRepository>();
                For<IHelpFunctionRepository>().Use<HelpFunctionRepository>();
                For<IBenefitsRepository>().Use<BenefitsRepository>();
                For<IDashBoardRepository>().Use<DashBoardRepository>();
                For<IAdministrationRepository>().Use<AdministrationRepository>();
                For<IAssetManagementRepository>().Use<AssetManagementRepository>();
                For<IMathematicalRepository>().Use<MathematicalRepository>();
                For<ITaskScheduleRepository>().Use <TaskScheduleRepository>();

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
                For<INationalQuotaService>().Use<NationalQuotaService>();
                For<IHelpFunctionService>().Use<HelpFunctionService>();
                For<IBenefitsService>().Use<BenefitsService>();
                For<IDashBoardService>().Use<DashBoardService>();
                For<IAdministrationService>().Use<AdministrationService>();
                For<IAssetManagementService>().Use<AssetManagementService>();
                For<IMathematicalService>().Use<MathematicalService>();
                For<ITaskScheduleService>().Use<TaskScheduleService>();

            }
        }
    }
}