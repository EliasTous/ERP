﻿using AionHR.Services.Messaging;
using Ext.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;

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
                    X.Msg.Alert(Resources.Common.Error, resp.Error + "<br>" + Resources.Errors.ErrorLogId + resp.LogId).Show();
            }
            else
            {
                X.Msg.Alert(Resources.Common.Error, Resources.Errors.Error_1).Show();
            }
              
        }
    }
}