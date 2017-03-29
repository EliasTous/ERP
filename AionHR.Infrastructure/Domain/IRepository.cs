using AionHR.Infrastructure.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Infrastructure.Domain
{
    /// <summary>
    /// Inteface to be implemented by all entities that need to exchange data
    /// </summary>
    /// <typeparam name="T">TEntity</typeparam>
    public interface IRepository<T, TID> where T :IEntity
    {

    
        RecordWebServiceResponse<T> GetRecord( Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null);

       
        ListWebServiceResponse<T> GetAll(Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null);

        PostWebServiceResponse AddOrUpdate(T entity, Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null);

        PostWebServiceResponse Delete(T entity, Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null);

         RecordWebServiceResponse<TChild> ChildGetRecord<TChild>(Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null);

         ListWebServiceResponse<TChild> ChildGetAll<TChild>(Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null);

        PostWebServiceResponse ChildAddOrUpdate<TChild>(TChild entity, Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null);

        PostWebServiceResponse ChildDelete<TChild>(TChild entity, Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null);
    }
}
