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
    public partial class UserComboFilter : System.Web.UI.UserControl
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        [DirectMethod]
        public object FillUsers(string action, Dictionary<string, object> extraParams)
        {
            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            List<UserInfo> data = GetUsersFiltered(prms.Query);

            //  return new
            // {
            return data;
        }

        private List<UserInfo> GetUsersFiltered(string query)
        {
            UsersListRequest req = new UsersListRequest();
            req.Size = "100";
            req.StartAt = "0";
            req.Filter = query;

            
            req.DepartmentId =  "0";
            req.PositionId = "0";
            req.BranchId =  "0";
            req.SortBy = "fullName";
            ListResponse<UserInfo> users = _systemService.ChildGetAll<UserInfo>(req);
            return users.Items;
        }

        public UserParameterSet GetUser()
        {
            UserParameterSet set = new UserParameterSet();
            if (!string.IsNullOrEmpty(userId.Text) && userId.Value.ToString() != "0")
            {
                set.UserId = Convert.ToInt32(userId.Value);



            }
            else
            {
                set.UserId = 0;

            }
            return set;
        }
    }
}