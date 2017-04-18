using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Resources;
using AionHR.Web.UI.Forms.Reports.App_LocalResources;

namespace AionHR.Web.UI.Forms.Reports
{
    public partial class SalaryHistory : DevExpress.XtraReports.UI.XtraReport
    {
        public SalaryHistory()
        {
            InitializeComponent();
            
           
            for (int i = 0; i < xrTable2.Rows[0].Cells.Count; i++)
            {
                xrTable2.Rows[0].Cells[i].Text = RT202_aspx.FieldDepartment;
            }
        }

        private void SalaryHistory_DesignerLoaded(object sender, DevExpress.XtraReports.UserDesigner.DesignerLoadedEventArgs e)
        {
          for(int i=0;i< xrTable2.Rows[0].Cells.Count;i++)
            {
                xrTable2.Rows[0].Cells[i].Text = "fff";
            }
        }
    }
}
