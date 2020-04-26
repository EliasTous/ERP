using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Web.UI.Forms.Utilities
{
    public class ModulesFileWrapper
    {
        public static string GetContent()
        {
            return Cache.valueForKey("modules");
        }

        public static void Store()
        {
            Cache.set("modules", File.ReadAllText(HttpContext.Current.Server.MapPath("~/Utilities/modules.txt")));
        }
    }
}