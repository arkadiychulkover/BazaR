using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BazaR.Models;
using Microsoft.AspNet.Identity.EntityFramework;

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
        public DbSet<VisitingModel> VisitingModels { get; set; }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<DeliveryAddress> DeliveryAddresses { get; set; }
        public DbSet<OrderRecipient> OrderRecipients { get; set; }
        public DbSet<AdditionalInfo> AdditionalInfos { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<BonusAccount> BonusAccounts { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PremiumSubscription> PremiumSubscriptions { get; set; }
        public DbSet<LookedCard> LookedCards { get; set; }
        public DbSet<MailingSetting> MailingSettings { get; set; }
        public DbSet<WalletTransaction> WalletTransactions { get; set; }

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

            modelBuilder.Entity<VisitingModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.SearchFiltersJson)
                    .HasColumnType("nvarchar(max)")
                    .HasColumnName("SearchFilters");
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
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.PaymentMethod);
                entity.Property(e => e.PaymentStatus);
                entity.Property(e => e.DeliveryMethod);
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
            // 1. Города
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

            // 2. Пользователи
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

            // 3. Бренды с правильными путями
            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, Name = "Apple", Logo = "/AssetsIconImg/images/brands/apple.png" },
                new Brand { Id = 2, Name = "Samsung", Logo = "/AssetsIconImg/images/brands/samsung.png" },
                new Brand { Id = 3, Name = "Xiaomi", Logo = "/AssetsIconImg/images/brands/xiaomi.png" },
                new Brand { Id = 4, Name = "Sony", Logo = "/AssetsIconImg/images/brands/sony.png" },
                new Brand { Id = 5, Name = "LG", Logo = "/AssetsIconImg/images/brands/lg.png" },
                new Brand { Id = 6, Name = "Bosch", Logo = "/AssetsIconImg/images/brands/bosch.png" },
                new Brand { Id = 7, Name = "Nike", Logo = "/AssetsIconImg/images/brands/nike.png" },
                new Brand { Id = 8, Name = "Adidas", Logo = "/AssetsIconImg/images/brands/adidas.png" },
                new Brand { Id = 9, Name = "Puma", Logo = "/AssetsIconImg/images/brands/puma.png" },
                new Brand { Id = 10, Name = "Zara", Logo = "/AssetsIconImg/images/brands/zara.png" },
                new Brand { Id = 11, Name = "H&M", Logo = "/AssetsIconImg/images/brands/hm.png" },
                new Brand { Id = 12, Name = "Dell", Logo = "/AssetsIconImg/images/brands/dell.png" },
                new Brand { Id = 13, Name = "HP", Logo = "/AssetsIconImg/images/brands/hp.png" },
                new Brand { Id = 14, Name = "Lenovo", Logo = "/AssetsIconImg/images/brands/lenovo.png" },
                new Brand { Id = 15, Name = "Asus", Logo = "/AssetsIconImg/images/brands/asus.png" },
                new Brand { Id = 16, Name = "Acer", Logo = "/AssetsIconImg/images/brands/acer.png" },
                new Brand { Id = 17, Name = "Microsoft", Logo = "/AssetsIconImg/images/brands/microsoft.png" },
                new Brand { Id = 18, Name = "Canon", Logo = "/AssetsIconImg/images/brands/canon.png" },
                new Brand { Id = 19, Name = "Nikon", Logo = "/AssetsIconImg/images/brands/nikon.png" },
                new Brand { Id = 20, Name = "Panasonic", Logo = "/AssetsIconImg/images/brands/panasonic.png" }
            );

            // 4. Главные категории (из папки TopLevelCategory)
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Ноутбуки та комп'ютери", IconUrl = "/AssetsIconImg/TopLevelCategory/icon-laptops-and-computers.svg", ImgUrl = "/AssetsIconImg/TopLevelCategory/icon-laptops-and-computers.svg", ParentCategoryId = null, DisplayOrder = 1 },
                new Category { Id = 2, Name = "Смартфони, ТВ та електроніка", IconUrl = "/AssetsIconImg/TopLevelCategory/icon-smartphones-tv-electronics.svg", ImgUrl = "/AssetsIconImg/TopLevelCategory/icon-smartphones-tv-electronics.svg", ParentCategoryId = null, DisplayOrder = 2 },
                new Category { Id = 3, Name = "Товари для геймерів", IconUrl = "/AssetsIconImg/TopLevelCategory/icon-gaming.svg", ImgUrl = "/AssetsIconImg/TopLevelCategory/icon-gaming.svg", ParentCategoryId = null, DisplayOrder = 3 },
                new Category { Id = 4, Name = "Побутова техніка", IconUrl = "/AssetsIconImg/TopLevelCategory/icon-home-appliances.svg", ImgUrl = "/AssetsIconImg/TopLevelCategory/icon-home-appliances.svg", ParentCategoryId = null, DisplayOrder = 4 },
                new Category { Id = 5, Name = "Товари для дому", IconUrl = "/AssetsIconImg/TopLevelCategory/icon-home-goods.svg", ImgUrl = "/AssetsIconImg/TopLevelCategory/icon-home-goods.svg", ParentCategoryId = null, DisplayOrder = 5 },
                new Category { Id = 6, Name = "Інструменти та автотовари", IconUrl = "/AssetsIconImg/TopLevelCategory/icon-tools-auto.svg", ImgUrl = "/AssetsIconImg/TopLevelCategory/icon-tools-auto.svg", ParentCategoryId = null, DisplayOrder = 6 },
                new Category { Id = 7, Name = "Сантехніка та ремонт", IconUrl = "/AssetsIconImg/TopLevelCategory/icon-plumbing-renovation.svg", ImgUrl = "/AssetsIconImg/TopLevelCategory/icon-plumbing-renovation.svg", ParentCategoryId = null, DisplayOrder = 7 },
                new Category { Id = 8, Name = "Дача, сад та город", IconUrl = "/AssetsIconImg/TopLevelCategory/icon-garden.svg", ImgUrl = "/AssetsIconImg/TopLevelCategory/icon-garden.svg", ParentCategoryId = null, DisplayOrder = 8 },
                new Category { Id = 9, Name = "Спорт та захоплення", IconUrl = "/AssetsIconImg/TopLevelCategory/icon-sports-hobbies.svg", ImgUrl = "/AssetsIconImg/TopLevelCategory/icon-sports-hobbies.svg", ParentCategoryId = null, DisplayOrder = 9 },
                new Category { Id = 10, Name = "Одяг, взуття та прикраси", IconUrl = "/AssetsIconImg/TopLevelCategory/icon-clothing-footwear-jewelry.svg", ImgUrl = "/AssetsIconImg/TopLevelCategory/icon-clothing-footwear-jewelry.svg", ParentCategoryId = null, DisplayOrder = 10 },
                new Category { Id = 11, Name = "Краса і здоров'я", IconUrl = "/AssetsIconImg/TopLevelCategory/icon-beauty-health.svg", ImgUrl = "/AssetsIconImg/TopLevelCategory/icon-beauty-health.svg", ParentCategoryId = null, DisplayOrder = 11 },
                new Category { Id = 12, Name = "Дитячі товари", IconUrl = "/AssetsIconImg/TopLevelCategory/icon-baby-products.svg", ImgUrl = "/AssetsIconImg/TopLevelCategory/icon-baby-products.svg", ParentCategoryId = null, DisplayOrder = 12 },
                new Category { Id = 13, Name = "Зоотовари", IconUrl = "/AssetsIconImg/TopLevelCategory/icon-pet-supplies.svg", ImgUrl = "/AssetsIconImg/TopLevelCategory/icon-pet-supplies.svg", ParentCategoryId = null, DisplayOrder = 13 },
                new Category { Id = 14, Name = "Канцтовари та книги", IconUrl = "/AssetsIconImg/TopLevelCategory/icon-stationery-books.svg", ImgUrl = "/AssetsIconImg/TopLevelCategory/icon-stationery-books.svg", ParentCategoryId = null, DisplayOrder = 14 },
                new Category { Id = 15, Name = "Алкогольні напої та продукти", IconUrl = "/AssetsIconImg/TopLevelCategory/icon-alcohol-food.svg", ImgUrl = "/AssetsIconImg/TopLevelCategory/icon-alcohol-food.svg", ParentCategoryId = null, DisplayOrder = 15 },
                new Category { Id = 16, Name = "Товари для бізнесу та послуги", IconUrl = "/AssetsIconImg/TopLevelCategory/icon-business-services.svg", ImgUrl = "/AssetsIconImg/TopLevelCategory/icon-business-services.svg", ParentCategoryId = null, DisplayOrder = 16 },
                new Category { Id = 17, Name = "Туризм та відпочинок", IconUrl = "/AssetsIconImg/TopLevelCategory/icon-tourism-outdoor.svg", ImgUrl = "/AssetsIconImg/TopLevelCategory/icon-tourism-outdoor.svg", ParentCategoryId = null, DisplayOrder = 17 },
                new Category { Id = 18, Name = "Акції", IconUrl = "/AssetsIconImg/TopLevelCategory/icon-promotions.svg", ImgUrl = "/AssetsIconImg/TopLevelCategory/icon-promotions.svg", ParentCategoryId = null, DisplayOrder = 18 },
                new Category { Id = 19, Name = "Тотальний розпродаж", IconUrl = "/AssetsIconImg/TopLevelCategory/icon-total-sale.svg", ImgUrl = "/AssetsIconImg/TopLevelCategory/icon-total-sale.svg", ParentCategoryId = null, DisplayOrder = 19 }
            );

            // 5. Подкатегории с правильными путями и расширениями

            // Категория 1 - LaptopsAndComputers
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 101, Name = "Ноутбуки", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Notebooks.svg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Notebooks.svg", ParentCategoryId = 1, DisplayOrder = 1 },
                new Category { Id = 102, Name = "Ігрові ноутбуки", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-GamingNotebooks.svg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-GamingNotebooks.svg", ParentCategoryId = 1, DisplayOrder = 2 },
                new Category { Id = 103, Name = "Ультрабуки", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Ultrabooks.svg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Ultrabooks.svg", ParentCategoryId = 1, DisplayOrder = 3 },
                new Category { Id = 104, Name = "Для навчання", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-ForStudy.svg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-ForStudy.svg", ParentCategoryId = 1, DisplayOrder = 4 },
                new Category { Id = 105, Name = "Для роботи", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-ForWork.svg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-ForWork.svg", ParentCategoryId = 1, DisplayOrder = 5 },
                new Category { Id = 106, Name = "Chromebook", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Chromebook.svg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Chromebook.svg", ParentCategoryId = 1, DisplayOrder = 6 },
                new Category { Id = 107, Name = "Комп'ютери", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Computers.svg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Computers.svg", ParentCategoryId = 1, DisplayOrder = 7 },
                new Category { Id = 108, Name = "Настільні ПК", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-DesktopPC.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-DesktopPC.webp", ParentCategoryId = 1, DisplayOrder = 8 },
                new Category { Id = 109, Name = "Ігрові ПК", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-GamingPC.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-GamingPC.webp", ParentCategoryId = 1, DisplayOrder = 9 },
                new Category { Id = 110, Name = "Міні-ПК", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-MiniPC.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-MiniPC.jpg", ParentCategoryId = 1, DisplayOrder = 10 },
                new Category { Id = 111, Name = "Моноблоки", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Monoblocks.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Monoblocks.webp", ParentCategoryId = 1, DisplayOrder = 11 },
                new Category { Id = 112, Name = "Робочі станції", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Workstations.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Workstations.jpg", ParentCategoryId = 1, DisplayOrder = 12 },
                new Category { Id = 113, Name = "Комплектуючі", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Components.svg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Components.svg", ParentCategoryId = 1, DisplayOrder = 13 },
                new Category { Id = 114, Name = "Процесори", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Processors.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Processors.webp", ParentCategoryId = 1, DisplayOrder = 14 },
                new Category { Id = 115, Name = "Відеокарти", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-VideoCards.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-VideoCards.webp", ParentCategoryId = 1, DisplayOrder = 15 },
                new Category { Id = 116, Name = "Материнські плати", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Motherboards.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Motherboards.webp", ParentCategoryId = 1, DisplayOrder = 16 },
                new Category { Id = 117, Name = "Оперативна пам'ять", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-RAM.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-RAM.webp", ParentCategoryId = 1, DisplayOrder = 17 },
                new Category { Id = 118, Name = "Блоки живлення", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-PowerSupplies.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-PowerSupplies.webp", ParentCategoryId = 1, DisplayOrder = 18 },
                new Category { Id = 119, Name = "Корпуси", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Cases.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Cases.webp", ParentCategoryId = 1, DisplayOrder = 19 },
                new Category { Id = 120, Name = "Накопичувачі", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Storage.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Storage.webp", ParentCategoryId = 1, DisplayOrder = 20 },
                new Category { Id = 121, Name = "SSD", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-SSD.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-SSD.webp", ParentCategoryId = 1, DisplayOrder = 21 },
                new Category { Id = 122, Name = "HDD", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-HDD.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-HDD.webp", ParentCategoryId = 1, DisplayOrder = 22 },
                new Category { Id = 123, Name = "Зовнішні диски", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-ExternalDrives.jfif", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-ExternalDrives.jfif", ParentCategoryId = 1, DisplayOrder = 23 },
                new Category { Id = 124, Name = "NAS", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-NAS.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-NAS.webp", ParentCategoryId = 1, DisplayOrder = 24 },
                new Category { Id = 125, Name = "Периферія", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Peripherals.svg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Peripherals.svg", ParentCategoryId = 1, DisplayOrder = 25 },
                new Category { Id = 126, Name = "Клавіатури", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Keyboards.svg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Keyboards.svg", ParentCategoryId = 1, DisplayOrder = 26 },
                new Category { Id = 127, Name = "Миші", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Mice.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Mice.webp", ParentCategoryId = 1, DisplayOrder = 27 },
                new Category { Id = 128, Name = "Килимки", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-MousePads.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-MousePads.webp", ParentCategoryId = 1, DisplayOrder = 28 },
                new Category { Id = 129, Name = "Вебкамери", IconUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Webcams.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Webcams.webp", ParentCategoryId = 1, DisplayOrder = 29 }
            );

            // Категория 2 - SmartphonesTvElectronics
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 201, Name = "Смартфони", IconUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Smartphones.svg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Smartphones.svg", ParentCategoryId = 2, DisplayOrder = 1 },
                new Category { Id = 202, Name = "Android", IconUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Android.svg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Android.svg", ParentCategoryId = 2, DisplayOrder = 2 },
                new Category { Id = 203, Name = "iPhone", IconUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-iPhone.svg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-iPhone.svg", ParentCategoryId = 2, DisplayOrder = 3 },
                new Category { Id = 204, Name = "Бюджетні", IconUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Budget.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Budget.webp", ParentCategoryId = 2, DisplayOrder = 4 },
                new Category { Id = 205, Name = "Флагмани", IconUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Flagship.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Flagship.webp", ParentCategoryId = 2, DisplayOrder = 5 },
                new Category { Id = 206, Name = "Телевізори", IconUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-TVs.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-TVs.jpg", ParentCategoryId = 2, DisplayOrder = 6 },
                new Category { Id = 207, Name = "Smart TV", IconUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-SmartTV.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-SmartTV.webp", ParentCategoryId = 2, DisplayOrder = 7 },
                new Category { Id = 208, Name = "LED", IconUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-LED.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-LED.webp", ParentCategoryId = 2, DisplayOrder = 8 },
                new Category { Id = 209, Name = "OLED", IconUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-OLED.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-OLED.webp", ParentCategoryId = 2, DisplayOrder = 9 },
                new Category { Id = 210, Name = "QLED", IconUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-QLED.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-QLED.webp", ParentCategoryId = 2, DisplayOrder = 10 },
                new Category { Id = 211, Name = "Аудіо", IconUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Audio.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Audio.jpg", ParentCategoryId = 2, DisplayOrder = 11 },
                new Category { Id = 212, Name = "Навушники", IconUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Headphones.png", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Headphones.png", ParentCategoryId = 2, DisplayOrder = 12 },
                new Category { Id = 213, Name = "Саундбари", IconUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Soundbars.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Soundbars.webp", ParentCategoryId = 2, DisplayOrder = 13 },
                new Category { Id = 214, Name = "Колонки", IconUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Speakers.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Speakers.webp", ParentCategoryId = 2, DisplayOrder = 14 },
                new Category { Id = 215, Name = "Домашні кінотеатри", IconUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-HomeTheaters.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-HomeTheaters.jpg", ParentCategoryId = 2, DisplayOrder = 15 },
                new Category { Id = 216, Name = "Планшети", IconUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Tablets.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Tablets.webp", ParentCategoryId = 2, DisplayOrder = 16 },
                new Category { Id = 217, Name = "iPad", IconUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-iPad.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-iPad.webp", ParentCategoryId = 2, DisplayOrder = 17 },
                new Category { Id = 218, Name = "Android планшети", IconUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-AndroidTablets.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-AndroidTablets.webp", ParentCategoryId = 2, DisplayOrder = 18 },
                new Category { Id = 219, Name = "Гаджети", IconUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Gadgets.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Gadgets.webp", ParentCategoryId = 2, DisplayOrder = 19 },
                new Category { Id = 220, Name = "Смарт-годинники", IconUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-SmartWatches.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-SmartWatches.webp", ParentCategoryId = 2, DisplayOrder = 20 },
                new Category { Id = 221, Name = "Фітнес-браслети", IconUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-FitnessBands.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-FitnessBands.webp", ParentCategoryId = 2, DisplayOrder = 21 }
            );

            // Категория 3 - Gaming
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 301, Name = "Консолі", IconUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-Consoles.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-Consoles.jpg", ParentCategoryId = 3, DisplayOrder = 1 },
                new Category { Id = 302, Name = "PlayStation", IconUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-PlayStation.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-PlayStation.webp", ParentCategoryId = 3, DisplayOrder = 2 },
                new Category { Id = 303, Name = "Xbox", IconUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-Xbox.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-Xbox.webp", ParentCategoryId = 3, DisplayOrder = 3 },
                new Category { Id = 304, Name = "Nintendo", IconUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-Nintendo.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-Nintendo.webp", ParentCategoryId = 3, DisplayOrder = 4 },
                new Category { Id = 305, Name = "Ігри", IconUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-Games.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-Games.webp", ParentCategoryId = 3, DisplayOrder = 5 },
                new Category { Id = 306, Name = "PlayStation ігри", IconUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-PlayStationGames.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-PlayStationGames.webp", ParentCategoryId = 3, DisplayOrder = 6 },
                new Category { Id = 307, Name = "Xbox ігри", IconUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-XboxGames.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-XboxGames.webp", ParentCategoryId = 3, DisplayOrder = 7 },
                new Category { Id = 308, Name = "PC ігри", IconUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-PCGames.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-PCGames.webp", ParentCategoryId = 3, DisplayOrder = 8 },
                new Category { Id = 309, Name = "Геймерська периферія", IconUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-GamingPeripherals.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-GamingPeripherals.jpg", ParentCategoryId = 3, DisplayOrder = 9 },
                new Category { Id = 310, Name = "Ігрові миші", IconUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-GamingMice.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-GamingMice.webp", ParentCategoryId = 3, DisplayOrder = 10 },
                new Category { Id = 311, Name = "Ігрові клавіатури", IconUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-GamingKeyboards.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-GamingKeyboards.webp", ParentCategoryId = 3, DisplayOrder = 11 },
                new Category { Id = 312, Name = "Геймерські навушники", IconUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-GamingHeadphones.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-GamingHeadphones.webp", ParentCategoryId = 3, DisplayOrder = 12 },
                new Category { Id = 313, Name = "VR", IconUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-VR.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-VR.jpg", ParentCategoryId = 3, DisplayOrder = 13 },
                new Category { Id = 314, Name = "VR шоломи", IconUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-VRHeadsets.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-VRHeadsets.webp", ParentCategoryId = 3, DisplayOrder = 14 },
                new Category { Id = 315, Name = "VR аксесуари", IconUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-VRAccessories.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-VRAccessories.webp", ParentCategoryId = 3, DisplayOrder = 15 }
            );

            // Категория 4 - HomeAppliances
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 401, Name = "Велика техніка", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-LargeAppliances.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-LargeAppliances.webp", ParentCategoryId = 4, DisplayOrder = 1 },
                new Category { Id = 402, Name = "Холодильники", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Refrigerators.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Refrigerators.webp", ParentCategoryId = 4, DisplayOrder = 2 },
                new Category { Id = 403, Name = "Пральні машини", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-WashingMachines.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-WashingMachines.webp", ParentCategoryId = 4, DisplayOrder = 3 },
                new Category { Id = 404, Name = "Посудомийні машини", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Dishwashers.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Dishwashers.webp", ParentCategoryId = 4, DisplayOrder = 4 },
                new Category { Id = 405, Name = "Кухонна техніка", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-KitchenAppliances.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-KitchenAppliances.webp", ParentCategoryId = 4, DisplayOrder = 5 },
                new Category { Id = 406, Name = "Мікрохвильові печі", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Microwaves.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Microwaves.webp", ParentCategoryId = 4, DisplayOrder = 6 },
                new Category { Id = 407, Name = "Блендери", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Blenders.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Blenders.webp", ParentCategoryId = 4, DisplayOrder = 7 },
                new Category { Id = 408, Name = "Міксери", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Mixers.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Mixers.webp", ParentCategoryId = 4, DisplayOrder = 8 },
                new Category { Id = 409, Name = "Мультиварки", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Multicookers.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Multicookers.webp", ParentCategoryId = 4, DisplayOrder = 9 },
                new Category { Id = 410, Name = "Кліматична техніка", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-ClimateControl.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-ClimateControl.webp", ParentCategoryId = 4, DisplayOrder = 10 },
                new Category { Id = 411, Name = "Кондиціонери", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-AirConditioners.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-AirConditioners.webp", ParentCategoryId = 4, DisplayOrder = 11 },
                new Category { Id = 412, Name = "Обігрівачі", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Heaters.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Heaters.webp", ParentCategoryId = 4, DisplayOrder = 12 },
                new Category { Id = 413, Name = "Вентилятори", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Fans.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Fans.webp", ParentCategoryId = 4, DisplayOrder = 13 },
                new Category { Id = 414, Name = "Прибирання", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Cleaning.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Cleaning.webp", ParentCategoryId = 4, DisplayOrder = 14 },
                new Category { Id = 415, Name = "Пилососи", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-VacuumCleaners.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-VacuumCleaners.webp", ParentCategoryId = 4, DisplayOrder = 15 },
                new Category { Id = 416, Name = "Роботи-пилососи", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-RobotVacuums.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-RobotVacuums.webp", ParentCategoryId = 4, DisplayOrder = 16 }
            );

            // Категория 5 - HomeGoods
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 501, Name = "Меблі", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Furniture.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Furniture.jpg", ParentCategoryId = 5, DisplayOrder = 1 },
                new Category { Id = 502, Name = "Дивани", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Sofas.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Sofas.webp", ParentCategoryId = 5, DisplayOrder = 2 },
                new Category { Id = 503, Name = "Ліжка", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Beds.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Beds.webp", ParentCategoryId = 5, DisplayOrder = 3 },
                new Category { Id = 504, Name = "Шафи", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Wardrobes.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Wardrobes.webp", ParentCategoryId = 5, DisplayOrder = 4 },
                new Category { Id = 505, Name = "Освітлення", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Lighting.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Lighting.jpg", ParentCategoryId = 5, DisplayOrder = 5 },
                new Category { Id = 506, Name = "Лампи", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Lamps.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Lamps.webp", ParentCategoryId = 5, DisplayOrder = 6 },
                new Category { Id = 507, Name = "Люстри", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Chandeliers.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Chandeliers.webp", ParentCategoryId = 5, DisplayOrder = 7 },
                new Category { Id = 508, Name = "LED освітлення", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-LEDLighting.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-LEDLighting.webp", ParentCategoryId = 5, DisplayOrder = 8 },
                new Category { Id = 509, Name = "Декор", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Decor.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Decor.jpg", ParentCategoryId = 5, DisplayOrder = 9 },
                new Category { Id = 510, Name = "Картини", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Paintings.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Paintings.webp", ParentCategoryId = 5, DisplayOrder = 10 },
                new Category { Id = 511, Name = "Дзеркала", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Mirrors.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Mirrors.webp", ParentCategoryId = 5, DisplayOrder = 11 },
                new Category { Id = 512, Name = "Годинники", IconUrl = "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Clocks.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Clocks.webp", ParentCategoryId = 5, DisplayOrder = 12 }
            );

            // Категория 6 - ToolsAndAuto
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 601, Name = "Електроінструменти", IconUrl = "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-PowerTools.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-PowerTools.jpg", ParentCategoryId = 6, DisplayOrder = 1 },
                new Category { Id = 602, Name = "Дрилі", IconUrl = "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-Drills.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-Drills.webp", ParentCategoryId = 6, DisplayOrder = 2 },
                new Category { Id = 603, Name = "Шуруповерти", IconUrl = "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-Screwdrivers.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-Screwdrivers.webp", ParentCategoryId = 6, DisplayOrder = 3 },
                new Category { Id = 604, Name = "Болгарки", IconUrl = "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-AngleGrinders.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-AngleGrinders.webp", ParentCategoryId = 6, DisplayOrder = 4 },
                new Category { Id = 605, Name = "Автоелектроніка", IconUrl = "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-CarElectronics.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-CarElectronics.webp", ParentCategoryId = 6, DisplayOrder = 5 },
                new Category { Id = 606, Name = "Відеореєстратори", IconUrl = "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-DashCams.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-DashCams.webp", ParentCategoryId = 6, DisplayOrder = 6 },
                new Category { Id = 607, Name = "GPS навігатори", IconUrl = "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-GPS.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-GPS.webp", ParentCategoryId = 6, DisplayOrder = 7 },
                new Category { Id = 608, Name = "Автоаксесуари", IconUrl = "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-CarAccessories.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-CarAccessories.jpg", ParentCategoryId = 6, DisplayOrder = 8 },
                new Category { Id = 609, Name = "Тримачі телефону", IconUrl = "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-PhoneHolders.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-PhoneHolders.webp", ParentCategoryId = 6, DisplayOrder = 9 },
                new Category { Id = 610, Name = "Зарядні пристрої", IconUrl = "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-CarChargers.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-CarChargers.webp", ParentCategoryId = 6, DisplayOrder = 10 }
            );

            // Категория 7 - PlumbingRenovation
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 701, Name = "Ванна кімната", IconUrl = "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Bathroom.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Bathroom.webp", ParentCategoryId = 7, DisplayOrder = 1 },
                new Category { Id = 702, Name = "Душові кабіни", IconUrl = "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-ShowerCubicles.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-ShowerCubicles.webp", ParentCategoryId = 7, DisplayOrder = 2 },
                new Category { Id = 703, Name = "Унітази", IconUrl = "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Toilets.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Toilets.webp", ParentCategoryId = 7, DisplayOrder = 3 },
                new Category { Id = 704, Name = "Раковини", IconUrl = "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Sinks.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Sinks.webp", ParentCategoryId = 7, DisplayOrder = 4 },
                new Category { Id = 705, Name = "Інструменти", IconUrl = "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Tools.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Tools.webp", ParentCategoryId = 7, DisplayOrder = 5 },
                new Category { Id = 706, Name = "Ручний інструмент", IconUrl = "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-HandTools.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-HandTools.jpg", ParentCategoryId = 7, DisplayOrder = 6 },
                new Category { Id = 707, Name = "Вимірювальні прилади", IconUrl = "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-MeasuringTools.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-MeasuringTools.jpg", ParentCategoryId = 7, DisplayOrder = 7 },
                new Category { Id = 708, Name = "Матеріали", IconUrl = "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Materials.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Materials.webp", ParentCategoryId = 7, DisplayOrder = 8 },
                new Category { Id = 709, Name = "Фарба", IconUrl = "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Paint.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Paint.webp", ParentCategoryId = 7, DisplayOrder = 9 },
                new Category { Id = 710, Name = "Плитка", IconUrl = "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Tiles.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Tiles.webp", ParentCategoryId = 7, DisplayOrder = 10 },
                new Category { Id = 711, Name = "Ламінат", IconUrl = "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Laminate.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Laminate.webp", ParentCategoryId = 7, DisplayOrder = 11 }
            );

            // Категория 8 - Garden
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 801, Name = "Садова техніка", IconUrl = "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-GardenTools.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-GardenTools.jpg", ParentCategoryId = 8, DisplayOrder = 1 },
                new Category { Id = 802, Name = "Газонокосарки", IconUrl = "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-LawnMowers.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-LawnMowers.webp", ParentCategoryId = 8, DisplayOrder = 2 },
                new Category { Id = 803, Name = "Тримери", IconUrl = "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-Trimmers.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-Trimmers.webp", ParentCategoryId = 8, DisplayOrder = 3 },
                new Category { Id = 804, Name = "Садові інструменти", IconUrl = "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-GardenImplements.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-GardenImplements.webp", ParentCategoryId = 8, DisplayOrder = 4 },
                new Category { Id = 805, Name = "Лопати", IconUrl = "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-Shovels.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-Shovels.webp", ParentCategoryId = 8, DisplayOrder = 5 },
                new Category { Id = 806, Name = "Секатори", IconUrl = "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-Pruners.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-Pruners.webp", ParentCategoryId = 8, DisplayOrder = 6 },
                new Category { Id = 807, Name = "Меблі для саду", IconUrl = "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-GardenFurniture.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-GardenFurniture.jpg", ParentCategoryId = 8, DisplayOrder = 7 },
                new Category { Id = 808, Name = "Садові столи", IconUrl = "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-GardenTables.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-GardenTables.webp", ParentCategoryId = 8, DisplayOrder = 8 },
                new Category { Id = 809, Name = "Крісла", IconUrl = "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-GardenChairs.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-GardenChairs.webp", ParentCategoryId = 8, DisplayOrder = 9 }
            );

            // Категория 9 - SportsHobbies
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 901, Name = "Фітнес", IconUrl = "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-Fitness.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-Fitness.webp", ParentCategoryId = 9, DisplayOrder = 1 },
                new Category { Id = 902, Name = "Гантелі", IconUrl = "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-Dumbbells.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-Dumbbells.webp", ParentCategoryId = 9, DisplayOrder = 2 },
                new Category { Id = 903, Name = "Бігові доріжки", IconUrl = "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-Treadmills.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-Treadmills.webp", ParentCategoryId = 9, DisplayOrder = 3 },
                new Category { Id = 904, Name = "Велоспорт", IconUrl = "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-Cycling.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-Cycling.webp", ParentCategoryId = 9, DisplayOrder = 4 },
                new Category { Id = 905, Name = "Велосипеди", IconUrl = "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-Bicycles.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-Bicycles.webp", ParentCategoryId = 9, DisplayOrder = 5 },
                new Category { Id = 906, Name = "Аксесуари", IconUrl = "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-CyclingAccessories.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-CyclingAccessories.webp", ParentCategoryId = 9, DisplayOrder = 6 },
                new Category { Id = 907, Name = "Активний відпочинок", IconUrl = "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-OutdoorRecreation.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-OutdoorRecreation.webp", ParentCategoryId = 9, DisplayOrder = 7 },
                new Category { Id = 908, Name = "Самокати", IconUrl = "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-KickScooters.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-KickScooters.webp", ParentCategoryId = 9, DisplayOrder = 8 },
                new Category { Id = 909, Name = "Електросамокати", IconUrl = "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-ElectricScooters.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-ElectricScooters.webp", ParentCategoryId = 9, DisplayOrder = 9 }
            );

            // Категория 10 - ClothingFootwearJewelry
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1001, Name = "Чоловічий одяг", IconUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-MensClothing.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-MensClothing.jpg", ParentCategoryId = 10, DisplayOrder = 1 },
                new Category { Id = 1002, Name = "Футболки", IconUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-TShirts.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-TShirts.jpg", ParentCategoryId = 10, DisplayOrder = 2 },
                new Category { Id = 1003, Name = "Джинси", IconUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Jeans.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Jeans.webp", ParentCategoryId = 10, DisplayOrder = 3 },
                new Category { Id = 1004, Name = "Куртки", IconUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Jackets.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Jackets.webp", ParentCategoryId = 10, DisplayOrder = 4 },
                new Category { Id = 1005, Name = "Жіночий одяг", IconUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-WomensClothing.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-WomensClothing.jpg", ParentCategoryId = 10, DisplayOrder = 5 },
                new Category { Id = 1006, Name = "Сукні", IconUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Dresses.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Dresses.webp", ParentCategoryId = 10, DisplayOrder = 6 },
                new Category { Id = 1007, Name = "Спідниці", IconUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Skirts.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Skirts.webp", ParentCategoryId = 10, DisplayOrder = 7 },
                new Category { Id = 1008, Name = "Взуття", IconUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Footwear.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Footwear.jpg", ParentCategoryId = 10, DisplayOrder = 8 },
                new Category { Id = 1009, Name = "Кросівки", IconUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Sneakers.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Sneakers.webp", ParentCategoryId = 10, DisplayOrder = 9 },
                new Category { Id = 1010, Name = "Черевики", IconUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Boots.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Boots.webp", ParentCategoryId = 10, DisplayOrder = 10 },
                new Category { Id = 1011, Name = "Аксесуари", IconUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Accessories.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Accessories.webp", ParentCategoryId = 10, DisplayOrder = 11 },
                new Category { Id = 1012, Name = "Сумки", IconUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Bags.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Bags.webp", ParentCategoryId = 10, DisplayOrder = 12 },
                new Category { Id = 1013, Name = "Ремені", IconUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Belts.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Belts.webp", ParentCategoryId = 10, DisplayOrder = 13 }
            );

            // Категория 11 - BeautyHealth
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1101, Name = "Догляд за обличчям", IconUrl = "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-FaceCare.png", ImgUrl = "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-FaceCare.png", ParentCategoryId = 11, DisplayOrder = 1 },
                new Category { Id = 1102, Name = "Креми", IconUrl = "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-Creams.png", ImgUrl = "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-Creams.png", ParentCategoryId = 11, DisplayOrder = 2 },
                new Category { Id = 1103, Name = "Сироватки", IconUrl = "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-Serums.png", ImgUrl = "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-Serums.png", ParentCategoryId = 11, DisplayOrder = 3 },
                new Category { Id = 1104, Name = "Догляд за волоссям", IconUrl = "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-HairCare.png", ImgUrl = "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-HairCare.png", ParentCategoryId = 11, DisplayOrder = 4 },
                new Category { Id = 1105, Name = "Шампуні", IconUrl = "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-Shampoos.png", ImgUrl = "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-Shampoos.png", ParentCategoryId = 11, DisplayOrder = 5 },
                new Category { Id = 1106, Name = "Маски", IconUrl = "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-HairMasks.png", ImgUrl = "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-HairMasks.png", ParentCategoryId = 11, DisplayOrder = 6 },
                new Category { Id = 1107, Name = "Техніка", IconUrl = "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-BeautyTech.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-BeautyTech.jpg", ParentCategoryId = 11, DisplayOrder = 7 },
                new Category { Id = 1108, Name = "Фени", IconUrl = "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-HairDryers.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-HairDryers.jpg", ParentCategoryId = 11, DisplayOrder = 8 },
                new Category { Id = 1109, Name = "Бритви", IconUrl = "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-Razors.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-Razors.jpg", ParentCategoryId = 11, DisplayOrder = 9 }
            );

            // Категория 12 - BabyProducts
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1201, Name = "Іграшки", IconUrl = "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Toys.png", ImgUrl = "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Toys.png", ParentCategoryId = 12, DisplayOrder = 1 },
                new Category { Id = 1202, Name = "Конструктори", IconUrl = "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-ConstructionToys.png", ImgUrl = "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-ConstructionToys.png", ParentCategoryId = 12, DisplayOrder = 2 },
                new Category { Id = 1203, Name = "Ляльки", IconUrl = "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Dolls.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Dolls.jpg", ParentCategoryId = 12, DisplayOrder = 3 },
                new Category { Id = 1204, Name = "Машинки", IconUrl = "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Cars.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Cars.jpg", ParentCategoryId = 12, DisplayOrder = 4 },
                new Category { Id = 1205, Name = "Для немовлят", IconUrl = "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Baby.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Baby.jpg", ParentCategoryId = 12, DisplayOrder = 5 },
                new Category { Id = 1206, Name = "Підгузки", IconUrl = "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Diapers.png", ImgUrl = "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Diapers.png", ParentCategoryId = 12, DisplayOrder = 6 },
                new Category { Id = 1207, Name = "Пляшечки", IconUrl = "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-BabyBottles.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-BabyBottles.webp", ParentCategoryId = 12, DisplayOrder = 7 },
                new Category { Id = 1208, Name = "Дитячий транспорт", IconUrl = "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-KidsVehicles.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-KidsVehicles.jpg", ParentCategoryId = 12, DisplayOrder = 8 },
                new Category { Id = 1209, Name = "Коляски", IconUrl = "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Strollers.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Strollers.webp", ParentCategoryId = 12, DisplayOrder = 9 },
                new Category { Id = 1210, Name = "Самокати", IconUrl = "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-KidsScooters.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-KidsScooters.webp", ParentCategoryId = 12, DisplayOrder = 10 }
            );

            // Категория 13 - PetSupplies
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1301, Name = "Для собак", IconUrl = "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-Dogs.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-Dogs.webp", ParentCategoryId = 13, DisplayOrder = 1 },
                new Category { Id = 1302, Name = "Корм", IconUrl = "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-DogFood.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-DogFood.webp", ParentCategoryId = 13, DisplayOrder = 2 },
                new Category { Id = 1303, Name = "Іграшки", IconUrl = "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-DogToys.png", ImgUrl = "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-DogToys.png", ParentCategoryId = 13, DisplayOrder = 3 },
                new Category { Id = 1304, Name = "Для котів", IconUrl = "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-Cats.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-Cats.webp", ParentCategoryId = 13, DisplayOrder = 4 },
                new Category { Id = 1305, Name = "Корм", IconUrl = "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-CatFood.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-CatFood.webp", ParentCategoryId = 13, DisplayOrder = 5 },
                new Category { Id = 1306, Name = "Наповнювачі", IconUrl = "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-CatLitter.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-CatLitter.webp", ParentCategoryId = 13, DisplayOrder = 6 },
                new Category { Id = 1307, Name = "Для гризунів", IconUrl = "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-Rodents.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-Rodents.webp", ParentCategoryId = 13, DisplayOrder = 7 },
                new Category { Id = 1308, Name = "Клітки", IconUrl = "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-Cages.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-Cages.jpg", ParentCategoryId = 13, DisplayOrder = 8 },
                new Category { Id = 1309, Name = "Корм", IconUrl = "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-RodentFood.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-RodentFood.jpg", ParentCategoryId = 13, DisplayOrder = 9 }
            );

            // Категория 14 - StationeryBooks
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1401, Name = "Канцтовари", IconUrl = "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Stationery.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Stationery.jpg", ParentCategoryId = 14, DisplayOrder = 1 },
                new Category { Id = 1402, Name = "Ручки", IconUrl = "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Pens.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Pens.jpg", ParentCategoryId = 14, DisplayOrder = 2 },
                new Category { Id = 1403, Name = "Зошити", IconUrl = "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Notebooks.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Notebooks.jpg", ParentCategoryId = 14, DisplayOrder = 3 },
                new Category { Id = 1404, Name = "Папір", IconUrl = "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Paper.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Paper.jpg", ParentCategoryId = 14, DisplayOrder = 4 },
                new Category { Id = 1405, Name = "Книги", IconUrl = "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Books.jpeg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Books.jpeg", ParentCategoryId = 14, DisplayOrder = 5 },
                new Category { Id = 1406, Name = "Художні", IconUrl = "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Fiction.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Fiction.jpg", ParentCategoryId = 14, DisplayOrder = 6 },
                new Category { Id = 1407, Name = "Навчальні", IconUrl = "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Educational.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Educational.jpg", ParentCategoryId = 14, DisplayOrder = 7 }
            );

            // Категория 15 - AlcoholFood
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1501, Name = "Алкоголь", IconUrl = "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Alcohol.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Alcohol.webp", ParentCategoryId = 15, DisplayOrder = 1 },
                new Category { Id = 1502, Name = "Вино", IconUrl = "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Wine.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Wine.webp", ParentCategoryId = 15, DisplayOrder = 2 },
                new Category { Id = 1503, Name = "Пиво", IconUrl = "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Beer.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Beer.webp", ParentCategoryId = 15, DisplayOrder = 3 },
                new Category { Id = 1504, Name = "Віскі", IconUrl = "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Whiskey.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Whiskey.webp", ParentCategoryId = 15, DisplayOrder = 4 },
                new Category { Id = 1505, Name = "Продукти", IconUrl = "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Food.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Food.webp", ParentCategoryId = 15, DisplayOrder = 5 },
                new Category { Id = 1506, Name = "Солодощі", IconUrl = "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Sweets.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Sweets.webp", ParentCategoryId = 15, DisplayOrder = 6 },
                new Category { Id = 1507, Name = "Снеки", IconUrl = "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Snacks.jpg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Snacks.jpg", ParentCategoryId = 15, DisplayOrder = 7 }
            );

            // Категория 16 - BusinessService
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1601, Name = "Офіс", IconUrl = "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-Office.jpeg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-Office.jpeg", ParentCategoryId = 16, DisplayOrder = 1 },
                new Category { Id = 1602, Name = "Офісна техніка", IconUrl = "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-OfficeEquipment.png", ImgUrl = "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-OfficeEquipment.png", ParentCategoryId = 16, DisplayOrder = 2 },
                new Category { Id = 1603, Name = "Меблі", IconUrl = "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-OfficeFurniture.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-OfficeFurniture.webp", ParentCategoryId = 16, DisplayOrder = 3 },
                new Category { Id = 1604, Name = "Бізнес обладнання", IconUrl = "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-BusinessEquipment.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-BusinessEquipment.webp", ParentCategoryId = 16, DisplayOrder = 4 },
                new Category { Id = 1605, Name = "POS системи", IconUrl = "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-POS.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-POS.webp", ParentCategoryId = 16, DisplayOrder = 5 },
                new Category { Id = 1606, Name = "Касові апарати", IconUrl = "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-CashRegisters.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-CashRegisters.webp", ParentCategoryId = 16, DisplayOrder = 6 }
            );

            // Категория 17 - TourismOutdoor
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1701, Name = "Туристичне спорядження", IconUrl = "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-TourismGear.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-TourismGear.webp", ParentCategoryId = 17, DisplayOrder = 1 },
                new Category { Id = 1702, Name = "Намет", IconUrl = "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-Tents.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-Tents.webp", ParentCategoryId = 17, DisplayOrder = 2 },
                new Category { Id = 1703, Name = "Спальні мішки", IconUrl = "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-SleepingBags.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-SleepingBags.webp", ParentCategoryId = 17, DisplayOrder = 3 },
                new Category { Id = 1704, Name = "Подорожі", IconUrl = "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-Travel.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-Travel.webp", ParentCategoryId = 17, DisplayOrder = 4 },
                new Category { Id = 1705, Name = "Валізи", IconUrl = "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-Suitcases.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-Suitcases.webp", ParentCategoryId = 17, DisplayOrder = 5 },
                new Category { Id = 1706, Name = "Рюкзаки", IconUrl = "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-Backpacks.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-Backpacks.webp", ParentCategoryId = 17, DisplayOrder = 6 }
            );

            // Категория 18 - Promotions
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1801, Name = "Товари зі знижками", IconUrl = "/AssetsIconImg/SecondLevelCategories/Promotions/categoryIcon-Discounted.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Promotions/categoryIcon-Discounted.webp", ParentCategoryId = 18, DisplayOrder = 1 },
                new Category { Id = 1802, Name = "Сезонні розпродажі", IconUrl = "/AssetsIconImg/SecondLevelCategories/Promotions/categoryIcon-SeasonalSales.webp", ImgUrl = "/AssetsIconImg/SecondLevelCategories/Promotions/categoryIcon-SeasonalSales.webp", ParentCategoryId = 18, DisplayOrder = 2 }
            );

            // Категория 19 - TotalSale
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1901, Name = "До −50%", IconUrl = "/AssetsIconImg/SecondLevelCategories/TotalSale/categoryIcon-UpTo50.svg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/TotalSale/categoryIcon-UpTo50.svg", ParentCategoryId = 19, DisplayOrder = 1 },
                new Category { Id = 1902, Name = "До −70%", IconUrl = "/AssetsIconImg/SecondLevelCategories/TotalSale/categoryIcon-UpTo70.svg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/TotalSale/categoryIcon-UpTo70.svg", ParentCategoryId = 19, DisplayOrder = 2 },
                new Category { Id = 1903, Name = "Останні екземпляри", IconUrl = "/AssetsIconImg/SecondLevelCategories/TotalSale/categoryIcon-LastItems.svg", ImgUrl = "/AssetsIconImg/SecondLevelCategories/TotalSale/categoryIcon-LastItems.svg", ParentCategoryId = 19, DisplayOrder = 3 }
            );

            // 6. Связи категорий с брендами
            modelBuilder.Entity<CategoryBrand>().HasData(
                new CategoryBrand { CategoryId = 1, BrandId = 1 },
                new CategoryBrand { CategoryId = 1, BrandId = 2 },
                new CategoryBrand { CategoryId = 1, BrandId = 12 },
                new CategoryBrand { CategoryId = 1, BrandId = 13 },
                new CategoryBrand { CategoryId = 1, BrandId = 14 },
                new CategoryBrand { CategoryId = 1, BrandId = 15 },
                new CategoryBrand { CategoryId = 1, BrandId = 16 },
                new CategoryBrand { CategoryId = 2, BrandId = 1 },
                new CategoryBrand { CategoryId = 2, BrandId = 2 },
                new CategoryBrand { CategoryId = 2, BrandId = 3 },
                new CategoryBrand { CategoryId = 3, BrandId = 4 },
                new CategoryBrand { CategoryId = 3, BrandId = 15 },
                new CategoryBrand { CategoryId = 4, BrandId = 2 },
                new CategoryBrand { CategoryId = 4, BrandId = 4 },
                new CategoryBrand { CategoryId = 4, BrandId = 5 },
                new CategoryBrand { CategoryId = 4, BrandId = 6 },
                new CategoryBrand { CategoryId = 9, BrandId = 7 },
                new CategoryBrand { CategoryId = 9, BrandId = 8 },
                new CategoryBrand { CategoryId = 9, BrandId = 9 },
                new CategoryBrand { CategoryId = 10, BrandId = 7 },
                new CategoryBrand { CategoryId = 10, BrandId = 8 },
                new CategoryBrand { CategoryId = 10, BrandId = 9 },
                new CategoryBrand { CategoryId = 10, BrandId = 10 },
                new CategoryBrand { CategoryId = 10, BrandId = 11 }
            );

            // 7. Товары
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
                        BrandId = 1,
                        UserId = 1,
                        ImageUrl = "/images/items/default.jpg"
                    });
                }
            }

            modelBuilder.Entity<Item>().HasData(items);

            // 8. Фильтры категорий
            var allCategoryIds = new List<int>();
            allCategoryIds.AddRange(Enumerable.Range(1, 19));
            allCategoryIds.AddRange(Enumerable.Range(101, 29));
            allCategoryIds.AddRange(Enumerable.Range(201, 21));
            allCategoryIds.AddRange(Enumerable.Range(301, 15));
            allCategoryIds.AddRange(Enumerable.Range(401, 16));
            allCategoryIds.AddRange(Enumerable.Range(501, 12));
            allCategoryIds.AddRange(Enumerable.Range(601, 10));
            allCategoryIds.AddRange(Enumerable.Range(701, 11));
            allCategoryIds.AddRange(Enumerable.Range(801, 9));
            allCategoryIds.AddRange(Enumerable.Range(901, 9));
            allCategoryIds.AddRange(Enumerable.Range(1001, 13));
            allCategoryIds.AddRange(Enumerable.Range(1101, 9));
            allCategoryIds.AddRange(Enumerable.Range(1201, 10));
            allCategoryIds.AddRange(Enumerable.Range(1301, 9));
            allCategoryIds.AddRange(Enumerable.Range(1401, 7));
            allCategoryIds.AddRange(Enumerable.Range(1501, 7));
            allCategoryIds.AddRange(Enumerable.Range(1601, 6));
            allCategoryIds.AddRange(Enumerable.Range(1701, 6));
            allCategoryIds.AddRange(Enumerable.Range(1801, 2));
            allCategoryIds.AddRange(Enumerable.Range(1901, 3));

            int filterId = -2000;
            var categoryFilters = new List<CategoryFilter>();

            foreach (var catId in allCategoryIds)
            {
                string group = GetCategoryGroup(catId);
                var filtersForGroup = GetFiltersForGroup(group, ref filterId, catId);
                categoryFilters.AddRange(filtersForGroup);
            }

            modelBuilder.Entity<CategoryFilter>().HasData(categoryFilters);

            // 9. Характеристики товаров
            var characteristics = new List<ItemCharacteristic>();
            int charId = -3000;

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

            return "default";
        }

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
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "brand", DisplayName = "Бренд", ValueType = FilterValueType.String });
                    filters.Add(new CategoryFilter { Id = filterId--, CategoryId = categoryId, Key = "price", DisplayName = "Ціна", ValueType = FilterValueType.Range });
                    break;
            }

            return filters;
        }
    }
}