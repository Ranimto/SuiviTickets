using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class TicketHistoryConfiguration : IEntityTypeConfiguration<TicketHistory>
{
    public void Configure(EntityTypeBuilder<TicketHistory> builder)
    {
        builder.ToTable("TicketHistories");

        builder.HasKey(h => h.Id);

        builder.Property(h => h.OldStatus).IsRequired();
        builder.Property(h => h.NewStatus).IsRequired();
        builder.Property(h => h.ChangedById).IsRequired();
    }
}