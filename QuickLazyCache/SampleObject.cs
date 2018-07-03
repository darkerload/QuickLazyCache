using QuickLazyCache.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickLazyCache
{
    public class SampleObject
    {
        public static List<string> GetItemsByCache()
        {
            return CacheManager.Initialize(typeof(SampleObject), () => GetItemsByDb());
        }

        private static List<string> GetItemsByDb()
        {
            return new List<string>()
            {
                "Item1",
                "Item2",
                "Item3"
            };
        }

        public static List<int> GetNumberByCache()
        {
            return CacheManager.Initialize(typeof(SampleObject), () => GetNumberByDb(), new TimeSpan(0, 2, 0));
        }

        private static List<int> GetNumberByDb()
        {
            return new List<int>()
            {
                1,
                3,
                5
            };
        }
    }
}
