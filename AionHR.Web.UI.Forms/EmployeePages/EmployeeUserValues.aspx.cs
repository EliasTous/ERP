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
using System.Net;
using AionHR.Infrastructure.JSON;
using AionHR.Model.Attributes;
using AionHR.Model.Access_Control;
using AionHR.Web.UI.Forms.ConstClasses;
using AionHR.Model.Employees;
using AionHR.Services.Messaging.Employees;

namespace AionHR.Web.UI.Forms.EmployeePages
{
    public partial class EmployeeUserValues : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService=ServiceLocator.Current.GetInstance<IEmployeeService>();
       
        IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();
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

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!X.IsAjaxRequest && !IsPostBack)
            {

                SetExtLanguage();
                HideShowButtons();
               
                if (string.IsNullOrEmpty(Request.QueryString["employeeId"]))
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorOperation).Show();
                CurrentEmployee.Text = Request.QueryString["employeeId"];
               
                
                EmployeeTerminated.Text = Request.QueryString["terminated"];

                bool disabled = EmployeeTerminated.Text == "1";
                Button2.Disabled  = disabled;
             
                if ((bool)_systemService.SessionHelper.Get("IsAdmin"))
                    return;
                try
                {
                  //  AccessControlApplier.ApplyAccessControlOnPage(typeof(Dependant), form, UserValueGrid, Button2, Button7);
                  
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport11.Hidden = true;
                    return;
                }
                //ApplyAccessControlOnAddress();
               
            }
           

        }
        //private void ApplyAccessControlOnAddress()
        //{
           
        //    var properties = AccessControlApplier.GetPropertiesLevels(typeof(Dependant));
        //    var att = properties.Where(x =>

        //        x.propertyId == "3115010"
        //   );
        //    int level = 0;
        //    if (att.Count() == 0 || att.ToList()[0].accessLevel == 2)
        //        return;
        //    level = att.ToList()[0].accessLevel;
        //    switch (level)
        //    {
        //        case 0:
        //            addressForm.Items.ForEach(x =>
        //            {
        //                (x as Field).InputType = InputType.Password;
        //                (x as Field).ReadOnly = true;
        //            });
        //            break;
        //        case 1:
        //            addressForm.Items.ForEach(x =>
        //            {

        //                (x as Field).ReadOnly = true;
        //            }); break;
        //    }

        //}
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
                this.Viewport11.RTL = true;

            }
        }

        //private Dependant GetDEById(string id)
        //{
        //    GetDependantRequest r = new GetDependantRequest();
        //    r.SeqNo = id.ToString();
        //    r.EmployeeId = CurrentEmployee.Text;
        //    RecordResponse<Dependant> response = _employeeService.ChildGetRecord<Dependant>(r);
        //    if (!response.Success)
        //    {
        //        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
        //         Common.errorMessage(response);
        //        return null;
        //    }
        //    return response.result;
        //}


       private void FillPropertyStore()
        {
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<UserProperty> resp = _employeeService.ChildGetAll<UserProperty>(request);

            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            propertyStore.DataSource = resp.Items;
            propertyStore.DataBind(); 
        }

        protected void PoPuP(object sender, DirectEventArgs e)
        {


            string propertyIdParamter = e.ExtraParams["propertyId"];
            string type = e.ExtraParams["type"];

            switch (type)
            {
                case "imgEdit":
                    EmployeeUserValueRecordRequest req = new EmployeeUserValueRecordRequest();
                    req.employeeId = CurrentEmployee.Text;
                    req.propertyId = propertyIdParamter;
                    RecordResponse<EmployeeUserValue> resp = _employeeService.ChildGetRecord<EmployeeUserValue>(req); 
                    if (!resp.Success)
                    {
                        Common.errorMessage(resp);
                        return; 
                    }

                    FillPropertyStore();
                    if (resp.result == null)
                    {
                        propertyId.Select(propertyIdParamter);
                    }
                    else
                    {
                        EmployeeValueForm.SetValues(resp.result);
                    }
                    this.EditWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteRecord({0})", propertyIdParamter),
                            Text = Resources.Common.Yes
                        },
                        No = new MessageBoxButtonConfig
                        {
                            Text = Resources.Common.No
                        }

                    }).Show();
                    break;

                default:
                    break;
            }


        }

        

        /// <summary>
        /// Deleting all selected record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        /// <summary>
        /// Direct method for removing multiple records
        /// </summary>


        /// <summary>
        /// Adding new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ADDNew(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
         
            this.EditWindow.Title = Resources.Common.AddNewRecord;
            EmployeeValueForm.Reset(); 



            this.EditWindow.Show();
        }

      

        protected void UserValueStore_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News

            EmployeeContactsListRequest request = new EmployeeContactsListRequest();
            request.EmployeeId = Convert.ToInt32(CurrentEmployee.Text);
            ListResponse<EmployeeUserValue> resp = _employeeService.ChildGetAll<EmployeeUserValue>(request);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            List<XMLDictionary> maskList = Common.XMLDictionaryList(_systemService, "35");

            resp.Items.ForEach(x => x.maskString = maskList.Where(y => y.key == Convert.ToInt32(x.mask)).First().value);
            this.UserValueStore.DataSource = resp.Items;
            e.Total = resp.count;

            this.UserValueStore.DataBind();

        }
      

        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
           

            string obj = e.ExtraParams["values"];
            EmployeeUserValue b = JsonConvert.DeserializeObject<EmployeeUserValue>(obj);
            b.employeeId = CurrentEmployee.Text;



           

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

                        this.EditWindow.Close();
                        //RowSelectionModel sm = this.dependandtsGrid.GetSelectionModel() as RowSelectionModel;
                        //sm.DeselectAll();
                        //sm.Select(b.seqNo.ToString());
                        UserValueStore.Reload();



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


      

  
        


        //private void FillState()
        //{
        //    ListRequest documentTypes = new ListRequest();
        //    ListResponse<State> resp = _systemService.ChildGetAll<State>(documentTypes);
        //    if (!resp.Success)
        //    {
        //       Common.errorMessage(resp);
        //        return;
        //    }
        //    stStore.DataSource = resp.Items;
        //    stStore.DataBind();

        //}

      


       
      

       

        [DirectMethod]
        public void DeleteRecord(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                EmployeeUserValue n = new EmployeeUserValue();
                n.propertyId = index; 
                n.employeeId= Request.QueryString["employeeId"];

                PostRequest<EmployeeUserValue> req = new PostRequest<EmployeeUserValue>();
                req.entity = n;
                PostResponse<EmployeeUserValue> res = _employeeService.ChildDelete<EmployeeUserValue>(req);
                if (!res.Success)
                {
                    //Show an error saving...
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(res);
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    UserValueStore.Reload();

                    //Step 3 : Showing a notification for the user 
                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordDeletedSucc
                    });
                }

            }
            catch (Exception ex)
            {
                //In case of error, showing a message box to the user
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorDeletingRecord).Show();

            }

        }

    }
}