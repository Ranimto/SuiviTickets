using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// CommentConfiguration
/// </summary>
public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Message)
               .IsRequired()
               .HasMaxLength(1000);

        builder.Property(c => c.CreatedById)
               .IsRequired();
    }
}