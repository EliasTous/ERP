using AionHR.Model.Company.Structure;
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
    public partial class DepartmentFilter : System.Web.UI.UserControl
    {
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                FillDepartments();
        }
        private void FillDepartments()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Branch> resp = _companyStructureService.ChildGetAll<Branch>(branchesRequest);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() : resp.Summary).Show();

            }
            departmentStore.DataSource = resp.Items;
            departmentStore.DataBind();

        }
        public JobInfoParameterSet GetJobInfo()
        {
            JobInfoParameterSet p = new JobInfoParameterSet();
            if (!string.IsNullOrEmpty(departmentId.Text) && departmentId.Value.ToString() != "0")
            {
                p.DepartmentId = Convert.ToInt32(departmentId.Value);



            }
            else
            {
                p.DepartmentId = 0;

            }

            p.BranchId = 0;


            p.PositionId = 0;


            p.DivisionId = 0;



            return p;
        }
    }
}