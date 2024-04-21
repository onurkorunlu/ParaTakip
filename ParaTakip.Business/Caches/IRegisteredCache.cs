namespace ParaTakip.Business.Caches
{
    public interface IRegisteredCache
    {
        public void Add(string cacheName);
        public List<string> List();
        public void Remove(string cacheName);

    }
}
