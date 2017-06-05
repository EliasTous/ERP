using AionHR.Infrastructure.Configuration;
using AionHR.Infrastructure.Domain;
using AionHR.Model.Access_Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Repository.WebService.Repositories
{
   public class AccessControlRepository:Repository<IEntity,string>,IAccessControlRepository
    {
        private string serviceName = "AL.asmx/";
        public AccessControlRepository()
        {
            base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;
           
            ChildGetLookup.Add(typeof(SecurityGroup), "getSG");
            ChildGetLookup.Add(typeof(ModuleClass), "getCL");
            ChildGetAllLookup.Add(typeof(SecurityGroup), "qrySG");
            ChildAddOrUpdateLookup.Add(typeof(SecurityGroup), "setSG");
           
            ChildDeleteLookup.Add(typeof(SecurityGroup), "delSG");


            ChildGetAllLookup.Add(typeof(ModuleClass), "qryCL");
            ChildGetAllLookup.Add(typeof(UC), "qryUC");
            ChildGetAllLookup.Add(typeof(ClassProperty), "qryCP");
            ChildAddOrUpdateLookup.Add(typeof(ClassProperty), "setCP");
            ChildGetAllLookup.Add(typeof(SecurityGroupUser), "qryUS");
            ChildAddOrUpdateLookup.Add(typeof(SecurityGroupUser), "setUS");
            ChildAddOrUpdateLookup.Add(typeof(ModuleClass), "setCL");
            ChildDeleteLookup.Add(typeof(SecurityGroupUser), "delUS");

        }
    }
}
