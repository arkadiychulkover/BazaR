using BazaR.Models;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Reflection.Emit;

namespace BazaR.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryFilter> CategoryFilters { get; set; }
        public DbSet<CategoryBrand> CategoryBrands { get; set; }
        public DbSet<Usluga> Uslugas { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Complect> Complects { get; set; }
        public DbSet<ComplectItem> ComplectItems { get; set; }
        public DbSet<ItemCharacteristic> ItemCharacteristics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ComplectItem>()
                .HasKey(ci => new { ci.ComplectId, ci.ItemId });

            modelBuilder.Entity<ComplectItem>()
                .HasOne(ci => ci.Complect)
                .WithMany(c => c.Items)
                .HasForeignKey(ci => ci.ComplectId);

            modelBuilder.Entity<ComplectItem>()
                .HasOne(ci => ci.Item)
                .WithMany(i => i.ComplectItems)
                .HasForeignKey(ci => ci.ItemId);

            modelBuilder.Entity<CategoryBrand>()
                .HasKey(cb => new { cb.CategoryId, cb.BrandId });

            modelBuilder.Entity<CategoryBrand>()
                .HasOne(cb => cb.Category)
                .WithMany(c => c.CategoryBrands)
                .HasForeignKey(cb => cb.CategoryId);

            modelBuilder.Entity<CategoryBrand>()
                .HasOne(cb => cb.Brand)
                .WithMany(b => b.CategoryBrands)
                .HasForeignKey(cb => cb.BrandId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Item)
                .WithMany(i => i.Reviews)
                .HasForeignKey(r => r.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.Brand)
                .WithMany()
                .HasForeignKey(i => i.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.Category)
                .WithMany()
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.City)
                .WithMany()
                .HasForeignKey(o => o.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Usluga>()
                .HasOne(u => u.Item)
                .WithMany(i => i.Uslugi)
                .HasForeignKey(u => u.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.Item)
                .WithMany(i => i.DeliveryVariants)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ItemCharacteristic>()
                .HasOne(ic => ic.Item)
                .WithMany(i => i.Characteristics)
                .HasForeignKey(ic => ic.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}