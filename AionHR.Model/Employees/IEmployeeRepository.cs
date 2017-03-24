using AionHR.Infrastructure.Domain;
using AionHR.Infrastructure.WebService;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees
{
    /// <summary>
    /// Interface for the EmployeeRepository
    /// </summary>
   public interface IEmployeeRepository:IRepository<Employee,string>,ICommonRepository
    {
        PostWebServiceResponse AddOrUpdateEmployeeWithImage(Employee emp, string imgName, byte[] imgDate,Dictionary<string,string> headers = null);

        PostWebServiceResponse AddOrUpdateEmployeeDocument(EmployeeDocument emp, string fileName, byte[] fileData, Dictionary<string, string> headers = null);
    }
}
