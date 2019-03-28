using AionHR.Infrastructure.Configuration;
using AionHR.Infrastructure.Domain;
using AionHR.Model.Attendance;
using AionHR.Model.Dashboard;
using AionHR.Model.Employees.Leaves;
using AionHR.Model.Employees.Profile;
using AionHR.Model.LeaveManagement;
using AionHR.Model.LoadTracking;
using AionHR.Model.Payroll;

using AionHR.Model.TimeAttendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionHR.Model.AssetManagementRepository;
using AionHR.Model.AssetManagement;

namespace AionHR.Repository.WebService.Repositories
{
  public  class AssetManagementRepository : Repository<IEntity, string>, IAssetManagementRepository
    {
       
            private string serviceName = "AM.asmx/";

            public AssetManagementRepository()
            {
                base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;

            ChildGetAllLookup.Add(typeof(AssetManagementSupplier), "qrySU");
            ChildGetAllLookup.Add(typeof(AssetManagementCategory), "qryCA");
            ChildGetAllLookup.Add(typeof(AssetManagementAsset), "qryAS");
            ChildGetAllLookup.Add(typeof(AssetManagementPurchaseOrder), "qryPO");
            ChildGetAllLookup.Add(typeof(AssetManagementOnBoarding), "qryOB");
            ChildGetAllLookup.Add(typeof(AssetManagementLoan), "qryLO");
            ChildGetAllLookup.Add(typeof(AssetManagementPurchaseOrderApproval), "qryPA");
            ChildGetAllLookup.Add(typeof(AssetManagementCategoryProperty), "qryCP");
            ChildGetAllLookup.Add(typeof(AssetPropertyValue), "qryAP");




            ChildGetLookup.Add(typeof(AssetManagementSupplier), "getSU");
            ChildGetLookup.Add(typeof(AssetManagementCategory), "getCA");
            ChildGetLookup.Add(typeof(AssetManagementAsset), "getAS");
            ChildGetLookup.Add(typeof(AssetManagementPurchaseOrder), "getPO");
            ChildGetLookup.Add(typeof(AssetManagementOnBoarding), "getOB");
            ChildGetLookup.Add(typeof(AssetManagementLoan), "getLO");
            ChildGetLookup.Add(typeof(AssetManagementCategoryProperty), "getCP");



            ChildAddOrUpdateLookup.Add(typeof(AssetManagementSupplier), "setSU");
            ChildAddOrUpdateLookup.Add(typeof(AssetManagementCategory), "setCA");
            ChildAddOrUpdateLookup.Add(typeof(AssetManagementAsset), "setAS");
            ChildAddOrUpdateLookup.Add(typeof(AssetManagementPurchaseOrder), "setPO");
            ChildAddOrUpdateLookup.Add(typeof(AssetManagementOnBoarding), "setOB");
            ChildAddOrUpdateLookup.Add(typeof(AssetManagementLoan), "setLO");
            ChildAddOrUpdateLookup.Add(typeof(AssetManagementCategoryProperty), "setCP");
            ChildAddOrUpdateLookup.Add(typeof(AssetManagementPurchaseOrderApproval), "setPA");
            ChildAddOrUpdateLookup.Add(typeof(AssetPropertyValue), "setAP");

            ChildAddOrUpdateLookup.Add(typeof(AssetPOReception), "genAS");

            ChildDeleteLookup.Add(typeof(AssetManagementSupplier), "delSU");
            ChildDeleteLookup.Add(typeof(AssetManagementCategory), "delCA");
            ChildDeleteLookup.Add(typeof(AssetManagementAsset), "delAS");
            ChildDeleteLookup.Add(typeof(AssetManagementPurchaseOrder), "delPO");
            ChildDeleteLookup.Add(typeof(AssetManagementOnBoarding), "delOB");
            ChildDeleteLookup.Add(typeof(AssetManagementLoan), "delLO");
            ChildDeleteLookup.Add(typeof(AssetManagementCategoryProperty), "delCP");

        }
        }
    }

