using AionHR.Infrastructure.Configuration;
using AionHR.Infrastructure.Domain;
using AionHR.Model.Benefits;
using AionHR.Model.Company.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Repository.WebService.Repositories
{
 public   class BenefitsRepository : Repository<IEntity, string>, IBenefitsRepository
    {
        private string serviceName = "BE.asmx/";

        public BenefitsRepository()
        {
            base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;

            ChildGetAllLookup.Add(typeof(Benefit), "qryBE");
            ChildGetAllLookup.Add(typeof(BenefitsSchedule), "qrySC");
            ChildGetAllLookup.Add(typeof(ScheduleBenefits), "qrySB");
            ChildGetAllLookup.Add(typeof(BenefitAcquisition), "qryBA");



            ChildGetLookup.Add(typeof(Benefit), "getBE");
            ChildGetLookup.Add(typeof(BenefitsSchedule), "getSC");
            ChildGetLookup.Add(typeof(ScheduleBenefits), "getSB");
            ChildGetLookup.Add(typeof(BenefitAcquisition), "getBA");


            ChildAddOrUpdateLookup.Add(typeof(Benefit), "setBE");
            ChildAddOrUpdateLookup.Add(typeof(BenefitsSchedule), "setSC");
            ChildAddOrUpdateLookup.Add(typeof(ScheduleBenefits), "setSB");
            ChildAddOrUpdateLookup.Add(typeof(BenefitAcquisition), "setBA");


            // ChildAddOrUpdateLookup.Add(typeof(LeaveDay[]), "arrLD");

            ChildDeleteLookup.Add(typeof(Benefit), "delBE");
            ChildDeleteLookup.Add(typeof(BenefitsSchedule), "delSC");
            ChildDeleteLookup.Add(typeof(ScheduleBenefits), "delSB");
            ChildDeleteLookup.Add(typeof(BenefitAcquisition), "delBA");

        }
    }
}
