using Infrastructure.Configuration;
using Infrastructure.Domain;
using Model;
using Model.AdminTemplates;
using Model.Company.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.WebService.Repositories
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
            ChildGetAllLookup.Add(typeof(ProcessNotification), "qryPN");
            ChildGetAllLookup.Add(typeof(AdminDocumentDue), "qryDD");
            ChildGetAllLookup.Add(typeof(AdminDocTransfer), "qryDT");
            ChildGetAllLookup.Add(typeof(AdminDocumentDX), "qryDX");
            ChildGetAllLookup.Add(typeof(AdminDepartment), "qryDO");



            ChildGetLookup.Add(typeof(AdTemplate), "getTE");
            ChildGetLookup.Add(typeof(TemplateBody), "getTB");
            ChildGetLookup.Add(typeof(TemplateTag), "getTM");
            ChildGetLookup.Add(typeof(EmployeeTemplatePreview), "sampleTB");
            ChildGetLookup.Add(typeof(BusinessPartnerCategory), "getBC");
            ChildGetLookup.Add(typeof(BusinessPartner), "getBP");
            ChildGetLookup.Add(typeof(DocumentCategory), "getDC");
            ChildGetLookup.Add(typeof(AdminDocument), "getDO");
            ChildGetLookup.Add(typeof(AdminDocumentNote), "getDN");
            ChildGetLookup.Add(typeof(ProcessNotification), "getPN");
            ChildGetLookup.Add(typeof(AdminDocumentDue), "getDD");
            ChildGetLookup.Add(typeof(AdminDocTransfer), "getDT");
            ChildGetLookup.Add(typeof(AdminDocumentDX), "getDX");
            ChildGetLookup.Add(typeof(MailEmployee), "mailEM");


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
            ChildAddOrUpdateLookup.Add(typeof(ProcessNotification), "setPN");
            ChildAddOrUpdateLookup.Add(typeof(AdminDocumentDue), "setDD");
            ChildAddOrUpdateLookup.Add(typeof(AdminDocTransfer), "setDT");
            ChildAddOrUpdateLookup.Add(typeof(AdminDocumentDX), "setDX");
            ChildAddOrUpdateLookup.Add(typeof(AdminDepartment), "setDE");
            ChildAddOrUpdateLookup.Add(typeof(AdminDocumentDX[]), "aseDX");
          //  ChildAddOrUpdateLookup.Add(typeof(MailEmployee), "mailEM");
            

            ChildDeleteLookup.Add(typeof(AdTemplate), "delTE");
            ChildDeleteLookup.Add(typeof(TemplateBody), "delTB");
            ChildDeleteLookup.Add(typeof(TemplateTag), "delTM");
            ChildDeleteLookup.Add(typeof(BusinessPartnerCategory), "delBC");
            ChildDeleteLookup.Add(typeof(BusinessPartner), "delBP");
            ChildDeleteLookup.Add(typeof(DocumentCategory), "delDC");
            ChildDeleteLookup.Add(typeof(AdminDocument), "delDO");
            ChildDeleteLookup.Add(typeof(AdminDocumentNote), "delDN");
            ChildDeleteLookup.Add(typeof(ProcessNotification), "delPN");
            ChildDeleteLookup.Add(typeof(AdminDocumentDue), "delDD");
            ChildDeleteLookup.Add(typeof(AdminDocTransfer), "delDT");
            ChildDeleteLookup.Add(typeof(AdminDocumentDX), "delDX");
            ChildDeleteLookup.Add(typeof(AdminDepartment), "delDE");
            ChildDeleteLookup.Add(typeof(AdminDocumentDX[]), "adeDX ");
        }
        }
}
