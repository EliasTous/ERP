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
    public partial class NationalityFilter : System.Web.UI.UserControl,IComboControl
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)

                FillNationality();

        }
        protected void Page_Init(object sender, EventArgs e)
        {

        }
        private void FillNationality()
        {
            try
            {
                ListRequest request = new ListRequest();
                ListResponse<Nationality> resp = _systemService.ChildGetAll<Nationality>(request);
                if (!resp.Success)

                    Common.errorMessage(resp);
                nationalityStore.DataSource = resp.Items;
                nationalityStore.DataBind();
            }
            catch (Exception exp)
            {
                Common.errorMessage(new ListResponse<Currency>());
            }
        }


        public string getNationality()
        {


            if (!string.IsNullOrEmpty(nationalityId.Text) && nationalityId.Value.ToString() != "0")
            {
                return nationalityId.Value.ToString();



            }
            else
            {
                return "0";

            }

        }

        public void setNationality(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                nationalityId.SetValue(id);
                nationalityId.Select(id);

            }

        }

        public void Select(object id)
        {
            nationalityId.Select(id);
        }

        public void SetLabel(string newLabel)
        {
            nationalityId.FieldLabel = newLabel;

        }

        public ComboBox GetComboBox()
        {
            return nationalityId;
        }
    }
}