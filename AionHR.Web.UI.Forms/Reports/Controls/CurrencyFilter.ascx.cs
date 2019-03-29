using AionHR.Model.System;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.Reports;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AionHR.Web.UI.Forms.Reports
{
    public partial class CurrencyFilter : System.Web.UI.UserControl
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)

                FillCurrency();

        }
        protected void Page_Init(object sender, EventArgs e)
        {
           
        }
        private void FillCurrency()
        {
            try
            {
                ListRequest request = new ListRequest();
                ListResponse<Currency> resp = _systemService.ChildGetAll<Currency>(request);
                if (!resp.Success)

                    Common.errorMessage(resp);
                currencyStore.DataSource = resp.Items;
                currencyStore.DataBind();
            }catch(Exception exp)
            {
                Common.errorMessage(new ListResponse < Currency >());
            }
        }


        public string getCurrency()
        {


            if (!string.IsNullOrEmpty(currencyId.Text) && currencyId.Value.ToString() != "0")
            {
                return currencyId.Value.ToString();



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
                currencyId.SetValue(id);
                currencyId.Select(id);
              
            }

        }
    }
}