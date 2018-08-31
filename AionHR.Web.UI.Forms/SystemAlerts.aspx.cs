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

namespace AionHR.Web.UI.Forms
{
    public partial class SystemAlerts : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        protected override void InitializeCulture()
        {

            bool rtl = true;
            if (!_systemService.SessionHelper.CheckIfArabicSession())
            {
                rtl = false;
                base.InitializeCulture();
                LocalisationManager.Instance.SetEnglishLocalisation();
            }

            if (rtl)
            {
                base.InitializeCulture();
                LocalisationManager.Instance.SetArabicLocalisation();
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!X.IsAjaxRequest && !IsPostBack)
            {

                SetExtLanguage();
                HideShowButtons();
                if (_systemService.SessionHelper.CheckIfIsAdmin())
                    return;
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(SystemAlert), null, GridPanel1, null, null);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
                }
                var properties = AccessControlApplier.GetPropertiesLevels(typeof(SystemAlert));
                foreach (var item in properties)
                {
                    var result = GridPanel1.ColumnModel.Columns.Where(x => x.DataIndex == item.index).ToList();
                    if (result.Count > 0)
                    {
                        switch(item.accessLevel)
                        {
                            case 0:
                                result[0].Renderer = new Renderer("return '***';");
                                result[0].Disabled = true;
                                if (result[0] is WidgetColumn)
                                {
                                    ((result[0] as WidgetColumn).Widget[0] as Field).InputType = InputType.Password;
                                    (result[0] as WidgetColumn).Widget[0].Disabled = true;
                                }
                                break;
                            case 1:
                                result[0].Disabled = true;
                                if (result[0] is WidgetColumn)
                                {

                                    (result[0] as WidgetColumn).Widget[0].Disabled = true;
                                }
                                break;
                            default: break;

                        }
                        
                           

                    }


                }
            }

        }



        /// <summary>
        /// the detailed tabs for the edit form. I put two tabs by default so hide unecessary or add addional
        /// </summary>
        private void HideShowTabs()
        {
            //this.OtherInfoTab.Visible = false;
        }



        private void HideShowButtons()
        {

        }


        /// <summary>
        /// hiding uncessary column in the grid. 
        /// </summary>



        private void SetExtLanguage()
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            if (rtl)
            {
                this.ResourceManager1.RTL = true;
                this.Viewport1.RTL = true;

            }
        }









        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            string s = File.ReadAllText(MapPath("~/Utilities/alerts.txt"));
            List<SystemAlert> preDefined = JsonConvert.DeserializeObject<List<SystemAlert>>(s);

            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<SystemAlert> stored = _systemService.ChildGetAll<SystemAlert>(request);
            if (!stored.Success)
            {
                X.Msg.Alert(Resources.Common.Error, stored.Summary).Show();
                return;
            }
            List<SystemAlert> union = new List<SystemAlert>();

            foreach (var item in preDefined)
            {
                if (stored.Items.Where(f => f.alertId == item.alertId).Count() > 0)
                {

                    SystemAlert added = stored.Items.Where(f => f.alertId == item.alertId).First();
                    added.predefined = true;
                    union.Add(added);
                    item.predefined = true;
                }
                else
                    union.Add(item);
            }
            this.Store1.DataSource = union;
            e.Total = union.Count;

            this.Store1.DataBind();
        }





        [DirectMethod]
        public string CheckSession()
        {
            if (!_systemService.SessionHelper.CheckUserLoggedIn())
            {
                return "0";
            }
            else return "1";
        }

        protected void SaveAlerts(object sender, DirectEventArgs e)
        {
            string values = e.ExtraParams["values"];

            List<SystemAlert> alerts = JsonConvert.DeserializeObject<List<SystemAlert>>(values);

            PostRequest<SystemAlert[]> req = new PostRequest<SystemAlert[]>();
            req.entity = alerts.ToArray();

            PostResponse<SystemAlert[]> resp = _systemService.ChildAddOrUpdate<SystemAlert[]>(req);

            if (!resp.Success)

            {
                X.Msg.Alert(Resources.Common.Error, GetGlobalResourceObject("Errors", resp.ErrorCode) != null ? GetGlobalResourceObject("Errors", resp.ErrorCode).ToString() + "<br>Technical Error: " + resp.ErrorCode + "<br> Summary: " + resp.LogId : resp.Summary).Show();
                return;
            }
            Notification.Show(new NotificationConfig
            {
                Title = Resources.Common.Notification,
                Icon = Icon.Information,
                Html = Resources.Common.RecordUpdatedSucc
            });
            Store1.Reload();
        }
    }
}