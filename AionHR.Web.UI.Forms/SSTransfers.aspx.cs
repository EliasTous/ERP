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
using Services.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Web.UI.Forms.Utilities;
using Model.Company.News;
using Services.Messaging;
using Model.Company.Structure;
using Model.System;
using Model.Employees.Profile;
using Services.Messaging.System;
using Model.SelfService;
using Services.Messaging.SelfService;
using Model;

namespace Web.UI.Forms
{
    public partial class SSTransfers : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ISelfServiceService _selfServiceService = ServiceLocator.Current.GetInstance<ISelfServiceService>();

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

                dateCol.Format = _systemService.SessionHelper.GetDateformat();
                
                documentsTransfersStore.Reload();
                statusFilter.Select("1");
                //try
                //{
                //    AccessControlApplier.ApplyAccessControlOnPage(typeof(LetterSelfservice), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
                //}
                //catch (AccessDeniedException exp)
                //{
                //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                //    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                //    Viewport1.Hidden = true;
                //    return;
                //}


            }

        }



        /// <summary>
        /// the detailed tabs for the edit form. I put two tabs by default so hide unecessary or add addional
        /// </summary>
     



    


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



        protected void PoPuPDT(object sender, DirectEventArgs e)
        {
            string type = e.ExtraParams["type"];
            string seqNo = e.ExtraParams["seqNo"];
            string doId = e.ExtraParams["doId"];
            string employeeName = e.ExtraParams["employeeName"];
            string departmentName = e.ExtraParams["departmentName"];
            string notes = e.ExtraParams["notes"];
            string date = DateTime.Parse(e.ExtraParams["date"]).ToString(_systemService.SessionHelper.GetDateformat()); ;


            switch (type)
            {


                case "imgEdit":
                    this.seqNo.Text = seqNo;
                    this.doId.Text = doId;
                    this.employeeName.Text = employeeName;
                    this.departmentName.Text = departmentName;
                    this.notes.Text = notes;
                    this.date.Text = date;
                    DocumentTransferWindow.Show();
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
        
       
   

        protected void TransfersStore_RefreshData(object sender, StoreReadDataEventArgs e)
        {
            string filter = string.Empty;

            int totalCount = 1;


            _systemService.SessionHelper.GetEmployeeId();
            //Fetching the corresponding list

            //in this test will take a list of News
            
            SelfServiceTransfersListRequest req = new SelfServiceTransfersListRequest();
            if(!string.IsNullOrEmpty(statusFilter.Text))
            {
                req.Status = statusFilter.Value.ToString();


            }
            else
            {
                req.Status = "1";

            }
            req.StartAt = "0";
            req.Size = "100";
            
            
            req.EmployeeId = _systemService.SessionHelper.GetEmployeeId(); ;
            ListResponse<AdminDocTransfer> transfers = _selfServiceService.ChildGetAll<AdminDocTransfer>(req);
            if (!transfers.Success)
            {
                X.Msg.Alert(Resources.Common.Error, transfers.Summary).Show();
                return;
            }
            this.documentsTransfersStore.DataSource = transfers.Items;
            e.Total = transfers.count;

            this.documentsTransfersStore.DataBind();

        }




        protected void saveDocumentTransfer(object sender, DirectEventArgs e)
        {
            
            string seqNo = e.ExtraParams["seqNo"];
            string doId = e.ExtraParams["doId"];
            string status = e.ExtraParams["status"];

            AdminDocTransfer t = new AdminDocTransfer();
            t.seqNo = seqNo;
            t.doId = Convert.ToInt32(doId);
            t.apStatus = status.Trim('\"');
            t.employeeId = _systemService.SessionHelper.GetEmployeeId();
            t.departmentId = "0";
           
            PostRequest<AdminDocTransfer> req = new PostRequest<AdminDocTransfer>();
            req.entity = t;
            PostResponse < AdminDocTransfer > resp = _selfServiceService.ChildAddOrUpdate<AdminDocTransfer>(req);
            if(!resp.Success)

            {
                //Show an error saving...
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                Common.errorMessage(resp);
                return;
            }
            DocumentTransferWindow.Close();
            documentsTransfersStore.Reload();


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

      
      
    
    }
}