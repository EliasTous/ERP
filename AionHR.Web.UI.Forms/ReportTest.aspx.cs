using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.Reports;
using AionHR.Web.UI.Forms.Reports;
using Microsoft.Practices.ServiceLocation;
using Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AionHR.Web.UI.Forms
{
    public partial class ReportTest : System.Web.UI.Page
    {
        IReportsService _reportsService = ServiceLocator.Current.GetInstance<IReportsService>();
        private JobInfoParameterSet GetJobInfo()
        {
            JobInfoParameterSet p = new JobInfoParameterSet();
           
           
                p.BranchId = 0;

            

           
                p.DepartmentId = 0;

            
            
                p.PositionId = 0;

            
           
                p.DivisionId = 0;

            

            return p;
        }
        private ReportCompositeRequest GetRequest()
        {
            ReportCompositeRequest req = new ReportCompositeRequest();

            req.Size = "1000";
            req.StartAt = "0";
            req.SortBy = "firstName";
            JobInfoParameterSet p = GetJobInfo();

            req.Add(p);

            ActiveStatusParameterSet s = new ActiveStatusParameterSet();

            s.active = DropDownList1.SelectedIndex;
            req.Add(s);
            return req;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ReportCompositeRequest req =  GetRequest();
            
            ListResponse<AionHR.Model.Reports.RT201> resp = _reportsService.ChildGetAll<AionHR.Model.Reports.RT201>(req);

           
            SalaryHistory h = new SalaryHistory(new Dictionary<string, string> ());
            h.DataSource = resp.Items;
           
            ASPxWebDocumentViewer1.OpenReport(h);
        }
    }
}