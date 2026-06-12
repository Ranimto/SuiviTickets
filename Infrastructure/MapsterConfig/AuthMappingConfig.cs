using Application.Models;
using Infrastructure.Identity;
using Mapster;

namespace Infrastructure.MapsterConfig
{
    public static class AuthMappingConfig
    {
        public static void Register()
        {
            TypeAdapterConfig<AppUser, AuthUserModel>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.UserName, src => src.UserName)
                .Ignore(dest => dest.Roles);

            TypeAdapterConfig<AuthUserModel,AppUser>.NewConfig()
              .Map(dest => dest.Id, src => src.Id)
              .Map(dest => dest.Email, src => src.Email)
              .Map(dest => dest.UserName, src => src.UserName)
              .Ignore(dest => dest.Roles);
        }
    }
}
