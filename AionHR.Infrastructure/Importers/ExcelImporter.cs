using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Infrastructure.Importers
{
    public class ExcelImporter : ImporterBase, IImporter
    {
        public ExcelImporter():base()
        {

        }

        public ExcelImporter(string file):base(file)
        {

        }
        public DataTable GetRows()
        {
            DataTable tb = new DataTable();
            return tb;
            //var connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;\"; ", FileName);
            //Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            //Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(FileName);
            //Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            //Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;
            //int rowCount = xlRange.Rows.Count;
            //int colCount = xlRange.Columns.Count;
            //DataTable t = new DataTable();
            //t.Columns.Add("dayId");
            //t.Columns.Add("checkIn");
            //t.Columns.Add("checkOut");
            //t.Columns.Add("employeeRef");

            ////iterate over the rows and columns and print to the console as it appears in the file
            ////excel is not zero based!!
            //for (int i =2; i <= rowCount; i++)
            //{
            //    DataRow row = t.NewRow();
            //    row["dayId"] = xlRange[i, 1].Value2;
            //    row["checkIn"] = xlRange[i, 2].Value2;
            //    row["checkOut"] = xlRange[i, 3].Value2;
            //    row["employeeRef"] = xlRange[i, 4].Value2;
            //    t.Rows.Add(row);
            //}
            //GC.Collect();
            //GC.WaitForPendingFinalizers();

            ////rule of thumb for releasing com objects:
            ////  never use two dots, all COM objects must be referenced and released individually
            ////  ex: [somthing].[something].[something] is bad

            ////release com objects to fully kill excel process from running in the background
            //Marshal.ReleaseComObject(xlRange);
            //Marshal.ReleaseComObject(xlWorksheet);

            ////close and release
            //xlWorkbook.Close();
            //Marshal.ReleaseComObject(xlWorkbook);

            ////quit and release
            //xlApp.Quit();
            //Marshal.ReleaseComObject(xlApp);
            //return t;
            //OleDbConnection objConn = new OleDbConnection(connectionString);

            //// Open connection with the database.
            //objConn.Open();
            //OleDbCommand objCmdSelect = new OleDbCommand("SELECT * FROM [Sheet1$]", objConn);
            //objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,null);
            //// Create new OleDbDataAdapter that is used to build a DataSet
            //// based on the preceding SQL SELECT statement.
            //OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();
            
            //// Pass the Select command to the adapter.
            //objAdapter1.SelectCommand = objCmdSelect;

            //// Create new DataSet to hold information from the worksheet.
            //DataSet objDataset1 = new DataSet();

            //// Fill the DataSet with the information from the worksheet.
            //objAdapter1.Fill(objDataset1, "XLData");
            
        }
    }
}
