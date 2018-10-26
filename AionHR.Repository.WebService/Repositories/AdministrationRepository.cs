using AionHR.Infrastructure.Configuration;
using AionHR.Infrastructure.Domain;
using AionHR.Model.AdminTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Repository.WebService.Repositories
{
    public class AdministrationRepository :Repository<IEntity, string>, IAdministrationRepository
    {
        private string serviceName = "AA.asmx/";
        public AdministrationRepository()
        {
            base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;

            ChildGetAllLookup.Add(typeof(AdTemplate), "qryTE");
            ChildGetAllLookup.Add(typeof(TemplateBody), "qryTB");
            ChildGetAllLookup.Add(typeof(TemplateTag), "qryTM");

            ChildGetLookup.Add(typeof(AdTemplate), "getTE");
            ChildGetLookup.Add(typeof(TemplateBody), "getTB");
            ChildGetLookup.Add(typeof(TemplateTag), "getTM");

            ChildAddOrUpdateLookup.Add(typeof(AdTemplate), "setTE");
            ChildAddOrUpdateLookup.Add(typeof(TemplateBody), "setTB");
            ChildAddOrUpdateLookup.Add(typeof(TemplateTag), "setTM");
            ChildAddOrUpdateLookup.Add(typeof(TemplateTag[]), "arrTM");

            ChildDeleteLookup.Add(typeof(AdTemplate), "delTE");
            ChildDeleteLookup.Add(typeof(TemplateBody), "delTB");
            ChildDeleteLookup.Add(typeof(TemplateTag), "delTM");
        }
        }
}
