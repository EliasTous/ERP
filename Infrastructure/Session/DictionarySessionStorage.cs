﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Session
{
   public class DictionarySessionStorage : ISessionStorage
    {

        private Dictionary<string, object> dict;
        public void Clear()
        {
            dict.Clear();
        }

        public object Retrieve(string key)
        {
            if (!dict.ContainsKey(key))
                dict.Add(key, 1);
            return dict[key];
        }

        public void Save(string key, object value)
        {
            if (dict.ContainsKey(key))
                dict[key] = value;
            else
                dict.Add(key, value);
        }

        public DictionarySessionStorage()
        {
            dict = new Dictionary<string, object>();
        }
    }
}
