using AionHR.Infrastructure.Configuration;
using AionHR.Model.EmployeeComplaints;
using AionHR.Model.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Repository.WebService.Repositories
{
    public class ComplaintsRepository :  Repository<Complaint, string>, IComplaintsRepository
    {
        private string serviceName = "EC.asmx/";
      
        public ComplaintsRepository()
        {

            base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;
            GetAllMethodName = "qryCO";
            AddOrUpdateMethodName = "setCO";
            GetRecordMethodName = "getCO";
            DeleteMethodName = "delCO";



        }
    }
}
