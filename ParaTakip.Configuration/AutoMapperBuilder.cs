using AutoMapper;

namespace ParaTakip.Configuration
{
    public static class AutoMapperBuilder
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(typeof(AutoMapperProfile)));
            var m = new Mapper(config);
            return m;
        }
    }
}
