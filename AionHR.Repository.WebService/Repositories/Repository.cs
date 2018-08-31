using AionHR.Infrastructure.Domain;
using AionHR.Infrastructure.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Repository.WebService.Repositories
{
    /// <summary>
    /// Abstract class for all the repositories used
    /// </summary>
    /// <typeparam name="T">T is an IEntity</typeparam>
    /// <typeparam name="TEntityKey">the key of the entity</typeparam>
    public abstract class Repository<T, TEntityKey> 
    {
        public string ServiceURL;
        protected string GetRecordMethodName;
        protected string GetAllMethodName;
        protected string AddOrUpdateMethodName;
        protected string DeleteMethodName;
        protected Dictionary<Type, string> ChildGetLookup;
        protected Dictionary<Type, string> ChildGetAllLookup;
        protected Dictionary<Type, string> ChildAddOrUpdateLookup;
        protected Dictionary<Type, string> ChildDeleteLookup;

        public Repository()
        {
            ChildGetLookup = new Dictionary<Type, string>();
            ChildGetAllLookup = new Dictionary<Type, string>();
            ChildAddOrUpdateLookup = new Dictionary<Type, string>();
            ChildDeleteLookup = new Dictionary<Type, string>();
        }

        /// <summary>
        /// Generic method for getting a record T from the webservice
        /// </summary>
        /// <param name="methodName">the methos inside the webservice</param>
        /// <param name="Headers">the addional data headers</param>
        /// <param name="QueryStringParams"> the query strings param shipped with the request</param>
        /// <returns>a record of the requested data inside the RecordWebserviceResponse</returns>
        public RecordWebServiceResponse<T> GetRecord( Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null)
        {
            var request = new HTTPWebServiceRequest();
            request.MethodType = "GET";
            request.URL = ServiceURL + GetRecordMethodName;
            if (Headers != null)
                request.Headers = Headers;
            if (QueryStringParams != null)
                request.QueryStringParams = QueryStringParams;

            return request.GetAsync<RecordWebServiceResponse<T>>();

        }

        /// <summary>
        /// Generic method for getting a list of record T from the webservice
        /// </summary>
        /// <param name="methodName">the methos inside the webservice</param>
        /// <param name="Headers">the addional data headers</param>
        /// <param name="QueryStringParams"> the query strings param shipped with the request</param>
        /// <returns>a list of records of the requested data inside the ListWebServiceResponse</returns>
        public ListWebServiceResponse<T> GetAll(Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null)
        {
            var request = new HTTPWebServiceRequest();
            request.MethodType = "GET";
            request.URL = ServiceURL + GetAllMethodName;
            if (Headers != null)
                request.Headers = Headers;
            if (QueryStringParams != null)
                request.QueryStringParams = QueryStringParams;


           return request.GetAsync<ListWebServiceResponse<T>>();

        }

        public PostWebServiceResponse AddOrUpdate( T entity, Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null)
        {
            var request = new HTTPWebServiceRequest();
            request.MethodType = "POST";
            request.URL = ServiceURL + AddOrUpdateMethodName;
            if (Headers != null)
                request.Headers = Headers;
            if (QueryStringParams != null)
                request.QueryStringParams = QueryStringParams;
            return request.PostAsyncFormData<T>(entity);
            
                
        }

        public PostWebServiceResponse AddOrUpdate(T entity,string fileName, byte[] fileData, Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null)
        {
            var request = new HTTPWebServiceRequest();
            request.MethodType = "POST";
            request.URL = ServiceURL + AddOrUpdateMethodName;
            if (Headers != null)
                request.Headers = Headers;
            if (QueryStringParams != null)
                request.QueryStringParams = QueryStringParams;
            return request.PostAsyncWithBinary<T>(entity, fileName, fileData);


        }

        public PostWebServiceResponse Delete(T entity, Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null)
        {
            var request = new HTTPWebServiceRequest();
            request.MethodType = "POST";
            request.URL = ServiceURL + DeleteMethodName;
            if (Headers != null)
                request.Headers = Headers;
            if (QueryStringParams != null)
                request.QueryStringParams = QueryStringParams;

            return request.PostAsyncFormData<T>(entity);
        }

       public RecordWebServiceResponse<TChild> ChildGetRecord<TChild>(Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null)
        {
            var request = new HTTPWebServiceRequest();
            request.MethodType = "GET";
            request.URL = ServiceURL + ChildGetLookup[typeof(TChild)];
            if (Headers != null)
                request.Headers = Headers;
            if (QueryStringParams != null)
                request.QueryStringParams = QueryStringParams;

            return request.GetAsync<RecordWebServiceResponse<TChild>>();
        }

        public ListWebServiceResponse<TChild> ChildGetAll<TChild>(Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null)
        {
            var request = new HTTPWebServiceRequest();
            request.MethodType = "GET";
            request.URL = ServiceURL + ChildGetAllLookup[typeof(TChild)];
            if (Headers != null)
                request.Headers = Headers;
            if (QueryStringParams != null)
                request.QueryStringParams = QueryStringParams;


            return request.GetAsync<ListWebServiceResponse<TChild>>();
        }

        public PostWebServiceResponse ChildAddOrUpdate<TChild>( TChild entity, Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null)
        {
            var request = new HTTPWebServiceRequest();
            request.MethodType = "POST";
            request.URL = ServiceURL + ChildAddOrUpdateLookup[typeof(TChild)];
            if (Headers != null)
                request.Headers = Headers;
            if (QueryStringParams != null)
                request.QueryStringParams = QueryStringParams;
            return request.PostAsyncFormData<TChild>(entity);
        }

        public PostWebServiceResponse ChildAddOrUpdate<TChild>(TChild entity,string fileName,byte[] fileData, Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null)
        {
            var request = new HTTPWebServiceRequest();
            request.MethodType = "POST";
            request.URL = ServiceURL + ChildAddOrUpdateLookup[typeof(TChild)];
            if (Headers != null)
                request.Headers = Headers;
            if (QueryStringParams != null)
                request.QueryStringParams = QueryStringParams;
            return request.PostAsyncWithBinary<TChild>(entity, fileName, fileData);
        }

        public PostWebServiceResponse ChildDelete<TChild>(TChild entity, Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null)
        {
            var request = new HTTPWebServiceRequest();
            request.MethodType = "POST";
            request.URL = ServiceURL + ChildDeleteLookup[typeof(TChild)];
            if (Headers != null)
                request.Headers = Headers;
            if (QueryStringParams != null)
                request.QueryStringParams = QueryStringParams;

            return request.PostAsyncFormData<TChild>(entity);
        }




                   }
}
