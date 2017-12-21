using AionHR.Infrastructure.Configuration;
using AionHR.Infrastructure.Domain;
using AionHR.Model.NationalQuota;
using AionHR.Model.SelfService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Repository.WebService.Repositories
{
  public  class NationalQuotaRepository: Repository<IEntity, string>, INationalQuotaRepository
    {
        private string serviceName = "NQ.asmx/";
        public NationalQuotaRepository()
        {
            base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;

            ChildGetAllLookup.Add(typeof(Industry), "qryIN");
            ChildGetAllLookup.Add(typeof(BusinessSize), "qryBS");
            ChildGetAllLookup.Add(typeof(Level), "qryLE");
            ChildGetAllLookup.Add(typeof(Citizenship), "qryCI");
            ChildGetAllLookup.Add(typeof(PointAcquisition), "qryPA");




            ChildGetLookup.Add(typeof(Industry), "getIN");
            ChildGetLookup.Add(typeof(BusinessSize), "getBS");
            ChildGetLookup.Add(typeof(Level), "getLE");
            ChildGetLookup.Add(typeof(Citizenship), "getCI");
            ChildGetLookup.Add(typeof(PointAcquisition), "getPA");



            ChildAddOrUpdateLookup.Add(typeof(Industry), "setIN");
            ChildAddOrUpdateLookup.Add(typeof(BusinessSize), "setBS");
            ChildAddOrUpdateLookup.Add(typeof(Level), "setLE");
            ChildAddOrUpdateLookup.Add(typeof(Citizenship), "setCI");
            ChildAddOrUpdateLookup.Add(typeof(Citizenship[]), "arrCI");
            ChildAddOrUpdateLookup.Add(typeof(PointAcquisition), "setPA");



            ChildDeleteLookup.Add(typeof(Industry), "delIN");
            ChildDeleteLookup.Add(typeof(BusinessSize), "delBS");
            ChildDeleteLookup.Add(typeof(Level), "delLE");
            ChildDeleteLookup.Add(typeof(Citizenship), "delCI");
            ChildDeleteLookup.Add(typeof(PointAcquisition), "delPA");
        }
    }
}
