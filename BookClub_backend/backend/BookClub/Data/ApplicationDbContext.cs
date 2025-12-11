// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using BookClub.Models.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BookClub.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Discussion> Discussions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Discussion>(entity =>
            {
                entity.ToTable("Discussions");
                entity.HasKey(d => d.Id);

                entity.Property(d => d.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(d => d.Description)
                    .IsRequired();

                entity.Property(d => d.CreatedAt)
                    .HasDefaultValueSql("SYSUTCDATETIME()");
            });
        }
    }
}
