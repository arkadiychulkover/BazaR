using Microsoft.EntityFrameworkCore;
using BazaR.Models;

namespace BazaR.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ItemColor> ItemColors { get; set; }
        public DbSet<City> Cities { get; set; }
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

            // ComplectItem many-to-many
            modelBuilder.Entity<ComplectItem>()
                .HasKey(ci => new { ci.ComplectId, ci.ItemId });

            modelBuilder.Entity<ComplectItem>()
                .HasOne(ci => ci.Complect)
                .WithMany(c => c.Items)
                .HasForeignKey(ci => ci.ComplectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ComplectItem>()
                .HasOne(ci => ci.Item)
                .WithMany(i => i.ComplectItems)
                .HasForeignKey(ci => ci.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            // CategoryBrand many-to-many
            modelBuilder.Entity<CategoryBrand>()
                .HasKey(cb => new { cb.CategoryId, cb.BrandId });

            modelBuilder.Entity<CategoryBrand>()
                .HasOne(cb => cb.Category)
                .WithMany(c => c.CategoryBrands)
                .HasForeignKey(cb => cb.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CategoryBrand>()
                .HasOne(cb => cb.Brand)
                .WithMany(b => b.CategoryBrands)
                .HasForeignKey(cb => cb.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            // Item -> Brand
            modelBuilder.Entity<Item>()
                .HasOne(i => i.Brand)
                .WithMany()
                .HasForeignKey(i => i.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            // Item -> Category
            modelBuilder.Entity<Item>()
                .HasOne(i => i.Category)
                .WithMany()
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Item -> User (seller)
            modelBuilder.Entity<Item>()
                .HasOne(i => i.User)
                .WithMany(u => u.SellingItems)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Review
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

            // Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Item)
                .WithMany(i => i.OrderItems)
                .HasForeignKey(oi => oi.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            // CartItem
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(ci => ci.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Item)
                .WithMany(i => i.CartItems)
                .HasForeignKey(ci => ci.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            // WishlistItem
            modelBuilder.Entity<WishlistItem>()
                .HasOne(w => w.User)
                .WithMany(u => u.WishlistItems)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WishlistItem>()
                .HasOne(w => w.Item)
                .WithMany(i => i.WishlistItems)
                .HasForeignKey(w => w.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            // ItemColor
            modelBuilder.Entity<ItemColor>()
                .HasOne(ic => ic.Item)
                .WithMany(i => i.Colors)
                .HasForeignKey(ic => ic.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            // Delivery
            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.Item)
                .WithMany(i => i.DeliveryVariants)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            // Usluga
            modelBuilder.Entity<Usluga>()
                .HasOne(u => u.Item)
                .WithMany(i => i.Uslugi)
                .HasForeignKey(u => u.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            // ItemCharacteristic
            modelBuilder.Entity<ItemCharacteristic>()
                .HasOne(ic => ic.Item)
                .WithMany(i => i.Characteristics)
                .HasForeignKey(ic => ic.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            // CategoryFilter
            modelBuilder.Entity<CategoryFilter>()
                .HasOne(cf => cf.Category)
                .WithMany(c => c.Filters)
                .HasForeignKey(cf => cf.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}