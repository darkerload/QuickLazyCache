using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace QuickLazyCache.Infrastructure
{
    public class MemoryBaseCache : IMemoryBaseCache
    {
        private readonly MemoryCache _memoryCache;
        private readonly TimeSpan _defaultCachingInterval;
        int defaultCacheDuration = 60;
        private static readonly Lazy<IMemoryBaseCache> _instance = new Lazy<IMemoryBaseCache>(() => new MemoryBaseCache(), true);

        public MemoryBaseCache()
        {
            _memoryCache = MemoryCache.Default;
            _defaultCachingInterval = TimeSpan.FromSeconds(defaultCacheDuration);
        }

        public void Add<T>(string key, T value, TimeSpan? cachingInterval = null) where T : class
        {
            cachingInterval = cachingInterval ?? _defaultCachingInterval;
            if (value != null)
                _memoryCache.Add(key, value, DateTimeOffset.Now.Add(cachingInterval.Value));
        }

        public List<string> GetAllKeys()
        {
            return MemoryCache.Default.Select(q => q.Key).ToList();
        }

        public void Remove(string key)
        {
            if (!string.IsNullOrEmpty(key))
                _memoryCache.Remove(key);
        }

        public T Get<T>(string key) where T : class
        {
            try
            {
                return (T)_memoryCache[key];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IMemoryBaseCache Instance
        {
            get { return _instance.Value; }
        }



    }
}
