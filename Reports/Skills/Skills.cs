using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using AionHR.Model.Reports;

namespace Reports.Skills
{
    public partial class Skills : DevExpress.XtraReports.UI.XtraReport
    {
        public Skills(List<RT114> details)
        {
            InitializeComponent();

            details.ForEach(u =>
            {
                var row = skillsDataSet1.Skills.NewSkillsRow();

                row.Reference = u.employeeName.reference;
                row.FullName = u.employeeName.fullName;
                row.DateFrom = u.dateFrom;
                row.DateTo = u.dateTo;
                row.Grade = u.grade;
                row.Institution = u.institution;
                row.Major = u.major;
                row.Level = u.clName;

                skillsDataSet1.Skills.AddSkillsRow(row);
            });
        }

    }
}
