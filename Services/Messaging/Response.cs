using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging
{
    /// <summary>
    /// Generic response result returne by services
    /// </summary>
    /// <typeparam name="T"> the generic type of objects to return</typeparam>
    public class Response<T>:ResponseBase
    {
        /// <summary>
        /// The generic object list
        /// </summary>
        public T result { get; set; }
        public string recordId { get; set; }

        /// <summary>
        /// Total record count : Used only for generic listing request
        /// </summary>
        public int rowCount { get; set; }
    }
}
