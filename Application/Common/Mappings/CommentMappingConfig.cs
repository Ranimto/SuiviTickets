using Application.DTOs;
using Domain.Entities;
using Mapster;

namespace Application.Common.Mappings
{
    /// <summary>
    /// Configuration Mapster pour l'entité Comment.
    /// </summary>
    public class CommentMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // Entity -> DTO
            config.NewConfig<Comment, CommentDto>();

            // DTO -> Entity
            config.NewConfig<CommentDto, Comment>()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.CreatedById);
        }
    }
}