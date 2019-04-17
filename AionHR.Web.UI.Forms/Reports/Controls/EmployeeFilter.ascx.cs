using AionHR.Model.Employees.Profile;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.Reports;
using Ext.Net;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AionHR.Web.UI.Forms.Reports.Controls
{
    public partial class EmployeeFilter : System.Web.UI.UserControl,IComboControl
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public ComboBox EmployeeComboBox { get
            {
                return employeeFilter;
            } }
        public EmployeeParameterSet GetEmployee()
        {
            EmployeeParameterSet s = new EmployeeParameterSet();
            int bulk;
            if (employeeFilter.Value == null || !int.TryParse(employeeFilter.Value.ToString(), out bulk))

                s.employeeId = 0;
            else
                s.employeeId = bulk;

            return s;
        }
        public String GetEmployeeName()
        {
           
            int bulk;
            if (employeeFilter.Value == null || !int.TryParse(employeeFilter.Value.ToString(), out bulk))

                return "";
            else
                return employeeFilter.SelectedItem.Text.ToString();


        }

        public void Select(object id)
        {
            RecordRequest req = new RecordRequest();
            req.RecordID = id.ToString();

            RecordResponse < Employee > response= _employeeService.Get<Employee>(req);
            List<Employee> onlyone = new List<Employee>();
            response.result.fullName = response.result.name.fullName;
            onlyone.Add(response.result);
            Store2.DataSource = onlyone;
            Store2.DataBind();
            
            employeeFilter.Select(response.result.recordId);
            
        }

        public void SetLabel(string newLabel)
        {
            employeeFilter.FieldLabel = newLabel;
        }
    }
}