using Lab13_Chavez.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab13_Chavez.Infrastructure.Persistence.Context;

public partial class Lab13ChavezDbContext : DbContext
{
    public Lab13ChavezDbContext()
    {
    }

    public Lab13ChavezDbContext(DbContextOptions<Lab13ChavezDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DataV23> DataV23s { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql(
            "server=127.0.0.1;port=3306;database=\"csv_db 9\";uid=root",
            ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<DataV23>(entity =>
        {
            entity.HasNoKey()
                .ToTable("data_v2_3");

            entity.Property(e => e.Col1)
                .HasMaxLength(42)
                .HasColumnName("COL 1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}