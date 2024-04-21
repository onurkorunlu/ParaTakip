using System.Globalization;

namespace ParaTakip.Core
{
    public class ParameterCache
    {
        private Dictionary<string, string> ParameterDictionary;
        private static readonly object lockObj = new object();
        private static ParameterCache instance = null;

        private ParameterCache()
        {
            ParameterDictionary = new Dictionary<string, string>();
        }

        public static ParameterCache Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new ParameterCache();
                    }
                    return instance;
                }
            }
        }

        public void Add(string key, string value)
        {
            this.ParameterDictionary.Add(key, value);
        }

        public bool TryAdd(string key, string value)
        {
            return this.ParameterDictionary.TryAdd(key, value);
        }

        public bool TryGet<T>(string key, out T value)
        {
            try
            {
                value = Get<T>(key);
                return true;
            }
            catch (Exception)
            {
                value = default;
                return false;
            }
        }

        public T Get<T>(string key)
        {
            if (ParameterDictionary.TryGetValue(key, out string _value))
            {
                if (Type.GetTypeCode(typeof(T)) is TypeCode.Decimal)
                {
                    if (decimal.TryParse(_value, NumberStyles.AllowDecimalPoint, new CultureInfo("en-US"), out decimal parsedValue))
                    {
                        return (T)Convert.ChangeType(parsedValue, typeof(T));
                    }
                    else
                    {
                        return (T)Convert.ChangeType(0, typeof(T));
                    }
                }
                else if (Type.GetTypeCode(typeof(T)) is TypeCode.Int16)
                {
                    if (short.TryParse(_value, out short parsedValue))
                    {
                        return (T)Convert.ChangeType(parsedValue, typeof(T));
                    }
                    else
                    {
                        return (T)Convert.ChangeType(0, typeof(T));
                    }
                }
                else if (Type.GetTypeCode(typeof(T)) is TypeCode.Int32)
                {
                    if (int.TryParse(_value, out int parsedValue))
                    {
                        return (T)Convert.ChangeType(parsedValue, typeof(T));
                    }
                    else
                    {
                        return (T)Convert.ChangeType(0, typeof(T));
                    }
                }
                else if (Type.GetTypeCode(typeof(T)) is TypeCode.Int64)
                {
                    if (long.TryParse(_value, out long parsedValue))
                    {
                        return (T)Convert.ChangeType(parsedValue, typeof(T));
                    }
                    else
                    {
                        return (T)Convert.ChangeType(0, typeof(T));
                    }
                }
            }

            return default;
        }

        public void Reload(List<KeyValuePair<string, string>> appParameters)
        {
            this.ParameterDictionary = new Dictionary<string, string>();
            appParameters.ForEach(x => this.TryAdd(x.Key, x.Value));
        }
    }

}
