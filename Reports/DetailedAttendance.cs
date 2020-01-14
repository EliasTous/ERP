using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace Reports
{
    public partial class DetailedAttendance : DevExpress.XtraReports.UI.XtraReport
    {
        public DetailedAttendance(Dictionary<string, string> parameters, string getLan)
        {
            InitializeComponent();
            printHeader(parameters);
            //this.Landscape = true;
            label1.Font = new Font(label1.Font.FontFamily, label1.Font.Size, FontStyle.Bold);
            if (getLan == "ar")
            {
                tableCell1.Text = "الفروع";
                tableCell10.Text = "مغادرة مبكرة";
                tableCell2.Text = "الموظف";
                tableCell3.Text = "التاريخ";
                tableCell4.Text = "المدة";
                tableCell5.Text = "البصمة الاولى";
                tableCell6.Text = "البصمة الأخيرة";
                tableCell7.Text = "حضور مبكر";
                tableCell8.Text = "تأخير عن الدوام";
                tableCell9.Text = "مغادرة خلال الدوام";
                xrTableCell1.Text = "عمل إضافي";
                xrTableCell3.Text = "حالة اليوم ";
                xrTableCell5.Text = "مجموع الـتأخير ";
                xrTableCell7.Text = "مجموع الوقت الاضافي ";
                xrLabel1.Text = "فروقات الدوام";
                label1.Text = "الحضور التفصيلي";

                /*
                xrLabel7.LocationF = new PointF((float)395, 5);
                xrLabel8.LocationF = new PointF((float)457, 5);
                xrLabel4.LocationF = new PointF((float)512.55, 5);
                xrLabel9.LocationF = new PointF((float)562.41, 5);
                xrLabel10.LocationF = new PointF((float)632, 5);
                xrLabel5.LocationF = new PointF((float)768, 5);
                xrLabel6.LocationF = new PointF((float)838, 5);

                xrLabel7.WidthF = (float)62;
                xrLabel8.WidthF = (float)57;
                xrLabel4.WidthF = (float)51;
                xrLabel9.WidthF = (float)69;
                xrLabel10.WidthF = (float)70;
                xrLabel5.WidthF = (float)70;
                xrLabel6.WidthF = (float)70;
                */

            }
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

            table.Font = new Font(table.Font.FontFamily, table.Font.Size, FontStyle.Bold);

            this.PageHeader.Controls.Add(table);

        }
        private void table_BeforePrint(object sender, PrintEventArgs e)
        {
            XRTable table = ((XRTable)sender);
            table.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
            table.WidthF = this.PageWidth - this.Margins.Left - this.Margins.Right;
        }

        public string timeformat(int time)
        {
            string hr, min;
            min = Convert.ToString(time % 60);
            hr = Convert.ToString(time / 60);
            if (hr.Length == 1) hr = "0" + hr;
            if (min.Length == 1) min = "0" + min;
            return hr + ":" + min;
        }

        private void xrLabel4_BeforePrint(object sender, PrintEventArgs e)
        {
            //if (GetCurrentColumnValue("xrLabel4") == null)
            //{
            //    XRLabel label = (XRLabel)sender;
            //    label.Text = "0";
            //    xrLabel4.Text = "0";
            //}
        }

        private void xrLabel5_BeforePrint(object sender, PrintEventArgs e)
        {
            //if (string.IsNullOrEmpty((sender as XRLabel).Text))
            //    return;
            //if ((sender as XRLabel).Text == "0" || (sender as XRLabel).Text == ".00")
            //    (sender as XRLabel).Text = "00:00";
            //else
            //{
            //    (sender as XRLabel).Text = TimeSpan.FromMinutes(Convert.ToDouble((sender as XRLabel).Text)).Hours.ToString().PadLeft(2, '0')
            //        + ":" + TimeSpan.FromMinutes(Convert.ToDouble((sender as XRLabel).Text)).Minutes.ToString().PadLeft(2, '0');
            //}
        }

        private void xrLabel6_BeforePrint(object sender, PrintEventArgs e)
        {
            //if (GetCurrentColumnValue("xrLabel6") == null)
            //{
            //    XRLabel label = (XRLabel)sender;
            //    label.Text = "0";
            //}
        }

        private void xrLabel4_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //if(GetCurrentColumnValue("xrLabel4") == null)
            //{
            //    XRLabel label = (XRLabel)sender;
            //    label.Text = "0";
            //    xrLabel4.Text = "0";
            //}
        }

        private void xrLabel5_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //if (GetCurrentColumnValue("xrLabel5") == null)
            //{
            //    XRLabel label = (XRLabel)sender;
            //    label.Text = "0";
            //    xrLabel5.Text = "0";
            //}
        }

        private void xrLabel6_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
        //    if (GetCurrentColumnValue("xrLabel6") == null)
        //    {
        //        XRLabel label = (XRLabel)sender;
        //        label.Text = "0";
        //        xrLabel6.Text = "0";
        //    }
        }

        //int hoursWorked, minsWorked, hoursLateness, minsLateness, hoursLeave, minsLeave, empLatenessHours, allLatenessHours;

        private void xrLabel5_SummaryReset(object sender, EventArgs e)
        {
            //hoursLateness = minsLateness = 0;
        }

        //int totalHoursWorked, totalMinsWorked, totalHoursLeave, toalMinsLeave, totalHoursLatenss, totalMinsLateness, empLatenessMins, allLatenessMins;

        private void xrLabel5_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            //if (string.IsNullOrEmpty((sender as XRLabel).Text))
            //    return;
            if ((e.Value).ToString() == "0" || (e.Value).ToString() == ".00")
                (sender as XRLabel).Text = "00:00";
            else
            {

                string hours = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Hours.ToString().PadLeft(2, '0');
                string minutes = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Minutes.ToString().PadLeft(2, '0');
                e.Text = hours + ":" + minutes;
            }
        }

        private void xrLabel5_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //char sign = ' ';
            //if (totalHoursLatenss < 0 || totalMinsLateness < 0)
            //    sign = '-';
            //totalHoursLatenss += totalMinsLateness / 60;
            //totalMinsLateness = totalMinsLateness % 60;
            //e.Result = sign + (Math.Abs(totalHoursLatenss).ToString().PadLeft(2, '0') + ":" + Math.Abs(minsLateness).ToString().PadLeft(2, '0'));
            //e.Handled = true;

            //hoursLateness = minsLateness = 0;
        }

        private void xrLabel6_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            //if (string.IsNullOrEmpty((sender as XRLabel).Text))
            //    return;
            if ((e.Value).ToString() == "0" || (e.Value).ToString() == ".00")
                (sender as XRLabel).Text = "00:00";
            else
            {

                string hours = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Hours.ToString().PadLeft(2, '0');
                string minutes = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Minutes.ToString().PadLeft(2, '0');
                e.Text = hours + ":" + minutes;
            }
        }

        private void xrLabel4_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            //if (string.IsNullOrEmpty((sender as XRLabel).Text))
            //    return;
            if ((e.Value).ToString() == "0" || (e.Value).ToString() == ".00")
                (sender as XRLabel).Text = "00:00";
            else
            {

                string hours = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Hours.ToString().PadLeft(2, '0');
                string minutes = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Minutes.ToString().PadLeft(2, '0');
                e.Text = hours + ":" + minutes;
            }
        }

        private void xrLabel7_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            //if (string.IsNullOrEmpty((sender as XRLabel).Text))
            //    return;
            if ((e.Value).ToString() == "0" || (e.Value).ToString() == ".00")
                (sender as XRLabel).Text = "00:00";
            else
            {

                string hours = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Hours.ToString().PadLeft(2, '0');
                string minutes = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Minutes.ToString().PadLeft(2, '0');
                e.Text = hours + ":" + minutes;
            }
        }

        private void xrLabel8_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            //if (string.IsNullOrEmpty((sender as XRLabel).Text))
            //    return;
            if ((e.Value).ToString() == "0" || (e.Value).ToString() == ".00")
                (sender as XRLabel).Text = "00:00";
            else
            {

                string hours = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Hours.ToString().PadLeft(2, '0');
                string minutes = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Minutes.ToString().PadLeft(2, '0');
                e.Text = hours + ":" + minutes;
            }
        }

        private void xrLabel9_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            //if (string.IsNullOrEmpty((sender as XRLabel).Text))
            //    return;
            if ((e.Value).ToString() == "0" || (e.Value).ToString() == ".00")
                (sender as XRLabel).Text = "00:00";
            else
            {

                string hours = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Hours.ToString().PadLeft(2, '0');
                string minutes = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Minutes.ToString().PadLeft(2, '0');
                e.Text = hours + ":" + minutes;
            }
        }

        private void xrLabel10_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            if (e.Value == null)
                return;
            if ((e.Value).ToString() == "0" || (e.Value).ToString() == ".00")
                (sender as XRLabel).Text = "00:00";
            else
            {

                string hours = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Hours.ToString().PadLeft(2, '0');
                string minutes = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Minutes.ToString().PadLeft(2, '0');
                e.Text = hours + ":" + minutes;
            }
        }
    }
}
