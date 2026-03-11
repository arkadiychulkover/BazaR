using Microsoft.EntityFrameworkCore;
using BazaR.Models;

namespace BazaR.Data
{
    public class AppDbContext : DbContext
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
        public DbSet<User> Users { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                entity.Property(e => e.Price).HasPrecision(18, 2); // Добавлено для decimal

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
                entity.Property(e => e.Price).HasPrecision(18, 2); // Добавлено для decimal
                entity.Property(e => e.PaymentType).HasConversion<int>();

                entity.HasOne(d => d.Item)
                    .WithMany(i => i.DeliveryVariants)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Настройка User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            });

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

                // Уникальность: один пользователь может добавить конкретный товар только один раз
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

                // Уникальность: один пользователь может добавить конкретный товар в избранное только один раз
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
                entity.Property(e => e.TotalAmount).HasPrecision(18, 2); // Добавлено для decimal

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
                entity.Property(e => e.PriceAtMoment).HasPrecision(18, 2); // Добавлено для decimal

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

            // 2. Затем пользователь (админ)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "admin@example.com",
                    PasswordHash = "AQAAAAIAAYagAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==", // "admin123"
                    Name = "Admin User",
                    IsAdmin = true
                },
                new User
                {
                    Id = 2,
                    Email = "test@example.com",
                    PasswordHash = "AQAAAAIAAYagAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==", // "test123"
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

            // 5. Подкатегории
            // Категория 1 - Ноутбуки та комп'ютери
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 101, Name = "Ноутбуки", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 1 },
                new Category { Id = 102, Name = "Ігрові ноутбуки", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 2 },
                new Category { Id = 103, Name = "Ультрабуки", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 3 },
                new Category { Id = 104, Name = "Для навчання", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 4 },
                new Category { Id = 105, Name = "Для роботи", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 5 },
                new Category { Id = 106, Name = "Chromebook", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 6 },
                new Category { Id = 107, Name = "Комп'ютери", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 7 },
                new Category { Id = 108, Name = "Настільні ПК", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 8 },
                new Category { Id = 109, Name = "Ігрові ПК", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 9 },
                new Category { Id = 110, Name = "Міні-ПК", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 10 },
                new Category { Id = 111, Name = "Моноблоки", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 11 },
                new Category { Id = 112, Name = "Робочі станції", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 12 },
                new Category { Id = 113, Name = "Комплектуючі", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 13 },
                new Category { Id = 114, Name = "Процесори", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 14 },
                new Category { Id = 115, Name = "Відеокарти", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 15 },
                new Category { Id = 116, Name = "Материнські плати", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 16 },
                new Category { Id = 117, Name = "Оперативна пам'ять", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 17 },
                new Category { Id = 118, Name = "Блоки живлення", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 18 },
                new Category { Id = 119, Name = "Корпуси", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 19 },
                new Category { Id = 120, Name = "Накопичувачі", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 20 },
                new Category { Id = 121, Name = "SSD", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 21 },
                new Category { Id = 122, Name = "HDD", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 22 },
                new Category { Id = 123, Name = "Зовнішні диски", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 23 },
                new Category { Id = 124, Name = "NAS", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 24 },
                new Category { Id = 125, Name = "Периферія", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 25 },
                new Category { Id = 126, Name = "Клавіатури", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 26 },
                new Category { Id = 127, Name = "Миші", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 27 },
                new Category { Id = 128, Name = "Килимки", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 28 },
                new Category { Id = 129, Name = "Вебкамери", IconUrl = null, ImgUrl = null, ParentCategoryId = 1, DisplayOrder = 29 }
            );

            // Категория 2 - Смартфони, ТВ та електроніка
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 201, Name = "Смартфони", IconUrl = null, ImgUrl = null, ParentCategoryId = 2, DisplayOrder = 1 },
                new Category { Id = 202, Name = "Android", IconUrl = null, ImgUrl = null, ParentCategoryId = 2, DisplayOrder = 2 },
                new Category { Id = 203, Name = "iPhone", IconUrl = null, ImgUrl = null, ParentCategoryId = 2, DisplayOrder = 3 },
                new Category { Id = 204, Name = "Бюджетні", IconUrl = null, ImgUrl = null, ParentCategoryId = 2, DisplayOrder = 4 },
                new Category { Id = 205, Name = "Флагмани", IconUrl = null, ImgUrl = null, ParentCategoryId = 2, DisplayOrder = 5 },
                new Category { Id = 206, Name = "Телевізори", IconUrl = null, ImgUrl = null, ParentCategoryId = 2, DisplayOrder = 6 },
                new Category { Id = 207, Name = "Smart TV", IconUrl = null, ImgUrl = null, ParentCategoryId = 2, DisplayOrder = 7 },
                new Category { Id = 208, Name = "LED", IconUrl = null, ImgUrl = null, ParentCategoryId = 2, DisplayOrder = 8 },
                new Category { Id = 209, Name = "OLED", IconUrl = null, ImgUrl = null, ParentCategoryId = 2, DisplayOrder = 9 },
                new Category { Id = 210, Name = "QLED", IconUrl = null, ImgUrl = null, ParentCategoryId = 2, DisplayOrder = 10 },
                new Category { Id = 211, Name = "Аудіо", IconUrl = null, ImgUrl = null, ParentCategoryId = 2, DisplayOrder = 11 },
                new Category { Id = 212, Name = "Навушники", IconUrl = null, ImgUrl = null, ParentCategoryId = 2, DisplayOrder = 12 },
                new Category { Id = 213, Name = "Саундбари", IconUrl = null, ImgUrl = null, ParentCategoryId = 2, DisplayOrder = 13 },
                new Category { Id = 214, Name = "Колонки", IconUrl = null, ImgUrl = null, ParentCategoryId = 2, DisplayOrder = 14 },
                new Category { Id = 215, Name = "Домашні кінотеатри", IconUrl = null, ImgUrl = null, ParentCategoryId = 2, DisplayOrder = 15 },
                new Category { Id = 216, Name = "Планшети", IconUrl = null, ImgUrl = null, ParentCategoryId = 2, DisplayOrder = 16 },
                new Category { Id = 217, Name = "iPad", IconUrl = null, ImgUrl = null, ParentCategoryId = 2, DisplayOrder = 17 },
                new Category { Id = 218, Name = "Android планшети", IconUrl = null, ImgUrl = null, ParentCategoryId = 2, DisplayOrder = 18 },
                new Category { Id = 219, Name = "Гаджети", IconUrl = null, ImgUrl = null, ParentCategoryId = 2, DisplayOrder = 19 },
                new Category { Id = 220, Name = "Смарт-годинники", IconUrl = null, ImgUrl = null, ParentCategoryId = 2, DisplayOrder = 20 },
                new Category { Id = 221, Name = "Фітнес-браслети", IconUrl = null, ImgUrl = null, ParentCategoryId = 2, DisplayOrder = 21 }
            );

            // Категория 3 - Товари для геймерів
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 301, Name = "Консолі", IconUrl = null, ImgUrl = null, ParentCategoryId = 3, DisplayOrder = 1 },
                new Category { Id = 302, Name = "PlayStation", IconUrl = null, ImgUrl = null, ParentCategoryId = 3, DisplayOrder = 2 },
                new Category { Id = 303, Name = "Xbox", IconUrl = null, ImgUrl = null, ParentCategoryId = 3, DisplayOrder = 3 },
                new Category { Id = 304, Name = "Nintendo", IconUrl = null, ImgUrl = null, ParentCategoryId = 3, DisplayOrder = 4 },
                new Category { Id = 305, Name = "Ігри", IconUrl = null, ImgUrl = null, ParentCategoryId = 3, DisplayOrder = 5 },
                new Category { Id = 306, Name = "PlayStation ігри", IconUrl = null, ImgUrl = null, ParentCategoryId = 3, DisplayOrder = 6 },
                new Category { Id = 307, Name = "Xbox ігри", IconUrl = null, ImgUrl = null, ParentCategoryId = 3, DisplayOrder = 7 },
                new Category { Id = 308, Name = "PC ігри", IconUrl = null, ImgUrl = null, ParentCategoryId = 3, DisplayOrder = 8 },
                new Category { Id = 309, Name = "Геймерська периферія", IconUrl = null, ImgUrl = null, ParentCategoryId = 3, DisplayOrder = 9 },
                new Category { Id = 310, Name = "Ігрові миші", IconUrl = null, ImgUrl = null, ParentCategoryId = 3, DisplayOrder = 10 },
                new Category { Id = 311, Name = "Ігрові клавіатури", IconUrl = null, ImgUrl = null, ParentCategoryId = 3, DisplayOrder = 11 },
                new Category { Id = 312, Name = "Геймерські навушники", IconUrl = null, ImgUrl = null, ParentCategoryId = 3, DisplayOrder = 12 },
                new Category { Id = 313, Name = "VR", IconUrl = null, ImgUrl = null, ParentCategoryId = 3, DisplayOrder = 13 },
                new Category { Id = 314, Name = "VR шоломи", IconUrl = null, ImgUrl = null, ParentCategoryId = 3, DisplayOrder = 14 },
                new Category { Id = 315, Name = "VR аксесуари", IconUrl = null, ImgUrl = null, ParentCategoryId = 3, DisplayOrder = 15 }
            );

            // Категория 4 - Побутова техніка
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 401, Name = "Велика техніка", IconUrl = null, ImgUrl = null, ParentCategoryId = 4, DisplayOrder = 1 },
                new Category { Id = 402, Name = "Холодильники", IconUrl = null, ImgUrl = null, ParentCategoryId = 4, DisplayOrder = 2 },
                new Category { Id = 403, Name = "Пральні машини", IconUrl = null, ImgUrl = null, ParentCategoryId = 4, DisplayOrder = 3 },
                new Category { Id = 404, Name = "Посудомийні машини", IconUrl = null, ImgUrl = null, ParentCategoryId = 4, DisplayOrder = 4 },
                new Category { Id = 405, Name = "Кухонна техніка", IconUrl = null, ImgUrl = null, ParentCategoryId = 4, DisplayOrder = 5 },
                new Category { Id = 406, Name = "Мікрохвильові печі", IconUrl = null, ImgUrl = null, ParentCategoryId = 4, DisplayOrder = 6 },
                new Category { Id = 407, Name = "Блендери", IconUrl = null, ImgUrl = null, ParentCategoryId = 4, DisplayOrder = 7 },
                new Category { Id = 408, Name = "Міксери", IconUrl = null, ImgUrl = null, ParentCategoryId = 4, DisplayOrder = 8 },
                new Category { Id = 409, Name = "Мультиварки", IconUrl = null, ImgUrl = null, ParentCategoryId = 4, DisplayOrder = 9 },
                new Category { Id = 410, Name = "Кліматична техніка", IconUrl = null, ImgUrl = null, ParentCategoryId = 4, DisplayOrder = 10 },
                new Category { Id = 411, Name = "Кондиціонери", IconUrl = null, ImgUrl = null, ParentCategoryId = 4, DisplayOrder = 11 },
                new Category { Id = 412, Name = "Обігрівачі", IconUrl = null, ImgUrl = null, ParentCategoryId = 4, DisplayOrder = 12 },
                new Category { Id = 413, Name = "Вентилятори", IconUrl = null, ImgUrl = null, ParentCategoryId = 4, DisplayOrder = 13 },
                new Category { Id = 414, Name = "Прибирання", IconUrl = null, ImgUrl = null, ParentCategoryId = 4, DisplayOrder = 14 },
                new Category { Id = 415, Name = "Пилососи", IconUrl = null, ImgUrl = null, ParentCategoryId = 4, DisplayOrder = 15 },
                new Category { Id = 416, Name = "Роботи-пилососи", IconUrl = null, ImgUrl = null, ParentCategoryId = 4, DisplayOrder = 16 }
            );

            // Категория 5 - Товари для дому
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 501, Name = "Меблі", IconUrl = null, ImgUrl = null, ParentCategoryId = 5, DisplayOrder = 1 },
                new Category { Id = 502, Name = "Дивани", IconUrl = null, ImgUrl = null, ParentCategoryId = 5, DisplayOrder = 2 },
                new Category { Id = 503, Name = "Ліжка", IconUrl = null, ImgUrl = null, ParentCategoryId = 5, DisplayOrder = 3 },
                new Category { Id = 504, Name = "Шафи", IconUrl = null, ImgUrl = null, ParentCategoryId = 5, DisplayOrder = 4 },
                new Category { Id = 505, Name = "Освітлення", IconUrl = null, ImgUrl = null, ParentCategoryId = 5, DisplayOrder = 5 },
                new Category { Id = 506, Name = "Лампи", IconUrl = null, ImgUrl = null, ParentCategoryId = 5, DisplayOrder = 6 },
                new Category { Id = 507, Name = "Люстри", IconUrl = null, ImgUrl = null, ParentCategoryId = 5, DisplayOrder = 7 },
                new Category { Id = 508, Name = "LED освітлення", IconUrl = null, ImgUrl = null, ParentCategoryId = 5, DisplayOrder = 8 },
                new Category { Id = 509, Name = "Декор", IconUrl = null, ImgUrl = null, ParentCategoryId = 5, DisplayOrder = 9 },
                new Category { Id = 510, Name = "Картини", IconUrl = null, ImgUrl = null, ParentCategoryId = 5, DisplayOrder = 10 },
                new Category { Id = 511, Name = "Дзеркала", IconUrl = null, ImgUrl = null, ParentCategoryId = 5, DisplayOrder = 11 },
                new Category { Id = 512, Name = "Годинники", IconUrl = null, ImgUrl = null, ParentCategoryId = 5, DisplayOrder = 12 }
            );

            // Категория 6 - Інструменти та автотовари
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 601, Name = "Електроінструменти", IconUrl = null, ImgUrl = null, ParentCategoryId = 6, DisplayOrder = 1 },
                new Category { Id = 602, Name = "Дрилі", IconUrl = null, ImgUrl = null, ParentCategoryId = 6, DisplayOrder = 2 },
                new Category { Id = 603, Name = "Шуруповерти", IconUrl = null, ImgUrl = null, ParentCategoryId = 6, DisplayOrder = 3 },
                new Category { Id = 604, Name = "Болгарки", IconUrl = null, ImgUrl = null, ParentCategoryId = 6, DisplayOrder = 4 },
                new Category { Id = 605, Name = "Автоелектроніка", IconUrl = null, ImgUrl = null, ParentCategoryId = 6, DisplayOrder = 5 },
                new Category { Id = 606, Name = "Відеореєстратори", IconUrl = null, ImgUrl = null, ParentCategoryId = 6, DisplayOrder = 6 },
                new Category { Id = 607, Name = "GPS навігатори", IconUrl = null, ImgUrl = null, ParentCategoryId = 6, DisplayOrder = 7 },
                new Category { Id = 608, Name = "Автоаксесуари", IconUrl = null, ImgUrl = null, ParentCategoryId = 6, DisplayOrder = 8 },
                new Category { Id = 609, Name = "Тримачі телефону", IconUrl = null, ImgUrl = null, ParentCategoryId = 6, DisplayOrder = 9 },
                new Category { Id = 610, Name = "Зарядні пристрої", IconUrl = null, ImgUrl = null, ParentCategoryId = 6, DisplayOrder = 10 }
            );

            // Категория 7 - Сантехніка та ремонт
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 701, Name = "Ванна кімната", IconUrl = null, ImgUrl = null, ParentCategoryId = 7, DisplayOrder = 1 },
                new Category { Id = 702, Name = "Душові кабіни", IconUrl = null, ImgUrl = null, ParentCategoryId = 7, DisplayOrder = 2 },
                new Category { Id = 703, Name = "Унітази", IconUrl = null, ImgUrl = null, ParentCategoryId = 7, DisplayOrder = 3 },
                new Category { Id = 704, Name = "Раковини", IconUrl = null, ImgUrl = null, ParentCategoryId = 7, DisplayOrder = 4 },
                new Category { Id = 705, Name = "Інструменти", IconUrl = null, ImgUrl = null, ParentCategoryId = 7, DisplayOrder = 5 },
                new Category { Id = 706, Name = "Ручний інструмент", IconUrl = null, ImgUrl = null, ParentCategoryId = 7, DisplayOrder = 6 },
                new Category { Id = 707, Name = "Вимірювальні прилади", IconUrl = null, ImgUrl = null, ParentCategoryId = 7, DisplayOrder = 7 },
                new Category { Id = 708, Name = "Матеріали", IconUrl = null, ImgUrl = null, ParentCategoryId = 7, DisplayOrder = 8 },
                new Category { Id = 709, Name = "Фарба", IconUrl = null, ImgUrl = null, ParentCategoryId = 7, DisplayOrder = 9 },
                new Category { Id = 710, Name = "Плитка", IconUrl = null, ImgUrl = null, ParentCategoryId = 7, DisplayOrder = 10 },
                new Category { Id = 711, Name = "Ламінат", IconUrl = null, ImgUrl = null, ParentCategoryId = 7, DisplayOrder = 11 }
            );

            // Категория 8 - Дача, сад та город
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 801, Name = "Садова техніка", IconUrl = null, ImgUrl = null, ParentCategoryId = 8, DisplayOrder = 1 },
                new Category { Id = 802, Name = "Газонокосарки", IconUrl = null, ImgUrl = null, ParentCategoryId = 8, DisplayOrder = 2 },
                new Category { Id = 803, Name = "Тримери", IconUrl = null, ImgUrl = null, ParentCategoryId = 8, DisplayOrder = 3 },
                new Category { Id = 804, Name = "Садові інструменти", IconUrl = null, ImgUrl = null, ParentCategoryId = 8, DisplayOrder = 4 },
                new Category { Id = 805, Name = "Лопати", IconUrl = null, ImgUrl = null, ParentCategoryId = 8, DisplayOrder = 5 },
                new Category { Id = 806, Name = "Секатори", IconUrl = null, ImgUrl = null, ParentCategoryId = 8, DisplayOrder = 6 },
                new Category { Id = 807, Name = "Меблі для саду", IconUrl = null, ImgUrl = null, ParentCategoryId = 8, DisplayOrder = 7 },
                new Category { Id = 808, Name = "Садові столи", IconUrl = null, ImgUrl = null, ParentCategoryId = 8, DisplayOrder = 8 },
                new Category { Id = 809, Name = "Крісла", IconUrl = null, ImgUrl = null, ParentCategoryId = 8, DisplayOrder = 9 }
            );

            // Категория 9 - Спорт та захоплення
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 901, Name = "Фітнес", IconUrl = null, ImgUrl = null, ParentCategoryId = 9, DisplayOrder = 1 },
                new Category { Id = 902, Name = "Гантелі", IconUrl = null, ImgUrl = null, ParentCategoryId = 9, DisplayOrder = 2 },
                new Category { Id = 903, Name = "Бігові доріжки", IconUrl = null, ImgUrl = null, ParentCategoryId = 9, DisplayOrder = 3 },
                new Category { Id = 904, Name = "Велоспорт", IconUrl = null, ImgUrl = null, ParentCategoryId = 9, DisplayOrder = 4 },
                new Category { Id = 905, Name = "Велосипеди", IconUrl = null, ImgUrl = null, ParentCategoryId = 9, DisplayOrder = 5 },
                new Category { Id = 906, Name = "Аксесуари", IconUrl = null, ImgUrl = null, ParentCategoryId = 9, DisplayOrder = 6 },
                new Category { Id = 907, Name = "Активний відпочинок", IconUrl = null, ImgUrl = null, ParentCategoryId = 9, DisplayOrder = 7 },
                new Category { Id = 908, Name = "Самокати", IconUrl = null, ImgUrl = null, ParentCategoryId = 9, DisplayOrder = 8 },
                new Category { Id = 909, Name = "Електросамокати", IconUrl = null, ImgUrl = null, ParentCategoryId = 9, DisplayOrder = 9 }
            );

            // Категория 10 - Одяг, взуття та прикраси
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1001, Name = "Чоловічий одяг", IconUrl = null, ImgUrl = null, ParentCategoryId = 10, DisplayOrder = 1 },
                new Category { Id = 1002, Name = "Футболки", IconUrl = null, ImgUrl = null, ParentCategoryId = 10, DisplayOrder = 2 },
                new Category { Id = 1003, Name = "Джинси", IconUrl = null, ImgUrl = null, ParentCategoryId = 10, DisplayOrder = 3 },
                new Category { Id = 1004, Name = "Куртки", IconUrl = null, ImgUrl = null, ParentCategoryId = 10, DisplayOrder = 4 },
                new Category { Id = 1005, Name = "Жіночий одяг", IconUrl = null, ImgUrl = null, ParentCategoryId = 10, DisplayOrder = 5 },
                new Category { Id = 1006, Name = "Сукні", IconUrl = null, ImgUrl = null, ParentCategoryId = 10, DisplayOrder = 6 },
                new Category { Id = 1007, Name = "Спідниці", IconUrl = null, ImgUrl = null, ParentCategoryId = 10, DisplayOrder = 7 },
                new Category { Id = 1008, Name = "Взуття", IconUrl = null, ImgUrl = null, ParentCategoryId = 10, DisplayOrder = 8 },
                new Category { Id = 1009, Name = "Кросівки", IconUrl = null, ImgUrl = null, ParentCategoryId = 10, DisplayOrder = 9 },
                new Category { Id = 1010, Name = "Черевики", IconUrl = null, ImgUrl = null, ParentCategoryId = 10, DisplayOrder = 10 },
                new Category { Id = 1011, Name = "Аксесуари", IconUrl = null, ImgUrl = null, ParentCategoryId = 10, DisplayOrder = 11 },
                new Category { Id = 1012, Name = "Сумки", IconUrl = null, ImgUrl = null, ParentCategoryId = 10, DisplayOrder = 12 },
                new Category { Id = 1013, Name = "Ремені", IconUrl = null, ImgUrl = null, ParentCategoryId = 10, DisplayOrder = 13 }
            );

            // Категория 11 - Краса і здоров'я
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1101, Name = "Догляд за обличчям", IconUrl = null, ImgUrl = null, ParentCategoryId = 11, DisplayOrder = 1 },
                new Category { Id = 1102, Name = "Креми", IconUrl = null, ImgUrl = null, ParentCategoryId = 11, DisplayOrder = 2 },
                new Category { Id = 1103, Name = "Сироватки", IconUrl = null, ImgUrl = null, ParentCategoryId = 11, DisplayOrder = 3 },
                new Category { Id = 1104, Name = "Догляд за волоссям", IconUrl = null, ImgUrl = null, ParentCategoryId = 11, DisplayOrder = 4 },
                new Category { Id = 1105, Name = "Шампуні", IconUrl = null, ImgUrl = null, ParentCategoryId = 11, DisplayOrder = 5 },
                new Category { Id = 1106, Name = "Маски", IconUrl = null, ImgUrl = null, ParentCategoryId = 11, DisplayOrder = 6 },
                new Category { Id = 1107, Name = "Техніка", IconUrl = null, ImgUrl = null, ParentCategoryId = 11, DisplayOrder = 7 },
                new Category { Id = 1108, Name = "Фени", IconUrl = null, ImgUrl = null, ParentCategoryId = 11, DisplayOrder = 8 },
                new Category { Id = 1109, Name = "Бритви", IconUrl = null, ImgUrl = null, ParentCategoryId = 11, DisplayOrder = 9 }
            );

            // Категория 12 - Дитячі товари
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1201, Name = "Іграшки", IconUrl = null, ImgUrl = null, ParentCategoryId = 12, DisplayOrder = 1 },
                new Category { Id = 1202, Name = "Конструктори", IconUrl = null, ImgUrl = null, ParentCategoryId = 12, DisplayOrder = 2 },
                new Category { Id = 1203, Name = "Ляльки", IconUrl = null, ImgUrl = null, ParentCategoryId = 12, DisplayOrder = 3 },
                new Category { Id = 1204, Name = "Машинки", IconUrl = null, ImgUrl = null, ParentCategoryId = 12, DisplayOrder = 4 },
                new Category { Id = 1205, Name = "Для немовлят", IconUrl = null, ImgUrl = null, ParentCategoryId = 12, DisplayOrder = 5 },
                new Category { Id = 1206, Name = "Підгузки", IconUrl = null, ImgUrl = null, ParentCategoryId = 12, DisplayOrder = 6 },
                new Category { Id = 1207, Name = "Пляшечки", IconUrl = null, ImgUrl = null, ParentCategoryId = 12, DisplayOrder = 7 },
                new Category { Id = 1208, Name = "Дитячий транспорт", IconUrl = null, ImgUrl = null, ParentCategoryId = 12, DisplayOrder = 8 },
                new Category { Id = 1209, Name = "Коляски", IconUrl = null, ImgUrl = null, ParentCategoryId = 12, DisplayOrder = 9 },
                new Category { Id = 1210, Name = "Самокати", IconUrl = null, ImgUrl = null, ParentCategoryId = 12, DisplayOrder = 10 }
            );

            // Категория 13 - Зоотовари
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1301, Name = "Для собак", IconUrl = null, ImgUrl = null, ParentCategoryId = 13, DisplayOrder = 1 },
                new Category { Id = 1302, Name = "Корм", IconUrl = null, ImgUrl = null, ParentCategoryId = 13, DisplayOrder = 2 },
                new Category { Id = 1303, Name = "Іграшки", IconUrl = null, ImgUrl = null, ParentCategoryId = 13, DisplayOrder = 3 },
                new Category { Id = 1304, Name = "Для котів", IconUrl = null, ImgUrl = null, ParentCategoryId = 13, DisplayOrder = 4 },
                new Category { Id = 1305, Name = "Корм", IconUrl = null, ImgUrl = null, ParentCategoryId = 13, DisplayOrder = 5 },
                new Category { Id = 1306, Name = "Наповнювачі", IconUrl = null, ImgUrl = null, ParentCategoryId = 13, DisplayOrder = 6 },
                new Category { Id = 1307, Name = "Для гризунів", IconUrl = null, ImgUrl = null, ParentCategoryId = 13, DisplayOrder = 7 },
                new Category { Id = 1308, Name = "Клітки", IconUrl = null, ImgUrl = null, ParentCategoryId = 13, DisplayOrder = 8 },
                new Category { Id = 1309, Name = "Корм", IconUrl = null, ImgUrl = null, ParentCategoryId = 13, DisplayOrder = 9 }
            );

            // Категория 14 - Канцтовари та книги
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1401, Name = "Канцтовари", IconUrl = null, ImgUrl = null, ParentCategoryId = 14, DisplayOrder = 1 },
                new Category { Id = 1402, Name = "Ручки", IconUrl = null, ImgUrl = null, ParentCategoryId = 14, DisplayOrder = 2 },
                new Category { Id = 1403, Name = "Зошити", IconUrl = null, ImgUrl = null, ParentCategoryId = 14, DisplayOrder = 3 },
                new Category { Id = 1404, Name = "Папір", IconUrl = null, ImgUrl = null, ParentCategoryId = 14, DisplayOrder = 4 },
                new Category { Id = 1405, Name = "Книги", IconUrl = null, ImgUrl = null, ParentCategoryId = 14, DisplayOrder = 5 },
                new Category { Id = 1406, Name = "Художні", IconUrl = null, ImgUrl = null, ParentCategoryId = 14, DisplayOrder = 6 },
                new Category { Id = 1407, Name = "Навчальні", IconUrl = null, ImgUrl = null, ParentCategoryId = 14, DisplayOrder = 7 }
            );

            // Категория 15 - Алкогольні напої та продукти
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1501, Name = "Алкоголь", IconUrl = null, ImgUrl = null, ParentCategoryId = 15, DisplayOrder = 1 },
                new Category { Id = 1502, Name = "Вино", IconUrl = null, ImgUrl = null, ParentCategoryId = 15, DisplayOrder = 2 },
                new Category { Id = 1503, Name = "Пиво", IconUrl = null, ImgUrl = null, ParentCategoryId = 15, DisplayOrder = 3 },
                new Category { Id = 1504, Name = "Віскі", IconUrl = null, ImgUrl = null, ParentCategoryId = 15, DisplayOrder = 4 },
                new Category { Id = 1505, Name = "Продукти", IconUrl = null, ImgUrl = null, ParentCategoryId = 15, DisplayOrder = 5 },
                new Category { Id = 1506, Name = "Солодощі", IconUrl = null, ImgUrl = null, ParentCategoryId = 15, DisplayOrder = 6 },
                new Category { Id = 1507, Name = "Снеки", IconUrl = null, ImgUrl = null, ParentCategoryId = 15, DisplayOrder = 7 }
            );

            // Категория 16 - Товари для бізнесу та послуги
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1601, Name = "Офіс", IconUrl = null, ImgUrl = null, ParentCategoryId = 16, DisplayOrder = 1 },
                new Category { Id = 1602, Name = "Офісна техніка", IconUrl = null, ImgUrl = null, ParentCategoryId = 16, DisplayOrder = 2 },
                new Category { Id = 1603, Name = "Меблі", IconUrl = null, ImgUrl = null, ParentCategoryId = 16, DisplayOrder = 3 },
                new Category { Id = 1604, Name = "Бізнес обладнання", IconUrl = null, ImgUrl = null, ParentCategoryId = 16, DisplayOrder = 4 },
                new Category { Id = 1605, Name = "POS системи", IconUrl = null, ImgUrl = null, ParentCategoryId = 16, DisplayOrder = 5 },
                new Category { Id = 1606, Name = "Касові апарати", IconUrl = null, ImgUrl = null, ParentCategoryId = 16, DisplayOrder = 6 }
            );

            // Категория 17 - Туризм та відпочинок
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1701, Name = "Туристичне спорядження", IconUrl = null, ImgUrl = null, ParentCategoryId = 17, DisplayOrder = 1 },
                new Category { Id = 1702, Name = "Намет", IconUrl = null, ImgUrl = null, ParentCategoryId = 17, DisplayOrder = 2 },
                new Category { Id = 1703, Name = "Спальні мішки", IconUrl = null, ImgUrl = null, ParentCategoryId = 17, DisplayOrder = 3 },
                new Category { Id = 1704, Name = "Подорожі", IconUrl = null, ImgUrl = null, ParentCategoryId = 17, DisplayOrder = 4 },
                new Category { Id = 1705, Name = "Валізи", IconUrl = null, ImgUrl = null, ParentCategoryId = 17, DisplayOrder = 5 },
                new Category { Id = 1706, Name = "Рюкзаки", IconUrl = null, ImgUrl = null, ParentCategoryId = 17, DisplayOrder = 6 }
            );

            // Категория 18 - Акції
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1801, Name = "Товари зі знижками", IconUrl = null, ImgUrl = null, ParentCategoryId = 18, DisplayOrder = 1 },
                new Category { Id = 1802, Name = "Сезонні розпродажі", IconUrl = null, ImgUrl = null, ParentCategoryId = 18, DisplayOrder = 2 }
            );

            // Категория 19 - Тотальний розпродаж
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1901, Name = "До −50%", IconUrl = null, ImgUrl = null, ParentCategoryId = 19, DisplayOrder = 1 },
                new Category { Id = 1902, Name = "До −70%", IconUrl = null, ImgUrl = null, ParentCategoryId = 19, DisplayOrder = 2 },
                new Category { Id = 1903, Name = "Останні екземпляри", IconUrl = null, ImgUrl = null, ParentCategoryId = 19, DisplayOrder = 3 }
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
        }
    }
}