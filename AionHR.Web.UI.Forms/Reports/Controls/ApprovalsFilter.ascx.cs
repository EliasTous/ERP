using Model.Company.Structure;
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
   
    public partial class ApprovalsFilter : System.Web.UI.UserControl
    {
        public string FieldLabel { get; set; }
        public string LabelWidth { get; set; }
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            int width = 140;
            if (!IsPostBack)
                FillApprovalsStore();
            if (!string.IsNullOrEmpty(FieldLabel))
                apId.FieldLabel = FieldLabel;
            if (!string.IsNullOrEmpty(LabelWidth))
            {
               
                if (Int32.TryParse(LabelWidth, out width))
                {
                    apId.LabelWidth = width;
                }
                
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
           
        }
        private void FillApprovalsStore()
        {
            try
            {
                ListRequest request = new ListRequest();

                request.Filter = "";
                ListResponse<Approval> resp = _companyStructureService.ChildGetAll<Approval>(request);
                if (!resp.Success)

                    Common.errorMessage(resp);
                apStore.DataSource = resp.Items;
                apStore.DataBind();

            }catch(Exception exp)
            {
                Common.errorMessage(new ListResponse <Approval>());
            }
        }


        public string getApproval()
        {


            if (!string.IsNullOrEmpty(apId.Text) && apId.Value.ToString() != "0")
            {
                return apId.Value.ToString();



            }
            else
            {
                return "0";

            }

        }

        public void setApproval(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                apId.SetValue(id);
                apId.Select(id);
              
            }

        }
    }
}