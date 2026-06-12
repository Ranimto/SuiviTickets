using Domain.Entities;
using Infrastructure.Identity;
using Mapster;

namespace Infrastructure.MapsterConfig
{
    public static class UserMappingConfig
    {
        public static void Register()
        {
            TypeAdapterConfig<AppUser, User>.NewConfig()
            .Map(dest => dest.Role, src => src.Roles.FirstOrDefault());

            TypeAdapterConfig<User, AppUser>.NewConfig()
     .Map(dest => dest.Id, src => src.Id.ToString()) // Guid → string
     .Map(dest => dest.UserName, src => src.UserName) // ou Email si tu veux
     .Map(dest => dest.Email, src => src.Email)
     .Map(dest => dest.Roles, src => new List<string> { src.Role.ToString() }) // ⚠️ important
     .IgnoreNullValues(true);


        }
    }
}

