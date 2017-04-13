using AionHR.Infrastructure.Configuration;
using AionHR.Infrastructure.Domain;
using AionHR.Model.TaskManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Repository.WebService.Repositories
{
    public class TaskManagementRepository:Repository<Model.TaskManagement.Task, string>,ITaskManagementRepository
    {
        private string serviceName = "TM.asmx/";
        public TaskManagementRepository()
        {
            base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;
            GetAllMethodName = "qryTA";
            AddOrUpdateMethodName = "setTA";
            GetRecordMethodName = "getTA";
            DeleteMethodName = "delTA";

            ChildGetLookup.Add(typeof(TaskType), "getTT");
            ChildGetAllLookup.Add(typeof(TaskType), "qryTT");
            ChildAddOrUpdateLookup.Add(typeof(TaskType), "setTT");
            ChildDeleteLookup.Add(typeof(TaskType), "delTT");

            ChildGetLookup.Add(typeof(TaskComment), "getTC");
            ChildGetAllLookup.Add(typeof(TaskComment), "qryTC");
            ChildAddOrUpdateLookup.Add(typeof(TaskComment), "setTC");
            ChildDeleteLookup.Add(typeof(TaskComment), "delTC");
        }
    }
}
