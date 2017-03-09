using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Infrastructure.WebService
{
    /// <summary>
    /// The object returned by a webserice call
    /// </summary>
    /// <typeparam name="T">IEntity</typeparam>
    public class ListWebServiceResponse<T> : BaseWebServiceResponse
    {

        public T[] list;

        public int count;

        public List<T> GetAll()
        {
            if (list != null)
                return list.ToList<T>();
            else return new List<T>();
        }


    }

}
