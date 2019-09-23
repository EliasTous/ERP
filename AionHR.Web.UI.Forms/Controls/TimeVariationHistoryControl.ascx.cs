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


using Microsoft.Practices.ServiceLocation;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging.System;
using AionHR.Model.System;
using AionHR.Services.Messaging;

namespace AionHR.Web.UI.Forms
{

    [DirectMethodProxyID(IDMode = DirectMethodProxyIDMode.Alias, Alias = "TLUC")]
    public partial class TimeVariationHistoryControl : System.Web.UI.UserControl
    {


        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
     
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!X.IsAjaxRequest && !IsPostBack)
            {


                HideShowButtons();

                //////FillDepartment();
                //////FillDivision();
                //////FillBranch();

                

            }

        }

        

        
        protected void PoPuP(object sender, DirectEventArgs e)
        {
          
            string dataParam = e.ExtraParams["data"];
            string type = e.ExtraParams["type"];
           

         


            switch (type)
            {
                case "imgEdit":


                    data.Text = dataParam;
                    this.EditRecordWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditRecordWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.UC.DeleteRecord({0},{1})", string.Empty, string.Empty),
                            Text = Resources.Common.Yes,
                            
                            
                            
                        },
                        No = new MessageBoxButtonConfig
                        {
                            Text = Resources.Common.No
                        }

                    }).Show();
                    break;

                case "imgAttach":

                    //Here will show up a winow relatice to attachement depending on the case we are working on
                    break;
                default:
                    break;
            }
        }



    [DirectMethod]
        public void DeleteRecord(string index, string plid)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                //Price n = new Price();
                //n.itemId = index;
                //n.plId = plid;
                //n.currencyId = currid;
                //n.priceType = ptid;
                //PostRequest<Price> req = new PostRequest<Price>();
                //req.entity = n;
                //PostResponse<Price> res = _saleService.ChildDelete<Price>(req);
                //if (!res.Success)
                //{
                //    //Show an error saving...
                //    Common.errorMessage(res);
                //    return;
                //}
                //else
                //{
                //    //Step 2 :  remove the object from the store
                //    FillItemPriceStore();

                //    //Step 3 : Showing a notification for the user 
                //    Notification.Show(new NotificationConfig
                //    {
                //        Title = Resources.Common.Notification,
                //        Icon = Icon.Information,
                //        Html = Resources.Common.RecordDeletedSucc
                //    });
                }



            
            catch (Exception ex)
            {
                //In case of error, showing a message box to the user
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorDeletingRecord).Show();

            }

        }


        
        protected void ADDNewRecord(object sender, DirectEventArgs e)
        {
           
        }
        protected void TimeVariationHistory_RefreshData(object sender, StoreReadDataEventArgs e)
        {
            TimeVariationHistoryListRequest req = new TimeVariationHistoryListRequest();
            req.classId = currentClassId.Text;
            req.masterRef = currentMasterRef.Text;
            req.userId = "0";
            req.type = "0";

            ListResponse<TimeVariationHistory> resp = _systemService.ChildGetAll<TimeVariationHistory>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            resp.Items.ForEach(x => x.data = x.data.Replace("{", string.Empty).Replace("\",",string.Empty).Replace(",", Environment.NewLine));
            TimeVariationHistoryStore.DataSource = resp.Items;
            TimeVariationHistoryStore.DataBind();

            

        }
        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    PriceGridPanel = new GridPanel();

        //}





        #region public interface



        public void Update(string id)
        {
        
            try {
               

             
                panelRecordDetails.ActiveIndex = 0;
                //TextField1.Text = "No";
                    //  ItemPriceStore.Reload();


                



                        //Step 1 : get the object from the Web Service 
                      

                        this.EditRecordWindow.Show();
                       
                            }
            catch (Exception exp)
            {
                X.Msg.Alert(Resources.Common.Error, exp.Message).Show();
            }
        }

        public void Add()
        {

          

        }

        public void Show(string classId ,string masterRef)
        {
            currentClassId.Text = classId;
            currentMasterRef.Text = masterRef;
            TimeVariationHistoryStore.Reload();
            TimeVariationHistoryWindow.Show();

        }




        public RefreshParent RefreshLeaveCalendarCallBack { get; set; }

        public delegate void RefreshParent();


        #endregion



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



        /// <summary>
        /// This direct method will be called after confirming the delete
        /// </summary>
        /// <param name="index">the ID of the object to delete</param>



        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.
            string id = e.ExtraParams["id"];
            string obj = e.ExtraParams["values"];
            string PPTabParamter = e.ExtraParams["PPTab"];
            //Item b = JsonConvert.DeserializeObject<Item>(obj);

            //ItemPhysProp c = JsonConvert.DeserializeObject<ItemPhysProp>(PPTabParamter);

            //if (id == "" && CurrentItemId.Text != "")
            //{
            //    id = CurrentItemId.Text;
            //}
            //b.recordId = id;


            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(id))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    //PostRequest<Item> request = new PostRequest<Item>();
                    //request.entity = b;
                    //PostResponse<Item> r = _inventoryService.ChildAddOrUpdate<Item>(request);
                    //b.recordId = r.recordId;

                    ////check if the insert failed
                    //if (!r.Success)//it maybe be another condition
                    //{
                    //    //Show an error saving...

                    //    Common.errorMessage(r);
                    //    return;
                    //}
                    //else
                    //{


                    //    //Add this record to the store 
                      

                    //    //Display successful notification
                    //    Notification.Show(new NotificationConfig
                    //    {
                    //        Title = Resources.Common.Notification,
                    //        Icon = Icon.Information,
                    //        Html = Resources.Common.RecordSavingSucc
                    //    });
                    //    if (Store1 != null)
                    //        this.Store1.Reload();

                    //    //this.EditRecordWindow.Close();
                    //    CurrentItemId.Text = b.recordId;
                    //    //RowSelectionModel sm = this.ItemGridPanel.GetSelectionModel() as RowSelectionModel;
                    //    //sm.DeselectAll();
                    //    //sm.Select(b.recordId.ToString());

                    //    PriceGridPanel.Disabled = false;

                    //    PPTab.Disabled = false;


                    
                }
                catch (Exception ex)
                {
                    //Error exception displaying a messsage box
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorSavingRecord).Show();
                }


            }
            else
            {
                //Update Mode

                try
                {
                    int index = Convert.ToInt32(id);//getting the id of the record
                    //if (panelRecordDetails.ActiveIndex==0)
                   



                }
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }
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

        protected void BasicInfoTab_Load(object sender, EventArgs e)
        {

        }



       








    }
}