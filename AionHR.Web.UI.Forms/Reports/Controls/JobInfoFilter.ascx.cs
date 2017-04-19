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

namespace AionHR.Web.UI.Forms.Reports
{
    public partial class JobInfoFilter : System.Web.UI.UserControl
    {
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            FillJobInfo();
           
        }
        private void FillJobInfo()
        {
            FillDepartment();
            FillPosition();
            FillBranch();
            FillDivision();

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
            if (!string.IsNullOrEmpty(positionId.Text) && positionId.Value.ToString() != "0")
            {
                p.PositionId = Convert.ToInt32(positionId.Value);


            }
            else
            {
                p.PositionId = 0;

            }
            if (!string.IsNullOrEmpty(divisionId.Text) && divisionId.Value.ToString() != "0")
            {
                p.DivisionId = Convert.ToInt32(divisionId.Value);


            }
            else
            {
                p.DivisionId = 0;

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

        private void FillDivision()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Division> resp = _companyStructureService.ChildGetAll<Division>(branchesRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            divisionStore.DataSource = resp.Items;
            divisionStore.DataBind();
        }
        private void FillPosition()
        {
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Model.Company.Structure.Position> resp = _companyStructureService.ChildGetAll<Model.Company.Structure.Position>(branchesRequest);
            if (!resp.Success)
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            positionStore.DataSource = resp.Items;
            positionStore.DataBind();
        }
    }
}