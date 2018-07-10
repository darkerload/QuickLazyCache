## QuickLazyCache

### Description
This is package purpose of quickly caching and manage.

### Install

You can use nuget package for download however have to install below package!

```
Install-Package System.Runtime.Caching -Version 4.5.0
 ```
 Then easily install package via nuget
 ```
Install-Package QuickLazyCache.Infrastructure -Version 1.0.0
 ```


### How it's working
-----
When cache manager calling via method of Initialize it's taking 2 required parameters from who is caller then creating auto cache-key for add to "MemoryBaseCache"

* [For more information about MemoryBaseCache](https://github.com/darkerload/LazyMemoryBaseCache)


### Usage

```csharp
  public class SampleObject
    {
        public static List<string> GetItemsByCache()
        {
            //Timespan is optional parameter. It's gonna keep up to default time if you are not pass this parameter.
            return CacheManager.Initialize(typeof(SampleObject), () => GetItemsByDb(), new TimeSpan(0, 2, 0));
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
    }
 ```
