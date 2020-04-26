using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Session;
using Services.Messaging;
using Model.Company.Structure;
using Infrastructure.Domain;

namespace Services.Interfaces
{
    public interface ICompanyStructureService : IBaseService
    {
        RecordResponse<Department> GetDepartmentByReference(DepartmentByReference request);
    }
}
