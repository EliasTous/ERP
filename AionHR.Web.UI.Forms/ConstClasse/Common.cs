using AionHR.Model.System;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Services.Messaging.System;
using Ext.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using AionHR.Web.UI.Forms.App_GlobalResources;

namespace AionHR.Web.UI.Forms
{
    public static class Common
    {
        public static void errorMessage(ResponseBase resp )
        {
            //System.Resources.ResourceManager MyResourceClass = new System.Resources.ResourceManager(typeof(Resources.Errors /* Reference to your resources class -- may be named differently in your case */));

            //ResourceSet resourceSet = Resources.Errors.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
           
           
            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
            if (resp != null)
            {
                if (string.IsNullOrEmpty(resp.Error))
                    X.Msg.Alert(Resources.Common.Error, Resources.Errors.Error_1).Show();
                else
                {
                    if (string.IsNullOrEmpty(resp.LogId)|| resp.LogId=="0")
                        X.Msg.Alert(Resources.Common.Error, resp.Error ).Show();
                    else
                    X.Msg.Alert(Resources.Common.Error, resp.Error + "<br>" + Resources.Errors.ErrorLogId + resp.LogId).Show();
                }
            }
            else
            {
                X.Msg.Alert(Resources.Common.Error, Resources.Errors.Error_1).Show();
            }
              
        }
        public static void ReportErrorMessage(ResponseBase resp,string Error1,string LogId)
        {
            if (resp == null || string.IsNullOrEmpty(resp.Error))
            {
                throw new Exception(Error1);
            }
            if (!resp.Success)
            {
                throw new Exception(resp.Error + "<br>" + LogId + resp.LogId + "</br>");

            }



        }
        public static List<XMLDictionary> XMLDictionaryList(ISystemService systemService,string database)
        {
            XMLDictionaryListRequest request = new XMLDictionaryListRequest();

            request.database = string.IsNullOrEmpty( database)?"0": database;
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