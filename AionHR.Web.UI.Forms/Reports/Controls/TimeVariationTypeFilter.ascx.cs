
using AionHR.Model.Payroll;
using AionHR.Model.System;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.Reports;
using AionHR.Services.Messaging.System;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AionHR.Web.UI.Forms.Reports.Controls
{
    public partial class TimeVariationTypeFilter : System.Web.UI.UserControl
    {
        IPayrollService _payrollService = ServiceLocator.Current.GetInstance<IPayrollService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FilltimeCodeStore();
               
            }
           
              
        }

        public string GetTimeCode()
        {
            if (string.IsNullOrEmpty(timeVariationType.Value.ToString()))
                return "0";
            return timeVariationType.Value.ToString();
        }
        public string GetTimeCodeString()

        {
            if (string.IsNullOrEmpty(timeVariationType.Value.ToString()))
                return " ";
            return timeVariationType.SelectedItem.Text;
        }
        private void FilltimeCodeStore()
        {
            //XMLDictionaryListRequest request = new XMLDictionaryListRequest();

            //request.database = "3";
            //ListResponse<XMLDictionary> resp = _systemService.ChildGetAll<XMLDictionary>(request);
            //if (!resp.Success)
            //{
            //    Common.errorMessage(resp);
            //    return;
            //}


            timeVariationStore.DataSource = Common.XMLDictionaryList(_systemService, "3");
            timeVariationStore.DataBind();

        }
    }
}