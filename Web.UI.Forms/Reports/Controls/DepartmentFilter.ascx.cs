﻿using Model.Company.Structure;
using Services.Interfaces;
using Services.Messaging;
using Services.Messaging.Reports;
using Ext.Net;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace Web.UI.Forms.Reports.Controls
{
    public partial class DepartmentFilter : System.Web.UI.UserControl,IComboControl
    {
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                FillDepartments();
        }
        private void FillDepartments()
        {
            DepartmentListRequest branchesRequest = new DepartmentListRequest();
            ListResponse<Department> resp = _companyStructureService.ChildGetAll<Department>(branchesRequest);
            if (!resp.Success)
            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.Summary : resp.Summary).Show();

            }
            if (resp.Items != null)
            {
                departmentStore.DataSource = resp.Items;
                departmentStore.DataBind();
            }
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

        public void Select(object id)
        {
            departmentId.Select(id);
        }

        public void SetLabel(string newLabel)
        {
            departmentId.FieldLabel = newLabel;
        }

        public ComboBox GetComboBox()
        {
            return departmentId;
        }
    }
}