﻿using AionHR.Model.Employees.Profile;
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
    public partial class SalaryChangeReasonFilter : System.Web.UI.UserControl
    {
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                FillSCR();
        }

        private void FillSCR()
        {
            ListRequest req = new ListRequest();
            ListResponse<SalaryChangeReason> resp = _employeeService.ChildGetAll<SalaryChangeReason>(req);
            if(!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return;
            }

            scrStore.DataSource = resp.Items;
            scrStore.DataBind();
        }

        public SalaryChangeReasonParameterSet GetSCR()
        {
            SalaryChangeReasonParameterSet p = new SalaryChangeReasonParameterSet();

            if (!string.IsNullOrEmpty(scrId.Text) && scrId.Value.ToString() != "0")
            {
                p.ChangeReasonId = Convert.ToInt32(scrId.Value);



            }
            else
            {
                p.ChangeReasonId = 0;

            }
            return p;
        }
    }
}