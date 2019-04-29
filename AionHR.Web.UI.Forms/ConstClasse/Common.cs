using AionHR.Model.System;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.System;
using Ext.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using AionHR.Web.UI.Forms.App_GlobalResources;
using System.Text.RegularExpressions;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;
using System.Drawing;

namespace AionHR.Web.UI.Forms
{
    public static class Common
    {
        public static void errorMessage(ResponseBase resp )
        {
            //System.Resources.ResourceManager MyResourceClass = new System.Resources.ResourceManager(typeof(Resources.Errors /* Reference to your resources class -- may be named differently in your case */));

            //ResourceSet resourceSet = Resources.Errors.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
           
           
            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
            if (resp != null)
            {
                if (string.IsNullOrEmpty(resp.Error))
                    X.Msg.Alert(Resources.Common.Error, Resources.Errors.Error_1).Show();
                else
                {
                    if (string.IsNullOrEmpty(resp.LogId)|| resp.LogId=="0")
                        X.Msg.Alert(Resources.Common.Error, resp.Error ).Show();
                    else
                    X.Msg.Alert(Resources.Common.Error, resp.Error + "<br>" + Resources.Errors.ErrorLogId + resp.LogId + "<br>").Show();
                }
            }
            else
            {
                X.Msg.Alert(Resources.Common.Error, Resources.Errors.Error_1).Show();
            }
              
        }
        public static void ReportErrorMessage(ResponseBase resp,string Error1,string LogId)
        {
            if ( string.IsNullOrEmpty(resp.Error))
            {
                throw new Exception(Error1);
            }
            if (!resp.Success)
            {
                throw new Exception(resp.Error + "<br>" + LogId + resp.LogId + "</br>");

            }



        }
        public static List<XMLDictionary> XMLDictionaryList(ISystemService systemService,string database)
        {
            XMLDictionaryListRequest request = new XMLDictionaryListRequest();

            request.database = string.IsNullOrEmpty( database)?"0": database;
            ListResponse<XMLDictionary> resp = systemService.ChildGetAll<XMLDictionary>(request);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return new List<XMLDictionary>();
            }
            return resp.Items;
        }
         public static Dictionary<string, string> FetchReportParameters(string text)
        {
            var values = text.Split(']');
            string[] filter = new string[values.Length - 1];
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            for (int i = 0; i < values.Length - 1; i++)
            {
                filter[i] = values[i];
                filter[i] = Regex.Replace(filter[i], @"\[", "");

                string[] parametrs = filter[i].Split(':');
                for (int x = 0; x <= parametrs.Length - 1; x = +2)
                {

                    parameters.Add(parametrs[x], parametrs[x + 1]);


                }
            }
            return parameters;
       

}
        public static DevExpress.XtraReports.UI.XtraReport ReportWithParameters( DevExpress.XtraReports.UI.XtraReport report, Dictionary<string, string> parameters)
        {

            if (parameters.Count == 0)
                return new XtraReport();
            PageHeaderBand header = new PageHeaderBand();
            report.Bands.Add(header);

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
            table.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
            table.WidthF = report.PageWidth - report.Margins.Left - report.Margins.Right;

            table.AdjustSize();
            table.EndInit();



            header.Controls.Add(table);


            return report;
       
        }
        
    }
}