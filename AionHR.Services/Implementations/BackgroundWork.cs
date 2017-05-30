using AionHR.Infrastructure.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
   public class BackgroundWork<T>

    {
        public List<T> Items { get; set; }

        public ISessionStorage SessionStorage { get; set; }
    }
}
