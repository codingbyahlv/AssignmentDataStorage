using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class CustomersOrdersDbContext(DbContextOptions<CustomersOrdersDbContext> options) : DbContext(options)
{
    public virtual DbSet<AddressEntity> Addresses { get; set; }
    public virtual DbSet<CustomerEntity> Customers { get; set; }
    public virtual DbSet<CustomerProfileEntity> CustomerProfiles { get; set; }
    public virtual DbSet<OrderEntity> Orders { get; set; }
    public virtual DbSet<OrderRowEntity> OrderRows { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerEntity>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<OrderRowEntity>()
            .HasKey(x => new { x.RowId, x.OrderId });
    }
}






//OnModelCreate!!!
//- speca Email på CustomerEntity som Unique
//- sammsánsatt nyckel (RowId, OrderId) på OrderRowEntity