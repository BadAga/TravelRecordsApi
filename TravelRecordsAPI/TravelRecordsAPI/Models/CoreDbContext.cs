using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TravelRecordsAPI.Models
{
    public partial class CoreDbContext : DbContext
    {
        public CoreDbContext()
        {
        }

        public CoreDbContext(DbContextOptions<CoreDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Trip> Trips { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Stage> Stages { get; set; } = null!;
        public virtual DbSet<Attraction> Attractions { get; set; } = null!;
        public virtual DbSet<HasAttraction> HasAttractions { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:travel-records.database.windows.net,1433;Initial Catalog=travel-records;Persist Security Info=False;User ID=travelrecordsadm;Password=Tr4velR3c0rds;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.Property(e => e.TripId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.PostId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Stage>(entity =>
            {
                entity.Property(e => e.StageId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Attraction>(entity =>
            {
                entity.Property(e => e.AttractionId).ValueGeneratedNever();
            });

            modelBuilder.Entity<HasAttraction>(entity =>
            {
                entity.HasKey(e => new { e.AttractionId, e.StageId })
                    .HasName("PK_has_attraction");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
