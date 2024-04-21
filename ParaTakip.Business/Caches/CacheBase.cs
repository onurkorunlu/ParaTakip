using Microsoft.Extensions.Caching.Memory;
using ParaTakip.Common;
using ParaTakip.Core;

namespace ParaTakip.Business.Caches
{
    public abstract class CacheBase<T>
    {
        private string CacheName { get; set; }
        public Dictionary<string, T> Values { get; set; } = [];

        public CacheBase(string CacheName)
        {
            this.CacheName = CacheName;
            if (AppServiceProvider.Instance.Get<IMemoryCache>().TryGetValue(CacheName, out Dictionary<string, T>? values))
            {
                Values = values ?? [];
            }
            else
            {
                Values = GetPrepareCacheDictionary();
            }

            RegisterCache();
        }

        private void RegisterCache()
        {
            AppServiceProvider.Instance.Get<IMemoryCache>().Set<Dictionary<string, T>>(CacheName, Values);
            AppServiceProvider.Instance.Get<IRegisteredCache>().Add(CacheName);
        }

        protected abstract Dictionary<string, T> GetPrepareCacheDictionary();
    }
}
