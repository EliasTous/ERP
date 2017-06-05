using AionHR.Model.Access_Control;
using AionHR.Model.System;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using Ext.Net;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AionHR.Web.UI.Forms
{
    public class AccessControlApplier
    {
        static ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        static IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();

        
        public static void ApplyAccessControlOnForm(string localFilePath, string classId, string moduleId, FormPanel form, GridPanel g)
        {
            RecordRequest userReq = new RecordRequest();
            userReq.RecordID = _systemService.SessionHelper.GetCurrentUserId();
            RecordResponse<UserInfo> userResp = _systemService.ChildGetRecord<UserInfo>(userReq);
            if (userResp.result != null && userResp.result.isAdmin)
                return;
            UserPropertiesPermissions req = new UserPropertiesPermissions();
            req.ClassId = classId;
            req.UserId = _systemService.SessionHelper.GetCurrentUserId();
            ListResponse<UC> resp = _accessControlService.ChildGetAll<UC>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return;
            }
            bool classLevel = false;
            int level = 0;
            if (resp.Items.Count == 0)
            {
                classLevel = true;
                ClassPermissionRecordRequest classReq = new ClassPermissionRecordRequest();
                classReq.ClassId = classId;
                RecordResponse<ModuleClass> modClass = _accessControlService.ChildGetRecord<ModuleClass>(classReq);
                level = (modClass.result == null) ? 0 : modClass.result.accessLevel;

            }

           
            string s = File.ReadAllText(localFilePath);
            List<Module> preDefined = JsonConvert.DeserializeObject<List<Module>>(s);
            List<ModuleClassDefinition> classes = preDefined.Where(x => x.id == moduleId).ToList()[0].classes;
            List<ClassPropertyDefinition> properites = classes.Where(x => x.id == classId).ToList()[0].properties;
            properites.ForEach(x => { var results = resp.Items.Where(d => d.propertyId == x.propertyId).ToList(); if (results.Count > 0) results[0].index = x.index; });
            bool alldisabled = true;

            foreach (var item in form.Items)
            {
                if (item is Field)
                {
                    
                    if (!classLevel)
                    {
                        var results = resp.Items.Where(x => x.index == (item as Field).Name || x.index == (item as Field).DataIndex).ToList();

                        if (results.Count > 0)
                        {
                            level = results[0].accessLevel;
                        }
                        else
                            continue;
                    }
                    switch (level)
                    {
                        case 0:

                            (item as Field).Hidden = true; break;
                        case 1:
                            (item as Field).ReadOnly = true; break;
                        case 2:
                            alldisabled = false; break;
                        default:
                            break;

                    }
                }
            }
            foreach (var item in g.ColumnModel.Columns)
            {

                var results = resp.Items.Where(x => x.index == item.DataIndex).ToList();
                if (results.Count > 0 && results[0].accessLevel < 1)
                    item.Renderer.Handler = "return '*****';";

            }

            if (alldisabled)
            {
                g.ColumnModel.Columns[g.ColumnModel.Columns.Count - 1].Renderer.Handler = "return '';";
            }
        }

        public static void ApplyAccessControlOnGrid(string localFilePath, string classId, string moduleId, FormPanel form, GridPanel g)
        {
            RecordRequest userReq = new RecordRequest();
            userReq.RecordID = _systemService.SessionHelper.GetCurrentUserId();
            RecordResponse<UserInfo> userResp = _systemService.ChildGetRecord<UserInfo>(userReq);
            if (userResp.result != null && userResp.result.isAdmin)
                return;
            UserPropertiesPermissions req = new UserPropertiesPermissions();
            req.ClassId = classId;
            req.UserId = _systemService.SessionHelper.GetCurrentUserId();
            ListResponse<UC> resp = _accessControlService.ChildGetAll<UC>(req);
            if (!resp.Success)
            {
                X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
                X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
                return;
            }
            bool classLevel = false;
            int level = 0;
            if (resp.Items.Count == 0)
            {
                classLevel = true;
                ClassPermissionRecordRequest classReq = new ClassPermissionRecordRequest();
                classReq.ClassId = classId;
                RecordResponse<ModuleClass> modClass = _accessControlService.ChildGetRecord<ModuleClass>(classReq);
                level = (modClass.result == null) ? 0 : modClass.result.accessLevel;

            }


            string s = File.ReadAllText(localFilePath);
            List<Module> preDefined = JsonConvert.DeserializeObject<List<Module>>(s);
            List<ModuleClassDefinition> classes = preDefined.Where(x => x.id == moduleId).ToList()[0].classes;
            List<ClassPropertyDefinition> properites = classes.Where(x => x.id == classId).ToList()[0].properties;
            properites.ForEach(x => { var results = resp.Items.Where(d => d.propertyId == x.propertyId).ToList(); if (results.Count > 0) results[0].index = x.index; });
            bool alldisabled = true;

            foreach (var item in form.Items)
            {
                if (item is Field)
                {

                    if (!classLevel)
                    {
                        var results = resp.Items.Where(x => x.index == (item as Field).Name || x.index == (item as Field).DataIndex).ToList();

                        if (results.Count > 0)
                        {
                            level = results[0].accessLevel;
                        }
                        else
                            continue;
                    }
                    switch (level)
                    {
                        case 0:

                            (item as Field).Hidden = true; break;
                        case 1:
                            (item as Field).ReadOnly = true; break;
                        case 2:
                            alldisabled = false; break;
                        default:
                            break;

                    }
                }
            }
            foreach (var item in g.ColumnModel.Columns)
            {

                var results = resp.Items.Where(x => x.index == item.DataIndex).ToList();
                if (results.Count > 0 && results[0].accessLevel < 1)
                    item.Renderer.Handler = "return '*****';";

            }

            if (alldisabled)
            {
                g.ColumnModel.Columns[g.ColumnModel.Columns.Count - 1].Renderer.Handler = "return '';";
            }
        }
    }
}