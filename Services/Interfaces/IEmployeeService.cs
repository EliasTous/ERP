using Model.Employees;
using Model.Employees.Profile;
using Model.System;
using Services.Messaging;
using Services.Messaging.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IEmployeeService:IBaseService
    {
     
        PostResponse<SalaryDetail> DeleteSalaryDetails(int SalaryId);

        PostResponse<Attachement> UploadEmployeePhoto(EmployeeUploadPhotoRequest req);
        PostResponse<ShareAttachment> ShareEmployeeAttachments(ShareAttachmentPostRequest req);
    }
}
