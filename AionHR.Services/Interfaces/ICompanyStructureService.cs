using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionHR.Infrastructure.Session;
using AionHR.Services.Messaging;
using AionHR.Model.Company.Structure;
using AionHR.Infrastructure.Domain;

namespace AionHR.Services.Interfaces
{
    public interface ICompanyStructureService : IBaseService
    {
        RecordResponse<Department> GetDepartmentByReference(DepartmentByReference request);
    }
}
