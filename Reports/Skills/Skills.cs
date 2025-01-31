﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using Model.Reports;
using System.Drawing.Printing;

namespace Reports.Skills
{
    public partial class Skills : DevExpress.XtraReports.UI.XtraReport
    {
        public Skills(List<RT114> details, Dictionary<string, string> parameters)
        {
            InitializeComponent();

            details.ForEach(u =>
            {
                var row = skillsDataSet1.Skills.NewSkillsRow();

                row.Reference = u.employeeRef;
                row.FullName = u.employeeName;
                row.DateFrom = u.dateFrom;
                row.DateTo = u.dateTo;
                row.Grade = u.grade;
                row.Institution = u.institution;
                row.Major = u.major;
                row.Level = u.clName;

                skillsDataSet1.Skills.AddSkillsRow(row);
            });
            printHeader(parameters);

            //
            // TODO: Add constructor logic here
            //
        }
        private void printHeader(Dictionary<string, string> parameters)
        {
            if (parameters.Count == 0)
                return;


            XRTable table = new XRTable();
            table.BeginInit();


            table.LocationF = new PointF(0, 0);
            int count = 0;
            XRTableRow row = new XRTableRow();

            foreach (KeyValuePair<string, string> item in parameters)
            {

                XRTableCell cell = new XRTableCell();

                cell.Text = item.Key;

                cell.BackColor = Color.Gray;
                cell.ForeColor = Color.White;

                XRTableCell valueCell = new XRTableCell();

                valueCell.Text = item.Value;

                row.Cells.Add(cell);
                row.Cells.Add(valueCell);

                count++;
                if (count % 4 == 0)
                {
                    table.Rows.Add(row);
                    row = new XRTableRow();
                }





            }
            if (count % 4 != 0)
            {
                for (int i = 0; i < (4 - (count % 4)) * 2; i++)
                {
                    XRTableCell cell = new XRTableCell();



                    row.Cells.Add(cell);
                }
                table.Rows.Add(row);
            }
            table.BeforePrint += new PrintEventHandler(table_BeforePrint);
            table.AdjustSize();
            table.EndInit();



            this.PageHeader.Controls.Add(table);

        }
        private void table_BeforePrint(object sender, PrintEventArgs e)
        {
            XRTable table = ((XRTable)sender);
            table.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
            table.WidthF = this.PageWidth - this.Margins.Left - this.Margins.Right;
        }

    }
}
