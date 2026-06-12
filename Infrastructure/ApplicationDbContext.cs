using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole, string>, IApplicationDBContext
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<TicketHistory> TicketHistories { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Comment> Comments { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);


            builder.Entity<RefreshToken>()
                .HasKey(x => x.Id);

            builder.Entity<RefreshToken>()
                .HasOne<AppUser>()
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(x => x.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ticket -> Category
            builder.Entity<Ticket>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); 

            // Attachment
            builder.Entity<Attachment>()
                .HasOne(a => a.Ticket)
                .WithMany(t => t.Attachments)
                .HasForeignKey(a => a.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            // Comment
            builder.Entity<Comment>()
                .HasOne(c => c.Ticket)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            // TicketHistory
            builder.Entity<TicketHistory>()
                .HasOne(h => h.Ticket)
                .WithMany(t => t.Histories)
                .HasForeignKey(h => h.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
