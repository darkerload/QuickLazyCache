using System;
using System.Collections.Generic;
using System.Text;

namespace QuickLazyCache.Infrastructure
{
    public interface IMemoryBaseCache
    {
        void Add<T>(string key, T value, TimeSpan? cachingInterval = null) where T : class;
        void Remove(string key);
        T Get<T>(string key) where T : class;
        List<string> GetAllKeys();
    }
}
