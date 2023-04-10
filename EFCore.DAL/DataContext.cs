using System.Configuration;
using EFCore.Model;
using Microsoft.EntityFrameworkCore;

namespace EFCore.DAL;
public class DataContext : DbContext
{
    public DbSet<Product> Product { get; set; }
    public DbSet<OrderItem> OrderItem { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Client> Client { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["LocalDb"].ConnectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();

        modelBuilder.Entity<Client>()
            .HasIndex(c => c.Email)
            .IsUnique();
    }
}
