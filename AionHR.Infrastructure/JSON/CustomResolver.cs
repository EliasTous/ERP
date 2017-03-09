using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Infrastructure.JSON
{
    /// <summary>
    /// Contract used to resolve a json data
    /// </summary>
    public class CustomResolver : DefaultContractResolver
    {
        private Dictionary<string, string> customRules;
        public CustomResolver()
        {

            customRules = new Dictionary<string, string>();
        }

        public void AddRule(string jsonAttribute, string propertyName)
        {
            customRules.Add(propertyName, jsonAttribute);
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            if (customRules.ContainsKey(propertyName))
                return customRules[propertyName];
            else return base.ResolvePropertyName(propertyName); ;
        }

    }
}