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
            ChildGetAllLookup.Add(typeof(EmployeementHistory), "qryEH");
            ChildGetAllLookup.Add(typeof(JobInfo), "qryJI");


            ChildGetLookup.Add(typeof(Sponsor), "getSP");
            ChildGetLookup.Add(typeof(AllowanceType), "getAT");
            ChildGetLookup.Add(typeof(CertificateLevel), "getCL");
            ChildGetLookup.Add(typeof(TrainingType), "getTT");
            ChildGetLookup.Add(typeof(EntitlementDeduction), "getED");
            ChildGetLookup.Add(typeof(DocumentType), "getDT");
            ChildGetLookup.Add(typeof(SalaryChangeReason), "getSC");
            ChildGetLookup.Add(typeof(AssetCategory), "getAC");
            ChildGetLookup.Add(typeof(BonusType), "getBT");

            ChildAddOrUpdateLookup.Add(typeof(Sponsor), "setSP");
            ChildAddOrUpdateLookup.Add(typeof(AllowanceType), "setAT");
            ChildAddOrUpdateLookup.Add(typeof(CertificateLevel), "setCL");
            ChildAddOrUpdateLookup.Add(typeof(TrainingType), "setTT");
            ChildAddOrUpdateLookup.Add(typeof(EntitlementDeduction), "setED");
            ChildAddOrUpdateLookup.Add(typeof(DocumentType), "setDT");
            ChildAddOrUpdateLookup.Add(typeof(SalaryChangeReason), "setSC");
            ChildAddOrUpdateLookup.Add(typeof(AssetCategory), "setAC");
            ChildAddOrUpdateLookup.Add(typeof(BonusType), "setBT");

            ChildDeleteLookup.Add(typeof(Sponsor), "delSP");
            ChildDeleteLookup.Add(typeof(AllowanceType), "delAT");
            ChildDeleteLookup.Add(typeof(CertificateLevel), "delCL");
            ChildDeleteLookup.Add(typeof(TrainingType), "delTT");
            ChildDeleteLookup.Add(typeof(EntitlementDeduction), "delED");
            ChildDeleteLookup.Add(typeof(DocumentType), "delDT");
            ChildDeleteLookup.Add(typeof(SalaryChangeReason), "delSC");
            ChildDeleteLookup.Add(typeof(AssetCategory), "delAC");
            ChildDeleteLookup.Add(typeof(BonusType), "delBT");
        }

        public PostWebServiceResponse AddOrUpdateEmployeeWithImage(Employee emp, string imgName, byte[] imgDate,Dictionary<string,string> headers = null)
        {
            var request = new HTTPWebServiceRequest();
            request.MethodType = "POST";
            request.URL = ServiceURL + addOrRemoveEmployeeWithImageMethodName;
            if (headers != null)
                request.Headers = headers;

            return request.PostAsyncWithBinary<Employee>(emp, imgName, imgDate);
        }
    }
}
