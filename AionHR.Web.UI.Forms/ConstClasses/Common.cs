using Ext.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;

namespace AionHR.Web.UI.Forms.ConstClasses
{
    public static class Common
    {
        public static void errorMessage(string errorCode,string message, string logId,string Summary)
        {
            System.Resources.ResourceManager MyResourceClass = new System.Resources.ResourceManager(typeof(Resources.Errors /* Reference to your resources class -- may be named differently in your case */));

            ResourceSet resourceSet = Resources.Errors.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
           
           
            X.MessageBox.ButtonText.Ok = Resources.Common.Ok;

           
            X.Msg.Alert(Resources.Common.Error,!string.IsNullOrEmpty( resourceSet.GetObject(errorCode).ToString()) ? resourceSet.GetObject(errorCode).ToString() + "<br>" + Resources.Errors.ErrorLogId+ logId : Summary).Show();
        }
    }
}