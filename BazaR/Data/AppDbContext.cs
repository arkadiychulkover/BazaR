// Data/AppDbContext.cs
using BazaR.Models;
using Microsoft.EntityFrameworkCore;

namespace BazaR.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<CategoryBrand> CategoryBrands { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemColor> ItemColors { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Usluga> Uslugi { get; set; }
        public DbSet<ItemCharacteristic> ItemCharacteristics { get; set; }
        public DbSet<Complect> Complects { get; set; }
        public DbSet<ComplectItem> ComplectItems { get; set; }
        public DbSet<CategoryFilter> CategoryFilters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка десятичных полей
            modelBuilder.Entity<Delivery>()
                .Property(d => d.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.PriceAtMoment)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Usluga>()
                .Property(u => u.Price)
                .HasPrecision(18, 2);

            // Связь Category (самоссылка для подкатегорий)
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Составные ключи
            modelBuilder.Entity<CategoryBrand>()
                .HasKey(cb => new { cb.CategoryId, cb.BrandId });

            modelBuilder.Entity<ComplectItem>()
                .HasKey(ci => new { ci.ComplectId, ci.ItemId });

            // CategoryBrand связи
            modelBuilder.Entity<CategoryBrand>()
                .HasOne(cb => cb.Category)
                .WithMany(c => c.CategoryBrands)
                .HasForeignKey(cb => cb.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CategoryBrand>()
                .HasOne(cb => cb.Brand)
                .WithMany(b => b.CategoryBrands)
                .HasForeignKey(cb => cb.BrandId)
                .OnDelete(DeleteBehavior.Cascade);

            // Item связи
            modelBuilder.Entity<Item>()
                .HasOne(i => i.Category)
                .WithMany()
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.Brand)
                .WithMany(b => b.Items) // Требует наличия свойства Items в Brand
                .HasForeignKey(i => i.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.User)
                .WithMany(u => u.SellingItems)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // CartItem (без множественных каскадов)
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(ci => ci.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Item)
                .WithMany(i => i.CartItems)
                .HasForeignKey(ci => ci.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            // WishlistItem
            modelBuilder.Entity<WishlistItem>()
                .HasOne(wi => wi.User)
                .WithMany(u => u.WishlistItems)
                .HasForeignKey(wi => wi.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WishlistItem>()
                .HasOne(wi => wi.Item)
                .WithMany(i => i.WishlistItems)
                .HasForeignKey(wi => wi.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            // Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.City)
                .WithMany()
                .HasForeignKey(o => o.CityId)
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

            // Review
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Item)
                .WithMany(i => i.Reviews)
                .HasForeignKey(r => r.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            // ItemColor
            modelBuilder.Entity<ItemColor>()
                .HasOne(ic => ic.Item)
                .WithMany(i => i.Colors)
                .HasForeignKey(ic => ic.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            // ItemCharacteristic
            modelBuilder.Entity<ItemCharacteristic>()
                .HasOne(ic => ic.Item)
                .WithMany(i => i.Characteristics)
                .HasForeignKey(ic => ic.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            // Usluga
            modelBuilder.Entity<Usluga>()
                .HasOne(u => u.Item)
                .WithMany(i => i.Uslugi)
                .HasForeignKey(u => u.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            // Delivery
            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.Item)
                .WithMany(i => i.DeliveryVariants)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            // ComplectItem
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

            // CategoryFilter
            modelBuilder.Entity<CategoryFilter>()
                .HasOne(cf => cf.Category)
                .WithMany(c => c.Filters)
                .HasForeignKey(cf => cf.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Индексы
            modelBuilder.Entity<Item>()
                .HasIndex(i => i.Name)
                .HasDatabaseName("IX_Items_Name");

            modelBuilder.Entity<Item>()
                .HasIndex(i => i.CategoryId)
                .HasDatabaseName("IX_Items_CategoryId");

            modelBuilder.Entity<Item>()
                .HasIndex(i => i.BrandId)
                .HasDatabaseName("IX_Items_BrandId");

            modelBuilder.Entity<Item>()
                .HasIndex(i => i.Price)
                .HasDatabaseName("IX_Items_Price");

            modelBuilder.Entity<Item>()
                .HasIndex(i => i.IsAvailable)
                .HasDatabaseName("IX_Items_IsAvailable");

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.ParentCategoryId)
                .HasDatabaseName("IX_Categories_ParentCategoryId");

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .HasDatabaseName("IX_Categories_Name");

            modelBuilder.Entity<Brand>()
                .HasIndex(b => b.Name)
                .HasDatabaseName("IX_Brands_Name");

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("IX_Users_Email");

            // Seed города
            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, Name = "Київ" },
                new City { Id = 2, Name = "Харків" },
                new City { Id = 3, Name = "Одеса" },
                new City { Id = 4, Name = "Дніпро" },
                new City { Id = 5, Name = "Львів" },
                new City { Id = 6, Name = "Запоріжжя" },
                new City { Id = 7, Name = "Миколаїв" },
                new City { Id = 8, Name = "Вінниця" },
                new City { Id = 9, Name = "Херсон" },
                new City { Id = 10, Name = "Полтава" }
            );

            // Обновление/добавление категорий (используем проверку в миграции, но здесь оставляем для целостности)
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Ноутбуки", IconUrl = "bi-laptop", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Смартфони, ТВ та електроніка", IconUrl = "bi-phone", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Товари для геймерів", IconUrl = "bi-controller", DisplayOrder = 3 },
                new Category { Id = 4, Name = "Побутова техніка", IconUrl = "bi-fan", DisplayOrder = 4 },
                new Category { Id = 5, Name = "Товари для дому", IconUrl = "bi-house", DisplayOrder = 5 },
                new Category { Id = 6, Name = "Інструменти та автотовари", IconUrl = "bi-tools", DisplayOrder = 6 },
                new Category { Id = 7, Name = "Сантехніка та ремонт", IconUrl = "bi-droplet", DisplayOrder = 7 },
                new Category { Id = 8, Name = "Дача, сад та город", IconUrl = "bi-flower", DisplayOrder = 8 },
                new Category { Id = 9, Name = "Спорт та захоплення", IconUrl = "bi-bicycle", DisplayOrder = 9 },
                new Category { Id = 10, Name = "Одяг, взуття та прикраси", IconUrl = "bi-tag", DisplayOrder = 10 },
                new Category { Id = 11, Name = "Краса і здоров'я", IconUrl = "bi-heart", DisplayOrder = 11 },
                new Category { Id = 12, Name = "Дитячі товари", IconUrl = "bi-emoji-smile", DisplayOrder = 12 },
                new Category { Id = 13, Name = "Зоотовари", IconUrl = "bi-bug", DisplayOrder = 13 },
                new Category { Id = 14, Name = "Канцтовари та книги", IconUrl = "bi-pencil", DisplayOrder = 14 },
                new Category { Id = 15, Name = "Алкогольні напої та продукти", IconUrl = "bi-cup-straw", DisplayOrder = 15 },
                new Category { Id = 16, Name = "Товари для бізнесу та послуги", IconUrl = "bi-briefcase", DisplayOrder = 16 },
                new Category { Id = 17, Name = "Тури та відпочинок", IconUrl = "bi-tree", DisplayOrder = 17 },
                new Category { Id = 18, Name = "Акції", IconUrl = "bi-percent", DisplayOrder = 18 },
                new Category { Id = 19, Name = "Тотальний розпродаж", IconUrl = "bi-fire", DisplayOrder = 19 }
            );

            // Подкатегории для Ноутбуков
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 101, Name = "Asus", ParentCategoryId = 1, DisplayOrder = 1 },
                new Category { Id = 102, Name = "Acer", ParentCategoryId = 1, DisplayOrder = 2 },
                new Category { Id = 103, Name = "HP", ParentCategoryId = 1, DisplayOrder = 3 },
                new Category { Id = 104, Name = "Lenovo", ParentCategoryId = 1, DisplayOrder = 4 },
                new Category { Id = 105, Name = "Dell", ParentCategoryId = 1, DisplayOrder = 5 },
                new Category { Id = 106, Name = "Apple", ParentCategoryId = 1, DisplayOrder = 6 },
                new Category { Id = 107, Name = "Аксесуари для ноутбуків і ПК", ParentCategoryId = 1, DisplayOrder = 7 },
                new Category { Id = 108, Name = "Флеш пам'ять USB", ParentCategoryId = 107, DisplayOrder = 1 },
                new Category { Id = 109, Name = "Сумки та рюкзаки для ноутбуків", ParentCategoryId = 107, DisplayOrder = 2 },
                new Category { Id = 110, Name = "Підставки та столики для ноутбуків", ParentCategoryId = 107, DisplayOrder = 3 },
                new Category { Id = 111, Name = "Веб-камери", ParentCategoryId = 107, DisplayOrder = 4 },
                new Category { Id = 120, Name = "Комп'ютери", ParentCategoryId = 1, DisplayOrder = 8 },
                new Category { Id = 121, Name = "Монітори", ParentCategoryId = 120, DisplayOrder = 1 },
                new Category { Id = 122, Name = "Миші", ParentCategoryId = 120, DisplayOrder = 2 },
                new Category { Id = 123, Name = "Клавіатури", ParentCategoryId = 120, DisplayOrder = 3 },
                new Category { Id = 124, Name = "Комплект: клавіатури + миші", ParentCategoryId = 120, DisplayOrder = 4 },
                new Category { Id = 125, Name = "Мережеві сховища (NAS)", ParentCategoryId = 120, DisplayOrder = 5 },
                new Category { Id = 130, Name = "Комплектуючі", ParentCategoryId = 1, DisplayOrder = 9 },
                new Category { Id = 131, Name = "Відеокарти", ParentCategoryId = 130, DisplayOrder = 1 },
                new Category { Id = 132, Name = "Жорсткі диски та дискові масиви", ParentCategoryId = 130, DisplayOrder = 2 },
                new Category { Id = 133, Name = "Процесори", ParentCategoryId = 130, DisplayOrder = 3 },
                new Category { Id = 134, Name = "SSD", ParentCategoryId = 130, DisplayOrder = 4 },
                new Category { Id = 135, Name = "Оперативна пам'ять", ParentCategoryId = 130, DisplayOrder = 5 },
                new Category { Id = 136, Name = "Материнські плати", ParentCategoryId = 130, DisplayOrder = 6 },
                new Category { Id = 137, Name = "Блоки живлення", ParentCategoryId = 130, DisplayOrder = 7 },
                new Category { Id = 140, Name = "Мережеве обладнання", ParentCategoryId = 1, DisplayOrder = 10 },
                new Category { Id = 141, Name = "Патч-корди", ParentCategoryId = 140, DisplayOrder = 1 },
                new Category { Id = 142, Name = "Маршрутизатори", ParentCategoryId = 140, DisplayOrder = 2 },
                new Category { Id = 143, Name = "IP-камери", ParentCategoryId = 140, DisplayOrder = 3 },
                new Category { Id = 144, Name = "Комутатори", ParentCategoryId = 140, DisplayOrder = 4 },
                new Category { Id = 145, Name = "Бездротові точки доступу", ParentCategoryId = 140, DisplayOrder = 5 },
                new Category { Id = 150, Name = "Серверне обладнання", ParentCategoryId = 1, DisplayOrder = 11 },
                new Category { Id = 160, Name = "Оргтехніка", ParentCategoryId = 1, DisplayOrder = 12 },
                new Category { Id = 161, Name = "БФП/Принтери", ParentCategoryId = 160, DisplayOrder = 1 },
                new Category { Id = 162, Name = "Проектори", ParentCategoryId = 160, DisplayOrder = 2 },
                new Category { Id = 163, Name = "Витратні матеріали для принтерів", ParentCategoryId = 160, DisplayOrder = 3 },
                new Category { Id = 164, Name = "Телефонні апарати", ParentCategoryId = 160, DisplayOrder = 4 },
                new Category { Id = 170, Name = "Програмне забезпечення", ParentCategoryId = 1, DisplayOrder = 13 },
                new Category { Id = 171, Name = "Операційні системи", ParentCategoryId = 170, DisplayOrder = 1 },
                new Category { Id = 172, Name = "Офісні програми", ParentCategoryId = 170, DisplayOrder = 2 },
                new Category { Id = 173, Name = "Антивірусні програми", ParentCategoryId = 170, DisplayOrder = 3 }
            );

