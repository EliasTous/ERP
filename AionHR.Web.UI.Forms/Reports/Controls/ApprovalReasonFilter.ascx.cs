using AionHR.Model.Company.Structure;
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
   
    public partial class ApprovalReasonFilter : System.Web.UI.UserControl
    {
        public string FieldLabel { get; set; }
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                 FillArStore();
            if (!string.IsNullOrEmpty(FieldLabel))
                arId.FieldLabel = FieldLabel;
        }
        protected void Page_Init(object sender, EventArgs e)
        {
           
        }
        private void FillArStore()
        {
            try
            {
                ListRequest request = new ListRequest();
                ListResponse<ApprovalReason> resp = _companyStructureService.ChildGetAll<ApprovalReason>(request);
                if (!resp.Success)

                    Common.errorMessage(resp);
                arStore.DataSource = resp.Items;
                arStore.DataBind();
            }catch(Exception exp)
            {
                Common.errorMessage(new ListResponse <ApprovalReason>());
            }
        }


        public string getApprovalReason()
        {


            if (!string.IsNullOrEmpty(arId.Text) && arId.Value.ToString() != "0")
            {
                return arId.Value.ToString();



            }
            else
            {
                return "0";

            }

        }

        public void setApprovalReason(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                arId.SetValue(id);
                arId.Select(id);
              
            }

        }
    }
}