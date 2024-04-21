namespace ParaTakip.Core
{
    public class ConfigurationCache
    {
        private Dictionary<object, object> ConfigurationDictionary;
        private static readonly object lockObj = new object();
        private static ConfigurationCache instance = null;

        private ConfigurationCache()
        {
            ConfigurationDictionary = new Dictionary<object, object>();
        }

        public static ConfigurationCache Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new ConfigurationCache();
                    }
                    return instance;
                }
            }
        }

        public void Add(object key, object value)
        {
            this.ConfigurationDictionary.Add(key, value);
        }

        public bool TryAdd(object key, object value)
        {
            return this.ConfigurationDictionary.TryAdd(key, value);
        }

        public bool TryGet<T>(object key, out T value)
        {
            if (ConfigurationDictionary.TryGetValue(key, out object _value))
            {
                value = (T)_value;
                return true;
            }

            value = default;
            return false;
        }
    }

}
