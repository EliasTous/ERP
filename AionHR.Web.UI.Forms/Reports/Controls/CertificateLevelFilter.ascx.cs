using AionHR.Model.Attendance;
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
    public partial class CertificateLevelFilter : System.Web.UI.UserControl, IComboControl
    {
        IEmployeeService employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)

                FillCL();

        }
        protected void Page_Init(object sender, EventArgs e)
        {

        }
        private void FillCL()
        {
            try
            {
                ListRequest request = new ListRequest();
                ListResponse<CertificateLevel> resp = employeeService.ChildGetAll<CertificateLevel>(request);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }
                clStore.DataSource = resp.Items;
                clStore.DataBind();
            }
            catch (Exception exp)
            {
                Common.errorMessage(new ListResponse<Currency>());
            }
        }


        public string getCL()
        {


            if (!string.IsNullOrEmpty(clId.Text) && clId.Value.ToString() != "0")
            {
                return clId.Value.ToString();



            }
            else
            {
                return "0";

            }

        }

        public void setCL(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                clId.SetValue(id);
                clId.Select(id);

            }

        }

        public string GetValue()
        {
            return "scheduleId=" + getCL();
        }

        public void Select(object id)
        {
            clId.Select(id);
        }

        public void SetLabel(string newLabel)
        {
            clId.FieldLabel = newLabel;
        }

        public ComboBox GetComboBox()
        {
            return clId;
        }
    }
}