            // Подкатегории для Смартфонов
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 201, Name = "Смартфони", ParentCategoryId = 2, DisplayOrder = 1 },
                new Category { Id = 202, Name = "Телевізори", ParentCategoryId = 2, DisplayOrder = 2 },
                new Category { Id = 203, Name = "Планшети", ParentCategoryId = 2, DisplayOrder = 3 },
                new Category { Id = 204, Name = "Аудіотехніка", ParentCategoryId = 2, DisplayOrder = 4 },
                new Category { Id = 205, Name = "Навушники", ParentCategoryId = 204, DisplayOrder = 1 },
                new Category { Id = 206, Name = "Колонки", ParentCategoryId = 204, DisplayOrder = 2 },
                new Category { Id = 207, Name = "MP3-плеєри", ParentCategoryId = 204, DisplayOrder = 3 },
                new Category { Id = 208, Name = "Фотоапарати", ParentCategoryId = 2, DisplayOrder = 5 },
                new Category { Id = 209, Name = "Відеокамери", ParentCategoryId = 2, DisplayOrder = 6 },
                new Category { Id = 210, Name = "Розумний дім", ParentCategoryId = 2, DisplayOrder = 7 },
                new Category { Id = 211, Name = "Аксесуари до телефонів", ParentCategoryId = 2, DisplayOrder = 8 },
                new Category { Id = 212, Name = "Чохли для телефонів", ParentCategoryId = 211, DisplayOrder = 1 },
                new Category { Id = 213, Name = "Захисні скельця", ParentCategoryId = 211, DisplayOrder = 2 },
                new Category { Id = 214, Name = "Зарядні пристрої", ParentCategoryId = 211, DisplayOrder = 3 },
                new Category { Id = 215, Name = "Power Bank", ParentCategoryId = 211, DisplayOrder = 4 }
            );

            // Подкатегории для Товаров для геймеров
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 301, Name = "PlayStation", ParentCategoryId = 3, DisplayOrder = 1 },
                new Category { Id = 302, Name = "Xbox", ParentCategoryId = 3, DisplayOrder = 2 },
                new Category { Id = 303, Name = "Nintendo", ParentCategoryId = 3, DisplayOrder = 3 },
                new Category { Id = 304, Name = "Ігрові консолі та приставки", ParentCategoryId = 3, DisplayOrder = 4 },
                new Category { Id = 305, Name = "Джойстики та аксесуари", ParentCategoryId = 3, DisplayOrder = 5 },
                new Category { Id = 306, Name = "Ігри", ParentCategoryId = 3, DisplayOrder = 6 },
                new Category { Id = 307, Name = "Ігрові поверхні", ParentCategoryId = 3, DisplayOrder = 7 },
                new Category { Id = 308, Name = "Ігрові крісла", ParentCategoryId = 3, DisplayOrder = 8 },
                new Category { Id = 309, Name = "Ігрові миші", ParentCategoryId = 3, DisplayOrder = 9 },
                new Category { Id = 310, Name = "Ігрові клавіатури", ParentCategoryId = 3, DisplayOrder = 10 },
                new Category { Id = 311, Name = "Ігрові навушники", ParentCategoryId = 3, DisplayOrder = 11 }
            );

            // Подкатегории для Побутовой техники
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 401, Name = "Велика побутова техніка", ParentCategoryId = 4, DisplayOrder = 1 },
                new Category { Id = 402, Name = "Холодильники", ParentCategoryId = 401, DisplayOrder = 1 },
                new Category { Id = 403, Name = "Пральні машини", ParentCategoryId = 401, DisplayOrder = 2 },
                new Category { Id = 404, Name = "Плити та духовки", ParentCategoryId = 401, DisplayOrder = 3 },
                new Category { Id = 405, Name = "Мікрохвильові печі", ParentCategoryId = 401, DisplayOrder = 4 },
                new Category { Id = 406, Name = "Посудомийні машини", ParentCategoryId = 401, DisplayOrder = 5 },
                new Category { Id = 407, Name = "Мала побутова техніка", ParentCategoryId = 4, DisplayOrder = 2 },
                new Category { Id = 408, Name = "Пилососи", ParentCategoryId = 407, DisplayOrder = 1 },
                new Category { Id = 409, Name = "Праски", ParentCategoryId = 407, DisplayOrder = 2 },
                new Category { Id = 410, Name = "Блендери, міксери", ParentCategoryId = 407, DisplayOrder = 3 },
                new Category { Id = 411, Name = "Кавомашини", ParentCategoryId = 407, DisplayOrder = 4 },
                new Category { Id = 412, Name = "Електрочайники", ParentCategoryId = 407, DisplayOrder = 5 },
                new Category { Id = 413, Name = "Фени", ParentCategoryId = 407, DisplayOrder = 6 },
                new Category { Id = 414, Name = "Кліматична техніка", ParentCategoryId = 4, DisplayOrder = 3 },
                new Category { Id = 415, Name = "Кондиціонери", ParentCategoryId = 414, DisplayOrder = 1 },
                new Category { Id = 416, Name = "Обігрівачі", ParentCategoryId = 414, DisplayOrder = 2 },
                new Category { Id = 417, Name = "Зволожувачі повітря", ParentCategoryId = 414, DisplayOrder = 3 },
                new Category { Id = 418, Name = "Вентилятори", ParentCategoryId = 414, DisplayOrder = 4 }
            );

            // Seed брендов с логотипами
            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, Name = "Samsung", Logo = "/images/brands/samsung.png" },
                new Brand { Id = 2, Name = "Apple", Logo = "/images/brands/apple.png" },
                new Brand { Id = 3, Name = "Xiaomi", Logo = "/images/brands/xiaomi.png" },
                new Brand { Id = 4, Name = "Sony", Logo = "/images/brands/sony.png" },
                new Brand { Id = 5, Name = "LG", Logo = "/images/brands/lg.png" },
                new Brand { Id = 6, Name = "Bosch", Logo = "/images/brands/bosch.png" },
                new Brand { Id = 7, Name = "Adidas", Logo = "/images/brands/adidas.png" },
                new Brand { Id = 8, Name = "Nike", Logo = "/images/brands/nike.png" },
                new Brand { Id = 9, Name = "Puma", Logo = "/images/brands/puma.png" },
                new Brand { Id = 10, Name = "Zara", Logo = "/images/brands/zara.png" },
                new Brand { Id = 11, Name = "H&M", Logo = "/images/brands/hm.png" },
                new Brand { Id = 12, Name = "Asus", Logo = "/images/brands/asus.png" },
                new Brand { Id = 13, Name = "Acer", Logo = "/images/brands/acer.png" },
                new Brand { Id = 14, Name = "HP", Logo = "/images/brands/hp.png" },
                new Brand { Id = 15, Name = "Lenovo", Logo = "/images/brands/lenovo.png" },
                new Brand { Id = 16, Name = "Dell", Logo = "/images/brands/dell.png" },
                new Brand { Id = 17, Name = "Canon", Logo = "/images/brands/canon.png" },
                new Brand { Id = 18, Name = "Epson", Logo = "/images/brands/epson.png" },
                new Brand { Id = 19, Name = "Makita", Logo = "/images/brands/makita.png" },
                new Brand { Id = 20, Name = "DeWalt", Logo = "/images/brands/dewalt.png" }
            );

            // Связи категория-бренд
            modelBuilder.Entity<CategoryBrand>().HasData(
                new CategoryBrand { CategoryId = 1, BrandId = 1 },
                new CategoryBrand { CategoryId = 1, BrandId = 2 },
                new CategoryBrand { CategoryId = 1, BrandId = 3 },
                new CategoryBrand { CategoryId = 1, BrandId = 4 },
                new CategoryBrand { CategoryId = 1, BrandId = 5 },
                new CategoryBrand { CategoryId = 1, BrandId = 12 },
                new CategoryBrand { CategoryId = 1, BrandId = 13 },
                new CategoryBrand { CategoryId = 1, BrandId = 14 },
                new CategoryBrand { CategoryId = 1, BrandId = 15 },
                new CategoryBrand { CategoryId = 1, BrandId = 16 },
                new CategoryBrand { CategoryId = 201, BrandId = 1 },
                new CategoryBrand { CategoryId = 201, BrandId = 2 },
                new CategoryBrand { CategoryId = 201, BrandId = 3 },
                new CategoryBrand { CategoryId = 201, BrandId = 4 },
                new CategoryBrand { CategoryId = 10, BrandId = 7 },
                new CategoryBrand { CategoryId = 10, BrandId = 8 },
                new CategoryBrand { CategoryId = 10, BrandId = 9 },
                new CategoryBrand { CategoryId = 10, BrandId = 10 },
                new CategoryBrand { CategoryId = 10, BrandId = 11 },
                new CategoryBrand { CategoryId = 9, BrandId = 7 },
                new CategoryBrand { CategoryId = 9, BrandId = 8 },
                new CategoryBrand { CategoryId = 9, BrandId = 9 },
                new CategoryBrand { CategoryId = 6, BrandId = 19 },
                new CategoryBrand { CategoryId = 6, BrandId = 20 },
                new CategoryBrand { CategoryId = 6, BrandId = 6 }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "test@example.com",
                    PasswordHash = "123456", // В реальном проекте нужно хэшировать
                    Name = "Test User",
                    IsAdmin = false
                }
            );

            // Очищаем предыдущие seed данные для Item (если они были)
            modelBuilder.Entity<Item>().HasData(new List<Item>());

            // Добавляем с отрицательными ID
            var items = new List<Item>();
            int itemId = -1000;

            var categoryIds = new[] { 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 121, 122, 123, 201, 202, 203, 204, 205 };

            foreach (var categoryId in categoryIds)
            {
                for (int i = 1; i <= 3; i++)
                {
                    items.Add(new Item
                    {
                        Id = itemId--,
                        Name = $"Товар {i} категорії {categoryId}",
                        Desc = $"Опис товару {i}",
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
        }
    }
}