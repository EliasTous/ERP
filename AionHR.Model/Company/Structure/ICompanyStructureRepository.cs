using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionHR.Infrastructure.WebService;

namespace AionHR.Model.Company.Structure
{
    public interface ICompanyStructureRepository : IRepository<IEntity, string>,ICommonRepository
    {
        RecordWebServiceResponse<Department> GetDepartmentByReference(Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null);
    }
}
