using AionHR.Infrastructure.Configuration;
using AionHR.Infrastructure.Domain;
using AionHR.Model.Employees.Profile;
using AionHR.Model.SelfService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Repository.WebService.Repositories
{
   
        public class SelfServiceRepository : Repository<IEntity, string>, ISelfServiceRepository
        {
            private string serviceName = "SS.asmx/";

            public SelfServiceRepository()
            {
                base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;

                ChildGetAllLookup.Add(typeof(MyInfo), "qryEM");



                ChildGetLookup.Add(typeof(MyInfo), "getEM1");


            ChildAddOrUpdateLookup.Add(typeof(MyInfo), "setEM");
            ChildAddOrUpdateLookup.Add(typeof(leaveRequetsSelfservice), "setLE");
            ChildAddOrUpdateLookup.Add(typeof(loanSelfService), "setLO");


            ChildDeleteLookup.Add(typeof(MyInfo), "delEM");
        }
        }
    
}
