﻿using Model.Company.Structure;
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
            } catch (Exception exp)
            {
                Common.errorMessage(new ListResponse<ApprovalReason>());
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

            arId.SetValue(id);
            arId.Select(id);



        }
        public void changeReadOnlyStatus (bool status)
        {

            arId.ReadOnly = status;



        }
    }
}