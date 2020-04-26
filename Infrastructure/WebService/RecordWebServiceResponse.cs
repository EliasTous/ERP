using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.WebService
{
    /// <summary>
    /// the object returned by webservice when requesting a unique item
    /// </summary>
    /// <typeparam name="T">IEntity</typeparam>
    public class RecordWebServiceResponse<T> : BaseWebServiceResponse
    {
        public T record { get; set; }
        public string recordId { get; set; }

    }
}
