using Infrastructure.Configuration;
using Infrastructure.Domain;
using Model;
using Model.Attendance;
using Model.Dashboard;
using Model.HelpFunction;
using Model.Payroll;
using Model.TimeAttendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.TaskSchedule;

namespace Repository.WebService.Repositories
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
