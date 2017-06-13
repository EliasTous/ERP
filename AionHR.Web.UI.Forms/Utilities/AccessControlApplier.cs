using AionHR.Model.Access_Control;
using AionHR.Model.Attributes;
using AionHR.Model.System;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using AionHR.Web.UI.Forms.Utilities;
using Ext.Net;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace AionHR.Web.UI.Forms
{
    public class AccessControlApplier
    {
        static ISystemService _systemService = ServiceLocator.Current.GetInstance<ISystemService>();
        static IMasterService _masterService = ServiceLocator.Current.GetInstance<IMasterService>();
        static IAccessControlService _accessControlService = ServiceLocator.Current.GetInstance<IAccessControlService>();


        public static void ApplyAccessControlOnPage(Type type, AbstractPanel form, GridPanel g, Button addButton, Button saveButton)
        {
            #region old
            //RecordRequest userReq = new RecordRequest();
            //userReq.RecordID = _systemService.SessionHelper.GetCurrentUserId();
            //RecordResponse<UserInfo> userResp = _systemService.ChildGetRecord<UserInfo>(userReq);
            //if (userResp.result != null && userResp.result.isAdmin)
            //    return;
            //UserPropertiesPermissions req = new UserPropertiesPermissions();
            //req.ClassId = classId;
            //req.UserId = _systemService.SessionHelper.GetCurrentUserId();
            //ListResponse<UC> resp = _accessControlService.ChildGetAll<UC>(req);
            //if (!resp.Success)
            //{
            //    X.MessageBox.ButtonText.Ok = Resources.Common.Ok;
            //    X.Msg.Alert(Resources.Common.Error, resp.Summary).Show();
            //    return;
            //}
            //bool classLevel = false;
            //int level = 0;
            //if (resp.Items.Count == 0)
            //{
            //    classLevel = true;
            //    ClassPermissionRecordRequest classReq = new ClassPermissionRecordRequest();
            //    classReq.ClassId = classId;
            //    classReq.UserId = _systemService.SessionHelper.GetCurrentUserId();
            //    RecordResponse<ModuleClass> modClass = _accessControlService.ChildGetRecord<ModuleClass>(classReq);
            //    level = (modClass.result == null) ? 0 : modClass.result.accessLevel;

            //}




            //List<ClassPropertyDefinition> properites = new List<ClassPropertyDefinition>();
            //properites.ForEach(x => { var results = resp.Items.Where(d => d.propertyId == x.propertyId).ToList(); if (results.Count > 0) results[0].index = x.index; });
            //bool alldisabled = true;
            //Type t = _masterService.ClassLookup[classId];

            //t.GetProperties().ToList<PropertyInfo>().ForEach(x => { if (x.GetCustomAttribute<PropertyID>() != null) { var results = resp.Items.Where(d => d.propertyId == x.GetCustomAttribute<PropertyID>().ID).ToList(); if (results.Count > 0) results[0].index = x.Name; } });
            //foreach (var item in form.Items)
            //{
            //    if (item is Field)
            //    {

            //        if (!classLevel)
            //        {
            //            var results = resp.Items.Where(x => x.index == (item as Field).Name || x.index == (item as Field).DataIndex).ToList();

            //            if (results.Count > 0)
            //            {
            //                level = results[0].accessLevel;
            //            }
            //            else
            //                continue;
            //        }
            //        switch (level)
            //        {
            //            case 0:

            //                (item as Field).InputType = InputType.Password;
            //                (item as Field).ReadOnly = true; break;
            //            case 1:
            //                (item as Field).ReadOnly = true; break;
            //            case 2:
            //                alldisabled = false; break;
            //            default:
            //                break;

            //        }
            //    }
            //}
            //foreach (var item in g.ColumnModel.Columns)
            //{

            //    var results = resp.Items.Where(x => x.index == item.DataIndex).ToList();
            //    if (results.Count > 0 && results[0].accessLevel < 1)
            //        item.Renderer.Handler = "return '*****';";

            //}

            //if (alldisabled)
            //{
            //    g.ColumnModel.Columns[g.ColumnModel.Columns.Count - 1].Renderer.Handler = "return '';";
            //}
            #endregion
            if ((bool)_systemService.SessionHelper.Get("IsAdmin"))
                return;
            ClassPermissionRecordRequest classReq = new ClassPermissionRecordRequest();
            classReq.ClassId = type.GetCustomAttribute<ClassIdentifier>().ClassID;
            classReq.UserId = _systemService.SessionHelper.GetCurrentUserId();
            RecordResponse<ModuleClass> modClass = _accessControlService.ChildGetRecord<ModuleClass>(classReq);
            if (modClass == null || modClass.result == null)
            {
                throw new AccessDeniedException();
            }
            else
            {
                switch (modClass.result.accessLevel)
                {
                    case 0: throw new AccessDeniedException();
                    case 1:
                        if (g != null)
                            g.ColumnModel.Columns[g.ColumnModel.Columns.Count - 1].Renderer.Handler = g.ColumnModel.Columns[g.ColumnModel.Columns.Count - 1].Renderer.Handler.Replace("deleteRender()", "' ' ");
                        if (addButton != null)
                            addButton.Disabled = true;
                        if (saveButton != null)
                            saveButton.Disabled = true;
                        if (form != null)
                            foreach (var item in form.Items)
                            {
                                item.Disabled = true;
                            }
                        break;
                    case 2:
                        if(g!=null)
                            g.ColumnModel.Columns[g.ColumnModel.Columns.Count - 1].Renderer.Handler = g.ColumnModel.Columns[g.ColumnModel.Columns.Count - 1].Renderer.Handler.Replace("deleteRender()", "' ' ");
                        break;
                    default: break;
                }
            }
            UserPropertiesPermissions req = new UserPropertiesPermissions();
            req.ClassId = type.GetCustomAttribute<ClassIdentifier>().ClassID;
            req.UserId = _systemService.SessionHelper.GetCurrentUserId();
            ListResponse<UC> resp = _accessControlService.ChildGetAll<UC>(req);
            List<UC> properites = new List<UC>();



            type.GetProperties().ToList<PropertyInfo>().ForEach(x =>
            {
                if (x.GetCustomAttribute<PropertyID>() != null)
                {
                    properites.Add(new UC() { index = x.Name, propertyId = x.GetCustomAttribute<PropertyID>().ID ,accessLevel= modClass.result.accessLevel });
                }
            });

            resp.Items.ForEach(x =>
            {
                properites.ForEach(y =>
                {
                    if (x.propertyId == y.propertyId)
                        y.accessLevel = x.accessLevel;
                    
                });
            });
            // type.GetProperties().ToList<PropertyInfo>().ForEach(x => { if (x.GetCustomAttribute<PropertyID>() != null) { resp.Items.Where(d => d.propertyId == x.GetCustomAttribute<PropertyID>().ID).ToList().ForEach(y=>y.index = x.Name); } });
            int level = 2;
            if (form != null)
            {
                foreach (var item in form.Items)
                {
                    if (item is Field)
                    {

                        var results = properites.Where(x => x.index == (item as Field).Name || x.index == (item as Field).DataIndex).ToList();

                        if (results.Count > 0)
                        {
                            level = results[0].accessLevel;
                        }
                        else
                            continue;

                        switch (level)
                        {
                            case 0:

                                (item as Field).InputType = InputType.Password; 
                                (item as Field).ReadOnly = true; break;
                            case 1:
                                (item as Field).ReadOnly = true; break;
                            case 2:
                                break;
                            default:
                                break;

                        }
                    }
                    else if (item is RadioGroup)
                    {
                        var results = properites.Where(x => x.index == (item as RadioGroup).GroupName).ToList();

                        if (results.Count > 0)
                        {
                            level = results[0].accessLevel;
                        }

                        switch (level)
                        {
                            case 0:
                                item.LazyItems.ForEach(x => (x as Radio).Disabled = true);
                                break;
                            case 1:
                                item.LazyItems.ForEach(x => (x as Radio).ReadOnly = true);
                                break;
                            default: break;
                        }

                    }
                   
                }
            }
            if (g != null)
            {
                foreach (var item in g.ColumnModel.Columns)
                {

                    var results = properites.Where(x => x.index == item.DataIndex).ToList();
                    if (results.Count > 0 && results[0].accessLevel < 1)
                        item.Renderer.Handler = "return '*****';";

                }
            }
        }

        public static List<UC> GetPropertiesLevels(Type type)
        {
            ClassPermissionRecordRequest classReq = new ClassPermissionRecordRequest();
            string classId= type.GetCustomAttribute<ClassIdentifier>().ClassID;
            classReq.ClassId = classId;
            classReq.UserId = _systemService.SessionHelper.GetCurrentUserId();
            RecordResponse<ModuleClass> modClass = _accessControlService.ChildGetRecord<ModuleClass>(classReq);
            if (modClass == null || modClass.result == null)
            {
                throw new AccessDeniedException();
            }
            UserPropertiesPermissions req = new UserPropertiesPermissions();
            req.ClassId = classId;
            req.UserId = _systemService.SessionHelper.GetCurrentUserId();
            ListResponse<UC> resp = _accessControlService.ChildGetAll<UC>(req);
            List<UC> properites = new List<UC>();
            type.GetProperties().ToList<PropertyInfo>().ForEach(x =>
            {
                if (x.GetCustomAttribute<PropertyID>() != null)
                {
                    properites.Add(new UC() { index = x.Name, propertyId = x.GetCustomAttribute<PropertyID>().ID, accessLevel = modClass.result.accessLevel });
                }
            });

            resp.Items.ForEach(x =>
            {
                properites.ForEach(y =>
                {
                    if (x.propertyId == y.propertyId)
                        y.accessLevel = Math.Min(y.accessLevel,x.accessLevel);

                });
            });
            return properites;
        }
    }
}