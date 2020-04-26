using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.WebService
{
    /// <summary>
    /// Interface for making a request to the webservice
    /// </summary>
    public interface IWebServiceRequest
    {
         T GetAsync<T>();
           
    }
}
