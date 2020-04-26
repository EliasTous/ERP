using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Importers
{
    public abstract class ImporterBase 
    {
        public string FileName
        {
            get; set;
        }
        public ImporterBase()
        {

        }

        public ImporterBase(string fileName)
        {
            this.FileName = fileName;
            
        }


        private DataTable table;

       
    }
}
