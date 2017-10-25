 using AionHR.Infrastructure.Configuration;
using AionHR.Infrastructure.Domain;
using AionHR.Model.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Repository.WebService.Repositories
{
    public class PayrollRepository:Repository<IEntity,string>,IPayrollRepository
    {
        private string serviceName = "PY.asmx/";

        public PayrollRepository()
        {
            base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;

            ChildGetAllLookup.Add(typeof(FiscalYear), "qryYE");
            ChildGetAllLookup.Add(typeof(GenerationHeader), "qryHE");
            ChildGetAllLookup.Add(typeof(FiscalPeriod), "qryPE");
            ChildGetAllLookup.Add(typeof(EmployeePayroll), "qryEM");
            ChildGetAllLookup.Add(typeof(PayrollEntitlementDeduction), "qryED");
            ChildGetAllLookup.Add(typeof(TimeSchedule), "qryTS");
            ChildGetAllLookup.Add(typeof(TimeCode), "qryTC");
            ChildGetAllLookup.Add(typeof(SocialSecuritySchedule), "qrySS");
            ChildGetAllLookup.Add(typeof(SocialSecurityScheduleSetup), "qrySC");
            ChildGetAllLookup.Add(typeof(FinalSettlement), "qryFS");
            ChildGetAllLookup.Add(typeof(FinalEntitlementsDeductions), "qryFD");
            ChildGetAllLookup.Add(typeof(PayCode), "qryPC");
            ChildGetAllLookup.Add(typeof(PayrollSocialSecurity), "qryES");
            ChildGetAllLookup.Add(typeof(PayrollIndemnity), "qryIS");
            ChildGetAllLookup.Add(typeof(PayrollIndemnityDetails), "qryID");
            ChildGetAllLookup.Add(typeof(PayrollIndemnityRecognition), "qryIR");


            ChildAddOrUpdateLookup.Add(typeof(FiscalYear), "setYE");
            ChildAddOrUpdateLookup.Add(typeof(GenerationHeader), "setHE");
            ChildAddOrUpdateLookup.Add(typeof(EmployeePayroll), "setEM");
            ChildAddOrUpdateLookup.Add(typeof(PayrollEntitlementDeduction[]), "arrED");
            ChildAddOrUpdateLookup.Add(typeof(PayrollEntitlementDeduction), "setED");
            ChildAddOrUpdateLookup.Add(typeof(SyncED), "syncED");
            ChildAddOrUpdateLookup.Add(typeof(TimeSchedule), "setTS");
            ChildAddOrUpdateLookup.Add(typeof(TimeCode[]), "arrTC");
            ChildAddOrUpdateLookup.Add(typeof(SocialSecuritySchedule), "setSS");
            ChildAddOrUpdateLookup.Add(typeof(SocialSecurityScheduleSetup), "setSC");
            ChildAddOrUpdateLookup.Add(typeof(FinalSettlement), "setFS");
            ChildAddOrUpdateLookup.Add(typeof(FinalEntitlementsDeductions), "setFD");
            ChildAddOrUpdateLookup.Add(typeof(PayCode), "setPC");
            ChildAddOrUpdateLookup.Add(typeof(PayrollIndemnity), "setIS");
            ChildAddOrUpdateLookup.Add(typeof(PayrollIndemnityDetails), "setID");
            ChildAddOrUpdateLookup.Add(typeof(PayrollIndemnityDetails[]), "arrID");
            ChildAddOrUpdateLookup.Add(typeof(PayrollIndemnityRecognition[]), "arrIR");


            ChildDeleteLookup.Add(typeof(FiscalYear), "delYE");
            ChildDeleteLookup.Add(typeof(PayrollEntitlementDeduction), "delED");
            ChildDeleteLookup.Add(typeof(GenerationHeader), "delHE");
            ChildDeleteLookup.Add(typeof(TimeSchedule), "delTS");
            ChildDeleteLookup.Add(typeof(SocialSecuritySchedule), "delSS");
            ChildDeleteLookup.Add(typeof(SocialSecurityScheduleSetup), "delSC");
            ChildDeleteLookup.Add(typeof(FinalSettlement), "delFS");
            ChildDeleteLookup.Add(typeof(FinalEntitlementsDeductions), "delFD");
            ChildDeleteLookup.Add(typeof(PayCode), "delPC");
            ChildDeleteLookup.Add(typeof(PayrollIndemnity), "delIS");
            ChildDeleteLookup.Add(typeof(PayrollIndemnityDetails), "delID");
            ChildDeleteLookup.Add(typeof(PayrollIndemnityRecognition), "delIR");


            ChildGetLookup.Add(typeof(GenerationHeader), "getHE");
            ChildGetLookup.Add(typeof(TimeSchedule), "getTS");
            ChildGetLookup.Add(typeof(SocialSecuritySchedule), "getSS");
            ChildGetLookup.Add(typeof(SocialSecurityScheduleSetup), "getSC");
            ChildGetLookup.Add(typeof(FinalSettlement), "getFS");
            ChildGetLookup.Add(typeof(FinalEntitlementsDeductions), "getFD");
            ChildGetLookup.Add(typeof(PayCode), "getPC");
            ChildGetLookup.Add(typeof(PayrollIndemnity), "getIS");
            ChildGetLookup.Add(typeof(PayrollIndemnityDetails), "getID");
            ChildGetLookup.Add(typeof(PayrollIndemnityRecognition), "getIR");
        }
    }
}
