using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configuration EF Core pour Ticket.
    /// </summary>
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(t => t.Description)
                   .IsRequired();

            builder.Property(t => t.Priority)
                   .IsRequired();

            builder.Property(t => t.Status)
                   .IsRequired();

            builder.Property(t => t.CreatedAt)
                   .IsRequired();

            builder.Property(t => t.CreatedById)
                   .IsRequired();

            builder.HasOne(t => t.Category)
                   .WithMany(c => c.Tickets)
                   .HasForeignKey(t => t.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(t => t.Comments)
                   .WithOne(c => c.Ticket)
                   .HasForeignKey(c => c.TicketId);

            builder.HasMany(t => t.Attachments)
                   .WithOne(a => a.Ticket)
                   .HasForeignKey(a => a.TicketId);

            builder.HasMany(t => t.Histories)
                   .WithOne(h => h.Ticket)
                   .HasForeignKey(h => h.TicketId);
        }
    }
}