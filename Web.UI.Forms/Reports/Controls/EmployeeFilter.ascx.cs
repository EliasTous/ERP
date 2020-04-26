using Model.Employees.Profile;
using Services.Interfaces;
using Services.Messaging;
using Services.Messaging.Reports;
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
    public partial class EmployeeFilter : System.Web.UI.UserControl,IComboControl
    {
        ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        IEmployeeService _employeeService = ServiceLocator.Current.GetInstance<IEmployeeService>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public ComboBox EmployeeComboBox { get
            {
                return employeeId;
            } }
        public EmployeeParameterSet GetEmployee()
        {
            EmployeeParameterSet s = new EmployeeParameterSet();
            int bulk;
            if (employeeId.Value == null || !int.TryParse(employeeId.Value.ToString(), out bulk))

                s.employeeId = 0;
            else
                s.employeeId = bulk;

            return s;
        }
        public String GetEmployeeName()
        {
           
            int bulk;
            if (employeeId.Value == null || !int.TryParse(employeeId.Value.ToString(), out bulk))

                return "";
            else
                return employeeId.SelectedItem.Text.ToString();


        }

        public void Select(object id)
        {
            RecordRequest req = new RecordRequest();
            req.RecordID = id.ToString();

            RecordResponse < Employee > response= _employeeService.Get<Employee>(req);
            List<Employee> onlyone = new List<Employee>();
            response.result.fullName = response.result.name.fullName;
            employeeId.GetStore().Add(new object[]
                           {
                                new
                                {
                                    recordId = response.result.recordId,
                                    fullName =response.result.name.fullName
                                }
                           });


           
        }

        public void SetLabel(string newLabel)
        {
            employeeId.FieldLabel = newLabel;
        }

        public ComboBox GetComboBox()
        {
            return employeeId;
        }
    }
}