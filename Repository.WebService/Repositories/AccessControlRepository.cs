using Infrastructure.Configuration;
using Infrastructure.Domain;
using Model.Access_Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.WebService.Repositories
{
   public class AccessControlRepository:Repository<IEntity,string>,IAccessControlRepository
    {
        private string serviceName = "AL.asmx/";
        public AccessControlRepository()
        {
            base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;
           
            ChildGetLookup.Add(typeof(SecurityGroup), "getSG");
            ChildGetLookup.Add(typeof(DataAccessItemView), "getDA");
            ChildGetLookup.Add(typeof(UserDataAccess), "getUD");
            ChildGetLookup.Add(typeof(ModuleClass), "getUC");



            ChildGetAllLookup.Add(typeof(SecurityGroup), "qrySG");
            ChildGetAllLookup.Add(typeof(DataAccessItemView), "qryDA");
            ChildGetAllLookup.Add(typeof(ModuleClass), "qryCL");
            ChildGetAllLookup.Add(typeof(UC), "qryUP");
            ChildGetAllLookup.Add(typeof(ClassProperty), "qryCP");
            ChildGetAllLookup.Add(typeof(SecurityGroupUser), "qryUS");

            ChildAddOrUpdateLookup.Add(typeof(SecurityGroup), "setSG");
            ChildAddOrUpdateLookup.Add(typeof(DataAccessItemView), "setDA");
            ChildAddOrUpdateLookup.Add(typeof(ClassProperty), "setCP");
            ChildAddOrUpdateLookup.Add(typeof(SecurityGroupUser), "setUS");
            ChildAddOrUpdateLookup.Add(typeof(ModuleClass), "setCL");
            ChildAddOrUpdateLookup.Add(typeof(ModuleClass[]), "arrCL");


            ChildDeleteLookup.Add(typeof(SecurityGroup), "delSG");
            ChildDeleteLookup.Add(typeof(DataAccessItemView), "delDA");
            ChildDeleteLookup.Add(typeof(SecurityGroupUser), "delUS");







        }
    }
}
