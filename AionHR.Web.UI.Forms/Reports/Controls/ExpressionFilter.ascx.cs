using Model.Company.Structure;
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
   
    public partial class ExpressionFilter : System.Web.UI.UserControl
    {
        public string FieldLabel { get; set; }
        public string LabelWidth { get; set; }
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IMathematicalService _mathematicalService = ServiceLocator.Current.GetInstance<IMathematicalService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            int width = 140;
            if (!IsPostBack)
                FillExpressionStore();
            if (!string.IsNullOrEmpty(FieldLabel))
                expressionId.FieldLabel = FieldLabel;
            if (!string.IsNullOrEmpty(LabelWidth))
            {
               
                if (Int32.TryParse(LabelWidth, out width))
                {
                    expressionId.LabelWidth = width;
                }
                
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
           
        }
        private void FillExpressionStore()
        {
            try
            {
                ListRequest request = new ListRequest();

                request.Filter = "";
                ListResponse<PayrollExpression> resp = _mathematicalService.ChildGetAll<PayrollExpression>(request);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }
                if (!resp.Success)

                    Common.errorMessage(resp);
                expressionStore.DataSource = resp.Items;
                expressionStore.DataBind();

            }catch(Exception exp)
            {
                Common.errorMessage(new ListResponse <Approval>());
            }
        }


        public string getExpression()
        {


            if (!string.IsNullOrEmpty(expressionId.Text) && expressionId.Value.ToString() != "0")
            {
                return expressionId.Value.ToString();



            }
            else
            {
                return "";

            }

        }

        public void setExpression(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                expressionId.SetValue(id);
                expressionId.Select(id);
              
            }

        }
    }
}