using System;
using AionHR.Model.Employees.Profile;
using AionHR.Model.System;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.Reports;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AionHR.Model.Payroll;
using System.Globalization;

namespace AionHR.Web.UI.Forms.Reports.Controls
{
    public partial class PayIdFilter : System.Web.UI.UserControl,IComboControl
    {
        IPayrollService payrollService = ServiceLocator.Current.GetInstance<IPayrollService>();
        ISystemService systemService = ServiceLocator.Current.GetInstance<ISystemService>();

        public void Select(object id)
        {
            payId.Select(id);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)

                FillPayId();

        }

        private void FillPayId()
        {
            try
            {
                PayrollListRequest req = new PayrollListRequest();
                req.Year = "0";
                req.PeriodType = "5";
                req.Status = "0";
                req.Size = "30";
                req.StartAt = "0";
                req.Filter = "";

                ListResponse<GenerationHeader> resp = payrollService.ChildGetAll<GenerationHeader>(req);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }
                
                string dateFormat = systemService.SessionHelper.GetDateformat();
                if (systemService.SessionHelper.CheckIfArabicSession())
                    resp.Items.ForEach(x => x.payRefWithDateRange = x.payRef + " ( " + x.startDate.ToString("dd/MM/yyyy", new CultureInfo("ar-AE")) + " - " + x.endDate.ToString("dd/MM/yyyy", new CultureInfo("ar-AE")) + " )");
                else
                    resp.Items.ForEach(x => x.payRefWithDateRange = x.payRef + " ( " + x.startDate.ToString("dd/MM/yyyy", new CultureInfo("en")) + " - " + x.endDate.ToString("dd/MM/yyyy", new CultureInfo("en")) + " )");
                payIdStore.DataSource = resp.Items;
                payIdStore.DataBind();
            }
            catch (Exception exp)
            {
                Common.errorMessage(new ListResponse<Currency>());
            }
        }
    }
}