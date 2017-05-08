using AionHR.Infrastructure.Configuration;
using AionHR.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionHR.Infrastructure.WebService;
using AionHR.Infrastructure.Domain;
using AionHR.Model.MasterModule;

namespace AionHR.Repository.WebService.Repositories
{
    /// <summary>
    /// Class that handle the communcation between the model and the webservice. it encapsultes all the user relative methods
    /// </summary>
    public class SystemRepository : Repository<UserInfo, string>, ISystemRepository,ICommonRepository
    {
        
       // the webservice name       
        private string serviceName = "SY.asmx/";
        public SystemRepository()
        {            
            base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName ;

            ChildAddOrUpdateLookup.Add(typeof(Nationality), "setNA");
            ChildAddOrUpdateLookup.Add(typeof(Currency), "setCU");
            ChildAddOrUpdateLookup.Add(typeof(UserInfo), "setUS");
            ChildAddOrUpdateLookup.Add(typeof(SystemFolder), "setFO");
            ChildAddOrUpdateLookup.Add(typeof(CompanyDocumentType), "setDT");
            ChildAddOrUpdateLookup.Add(typeof(CompanyRightToWork), "setRW");
            ChildAddOrUpdateLookup.Add(typeof(Attachement), "setAT");
            ChildAddOrUpdateLookup.Add(typeof(State), "setST");



            ChildGetLookup.Add(typeof(UserInfo), "getUS");
            ChildGetLookup.Add(typeof(TransactionLog), "getTL");
            ChildGetLookup.Add(typeof(SystemFolder), "getFO");
            ChildGetLookup.Add(typeof(KeyValuePair<string, string>), "getDE");
            ChildGetLookup.Add(typeof(Currency), "getCU");
            ChildGetLookup.Add(typeof(Nationality), "getNA");
            ChildGetLookup.Add(typeof(CompanyDocumentType), "getDT");
            ChildGetLookup.Add(typeof(CompanyRightToWork), "getRW");
            ChildGetLookup.Add(typeof(State), "getST");


            ChildDeleteLookup.Add(typeof(Nationality) ,"delNA");
            ChildDeleteLookup.Add(typeof(UserInfo), "delUS");
            ChildDeleteLookup.Add(typeof(Currency), "delCU");
            ChildDeleteLookup.Add(typeof(Attachement), "delAT");
            ChildDeleteLookup.Add(typeof(SystemFolder), "delFO");
            ChildDeleteLookup.Add(typeof(CompanyDocumentType), "delDT");
            ChildDeleteLookup.Add(typeof(CompanyRightToWork), "delRW");
            ChildDeleteLookup.Add(typeof(State), "delST");



            ChildGetAllLookup.Add(typeof(Attachement), "qryAT");
            ChildGetAllLookup.Add(typeof(KeyValuePair<string,string>), "qryDE");
            ChildGetAllLookup.Add(typeof(TransactionLog), "qryTL");
            ChildGetAllLookup.Add(typeof(SystemFolder), "qryFO");
            ChildGetAllLookup.Add(typeof(Nationality), "qryNA");
            ChildGetAllLookup.Add(typeof(Currency), "qryCU");
            ChildGetAllLookup.Add(typeof(UserInfo), "qryUS");
            ChildGetAllLookup.Add(typeof(State), "qryST");
            ChildGetAllLookup.Add(typeof(CompanyDocumentType), "qryDT");
            ChildGetAllLookup.Add(typeof(CompanyRightToWork), "qryRW");
            ChildGetAllLookup.Add(typeof(SystemAlert), "qryAA");
            ChildGetAllLookup.Add(typeof(DashboardItem), "dashBoard");




            ChildAddOrUpdateLookup.Add(typeof(KeyValuePair<string, string>[]), "arrDE");
            ChildAddOrUpdateLookup.Add(typeof(SystemAlert[]), "arrAA");


            ChildAddOrUpdateLookup.Add(typeof(BatchSql), "batchSQL");


        }

        public RecordWebServiceResponse<UserInfo> Authenticate(Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null)
        {
            var request = new HTTPWebServiceRequest();
            request.MethodType = "GET";
            request.URL = ServiceURL + "signIn";
            if (Headers != null)
                request.Headers = Headers;
            if (QueryStringParams != null)
                request.QueryStringParams = QueryStringParams;

            return request.GetAsync<RecordWebServiceResponse<UserInfo>>();

        }

        public RecordWebServiceResponse<UserInfo> RequestPasswordRecovery(Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null)
        {
            var request = new HTTPWebServiceRequest();
            request.MethodType = "GET";
            request.URL = ServiceURL + "reqPW";
            if (Headers != null)
                request.Headers = Headers;
            if (QueryStringParams != null)
                request.QueryStringParams = QueryStringParams;

            return request.GetAsync<RecordWebServiceResponse<UserInfo>>();

        }

        public RecordWebServiceResponse<UserInfo> ResetPassword(Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null)
        {
            var request = new HTTPWebServiceRequest();
            request.MethodType = "GET";
            request.URL = ServiceURL + "resetPW";
            if (Headers != null)
                request.Headers = Headers;
            if (QueryStringParams != null)
                request.QueryStringParams = QueryStringParams;

            return request.GetAsync<RecordWebServiceResponse<UserInfo>>();

        }

        public PostWebServiceResponse UploadMultipleAttachments(Attachement at, List<string> fileNames,List<byte[]> filesData,Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null)
        {
            var request = new HTTPWebServiceRequest();
            request.MethodType = "POST";
            request.URL = ServiceURL + "setAT";
            if (Headers != null)
                request.Headers = Headers;
            if (QueryStringParams != null)
                request.QueryStringParams = QueryStringParams;
            return request.PostAsyncWithMultipleAttachments<Attachement>(at, fileNames, filesData);
        }


    }
}
