using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace ParaTakip.Core
{
    public class AppServiceProvider
    {
        private static AppServiceProvider _instance = new AppServiceProvider();
        private readonly ConcurrentDictionary<RuntimeTypeHandle, EntityMeta> registeredServices;

        public static AppServiceProvider Instance { get => _instance; }

        private AppServiceProvider()
        {
            registeredServices = new ConcurrentDictionary<RuntimeTypeHandle, EntityMeta>();
        }


        public void Register(Type type, object instance)
        {
            registeredServices.TryAdd(type.TypeHandle, new EntityMeta(instance.GetType(), null));
        }

        public void RegisterAsSingleton(Type type, object singletonInstance)
        {
            if (!registeredServices.ContainsKey(type.TypeHandle))
            {
                registeredServices.TryAdd(type.TypeHandle, new EntityMeta(singletonInstance.GetType(), singletonInstance));
            }
        }

        public T Get<T>()
        {

            if (!registeredServices.TryGetValue(typeof(T).TypeHandle, out var instanceMeta))
                throw new Exception(string.Format("{0} not found in dictionary!", typeof(T).ToString()));

            return instanceMeta.CreateInstance<T>();
        }

        internal class EntityMeta
        {
            private readonly Func<object> constructor = null;
            private readonly object singletonInstance = null;
            private readonly Type instanceType = null;
            private readonly bool isTransactional = false;

            public EntityMeta(Type instanceType, object singletonInstance)
            {
                this.instanceType = instanceType;
                this.singletonInstance = singletonInstance;
                if (singletonInstance == null || this.isTransactional)
                    this.constructor = Expression.Lambda<Func<object>>(Expression.New(instanceType)).Compile();
            }

            internal T CreateInstance<T>()
            {
                if (singletonInstance == null)
                {
                    var instance = constructor();
                    return (T)instance;
                }
                else
                {
                    return (T)singletonInstance;
                }
            }
        }
    }
}
