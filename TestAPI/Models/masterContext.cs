using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TestAPI.Models
{
    public partial class masterContext : DbContext
    {
        public virtual DbSet<Contracts> Contracts { get; set; }
        public virtual DbSet<EventLog> EventLog { get; set; }

        public masterContext(DbContextOptions<masterContext> options)
          : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contracts>(entity =>
            {
                entity.HasKey(e => e.ContractId);

                entity.Property(e => e.BusinessNumber)
                    .HasColumnName("businessNumber")
                    .HasMaxLength(20);

                entity.Property(e => e.ContractActivationDate).HasColumnType("datetime");

                entity.Property(e => e.ContractType).HasMaxLength(20);

                entity.Property(e => e.DealerName)
                    .IsRequired()
                    .HasColumnName("dealerName");

                entity.Property(e => e.LoanAmount).HasColumnType("numeric(19, 5)");

                entity.Property(e => e.RowCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.RowCreatedUser).HasMaxLength(20);

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.Property(e => e.VersionDate).HasColumnType("datetime");

                entity.Property(e => e.VersionUser).HasMaxLength(20);
            });

            modelBuilder.Entity<EventLog>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.RowCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.RowCreatedUser).HasMaxLength(20);

                entity.Property(e => e.VersionDate).HasColumnType("datetime");

                entity.Property(e => e.VersionUser).HasMaxLength(20);
            });
        }
    }
}
