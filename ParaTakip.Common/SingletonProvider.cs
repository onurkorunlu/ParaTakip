using System.Reflection;

namespace ParaTakip.Common
{
    public static class SingletonProvider<T>
       where T : class
    {
        static volatile T _instance;
        static object _lock = new object();

        static SingletonProvider()
        {
        }

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            ConstructorInfo constructor = null;

                            try
                            {
                                // Binding flags exclude public constructors.
                                constructor = typeof(T).GetConstructors().FirstOrDefault();
                            }
                            catch (Exception exception)
                            {
                                throw exception;
                            }

                            if (constructor == null || constructor.IsAssembly)
                                // Also exclude internal constructors.
                                throw new Exception(string.Format("A private or " +
                                      "protected constructor is missing for '{0}'.", typeof(T).Name));

                            _instance = (T)constructor.Invoke(null);
                        }
                    }

                return _instance;
            }
        }
    }
}
