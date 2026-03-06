using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BazaR.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Complects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryBrands",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryBrands", x => new { x.CategoryId, x.BrandId });
                    table.ForeignKey(
                        name: "FK_CategoryBrands_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryBrands_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryFilters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryFilters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryFilters_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Garantia = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ttn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComplectItems",
                columns: table => new
                {
                    ComplectId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplectItems", x => new { x.ComplectId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_ComplectItems_Complects_ComplectId",
                        column: x => x.ComplectId,
                        principalTable: "Complects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComplectItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendingPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    SendingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliveries_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemCharacteristics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCharacteristics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemCharacteristics_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemColors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemColors_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Uslugi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uslugi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uslugi_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishlistItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishlistItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WishlistItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PriceAtMoment = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Logo", "Name" },
                values: new object[,]
                {
                    { 1, "/images/brands/samsung.png", "Samsung" },
                    { 2, "/images/brands/apple.png", "Apple" },
                    { 3, "/images/brands/xiaomi.png", "Xiaomi" },
                    { 4, "/images/brands/sony.png", "Sony" },
                    { 5, "/images/brands/lg.png", "LG" },
                    { 6, "/images/brands/bosch.png", "Bosch" },
                    { 7, "/images/brands/adidas.png", "Adidas" },
                    { 8, "/images/brands/nike.png", "Nike" },
                    { 9, "/images/brands/puma.png", "Puma" },
                    { 10, "/images/brands/zara.png", "Zara" },
                    { 11, "/images/brands/hm.png", "H&M" },
                    { 12, "/images/brands/asus.png", "Asus" },
                    { 13, "/images/brands/acer.png", "Acer" },
                    { 14, "/images/brands/hp.png", "HP" },
                    { 15, "/images/brands/lenovo.png", "Lenovo" },
                    { 16, "/images/brands/dell.png", "Dell" },
                    { 17, "/images/brands/canon.png", "Canon" },
                    { 18, "/images/brands/epson.png", "Epson" },
                    { 19, "/images/brands/makita.png", "Makita" },
                    { 20, "/images/brands/dewalt.png", "DeWalt" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "IconUrl", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { 1, 1, "bi-laptop", "Ноутбуки", null },
                    { 2, 2, "bi-phone", "Смартфони, ТВ та електроніка", null },
                    { 3, 3, "bi-controller", "Товари для геймерів", null },
                    { 4, 4, "bi-fan", "Побутова техніка", null },
                    { 5, 5, "bi-house", "Товари для дому", null },
                    { 6, 6, "bi-tools", "Інструменти та автотовари", null },
                    { 7, 7, "bi-droplet", "Сантехніка та ремонт", null },
                    { 8, 8, "bi-flower", "Дача, сад та город", null },
                    { 9, 9, "bi-bicycle", "Спорт та захоплення", null },
                    { 10, 10, "bi-tag", "Одяг, взуття та прикраси", null },
                    { 11, 11, "bi-heart", "Краса і здоров'я", null },
                    { 12, 12, "bi-emoji-smile", "Дитячі товари", null },
                    { 13, 13, "bi-bug", "Зоотовари", null },
                    { 14, 14, "bi-pencil", "Канцтовари та книги", null },
                    { 15, 15, "bi-cup-straw", "Алкогольні напої та продукти", null },
                    { 16, 16, "bi-briefcase", "Товари для бізнесу та послуги", null },
                    { 17, 17, "bi-tree", "Тури та відпочинок", null },
                    { 18, 18, "bi-percent", "Акції", null },
                    { 19, 19, "bi-fire", "Тотальний розпродаж", null }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Київ" },
                    { 2, "Харків" },
                    { 3, "Одеса" },
                    { 4, "Дніпро" },
                    { 5, "Львів" },
                    { 6, "Запоріжжя" },
                    { 7, "Миколаїв" },
                    { 8, "Вінниця" },
                    { 9, "Херсон" },
                    { 10, "Полтава" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsAdmin", "Name", "PasswordHash" },
                values: new object[] { 1, "test@example.com", false, "Test User", "123456" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "IconUrl", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { 101, 1, null, "Asus", 1 },
                    { 102, 2, null, "Acer", 1 },
                    { 103, 3, null, "HP", 1 },
                    { 104, 4, null, "Lenovo", 1 },
                    { 105, 5, null, "Dell", 1 },
                    { 106, 6, null, "Apple", 1 },
                    { 107, 7, null, "Аксесуари для ноутбуків і ПК", 1 },
                    { 120, 8, null, "Комп'ютери", 1 },
                    { 130, 9, null, "Комплектуючі", 1 },
                    { 140, 10, null, "Мережеве обладнання", 1 },
                    { 150, 11, null, "Серверне обладнання", 1 },
                    { 160, 12, null, "Оргтехніка", 1 },
                    { 170, 13, null, "Програмне забезпечення", 1 },
                    { 201, 1, null, "Смартфони", 2 },
                    { 202, 2, null, "Телевізори", 2 },
                    { 203, 3, null, "Планшети", 2 },
                    { 204, 4, null, "Аудіотехніка", 2 },
                    { 208, 5, null, "Фотоапарати", 2 },
                    { 209, 6, null, "Відеокамери", 2 },
                    { 210, 7, null, "Розумний дім", 2 },
                    { 211, 8, null, "Аксесуари до телефонів", 2 },
                    { 301, 1, null, "PlayStation", 3 },
                    { 302, 2, null, "Xbox", 3 },
                    { 303, 3, null, "Nintendo", 3 },
                    { 304, 4, null, "Ігрові консолі та приставки", 3 },
                    { 305, 5, null, "Джойстики та аксесуари", 3 },
                    { 306, 6, null, "Ігри", 3 },
                    { 307, 7, null, "Ігрові поверхні", 3 },
                    { 308, 8, null, "Ігрові крісла", 3 },
                    { 309, 9, null, "Ігрові миші", 3 },
                    { 310, 10, null, "Ігрові клавіатури", 3 },
                    { 311, 11, null, "Ігрові навушники", 3 },
                    { 401, 1, null, "Велика побутова техніка", 4 },
                    { 407, 2, null, "Мала побутова техніка", 4 },
                    { 414, 3, null, "Кліматична техніка", 4 }
                });

            migrationBuilder.InsertData(
                table: "CategoryBrands",
                columns: new[] { "BrandId", "CategoryId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 12, 1 },
                    { 13, 1 },
                    { 14, 1 },
                    { 15, 1 },
                    { 16, 1 },
                    { 6, 6 },
                    { 19, 6 },
                    { 20, 6 },
                    { 7, 9 },
                    { 8, 9 },
                    { 9, 9 },
                    { 7, 10 },
                    { 8, 10 },
                    { 9, 10 },
                    { 10, 10 },
                    { 11, 10 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "IconUrl", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { 108, 1, null, "Флеш пам'ять USB", 107 },
                    { 109, 2, null, "Сумки та рюкзаки для ноутбуків", 107 },
                    { 110, 3, null, "Підставки та столики для ноутбуків", 107 },
                    { 111, 4, null, "Веб-камери", 107 },
                    { 121, 1, null, "Монітори", 120 },
                    { 122, 2, null, "Миші", 120 },
                    { 123, 3, null, "Клавіатури", 120 },
                    { 124, 4, null, "Комплект: клавіатури + миші", 120 },
                    { 125, 5, null, "Мережеві сховища (NAS)", 120 },
                    { 131, 1, null, "Відеокарти", 130 },
                    { 132, 2, null, "Жорсткі диски та дискові масиви", 130 },
                    { 133, 3, null, "Процесори", 130 },
                    { 134, 4, null, "SSD", 130 },
                    { 135, 5, null, "Оперативна пам'ять", 130 },
                    { 136, 6, null, "Материнські плати", 130 },
                    { 137, 7, null, "Блоки живлення", 130 },
                    { 141, 1, null, "Патч-корди", 140 },
                    { 142, 2, null, "Маршрутизатори", 140 },
                    { 143, 3, null, "IP-камери", 140 },
                    { 144, 4, null, "Комутатори", 140 },
                    { 145, 5, null, "Бездротові точки доступу", 140 },
                    { 161, 1, null, "БФП/Принтери", 160 },
                    { 162, 2, null, "Проектори", 160 },
                    { 163, 3, null, "Витратні матеріали для принтерів", 160 },
                    { 164, 4, null, "Телефонні апарати", 160 },
                    { 171, 1, null, "Операційні системи", 170 },
                    { 172, 2, null, "Офісні програми", 170 },
                    { 173, 3, null, "Антивірусні програми", 170 },
                    { 205, 1, null, "Навушники", 204 },
                    { 206, 2, null, "Колонки", 204 },
                    { 207, 3, null, "MP3-плеєри", 204 },
                    { 212, 1, null, "Чохли для телефонів", 211 },
                    { 213, 2, null, "Захисні скельця", 211 },
                    { 214, 3, null, "Зарядні пристрої", 211 },
                    { 215, 4, null, "Power Bank", 211 },
                    { 402, 1, null, "Холодильники", 401 },
                    { 403, 2, null, "Пральні машини", 401 },
                    { 404, 3, null, "Плити та духовки", 401 },
                    { 405, 4, null, "Мікрохвильові печі", 401 },
                    { 406, 5, null, "Посудомийні машини", 401 },
                    { 408, 1, null, "Пилососи", 407 },
                    { 409, 2, null, "Праски", 407 },
                    { 410, 3, null, "Блендери, міксери", 407 },
                    { 411, 4, null, "Кавомашини", 407 },
                    { 412, 5, null, "Електрочайники", 407 },
                    { 413, 6, null, "Фени", 407 },
                    { 415, 1, null, "Кондиціонери", 414 },
                    { 416, 2, null, "Обігрівачі", 414 },
                    { 417, 3, null, "Зволожувачі повітря", 414 },
                    { 418, 4, null, "Вентилятори", 414 }
                });

            migrationBuilder.InsertData(
                table: "CategoryBrands",
                columns: new[] { "BrandId", "CategoryId" },
                values: new object[,]
                {
                    { 1, 201 },
                    { 2, 201 },
                    { 3, 201 },
                    { 4, 201 }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "BrandId", "CategoryId", "Desc", "Garantia", "ImageUrl", "IsAvailable", "Name", "Price", "UserId" },
                values: new object[,]
                {
                    { -1050, 1, 204, "Опис товару 3", 12, "/images/items/default.jpg", true, "Товар 3 категорії 204", 2500, 1 },
                    { -1049, 1, 204, "Опис товару 2", 12, "/images/items/default.jpg", true, "Товар 2 категорії 204", 2000, 1 },
                    { -1048, 1, 204, "Опис товару 1", 12, "/images/items/default.jpg", true, "Товар 1 категорії 204", 1500, 1 },
                    { -1047, 1, 203, "Опис товару 3", 12, "/images/items/default.jpg", true, "Товар 3 категорії 203", 2500, 1 },
                    { -1046, 1, 203, "Опис товару 2", 12, "/images/items/default.jpg", true, "Товар 2 категорії 203", 2000, 1 },
                    { -1045, 1, 203, "Опис товару 1", 12, "/images/items/default.jpg", true, "Товар 1 категорії 203", 1500, 1 },
                    { -1044, 1, 202, "Опис товару 3", 12, "/images/items/default.jpg", true, "Товар 3 категорії 202", 2500, 1 },
                    { -1043, 1, 202, "Опис товару 2", 12, "/images/items/default.jpg", true, "Товар 2 категорії 202", 2000, 1 },
                    { -1042, 1, 202, "Опис товару 1", 12, "/images/items/default.jpg", true, "Товар 1 категорії 202", 1500, 1 },
                    { -1041, 1, 201, "Опис товару 3", 12, "/images/items/default.jpg", true, "Товар 3 категорії 201", 2500, 1 },
                    { -1040, 1, 201, "Опис товару 2", 12, "/images/items/default.jpg", true, "Товар 2 категорії 201", 2000, 1 },
                    { -1039, 1, 201, "Опис товару 1", 12, "/images/items/default.jpg", true, "Товар 1 категорії 201", 1500, 1 },
                    { -1020, 1, 107, "Опис товару 3", 12, "/images/items/default.jpg", true, "Товар 3 категорії 107", 2500, 1 },
                    { -1019, 1, 107, "Опис товару 2", 12, "/images/items/default.jpg", true, "Товар 2 категорії 107", 2000, 1 },
                    { -1018, 1, 107, "Опис товару 1", 12, "/images/items/default.jpg", true, "Товар 1 категорії 107", 1500, 1 },
                    { -1017, 1, 106, "Опис товару 3", 12, "/images/items/default.jpg", true, "Товар 3 категорії 106", 2500, 1 },
                    { -1016, 1, 106, "Опис товару 2", 12, "/images/items/default.jpg", true, "Товар 2 категорії 106", 2000, 1 },
                    { -1015, 1, 106, "Опис товару 1", 12, "/images/items/default.jpg", true, "Товар 1 категорії 106", 1500, 1 },
                    { -1014, 1, 105, "Опис товару 3", 12, "/images/items/default.jpg", true, "Товар 3 категорії 105", 2500, 1 },
                    { -1013, 1, 105, "Опис товару 2", 12, "/images/items/default.jpg", true, "Товар 2 категорії 105", 2000, 1 },
                    { -1012, 1, 105, "Опис товару 1", 12, "/images/items/default.jpg", true, "Товар 1 категорії 105", 1500, 1 },
                    { -1011, 1, 104, "Опис товару 3", 12, "/images/items/default.jpg", true, "Товар 3 категорії 104", 2500, 1 },
                    { -1010, 1, 104, "Опис товару 2", 12, "/images/items/default.jpg", true, "Товар 2 категорії 104", 2000, 1 },
                    { -1009, 1, 104, "Опис товару 1", 12, "/images/items/default.jpg", true, "Товар 1 категорії 104", 1500, 1 },
                    { -1008, 1, 103, "Опис товару 3", 12, "/images/items/default.jpg", true, "Товар 3 категорії 103", 2500, 1 },
                    { -1007, 1, 103, "Опис товару 2", 12, "/images/items/default.jpg", true, "Товар 2 категорії 103", 2000, 1 },
                    { -1006, 1, 103, "Опис товару 1", 12, "/images/items/default.jpg", true, "Товар 1 категорії 103", 1500, 1 },
                    { -1005, 1, 102, "Опис товару 3", 12, "/images/items/default.jpg", true, "Товар 3 категорії 102", 2500, 1 },
                    { -1004, 1, 102, "Опис товару 2", 12, "/images/items/default.jpg", true, "Товар 2 категорії 102", 2000, 1 },
                    { -1003, 1, 102, "Опис товару 1", 12, "/images/items/default.jpg", true, "Товар 1 категорії 102", 1500, 1 },
                    { -1002, 1, 101, "Опис товару 3", 12, "/images/items/default.jpg", true, "Товар 3 категорії 101", 2500, 1 },
                    { -1001, 1, 101, "Опис товару 2", 12, "/images/items/default.jpg", true, "Товар 2 категорії 101", 2000, 1 },
                    { -1000, 1, 101, "Опис товару 1", 12, "/images/items/default.jpg", true, "Товар 1 категорії 101", 1500, 1 },
                    { -1053, 1, 205, "Опис товару 3", 12, "/images/items/default.jpg", true, "Товар 3 категорії 205", 2500, 1 },
                    { -1052, 1, 205, "Опис товару 2", 12, "/images/items/default.jpg", true, "Товар 2 категорії 205", 2000, 1 },
                    { -1051, 1, 205, "Опис товару 1", 12, "/images/items/default.jpg", true, "Товар 1 категорії 205", 1500, 1 },
                    { -1038, 1, 123, "Опис товару 3", 12, "/images/items/default.jpg", true, "Товар 3 категорії 123", 2500, 1 },
                    { -1037, 1, 123, "Опис товару 2", 12, "/images/items/default.jpg", true, "Товар 2 категорії 123", 2000, 1 },
                    { -1036, 1, 123, "Опис товару 1", 12, "/images/items/default.jpg", true, "Товар 1 категорії 123", 1500, 1 },
                    { -1035, 1, 122, "Опис товару 3", 12, "/images/items/default.jpg", true, "Товар 3 категорії 122", 2500, 1 },
                    { -1034, 1, 122, "Опис товару 2", 12, "/images/items/default.jpg", true, "Товар 2 категорії 122", 2000, 1 },
                    { -1033, 1, 122, "Опис товару 1", 12, "/images/items/default.jpg", true, "Товар 1 категорії 122", 1500, 1 },
                    { -1032, 1, 121, "Опис товару 3", 12, "/images/items/default.jpg", true, "Товар 3 категорії 121", 2500, 1 },
                    { -1031, 1, 121, "Опис товару 2", 12, "/images/items/default.jpg", true, "Товар 2 категорії 121", 2000, 1 },
                    { -1030, 1, 121, "Опис товару 1", 12, "/images/items/default.jpg", true, "Товар 1 категорії 121", 1500, 1 },
                    { -1029, 1, 110, "Опис товару 3", 12, "/images/items/default.jpg", true, "Товар 3 категорії 110", 2500, 1 },
                    { -1028, 1, 110, "Опис товару 2", 12, "/images/items/default.jpg", true, "Товар 2 категорії 110", 2000, 1 },
                    { -1027, 1, 110, "Опис товару 1", 12, "/images/items/default.jpg", true, "Товар 1 категорії 110", 1500, 1 },
                    { -1026, 1, 109, "Опис товару 3", 12, "/images/items/default.jpg", true, "Товар 3 категорії 109", 2500, 1 },
                    { -1025, 1, 109, "Опис товару 2", 12, "/images/items/default.jpg", true, "Товар 2 категорії 109", 2000, 1 },
                    { -1024, 1, 109, "Опис товару 1", 12, "/images/items/default.jpg", true, "Товар 1 категорії 109", 1500, 1 },
                    { -1023, 1, 108, "Опис товару 3", 12, "/images/items/default.jpg", true, "Товар 3 категорії 108", 2500, 1 },
                    { -1022, 1, 108, "Опис товару 2", 12, "/images/items/default.jpg", true, "Товар 2 категорії 108", 2000, 1 },
                    { -1021, 1, 108, "Опис товару 1", 12, "/images/items/default.jpg", true, "Товар 1 категорії 108", 1500, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brands_Name",
                table: "Brands",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ItemId",
                table: "CartItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_UserId",
                table: "CartItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryBrands_BrandId",
                table: "CategoryBrands",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryFilters_CategoryId",
                table: "CategoryFilters",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplectItems_ItemId",
                table: "ComplectItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_ItemId",
                table: "Deliveries",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCharacteristics_ItemId",
                table: "ItemCharacteristics",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemColors_ItemId",
                table: "ItemColors",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_BrandId",
                table: "Items",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_IsAvailable",
                table: "Items",
                column: "IsAvailable");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Name",
                table: "Items",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Price",
                table: "Items",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UserId",
                table: "Items",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ItemId",
                table: "OrderItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CityId",
                table: "Orders",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ItemId",
                table: "Reviews",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Uslugi_ItemId",
                table: "Uslugi",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_ItemId",
                table: "WishlistItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_UserId",
                table: "WishlistItems",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "CategoryBrands");

            migrationBuilder.DropTable(
                name: "CategoryFilters");

            migrationBuilder.DropTable(
                name: "ComplectItems");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "ItemCharacteristics");

            migrationBuilder.DropTable(
                name: "ItemColors");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Uslugi");

            migrationBuilder.DropTable(
                name: "WishlistItems");

            migrationBuilder.DropTable(
                name: "Complects");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
