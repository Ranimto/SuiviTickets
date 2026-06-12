using Application.DTOs;
using Domain.Entities;
using Mapster;

namespace Application.Common.Mappings
{
    /// <summary>
    /// Configuration Mapster pour l'entité Ticket.
    /// </summary>
    public class TicketMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // Entity -> DTO
            config.NewConfig<Ticket, TicketDto>();

            // DTO -> Entity
            config.NewConfig<TicketDto, Ticket>()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.CreatedById);
        }
    }
}