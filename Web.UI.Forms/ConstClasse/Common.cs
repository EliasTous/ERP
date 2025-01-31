﻿using Model.System;
using Services.Interfaces;
using Services.Messaging;
using Services.Messaging.System;
using Ext.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using Web.UI.Forms.App_GlobalResources;
using System.Text.RegularExpressions;
using Model.Employees.Profile;
using Microsoft.Practices.ServiceLocation;

namespace Web.UI.Forms
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
                        X.Msg.Alert(Resources.Common.Error, string.Format("{0, -4}", resp.Error)).Show();
                    else
                    X.Msg.Alert(Resources.Common.Error, resp.Error + "<br>" + Resources.Errors.ErrorLogId + resp.LogId + "<br>").Show();
                }
            }
            else
            {
             X.Msg.Alert(Resources.Common.Error, Resources.Errors.Error_1).Show();
            }
              
        }
        public static void ReportErrorMessage(ResponseBase resp,string Error1,string LogId)
        {
            if ( string.IsNullOrEmpty(resp.Error))
            {
                throw new Exception(Error1);
            }
            if (!resp.Success)
            {
                if (string.IsNullOrEmpty(resp.LogId) || resp.LogId == "0")
                    throw new Exception(Resources.Common.Error + " : "+ resp.Error);
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
         public static Dictionary<string, string> FetchReportParameters(string text)
        {
            var values = text.Split(']');
            string[] filter = new string[values.Length - 1];
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            for (int i = 0; i < values.Length - 1; i++)
            {
                filter[i] = values[i];
                filter[i] = Regex.Replace(filter[i], @"\[", "");

                string[] parametrs = filter[i].Split(':');
                for (int x = 0; x <= parametrs.Length - 1; x = +2)
                {

                    parameters.Add(parametrs[x], parametrs[x + 1]);


                }
            }
            return parameters;
       

}
        public static Dictionary<string, string> FetchParametersAsDictionary(string text)
        {
            var values = text.Split('^');
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            foreach( var item in values)
            {
              var value=  item.Split('|');
                if (value.Length == 2)
                    parameters.Add(value[0], value[1]);
                   

            }
            return parameters;


        }
        public static void ChangeKey <TKey, TValue>(this IDictionary<TKey, TValue> dic,
                                     TKey fromKey, TKey toKey)
        {
            TValue value = dic[fromKey];
            dic.Remove(fromKey);
            dic[toKey] = value;
        }

        public static List<EmployeeSnapShot> GetEmployeesFiltered(string query)
        {

            EmployeeSnapshotListRequest req = new EmployeeSnapshotListRequest();

            req.BranchId = "0";


            req.StartAt = "0";
            req.Size = "20";
            req.Filter = query;




            ListResponse<EmployeeSnapShot> response = ServiceLocator.Current.GetInstance<IEmployeeService>().ChildGetAll<EmployeeSnapShot>(req);
            if (!response.Success)
            {
                Common.errorMessage(response);
                return new List<EmployeeSnapShot>();
            }
            response.Items.ForEach(s => s.fullName = s.name.fullName);
            return response.Items;
        }

        public static bool isThreadSafe()
        {
            if (HttpRuntime.Cache.Get("isThreadSafe") != null)
            {
                if (HttpRuntime.Cache.Get("isThreadSafe").ToString() == "200")
                    return false;
            }



            return true;



        }
        public static string getThreadSafeCounter()
        {
            if (HttpRuntime.Cache.Get("isThreadSafe") != null)
                return HttpRuntime.Cache.Get("isThreadSafe").ToString();
            else
                return "0";


        }

        public static string time(int _minutes, bool _signed)
        {
            if (_minutes == 0)
                return "00:00";

            bool isNegative = _minutes < 0 ? true : false;

            _minutes = Math.Abs(_minutes);

            string hours = (_minutes / 60).ToString(), minutes = (_minutes % 60).ToString(), formattedTime;

            if (hours.Length == 1)
                hours = "0" + hours;

            if (minutes.Length == 1)
                minutes = "0" + minutes;

            formattedTime = hours + ':' + minutes;

            if (isNegative && _signed)
                formattedTime = "-" + formattedTime;

            return formattedTime;
        }

        public static void increasThreadSafeCounter()
        {
            int counter = 0;
            if (HttpRuntime.Cache.Get("isThreadSafe") != null)
            {
                counter = Convert.ToInt32(getThreadSafeCounter());
                counter++;

                HttpRuntime.Cache.Insert("isThreadSafe", counter.ToString());
            }
            else
                HttpRuntime.Cache.Insert("isThreadSafe", 1);



        }


    }
    public enum ActiveStatus { ACTIVE = 1, INACTIVE = -1 }

    public class DateTimeRange
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public bool Intersects(DateTimeRange test)
        {
            //if (this.Start > this.End || test.Start > test.End)
            //    throw new Exception();

            if (this.Start == this.End || test.Start == test.End)
                return false; // No actual date range

            if (this.Start == test.Start || this.End == test.End)
                return true; // If any set is the same time, then by default there must be some overlap. 

            if (this.Start < test.Start)
            {
                if (this.End > test.Start && this.End < test.End)
                    return true; // Condition 1

                if (this.End > test.End)
                    return true; // Condition 3
            }
            else
            {
                if (test.End > this.Start && test.End < this.End)
                    return true; // Condition 2

                if (test.End > this.End)
                    return true; // Condition 4
            }

            return false;
        }
      
    }
  


}