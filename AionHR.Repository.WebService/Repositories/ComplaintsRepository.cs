using Infrastructure.Configuration;
using Model.EmployeeComplaints;
using Model.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.WebService.Repositories
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
