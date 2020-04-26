using Model.Payroll;
using Model.System;
using Services.Interfaces;
using Services.Messaging;
using Services.Messaging.Reports;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.UI.Forms.Reports
{
    public partial class FiscalYearFilter : System.Web.UI.UserControl
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IPayrollService _payrollService = ServiceLocator.Current.GetInstance<IPayrollService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)

                FillYearStore();

        }
        protected void Page_Init(object sender, EventArgs e)
        {
           
        }
        private void FillYearStore()
        {
            ListRequest l = new ListRequest();
            ListResponse<FiscalYear> resp = _payrollService.ChildGetAll<FiscalYear>(l);
            if (!resp.Success)
            {
                
                Common.errorMessage(resp);
                return;
            }
           
        }


        public string getCurrency()
        {


            if (!string.IsNullOrEmpty(year.Text) && year.Value.ToString() != "0")
            {
                return year.Value.ToString();



            }
            else
            {
                return "0";

            }

        }

        public void setCurrency(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                year.SetValue(id);
                year.Select(id);
              
            }

        }
    }
}