using Model.System;
using Services.Interfaces;
using Services.Messaging;
using Services.Messaging.Reports;
using Services.Messaging.System;
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
            req.Size = "1000";
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