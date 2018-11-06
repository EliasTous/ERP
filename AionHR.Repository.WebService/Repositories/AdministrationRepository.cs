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
            ChildGetAllLookup.Add(typeof(BusinessPartnerCategory), "qryBC");
            ChildGetAllLookup.Add(typeof(BusinessPartner), "qryBP");
            ChildGetAllLookup.Add(typeof(DocumentCategory), "qryDC");
            ChildGetAllLookup.Add(typeof(AdminDocument), "qryDO");
            ChildGetAllLookup.Add(typeof(AdminDocumentNote), "qryDN");


            ChildGetLookup.Add(typeof(AdTemplate), "getTE");
            ChildGetLookup.Add(typeof(TemplateBody), "getTB");
            ChildGetLookup.Add(typeof(TemplateTag), "getTM");
            ChildGetLookup.Add(typeof(EmployeeTemplatePreview), "sampleTB");
            ChildGetLookup.Add(typeof(BusinessPartnerCategory), "getBC");
            ChildGetLookup.Add(typeof(BusinessPartner), "getBP");
            ChildGetLookup.Add(typeof(DocumentCategory), "getDC");
            ChildGetLookup.Add(typeof(AdminDocument), "getDO");
            ChildGetLookup.Add(typeof(AdminDocumentNote), "getDN");

            ChildAddOrUpdateLookup.Add(typeof(AdTemplate), "setTE");
            ChildAddOrUpdateLookup.Add(typeof(TemplateBody), "setTB");
            ChildAddOrUpdateLookup.Add(typeof(TemplateTag), "setTM");
            ChildAddOrUpdateLookup.Add(typeof(TemplateTag[]), "arrTM");
            ChildAddOrUpdateLookup.Add(typeof(BusinessPartnerCategory), "setBC");
            ChildAddOrUpdateLookup.Add(typeof(BusinessPartner), "setBP");
            ChildAddOrUpdateLookup.Add(typeof(DocumentCategory), "setDC");
            ChildAddOrUpdateLookup.Add(typeof(AdminDocument), "setDO");
            ChildAddOrUpdateLookup.Add(typeof(AdminDocumentNote), "setDN");
            ChildAddOrUpdateLookup.Add(typeof(GenerateAdminDocumentDue), "genDD");

            ChildDeleteLookup.Add(typeof(AdTemplate), "delTE");
            ChildDeleteLookup.Add(typeof(TemplateBody), "delTB");
            ChildDeleteLookup.Add(typeof(TemplateTag), "delTM");
            ChildDeleteLookup.Add(typeof(BusinessPartnerCategory), "delBC");
            ChildDeleteLookup.Add(typeof(BusinessPartner), "delBP");
            ChildDeleteLookup.Add(typeof(DocumentCategory), "delDC");
            ChildDeleteLookup.Add(typeof(AdminDocument), "delDO");
            ChildDeleteLookup.Add(typeof(AdminDocumentNote), "delDN");
        }
        }
}
