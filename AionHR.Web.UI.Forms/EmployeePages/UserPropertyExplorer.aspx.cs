using AionHR.Model.AssetManagement;
using AionHR.Model.Employees;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.Asset_Management;
using AionHR.Services.Messaging.Employees;
using AionHR.Web.UI.Forms.Utilities;
using Ext.Net;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AionHR.Web.UI.Forms
{
    public partial class UserPropertyExplorer : System.Web.UI.Page
    {
        protected override void InitializeCulture()
        {

            switch (_systemService.SessionHelper.getLangauge())
            {
                case "ar":
                    {
                        base.InitializeCulture();
                        LocalisationManager.Instance.SetArabicLocalisation();
                    }
                    break;
                case "en":
                    {
                        base.InitializeCulture();
                        LocalisationManager.Instance.SetEnglishLocalisation();
                    }
                    break;

                case "fr":
                    {
                        base.InitializeCulture();
                        LocalisationManager.Instance.SetFrenchLocalisation();
                    }
                    break;
                case "de":
                    {
                        base.InitializeCulture();
                        LocalisationManager.Instance.SetGermanyLocalisation();
                    }
                    break;
                default:
                    {


                        base.InitializeCulture();
                        LocalisationManager.Instance.SetEnglishLocalisation();
                    }
                    break;
            }
        }
        IAssetManagementService _assetManagementService = ServiceLocator.Current.GetInstance<IAssetManagementService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        private void SetExtLanguage()
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            if (rtl)
            {
                this.ResourceManager1.RTL = true;
                this.Viewport1.RTL = true;

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest && !IsPostBack)
            {
                SetExtLanguage();


                EmployeeContactsListRequest EVreq = new EmployeeContactsListRequest();
                EVreq.EmployeeId = Convert.ToInt32(Request.QueryString["employeeId"]);
                ListResponse<EmployeeUserValue> EVresp = _employeeService.ChildGetAll<EmployeeUserValue>(EVreq);
                if (!EVresp.Success)
                {
                    Common.errorMessage(EVresp);
                    return;
                }

                ListRequest UPReq = new ListRequest();

                UPReq.Filter = "";
                ListResponse<UserProperty> UPResp = _employeeService.ChildGetAll<UserProperty>(UPReq);
                if (!UPResp.Success)
                {
                    Common.errorMessage(UPResp);
                    return;
                }

                //UserPropertyRecordRequest UPReq = new UserPropertyRecordRequest();
                //UPReq.propertyId = propertyIdParamter;

                //RecordResponse<UserProperty> UPResp = _employeeService.ChildGetRecord<UserProperty>(UPReq);
                //if (!UPResp.Success)
                //{
                //    Common.errorMessage(UPResp);
                //    return;
                //}
                try
                {
                    //EmployeeUserValue EV;
                    //if (EVresp.result != null)
                    //    EV = EVresp.result;
                    //else
                    //    EV = new EmployeeUserValue();


                    EVresp.Items.ForEach(x =>
                    {
                        UserProperty corrVal = UPResp.Items.Where(y => y.recordId == x.propertyId).Count() != 0 ? UPResp.Items.Where(y => y.recordId == x.propertyId).First() : new UserProperty(); 
                            //AssetPropertyValue corrVal = valResp.Items.Where(d => d.propertyId == x.propertyId).ToList().Count > 0 ? valResp.Items.Where(d => d.propertyId == x.propertyId).ToList()[0] : null;
                            switch (x.mask)
                            {
                                case 1: propertiesForm.Items.Add(new TextField() { Text = x.value != null && !string.IsNullOrEmpty(x.value) ? x.value : "", FieldLabel = x.propertyName, Name = x.propertyId }); break; //text
                                case 2: propertiesForm.Items.Add(new NumberField() { Value = x.value != null && !string.IsNullOrEmpty(x.value) ? x.GetValueDouble() : 0, FieldLabel = x.propertyName, Name = x.propertyId }); break; //number
                                case 3: propertiesForm.Items.Add(new DateField() { Value = x.value, Name = x.propertyId, FieldLabel = x.propertyName, Format = _systemService.SessionHelper.GetDateformat() }); break;//datetime
                            case 4: propertiesForm.Items.Add(new DateField() { Value = x.value!=null?x.value.Replace('.',':'):"" , Name = x.propertyId, FieldLabel = x.propertyName ,Format=_systemService.SessionHelper.GetDateformat()+ " HH.mm",SubmitFormat = "m/d/Y H.i" }); break;
                            case 5: propertiesForm.Items.Add(new Checkbox() { Checked = x.value != null && !string.IsNullOrEmpty(x.value) ? x.GetValueBool() : false, FieldLabel = x.propertyName, Name = x.propertyId, InputValue = "true" }); break;//checkbox
                            }
                        
                    });
                }
                catch (Exception exp)
                {
                    X.Msg.Alert("Error", exp.Message).Show();
                }



            }
        }


        protected void SaveProperties(object sender, DirectEventArgs e)
        {
          //  string AssetId = currentAsset.Text;
         //   string catId = currentCat.Text;
            string[] values = e.ExtraParams["values"].Split(',');
            List<string> sentIds = new List<string>();
            for (int i = 0; i < values.Length; i++)
            {

                sentIds.Add(values[i].Split(':')[0].Replace("\"", "").Replace("{", ""));
                PostRequest<EmployeeUserValue> req = new PostRequest<EmployeeUserValue>();
                req.entity = new EmployeeUserValue() {employeeId= Request.QueryString["employeeId"],  propertyId = values[i].Split(':')[0].Replace("\"", "").Replace("{", ""), value = values[i].Split(':')[1].Replace("\"", "").Replace("}", "") };
                PostResponse<EmployeeUserValue> resp = _employeeService.ChildAddOrUpdate<EmployeeUserValue>(req);
                if (!resp.Success)
                    Common.errorMessage(resp);
            }
            //AssetManagementCategoryPropertyListRequest propReq = new AssetManagementCategoryPropertyListRequest();
            ////propReq.categoryId = catId;
            ////propReq.categoryId = catId;
            //ListResponse<AssetManagementCategoryProperty> propResp = _assetManagementService.ChildGetAll<AssetManagementCategoryProperty>(propReq);
            //if (!propResp.Success)
            //    Common.errorMessage(propResp);
            //propResp.Items.ForEach(x =>
            //{
            //    if (x.mask == 5 && !sentIds.Contains(x.propertyId))
            //    {
            //        PostRequest<AssetPropertyValue> req = new PostRequest<AssetPropertyValue>();
            //        PostResponse<AssetPropertyValue> resp = _assetManagementService.ChildAddOrUpdate<AssetPropertyValue>(req);
            //        if (!resp.Success)
            //            Common.errorMessage(resp);
            //    }

            //});

            Notification.Show(new NotificationConfig
            {
                Title = Resources.Common.Notification,
                Icon = Icon.Information,
                Html = Resources.Common.RecordSavingSucc
            });
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
        protected void CancelClicked(object sender, DirectEventArgs e)
        {
            X.Call("parent.hideWindow");
        }
    }
}