using AionHR.Infrastructure.Configuration;
using AionHR.Infrastructure.Domain;
using AionHR.Model.Employees.Profile;
using AionHR.Model.LeaveManagement;
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
            ChildGetAllLookup.Add(typeof(LetterSelfservice), "qryLE");



            ChildGetLookup.Add(typeof(MyInfo), "getEM1");


            ChildAddOrUpdateLookup.Add(typeof(MyInfo), "setEM");
            ChildAddOrUpdateLookup.Add(typeof(leaveRequetsSelfservice), "setLR");
            ChildAddOrUpdateLookup.Add(typeof(loanSelfService), "setLO");
            ChildAddOrUpdateLookup.Add(typeof(LetterSelfservice), "setLE");
            ChildAddOrUpdateLookup.Add(typeof(LeaveDay[]), "arrLD");

            ChildDeleteLookup.Add(typeof(MyInfo), "delEM");
        }
        }
    
}
