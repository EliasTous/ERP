using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Infrastructure.Importers
{
    public class CSVImporter : ImporterBase, IImporter
    {
        public CSVImporter():base()
        {

        }

        public CSVImporter(string file):base(file)
        {

        }
        public DataTable GetRows()
        {
            string referance = "";
            try
            {
                DataTable tb = null;
              
                using (var fs = File.OpenRead(FileName))
                using (var reader = new StreamReader(fs))
                {

                    while (!reader.EndOfStream)
                    {

                        var line = reader.ReadLine();

                        var values = line.Split(',');
                      
                        if (string.IsNullOrWhiteSpace(line))
                            continue;
                        referance = values[0];
                        if (tb == null)
                        {
                            tb = new DataTable();
                            for (int i = 0; i < values.Length; i++)
                            {
                                tb.Columns.Add();
                            }
                        }
                        DataRow r = tb.NewRow();
                        for (int i = 0; i < values.Length; i++)
                        {
                            r[i] = values[i];
                        }
                        tb.Rows.Add(r);
                    }
                }


                return tb;
            }
            catch(Exception e)
            {
                e.Source = referance;
                throw;
            }
        }
    }
}
