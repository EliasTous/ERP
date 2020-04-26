using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.System
{
   public class LetterTemplate :ModelBase

    {

        public string name { get; set; }
        public int languageId { get; set; }
        public short usage { get; set; }
        public string bodyText { get; set; }
    }
}
