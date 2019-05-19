using AionHR.Model.System;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.Reports;
using AionHR.Services.Messaging.System;
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
    public partial class UserComboFilter : System.Web.UI.UserControl, IComboControl
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                FillUsers();
        }
        private void FillUsers()
        {
            UsersListRequest req = new UsersListRequest();
            req.Size = "100";
            req.StartAt = "0";
            


            req.DepartmentId = "0";
            req.PositionId = "0";
            req.BranchId = "0";
            req.SortBy = "fullName";
            ListResponse<UserInfo> users = _systemService.ChildGetAll<UserInfo>(req);
            if(!users.Success)
            {
                Common.errorMessage(users);
                return;
            }
            userStore.DataSource = users.Items;
            userStore.DataBind();
        }
     

        public void Select(object id)
        {
            userId.Select(id);
        }

        public void SetLabel(string newLabel)
        {
            userId.FieldLabel = newLabel;
        }

        public ComboBox GetComboBox()
        {
            return userId;
        }
    }
}