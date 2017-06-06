using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Xml.Linq;
using System.IO;

namespace AionHR.Infrastructure.Domain
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

        
    }
}