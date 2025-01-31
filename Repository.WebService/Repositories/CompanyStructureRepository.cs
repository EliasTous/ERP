﻿using Model.Company.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.WebService;
using Infrastructure.Configuration;
using Infrastructure.Domain;

namespace Repository.WebService.Repositories
{
    public class CompanyStructureRepository : Repository<IEntity,string>, ICompanyStructureRepository
    {
        private string serviceName = "CS.asmx/";
        public CompanyStructureRepository()
        {

            base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;
            
            base.ChildGetLookup.Add(typeof(Branch), "getBR");
            base.ChildGetLookup.Add(typeof(Department), "getDE");
            base.ChildGetLookup.Add(typeof(Position), "getPO");
            base.ChildGetLookup.Add(typeof(Division), "getDI");
            base.ChildGetLookup.Add(typeof(LegalReference), "getBL");
            base.ChildGetLookup.Add(typeof(Approval), "getAP");
            base.ChildGetLookup.Add(typeof(ApprovelDepartment), "getAD");
            base.ChildGetLookup.Add(typeof(ApprovalReason), "getAR");
            base.ChildGetLookup.Add(typeof(WorkFlow), "getWF");
            base.ChildGetLookup.Add(typeof(WorkSequence), "getWS");
            base.ChildGetLookup.Add(typeof(Rule), "getRU");
            base.ChildGetLookup.Add(typeof(RuleCondition), "getRC");
            base.ChildGetLookup.Add(typeof(RuleMessage), "getRM");

            base.ChildGetAllLookup.Add(typeof(Branch), "qryBR");
            base.ChildGetAllLookup.Add(typeof(Department), "qryDE");
            base.ChildGetAllLookup.Add(typeof(Position), "qryPO");
            base.ChildGetAllLookup.Add(typeof(Division), "qryDI");
            base.ChildGetAllLookup.Add(typeof(LegalReference), "qryBL");
            base.ChildGetAllLookup.Add(typeof(Approval), "qryAP");
            base.ChildGetAllLookup.Add(typeof(ApprovelDepartment), "qryAD");
            base.ChildGetAllLookup.Add(typeof(ApprovalReason), "qryAR");
            base.ChildGetAllLookup.Add(typeof(WorkFlow), "qryWF");
            base.ChildGetAllLookup.Add(typeof(WorkSequence), "qryWS");
            base.ChildGetAllLookup.Add(typeof(Rule), "qryRU");
            base.ChildGetAllLookup.Add(typeof(RuleCondition), "qryRC");
            base.ChildGetAllLookup.Add(typeof(RuleMessage), "qryRM");
            base.ChildGetAllLookup.Add(typeof(RuleTrigger), "qryRT");


            base.ChildAddOrUpdateLookup.Add(typeof(Branch), "setBR");
            base.ChildAddOrUpdateLookup.Add(typeof(Department), "setDE");
            base.ChildAddOrUpdateLookup.Add(typeof(Position), "setPO");
            base.ChildAddOrUpdateLookup.Add(typeof(Division), "setDI");
            base.ChildAddOrUpdateLookup.Add(typeof(LegalReference), "setBL");
            base.ChildAddOrUpdateLookup.Add(typeof(Approval), "setAP");
            base.ChildAddOrUpdateLookup.Add(typeof(ApprovelDepartment), "setAD");
            base.ChildAddOrUpdateLookup.Add(typeof(ApprovalReason), "setAR");
            base.ChildAddOrUpdateLookup.Add(typeof(WorkFlow), "setWF");
            base.ChildAddOrUpdateLookup.Add(typeof(WorkSequence), "setWS");
            base.ChildAddOrUpdateLookup.Add(typeof(Rule), "setRU");
            base.ChildAddOrUpdateLookup.Add(typeof(RuleCondition), "setRC");
            base.ChildAddOrUpdateLookup.Add(typeof(RuleMessage), "setRM");
            base.ChildAddOrUpdateLookup.Add(typeof(RuleTrigger), "setRT");

            ChildDeleteLookup.Add(typeof(Branch), "delBR");
            ChildDeleteLookup.Add(typeof(Department), "delDE");
            ChildDeleteLookup.Add(typeof(Position), "delPO");
            ChildDeleteLookup.Add(typeof(Division), "delDI");
            ChildDeleteLookup.Add(typeof(Approval), "delAP");
            ChildDeleteLookup.Add(typeof(ApprovelDepartment), "delAD");
            ChildDeleteLookup.Add(typeof(LegalReference), "delBL");
            ChildDeleteLookup.Add(typeof(ApprovalReason), "delAR");
            ChildDeleteLookup.Add(typeof(WorkFlow), "delWF");
            ChildDeleteLookup.Add(typeof(WorkSequence), "delWS");

            ChildDeleteLookup.Add(typeof(Rule), "delRU");
            ChildDeleteLookup.Add(typeof(RuleCondition), "delRC");
            ChildDeleteLookup.Add(typeof(RuleMessage), "delRM");
            ChildDeleteLookup.Add(typeof(RuleTrigger), "delRT");



            base.GetAllMethodName = "";
            base.GetRecordMethodName = "";
            base.DeleteMethodName = "";
            base.AddOrUpdateMethodName = "";
        }

        public RecordWebServiceResponse<Department> GetDepartmentByReference(Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null)
        {
            var request = new HTTPWebServiceRequest();
            request.MethodType = "GET";
            request.URL = ServiceURL + "getDE2";
            if (Headers != null)
                request.Headers = Headers;
            if (QueryStringParams != null)
                request.QueryStringParams = QueryStringParams;

            return request.GetAsync<RecordWebServiceResponse<Department>>();
        }
    }
}
