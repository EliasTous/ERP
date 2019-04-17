using AionHR.Model.Employees.Profile;
using AionHR.Model.System;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.Reports;
using Ext.Net;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace AionHR.Web.UI.Forms.Reports.Controls
{
    public partial class SponsorFilter : System.Web.UI.UserControl,IComboControl
    {
        IEmployeeService employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();

        public ComboBox GetComboBox()
        {
            return sponsorId;
        }

        public void Select(object id)
        {
            sponsorId.Select(id);
        }

        public void SetLabel(string newLabel)
        {
            sponsorId.FieldLabel = newLabel;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)

                FillSponsors();

        }
       
        private void FillSponsors()
        {
            try
            {
                ListRequest request = new ListRequest();
                ListResponse<Sponsor> resp = employeeService.ChildGetAll<Sponsor>(request);
                if (!resp.Success)

                    Common.errorMessage(resp);
                sponsorsStore.DataSource = resp.Items;
                sponsorsStore.DataBind();
            }
            catch (Exception exp)
            {
                Common.errorMessage(new ListResponse<Currency>());
            }
        }


       
    }
}