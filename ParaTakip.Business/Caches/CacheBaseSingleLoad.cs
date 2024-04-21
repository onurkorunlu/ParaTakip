using Microsoft.Extensions.Caching.Memory;
using ParaTakip.Common;
using ParaTakip.Core;

namespace ParaTakip.Business.Caches
{
    public abstract class CacheBaseSingleLoad<T>
    {
        private string CacheName { get; set; }
        public Dictionary<string, T> Values { get; set; } = [];

        public CacheBaseSingleLoad(string CacheName)
        {
            this.CacheName = CacheName;

            if (AppServiceProvider.Instance.Get<IMemoryCache>().TryGetValue(CacheName, out Dictionary<string, T>? values))
                Values = values ?? [];

            RegisterCache();
        }

        private void RegisterCache()
        {
            AppServiceProvider.Instance.Get<IMemoryCache>().Set<Dictionary<string, T>>(CacheName, Values);
            AppServiceProvider.Instance.Get<IRegisteredCache>().Add(CacheName);
        }

        public abstract T GetCacheValue(string CacheName);
    }
}
