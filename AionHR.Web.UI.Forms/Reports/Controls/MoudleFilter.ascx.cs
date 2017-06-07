﻿using AionHR.Services.Messaging.Reports;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                moduleId.Select(0);
                if (!string.IsNullOrEmpty(selectHandler))
                    moduleId.AddListener("Select", selectHandler);
            }
        }

        public ClassIdParameterSet GetModule()
        {
            ClassIdParameterSet s = new ClassIdParameterSet();
            int bulk;
            if (moduleId.Value == null || !int.TryParse(moduleId.Value.ToString(), out bulk))
                s.ClassId = 20;
            else
                s.ClassId = bulk;

            return s;
        }

        public string GetModuleId()
        {
            int bulk;
            if (moduleId.Value == null || !int.TryParse(moduleId.Value.ToString(), out bulk))
                return "20";
            else
                return moduleId.Value.ToString();

        }

        private string selectHandler;

        public string SelectHandler
        {
            get
            {
                return selectHandler;
            }

            set
            {
                selectHandler = value;
            }
        }
    }
}