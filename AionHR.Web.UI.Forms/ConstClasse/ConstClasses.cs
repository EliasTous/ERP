using Model.System;
using Services.Interfaces;
using Services.Messaging;
using Services.Messaging.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.UI.Forms.ConstClasse
{
    public static class ConstClasses
    {

        public static List<XMLDictionary> FillAcessLevel(ISystemService systemService)
        {
            XMLDictionaryListRequest request = new XMLDictionaryListRequest();

            request.database = "5";
            ListResponse<XMLDictionary> resp = systemService.ChildGetAll<XMLDictionary>(request);
            if (!resp.Success)
            {
                Common.errorMessage(resp);
                return new List<XMLDictionary>(); 
            }
            return resp.Items;
               
        }
    }
}