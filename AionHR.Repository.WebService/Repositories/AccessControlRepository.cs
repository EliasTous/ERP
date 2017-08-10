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
            ChildGetLookup.Add(typeof(ModuleClass), "getUC");
            ChildGetAllLookup.Add(typeof(SecurityGroup), "qrySG");
            ChildGetAllLookup.Add(typeof(DataAccessItemView), "qryDA");
            ChildAddOrUpdateLookup.Add(typeof(SecurityGroup), "setSG");
            ChildAddOrUpdateLookup.Add(typeof(DataAccessItemView), "setDA");


            ChildDeleteLookup.Add(typeof(SecurityGroup), "delSG");
            ChildDeleteLookup.Add(typeof(DataAccessItemView), "delDA");


            ChildGetAllLookup.Add(typeof(ModuleClass), "qryCL");
            ChildGetAllLookup.Add(typeof(UC), "qryUP");
            ChildGetAllLookup.Add(typeof(ClassProperty), "qryCP");
            ChildAddOrUpdateLookup.Add(typeof(ClassProperty), "setCP");
            ChildGetAllLookup.Add(typeof(SecurityGroupUser), "qryUS");
            ChildAddOrUpdateLookup.Add(typeof(SecurityGroupUser), "setUS");
            ChildAddOrUpdateLookup.Add(typeof(ModuleClass), "setCL");
            ChildAddOrUpdateLookup.Add(typeof(ModuleClass[]), "arrCL");
            ChildDeleteLookup.Add(typeof(SecurityGroupUser), "delUS");

        }
    }
}
