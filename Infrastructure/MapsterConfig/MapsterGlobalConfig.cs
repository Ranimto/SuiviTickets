using Infrastructure.MapsterConfig;

namespace Infrastructure.Mapping
{
    public static class MapsterGlobalConfig
    {
        public static void RegisterMappings()
        {
            UserMappingConfig.Register();
            AuthMappingConfig.Register();
        }
    }
}
