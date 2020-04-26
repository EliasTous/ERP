using Model.Company.Structure;
using Model.Employees.Profile;
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
   
    public partial class EntitlementDeductionFilter : System.Web.UI.UserControl
    {
        public string FieldLabel { get; set; }
        public string LabelWidth { get; set; }
        public int Filter { get; set; }
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            int width = 140;
            if (!IsPostBack)
                FillEntitlementDeductionStore();
            if (!string.IsNullOrEmpty(FieldLabel))
                edId.FieldLabel = FieldLabel;
            if (!string.IsNullOrEmpty(LabelWidth))
            {
               
                if (Int32.TryParse(LabelWidth, out width))
                {
                    edId.LabelWidth = width;
                }
                
            }
            if (Filter !=0 )
                FillEntitlementDeductionStore(Filter);
            
        }
        protected void Page_Init(object sender, EventArgs e)
        {
           
        }
        private void FillEntitlementDeductionStore(int filter =0)
        {
            try
            {
                ListRequest request = new ListRequest();

                request.Filter = "";
                ListResponse<EntitlementDeduction> resp = _employeeService.ChildGetAll<EntitlementDeduction>(request);
                if (!resp.Success)
                {
                    Common.errorMessage(resp);
                    return;
                }
                switch(filter)
                {
                 case  0: edStore.DataSource = resp.Items;
                        break;
                    case 1:
                        edStore.DataSource = resp.Items.Where(x=>x.type==1).ToList();
                        break;
                    case 2:
                        edStore.DataSource = resp.Items.Where(x => x.type == 2).ToList();
                        break;
                    default: edStore.DataSource = resp.Items;
                        break;


                }
               
                edStore.DataBind();

            }catch(Exception exp)
            {
                Common.errorMessage(new ListResponse <Approval>());
            }
        }


        public string getEntitlementDeduction()
        {


            if (!string.IsNullOrEmpty(edId.Text) && edId.Value.ToString() != "0")
            {
                return edId.Value.ToString();



            }
            else
            {
                return "0";

            }

        }

        public void setEntitlementDeduction(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                edId.SetValue(id);
                edId.Select(id);
              
            }

        }
    }
}