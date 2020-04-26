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
using Model.Attendance;
using Model.Employees.Leaves;
using Model.Employees.Profile;
using Web.UI.Forms.ConstClasses;
using Services.Messaging.CompanyStructure;

namespace Web.UI.Forms
{
    public partial class WorkFlows : System.Web.UI.Page
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        ICompanyStructureService _companyStructureRepository = ServiceLocator.Current.GetInstance<ICompanyStructureService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();

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
                HideShowColumns();
                try
                {
                    AccessControlApplier.ApplyAccessControlOnPage(typeof(WorkFlow), BasicInfoTab, GridPanel1, btnAdd, SaveButton);
                }
                catch (AccessDeniedException exp)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorAccessDenied).Show();
                    Viewport1.Hidden = true;
                    return;
                }
                FillBranch();
                FillDepartment();
                FillapproverPositionStore();


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
        private void HideShowColumns()
        {
            this.colAttach.Visible = false;
        }


        private void SetExtLanguage()
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            if (rtl)
            {
                this.ResourceManager1.RTL = true;
                this.Viewport1.RTL = true;

            }
        }


        protected void SubmitData(object sender, StoreSubmitDataEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(currentWorkFlowId.Text))
                    return;
                deleteAllWorkSequence(currentWorkFlowId.Text);
                List<WorkSequence> PN = e.Object<WorkSequence>();

                PostRequest<WorkSequence> req = new PostRequest<WorkSequence>();
                int counter = 1;
                PN.ForEach(x =>
                {

                    req.entity = x;
                    req.entity.seqNo = counter;
                    req.entity.wfId = currentWorkFlowId.Text;
                    counter++;
                    PostResponse<WorkSequence> resp1 = _companyStructureRepository.ChildAddOrUpdate<WorkSequence>(req);
                    if (!resp1.Success)
                    {
                        Common.errorMessage(resp1);
                        throw new Exception();
                    }
                });
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.RecordSavingSucc
                });
            }
            catch( Exception exp)
            {
                X.MessageBox.Alert(Resources.Common.Error, exp.Message).Show();
            }

        }
        private void FillWorkSequenceStore(string currentWfId)
        {
            WorkSequenceListRequest req = new WorkSequenceListRequest();
            req.wfId = currentWfId;
            ListResponse<WorkSequence> resp = _companyStructureRepository.ChildGetAll<WorkSequence>(req); 
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return; 
            }
            if (resp.Items.Count!=0)
            currentSeq.Text = resp.Items.OrderBy(x => x.seqNo).Last().seqNo.ToString();
            workSequenceStore.DataSource = resp.Items;
            workSequenceStore.DataBind(); 

        }

        private void deleteAllWorkSequence(string currentWfId)
        {
            WorkSequenceListRequest req = new WorkSequenceListRequest();
            req.wfId = currentWfId;
            ListResponse<WorkSequence> resp = _companyStructureRepository.ChildGetAll<WorkSequence>(req);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }

            PostRequest<WorkSequence> req1 = new PostRequest<WorkSequence>();
            resp.Items.ForEach(x=>
            {
                req1.entity = x;
                PostResponse<WorkSequence> delresp = _companyStructureRepository.ChildDelete<WorkSequence>(req1);
                if (!delresp.Success)
                {
                    Common.errorMessage(delresp);
                    throw new Exception();
                }

            });

        }
        protected void PoPuP(object sender, DirectEventArgs e)
        {


            string id = e.ExtraParams["id"];
            string type = e.ExtraParams["type"];
            panelRecordDetails.ActiveIndex = 0;
            currentWorkFlowId.Text = id;
            workSequenceGrid.Disabled = false;
           
            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 
                    RecordRequest r = new RecordRequest();
                    r.RecordID = id;

                    RecordResponse<WorkFlow> response = _companyStructureRepository.ChildGetRecord<WorkFlow>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.BasicInfoTab.SetValues(response.result);

                    FillWorkSequenceStore(id);
                   
                    this.EditRecordWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditRecordWindow.Show();
                    break;

                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteRecord({0})", id),
                            Text = Resources.Common.Yes
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
        private void FillDepartment()
        {
            DepartmentListRequest req = new DepartmentListRequest();
            req.type = 0;

            ListResponse<Department> response = _companyStructureRepository.ChildGetAll<Department>(req);
            if (!response.Success)
            {
                Common.errorMessage(response);
                departmentStore.DataSource = new List<Department>();
            }
            departmentStore.DataSource = response.Items;
            departmentStore.DataBind();
        }
        private void FillBranch()
        {

            ListRequest branchesRequest = new ListRequest();
            ListResponse<Branch> resp = _companyStructureRepository.ChildGetAll<Branch>(branchesRequest);
            if (!resp.Success)
                Common.errorMessage(resp);
            branchStore.DataSource = resp.Items;
            branchStore.DataBind();
            if (_systemService.SessionHelper.CheckIfIsAdmin())
                return;

        }
        private void FillapproverPositionStore()
        {
            approverPositionStore.DataSource = Common.XMLDictionaryList(_systemService, "28");
            approverPositionStore.DataBind(); 
        }
        protected void PoPuPWS(object sender, DirectEventArgs e)
        {


            string branchIdParameter  = e.ExtraParams["branchId"];
            string departmentIdParameter = e.ExtraParams["departmentId"];
            string seqNo = e.ExtraParams["seqNo"];
            string approverPositionParameter = e.ExtraParams["approverPosition"];
            string type = e.ExtraParams["type"];



            switch (type)
            {
                case "imgEdit":
                    //Step 1 : get the object from the Web Service 

                    //Step 2 : call setvalues with the retrieved object

                    WorkSequenceRecordRequest r = new WorkSequenceRecordRequest();
                    r.seqNo = seqNo;
                    r.wfId = currentWorkFlowId.Text;

                    RecordResponse<WorkSequence> response = _companyStructureRepository.ChildGetRecord<WorkSequence>(r);
                    if (!response.Success)
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(response);
                        return;
                    }
                    //Step 2 : call setvalues with the retrieved object
                    this.WSForm.SetValues(response.result);

                    seqNO.Text = seqNo;
                   
                    //branchId.Select(branchIdParameter);
                    //branchId.SetValue(branchIdParameter);
                    //departmentId.Select(departmentIdParameter);
                    //departmentId.SetValue(departmentIdParameter);
                    //approverPosition.Select(approverPositionParameter);
                    //approverPosition.SetValue(approverPositionParameter);
                    



                    this.EditWSWindow.Title = Resources.Common.EditWindowsTitle;
                    this.EditWSWindow.Show();
                    break;

             


                case "imgDelete":
                    X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteOneRecord, new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            //We are call a direct request metho for deleting a record
                            Handler = String.Format("App.direct.DeleteWSRecord({0})",Convert.ToInt32( seqNo)),
                            Text = Resources.Common.Yes
                        },
                        No = new MessageBoxButtonConfig
                        {
                            Text = Resources.Common.No
                        }

                    }).Show();
                    break;


                    Notification.Show(new NotificationConfig
                    {
                        Title = Resources.Common.Notification,
                        Icon = Icon.Information,
                        Html = Resources.Common.RecordUpdatedSucc
                    });
                 


                    break;

                case "imgAttach":

                    //Here will show up a winow relatice to attachement depending on the case we are working on
                    break;
                default:
                    break;
            }


        }

        /// <summary>
        /// This direct method will be called after confirming the delete
        /// </summary>
        /// <param name="index">the ID of the object to delete</param>
        [DirectMethod]
        public void DeleteRecord(string index)
        {
            try
            {
                //Step 1 Code to delete the object from the database 
                WorkFlow s = new WorkFlow();
                s.recordId = index;
                //s.reference = "";

                s.name = "";
                PostRequest<WorkFlow> req = new PostRequest<WorkFlow>();
                req.entity = s;
                PostResponse<WorkFlow> r = _companyStructureRepository.ChildDelete<WorkFlow>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(r);
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    Store1.Remove(index);

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

        [DirectMethod]
        public void DeleteWSRecord(int index)
        {
            try
            {

                //Step 2 :  remove the object from the store
                WorkSequence s = new WorkSequence();
                s.seqNo = index;
                //s.reference = "";

                s.wfId = currentWorkFlowId.Text;
                PostRequest<WorkSequence> req = new PostRequest<WorkSequence>();
                req.entity = s;
                PostResponse<WorkSequence> r = _companyStructureRepository.ChildDelete<WorkSequence>(req);
                if (!r.Success)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    Common.errorMessage(r);
                    return;
                }
                else
                {
                    //Step 2 :  remove the object from the store
                    workSequenceStore.Remove(index);

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





        /// <summary>
        /// Deleting all selected record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteAll(object sender, DirectEventArgs e)
        {


            RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;
            if (sm.SelectedRows.Count() <= 0)
                return;
            X.Msg.Confirm(Resources.Common.Confirmation, Resources.Common.DeleteManyRecord, new MessageBoxButtonsConfig
            {
                //Calling DoYes the direct method for removing selecte record
                Yes = new MessageBoxButtonConfig
                {
                    Handler = "App.direct.DoYes()",
                    Text = Resources.Common.Yes
                },
                No = new MessageBoxButtonConfig
                {
                    Text = Resources.Common.No
                }

            }).Show();
        }

        /// <summary>
        /// Direct method for removing multiple records
        /// </summary>
        [DirectMethod(ShowMask = true)]
        public void DoYes()
        {
            try
            {
                RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;

                foreach (SelectedRow row in sm.SelectedRows)
                {
                    //Step 1 :Getting the id of the selected record: it maybe string 
                    int id = int.Parse(row.RecordID);


                    //Step 2 : removing the record from the store
                    //To do add code here 

                    //Step 3 :  remove the record from the store
                    Store1.Remove(id);

                }
                //Showing successful notification
                Notification.Show(new NotificationConfig
                {
                    Title = Resources.Common.Notification,
                    Icon = Icon.Information,
                    Html = Resources.Common.ManyRecordDeletedSucc
                });

            }
            catch (Exception ex)
            {
                //Alert in case of any failure
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorDeletingRecord).Show();

            }
        }

        /// <summary>
        /// Adding new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ADDNewRecord(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            BasicInfoTab.Reset();
            currentSeq.Text = string.Empty;
            currentWorkFlowId.Text = string.Empty;
            workSequenceGrid.Disabled = true;
            panelRecordDetails.ActiveIndex = 0;
            this.EditRecordWindow.Title = Resources.Common.AddNewRecord;


            this.EditRecordWindow.Show();
        }
        protected void ADDNewWSRecord(object sender, DirectEventArgs e)
        {

            //Reset all values of the relative object
            WSForm.Reset();


            //FillWorkSequenceStore(currentWorkFlowId.Text);
            this.EditWSWindow.Title = Resources.Common.AddNewRecord;


            this.EditWSWindow.Show();
        }

        protected void Store1_RefreshData(object sender, StoreReadDataEventArgs e)
        {

            //GEtting the filter from the page
            string filter = string.Empty;
            int totalCount = 1;



            //Fetching the corresponding list

            //in this test will take a list of News
            ListRequest request = new ListRequest();

            request.Filter = "";
            ListResponse<WorkFlow> routers = _companyStructureRepository.ChildGetAll<WorkFlow>(request);
            if (!routers.Success)
                return;
            this.Store1.DataSource = routers.Items;
            e.Total = routers.Items.Count; ;

            this.Store1.DataBind();
        }




        protected void SaveNewRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.

            string count = e.ExtraParams["count"];
            string obj = e.ExtraParams["values"];
            WorkFlow b = JsonConvert.DeserializeObject<WorkFlow>(obj);

            string id = e.ExtraParams["id"];
            // Define the object to add or edit as null
            
            if (string.IsNullOrEmpty(currentWorkFlowId.Text))
            {

                try
                {
                    //New Mode
                    //Step 1 : Fill The object and insert in the store 
                    PostRequest<WorkFlow> request = new PostRequest<WorkFlow>();

                    request.entity = b;
                    PostResponse<WorkFlow> r = _companyStructureRepository.ChildAddOrUpdate<WorkFlow>(request);


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
                        b.recordId = r.recordId;
                        currentWorkFlowId.Text = r.recordId;
                        //Add this record to the store 
                        this.Store1.Insert(0, b);

                        //Display successful notification
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        workSequenceStore.DataSource = new List<WorkSequence>();
                        workSequenceStore.DataBind();
                        workSequenceGrid.Disabled = false;



                    }
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
                    //getting the id of the record
                    PostRequest<WorkFlow> request = new PostRequest<WorkFlow>();
                    request.entity = b;
                    request.entity.recordId = currentWorkFlowId.Text;

                    PostResponse<WorkFlow> r = _companyStructureRepository.ChildAddOrUpdate<WorkFlow>(request);                      //Step 1 Selecting the object or building up the object for update purpose

                    //Step 2 : saving to store

                    //Step 3 :  Check if request fails
                    if (!r.Success)//it maybe another check
                    {
                        X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                        Common.errorMessage(r);
                        return;
                    }
                    else
                    {


                        Store1.Reload();
                        recordId.Text = currentWorkFlowId.Text;
                        Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });
                        this.EditRecordWindow.Close();


                    }
                    

                }
                
                catch (Exception ex)
                {
                    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                    X.Msg.Alert(Resources.Common.Error, Resources.Common.ErrorUpdatingRecord).Show();
                }
            }
        }

        protected void SaveNewWSRecord(object sender, DirectEventArgs e)
        {


            //Getting the id to check if it is an Add or an edit as they are managed within the same form.

           
            string obj = e.ExtraParams["values"];
            string seqNO = e.ExtraParams["seqNO"];
            WorkSequence b = JsonConvert.DeserializeObject<WorkSequence>(obj);
            b.approverPositionName = approverPosition.SelectedItem.Text;
            b.branchName = branchId.SelectedItem.Text;
            b.departmentName = departmentId.SelectedItem.Text;
            if (string.IsNullOrEmpty(seqNO))

                b.seqNo = null;
            else

                b.seqNo= Convert.ToInt32(seqNO);


            // Define the object to add or edit as null

            if (string.IsNullOrEmpty(seqNO))
            {

                try
                {
                    PostRequest<WorkSequence> request = new PostRequest<WorkSequence>();
                    request.entity = b;
                    request.entity.wfId = currentWorkFlowId.Text;
                    if (string.IsNullOrEmpty(currentSeq.Text))
                    {


                        request.entity.seqNo = 1;
                        currentSeq.Text = "1";

                    }
                    else
                    {
                        request.entity.seqNo = Convert.ToInt32(currentSeq.Text) + 1;
                        currentSeq.Text = (Convert.ToInt32(currentSeq.Text) + 1).ToString();
                    }

                    PostResponse<WorkSequence> r = _companyStructureRepository.ChildAddOrUpdate<WorkSequence>(request);
                    if (!r.Success)
                    {
                        Common.errorMessage(r);
                        return;
                    }

                    FillWorkSequenceStore(currentWorkFlowId.Text);
                    Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordSavingSucc
                        });

                        this.EditWSWindow.Close();
                       



                    
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


                    PostRequest<WorkSequence> request = new PostRequest<WorkSequence>();
                    request.entity = b;
                    request.entity.wfId = currentWorkFlowId.Text;



                    PostResponse<WorkSequence> r = _companyStructureRepository.ChildAddOrUpdate<WorkSequence>(request);
                    if (!r.Success)
                    {
                        Common.errorMessage(r);
                        return;
                    }


                    Notification.Show(new NotificationConfig
                        {
                            Title = Resources.Common.Notification,
                            Icon = Icon.Information,
                            Html = Resources.Common.RecordUpdatedSucc
                        });
                    FillWorkSequenceStore(currentWorkFlowId.Text);
                        this.EditWSWindow.Close();


                    

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