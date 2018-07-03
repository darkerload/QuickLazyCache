using QuickLazyCache.Infrastructure;
using System;

namespace QuickLazyCache
{
    class Program
    {
        static void Main(string[] args)
        {
            var val = SampleObject.GetItemsByCache();
            val.ForEach(c =>
            {
                Console.WriteLine(c);
            });

            var val2 = SampleObject.GetNumberByCache();
            val2.ForEach(c =>
            {
                Console.WriteLine(c.ToString());
            });


            Console.Read();
        }


    }

}
