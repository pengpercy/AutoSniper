using CacheManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.Framework.Cache
{
    public static class RuntimeCache
    {
        public static ICacheManager<object> Cache;

        public static ICacheManager<object> BuildCache()
        {
            if (Cache != null) return Cache;
            Cache = CacheFactory.Build("AutoSniperCacheBucket", s => { s.WithSystemRuntimeCacheHandle("handleName"); });
            return Cache;
        }
    }
}
