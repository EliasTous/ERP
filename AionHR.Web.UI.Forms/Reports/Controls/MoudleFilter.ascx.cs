using AionHR.Model.System;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.Reports;
using AionHR.Services.Messaging.System;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AionHR.Web.UI.Forms.Reports.Controls
{
    public partial class MoudleFilter : System.Web.UI.UserControl
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        List<XMLDictionary> moduleList = new List<XMLDictionary>(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillModulesStore();
                modulesCombo.Select(0);
                modulesCombo.SetValue(moduleList.Count != 0 ? moduleList.First().value : "10");
            }
        }

        public ClassIdParameterSet GetModule()
        {
            ClassIdParameterSet s = new ClassIdParameterSet();
            int bulk;
            if (modulesCombo.Value == null || !int.TryParse(modulesCombo.Value.ToString(), out bulk))
                s.ClassId = 20;
            else
                s.ClassId = bulk;

            return s;
        }

        public string GetModuleId()
        {
            int bulk;
            if (modulesCombo.Value == null || !int.TryParse(modulesCombo.Value.ToString(), out bulk))
                return moduleList.Count != 0 ? moduleList.First().value : "10";
            else
                return modulesCombo.Value.ToString();

        }

      
        private void FillModulesStore()
        {
            XMLDictionaryListRequest request = new XMLDictionaryListRequest();

            request.database = "1";
            ListResponse<XMLDictionary> resp = _systemService.ChildGetAll<XMLDictionary>(request);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return;
            }
            moduleList = resp.Items; 
            this.modulesStore.DataSource = resp.Items;


            this.modulesStore.DataBind();
        }
        public void ADDHandler(string Event,string Function)
        {
           
            this.modulesCombo.AddListener(Event, "function() {" + Function + "}");
        }
    }
}