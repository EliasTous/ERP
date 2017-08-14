using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Xml.Linq;
using System.IO;

namespace AionWebService
{
    public class Cache
    {
        public static string valueForKey(string _key)
        {
            ObjectCache cache = MemoryCache.Default;
            return (string)cache[_key];
        }

        public static void set(string _key, string _value)
        {
            ObjectCache cache = MemoryCache.Default;
            CacheItem cacheItem = new CacheItem(_key);
            cacheItem.Value = _value;
            int cacheExpiryInSeconds = 365 * 24 * 60 * 60; // 1 year
            CacheItemPolicy cachePolicy = new CacheItemPolicy()
            {
                AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddSeconds(cacheExpiryInSeconds))
            };
            cache.Add(cacheItem, cachePolicy);
        }

        public static string templateBody(int _templateId, int _languageId)
        {
            string languageRef = Cache.languageRef(_languageId);

            string templateId = _templateId.ToString();
            string keyName = String.Format("T-{0}{1}", languageRef, templateId);
            string templateBody = valueForKey(keyName);


            if (templateBody != null)
                return templateBody;

            string filePath = String.Format("{0}{1}\\{2}.txt", FilePath.templatesFolder, languageRef, templateId);

            try
            {
                using (var streamReader = new StreamReader(filePath))
                {
                    templateBody = streamReader.ReadToEnd();
                    set(keyName, templateBody);
                }
            }
            catch
            {
                throw new AionException(DataError.LOAD_TEMPLATE_ERROR, filePath, "");
            }
            return templateBody;
        }

        public static string valueForKey(string _descendants, int _Id, string _default)
        {
            string key = _descendants + _Id.ToString();
            string value = valueForKey(key);

            if (value != null)
                return value;
            try
            {
                XDocument doc = XDocument.Load(FilePath.xmlCache);
                Dictionary<string, string> dict = doc.Descendants(_descendants).ToDictionary(x => x.Attribute("id").Value, x => x.Attribute("name").Value);
                value = dict[_Id.ToString()];
                set(key, value);
            }
            catch
            {
                value = _default;
            }
            return value;
        }
        public static string languageRef(int _Id)
        {
            return valueForKey("Lang", _Id, "en");
        }
        public static string errorName(int _Id)
        {
            return valueForKey("Error", _Id, "unknown error");
        }
    }
}