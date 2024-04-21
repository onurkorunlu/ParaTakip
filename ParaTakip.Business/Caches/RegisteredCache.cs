using ParaTakip.Core;

namespace ParaTakip.Business.Caches
{
    public class RegisteredCache : IRegisteredCache
    {
        public List<string> Caches { get; set; }

        public RegisteredCache()
        {
            Caches = [];
        }

        public void Add(string cacheName)
        {
            if (!Caches.Contains(cacheName))
            {
                Caches.Add(cacheName);
            }
        }

        public void Remove(string cacheName)
        {
            if (Caches.Contains(cacheName))
            {
                Caches.Remove(cacheName);
            }
        }

        public List<string> List()
        {
            return Caches;
        }
    }
}
