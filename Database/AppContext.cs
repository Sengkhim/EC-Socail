using GraphQL_APIs.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQL_APIs.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) 
    : DbContext(options)
{

    public DbSet<Booking> Bookings { get; init; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("K_Sport_DB")); 
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.ToTable("booking", "Booking");
            entity.HasKey(e => e.Id);
            entity.HasIndex(i => i.Code).IsUnique();
            entity.Property(p => p.Text).HasMaxLength(100).IsRequired();
            entity.Property(p => p.Description).HasMaxLength(50);
            entity.Property(p => p.Date).HasColumnName("BookingDate");
        });
        
        base.OnModelCreating(modelBuilder);
    }
}