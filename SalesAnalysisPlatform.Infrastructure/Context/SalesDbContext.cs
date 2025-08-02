using Microsoft.EntityFrameworkCore;
using SalesAnalysisPlatform.Domain.Entities;

namespace SalesAnalysisPlatform.Infrastructure.Context
{
    public class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options) { }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>().ToTable("SALES", "RYAN_ORTIZ");

            modelBuilder.Entity<Sale>().Property(s => s.Id).HasColumnName("ID");
            modelBuilder.Entity<Sale>().Property(s => s.ProductName).HasColumnName("PRODUCTNAME");
            modelBuilder.Entity<Sale>().Property(s => s.Price).HasColumnName("PRICE");
            modelBuilder.Entity<Sale>().Property(s => s.Quantity).HasColumnName("QUANTITY");
            modelBuilder.Entity<Sale>().Property(s => s.SaleDate).HasColumnName("SALEDATE");

            modelBuilder.Entity<Customer>().ToTable("CUSTOMERS", "RYAN_ORTIZ");
            
            modelBuilder.Entity<Customer>().Property(c => c.Id).HasColumnName("ID");
            modelBuilder.Entity<Customer>().Property(c => c.Name).HasColumnName("NAME");
            modelBuilder.Entity<Customer>().Property(c => c.Email).HasColumnName("EMAIL");
            modelBuilder.Entity<Customer>().Property(c => c.Phone).HasColumnName("PHONE");
            modelBuilder.Entity<Customer>().Property(c => c.Address).HasColumnName("ADDRESS");
        }
    }
}