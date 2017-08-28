using AionHR.Infrastructure.Importers;
using AionHR.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Interfaces
{
    public class ImportingServiceBase<T> where T : new()
    {

        protected IImporter importer;
        public string FileName { get; set; }

        public ImportingServiceBase(IImporter imp)
        {
            importer = imp;
        }

        //public List<ImportedRecord<T>> ImportWithValidation(string fileName)
        //{
        //    DataTable t = new DataTable();
        //    List<ImportedRecord<T>> items = new List<ImportedRecord<T>>();
        //    for (int i = 0; i < t.Rows.Count; i++)
        //    {
        //        List<T> item = GetItem(t.Rows[i]);
        //        item.ForEach(x=>items.Add(new ImportedRecord<T>() { Record = x, IsValid = ValidateRecord(x) }));
        //    }

        //    return items;
        //}
        public DataTable ImportWithValidation(string fileName)
        {
            DataTable t = importer.GetRows();
            t.Columns[0].ColumnName = "employeeRef";
            t.Columns[1].ColumnName = "dayId";
            t.Columns[2].ColumnName = "checkIn";
            t.Columns[3].ColumnName = "checkOut";
            t.Rows[0].SetColumnError(t.Columns[0], "errrrror");

            return t;
        }
        public List<T> ImportUnvalidated(string fileName)
        {
            DataTable t = importer.GetRows();
            List<T> items = new List<T>();
            for (int i = 0; i < t.Rows.Count; i++)
            {

                items.AddRange(GetItem(t.Rows[i]));
            }

            return items;
        }

        virtual public bool ValidateRecord(T record)
        {
            return true;
        }

        virtual protected List<T> GetItem(DataRow row)
        {
            return new List<T>();
        }
    }
}
