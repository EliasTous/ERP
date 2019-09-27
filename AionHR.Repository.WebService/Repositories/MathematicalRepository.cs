using AionHR.Infrastructure.Configuration;
using AionHR.Infrastructure.Domain;
using AionHR.Model;
using AionHR.Model.Attendance;
using AionHR.Model.Dashboard;
using AionHR.Model.Employees.Leaves;
using AionHR.Model.Employees.Profile;
using AionHR.Model.LeaveManagement;
using AionHR.Model.LoadTracking;
using AionHR.Model.Payroll;
using AionHR.Model.SelfService;
using AionHR.Model.TimeAttendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Repository.WebService.Repositories
{
   
        public class MathematicalRepository : Repository<IEntity, string>, IMathematicalRepository
        {
            private string serviceName = "ME.asmx/";

            public MathematicalRepository()
            {
                base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;

            ChildGetAllLookup.Add(typeof(PayrollExpression), "qryEX");
            ChildGetAllLookup.Add(typeof(PayrollFunction), "qryFU");
            ChildGetAllLookup.Add(typeof(PayrollExpFunc), "qryEF");
            ChildGetAllLookup.Add(typeof(PayrollFunConst), "qryFC");
            ChildGetAllLookup.Add(typeof(PayrollConstant), "qryCO");
           

            ChildAddOrUpdateLookup.Add(typeof(PayrollExpression), "setEX");
            ChildAddOrUpdateLookup.Add(typeof(CheckExpression), "checkEX");
            ChildAddOrUpdateLookup.Add(typeof(PayrollConstant), "setCO");
            ChildAddOrUpdateLookup.Add(typeof(PayrollFunction), "setFU");
            ChildAddOrUpdateLookup.Add(typeof(MyInfo), "setEM");
            ChildAddOrUpdateLookup.Add(typeof(PayrollExpFunc), "setEF");
            ChildAddOrUpdateLookup.Add(typeof(PayrollFunConst), "setFC");


            ChildDeleteLookup.Add(typeof(MyInfo), "delEM");
            ChildDeleteLookup.Add(typeof(PayrollConstant), "delCO");
            ChildDeleteLookup.Add(typeof(PayrollExpression), "delEX");
            ChildDeleteLookup.Add(typeof(PayrollFunction), "delFU");
            ChildDeleteLookup.Add(typeof(PayrollExpFunc), "delEF");
            ChildDeleteLookup.Add(typeof(PayrollFunConst), "delFC");



            ChildGetLookup.Add(typeof(PayrollConstant), "getCO");
            ChildGetLookup.Add(typeof(PayrollExpression), "getEX");
            ChildGetLookup.Add(typeof(PayrollFunction), "getFU");
            ChildGetLookup.Add(typeof(PayrollExpFunc), "getEF");
            ChildGetLookup.Add(typeof(PayrollFunConst), "getFC");


        }
        }
    
}
