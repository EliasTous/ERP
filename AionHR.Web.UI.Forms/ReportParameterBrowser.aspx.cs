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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
                this.ResourceManager2.RTL = true;

                this.Viewport2.RTL = true;


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
                case 51201: return "PayIdFilter.ascx";
                case 31107: return "EmploymentStatusFilter.ascx";

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
                bool fillValues = false;
                Dictionary<string, string> valuesDict = null;
                if(!string.IsNullOrEmpty(Request.QueryString["values"]))
                {
                    fillValues = true;
                    valuesDict = new Dictionary<string, string>();
                    string[] valsPairs = Request.QueryString["values"].Split('^');
                    foreach(var item in valsPairs)
                    {
                        string[] pair = item.Split('|');
                        valuesDict.Add(pair[0], pair[1]);
                    }
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
                        
                        case 1: TextField tf = new TextField() { ID = "tb_" + item.id , FieldLabel=item.caption };
                            FormPanel1.Items.Add(tf);
                            if (valuesDict != null && valuesDict.ContainsKey(item.id)) tf.Text = valuesDict[item.id]; break;
                        case 3:
                        case 2:NumberField nf = new NumberField() { ID = "nb_" + item.id, FieldLabel = item.caption };
                            if (valuesDict != null && valuesDict.ContainsKey(item.id))
                                nf.Value = Convert.ToDouble(valuesDict[item.id]);
                                FormPanel1.Items.Add(nf);break;
                        case 4: DateField d = new DateField() {  ID = "date_" + item.id, FieldLabel = item.caption }; FormPanel1.Items.Add(d);
                            if (valuesDict != null && valuesDict.ContainsKey(item.id))
                                d.SelectedDate = DateTime.ParseExact(valuesDict[item.id],"yyyyMMdd",new CultureInfo("en"));
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
                                box.Name = "control_" + item.id;
                                box.AnyMatch = true;
                                box.CaseSensitive = false;
                                box.Editable = false;
                                
                                box.FieldLabel = item.caption;
                                if (valuesDict != null && valuesDict.ContainsKey(item.id))
                                    box.Select(valuesDict[item.id]);
                                FormPanel1.Items.Add(box);


                                break;
                            }
                            Control cont = LoadControl("Reports/Controls/" + GetControlNameByClassId(item.classId));
                            IComboControl contAsCombo = cont as IComboControl;
                            if (contAsCombo != null)
                            {
                                contAsCombo.SetLabel(item.caption);
                             
                            }
                            if(item.classId== 31201)
                            {
                                ((EmployeeFilter)cont).EmployeeComboBox.FieldLabel = item.caption;
                                ((EmployeeFilter)cont).EmployeeComboBox.EmptyText = "";
                            }
                            if (item.classId == 31107)
                            {
                                ((EmploymentStatusFilter)cont).Filter.FieldLabel = ((EmploymentStatusFilter)cont).Filter.EmptyText;
                                ((EmploymentStatusFilter)cont).Filter.EmptyText = "";
                            }
                            cont.ID= "control_" + item.id;
                            
                            if(valuesDict!=null && valuesDict.ContainsKey(item.id))
                            {
                               
                                if (contAsCombo != null)
                                    contAsCombo.Select(valuesDict[item.id]);
                            }
                             
                            Container c = new Container() { Layout = "FitLayout" };
                            c.ContentControls.Add(cont);
                            FormPanel1.Items.Add(c);
                            break;
                        case 6: Checkbox cb = new Checkbox() { ID = "control_" + item.id, FieldLabel = item.caption }; cb.InputValue = "true"; if (valuesDict != null && valuesDict.ContainsKey(item.id)) cb.Checked
                                    = valuesDict[item.id] == "true"; break;
                        default: X.Msg.Alert("Error", "unknown control"); break;
                    }
                    labels += item.caption+"^";
                    
                }
                labels.TrimEnd('^');
                X.Call("parent.setLabels", labels);
                this.labels.Text = labels;
            }
        }


        protected void SaveProperties(object sender, DirectEventArgs e)
        {


            string vals = "";
            string texts = "";
            string text_id = "";
            string friendlyText = "";
            string[] allCaptions = labels.Text.Split('^');
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
                    if (((pair[0].Split('_')[0] == "tb")|| (pair[0].Split('_')[0] == "nb")) && !string.IsNullOrEmpty(pair[1]))
                    {
                        vals += pair[0].Split('_')[1] + "|" + pair[1] + "^";
                        texts+= "["+allCaptions[Convert.ToInt32(pair[0].Split('_')[1])-1]+":"+pair[1]+"]";
                        

                    }
                    if (pair[0] == "text")
                    {
                        string cont = pair[1];
                        if(cont.StartsWith("u")&& cont.Length>5&& cont[5]=='u')
                        {
                            string[] words = cont.Split(' ');
                            cont = "";
                            
                            foreach(string word in words)
                            {
                                string[] chars = word.Split('u');
                                foreach(string uchar in chars)
                                {
                                    cont += DirtyUTFWork(uchar);
                                }
                            }
                        }
                        texts += "["+allCaptions[Convert.ToInt32(text_id)-1]+":"+cont + "]";
                        
                    }
                    if (pair[0].StartsWith("date_") && !string.IsNullOrEmpty(pair[1]))
                    {
                        vals += pair[0].Split('_')[1] + "|" + DateTime.Parse(pair[1]).ToString("yyyyMMdd") + "^";
                        texts+= "[" + allCaptions[Convert.ToInt32(pair[0].Split('_')[1]) - 1] + ":" + DateTime.Parse(pair[1]).ToString(_systemService.SessionHelper.GetDateformat()) + "]";
                    }
                }
            }
            vals = vals.TrimEnd('^');
            texts = texts.TrimEnd('^');
            X.Call("parent.setVals", vals);
            X.Call("parent.showFriendlyText",texts.Replace("][","$").Replace('[',' ').Replace(']',' '));

            X.Call("parent.setTexts", texts);
            
        }
        public  string DecodeFromUtf8( string utf8String)
        {
            // copy the string as UTF-8 bytes.
            byte[] utf8Bytes = new byte[utf8String.Length];
            for (int i = 0; i < utf8String.Length; ++i)
            {
                //Debug.Assert( 0 <= utf8String[i] && utf8String[i] <= 255, "the char must be in byte's range");
                utf8Bytes[i] = (byte)utf8String[i];
            }

            return Encoding.UTF8.GetString(utf8Bytes, 0, utf8Bytes.Length);
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
            return Common.GetEmployeesFiltered(prms.Query);
        }

        

        private string GetNameFormat()
        {
            return _systemService.SessionHelper.Get("nameFormat").ToString();
        }
        private  string DirtyUTFWork(string number)
        {
            switch (number)
            {

                case "0621": return "ء";
                case "0622": return "آ";
                case "0623": return "أ";
                case "0624": return "ؤ";
                case "0625": return "إ";
                case "0626": return "ئ";
                case "0627": return "ا";
                case "0628": return "ب";
                case "0629": return "ة";
                case "062a": return "ت";
                case "062b": return "ث";
                case "062c": return "ج";
                case "062d": return "ح";
                case "062e": return "خ";
                case "062f": return "د";
                case "0630": return "ذ";
                case "0631": return "ر";
                case "0632": return "ز";
                case "0633": return "س";
                case "0634": return "ش";
                case "0635": return "ص";
                case "0636": return "ض";
                case "0637": return "ط";
                case "0638": return "ظ";
                case "0639": return "ع";
                case "063a": return "غ";
                case "0641": return "ف";
                case "0642": return "ق";
                case "0643": return "ك";
                case "0644": return "ل";
                case "0645": return "م";
                case "0646": return "ن";
                case "0647": return "ه";
                case "0648": return "و";
                case "0649": return "ى";
                case "064a": return "ي";
                default: return " ";
            }
        }
    }
    
}