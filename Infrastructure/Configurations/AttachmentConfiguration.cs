using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

/// <summary>
/// AttachmentConfiguration
/// </summary>
public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable("Attachments");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.FileUrl)
               .IsRequired();

        builder.Property(a => a.FileType)
               .IsRequired();
    }
}