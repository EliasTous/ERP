using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class ImportedRecord<T>
    {
       public T Record { get; set; }

        public bool IsValid { get; set; }
    }
}
