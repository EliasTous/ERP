using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;
using System.Collections.Generic;


namespace Reports
{
    public partial class PendingPunches : DevExpress.XtraReports.UI.XtraReport
    {
        public PendingPunches(Dictionary<string, string> parameters, string getLan)
        {
            InitializeComponent();
            
            if (getLan == "fr")
            {
                //tableCell1.Text = "ID";
                //tableCell2.Text = "Type";
                //tableCell3.Text = "Reference";
                //tableCell5.Text = "Nom";
                //tableCell6.Text = "Numero Serie";
                tableCell7.Text = "Temps";
                //tableCell8.Text = "UDID";
                //label1.Text = "Poinçons en attente";


            }
            else if (getLan == "ar")
            {
                //tableCell1.Text = "هوية";
                //tableCell2.Text = "نوع";
                //tableCell3.Text = "المرجع";
                //tableCell5.Text = "اسم";
                //tableCell6.Text = "تسلسل";
                tableCell7.Text = "الساعة";
                //tableCell8.Text = "UDID";
                //label1.Text = "بصمات قيد الإنتظار";
            }
            
        }

    }
}
