﻿using Model.Access_Control;
using Model.Attributes;
using Model.Company.Structure;
using Model.Employees.Profile;
using Services.Interfaces;
using Services.Messaging;
using Services.Messaging.CompanyStructure;
using Services.Messaging.Reports;
using Ext.Net;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.UI.Forms.Reports
{
    public partial class JobInfoFilter : System.Web.UI.UserControl
    {
        string defaultDepartmentId, defaultBranchId, defaultDivisiontId;
        ICompanyStructureService _companyStructureService = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                X.Call("setStatus", EnableDepartment, EnableBranch, EnablePosition, EnableDivision);
                X.Call("setWidth", FieldWidth);
                FillJobInfo();

            }

        }
        private void FillJobInfo()
        {
            try
            {
                if (_systemService.SessionHelper.GetEmployeeId() != null)
                {
                    RecordRequest req = new RecordRequest();
                    req.RecordID = _systemService.SessionHelper.GetEmployeeId();
                    RecordResponse<Employee> response = _employeeService.Get<Employee>(req);
                    if (!response.Success)
                        Common.errorMessage(response);

                    if (response.result != null)
                    {
                        defaultDepartmentId = response.result.departmentId.ToString();
                        defaultBranchId = response.result.branchId.ToString();
                        defaultDivisiontId = response.result.divisionId.ToString();
                    }

                }
                FillDepartment();
                FillPosition();
                FillBranch();
                FillDivision();
            }
            catch (Exception exp)
            {
                X.MessageBox.Alert(GetGlobalResourceObject("Common", "Error").ToString(), exp.Message);
            }

        }

        public JobInfoParameterSet GetJobInfo()
        {
            JobInfoParameterSet p = new JobInfoParameterSet();
            if (!EnableBranch)
                p.BranchId = null;
            else
            {
                if (!string.IsNullOrEmpty(branchId.Text) && branchId.Value.ToString() != "0")
                {
                    p.BranchId = Convert.ToInt32(branchId.Value);



                }
                else
                {
                    p.BranchId = 0;

                }
            }
            if (!EnableDepartment)
                p.DepartmentId = null;
            else
            {
                if (!string.IsNullOrEmpty(departmentId.Text) && departmentId.Value.ToString() != "0")
                {
                    p.DepartmentId = Convert.ToInt32(departmentId.Value);


                }
                else
                {
                    p.DepartmentId = 0;

                }
            }
            if (!EnablePosition)
            {
                p.PositionId = null;
            }
            else
            {
                if (!string.IsNullOrEmpty(positionId.Text) && positionId.Value.ToString() != "0")
                {
                    p.PositionId = Convert.ToInt32(positionId.Value);


                }
                else
                {
                    p.PositionId = 0;

                }
            }
            if (!EnableDivision)
            {
                p.DivisionId = null;
            }
            else
            {
                if (!string.IsNullOrEmpty(divisionId.Text) && divisionId.Value.ToString() != "0")
                {
                    p.DivisionId = Convert.ToInt32(divisionId.Value);


                }
                else
                {
                    p.DivisionId = 0;

                }
            }
            return p;
        }
        public string GetJobInfoString()
        {
            string p = "";
            if (EnableBranch)

            {
                if (!string.IsNullOrEmpty(branchId.Text) && branchId.Value.ToString() != "0")
                {
                    p += "branchId=" + branchId.Value.ToString() + "&";



                }
                else
                {
                    p += "branchId=0&";

                }
            }
            if (EnableDepartment)

            {
                if (!string.IsNullOrEmpty(departmentId.Text) && departmentId.Value.ToString() != "0")
                {
                    p += "departmentId" + departmentId.Value.ToString() + "&";


                }
                else
                {
                    p += "departmentId=0&";

                }
            }
            if (EnablePosition)
            {
                if (!string.IsNullOrEmpty(positionId.Text) && positionId.Value.ToString() != "0")
                {
                    p += "positionId" + positionId.Value.ToString() + "&";


                }
                else
                {
                    p += "positionId=0&";

                }
            }
            if (EnableDivision)
            
            {
                if (!string.IsNullOrEmpty(divisionId.Text) && divisionId.Value.ToString() != "0")
                {
                    p += "divisionId" + divisionId.Value.ToString() ;


                }
                else
                {
                    p += "divisionId=0";

                }
            }
            return p.TrimEnd('&');
        }
        private void FillDepartment()
        {
            if (!EnableDepartment)
                return;

            DepartmentListRequest departmentsRequest = new DepartmentListRequest();

            departmentsRequest.type = 0;
            ListResponse<Department> resp = _companyStructureService.ChildGetAll<Department>(departmentsRequest);
            if (!resp.Success)
                Common.errorMessage(resp);
            departmentStore.DataSource = resp.Items;
            departmentStore.DataBind();
            if (_systemService.SessionHelper.CheckIfIsAdmin())
                return;
            UserDataRecordRequest ud = new UserDataRecordRequest();
            ud.UserId = _systemService.SessionHelper.GetCurrentUserId();
            ud.RecordID = "0";
            ud.classId = ((ClassIdentifier)typeof(Department).GetCustomAttributes(true).Where(t => t is ClassIdentifier).ToList()[0]).ClassID;
            RecordResponse<UserDataAccess> udR = _accessControlService.ChildGetRecord<UserDataAccess>(ud);

            if (udR.result == null || !udR.result.hasAccess)
            {
                if (defaultDepartmentId == null)
                {
                    departmentId.Select(0);

                    departmentId.SetValue(resp.Items.Count != 0 ? resp.Items[0].recordId : null);
                }
                else
                {
                    departmentId.Select(defaultDepartmentId);

                    departmentId.SetValue(defaultDepartmentId);
                }
                X.Call("setDepartmentAllowBlank", true);
            }
        }
        private void FillBranch()
        {
            if (!EnableBranch)
                return;
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Branch> resp = _companyStructureService.ChildGetAll<Branch>(branchesRequest);
            if (!resp.Success)
                Common.errorMessage(resp);
            branchStore.DataSource = resp.Items;
            branchStore.DataBind();
            if (_systemService.SessionHelper.CheckIfIsAdmin())
                return;
            UserDataRecordRequest ud = new UserDataRecordRequest();
            ud.UserId = _systemService.SessionHelper.GetCurrentUserId();
            ud.RecordID = "0";
            ud.classId = ((ClassIdentifier)typeof(Branch).GetCustomAttributes(true).Where(t => t is ClassIdentifier).ToList()[0]).ClassID;
            RecordResponse<UserDataAccess> udR = _accessControlService.ChildGetRecord<UserDataAccess>(ud);
            if (udR.result == null || !udR.result.hasAccess)
            {
                if (defaultBranchId == null)
                {
                    branchId.Select(0);

                    branchId.SetValue(resp.Items.Count != 0 ? resp.Items[0].recordId : null);
                }
                else
                {
                    branchId.Select(defaultBranchId);

                    branchId.SetValue(defaultBranchId);
                }
                X.Call("setBranchAllowBlank", true);
            }
        }

        private void FillDivision()
        {
            if (!EnableDivision)
                return;
            ListRequest branchesRequest = new ListRequest();
            ListResponse<Division> resp = _companyStructureService.ChildGetAll<Division>(branchesRequest);
            if (!resp.Success)
                Common.errorMessage(resp);
            divisionStore.DataSource = resp.Items;
            divisionStore.DataBind();
            if (_systemService.SessionHelper.CheckIfIsAdmin())
                return;
            UserDataRecordRequest ud = new UserDataRecordRequest();
            ud.UserId = _systemService.SessionHelper.GetCurrentUserId();
            ud.RecordID = "0";
            ud.classId = ((ClassIdentifier)typeof(Division).GetCustomAttributes(true).Where(t => t is ClassIdentifier).ToList()[0]).ClassID;
            RecordResponse<UserDataAccess> udR = _accessControlService.ChildGetRecord<UserDataAccess>(ud);
            if (udR.result == null || !udR.result.hasAccess)
            {
                if (defaultDivisiontId == null)
                {
                    divisionId.Select(0);

                    divisionId.SetValue(resp.Items.Count != 0 ? resp.Items[0].recordId : null);
                }
                else
                {
                    divisionId.Select(defaultDivisiontId);

                    divisionId.SetValue(defaultDivisiontId);
                }
                X.Call("setDivisionAllowBlank", true);
            }
        }
        private void FillPosition()
        {
            if (!EnablePosition)
                return;
            PositionListRequest branchesRequest = new PositionListRequest();
            branchesRequest.StartAt = "0";
            branchesRequest.Size = "1000";
            branchesRequest.SortBy = "recordId";

            ListResponse<Model.Company.Structure.Position> resp = _companyStructureService.ChildGetAll<Model.Company.Structure.Position>(branchesRequest);
            if (!resp.Success)
                Common.errorMessage(resp);
            positionStore.DataSource = resp.Items;
            positionStore.DataBind();
        }

        private bool enableBranch = true;
        private bool enableDepartment = true;
        private bool enablePosition = true;
        private bool enableDivision = true;

        private int fieldWidth = 120;
        public bool EnableBranch
        {
            get
            {
                return enableBranch;
            }

            set
            {
                enableBranch = value;
            }
        }

        public bool EnableDepartment
        {
            get
            {
                return enableDepartment;
            }

            set
            {
                enableDepartment = value;
            }
        }

        public bool EnablePosition
        {
            get
            {
                return enablePosition;
            }

            set
            {
                enablePosition = value;
            }
        }

        public bool EnableDivision
        {
            get
            {
                return enableDivision;
            }

            set
            {
                enableDivision = value;
            }
        }

        public int FieldWidth
        {
            get
            {
                return fieldWidth;
            }
            set
            {
                fieldWidth = value;
            }
        }

        public string GetDepartment()
        {
            if (departmentId.SelectedItem != null)
                return departmentId.SelectedItem.Text;
            else
                return "";
        }
        public string GetBranch()
        {
            if (branchId.SelectedItem != null)
                return branchId.SelectedItem.Text;
            else
                return "";
        }
        public string GetDivision()
        {
            if (divisionId.SelectedItem != null)
                return divisionId.SelectedItem.Text;
            else
                return "";
        }
        public string GetPosition()
        {
            if (positionId.SelectedItem != null)
                return positionId.SelectedItem.Text;
            else
                return "";
        }

        public string GetValue()
        {
            return GetJobInfoString();
        }


        public void setBranch(string id)
        {
            branchId.SetValue(id);
            branchId.Select(id);
        }
        public void setDepartment(string id)
        {
            departmentId.SetValue(id);
            departmentId.Select(id);
        }
    }
}