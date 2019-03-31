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
using AionHR.Model.System;

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
            DeleteMethodName = "delEM";

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
            ChildGetAllLookup.Add(typeof(Dependant), "qryDE");
            ChildGetAllLookup.Add(typeof(TerminationReason), "qryTR");
            ChildGetAllLookup.Add(typeof(AssetAllowance), "qryAA");
            ChildGetAllLookup.Add(typeof(EmployeeEmergencyContact), "qryEC");
            ChildGetAllLookup.Add(typeof(RelationshipType), "qryRT");
            ChildGetAllLookup.Add(typeof(TeamMember), "qryTM");
            ChildGetAllLookup.Add(typeof(EmployeeContact), "qryCO");
            ChildGetAllLookup.Add(typeof(NoticePeriod), "qryNP");
            ChildGetAllLookup.Add(typeof(EmployeeCalendar), "qryCA");
            ChildGetAllLookup.Add(typeof(EmployeeCal), "qryCA");
            ChildGetAllLookup.Add(typeof(EmployeePenalty), "qryPE");
            ChildGetAllLookup.Add(typeof(EmployeePenaltyApproval), "qryPA");
            

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
            ChildGetLookup.Add(typeof(Dependant), "getDE");
            ChildGetLookup.Add(typeof(TerminationReason), "getTR");
            ChildGetLookup.Add(typeof(AssetAllowance), "getAA");
            ChildGetLookup.Add(typeof(EmployeeEmergencyContact), "getEC");
            ChildGetLookup.Add(typeof(RelationshipType), "getRT");
            ChildGetLookup.Add(typeof(EmployeeQuickView), "getQV");
            ChildGetLookup.Add(typeof(EmployeeContact), "getCO");
            ChildGetLookup.Add(typeof(HireInfo), "getRE");
            ChildGetLookup.Add(typeof(Employee), "getEM2");
            ChildGetLookup.Add(typeof(EmployeeCount), "cntEM");
            ChildGetLookup.Add(typeof(EmployeeCalendar), "getCA");
            ChildGetLookup.Add(typeof(EmployeeCal), "getCA");

            ChildGetLookup.Add(typeof(NoticePeriod), "getNP");
            ChildGetLookup.Add(typeof(EmployeeTermination), "getTE");
            ChildGetLookup.Add(typeof(EmploymentStatus), "getST");
            ChildGetLookup.Add(typeof(EmploymentStatusByReferance), "getST2");
            ChildGetLookup.Add(typeof(EmployeePenalty), "getPE");





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
            ChildAddOrUpdateLookup.Add(typeof(Dependant), "setDE");
            ChildAddOrUpdateLookup.Add(typeof(SalaryDetail[]), "arrSD");
            ChildAddOrUpdateLookup.Add(typeof(EmployeeNote), "setNO");
            ChildAddOrUpdateLookup.Add(typeof(EmployeeDocument), "setDO");
            ChildAddOrUpdateLookup.Add(typeof(EmployeeCertificate), "setCE");
            ChildAddOrUpdateLookup.Add(typeof(EmployeeRightToWork), "setRW");
            ChildAddOrUpdateLookup.Add(typeof(EmployeeBackgroundCheck), "setBC");
            ChildAddOrUpdateLookup.Add(typeof(CheckType), "setCT");
            ChildAddOrUpdateLookup.Add(typeof(TerminationReason), "setTR");
            ChildAddOrUpdateLookup.Add(typeof(EmployeeTermination), "setTE");
            ChildAddOrUpdateLookup.Add(typeof(AssetAllowance), "setAA");
            ChildAddOrUpdateLookup.Add(typeof(EmployeeEmergencyContact), "setEC");
            ChildAddOrUpdateLookup.Add(typeof(RelationshipType), "setRT");
            ChildAddOrUpdateLookup.Add(typeof(EmployeeContact), "setCO");
            ChildAddOrUpdateLookup.Add(typeof(HireInfo), "setRE");
            ChildAddOrUpdateLookup.Add(typeof(NoticePeriod), "setNP");
            ChildAddOrUpdateLookup.Add(typeof(BatchEM), "batEM");
            ChildAddOrUpdateLookup.Add(typeof(EmployeeCalendar), "setCA");
            ChildAddOrUpdateLookup.Add(typeof(EmployeeCal), "setCA");
            ChildAddOrUpdateLookup.Add(typeof(EmployeePenalty), "setPE");
            ChildAddOrUpdateLookup.Add(typeof(EmployeePenaltyApproval), "setPA");
            ChildAddOrUpdateLookup.Add(typeof(SyncFullName), "syncFullName");
            ChildAddOrUpdateLookup.Add(typeof(ShareAttachment), "share");



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
            ChildDeleteLookup.Add(typeof(Dependant), "delDE");
            ChildDeleteLookup.Add(typeof(EmployeeCertificate), "delCE");
            ChildDeleteLookup.Add(typeof(EmployeeRightToWork), "delRW");
            ChildDeleteLookup.Add(typeof(EmployeeBackgroundCheck), "delBC");
            ChildDeleteLookup.Add(typeof(CheckType), "delCT");
            ChildDeleteLookup.Add(typeof(TerminationReason), "delTR");
            ChildDeleteLookup.Add(typeof(AssetAllowance), "delAA");
            ChildDeleteLookup.Add(typeof(EmployeeEmergencyContact), "delEC");
            ChildDeleteLookup.Add(typeof(EmployeeContact), "delCO");
            ChildDeleteLookup.Add(typeof(RelationshipType), "delRT");
            ChildDeleteLookup.Add(typeof(EmployeeCalendar), "delCA");
            ChildDeleteLookup.Add(typeof(NoticePeriod), "delNP");
            ChildDeleteLookup.Add(typeof(EmploymentStatus), "delST");
            ChildDeleteLookup.Add(typeof(EmployeeCal), "delCA");
            ChildDeleteLookup.Add(typeof(EmployeeTermination), "delTE");
            ChildDeleteLookup.Add(typeof(EmployeePenalty), "delPE");

        }

        public PostWebServiceResponse UploadEmployeePhoto(Attachement at, string fileName, byte[] fileData, Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null)
        {
            var request = new HTTPWebServiceRequest();
            request.MethodType = "POST";
            request.URL = ServiceURL + "setPI";
            if (Headers != null)
                request.Headers = Headers;
            if (QueryStringParams != null)
                request.QueryStringParams = QueryStringParams;
            List<string> filenames = new List<string>();
            filenames.Add(fileName);
            List<byte[]> filesData = new List<byte[]>();
            filesData.Add(fileData);
            return request.PostAsyncWithMultipleAttachments<Attachement>(at, filenames, filesData);
        }



    }
}
