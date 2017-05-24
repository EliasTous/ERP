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

        public List<ImportedRecord<T>> ImportWithValidation(string fileName)
        {
            DataTable t = new DataTable();
            List<ImportedRecord<T>> items = new List<ImportedRecord<T>>();
            for (int i = 0; i < t.Rows.Count; i++)
            {
                T item = GetItem(t.Rows[i]);
                items.Add(new ImportedRecord<T>() { Record = item, IsValid = ValidateRecord(item) });
            }

            return items;
        }

        public List<T> ImportUnvalidated(string fileName)
        {
            DataTable t = importer.GetRows();
            List<T> items = new List<T>();
            for (int i = 0; i < t.Rows.Count; i++)
            {

                items.Add(GetItem(t.Rows[i]));
            }

            return items;
        }

        virtual public bool ValidateRecord(T record)
        {
            return true;
        }

        virtual public T GetItem(DataRow row)
        {
            return new T();
        }
    }
}
