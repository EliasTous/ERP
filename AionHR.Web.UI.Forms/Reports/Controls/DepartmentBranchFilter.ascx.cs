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
    public partial class DepartmentBranchFilter : System.Web.UI.UserControl
    {
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                FillJobInfo();

        }
        private void FillJobInfo()
        {
            FillDepartment();

            FillBranch();


        }

        public JobInfoParameterSet GetJobInfo()
        {
            JobInfoParameterSet p = new JobInfoParameterSet();
            if (!string.IsNullOrEmpty(branchId.Text) && branchId.Value.ToString() != "0")
            {
                p.BranchId = Convert.ToInt32(branchId.Value);



            }
            else
            {
                p.BranchId = 0;

            }

            if (!string.IsNullOrEmpty(departmentId.Text) && departmentId.Value.ToString() != "0")
            {
                p.DepartmentId = Convert.ToInt32(departmentId.Value);


            }
            else
            {
                p.DepartmentId = 0;

            }


            return p;
        }
        private void FillDepartment()
        {
            ListRequest departmentsRequest = new ListRequest();
            ListResponse<Department> resp = _companyStructureService.ChildGetAll<Department>(departmentsRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            departmentStore.DataSource = resp.Items;
            departmentStore.DataBind();
        }
        private void FillBranch()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Branch> resp = _companyStructureService.ChildGetAll<Branch>(branchesRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            branchStore.DataSource = resp.Items;
            branchStore.DataBind();

        }


    }
}