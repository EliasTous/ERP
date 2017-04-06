using AionHR.Model.Employees;
using AionHR.Model.Employees.Profile;
using AionHR.Model.System;
using AionHR.Services.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Interfaces
{
    public interface IEmployeeService:IBaseService
    {
     
        PostResponse<SalaryDetail> DeleteSalaryDetails(int SalaryId);

        PostResponse<Attachement> UploadEmployeePhoto(EmployeeUploadPhotoRequest req);
    }
}
