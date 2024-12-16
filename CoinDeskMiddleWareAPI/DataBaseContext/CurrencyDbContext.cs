using CurrencyDBContext.Models;
using Microsoft.EntityFrameworkCore;



namespace CurrencyDBContext
{
    public class CurrencyDbContext : DbContext
    {
      
            public CurrencyDbContext(DbContextOptions<CurrencyDbContext> options)
                : base(options)
            {
            }

            public DbSet<Currency> Currencies { get; set; }

            public DbSet<CurrencyChgLog> CurrencyChgLogs { get; set; }
            
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currency");

                entity.HasKey(e => e.CurrencyId);

                entity.Property(e => e.CurrencyId)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CurrencyCode)
                    .HasColumnType("nvarchar(50)")
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasColumnType("nvarchar(100)")
                    .IsRequired();

                entity.Property(e => e.CreateID)
                    .HasColumnType("nvarchar(50)")
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("GETDATE()")
                    .IsRequired();

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .IsRequired(false);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("nvarchar(50)")
                    .IsRequired(false);

                entity.HasIndex(e => e.CurrencyId)
                    .HasDatabaseName("IDX_CurrencyId");

                entity.HasIndex(e => e.CurrencyCode)
                    .IsUnique()
                    .HasDatabaseName("UQ_CurrencyCode");
            });

            modelBuilder.Entity<CurrencyChgLog>(entity =>
            {
                entity.ToTable("CurrencyChgLog");

                entity.HasKey(e => e.LogID);

                entity.Property(e => e.LogID)
                    .HasDefaultValueSql("NEWID()");

                entity.Property(e => e.OldData)
                    .HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                entity.Property(e => e.NewData)
                    .HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                entity.Property(e => e.Operation)
                    .HasColumnType("nvarchar(50)")
                    .IsRequired();

                entity.Property(e => e.ModifyUser)
                    .HasColumnType("nvarchar(50)")
                    .IsRequired();

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("GETDATE()")
                    .IsRequired();
            });
        }
    }
}
