using AionHR.Model.AssetManagement;
using AionHR.Model.Employees.Profile;
using AionHR.Model.Reports;
using AionHR.Model.System;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.Asset_Management;
using AionHR.Services.Messaging.Reports;
using AionHR.Web.UI.Forms.Reports;
using AionHR.Web.UI.Forms.Reports.Controls;
using AionHR.Web.UI.Forms.Utilities;
using Ext.Net;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AionHR.Web.UI.Forms
{
    public partial class ReportParameterBrowser : System.Web.UI.Page
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
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        private void SetExtLanguage()
        {
            bool rtl = _systemService.SessionHelper.CheckIfArabicSession();
            if (rtl)
            {
                

            }
        }
        private List<UserControl> activeControls;
        private string GetControlNameByClassId(int classId)
        {
            switch (classId)
            {
                case 21102: return "BranchFilter.ascx";
                case 41105: return "ScheduleFilter.ascx";
                case 21104: return "DepartmentFilter.ascx";
                case 21103: return "PositionFilter.ascx";
                case 21101: return "DivisionFilter.ascx";
                case 20105: return "NationalityFilter.ascx";
                case 31201: return "EmployeeFilter.ascx";
                case 31108: return "SponsorFilter.ascx";
                
                default: X.Msg.Alert("Error","unknown control"); return "";

            }   
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest && !IsPostBack)
            {
                activeControls = new List<UserControl>();
                SetExtLanguage();
                if (string.IsNullOrEmpty(Request.QueryString["_reportName"]))
                {
                    X.Msg.Alert("Error", "Error");
                }
                ReportParametersListRequest req = new ReportParametersListRequest();
                req.ReportName = Request.QueryString["_reportName"];
                ListResponse<ReportParameter> parameters = _systemService.ChildGetAll<ReportParameter>(req);
                if (!parameters.Success)
                {
                    X.Msg.Alert("Error", "Error");
                }
                int i = -1;

                string labels = "";
                foreach (var item in parameters.Items)
                {
                    i += 1;
                    
                    switch (item.controlType)
                    {
                        
                        case 1: TextField tf = new TextField() { ID = "control_" + item.id , FieldLabel=item.caption };
                            FormPanel1.Items.Add(tf); break;
                        case 3:
                        case 2:NumberField nf = new NumberField() { ID = "control_" + item.id, FieldLabel = item.caption };
                            FormPanel1.Items.Add(nf);break;
                        case 4: DateField d = new DateField() { ID = "date_" + item.id, FieldLabel = item.caption }; FormPanel1.Items.Add(d);
                           
                            break;
                        
                        case 5:
                            if(item.classId==0)
                            {
                                ComboBox box = new ComboBox();
                                List<XMLDictionary> dict = Common.XMLDictionaryList(_systemService, item.data);
                                box.Triggers.Add(new FieldTrigger() { Icon = TriggerIcon.Clear });
                                box.Listeners.TriggerClick.Handler = "this.clearValue();";
                                foreach(var xml_elem in dict)
                                {
                                    box.Items.Add(new Ext.Net.ListItem(xml_elem.value, xml_elem.key));
                                    
                                }
                                box.ID = "control_" + item.id;
                                box.AnyMatch = true;
                                box.CaseSensitive = false;
                                box.Editable = false;
                                
                                box.FieldLabel = item.caption;
                                FormPanel1.Items.Add(box);


                                break;
                            }
                            Control cont = LoadControl("Reports/Controls/" + GetControlNameByClassId(item.classId));
                            if(item.classId== 31201)
                            {
                                ((EmployeeFilter)cont).EmployeeComboBox.FieldLabel = ((EmployeeFilter)cont).EmployeeComboBox.EmptyText;
                                ((EmployeeFilter)cont).EmployeeComboBox.EmptyText = "";
                            }
                            cont.ID= "control_" + item.id;
                            Container c = new Container() { Layout = "FitLayout" };
                            c.ContentControls.Add(cont);
                            FormPanel1.Items.Add(c);
                            break;
                        case 6: Checkbox cb = new Checkbox() { ID = "control_" + item.id, FieldLabel = item.caption }; cb.InputValue = "true"; break;
                        default: X.Msg.Alert("Error", "unknown control"); break;
                    }
                    labels += item.caption+"^";
                    
                }
                labels.TrimEnd('^');
                X.Call("parent.setLabels", labels);
            }
        }


        protected void SaveProperties(object sender, DirectEventArgs e)
        {


            string vals = "";
            string texts = "";
            string text_id = "";
            string[] values = e.ExtraParams["values"].Replace("[", "")
                .Replace("]", "").Replace("\"", "").Replace("{", ",").
                Replace("}", ",").Replace("\\", "").Split(',');
            for (int i = 0; i < values.Length; i++)
            {
                if (string.IsNullOrEmpty(values[i]))
                    continue;
                string[] pair = values[i].Split(':');
                if (pair.Length == 1)
                    continue;
                if(pair.Length==2)
                {
                    if (pair[0].EndsWith("_state")|| pair[0]=="index" || pair[0]=="value")
                        continue;

                    if (pair[0].Split('_')[0] == "control" && ! string.IsNullOrEmpty(pair[1]))
                    {
                        vals += pair[0].Split('_')[1] + "|" + pair[1] + "^";
                        text_id = pair[0].Split('_')[1];


                    }

                    if (pair[0] == "text")
                    {
                        texts += text_id+"|"+pair[1] + "^";
                        
                    }
                    if (pair[0].StartsWith("date_") && !string.IsNullOrEmpty(pair[1]))
                    {
                        vals += pair[0].Split('_')[1] + "|" + DateTime.Parse(pair[1]).ToString("yyyyMMdd") + "^";
                        
                    }
                }
            }
            vals = vals.TrimEnd('^');
            texts = texts.TrimEnd('^');
            X.Call("parent.setVals", vals);
            X.Call("parent.setTexts", texts);
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

        [DirectMethod]
        public object FillEmployee(string action, Dictionary<string, object> extraParams)
        {
            StoreRequestParameters prms = new StoreRequestParameters(extraParams);
            List<Employee> data = GetEmployeesFiltered(prms.Query);
            data.ForEach(s => { s.fullName = s.name.fullName; });
            //  return new
            // {
            return data;
        }

        private List<Employee> GetEmployeesFiltered(string query)
        {

            EmployeeListRequest req = new EmployeeListRequest();
            req.DepartmentId = "0";
            req.BranchId = "0";
            req.IncludeIsInactive = 2;
            req.SortBy = GetNameFormat();

            req.StartAt = "0";
            req.Size = "20";
            req.Filter = query;

            ListResponse<Employee> response = _employeeService.GetAll<Employee>(req);
            return response.Items;
        }

        private string GetNameFormat()
        {
            return _systemService.SessionHelper.Get("nameFormat").ToString();
        }
    }
}