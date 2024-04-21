using AutoMapper;
using ParaTakip.Core;

namespace ParaTakip.Common
{
    public static class AutoMapperHelper
    {
        public static T ToMap<T>(this object obj)
        {
            Type destType = typeof(T);
            if (obj == null)
            {
                return (T)Activator.CreateInstance(destType);
            }
            Type sourceType = obj.GetType();
            return (T)AppServiceProvider.Instance.Get<IMapper>().Map(obj, sourceType, destType);
        }
    }
}
