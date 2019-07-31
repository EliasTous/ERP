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
                if (string.IsNullOrEmpty(Request.QueryString["_propertyId"]) || string.IsNullOrEmpty(Request.QueryString["_employeeId"]))
                {
                    X.Msg.Alert("Error", "Error").Show();
                    return;
                }
                currentPropertyId.Text = Request.QueryString["_propertyId"];
                currentEmployeeId.Text = Request.QueryString["_employeeId"];


                EmployeeUserValueRecordRequest EVReq = new EmployeeUserValueRecordRequest();
                EVReq.employeeId = currentEmployeeId.Text;
                EVReq.propertyId = currentPropertyId.Text;
                RecordResponse<EmployeeUserValue> EVresp = _employeeService.ChildGetRecord<EmployeeUserValue>(EVReq);
                if (!EVresp.Success)
                {
                    Common.errorMessage(EVresp);
                    return;
                }

                UserPropertyRecordRequest UPReq = new UserPropertyRecordRequest();
                UPReq.propertyId = currentPropertyId.Text;

                RecordResponse<UserProperty> UPResp = _employeeService.ChildGetRecord<UserProperty>(UPReq);
                if (!UPResp.Success)
                {
                    Common.errorMessage(UPResp);
                    return;
                }
                try
                {
                    EmployeeUserValue EV;
                    if (EVresp.result != null)
                        EV = EVresp.result;
                    else
                        EV = new EmployeeUserValue();


                            switch (UPResp.result.mask)
                            {
                               
                            case 2: propertiesForm.Items.Add(new NumberField() { Value= !string.IsNullOrEmpty(EV.value) ? EV.GetValueDouble() : 0, FieldLabel = "value", Name = "value" }); break; //number
                            case 3: propertiesForm.Items.Add(new DateField() { SelectedDate =  !string.IsNullOrEmpty(EV.value) ? EV.GetValueDateTime() : DateTime.Now, Name = "value", FieldLabel = "value" }); break;
                            case 4: propertiesForm.Items.Add(new DateField() { SelectedDate = !string.IsNullOrEmpty(EV.value) ? EV.GetValueDateTime() : DateTime.Now, Name = "value", FieldLabel = "value" ,Format=_systemService.SessionHelper.GetDateformat()+" HH:mm" }); break;//datetime

                             case 5: propertiesForm.Items.Add(new Checkbox() { Checked =  !string.IsNullOrEmpty(EV.value) ? EV.GetValueBool() : false, FieldLabel = "value" , Name = "value", InputValue = "true" }); break;//checkbox
                            default: propertiesForm.Items.Add(new TextField() { Text = !string.IsNullOrEmpty(EV.value) ? EV.value : "", FieldLabel = "value", Name = "value" }); break;
                    }
                        
                   
                }
                catch(Exception exp)
                {
                    X.Msg.Alert("Error", exp.Message);
                }



            }
        }


        protected void SaveProperties(object sender, DirectEventArgs e)
        {
            string obj = e.ExtraParams["values"];
            EmployeeUserValue b = JsonConvert.DeserializeObject<EmployeeUserValue>(obj);
            b.employeeId = currentEmployeeId.Text;
            b.propertyId = currentPropertyId.Text; 




            try
            {
                //New Mode
                PostRequest<EmployeeUserValue> request = new PostRequest<EmployeeUserValue>();
                request.entity = b;

                PostResponse<EmployeeUserValue> r = _employeeService.ChildAddOrUpdate<EmployeeUserValue>(request);


                //check if the insert failed
                if (!r.Success)//it maybe be another condition
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(r);
                    return;
                }
                else
                {

                    //Add this record to the store 
                    //this.dependandtsStore.Insert(0, b);

                    //Display successful notification
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordSavingSucc,
                        HideDelay = 250
                    });


                    X.Call("parent.hideWindow");
                    X.Call("parent.refreshStore");

                    


                }
            }
            catch (Exception ex)
            {
                //Error exception displaying a messsage box
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorSavingRecord).Show();
            }



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