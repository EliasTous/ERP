using AionHR.Infrastructure.Configuration;
using AionHR.Infrastructure.Domain;
using AionHR.Model;
using AionHR.Model.Attendance;
using AionHR.Model.Dashboard;
using AionHR.Model.HelpFunction;
using AionHR.Model.Payroll;
using AionHR.Model.TimeAttendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionHR.Model.TaskSchedule;

namespace AionHR.Repository.WebService.Repositories
{
    public class TaskScheduleRepository : Repository<IEntity, string>, ITaskScheduleRepository
    {
        private string serviceName = "TS.asmx/";

        public TaskScheduleRepository()
        {
            base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;

            ChildGetLookup.Add(typeof(Service), "getTH");
            ChildGetLookup.Add(typeof(Report), "getRE");
            ChildGetLookup.Add(typeof(Receiver), "getSG");
           


            ChildGetAllLookup.Add(typeof(Service), "qryTH");
            ChildGetAllLookup.Add(typeof(Report), "qryRE");
            ChildGetAllLookup.Add(typeof(Receiver), "qrySG");
           


            
            ChildAddOrUpdateLookup.Add(typeof(Service), "setTH");
            ChildAddOrUpdateLookup.Add(typeof(Report), "setRE");
            ChildAddOrUpdateLookup.Add(typeof(Receiver), "setSG");

           
            ChildDeleteLookup.Add(typeof(Service), "delTH");
            ChildDeleteLookup.Add(typeof(Report), "delRE");
            ChildDeleteLookup.Add(typeof(Receiver), "delSG");


            //Flat Schedule



        }

    }
}
