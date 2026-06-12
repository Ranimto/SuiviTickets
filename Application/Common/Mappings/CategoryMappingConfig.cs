using Application.DTOs;
using Domain.Entities;
using Mapster;

namespace Application.Common.Mappings
{
    /// <summary>
    /// Configuration Mapster pour l'entité Ticket.
    /// </summary>
    public class CategoryMappingConfig : IRegister

    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Category, CategoryDto>();

            config.NewConfig<CategoryDto, Category>()
                 .Map(dest => dest.Id, src => src.Id)
                 .Map(dest => dest.Name, src => src.Name)
                 .Map(dest => dest.Description, src => src.Description)
                 .Ignore(dest => dest.CreatedAt)
                 .Ignore(dest => dest.CreatedById);

        }
    }
}