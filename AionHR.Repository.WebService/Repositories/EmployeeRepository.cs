using AionHR.Model.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionHR.Infrastructure.Domain;
using AionHR.Infrastructure.Configuration;
using AionHR.Model.Employees.Profile;
using AionHR.Infrastructure.WebService;

namespace AionHR.Repository.WebService.Repositories
{
    /// Class that handle the communcation between the model and the webservice. it encapsultes all the employee relative methods
    public class EmployeeRepository : Repository<Employee, string>, IEmployeeRepository
    {

        /// <summary>
        /// the service name
        /// </summary>
        private string serviceName = "EP.asmx/";
        private string addOrRemoveEmployeeWithImageMethodName = "setEM";
        private string addOrRemoveEmployeeDocumentMethodName = "setDO";
        public EmployeeRepository()
        {

            base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;
            GetAllMethodName = "qryES";
            AddOrUpdateMethodName = "setEM";
            GetRecordMethodName = "getEM1";

            ChildGetAllLookup.Add(typeof(Sponsor), "qrySP");
            ChildGetAllLookup.Add(typeof(AllowanceType), "qryAT");
            ChildGetAllLookup.Add(typeof(CertificateLevel), "qryCL");
            ChildGetAllLookup.Add(typeof(TrainingType), "qryTT");
            ChildGetAllLookup.Add(typeof(EntitlementDeduction), "qryED");
            ChildGetAllLookup.Add(typeof(DocumentType), "qryDT");
            ChildGetAllLookup.Add(typeof(SalaryChangeReason), "qrySC");
            ChildGetAllLookup.Add(typeof(AssetCategory), "qryAC");
            ChildGetAllLookup.Add(typeof(BonusType), "qryBT");
            ChildGetAllLookup.Add(typeof(EmploymentHistory), "qryEH");
            ChildGetAllLookup.Add(typeof(JobInfo), "qryJI");
            ChildGetAllLookup.Add(typeof(EmploymentStatus), "qryST");
            ChildGetAllLookup.Add(typeof(EmployeeSalary), "qrySA");
            ChildGetAllLookup.Add(typeof(Bonus), "qryBO");
            ChildGetAllLookup.Add(typeof(SalaryDetail), "qrySD");
            ChildGetAllLookup.Add(typeof(EmployeeNote), "qryNO");
            ChildGetAllLookup.Add(typeof(EmployeeDocument), "qryDO");
            ChildGetAllLookup.Add(typeof(EmployeeCertificate), "qryCE");
            ChildGetAllLookup.Add(typeof(EmployeeRightToWork), "qryRW");
            ChildGetAllLookup.Add(typeof(EmployeeBackgroundCheck), "qryBC");
            ChildGetAllLookup.Add(typeof(CheckType), "qryCT");

            ChildGetLookup.Add(typeof(Sponsor), "getSP");
            ChildGetLookup.Add(typeof(AllowanceType), "getAT");
            ChildGetLookup.Add(typeof(CertificateLevel), "getCL");
            ChildGetLookup.Add(typeof(TrainingType), "getTT");
            ChildGetLookup.Add(typeof(EntitlementDeduction), "getED");
            ChildGetLookup.Add(typeof(DocumentType), "getDT");
            ChildGetLookup.Add(typeof(SalaryChangeReason), "getSC");
            ChildGetLookup.Add(typeof(AssetCategory), "getAC");
            ChildGetLookup.Add(typeof(BonusType), "getBT");
            ChildGetLookup.Add(typeof(EmploymentHistory), "getEH");
            ChildGetLookup.Add(typeof(JobInfo), "getJI");
            ChildGetLookup.Add(typeof(EmployeeSalary), "getSA");
            ChildGetLookup.Add(typeof(Bonus), "getBO");
            ChildGetLookup.Add(typeof(SalaryDetail), "getSD");
            ChildGetLookup.Add(typeof(EmployeeNote), "getNO");
            ChildGetLookup.Add(typeof(EmployeeDocument), "getDO");
            ChildGetLookup.Add(typeof(EmployeeCertificate), "getCE");
            ChildGetLookup.Add(typeof(EmployeeRightToWork), "getRW");
            ChildGetLookup.Add(typeof(EmployeeBackgroundCheck), "getBC");
            ChildGetLookup.Add(typeof(CheckType), "getCT");

            ChildAddOrUpdateLookup.Add(typeof(Sponsor), "setSP");
            ChildAddOrUpdateLookup.Add(typeof(AllowanceType), "setAT");
            ChildAddOrUpdateLookup.Add(typeof(CertificateLevel), "setCL");
            ChildAddOrUpdateLookup.Add(typeof(TrainingType), "setTT");
            ChildAddOrUpdateLookup.Add(typeof(EntitlementDeduction), "setED");
            ChildAddOrUpdateLookup.Add(typeof(DocumentType), "setDT");
            ChildAddOrUpdateLookup.Add(typeof(SalaryChangeReason), "setSC");
            ChildAddOrUpdateLookup.Add(typeof(AssetCategory), "setAC");
            ChildAddOrUpdateLookup.Add(typeof(BonusType), "setBT");
            ChildAddOrUpdateLookup.Add(typeof(EmploymentHistory), "setEH");
            ChildAddOrUpdateLookup.Add(typeof(JobInfo), "setJI");
            ChildAddOrUpdateLookup.Add(typeof(EmploymentStatus), "setST");
            ChildAddOrUpdateLookup.Add(typeof(EmployeeSalary), "setSA");
            ChildAddOrUpdateLookup.Add(typeof(Bonus), "setBO");
            ChildAddOrUpdateLookup.Add(typeof(SalaryDetail[]), "arrSD");
            ChildAddOrUpdateLookup.Add(typeof(EmployeeNote), "setNO");
            ChildAddOrUpdateLookup.Add(typeof(EmployeeDocument), "setDO");
            ChildAddOrUpdateLookup.Add(typeof(EmployeeCertificate), "setCE");
            ChildAddOrUpdateLookup.Add(typeof(EmployeeRightToWork), "setRW");
            ChildAddOrUpdateLookup.Add(typeof(EmployeeBackgroundCheck), "setBC");
            ChildAddOrUpdateLookup.Add(typeof(CheckType), "setCT");

            ChildDeleteLookup.Add(typeof(Sponsor), "delSP");
            ChildDeleteLookup.Add(typeof(AllowanceType), "delAT");
            ChildDeleteLookup.Add(typeof(CertificateLevel), "delCL");
            ChildDeleteLookup.Add(typeof(TrainingType), "delTT");
            ChildDeleteLookup.Add(typeof(EntitlementDeduction), "delED");
            ChildDeleteLookup.Add(typeof(DocumentType), "delDT");
            ChildDeleteLookup.Add(typeof(SalaryChangeReason), "delSC");
            ChildDeleteLookup.Add(typeof(AssetCategory), "delAC");
            ChildDeleteLookup.Add(typeof(BonusType), "delBT");
            ChildDeleteLookup.Add(typeof(EmploymentHistory), "delEH");
            ChildDeleteLookup.Add(typeof(JobInfo), "delJI");
            ChildDeleteLookup.Add(typeof(EmployeeSalary), "delSA");
            ChildDeleteLookup.Add(typeof(Bonus), "delBO");
            ChildDeleteLookup.Add(typeof(SalaryDetail), "delSD");
            ChildDeleteLookup.Add(typeof(EmployeeNote), "delNO");
            ChildDeleteLookup.Add(typeof(EmployeeDocument), "delDO");
            ChildDeleteLookup.Add(typeof(EmployeeCertificate), "delCE");
            ChildDeleteLookup.Add(typeof(EmployeeRightToWork), "delRW");
            ChildDeleteLookup.Add(typeof(EmployeeBackgroundCheck), "delBC");
            ChildDeleteLookup.Add(typeof(CheckType), "delCT");

        }

  

    }
}
