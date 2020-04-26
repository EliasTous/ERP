using Model.System;
using Services.Interfaces;
using Services.Messaging;
using Services.Messaging.Reports;
using Services.Messaging.System;
using Ext.Net;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.UI.Forms.Reports.Controls
{
    public partial class SalaryTypeFilter : System.Web.UI.UserControl,IComboControl
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        public List<XMLDictionary> salaryTypeList = new List<XMLDictionary>();
        public string SetText { get; set; }
        public string Width { get; set; }
        public string setEmptyText { get; set; }
        public string setLabelWidth { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    //XMLDictionaryListRequest request = new XMLDictionaryListRequest();

                    //request.database = "2";
                    //ListResponse<XMLDictionary> resp = _systemService.ChildGetAll<XMLDictionary>(request);
                    //if (!resp.Success)
                    //{
                    //    Common.errorMessage(resp);
                    //    return;
                    //}
                    salaryTypeList.AddRange(Common.XMLDictionaryList(_systemService,"2"));
                    FillSalaryTypeStore();
                    //  salaryTypeId.Select(0);
                    if (string.IsNullOrEmpty(Width))
                    {
                     
                        this.salaryTypeId.SetWidth(Convert.ToInt16(Width));
                    }

                    if (string.IsNullOrEmpty(SetText))

                        this.salaryTypeId.EmptyText = GetGlobalResourceObject("Common", "SalaryType").ToString();
                    else
                        this.salaryTypeId.FieldLabel = SetText.ToString();
                    if (!string.IsNullOrEmpty(setLabelWidth))
                        this.salaryTypeId.LabelWidth = Convert.ToInt32(setLabelWidth);
                    
                }
                
            }
            catch(Exception exp)

            {
             

            }
        }

        public SalaryTypeParameterSet GetSalaryType()
        {
            SalaryTypeParameterSet s = new SalaryTypeParameterSet();
            int bulk;
            if (salaryTypeId.Value == null || !int.TryParse(salaryTypeId.Value.ToString(), out bulk))
                s.SalaryTypeId = 0;
            else
                s.SalaryTypeId = bulk;

            return s;
        }
        public string GetSalaryTypeId()
        {
            int bulk;
            if (salaryTypeId.Value == null || !int.TryParse(salaryTypeId.Value.ToString(), out bulk))
                return "0";
            else
                return salaryTypeId.Value.ToString();

        }
        public string GetSalaryTypeString()

        {
            return salaryTypeId.SelectedItem.Text;
        }

        private void FillSalaryTypeStore()
        {
            //XMLDictionaryListRequest request = new XMLDictionaryListRequest();

            //request.database = "2";
            //ListResponse<XMLDictionary> resp = _systemService.ChildGetAll<XMLDictionary>(request);
            //if (!resp.Success)
            //{
            //    Common.errorMessage(resp);
            //    return;
            //}

            this.salaryTypeStore.DataSource = salaryTypeList;


            this.salaryTypeStore.DataBind();
        }
        public void ADDHandler(string Event, string Function)
        {

            this.salaryTypeId.AddListener(Event, "function() {" + Function + "}");
        }
        public void setSalaryType(string value)
        {
            this.salaryTypeId.Select(value);
            this.salaryTypeId.SetValue(value);
        }

        public void Select(object id)
        {
            salaryTypeId.Select(id);
        }

        public void SetLabel(string newLabel)
        {
            salaryTypeId.FieldLabel = newLabel;
        }

        public ComboBox GetComboBox()
        {
            return salaryTypeId;
        }
    }
}