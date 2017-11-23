using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Xsl;
using Ext.Net;
using Newtonsoft.Json;
using AionHR.Services.Interfaces;
using Microsoft.Practices.ServiceLocation;
using AionHR.Web.UI.Forms.Utilities;
using AionHR.Model.Company.News;
using AionHR.Services.Messaging;
using AionHR.Model.Company.Structure;
using AionHR.Model.System;
using AionHR.Model.Employees.Profile;

namespace AionHR.Web.UI.Forms
{
    public partial class LeaveRequestsSelfService : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            RecordRequest r = new RecordRequest();
            r.RecordID = _systemService.SessionHelper.GetCurrentUserId();
            RecordResponse<UserInfo> response = _systemService.ChildGetRecord<UserInfo>(r);
            CurrentEmployee.Text = response.result.employeeId;
            leaveRequest1.Update(CurrentEmployee.Text);

        }
        protected void Page_Init(object sender, EventArgs e)
        {

            leaveRequest1.Store1 = null;
            leaveRequest1.GrigPanel1 = null;

        }
    }
}