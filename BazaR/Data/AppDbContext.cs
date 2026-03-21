using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BazaR.Models;

namespace BazaR.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryFilter> CategoryFilters { get; set; }
        public DbSet<CategoryBrand> CategoryBrands { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCharacteristic> ItemCharacteristics { get; set; }
        public DbSet<ItemColor> ItemColors { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Complect> Complects { get; set; }
        public DbSet<ComplectItem> ComplectItems { get; set; }
        public DbSet<Usluga> Uslugi { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<SearchItem> SearchItems { get; set; }
        public DbSet<UserUseStatistick> UserUseStatisticks { get; set; }
        public DbSet<CategoryStatistik> CategoryStatistiks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.ParentCategoryId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.IconUrl)
                    .HasMaxLength(500);

                entity.Property(e => e.ImgUrl)
                    .HasMaxLength(500);

                // Связь для подкатегорий (самоссылающаяся)
                entity.HasOne(c => c.ParentCategory)
                    .WithMany(c => c.SubCategories)
                    .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            // Настройка CategoryFilter
            modelBuilder.Entity<CategoryFilter>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.CategoryId);

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ValueType)
                    .HasConversion<int>();

                entity.HasOne(cf => cf.Category)
                    .WithMany(c => c.Filters)
                    .HasForeignKey(cf => cf.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Настройка CategoryBrand (составной ключ)
            modelBuilder.Entity<CategoryBrand>(entity =>
            {
                entity.HasKey(cb => new { cb.CategoryId, cb.BrandId });

                entity.HasOne(cb => cb.Category)
                .WithMany(c => c.CategoryBrands)
                .HasForeignKey(cb => cb.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(cb => cb.Brand)
                .WithMany(b => b.CategoryBrands)
                .HasForeignKey(cb => cb.BrandId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Настройка Brand
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Name).IsUnique();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Logo).HasMaxLength(500);
            });

            // Настройка Item
            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(300);
                entity.Property(e => e.Desc).HasMaxLength(4000);
                entity.Property(e => e.ImageUrl).HasMaxLength(500);
                entity.Property(e => e.Price).IsRequired();
                entity.Property(e => e.Garantia).IsRequired();

                entity.HasOne(i => i.Brand)
                    .WithMany(b => b.Items)
                .HasForeignKey(i => i.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(i => i.Category)
                .WithMany()
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(i => i.User)
                .WithMany(u => u.SellingItems)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            // Настройка ItemCharacteristic
            modelBuilder.Entity<ItemCharacteristic>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Key).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Value).IsRequired().HasMaxLength(500);

                entity.HasOne(ic => ic.Item)
                    .WithMany(i => i.Characteristics)
                    .HasForeignKey(ic => ic.ItemId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Настройка ItemColor
            modelBuilder.Entity<ItemColor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Color).IsRequired().HasMaxLength(100);

                entity.HasOne(ic => ic.Item)
                    .WithMany(i => i.Colors)
                    .HasForeignKey(ic => ic.ItemId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Настройка Review
            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Comment).HasMaxLength(2000);
                entity.Property(e => e.Rating).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();

                entity.HasOne(r => r.Item)
                .WithMany(i => i.Reviews)
                .HasForeignKey(r => r.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            // Настройка Complect
            modelBuilder.Entity<Complect>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            });

            // Настройка ComplectItem (составной ключ)
            modelBuilder.Entity<ComplectItem>(entity =>
            {
                entity.HasKey(ci => new { ci.ComplectId, ci.ItemId });

                entity.HasOne(ci => ci.Complect)
                    .WithMany(c => c.Items)
                    .HasForeignKey(ci => ci.ComplectId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ci => ci.Item)
                    .WithMany(i => i.ComplectItems)
                    .HasForeignKey(ci => ci.ItemId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            // Настройка Usluga
            modelBuilder.Entity<Usluga>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.Price).HasPrecision(18, 2);

                entity.HasOne(u => u.Item)
                    .WithMany(i => i.Uslugi)
                    .HasForeignKey(u => u.ItemId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Настройка Delivery
            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DeliveryPlace).IsRequired().HasMaxLength(200);
                entity.Property(e => e.SendingPlace).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Price).HasPrecision(18, 2);
                entity.Property(e => e.PaymentType).HasConversion<int>();

                entity.HasOne(d => d.Item)
                    .WithMany(i => i.DeliveryVariants)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // User managed by Identity; only custom nav props configured here

            // Настройка CartItem
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Quantity).IsRequired();

                entity.HasOne(ci => ci.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(ci => ci.UserId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ci => ci.Item)
                .WithMany(i => i.CartItems)
                .HasForeignKey(ci => ci.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(ci => new { ci.UserId, ci.ItemId }).IsUnique();
            });

            // Настройка WishlistItem
            modelBuilder.Entity<WishlistItem>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(wi => wi.User)
                .WithMany(u => u.WishlistItems)
                    .HasForeignKey(wi => wi.UserId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(wi => wi.Item)
                .WithMany(i => i.WishlistItems)
                    .HasForeignKey(wi => wi.ItemId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(wi => new { wi.UserId, wi.ItemId }).IsUnique();
            });

            // Настройка Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Number).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.Number).IsUnique();
                entity.Property(e => e.Address).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PaymentMethod).HasMaxLength(50);
                entity.Property(e => e.PaymentStatus).HasMaxLength(50);
                entity.Property(e => e.DeliveryMethod).HasMaxLength(100);
                entity.Property(e => e.Ttn).HasMaxLength(100);
                entity.Property(e => e.TotalAmount).HasPrecision(18, 2);

                entity.HasOne(o => o.User)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(o => o.City)
                    .WithMany()
                    .HasForeignKey(o => o.CityId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Настройка OrderItem
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.PriceAtMoment).HasPrecision(18, 2);

                entity.HasOne(oi => oi.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(oi => oi.Item)
                    .WithMany(i => i.OrderItems)
                    .HasForeignKey(oi => oi.ItemId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Настройка City
            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Name).IsUnique();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            // Заполнение начальными данными
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // 1. Сначала города
            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, Name = "Kyiv" },
                new City { Id = 2, Name = "Kharkiv" },
                new City { Id = 3, Name = "Odesa" },
                new City { Id = 4, Name = "Dnipro" },
                new City { Id = 5, Name = "Lviv" },
                new City { Id = 6, Name = "Zaporizhzhia" },
                new City { Id = 7, Name = "Mykolaiv" },
                new City { Id = 8, Name = "Vinnytsia" },
                new City { Id = 9, Name = "Kherson" },
                new City { Id = 10, Name = "Poltava" },
                new City { Id = 11, Name = "Chernihiv" },
                new City { Id = 12, Name = "Cherkasy" },
                new City { Id = 13, Name = "Zhytomyr" },
                new City { Id = 14, Name = "Sumy" },
                new City { Id = 15, Name = "Rivne" },
                new City { Id = 16, Name = "Ternopil" },
                new City { Id = 17, Name = "Lutsk" },
                new City { Id = 18, Name = "Uzhhorod" },
                new City { Id = 19, Name = "Chernivtsi" },
                new City { Id = 20, Name = "IvanoFrankivsk" },
                new City { Id = 21, Name = "Kropyvnytskyi" },
                new City { Id = 22, Name = "Khmelnytskyi" }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "admin@example.com",
                    NormalizedUserName = "ADMIN@EXAMPLE.COM",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    SecurityStamp = "STATIC-SEED-STAMP-1",
                    ConcurrencyStamp = "00000000-0000-0000-0000-000000000001",
                    Name = "Admin User",
                    IsAdmin = true
                },
                new User
                {
                    Id = 2,
                    UserName = "test@example.com",
                    NormalizedUserName = "TEST@EXAMPLE.COM",
                    Email = "test@example.com",
                    NormalizedEmail = "TEST@EXAMPLE.COM",
                    EmailConfirmed = true,
                    SecurityStamp = "STATIC-SEED-STAMP-2",
                    ConcurrencyStamp = "00000000-0000-0000-0000-000000000002",
                    Name = "Test User",
                    IsAdmin = false
                }
            );

            // 3. Бренды (добавляем перед Items)
            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, Name = "Apple", Logo = "/images/brands/apple.png" },
                new Brand { Id = 2, Name = "Samsung", Logo = "/images/brands/samsung.png" },
                new Brand { Id = 3, Name = "Xiaomi", Logo = "/images/brands/xiaomi.png" },
                new Brand { Id = 4, Name = "Sony", Logo = "/images/brands/sony.png" },
                new Brand { Id = 5, Name = "LG", Logo = "/images/brands/lg.png" },
                new Brand { Id = 6, Name = "Bosch", Logo = "/images/brands/bosch.png" },
                new Brand { Id = 7, Name = "Nike", Logo = "/images/brands/nike.png" },
                new Brand { Id = 8, Name = "Adidas", Logo = "/images/brands/adidas.png" },
                new Brand { Id = 9, Name = "Puma", Logo = "/images/brands/puma.png" },
                new Brand { Id = 10, Name = "Zara", Logo = "/images/brands/zara.png" },
                new Brand { Id = 11, Name = "H&M", Logo = "/images/brands/hm.png" },
                new Brand { Id = 12, Name = "Dell", Logo = "/images/brands/dell.png" },
                new Brand { Id = 13, Name = "HP", Logo = "/images/brands/hp.png" },
                new Brand { Id = 14, Name = "Lenovo", Logo = "/images/brands/lenovo.png" },
                new Brand { Id = 15, Name = "Asus", Logo = "/images/brands/asus.png" },
                new Brand { Id = 16, Name = "Acer", Logo = "/images/brands/acer.png" },
                new Brand { Id = 17, Name = "Microsoft", Logo = "/images/brands/microsoft.png" },
                new Brand { Id = 18, Name = "Canon", Logo = "/images/brands/canon.png" },
                new Brand { Id = 19, Name = "Nikon", Logo = "/images/brands/nikon.png" },
                new Brand { Id = 20, Name = "Panasonic", Logo = "/images/brands/panasonic.png" }
            );

            // 4. Категории верхнего уровня
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Ноутбуки та комп'ютери", IconUrl = "icon-laptops-and-computers.svg", ImgUrl = "categoryImg-laptops-and-computers.svg", ParentCategoryId = null, DisplayOrder = 1 },
                new Category { Id = 2, Name = "Смартфони, ТВ та електроніка", IconUrl = "icon-smartphones-tv-electronics.svg", ImgUrl = "categoryImg-smartphones-tv-electronics.svg", ParentCategoryId = null, DisplayOrder = 2 },
                new Category { Id = 3, Name = "Товари для геймерів", IconUrl = "icon-gaming.svg", ImgUrl = "categoryImg-gaming.svg", ParentCategoryId = null, DisplayOrder = 3 },
                new Category { Id = 4, Name = "Побутова техніка", IconUrl = "icon-home-appliances.svg", ImgUrl = "categoryImg-home-appliances.svg", ParentCategoryId = null, DisplayOrder = 4 },
                new Category { Id = 5, Name = "Товари для дому", IconUrl = "icon-home-goods.svg", ImgUrl = "categoryImg-home-goods.svg", ParentCategoryId = null, DisplayOrder = 5 },
                new Category { Id = 6, Name = "Інструменти та автотовари", IconUrl = "icon-tools-auto.svg", ImgUrl = "categoryImg-tools-auto.svg", ParentCategoryId = null, DisplayOrder = 6 },
                new Category { Id = 7, Name = "Сантехніка та ремонт", IconUrl = "icon-plumbing-renovation.svg", ImgUrl = "categoryImg-plumbing-renovation.svg", ParentCategoryId = null, DisplayOrder = 7 },
                new Category { Id = 8, Name = "Дача, сад та город", IconUrl = "icon-garden.svg", ImgUrl = "categoryImg-garden.svg", ParentCategoryId = null, DisplayOrder = 8 },
                new Category { Id = 9, Name = "Спорт та захоплення", IconUrl = "icon-sports-hobbies.svg", ImgUrl = "categoryImg-sports-hobbies.svg", ParentCategoryId = null, DisplayOrder = 9 },
                new Category { Id = 10, Name = "Одяг, взуття та прикраси", IconUrl = "icon-clothing-footwear-jewelry.svg", ImgUrl = "categoryImg-clothing-footwear-jewelry.svg", ParentCategoryId = null, DisplayOrder = 10 },
                new Category { Id = 11, Name = "Краса і здоров'я", IconUrl = "icon-beauty-health.svg", ImgUrl = "categoryImg-beauty-health.svg", ParentCategoryId = null, DisplayOrder = 11 },
                new Category { Id = 12, Name = "Дитячі товари", IconUrl = "icon-baby-products.svg", ImgUrl = "categoryImg-baby-products.svg", ParentCategoryId = null, DisplayOrder = 12 },
                new Category { Id = 13, Name = "Зоотовари", IconUrl = "icon-pet-supplies.svg", ImgUrl = "categoryImg-pet-supplies.svg", ParentCategoryId = null, DisplayOrder = 13 },
                new Category { Id = 14, Name = "Канцтовари та книги", IconUrl = "icon-stationery-books.svg", ImgUrl = "categoryImg-stationery-books.svg", ParentCategoryId = null, DisplayOrder = 14 },
                new Category { Id = 15, Name = "Алкогольні напої та продукти", IconUrl = "icon-alcohol-food.svg", ImgUrl = "categoryImg-alcohol-food.svg", ParentCategoryId = null, DisplayOrder = 15 },
                new Category { Id = 16, Name = "Товари для бізнесу та послуги", IconUrl = "icon-business-services.svg", ImgUrl = "categoryImg-business-services.svg", ParentCategoryId = null, DisplayOrder = 16 },
                new Category { Id = 17, Name = "Туризм та відпочинок", IconUrl = "icon-tourism-outdoor.svg", ImgUrl = "categoryImg-tourism-outdoor.svg", ParentCategoryId = null, DisplayOrder = 17 },
                new Category { Id = 18, Name = "Акції", IconUrl = "icon-promotions.svg", ImgUrl = "categoryImg-promotions.svg", ParentCategoryId = null, DisplayOrder = 18 },
                new Category { Id = 19, Name = "Тотальний розпродаж", IconUrl = "icon-total-sale.svg", ImgUrl = "categoryImg-total-sale.svg", ParentCategoryId = null, DisplayOrder = 19 }
            );

            // 5. Подкатегории с заполненными IconUrl и ImgUrl
            // Категория 1 - Ноутбуки та комп'ютери
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 101, Name = "Ноутбуки", IconUrl = "categoryIcon-Notebooks.svg", ImgUrl = "categoryImg-Notebooks.svg", ParentCategoryId = 1, DisplayOrder = 1 },
                new Category { Id = 102, Name = "Ігрові ноутбуки", IconUrl = "categoryIcon-GamingNotebooks.svg", ImgUrl = "categoryImg-GamingNotebooks.svg", ParentCategoryId = 1, DisplayOrder = 2 },
                new Category { Id = 103, Name = "Ультрабуки", IconUrl = "categoryIcon-Ultrabooks.svg", ImgUrl = "categoryImg-Ultrabooks.svg", ParentCategoryId = 1, DisplayOrder = 3 },
                new Category { Id = 104, Name = "Для навчання", IconUrl = "categoryIcon-ForStudy.svg", ImgUrl = "categoryImg-ForStudy.svg", ParentCategoryId = 1, DisplayOrder = 4 },
                new Category { Id = 105, Name = "Для роботи", IconUrl = "categoryIcon-ForWork.svg", ImgUrl = "categoryImg-ForWork.svg", ParentCategoryId = 1, DisplayOrder = 5 },
                new Category { Id = 106, Name = "Chromebook", IconUrl = "categoryIcon-Chromebook.svg", ImgUrl = "categoryImg-Chromebook.svg", ParentCategoryId = 1, DisplayOrder = 6 },
                new Category { Id = 107, Name = "Комп'ютери", IconUrl = "categoryIcon-Computers.svg", ImgUrl = "categoryImg-Computers.svg", ParentCategoryId = 1, DisplayOrder = 7 },
                new Category { Id = 108, Name = "Настільні ПК", IconUrl = "categoryIcon-DesktopPC.svg", ImgUrl = "categoryImg-DesktopPC.svg", ParentCategoryId = 1, DisplayOrder = 8 },
                new Category { Id = 109, Name = "Ігрові ПК", IconUrl = "categoryIcon-GamingPC.svg", ImgUrl = "categoryImg-GamingPC.svg", ParentCategoryId = 1, DisplayOrder = 9 },
                new Category { Id = 110, Name = "Міні-ПК", IconUrl = "categoryIcon-MiniPC.svg", ImgUrl = "categoryImg-MiniPC.svg", ParentCategoryId = 1, DisplayOrder = 10 },
                new Category { Id = 111, Name = "Моноблоки", IconUrl = "categoryIcon-Monoblocks.svg", ImgUrl = "categoryImg-Monoblocks.svg", ParentCategoryId = 1, DisplayOrder = 11 },
                new Category { Id = 112, Name = "Робочі станції", IconUrl = "categoryIcon-Workstations.svg", ImgUrl = "categoryImg-Workstations.svg", ParentCategoryId = 1, DisplayOrder = 12 },
                new Category { Id = 113, Name = "Комплектуючі", IconUrl = "categoryIcon-Components.svg", ImgUrl = "categoryImg-Components.svg", ParentCategoryId = 1, DisplayOrder = 13 },
                new Category { Id = 114, Name = "Процесори", IconUrl = "categoryIcon-Processors.svg", ImgUrl = "categoryImg-Processors.svg", ParentCategoryId = 1, DisplayOrder = 14 },
                new Category { Id = 115, Name = "Відеокарти", IconUrl = "categoryIcon-VideoCards.svg", ImgUrl = "categoryImg-VideoCards.svg", ParentCategoryId = 1, DisplayOrder = 15 },
                new Category { Id = 116, Name = "Материнські плати", IconUrl = "categoryIcon-Motherboards.svg", ImgUrl = "categoryImg-Motherboards.svg", ParentCategoryId = 1, DisplayOrder = 16 },
                new Category { Id = 117, Name = "Оперативна пам'ять", IconUrl = "categoryIcon-RAM.svg", ImgUrl = "categoryImg-RAM.svg", ParentCategoryId = 1, DisplayOrder = 17 },
                new Category { Id = 118, Name = "Блоки живлення", IconUrl = "categoryIcon-PowerSupplies.svg", ImgUrl = "categoryImg-PowerSupplies.svg", ParentCategoryId = 1, DisplayOrder = 18 },
                new Category { Id = 119, Name = "Корпуси", IconUrl = "categoryIcon-Cases.svg", ImgUrl = "categoryImg-Cases.svg", ParentCategoryId = 1, DisplayOrder = 19 },
                new Category { Id = 120, Name = "Накопичувачі", IconUrl = "categoryIcon-Storage.svg", ImgUrl = "categoryImg-Storage.svg", ParentCategoryId = 1, DisplayOrder = 20 },
                new Category { Id = 121, Name = "SSD", IconUrl = "categoryIcon-SSD.svg", ImgUrl = "categoryImg-SSD.svg", ParentCategoryId = 1, DisplayOrder = 21 },
                new Category { Id = 122, Name = "HDD", IconUrl = "categoryIcon-HDD.svg", ImgUrl = "categoryImg-HDD.svg", ParentCategoryId = 1, DisplayOrder = 22 },
                new Category { Id = 123, Name = "Зовнішні диски", IconUrl = "categoryIcon-ExternalDrives.svg", ImgUrl = "categoryImg-ExternalDrives.svg", ParentCategoryId = 1, DisplayOrder = 23 },
                new Category { Id = 124, Name = "NAS", IconUrl = "categoryIcon-NAS.svg", ImgUrl = "categoryImg-NAS.svg", ParentCategoryId = 1, DisplayOrder = 24 },
                new Category { Id = 125, Name = "Периферія", IconUrl = "categoryIcon-Peripherals.svg", ImgUrl = "categoryImg-Peripherals.svg", ParentCategoryId = 1, DisplayOrder = 25 },
                new Category { Id = 126, Name = "Клавіатури", IconUrl = "categoryIcon-Keyboards.svg", ImgUrl = "categoryImg-Keyboards.svg", ParentCategoryId = 1, DisplayOrder = 26 },
                new Category { Id = 127, Name = "Миші", IconUrl = "categoryIcon-Mice.svg", ImgUrl = "categoryImg-Mice.svg", ParentCategoryId = 1, DisplayOrder = 27 },
                new Category { Id = 128, Name = "Килимки", IconUrl = "categoryIcon-MousePads.svg", ImgUrl = "categoryImg-MousePads.svg", ParentCategoryId = 1, DisplayOrder = 28 },
                new Category { Id = 129, Name = "Вебкамери", IconUrl = "categoryIcon-Webcams.svg", ImgUrl = "categoryImg-Webcams.svg", ParentCategoryId = 1, DisplayOrder = 29 }
            );

            // Категория 2 - Смартфони, ТВ та електроніка
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 201, Name = "Смартфони", IconUrl = "categoryIcon-Smartphones.svg", ImgUrl = "categoryImg-Smartphones.svg", ParentCategoryId = 2, DisplayOrder = 1 },
                new Category { Id = 202, Name = "Android", IconUrl = "categoryIcon-Android.svg", ImgUrl = "categoryImg-Android.svg", ParentCategoryId = 2, DisplayOrder = 2 },
                new Category { Id = 203, Name = "iPhone", IconUrl = "categoryIcon-iPhone.svg", ImgUrl = "categoryImg-iPhone.svg", ParentCategoryId = 2, DisplayOrder = 3 },
                new Category { Id = 204, Name = "Бюджетні", IconUrl = "categoryIcon-Budget.svg", ImgUrl = "categoryImg-Budget.svg", ParentCategoryId = 2, DisplayOrder = 4 },
                new Category { Id = 205, Name = "Флагмани", IconUrl = "categoryIcon-Flagship.svg", ImgUrl = "categoryImg-Flagship.svg", ParentCategoryId = 2, DisplayOrder = 5 },
                new Category { Id = 206, Name = "Телевізори", IconUrl = "categoryIcon-TVs.svg", ImgUrl = "categoryImg-TVs.svg", ParentCategoryId = 2, DisplayOrder = 6 },
                new Category { Id = 207, Name = "Smart TV", IconUrl = "categoryIcon-SmartTV.svg", ImgUrl = "categoryImg-SmartTV.svg", ParentCategoryId = 2, DisplayOrder = 7 },
                new Category { Id = 208, Name = "LED", IconUrl = "categoryIcon-LED.svg", ImgUrl = "categoryImg-LED.svg", ParentCategoryId = 2, DisplayOrder = 8 },
                new Category { Id = 209, Name = "OLED", IconUrl = "categoryIcon-OLED.svg", ImgUrl = "categoryImg-OLED.svg", ParentCategoryId = 2, DisplayOrder = 9 },
                new Category { Id = 210, Name = "QLED", IconUrl = "categoryIcon-QLED.svg", ImgUrl = "categoryImg-QLED.svg", ParentCategoryId = 2, DisplayOrder = 10 },
                new Category { Id = 211, Name = "Аудіо", IconUrl = "categoryIcon-Audio.svg", ImgUrl = "categoryImg-Audio.svg", ParentCategoryId = 2, DisplayOrder = 11 },
                new Category { Id = 212, Name = "Навушники", IconUrl = "categoryIcon-Headphones.svg", ImgUrl = "categoryImg-Headphones.svg", ParentCategoryId = 2, DisplayOrder = 12 },
                new Category { Id = 213, Name = "Саундбари", IconUrl = "categoryIcon-Soundbars.svg", ImgUrl = "categoryImg-Soundbars.svg", ParentCategoryId = 2, DisplayOrder = 13 },
                new Category { Id = 214, Name = "Колонки", IconUrl = "categoryIcon-Speakers.svg", ImgUrl = "categoryImg-Speakers.svg", ParentCategoryId = 2, DisplayOrder = 14 },
                new Category { Id = 215, Name = "Домашні кінотеатри", IconUrl = "categoryIcon-HomeTheaters.svg", ImgUrl = "categoryImg-HomeTheaters.svg", ParentCategoryId = 2, DisplayOrder = 15 },
                new Category { Id = 216, Name = "Планшети", IconUrl = "categoryIcon-Tablets.svg", ImgUrl = "categoryImg-Tablets.svg", ParentCategoryId = 2, DisplayOrder = 16 },
                new Category { Id = 217, Name = "iPad", IconUrl = "categoryIcon-iPad.svg", ImgUrl = "categoryImg-iPad.svg", ParentCategoryId = 2, DisplayOrder = 17 },
                new Category { Id = 218, Name = "Android планшети", IconUrl = "categoryIcon-AndroidTablets.svg", ImgUrl = "categoryImg-AndroidTablets.svg", ParentCategoryId = 2, DisplayOrder = 18 },
                new Category { Id = 219, Name = "Гаджети", IconUrl = "categoryIcon-Gadgets.svg", ImgUrl = "categoryImg-Gadgets.svg", ParentCategoryId = 2, DisplayOrder = 19 },
                new Category { Id = 220, Name = "Смарт-годинники", IconUrl = "categoryIcon-SmartWatches.svg", ImgUrl = "categoryImg-SmartWatches.svg", ParentCategoryId = 2, DisplayOrder = 20 },
                new Category { Id = 221, Name = "Фітнес-браслети", IconUrl = "categoryIcon-FitnessBands.svg", ImgUrl = "categoryImg-FitnessBands.svg", ParentCategoryId = 2, DisplayOrder = 21 }
            );

            // Категория 3 - Товари для геймерів
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 301, Name = "Консолі", IconUrl = "categoryIcon-Consoles.svg", ImgUrl = "categoryImg-Consoles.svg", ParentCategoryId = 3, DisplayOrder = 1 },
                new Category { Id = 302, Name = "PlayStation", IconUrl = "categoryIcon-PlayStation.svg", ImgUrl = "categoryImg-PlayStation.svg", ParentCategoryId = 3, DisplayOrder = 2 },
                new Category { Id = 303, Name = "Xbox", IconUrl = "categoryIcon-Xbox.svg", ImgUrl = "categoryImg-Xbox.svg", ParentCategoryId = 3, DisplayOrder = 3 },
                new Category { Id = 304, Name = "Nintendo", IconUrl = "categoryIcon-Nintendo.svg", ImgUrl = "categoryImg-Nintendo.svg", ParentCategoryId = 3, DisplayOrder = 4 },
                new Category { Id = 305, Name = "Ігри", IconUrl = "categoryIcon-Games.svg", ImgUrl = "categoryImg-Games.svg", ParentCategoryId = 3, DisplayOrder = 5 },
                new Category { Id = 306, Name = "PlayStation ігри", IconUrl = "categoryIcon-PlayStationGames.svg", ImgUrl = "categoryImg-PlayStationGames.svg", ParentCategoryId = 3, DisplayOrder = 6 },
                new Category { Id = 307, Name = "Xbox ігри", IconUrl = "categoryIcon-XboxGames.svg", ImgUrl = "categoryImg-XboxGames.svg", ParentCategoryId = 3, DisplayOrder = 7 },
                new Category { Id = 308, Name = "PC ігри", IconUrl = "categoryIcon-PCGames.svg", ImgUrl = "categoryImg-PCGames.svg", ParentCategoryId = 3, DisplayOrder = 8 },
                new Category { Id = 309, Name = "Геймерська периферія", IconUrl = "categoryIcon-GamingPeripherals.svg", ImgUrl = "categoryImg-GamingPeripherals.svg", ParentCategoryId = 3, DisplayOrder = 9 },
                new Category { Id = 310, Name = "Ігрові миші", IconUrl = "categoryIcon-GamingMice.svg", ImgUrl = "categoryImg-GamingMice.svg", ParentCategoryId = 3, DisplayOrder = 10 },
                new Category { Id = 311, Name = "Ігрові клавіатури", IconUrl = "categoryIcon-GamingKeyboards.svg", ImgUrl = "categoryImg-GamingKeyboards.svg", ParentCategoryId = 3, DisplayOrder = 11 },
                new Category { Id = 312, Name = "Геймерські навушники", IconUrl = "categoryIcon-GamingHeadphones.svg", ImgUrl = "categoryImg-GamingHeadphones.svg", ParentCategoryId = 3, DisplayOrder = 12 },
                new Category { Id = 313, Name = "VR", IconUrl = "categoryIcon-VR.svg", ImgUrl = "categoryImg-VR.svg", ParentCategoryId = 3, DisplayOrder = 13 },
                new Category { Id = 314, Name = "VR шоломи", IconUrl = "categoryIcon-VRHeadsets.svg", ImgUrl = "categoryImg-VRHeadsets.svg", ParentCategoryId = 3, DisplayOrder = 14 },
                new Category { Id = 315, Name = "VR аксесуари", IconUrl = "categoryIcon-VRAccessories.svg", ImgUrl = "categoryImg-VRAccessories.svg", ParentCategoryId = 3, DisplayOrder = 15 }
            );

            // Категория 4 - Побутова техніка
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 401, Name = "Велика техніка", IconUrl = "categoryIcon-LargeAppliances.svg", ImgUrl = "categoryImg-LargeAppliances.svg", ParentCategoryId = 4, DisplayOrder = 1 },
                new Category { Id = 402, Name = "Холодильники", IconUrl = "categoryIcon-Refrigerators.svg", ImgUrl = "categoryImg-Refrigerators.svg", ParentCategoryId = 4, DisplayOrder = 2 },
                new Category { Id = 403, Name = "Пральні машини", IconUrl = "categoryIcon-WashingMachines.svg", ImgUrl = "categoryImg-WashingMachines.svg", ParentCategoryId = 4, DisplayOrder = 3 },
                new Category { Id = 404, Name = "Посудомийні машини", IconUrl = "categoryIcon-Dishwashers.svg", ImgUrl = "categoryImg-Dishwashers.svg", ParentCategoryId = 4, DisplayOrder = 4 },
                new Category { Id = 405, Name = "Кухонна техніка", IconUrl = "categoryIcon-KitchenAppliances.svg", ImgUrl = "categoryImg-KitchenAppliances.svg", ParentCategoryId = 4, DisplayOrder = 5 },
                new Category { Id = 406, Name = "Мікрохвильові печі", IconUrl = "categoryIcon-Microwaves.svg", ImgUrl = "categoryImg-Microwaves.svg", ParentCategoryId = 4, DisplayOrder = 6 },
                new Category { Id = 407, Name = "Блендери", IconUrl = "categoryIcon-Blenders.svg", ImgUrl = "categoryImg-Blenders.svg", ParentCategoryId = 4, DisplayOrder = 7 },
                new Category { Id = 408, Name = "Міксери", IconUrl = "categoryIcon-Mixers.svg", ImgUrl = "categoryImg-Mixers.svg", ParentCategoryId = 4, DisplayOrder = 8 },
                new Category { Id = 409, Name = "Мультиварки", IconUrl = "categoryIcon-Multicookers.svg", ImgUrl = "categoryImg-Multicookers.svg", ParentCategoryId = 4, DisplayOrder = 9 },
                new Category { Id = 410, Name = "Кліматична техніка", IconUrl = "categoryIcon-ClimateControl.svg", ImgUrl = "categoryImg-ClimateControl.svg", ParentCategoryId = 4, DisplayOrder = 10 },
                new Category { Id = 411, Name = "Кондиціонери", IconUrl = "categoryIcon-AirConditioners.svg", ImgUrl = "categoryImg-AirConditioners.svg", ParentCategoryId = 4, DisplayOrder = 11 },
                new Category { Id = 412, Name = "Обігрівачі", IconUrl = "categoryIcon-Heaters.svg", ImgUrl = "categoryImg-Heaters.svg", ParentCategoryId = 4, DisplayOrder = 12 },
                new Category { Id = 413, Name = "Вентилятори", IconUrl = "categoryIcon-Fans.svg", ImgUrl = "categoryImg-Fans.svg", ParentCategoryId = 4, DisplayOrder = 13 },
                new Category { Id = 414, Name = "Прибирання", IconUrl = "categoryIcon-Cleaning.svg", ImgUrl = "categoryImg-Cleaning.svg", ParentCategoryId = 4, DisplayOrder = 14 },
                new Category { Id = 415, Name = "Пилососи", IconUrl = "categoryIcon-VacuumCleaners.svg", ImgUrl = "categoryImg-VacuumCleaners.svg", ParentCategoryId = 4, DisplayOrder = 15 },
                new Category { Id = 416, Name = "Роботи-пилососи", IconUrl = "categoryIcon-RobotVacuums.svg", ImgUrl = "categoryImg-RobotVacuums.svg", ParentCategoryId = 4, DisplayOrder = 16 }
            );

            // Категория 5 - Товари для дому
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 501, Name = "Меблі", IconUrl = "categoryIcon-Furniture.svg", ImgUrl = "categoryImg-Furniture.svg", ParentCategoryId = 5, DisplayOrder = 1 },
                new Category { Id = 502, Name = "Дивани", IconUrl = "categoryIcon-Sofas.svg", ImgUrl = "categoryImg-Sofas.svg", ParentCategoryId = 5, DisplayOrder = 2 },
                new Category { Id = 503, Name = "Ліжка", IconUrl = "categoryIcon-Beds.svg", ImgUrl = "categoryImg-Beds.svg", ParentCategoryId = 5, DisplayOrder = 3 },
                new Category { Id = 504, Name = "Шафи", IconUrl = "categoryIcon-Wardrobes.svg", ImgUrl = "categoryImg-Wardrobes.svg", ParentCategoryId = 5, DisplayOrder = 4 },
                new Category { Id = 505, Name = "Освітлення", IconUrl = "categoryIcon-Lighting.svg", ImgUrl = "categoryImg-Lighting.svg", ParentCategoryId = 5, DisplayOrder = 5 },
                new Category { Id = 506, Name = "Лампи", IconUrl = "categoryIcon-Lamps.svg", ImgUrl = "categoryImg-Lamps.svg", ParentCategoryId = 5, DisplayOrder = 6 },
                new Category { Id = 507, Name = "Люстри", IconUrl = "categoryIcon-Chandeliers.svg", ImgUrl = "categoryImg-Chandeliers.svg", ParentCategoryId = 5, DisplayOrder = 7 },
                new Category { Id = 508, Name = "LED освітлення", IconUrl = "categoryIcon-LEDLighting.svg", ImgUrl = "categoryImg-LEDLighting.svg", ParentCategoryId = 5, DisplayOrder = 8 },
                new Category { Id = 509, Name = "Декор", IconUrl = "categoryIcon-Decor.svg", ImgUrl = "categoryImg-Decor.svg", ParentCategoryId = 5, DisplayOrder = 9 },
                new Category { Id = 510, Name = "Картини", IconUrl = "categoryIcon-Paintings.svg", ImgUrl = "categoryImg-Paintings.svg", ParentCategoryId = 5, DisplayOrder = 10 },
                new Category { Id = 511, Name = "Дзеркала", IconUrl = "categoryIcon-Mirrors.svg", ImgUrl = "categoryImg-Mirrors.svg", ParentCategoryId = 5, DisplayOrder = 11 },
                new Category { Id = 512, Name = "Годинники", IconUrl = "categoryIcon-Clocks.svg", ImgUrl = "categoryImg-Clocks.svg", ParentCategoryId = 5, DisplayOrder = 12 }
            );

            // Категория 6 - Інструменти та автотовари
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 601, Name = "Електроінструменти", IconUrl = "categoryIcon-PowerTools.svg", ImgUrl = "categoryImg-PowerTools.svg", ParentCategoryId = 6, DisplayOrder = 1 },
                new Category { Id = 602, Name = "Дрилі", IconUrl = "categoryIcon-Drills.svg", ImgUrl = "categoryImg-Drills.svg", ParentCategoryId = 6, DisplayOrder = 2 },
                new Category { Id = 603, Name = "Шуруповерти", IconUrl = "categoryIcon-Screwdrivers.svg", ImgUrl = "categoryImg-Screwdrivers.svg", ParentCategoryId = 6, DisplayOrder = 3 },
                new Category { Id = 604, Name = "Болгарки", IconUrl = "categoryIcon-AngleGrinders.svg", ImgUrl = "categoryImg-AngleGrinders.svg", ParentCategoryId = 6, DisplayOrder = 4 },
                new Category { Id = 605, Name = "Автоелектроніка", IconUrl = "categoryIcon-CarElectronics.svg", ImgUrl = "categoryImg-CarElectronics.svg", ParentCategoryId = 6, DisplayOrder = 5 },
                new Category { Id = 606, Name = "Відеореєстратори", IconUrl = "categoryIcon-DashCams.svg", ImgUrl = "categoryImg-DashCams.svg", ParentCategoryId = 6, DisplayOrder = 6 },
                new Category { Id = 607, Name = "GPS навігатори", IconUrl = "categoryIcon-GPS.svg", ImgUrl = "categoryImg-GPS.svg", ParentCategoryId = 6, DisplayOrder = 7 },
                new Category { Id = 608, Name = "Автоаксесуари", IconUrl = "categoryIcon-CarAccessories.svg", ImgUrl = "categoryImg-CarAccessories.svg", ParentCategoryId = 6, DisplayOrder = 8 },
                new Category { Id = 609, Name = "Тримачі телефону", IconUrl = "categoryIcon-PhoneHolders.svg", ImgUrl = "categoryImg-PhoneHolders.svg", ParentCategoryId = 6, DisplayOrder = 9 },
                new Category { Id = 610, Name = "Зарядні пристрої", IconUrl = "categoryIcon-CarChargers.svg", ImgUrl = "categoryImg-CarChargers.svg", ParentCategoryId = 6, DisplayOrder = 10 }
            );

            // Категория 7 - Сантехніка та ремонт
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 701, Name = "Ванна кімната", IconUrl = "categoryIcon-Bathroom.svg", ImgUrl = "categoryImg-Bathroom.svg", ParentCategoryId = 7, DisplayOrder = 1 },
                new Category { Id = 702, Name = "Душові кабіни", IconUrl = "categoryIcon-ShowerCubicles.svg", ImgUrl = "categoryImg-ShowerCubicles.svg", ParentCategoryId = 7, DisplayOrder = 2 },
                new Category { Id = 703, Name = "Унітази", IconUrl = "categoryIcon-Toilets.svg", ImgUrl = "categoryImg-Toilets.svg", ParentCategoryId = 7, DisplayOrder = 3 },
                new Category { Id = 704, Name = "Раковини", IconUrl = "categoryIcon-Sinks.svg", ImgUrl = "categoryImg-Sinks.svg", ParentCategoryId = 7, DisplayOrder = 4 },
                new Category { Id = 705, Name = "Інструменти", IconUrl = "categoryIcon-Tools.svg", ImgUrl = "categoryImg-Tools.svg", ParentCategoryId = 7, DisplayOrder = 5 },
                new Category { Id = 706, Name = "Ручний інструмент", IconUrl = "categoryIcon-HandTools.svg", ImgUrl = "categoryImg-HandTools.svg", ParentCategoryId = 7, DisplayOrder = 6 },
                new Category { Id = 707, Name = "Вимірювальні прилади", IconUrl = "categoryIcon-MeasuringTools.svg", ImgUrl = "categoryImg-MeasuringTools.svg", ParentCategoryId = 7, DisplayOrder = 7 },
                new Category { Id = 708, Name = "Матеріали", IconUrl = "categoryIcon-Materials.svg", ImgUrl = "categoryImg-Materials.svg", ParentCategoryId = 7, DisplayOrder = 8 },
                new Category { Id = 709, Name = "Фарба", IconUrl = "categoryIcon-Paint.svg", ImgUrl = "categoryImg-Paint.svg", ParentCategoryId = 7, DisplayOrder = 9 },
                new Category { Id = 710, Name = "Плитка", IconUrl = "categoryIcon-Tiles.svg", ImgUrl = "categoryImg-Tiles.svg", ParentCategoryId = 7, DisplayOrder = 10 },
                new Category { Id = 711, Name = "Ламінат", IconUrl = "categoryIcon-Laminate.svg", ImgUrl = "categoryImg-Laminate.svg", ParentCategoryId = 7, DisplayOrder = 11 }
            );

            // Категория 8 - Дача, сад та город
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 801, Name = "Садова техніка", IconUrl = "categoryIcon-GardenTools.svg", ImgUrl = "categoryImg-GardenTools.svg", ParentCategoryId = 8, DisplayOrder = 1 },
                new Category { Id = 802, Name = "Газонокосарки", IconUrl = "categoryIcon-LawnMowers.svg", ImgUrl = "categoryImg-LawnMowers.svg", ParentCategoryId = 8, DisplayOrder = 2 },
                new Category { Id = 803, Name = "Тримери", IconUrl = "categoryIcon-Trimmers.svg", ImgUrl = "categoryImg-Trimmers.svg", ParentCategoryId = 8, DisplayOrder = 3 },
                new Category { Id = 804, Name = "Садові інструменти", IconUrl = "categoryIcon-GardenImplements.svg", ImgUrl = "categoryImg-GardenImplements.svg", ParentCategoryId = 8, DisplayOrder = 4 },
                new Category { Id = 805, Name = "Лопати", IconUrl = "categoryIcon-Shovels.svg", ImgUrl = "categoryImg-Shovels.svg", ParentCategoryId = 8, DisplayOrder = 5 },
                new Category { Id = 806, Name = "Секатори", IconUrl = "categoryIcon-Pruners.svg", ImgUrl = "categoryImg-Pruners.svg", ParentCategoryId = 8, DisplayOrder = 6 },
                new Category { Id = 807, Name = "Меблі для саду", IconUrl = "categoryIcon-GardenFurniture.svg", ImgUrl = "categoryImg-GardenFurniture.svg", ParentCategoryId = 8, DisplayOrder = 7 },
                new Category { Id = 808, Name = "Садові столи", IconUrl = "categoryIcon-GardenTables.svg", ImgUrl = "categoryImg-GardenTables.svg", ParentCategoryId = 8, DisplayOrder = 8 },
                new Category { Id = 809, Name = "Крісла", IconUrl = "categoryIcon-GardenChairs.svg", ImgUrl = "categoryImg-GardenChairs.svg", ParentCategoryId = 8, DisplayOrder = 9 }
            );

            // Категория 9 - Спорт та захоплення
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 901, Name = "Фітнес", IconUrl = "categoryIcon-Fitness.svg", ImgUrl = "categoryImg-Fitness.svg", ParentCategoryId = 9, DisplayOrder = 1 },
                new Category { Id = 902, Name = "Гантелі", IconUrl = "categoryIcon-Dumbbells.svg", ImgUrl = "categoryImg-Dumbbells.svg", ParentCategoryId = 9, DisplayOrder = 2 },
                new Category { Id = 903, Name = "Бігові доріжки", IconUrl = "categoryIcon-Treadmills.svg", ImgUrl = "categoryImg-Treadmills.svg", ParentCategoryId = 9, DisplayOrder = 3 },
                new Category { Id = 904, Name = "Велоспорт", IconUrl = "categoryIcon-Cycling.svg", ImgUrl = "categoryImg-Cycling.svg", ParentCategoryId = 9, DisplayOrder = 4 },
                new Category { Id = 905, Name = "Велосипеди", IconUrl = "categoryIcon-Bicycles.svg", ImgUrl = "categoryImg-Bicycles.svg", ParentCategoryId = 9, DisplayOrder = 5 },
                new Category { Id = 906, Name = "Аксесуари", IconUrl = "categoryIcon-CyclingAccessories.svg", ImgUrl = "categoryImg-CyclingAccessories.svg", ParentCategoryId = 9, DisplayOrder = 6 },
                new Category { Id = 907, Name = "Активний відпочинок", IconUrl = "categoryIcon-OutdoorRecreation.svg", ImgUrl = "categoryImg-OutdoorRecreation.svg", ParentCategoryId = 9, DisplayOrder = 7 },
                new Category { Id = 908, Name = "Самокати", IconUrl = "categoryIcon-KickScooters.svg", ImgUrl = "categoryImg-KickScooters.svg", ParentCategoryId = 9, DisplayOrder = 8 },
                new Category { Id = 909, Name = "Електросамокати", IconUrl = "categoryIcon-ElectricScooters.svg", ImgUrl = "categoryImg-ElectricScooters.svg", ParentCategoryId = 9, DisplayOrder = 9 }
            );

            // Категория 10 - Одяг, взуття та прикраси
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1001, Name = "Чоловічий одяг", IconUrl = "categoryIcon-MensClothing.svg", ImgUrl = "categoryImg-MensClothing.svg", ParentCategoryId = 10, DisplayOrder = 1 },
                new Category { Id = 1002, Name = "Футболки", IconUrl = "categoryIcon-TShirts.svg", ImgUrl = "categoryImg-TShirts.svg", ParentCategoryId = 10, DisplayOrder = 2 },
                new Category { Id = 1003, Name = "Джинси", IconUrl = "categoryIcon-Jeans.svg", ImgUrl = "categoryImg-Jeans.svg", ParentCategoryId = 10, DisplayOrder = 3 },
                new Category { Id = 1004, Name = "Куртки", IconUrl = "categoryIcon-Jackets.svg", ImgUrl = "categoryImg-Jackets.svg", ParentCategoryId = 10, DisplayOrder = 4 },
                new Category { Id = 1005, Name = "Жіночий одяг", IconUrl = "categoryIcon-WomensClothing.svg", ImgUrl = "categoryImg-WomensClothing.svg", ParentCategoryId = 10, DisplayOrder = 5 },
                new Category { Id = 1006, Name = "Сукні", IconUrl = "categoryIcon-Dresses.svg", ImgUrl = "categoryImg-Dresses.svg", ParentCategoryId = 10, DisplayOrder = 6 },
                new Category { Id = 1007, Name = "Спідниці", IconUrl = "categoryIcon-Skirts.svg", ImgUrl = "categoryImg-Skirts.svg", ParentCategoryId = 10, DisplayOrder = 7 },
                new Category { Id = 1008, Name = "Взуття", IconUrl = "categoryIcon-Footwear.svg", ImgUrl = "categoryImg-Footwear.svg", ParentCategoryId = 10, DisplayOrder = 8 },
                new Category { Id = 1009, Name = "Кросівки", IconUrl = "categoryIcon-Sneakers.svg", ImgUrl = "categoryImg-Sneakers.svg", ParentCategoryId = 10, DisplayOrder = 9 },
                new Category { Id = 1010, Name = "Черевики", IconUrl = "categoryIcon-Boots.svg", ImgUrl = "categoryImg-Boots.svg", ParentCategoryId = 10, DisplayOrder = 10 },
                new Category { Id = 1011, Name = "Аксесуари", IconUrl = "categoryIcon-Accessories.svg", ImgUrl = "categoryImg-Accessories.svg", ParentCategoryId = 10, DisplayOrder = 11 },
                new Category { Id = 1012, Name = "Сумки", IconUrl = "categoryIcon-Bags.svg", ImgUrl = "categoryImg-Bags.svg", ParentCategoryId = 10, DisplayOrder = 12 },
                new Category { Id = 1013, Name = "Ремені", IconUrl = "categoryIcon-Belts.svg", ImgUrl = "categoryImg-Belts.svg", ParentCategoryId = 10, DisplayOrder = 13 }
            );

            // Категория 11 - Краса і здоров'я
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1101, Name = "Догляд за обличчям", IconUrl = "categoryIcon-FaceCare.svg", ImgUrl = "categoryImg-FaceCare.svg", ParentCategoryId = 11, DisplayOrder = 1 },
                new Category { Id = 1102, Name = "Креми", IconUrl = "categoryIcon-Creams.svg", ImgUrl = "categoryImg-Creams.svg", ParentCategoryId = 11, DisplayOrder = 2 },
                new Category { Id = 1103, Name = "Сироватки", IconUrl = "categoryIcon-Serums.svg", ImgUrl = "categoryImg-Serums.svg", ParentCategoryId = 11, DisplayOrder = 3 },
                new Category { Id = 1104, Name = "Догляд за волоссям", IconUrl = "categoryIcon-HairCare.svg", ImgUrl = "categoryImg-HairCare.svg", ParentCategoryId = 11, DisplayOrder = 4 },
                new Category { Id = 1105, Name = "Шампуні", IconUrl = "categoryIcon-Shampoos.svg", ImgUrl = "categoryImg-Shampoos.svg", ParentCategoryId = 11, DisplayOrder = 5 },
                new Category { Id = 1106, Name = "Маски", IconUrl = "categoryIcon-HairMasks.svg", ImgUrl = "categoryImg-HairMasks.svg", ParentCategoryId = 11, DisplayOrder = 6 },
                new Category { Id = 1107, Name = "Техніка", IconUrl = "categoryIcon-BeautyTech.svg", ImgUrl = "categoryImg-BeautyTech.svg", ParentCategoryId = 11, DisplayOrder = 7 },
                new Category { Id = 1108, Name = "Фени", IconUrl = "categoryIcon-HairDryers.svg", ImgUrl = "categoryImg-HairDryers.svg", ParentCategoryId = 11, DisplayOrder = 8 },
                new Category { Id = 1109, Name = "Бритви", IconUrl = "categoryIcon-Razors.svg", ImgUrl = "categoryImg-Razors.svg", ParentCategoryId = 11, DisplayOrder = 9 }
            );

            // Категория 12 - Дитячі товари
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1201, Name = "Іграшки", IconUrl = "categoryIcon-Toys.svg", ImgUrl = "categoryImg-Toys.svg", ParentCategoryId = 12, DisplayOrder = 1 },
                new Category { Id = 1202, Name = "Конструктори", IconUrl = "categoryIcon-ConstructionToys.svg", ImgUrl = "categoryImg-ConstructionToys.svg", ParentCategoryId = 12, DisplayOrder = 2 },
                new Category { Id = 1203, Name = "Ляльки", IconUrl = "categoryIcon-Dolls.svg", ImgUrl = "categoryImg-Dolls.svg", ParentCategoryId = 12, DisplayOrder = 3 },
                new Category { Id = 1204, Name = "Машинки", IconUrl = "categoryIcon-Cars.svg", ImgUrl = "categoryImg-Cars.svg", ParentCategoryId = 12, DisplayOrder = 4 },
                new Category { Id = 1205, Name = "Для немовлят", IconUrl = "categoryIcon-Baby.svg", ImgUrl = "categoryImg-Baby.svg", ParentCategoryId = 12, DisplayOrder = 5 },
                new Category { Id = 1206, Name = "Підгузки", IconUrl = "categoryIcon-Diapers.svg", ImgUrl = "categoryImg-Diapers.svg", ParentCategoryId = 12, DisplayOrder = 6 },
                new Category { Id = 1207, Name = "Пляшечки", IconUrl = "categoryIcon-BabyBottles.svg", ImgUrl = "categoryImg-BabyBottles.svg", ParentCategoryId = 12, DisplayOrder = 7 },
                new Category { Id = 1208, Name = "Дитячий транспорт", IconUrl = "categoryIcon-KidsVehicles.svg", ImgUrl = "categoryImg-KidsVehicles.svg", ParentCategoryId = 12, DisplayOrder = 8 },
                new Category { Id = 1209, Name = "Коляски", IconUrl = "categoryIcon-Strollers.svg", ImgUrl = "categoryImg-Strollers.svg", ParentCategoryId = 12, DisplayOrder = 9 },
                new Category { Id = 1210, Name = "Самокати", IconUrl = "categoryIcon-KidsScooters.svg", ImgUrl = "categoryImg-KidsScooters.svg", ParentCategoryId = 12, DisplayOrder = 10 }
            );

            // Категория 13 - Зоотовари
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1301, Name = "Для собак", IconUrl = "categoryIcon-Dogs.svg", ImgUrl = "categoryImg-Dogs.svg", ParentCategoryId = 13, DisplayOrder = 1 },
                new Category { Id = 1302, Name = "Корм", IconUrl = "categoryIcon-DogFood.svg", ImgUrl = "categoryImg-DogFood.svg", ParentCategoryId = 13, DisplayOrder = 2 },
                new Category { Id = 1303, Name = "Іграшки", IconUrl = "categoryIcon-DogToys.svg", ImgUrl = "categoryImg-DogToys.svg", ParentCategoryId = 13, DisplayOrder = 3 },
                new Category { Id = 1304, Name = "Для котів", IconUrl = "categoryIcon-Cats.svg", ImgUrl = "categoryImg-Cats.svg", ParentCategoryId = 13, DisplayOrder = 4 },
                new Category { Id = 1305, Name = "Корм", IconUrl = "categoryIcon-CatFood.svg", ImgUrl = "categoryImg-CatFood.svg", ParentCategoryId = 13, DisplayOrder = 5 },
                new Category { Id = 1306, Name = "Наповнювачі", IconUrl = "categoryIcon-CatLitter.svg", ImgUrl = "categoryImg-CatLitter.svg", ParentCategoryId = 13, DisplayOrder = 6 },
                new Category { Id = 1307, Name = "Для гризунів", IconUrl = "categoryIcon-Rodents.svg", ImgUrl = "categoryImg-Rodents.svg", ParentCategoryId = 13, DisplayOrder = 7 },
                new Category { Id = 1308, Name = "Клітки", IconUrl = "categoryIcon-Cages.svg", ImgUrl = "categoryImg-Cages.svg", ParentCategoryId = 13, DisplayOrder = 8 },
                new Category { Id = 1309, Name = "Корм", IconUrl = "categoryIcon-RodentFood.svg", ImgUrl = "categoryImg-RodentFood.svg", ParentCategoryId = 13, DisplayOrder = 9 }
            );

            // Категория 14 - Канцтовари та книги
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1401, Name = "Канцтовари", IconUrl = "categoryIcon-Stationery.svg", ImgUrl = "categoryImg-Stationery.svg", ParentCategoryId = 14, DisplayOrder = 1 },
                new Category { Id = 1402, Name = "Ручки", IconUrl = "categoryIcon-Pens.svg", ImgUrl = "categoryImg-Pens.svg", ParentCategoryId = 14, DisplayOrder = 2 },
                new Category { Id = 1403, Name = "Зошити", IconUrl = "categoryIcon-Notebooks.svg", ImgUrl = "categoryImg-Notebooks.svg", ParentCategoryId = 14, DisplayOrder = 3 },
                new Category { Id = 1404, Name = "Папір", IconUrl = "categoryIcon-Paper.svg", ImgUrl = "categoryImg-Paper.svg", ParentCategoryId = 14, DisplayOrder = 4 },
                new Category { Id = 1405, Name = "Книги", IconUrl = "categoryIcon-Books.svg", ImgUrl = "categoryImg-Books.svg", ParentCategoryId = 14, DisplayOrder = 5 },
                new Category { Id = 1406, Name = "Художні", IconUrl = "categoryIcon-Fiction.svg", ImgUrl = "categoryImg-Fiction.svg", ParentCategoryId = 14, DisplayOrder = 6 },
                new Category { Id = 1407, Name = "Навчальні", IconUrl = "categoryIcon-Educational.svg", ImgUrl = "categoryImg-Educational.svg", ParentCategoryId = 14, DisplayOrder = 7 }
            );

            // Категория 15 - Алкогольні напої та продукти
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1501, Name = "Алкоголь", IconUrl = "categoryIcon-Alcohol.svg", ImgUrl = "categoryImg-Alcohol.svg", ParentCategoryId = 15, DisplayOrder = 1 },
                new Category { Id = 1502, Name = "Вино", IconUrl = "categoryIcon-Wine.svg", ImgUrl = "categoryImg-Wine.svg", ParentCategoryId = 15, DisplayOrder = 2 },
                new Category { Id = 1503, Name = "Пиво", IconUrl = "categoryIcon-Beer.svg", ImgUrl = "categoryImg-Beer.svg", ParentCategoryId = 15, DisplayOrder = 3 },
                new Category { Id = 1504, Name = "Віскі", IconUrl = "categoryIcon-Whiskey.svg", ImgUrl = "categoryImg-Whiskey.svg", ParentCategoryId = 15, DisplayOrder = 4 },
                new Category { Id = 1505, Name = "Продукти", IconUrl = "categoryIcon-Food.svg", ImgUrl = "categoryImg-Food.svg", ParentCategoryId = 15, DisplayOrder = 5 },
                new Category { Id = 1506, Name = "Солодощі", IconUrl = "categoryIcon-Sweets.svg", ImgUrl = "categoryImg-Sweets.svg", ParentCategoryId = 15, DisplayOrder = 6 },
                new Category { Id = 1507, Name = "Снеки", IconUrl = "categoryIcon-Snacks.svg", ImgUrl = "categoryImg-Snacks.svg", ParentCategoryId = 15, DisplayOrder = 7 }
            );

            // Категория 16 - Товари для бізнесу та послуги
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1601, Name = "Офіс", IconUrl = "categoryIcon-Office.svg", ImgUrl = "categoryImg-Office.svg", ParentCategoryId = 16, DisplayOrder = 1 },
                new Category { Id = 1602, Name = "Офісна техніка", IconUrl = "categoryIcon-OfficeEquipment.svg", ImgUrl = "categoryImg-OfficeEquipment.svg", ParentCategoryId = 16, DisplayOrder = 2 },
                new Category { Id = 1603, Name = "Меблі", IconUrl = "categoryIcon-OfficeFurniture.svg", ImgUrl = "categoryImg-OfficeFurniture.svg", ParentCategoryId = 16, DisplayOrder = 3 },
                new Category { Id = 1604, Name = "Бізнес обладнання", IconUrl = "categoryIcon-BusinessEquipment.svg", ImgUrl = "categoryImg-BusinessEquipment.svg", ParentCategoryId = 16, DisplayOrder = 4 },
                new Category { Id = 1605, Name = "POS системи", IconUrl = "categoryIcon-POS.svg", ImgUrl = "categoryImg-POS.svg", ParentCategoryId = 16, DisplayOrder = 5 },
                new Category { Id = 1606, Name = "Касові апарати", IconUrl = "categoryIcon-CashRegisters.svg", ImgUrl = "categoryImg-CashRegisters.svg", ParentCategoryId = 16, DisplayOrder = 6 }
            );

            // Категория 17 - Туризм та відпочинок
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1701, Name = "Туристичне спорядження", IconUrl = "categoryIcon-TourismGear.svg", ImgUrl = "categoryImg-TourismGear.svg", ParentCategoryId = 17, DisplayOrder = 1 },
                new Category { Id = 1702, Name = "Намет", IconUrl = "categoryIcon-Tents.svg", ImgUrl = "categoryImg-Tents.svg", ParentCategoryId = 17, DisplayOrder = 2 },
                new Category { Id = 1703, Name = "Спальні мішки", IconUrl = "categoryIcon-SleepingBags.svg", ImgUrl = "categoryImg-SleepingBags.svg", ParentCategoryId = 17, DisplayOrder = 3 },
                new Category { Id = 1704, Name = "Подорожі", IconUrl = "categoryIcon-Travel.svg", ImgUrl = "categoryImg-Travel.svg", ParentCategoryId = 17, DisplayOrder = 4 },
                new Category { Id = 1705, Name = "Валізи", IconUrl = "categoryIcon-Suitcases.svg", ImgUrl = "categoryImg-Suitcases.svg", ParentCategoryId = 17, DisplayOrder = 5 },
                new Category { Id = 1706, Name = "Рюкзаки", IconUrl = "categoryIcon-Backpacks.svg", ImgUrl = "categoryImg-Backpacks.svg", ParentCategoryId = 17, DisplayOrder = 6 }
            );

            // Категория 18 - Акції
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1801, Name = "Товари зі знижками", IconUrl = "categoryIcon-Discounted.svg", ImgUrl = "categoryImg-Discounted.svg", ParentCategoryId = 18, DisplayOrder = 1 },
                new Category { Id = 1802, Name = "Сезонні розпродажі", IconUrl = "categoryIcon-SeasonalSales.svg", ImgUrl = "categoryImg-SeasonalSales.svg", ParentCategoryId = 18, DisplayOrder = 2 }
            );

            // Категория 19 - Тотальний розпродаж
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1901, Name = "До −50%", IconUrl = "categoryIcon-UpTo50.svg", ImgUrl = "categoryImg-UpTo50.svg", ParentCategoryId = 19, DisplayOrder = 1 },
                new Category { Id = 1902, Name = "До −70%", IconUrl = "categoryIcon-UpTo70.svg", ImgUrl = "categoryImg-UpTo70.svg", ParentCategoryId = 19, DisplayOrder = 2 },
                new Category { Id = 1903, Name = "Останні екземпляри", IconUrl = "categoryIcon-LastItems.svg", ImgUrl = "categoryImg-LastItems.svg", ParentCategoryId = 19, DisplayOrder = 3 }
            );

            // 6. Связи категорий с брендами
            modelBuilder.Entity<CategoryBrand>().HasData(
                // Категория 1 (Ноутбуки) с брендами
                new CategoryBrand { CategoryId = 1, BrandId = 1 },
                new CategoryBrand { CategoryId = 1, BrandId = 2 },
                new CategoryBrand { CategoryId = 1, BrandId = 12 },
                new CategoryBrand { CategoryId = 1, BrandId = 13 },
                new CategoryBrand { CategoryId = 1, BrandId = 14 },
                new CategoryBrand { CategoryId = 1, BrandId = 15 },
                new CategoryBrand { CategoryId = 1, BrandId = 16 },

                // Категория 2 (Смартфоны) с брендами
                new CategoryBrand { CategoryId = 2, BrandId = 1 },
                new CategoryBrand { CategoryId = 2, BrandId = 2 },
                new CategoryBrand { CategoryId = 2, BrandId = 3 },

                // Категория 3 (Гейминг) с брендами
                new CategoryBrand { CategoryId = 3, BrandId = 4 },
                new CategoryBrand { CategoryId = 3, BrandId = 15 },

                // Категория 4 (Побутова техніка) с брендами
                new CategoryBrand { CategoryId = 4, BrandId = 2 },
                new CategoryBrand { CategoryId = 4, BrandId = 4 },
                new CategoryBrand { CategoryId = 4, BrandId = 5 },
                new CategoryBrand { CategoryId = 4, BrandId = 6 },

                // Категория 9 (Спорт) с брендами
                new CategoryBrand { CategoryId = 9, BrandId = 7 },
                new CategoryBrand { CategoryId = 9, BrandId = 8 },
                new CategoryBrand { CategoryId = 9, BrandId = 9 },

                // Категория 10 (Одяг) с брендами
                new CategoryBrand { CategoryId = 10, BrandId = 7 },
                new CategoryBrand { CategoryId = 10, BrandId = 8 },
                new CategoryBrand { CategoryId = 10, BrandId = 9 },
                new CategoryBrand { CategoryId = 10, BrandId = 10 },
                new CategoryBrand { CategoryId = 10, BrandId = 11 }
            );

            // 7. Товары (теперь BrandId=1 существует)
            var items = new List<Item>();
            int itemId = -1053;

            var categoryIds = new[] {
                101, 102, 103, 104, 105, 106, 107, 108, 109, 110,
                121, 122, 123, 201, 202, 203, 204, 205
            };

            foreach (var categoryId in categoryIds)
            {
                for (int i = 1; i <= 3; i++)
                {
                    items.Add(new Item
                    {
                        Id = itemId--,
                        Name = $"Товар {i} категорії {categoryId}",
                        Desc = $"Опис товару {i} для категорії {categoryId}. Це якісний товар від відомого бренду.",
                        Price = 1000 + (i * 500),
                        Garantia = 12,
                        IsAvailable = true,
                        CategoryId = categoryId,
                        BrandId = 1, // Apple
                        UserId = 1, // Admin
                        ImageUrl = "/images/items/default.jpg"
                    });
                }
            }

            modelBuilder.Entity<Item>().HasData(items);

            // 8. Добавление фильтров категорий (минимум 2 специфических на каждую категорию)
            var allCategoryIds = new List<int>();

            // Верхние категории (1-19)
            allCategoryIds.AddRange(Enumerable.Range(1, 19));

            // Подкатегории (диапазоны из ранее добавленных данных)
            allCategoryIds.AddRange(Enumerable.Range(101, 29));   // 101–129
            allCategoryIds.AddRange(Enumerable.Range(201, 21));   // 201–221
            allCategoryIds.AddRange(Enumerable.Range(301, 15));   // 301–315
            allCategoryIds.AddRange(Enumerable.Range(401, 16));   // 401–416
            allCategoryIds.AddRange(Enumerable.Range(501, 12));   // 501–512
            allCategoryIds.AddRange(Enumerable.Range(601, 10));   // 601–610
            allCategoryIds.AddRange(Enumerable.Range(701, 11));   // 701–711
            allCategoryIds.AddRange(Enumerable.Range(801, 9));    // 801–809
            allCategoryIds.AddRange(Enumerable.Range(901, 9));    // 901–909
            allCategoryIds.AddRange(Enumerable.Range(1001, 13));  // 1001–1013
            allCategoryIds.AddRange(Enumerable.Range(1101, 9));   // 1101–1109
            allCategoryIds.AddRange(Enumerable.Range(1201, 10));  // 1201–1210
            allCategoryIds.AddRange(Enumerable.Range(1301, 9));   // 1301–1309
            allCategoryIds.AddRange(Enumerable.Range(1401, 7));   // 1401–1407
            allCategoryIds.AddRange(Enumerable.Range(1501, 7));   // 1501–1507
            allCategoryIds.AddRange(Enumerable.Range(1601, 6));   // 1601–1606
            allCategoryIds.AddRange(Enumerable.Range(1701, 6));   // 1701–1706
            allCategoryIds.AddRange(Enumerable.Range(1801, 2));   // 1801–1802
            allCategoryIds.AddRange(Enumerable.Range(1901, 3));   // 1901–1903

            int filterId = -2000;
            var categoryFilters = new List<CategoryFilter>();

            foreach (var catId in allCategoryIds)
            {
                string group = GetCategoryGroup(catId);
                var filtersForGroup = GetFiltersForGroup(group, ref filterId, catId);
                categoryFilters.AddRange(filtersForGroup);
            }

            modelBuilder.Entity<CategoryFilter>().HasData(categoryFilters);

            // После добавления товаров добавьте характеристики
            var characteristics = new List<ItemCharacteristic>();
            int charId = -3000;

            // Для товаров в категории ноутбуков (101-110)
            for (int i = 101; i <= 110; i++)
            {
                var itemsInCat = items.Where(item => item.CategoryId == i).ToList();
                foreach (var item in itemsInCat)
                {
                    characteristics.Add(new ItemCharacteristic
                    {
                        Id = charId--,
                        ItemId = item.Id,
                        Key = "processor",
                        Value = i % 2 == 0 ? "Intel Core i5" : "Intel Core i7"
                    });

                    characteristics.Add(new ItemCharacteristic
                    {
                        Id = charId--,
                        ItemId = item.Id,
                        Key = "ram",
                        Value = i % 2 == 0 ? "8GB" : "16GB"
                    });

                    characteristics.Add(new ItemCharacteristic
                    {
                        Id = charId--,
                        ItemId = item.Id,
                        Key = "storage",
                        Value = i % 2 == 0 ? "256GB SSD" : "512GB SSD"
                    });
                }
            }

            // Для товаров в категории смартфонов (201-205)
            for (int i = 201; i <= 205; i++)
            {
                var itemsInCat = items.Where(item => item.CategoryId == i).ToList();
                foreach (var item in itemsInCat)
                {
                    characteristics.Add(new ItemCharacteristic
                    {
                        Id = charId--,
                        ItemId = item.Id,
                        Key = "screen_size",
                        Value = i % 2 == 0 ? "6.1\"" : "6.7\""
                    });

                    characteristics.Add(new ItemCharacteristic
                    {
                        Id = charId--,
                        ItemId = item.Id,
                        Key = "color",
                        Value = i % 2 == 0 ? "Black" : "Silver"
                    });

                    characteristics.Add(new ItemCharacteristic
                    {
                        Id = charId--,
                        ItemId = item.Id,
                        Key = "memory",
                        Value = i % 2 == 0 ? "128GB" : "256GB"
                    });
                }
            }

            modelBuilder.Entity<ItemCharacteristic>().HasData(characteristics);
        }

        // Вспомогательный метод для определения группы категории по её ID
        private string GetCategoryGroup(int categoryId)
        {
            if (categoryId == 1 || (categoryId >= 101 && categoryId <= 129))
                return "computers";
            if (categoryId == 2 || (categoryId >= 201 && categoryId <= 221))
                return "electronics";
            if (categoryId == 3 || (categoryId >= 301 && categoryId <= 315))
                return "gaming";
            if (categoryId == 4 || (categoryId >= 401 && categoryId <= 416))
                return "appliances";
            if (categoryId == 5 || (categoryId >= 501 && categoryId <= 512))
                return "home";
            if (categoryId == 6 || (categoryId >= 601 && categoryId <= 610))
                return "tools";
            if (categoryId == 7 || (categoryId >= 701 && categoryId <= 711))
                return "plumbing";
            if (categoryId == 8 || (categoryId >= 801 && categoryId <= 809))
                return "garden";
            if (categoryId == 9 || (categoryId >= 901 && categoryId <= 909))
                return "sports";
            if (categoryId == 10 || (categoryId >= 1001 && categoryId <= 1013))
                return "clothing";
            if (categoryId == 11 || (categoryId >= 1101 && categoryId <= 1109))
                return "beauty";
            if (categoryId == 12 || (categoryId >= 1201 && categoryId <= 1210))
                return "kids";
            if (categoryId == 13 || (categoryId >= 1301 && categoryId <= 1309))
                return "pets";
            if (categoryId == 14 || (categoryId >= 1401 && categoryId <= 1407))
                return "stationery";
            if (categoryId == 15 || (categoryId >= 1501 && categoryId <= 1507))
                return "food";
            if (categoryId == 16 || (categoryId >= 1601 && categoryId <= 1606))
                return "business";
            if (categoryId == 17 || (categoryId >= 1701 && categoryId <= 1706))
                return "tourism";
            if (categoryId == 18 || (categoryId >= 1801 && categoryId <= 1802))
                return "promo";
            if (categoryId == 19 || (categoryId >= 1901 && categoryId <= 1903))
                return "sale";

            return "default"; // на всякий случай
        }

        // Вспомогательный метод для получения списка фильтров по группе
        private List<CategoryFilter> GetFiltersForGroup(string group, ref int filterId, int categoryId)
        {
            var filters = new List<CategoryFilter>();

            switch (group)
            {
                case "computers":
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "processor", DisplayName = "Тип процесора", ValueType = FilterValueType.String });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "ram", DisplayName = "Об'єм оперативної пам'яті", ValueType = FilterValueType.Number });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "storage", DisplayName = "Об'єм накопичувача", ValueType = FilterValueType.Number });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "graphics", DisplayName = "Тип відеокарти", ValueType = FilterValueType.String });
                    break;

                case "electronics":
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "screen_size", DisplayName = "Діагональ екрану", ValueType = FilterValueType.Number });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "color", DisplayName = "Колір", ValueType = FilterValueType.String });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "memory", DisplayName = "Вбудована пам'ять", ValueType = FilterValueType.Number });
                    break;

                case "gaming":
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "platform", DisplayName = "Платформа", ValueType = FilterValueType.String });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "genre", DisplayName = "Жанр", ValueType = FilterValueType.String });
                    break;

                case "appliances":
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "energy_class", DisplayName = "Клас енергоспоживання", ValueType = FilterValueType.String });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "color", DisplayName = "Колір", ValueType = FilterValueType.String });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "type", DisplayName = "Тип", ValueType = FilterValueType.String });
                    break;

                case "home":
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "material", DisplayName = "Матеріал", ValueType = FilterValueType.String });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "color", DisplayName = "Колір", ValueType = FilterValueType.String });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "size", DisplayName = "Розмір", ValueType = FilterValueType.String });
                    break;

                case "tools":
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "power", DisplayName = "Потужність", ValueType = FilterValueType.Number });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "type", DisplayName = "Тип інструменту", ValueType = FilterValueType.String });
                    break;

                case "plumbing":
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "material", DisplayName = "Матеріал", ValueType = FilterValueType.String });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "size", DisplayName = "Розмір", ValueType = FilterValueType.String });
                    break;

                case "garden":
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "type", DisplayName = "Тип", ValueType = FilterValueType.String });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "material", DisplayName = "Матеріал", ValueType = FilterValueType.String });
                    break;

                case "sports":
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "sport_type", DisplayName = "Вид спорту", ValueType = FilterValueType.String });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "size", DisplayName = "Розмір", ValueType = FilterValueType.String });
                    break;

                case "clothing":
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "size", DisplayName = "Розмір", ValueType = FilterValueType.String });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "color", DisplayName = "Колір", ValueType = FilterValueType.String });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "material", DisplayName = "Матеріал", ValueType = FilterValueType.String });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "gender", DisplayName = "Стать", ValueType = FilterValueType.String });
                    break;

                case "beauty":
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "type", DisplayName = "Тип", ValueType = FilterValueType.String });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "for_whom", DisplayName = "Для кого", ValueType = FilterValueType.String });
                    break;

                case "kids":
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "age", DisplayName = "Вік", ValueType = FilterValueType.Number });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "type", DisplayName = "Тип", ValueType = FilterValueType.String });
                    break;

                case "pets":
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "animal_type", DisplayName = "Тип тварини", ValueType = FilterValueType.String });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "product_type", DisplayName = "Вид товару", ValueType = FilterValueType.String });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "animal_age", DisplayName = "Вік тварини", ValueType = FilterValueType.String });
                    break;

                case "stationery":
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "type", DisplayName = "Тип", ValueType = FilterValueType.String });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "format", DisplayName = "Формат", ValueType = FilterValueType.String });
                    break;

                case "food":
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "type", DisplayName = "Тип", ValueType = FilterValueType.String });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "volume", DisplayName = "Об'єм", ValueType = FilterValueType.Number });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "strength", DisplayName = "Міцність", ValueType = FilterValueType.Number });
                    break;

                case "business":
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "equipment_type", DisplayName = "Тип обладнання", ValueType = FilterValueType.String });
                    break;

                case "tourism":
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "type", DisplayName = "Тип", ValueType = FilterValueType.String });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "capacity", DisplayName = "Місткість", ValueType = FilterValueType.Number });
                    break;

                case "promo":
                case "sale":
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "discount_size", DisplayName = "Розмір знижки", ValueType = FilterValueType.Number });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "category", DisplayName = "Категорія товару", ValueType = FilterValueType.String });
                    break;

                default:
                    // Если группа не определена, добавим универсальные фильтры
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "brand", DisplayName = "Бренд", ValueType = FilterValueType.String });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "price", DisplayName = "Ціна", ValueType = FilterValueType.Range });
                    break;
            }

            return filters;
        }

    }
}