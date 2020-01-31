using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace Reports
{
    public partial class DetailedAttendanceWOVariation : DevExpress.XtraReports.UI.XtraReport
    {
        public DetailedAttendanceWOVariation(Dictionary<string, string> parameters, string getLan)
        {
            InitializeComponent();
            //printHeader(parameters);
            
               
        }
        //private void printHeader(Dictionary<string, string> parameters)
        //{
        //    if (parameters.Count == 0)
        //        return;


        //    XRTable table = new XRTable();
        //    table.BeginInit();


        //    table.LocationF = new PointF(0, 0);
        //    int count = 0;
        //    XRTableRow row = new XRTableRow();

        //    foreach (KeyValuePair<string, string> item in parameters)
        //    {

        //        XRTableCell cell = new XRTableCell();

        //        cell.Text = item.Key;

        //        cell.BackColor = Color.Gray;
        //        cell.ForeColor = Color.White;

        //        XRTableCell valueCell = new XRTableCell();

        //        valueCell.Text = item.Value;

        //        row.Cells.Add(cell);
        //        row.Cells.Add(valueCell);

        //        count++;
        //        if (count % 4 == 0)
        //        {
        //            table.Rows.Add(row);
        //            row = new XRTableRow();
        //        }





        //    }
        //    if (count % 4 != 0)
        //    {
        //        for (int i = 0; i < (4 - (count % 4)) * 2; i++)
        //        {
        //            XRTableCell cell = new XRTableCell();



        //            row.Cells.Add(cell);
        //        }
        //        table.Rows.Add(row);
        //    }
        //    table.BeforePrint += new PrintEventHandler(table_BeforePrint);
        //    table.AdjustSize();
        //    table.EndInit();

        //    table.Font = new Font(table.Font.FontFamily, table.Font.Size, FontStyle.Bold);

        //   this.PageHeader.Controls.Add(table);

        //}
        //private void table_BeforePrint(object sender, PrintEventArgs e)
        //{
        //    XRTable table = ((XRTable)sender);
        //    table.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
        //    table.WidthF = this.PageWidth - this.Margins.Left - this.Margins.Right;
        //}

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
            if (e.Value == null)
                return;
            if ((e.Value).ToString() == "0" || (e.Value).ToString() == ".00")
                (sender as XRLabel).Text = "00:00";
            else
            {

                //string hours = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Hours.ToString().PadLeft(2, '0');
                //string minutes = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Minutes.ToString().PadLeft(2, '0');
                e.Text = timeformat(Convert.ToInt32(e.Value));// hours + ":" + minutes;
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
            if (e.Value == null)
                return;
            if ((e.Value).ToString() == "0" || (e.Value).ToString() == ".00")
                (sender as XRLabel).Text = "00:00";
            else
            {

                //string hours = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Hours.ToString().PadLeft(2, '0');
                //string minutes = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Minutes.ToString().PadLeft(2, '0');
                e.Text = timeformat(Convert.ToInt32(e.Value));// hours + ":" + minutes;
            }
        }

        private void xrLabel4_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            if (e.Value == null)
                return;
            if ((e.Value).ToString() == "0" || (e.Value).ToString() == ".00")
                (sender as XRLabel).Text = "00:00";
            else
            {

                //string hours = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Hours.ToString().PadLeft(2, '0');
                //string minutes = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Minutes.ToString().PadLeft(2, '0');
                e.Text = timeformat(Convert.ToInt32(e.Value));//hours + ":" + minutes;
            }
        }

        private void xrLabel7_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            if (e.Value == null)
                return;
            if ((e.Value).ToString() == "0" || (e.Value).ToString() == ".00")
                (sender as XRLabel).Text = "00:00";
            else
            {

                //string hours = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Hours.ToString().PadLeft(2, '0');
                //string minutes = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Minutes.ToString().PadLeft(2, '0');
                e.Text = timeformat(Convert.ToInt32(e.Value));//hours + ":" + minutes;
            }
        }

        private void xrLabel8_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            if (e.Value == null)
                return;
            if ((e.Value).ToString() == "0" || (e.Value).ToString() == ".00")
                (sender as XRLabel).Text = "00:00";
            else
            {

                //string hours = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Hours.ToString().PadLeft(2, '0');
                //string minutes = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Minutes.ToString().PadLeft(2, '0');
                e.Text = timeformat(Convert.ToInt32(e.Value));//hours + ":" + minutes;
            }
        }

        private void xrLabel9_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            if (e.Value == null)
                return;
            if ((e.Value).ToString() == "0" || (e.Value).ToString() == ".00")
                (sender as XRLabel).Text = "00:00";
            else
            {

                //string hours = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Hours.ToString().PadLeft(2, '0');
                //string minutes = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Minutes.ToString().PadLeft(2, '0');
                e.Text = timeformat(Convert.ToInt32(e.Value));//hours + ":" + minutes;
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

                //string hours = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Hours.ToString().PadLeft(2, '0');
                //string minutes = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Minutes.ToString().PadLeft(2, '0');
                e.Text = timeformat(Convert.ToInt32(e.Value));//hours + ":" + minutes;
            }
        }

        private void xrLabel11_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            if (e.Value == null)
                return;
            if ((e.Value).ToString() == "0" || (e.Value).ToString() == ".00")
                (sender as XRLabel).Text = "00:00";
            else
            {

                //string hours = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Hours.ToString().PadLeft(2, '0');
                //string minutes = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Minutes.ToString().PadLeft(2, '0');
                e.Text = timeformat(Convert.ToInt32(e.Value));//hours + ":" + minutes;
            }
        }

        private void xrLabel12_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            if (e.Value == null)
                return;
            if ((e.Value).ToString() == "0" || (e.Value).ToString() == ".00")
                (sender as XRLabel).Text = "00:00";
            else
            {

                //string hours = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Hours.ToString().PadLeft(2, '0');
                //string minutes = TimeSpan.FromMinutes(Convert.ToDouble((e.Value))).Minutes.ToString().PadLeft(2, '0');
                e.Text = timeformat(Convert.ToInt32(e.Value));//hours + ":" + minutes;
            }

        }


        int missedShitHours, missedShitMinutes;

        private void xrLabel12_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //missedShitHours += missedShitMinutes / 60;
            //missedShitMinutes = missedShitMinutes % 60;
            //e.Result = (missedShitHours.ToString().PadLeft(2, '0') + ":" + missedShitMinutes.ToString().PadLeft(2, '0'));            
            //e.Handled = true;

            //missedShitHours = missedShitMinutes = 0;
        }

        private void xrLabel12_SummaryRowChanged(object sender, EventArgs e)
        {
            //missedShitHours += Convert.ToInt32(GetCurrentColumnValue("strMissedShift").ToString().Substring(0, 2));
            //missedShitMinutes += Convert.ToInt32(GetCurrentColumnValue("strMissedShift").ToString().Substring(3, 2));
        }

        private void xrLabel36_BeforePrint(object sender, PrintEventArgs e)
        {
            string value = (sender as XRLabel).Text;
            int a = 0;
            if (int.TryParse(value, out a))
            {
                (sender as XRLabel).Text = time(a, false);
            }

        }

        private void xrLabel35_BeforePrint(object sender, PrintEventArgs e)
        {
            string value = (sender as XRLabel).Text;
            int a = 0;
            if (int.TryParse(value, out a))
            {
                (sender as XRLabel).Text = time(a, false);
            }
        }

        private void xrLabel34_BeforePrint(object sender, PrintEventArgs e)
        {
            string value = (sender as XRLabel).Text;
            int a = 0;
            if (int.TryParse(value, out a))
            {
                (sender as XRLabel).Text = time(a, false);
            }
        }

        private void xrLabel38_BeforePrint(object sender, PrintEventArgs e)
        {
            string value = (sender as XRLabel).Text;
            int a = 0;
            if (int.TryParse(value, out a))
            {
                (sender as XRLabel).Text = time(a, false);
            }
        }

        private void xrLabel31_BeforePrint(object sender, PrintEventArgs e)
        {

        }

        private string time(int _minutes, bool _signed)
        {
            if (_minutes == 0)
                return "00:00";

            bool isNegative = _minutes < 0 ? true : false;

            _minutes = Math.Abs(_minutes);

            string hours = (_minutes / 60).ToString(), minutes = (_minutes % 60).ToString(), formattedTime;

            if (hours.Length == 1)
                hours = "0" + hours;

            if (minutes.Length == 1)
                minutes = "0" + minutes;

            formattedTime = hours + ':' + minutes;

            if (isNegative && _signed)
                formattedTime = "-" + formattedTime;

            return formattedTime;
        }

    }
}
