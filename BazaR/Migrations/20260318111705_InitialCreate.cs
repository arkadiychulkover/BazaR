using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BazaR.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
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
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ImgUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SearchItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchItems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
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
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Garantia = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsReadyToSend = table.Column<bool>(type: "bit", nullable: false),
                    IsNoPercentCredit = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SellerType = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DeliveryMethod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ttn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
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
                        name: "FK_CartItems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryFilters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ValueType = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_CategoryFilters_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
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
                    DeliveryPlace = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SendingPlace = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
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
                    Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
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
                    Color = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
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
                    Comment = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Uslugi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
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
                        name: "FK_WishlistItems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
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
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsAdmin", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "00000000-0000-0000-0000-000000000001", "admin@example.com", true, true, false, null, "Admin User", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", null, null, false, "STATIC-SEED-STAMP-1", false, "admin@example.com" },
                    { 2, 0, "00000000-0000-0000-0000-000000000002", "test@example.com", true, false, false, null, "Test User", "TEST@EXAMPLE.COM", "TEST@EXAMPLE.COM", null, null, false, "STATIC-SEED-STAMP-2", false, "test@example.com" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Logo", "Name" },
                values: new object[,]
                {
                    { 1, "/images/brands/apple.png", "Apple" },
                    { 2, "/images/brands/samsung.png", "Samsung" },
                    { 3, "/images/brands/xiaomi.png", "Xiaomi" },
                    { 4, "/images/brands/sony.png", "Sony" },
                    { 5, "/images/brands/lg.png", "LG" },
                    { 6, "/images/brands/bosch.png", "Bosch" },
                    { 7, "/images/brands/nike.png", "Nike" },
                    { 8, "/images/brands/adidas.png", "Adidas" },
                    { 9, "/images/brands/puma.png", "Puma" },
                    { 10, "/images/brands/zara.png", "Zara" },
                    { 11, "/images/brands/hm.png", "H&M" },
                    { 12, "/images/brands/dell.png", "Dell" },
                    { 13, "/images/brands/hp.png", "HP" },
                    { 14, "/images/brands/lenovo.png", "Lenovo" },
                    { 15, "/images/brands/asus.png", "Asus" },
                    { 16, "/images/brands/acer.png", "Acer" },
                    { 17, "/images/brands/microsoft.png", "Microsoft" },
                    { 18, "/images/brands/canon.png", "Canon" },
                    { 19, "/images/brands/nikon.png", "Nikon" },
                    { 20, "/images/brands/panasonic.png", "Panasonic" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "IconUrl", "ImgUrl", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { 1, 1, "icon-laptops-and-computers.svg", "categoryImg-laptops-and-computers.svg", "Ноутбуки та комп'ютери", null },
                    { 2, 2, "icon-smartphones-tv-electronics.svg", "categoryImg-smartphones-tv-electronics.svg", "Смартфони, ТВ та електроніка", null },
                    { 3, 3, "icon-gaming.svg", "categoryImg-gaming.svg", "Товари для геймерів", null },
                    { 4, 4, "icon-home-appliances.svg", "categoryImg-home-appliances.svg", "Побутова техніка", null },
                    { 5, 5, "icon-home-goods.svg", "categoryImg-home-goods.svg", "Товари для дому", null },
                    { 6, 6, "icon-tools-auto.svg", "categoryImg-tools-auto.svg", "Інструменти та автотовари", null },
                    { 7, 7, "icon-plumbing-renovation.svg", "categoryImg-plumbing-renovation.svg", "Сантехніка та ремонт", null },
                    { 8, 8, "icon-garden.svg", "categoryImg-garden.svg", "Дача, сад та город", null },
                    { 9, 9, "icon-sports-hobbies.svg", "categoryImg-sports-hobbies.svg", "Спорт та захоплення", null },
                    { 10, 10, "icon-clothing-footwear-jewelry.svg", "categoryImg-clothing-footwear-jewelry.svg", "Одяг, взуття та прикраси", null },
                    { 11, 11, "icon-beauty-health.svg", "categoryImg-beauty-health.svg", "Краса і здоров'я", null },
                    { 12, 12, "icon-baby-products.svg", "categoryImg-baby-products.svg", "Дитячі товари", null },
                    { 13, 13, "icon-pet-supplies.svg", "categoryImg-pet-supplies.svg", "Зоотовари", null },
                    { 14, 14, "icon-stationery-books.svg", "categoryImg-stationery-books.svg", "Канцтовари та книги", null },
                    { 15, 15, "icon-alcohol-food.svg", "categoryImg-alcohol-food.svg", "Алкогольні напої та продукти", null },
                    { 16, 16, "icon-business-services.svg", "categoryImg-business-services.svg", "Товари для бізнесу та послуги", null },
                    { 17, 17, "icon-tourism-outdoor.svg", "categoryImg-tourism-outdoor.svg", "Туризм та відпочинок", null },
                    { 18, 18, "icon-promotions.svg", "categoryImg-promotions.svg", "Акції", null },
                    { 19, 19, "icon-total-sale.svg", "categoryImg-total-sale.svg", "Тотальний розпродаж", null }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Kyiv" },
                    { 2, "Kharkiv" },
                    { 3, "Odesa" },
                    { 4, "Dnipro" },
                    { 5, "Lviv" },
                    { 6, "Zaporizhzhia" },
                    { 7, "Mykolaiv" },
                    { 8, "Vinnytsia" },
                    { 9, "Kherson" },
                    { 10, "Poltava" },
                    { 11, "Chernihiv" },
                    { 12, "Cherkasy" },
                    { 13, "Zhytomyr" },
                    { 14, "Sumy" },
                    { 15, "Rivne" },
                    { 16, "Ternopil" },
                    { 17, "Lutsk" },
                    { 18, "Uzhhorod" },
                    { 19, "Chernivtsi" },
                    { 20, "IvanoFrankivsk" },
                    { 21, "Kropyvnytskyi" },
                    { 22, "Khmelnytskyi" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "IconUrl", "ImgUrl", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { 101, 1, "categoryIcon-Notebooks.svg", "categoryImg-Notebooks.svg", "Ноутбуки", 1 },
                    { 102, 2, "categoryIcon-GamingNotebooks.svg", "categoryImg-GamingNotebooks.svg", "Ігрові ноутбуки", 1 },
                    { 103, 3, "categoryIcon-Ultrabooks.svg", "categoryImg-Ultrabooks.svg", "Ультрабуки", 1 },
                    { 104, 4, "categoryIcon-ForStudy.svg", "categoryImg-ForStudy.svg", "Для навчання", 1 },
                    { 105, 5, "categoryIcon-ForWork.svg", "categoryImg-ForWork.svg", "Для роботи", 1 },
                    { 106, 6, "categoryIcon-Chromebook.svg", "categoryImg-Chromebook.svg", "Chromebook", 1 },
                    { 107, 7, "categoryIcon-Computers.svg", "categoryImg-Computers.svg", "Комп'ютери", 1 },
                    { 108, 8, "categoryIcon-DesktopPC.svg", "categoryImg-DesktopPC.svg", "Настільні ПК", 1 },
                    { 109, 9, "categoryIcon-GamingPC.svg", "categoryImg-GamingPC.svg", "Ігрові ПК", 1 },
                    { 110, 10, "categoryIcon-MiniPC.svg", "categoryImg-MiniPC.svg", "Міні-ПК", 1 },
                    { 111, 11, "categoryIcon-Monoblocks.svg", "categoryImg-Monoblocks.svg", "Моноблоки", 1 },
                    { 112, 12, "categoryIcon-Workstations.svg", "categoryImg-Workstations.svg", "Робочі станції", 1 },
                    { 113, 13, "categoryIcon-Components.svg", "categoryImg-Components.svg", "Комплектуючі", 1 },
                    { 114, 14, "categoryIcon-Processors.svg", "categoryImg-Processors.svg", "Процесори", 1 },
                    { 115, 15, "categoryIcon-VideoCards.svg", "categoryImg-VideoCards.svg", "Відеокарти", 1 },
                    { 116, 16, "categoryIcon-Motherboards.svg", "categoryImg-Motherboards.svg", "Материнські плати", 1 },
                    { 117, 17, "categoryIcon-RAM.svg", "categoryImg-RAM.svg", "Оперативна пам'ять", 1 },
                    { 118, 18, "categoryIcon-PowerSupplies.svg", "categoryImg-PowerSupplies.svg", "Блоки живлення", 1 },
                    { 119, 19, "categoryIcon-Cases.svg", "categoryImg-Cases.svg", "Корпуси", 1 },
                    { 120, 20, "categoryIcon-Storage.svg", "categoryImg-Storage.svg", "Накопичувачі", 1 },
                    { 121, 21, "categoryIcon-SSD.svg", "categoryImg-SSD.svg", "SSD", 1 },
                    { 122, 22, "categoryIcon-HDD.svg", "categoryImg-HDD.svg", "HDD", 1 },
                    { 123, 23, "categoryIcon-ExternalDrives.svg", "categoryImg-ExternalDrives.svg", "Зовнішні диски", 1 },
                    { 124, 24, "categoryIcon-NAS.svg", "categoryImg-NAS.svg", "NAS", 1 },
                    { 125, 25, "categoryIcon-Peripherals.svg", "categoryImg-Peripherals.svg", "Периферія", 1 },
                    { 126, 26, "categoryIcon-Keyboards.svg", "categoryImg-Keyboards.svg", "Клавіатури", 1 },
                    { 127, 27, "categoryIcon-Mice.svg", "categoryImg-Mice.svg", "Миші", 1 },
                    { 128, 28, "categoryIcon-MousePads.svg", "categoryImg-MousePads.svg", "Килимки", 1 },
                    { 129, 29, "categoryIcon-Webcams.svg", "categoryImg-Webcams.svg", "Вебкамери", 1 },
                    { 201, 1, "categoryIcon-Smartphones.svg", "categoryImg-Smartphones.svg", "Смартфони", 2 },
                    { 202, 2, "categoryIcon-Android.svg", "categoryImg-Android.svg", "Android", 2 },
                    { 203, 3, "categoryIcon-iPhone.svg", "categoryImg-iPhone.svg", "iPhone", 2 },
                    { 204, 4, "categoryIcon-Budget.svg", "categoryImg-Budget.svg", "Бюджетні", 2 },
                    { 205, 5, "categoryIcon-Flagship.svg", "categoryImg-Flagship.svg", "Флагмани", 2 },
                    { 206, 6, "categoryIcon-TVs.svg", "categoryImg-TVs.svg", "Телевізори", 2 },
                    { 207, 7, "categoryIcon-SmartTV.svg", "categoryImg-SmartTV.svg", "Smart TV", 2 },
                    { 208, 8, "categoryIcon-LED.svg", "categoryImg-LED.svg", "LED", 2 },
                    { 209, 9, "categoryIcon-OLED.svg", "categoryImg-OLED.svg", "OLED", 2 },
                    { 210, 10, "categoryIcon-QLED.svg", "categoryImg-QLED.svg", "QLED", 2 },
                    { 211, 11, "categoryIcon-Audio.svg", "categoryImg-Audio.svg", "Аудіо", 2 },
                    { 212, 12, "categoryIcon-Headphones.svg", "categoryImg-Headphones.svg", "Навушники", 2 },
                    { 213, 13, "categoryIcon-Soundbars.svg", "categoryImg-Soundbars.svg", "Саундбари", 2 },
                    { 214, 14, "categoryIcon-Speakers.svg", "categoryImg-Speakers.svg", "Колонки", 2 },
                    { 215, 15, "categoryIcon-HomeTheaters.svg", "categoryImg-HomeTheaters.svg", "Домашні кінотеатри", 2 },
                    { 216, 16, "categoryIcon-Tablets.svg", "categoryImg-Tablets.svg", "Планшети", 2 },
                    { 217, 17, "categoryIcon-iPad.svg", "categoryImg-iPad.svg", "iPad", 2 },
                    { 218, 18, "categoryIcon-AndroidTablets.svg", "categoryImg-AndroidTablets.svg", "Android планшети", 2 },
                    { 219, 19, "categoryIcon-Gadgets.svg", "categoryImg-Gadgets.svg", "Гаджети", 2 },
                    { 220, 20, "categoryIcon-SmartWatches.svg", "categoryImg-SmartWatches.svg", "Смарт-годинники", 2 },
                    { 221, 21, "categoryIcon-FitnessBands.svg", "categoryImg-FitnessBands.svg", "Фітнес-браслети", 2 },
                    { 301, 1, "categoryIcon-Consoles.svg", "categoryImg-Consoles.svg", "Консолі", 3 },
                    { 302, 2, "categoryIcon-PlayStation.svg", "categoryImg-PlayStation.svg", "PlayStation", 3 },
                    { 303, 3, "categoryIcon-Xbox.svg", "categoryImg-Xbox.svg", "Xbox", 3 },
                    { 304, 4, "categoryIcon-Nintendo.svg", "categoryImg-Nintendo.svg", "Nintendo", 3 },
                    { 305, 5, "categoryIcon-Games.svg", "categoryImg-Games.svg", "Ігри", 3 },
                    { 306, 6, "categoryIcon-PlayStationGames.svg", "categoryImg-PlayStationGames.svg", "PlayStation ігри", 3 },
                    { 307, 7, "categoryIcon-XboxGames.svg", "categoryImg-XboxGames.svg", "Xbox ігри", 3 },
                    { 308, 8, "categoryIcon-PCGames.svg", "categoryImg-PCGames.svg", "PC ігри", 3 },
                    { 309, 9, "categoryIcon-GamingPeripherals.svg", "categoryImg-GamingPeripherals.svg", "Геймерська периферія", 3 },
                    { 310, 10, "categoryIcon-GamingMice.svg", "categoryImg-GamingMice.svg", "Ігрові миші", 3 },
                    { 311, 11, "categoryIcon-GamingKeyboards.svg", "categoryImg-GamingKeyboards.svg", "Ігрові клавіатури", 3 },
                    { 312, 12, "categoryIcon-GamingHeadphones.svg", "categoryImg-GamingHeadphones.svg", "Геймерські навушники", 3 },
                    { 313, 13, "categoryIcon-VR.svg", "categoryImg-VR.svg", "VR", 3 },
                    { 314, 14, "categoryIcon-VRHeadsets.svg", "categoryImg-VRHeadsets.svg", "VR шоломи", 3 },
                    { 315, 15, "categoryIcon-VRAccessories.svg", "categoryImg-VRAccessories.svg", "VR аксесуари", 3 },
                    { 401, 1, "categoryIcon-LargeAppliances.svg", "categoryImg-LargeAppliances.svg", "Велика техніка", 4 },
                    { 402, 2, "categoryIcon-Refrigerators.svg", "categoryImg-Refrigerators.svg", "Холодильники", 4 },
                    { 403, 3, "categoryIcon-WashingMachines.svg", "categoryImg-WashingMachines.svg", "Пральні машини", 4 },
                    { 404, 4, "categoryIcon-Dishwashers.svg", "categoryImg-Dishwashers.svg", "Посудомийні машини", 4 },
                    { 405, 5, "categoryIcon-KitchenAppliances.svg", "categoryImg-KitchenAppliances.svg", "Кухонна техніка", 4 },
                    { 406, 6, "categoryIcon-Microwaves.svg", "categoryImg-Microwaves.svg", "Мікрохвильові печі", 4 },
                    { 407, 7, "categoryIcon-Blenders.svg", "categoryImg-Blenders.svg", "Блендери", 4 },
                    { 408, 8, "categoryIcon-Mixers.svg", "categoryImg-Mixers.svg", "Міксери", 4 },
                    { 409, 9, "categoryIcon-Multicookers.svg", "categoryImg-Multicookers.svg", "Мультиварки", 4 },
                    { 410, 10, "categoryIcon-ClimateControl.svg", "categoryImg-ClimateControl.svg", "Кліматична техніка", 4 },
                    { 411, 11, "categoryIcon-AirConditioners.svg", "categoryImg-AirConditioners.svg", "Кондиціонери", 4 },
                    { 412, 12, "categoryIcon-Heaters.svg", "categoryImg-Heaters.svg", "Обігрівачі", 4 },
                    { 413, 13, "categoryIcon-Fans.svg", "categoryImg-Fans.svg", "Вентилятори", 4 },
                    { 414, 14, "categoryIcon-Cleaning.svg", "categoryImg-Cleaning.svg", "Прибирання", 4 },
                    { 415, 15, "categoryIcon-VacuumCleaners.svg", "categoryImg-VacuumCleaners.svg", "Пилососи", 4 },
                    { 416, 16, "categoryIcon-RobotVacuums.svg", "categoryImg-RobotVacuums.svg", "Роботи-пилососи", 4 },
                    { 501, 1, "categoryIcon-Furniture.svg", "categoryImg-Furniture.svg", "Меблі", 5 },
                    { 502, 2, "categoryIcon-Sofas.svg", "categoryImg-Sofas.svg", "Дивани", 5 },
                    { 503, 3, "categoryIcon-Beds.svg", "categoryImg-Beds.svg", "Ліжка", 5 },
                    { 504, 4, "categoryIcon-Wardrobes.svg", "categoryImg-Wardrobes.svg", "Шафи", 5 },
                    { 505, 5, "categoryIcon-Lighting.svg", "categoryImg-Lighting.svg", "Освітлення", 5 },
                    { 506, 6, "categoryIcon-Lamps.svg", "categoryImg-Lamps.svg", "Лампи", 5 },
                    { 507, 7, "categoryIcon-Chandeliers.svg", "categoryImg-Chandeliers.svg", "Люстри", 5 },
                    { 508, 8, "categoryIcon-LEDLighting.svg", "categoryImg-LEDLighting.svg", "LED освітлення", 5 },
                    { 509, 9, "categoryIcon-Decor.svg", "categoryImg-Decor.svg", "Декор", 5 },
                    { 510, 10, "categoryIcon-Paintings.svg", "categoryImg-Paintings.svg", "Картини", 5 },
                    { 511, 11, "categoryIcon-Mirrors.svg", "categoryImg-Mirrors.svg", "Дзеркала", 5 },
                    { 512, 12, "categoryIcon-Clocks.svg", "categoryImg-Clocks.svg", "Годинники", 5 },
                    { 601, 1, "categoryIcon-PowerTools.svg", "categoryImg-PowerTools.svg", "Електроінструменти", 6 },
                    { 602, 2, "categoryIcon-Drills.svg", "categoryImg-Drills.svg", "Дрилі", 6 },
                    { 603, 3, "categoryIcon-Screwdrivers.svg", "categoryImg-Screwdrivers.svg", "Шуруповерти", 6 },
                    { 604, 4, "categoryIcon-AngleGrinders.svg", "categoryImg-AngleGrinders.svg", "Болгарки", 6 },
                    { 605, 5, "categoryIcon-CarElectronics.svg", "categoryImg-CarElectronics.svg", "Автоелектроніка", 6 },
                    { 606, 6, "categoryIcon-DashCams.svg", "categoryImg-DashCams.svg", "Відеореєстратори", 6 },
                    { 607, 7, "categoryIcon-GPS.svg", "categoryImg-GPS.svg", "GPS навігатори", 6 },
                    { 608, 8, "categoryIcon-CarAccessories.svg", "categoryImg-CarAccessories.svg", "Автоаксесуари", 6 },
                    { 609, 9, "categoryIcon-PhoneHolders.svg", "categoryImg-PhoneHolders.svg", "Тримачі телефону", 6 },
                    { 610, 10, "categoryIcon-CarChargers.svg", "categoryImg-CarChargers.svg", "Зарядні пристрої", 6 },
                    { 701, 1, "categoryIcon-Bathroom.svg", "categoryImg-Bathroom.svg", "Ванна кімната", 7 },
                    { 702, 2, "categoryIcon-ShowerCubicles.svg", "categoryImg-ShowerCubicles.svg", "Душові кабіни", 7 },
                    { 703, 3, "categoryIcon-Toilets.svg", "categoryImg-Toilets.svg", "Унітази", 7 },
                    { 704, 4, "categoryIcon-Sinks.svg", "categoryImg-Sinks.svg", "Раковини", 7 },
                    { 705, 5, "categoryIcon-Tools.svg", "categoryImg-Tools.svg", "Інструменти", 7 },
                    { 706, 6, "categoryIcon-HandTools.svg", "categoryImg-HandTools.svg", "Ручний інструмент", 7 },
                    { 707, 7, "categoryIcon-MeasuringTools.svg", "categoryImg-MeasuringTools.svg", "Вимірювальні прилади", 7 },
                    { 708, 8, "categoryIcon-Materials.svg", "categoryImg-Materials.svg", "Матеріали", 7 },
                    { 709, 9, "categoryIcon-Paint.svg", "categoryImg-Paint.svg", "Фарба", 7 },
                    { 710, 10, "categoryIcon-Tiles.svg", "categoryImg-Tiles.svg", "Плитка", 7 },
                    { 711, 11, "categoryIcon-Laminate.svg", "categoryImg-Laminate.svg", "Ламінат", 7 },
                    { 801, 1, "categoryIcon-GardenTools.svg", "categoryImg-GardenTools.svg", "Садова техніка", 8 },
                    { 802, 2, "categoryIcon-LawnMowers.svg", "categoryImg-LawnMowers.svg", "Газонокосарки", 8 },
                    { 803, 3, "categoryIcon-Trimmers.svg", "categoryImg-Trimmers.svg", "Тримери", 8 },
                    { 804, 4, "categoryIcon-GardenImplements.svg", "categoryImg-GardenImplements.svg", "Садові інструменти", 8 },
                    { 805, 5, "categoryIcon-Shovels.svg", "categoryImg-Shovels.svg", "Лопати", 8 },
                    { 806, 6, "categoryIcon-Pruners.svg", "categoryImg-Pruners.svg", "Секатори", 8 },
                    { 807, 7, "categoryIcon-GardenFurniture.svg", "categoryImg-GardenFurniture.svg", "Меблі для саду", 8 },
                    { 808, 8, "categoryIcon-GardenTables.svg", "categoryImg-GardenTables.svg", "Садові столи", 8 },
                    { 809, 9, "categoryIcon-GardenChairs.svg", "categoryImg-GardenChairs.svg", "Крісла", 8 },
                    { 901, 1, "categoryIcon-Fitness.svg", "categoryImg-Fitness.svg", "Фітнес", 9 },
                    { 902, 2, "categoryIcon-Dumbbells.svg", "categoryImg-Dumbbells.svg", "Гантелі", 9 },
                    { 903, 3, "categoryIcon-Treadmills.svg", "categoryImg-Treadmills.svg", "Бігові доріжки", 9 },
                    { 904, 4, "categoryIcon-Cycling.svg", "categoryImg-Cycling.svg", "Велоспорт", 9 },
                    { 905, 5, "categoryIcon-Bicycles.svg", "categoryImg-Bicycles.svg", "Велосипеди", 9 },
                    { 906, 6, "categoryIcon-CyclingAccessories.svg", "categoryImg-CyclingAccessories.svg", "Аксесуари", 9 },
                    { 907, 7, "categoryIcon-OutdoorRecreation.svg", "categoryImg-OutdoorRecreation.svg", "Активний відпочинок", 9 },
                    { 908, 8, "categoryIcon-KickScooters.svg", "categoryImg-KickScooters.svg", "Самокати", 9 },
                    { 909, 9, "categoryIcon-ElectricScooters.svg", "categoryImg-ElectricScooters.svg", "Електросамокати", 9 },
                    { 1001, 1, "categoryIcon-MensClothing.svg", "categoryImg-MensClothing.svg", "Чоловічий одяг", 10 },
                    { 1002, 2, "categoryIcon-TShirts.svg", "categoryImg-TShirts.svg", "Футболки", 10 },
                    { 1003, 3, "categoryIcon-Jeans.svg", "categoryImg-Jeans.svg", "Джинси", 10 },
                    { 1004, 4, "categoryIcon-Jackets.svg", "categoryImg-Jackets.svg", "Куртки", 10 },
                    { 1005, 5, "categoryIcon-WomensClothing.svg", "categoryImg-WomensClothing.svg", "Жіночий одяг", 10 },
                    { 1006, 6, "categoryIcon-Dresses.svg", "categoryImg-Dresses.svg", "Сукні", 10 },
                    { 1007, 7, "categoryIcon-Skirts.svg", "categoryImg-Skirts.svg", "Спідниці", 10 },
                    { 1008, 8, "categoryIcon-Footwear.svg", "categoryImg-Footwear.svg", "Взуття", 10 },
                    { 1009, 9, "categoryIcon-Sneakers.svg", "categoryImg-Sneakers.svg", "Кросівки", 10 },
                    { 1010, 10, "categoryIcon-Boots.svg", "categoryImg-Boots.svg", "Черевики", 10 },
                    { 1011, 11, "categoryIcon-Accessories.svg", "categoryImg-Accessories.svg", "Аксесуари", 10 },
                    { 1012, 12, "categoryIcon-Bags.svg", "categoryImg-Bags.svg", "Сумки", 10 },
                    { 1013, 13, "categoryIcon-Belts.svg", "categoryImg-Belts.svg", "Ремені", 10 },
                    { 1101, 1, "categoryIcon-FaceCare.svg", "categoryImg-FaceCare.svg", "Догляд за обличчям", 11 },
                    { 1102, 2, "categoryIcon-Creams.svg", "categoryImg-Creams.svg", "Креми", 11 },
                    { 1103, 3, "categoryIcon-Serums.svg", "categoryImg-Serums.svg", "Сироватки", 11 },
                    { 1104, 4, "categoryIcon-HairCare.svg", "categoryImg-HairCare.svg", "Догляд за волоссям", 11 },
                    { 1105, 5, "categoryIcon-Shampoos.svg", "categoryImg-Shampoos.svg", "Шампуні", 11 },
                    { 1106, 6, "categoryIcon-HairMasks.svg", "categoryImg-HairMasks.svg", "Маски", 11 },
                    { 1107, 7, "categoryIcon-BeautyTech.svg", "categoryImg-BeautyTech.svg", "Техніка", 11 },
                    { 1108, 8, "categoryIcon-HairDryers.svg", "categoryImg-HairDryers.svg", "Фени", 11 },
                    { 1109, 9, "categoryIcon-Razors.svg", "categoryImg-Razors.svg", "Бритви", 11 },
                    { 1201, 1, "categoryIcon-Toys.svg", "categoryImg-Toys.svg", "Іграшки", 12 },
                    { 1202, 2, "categoryIcon-ConstructionToys.svg", "categoryImg-ConstructionToys.svg", "Конструктори", 12 },
                    { 1203, 3, "categoryIcon-Dolls.svg", "categoryImg-Dolls.svg", "Ляльки", 12 },
                    { 1204, 4, "categoryIcon-Cars.svg", "categoryImg-Cars.svg", "Машинки", 12 },
                    { 1205, 5, "categoryIcon-Baby.svg", "categoryImg-Baby.svg", "Для немовлят", 12 },
                    { 1206, 6, "categoryIcon-Diapers.svg", "categoryImg-Diapers.svg", "Підгузки", 12 },
                    { 1207, 7, "categoryIcon-BabyBottles.svg", "categoryImg-BabyBottles.svg", "Пляшечки", 12 },
                    { 1208, 8, "categoryIcon-KidsVehicles.svg", "categoryImg-KidsVehicles.svg", "Дитячий транспорт", 12 },
                    { 1209, 9, "categoryIcon-Strollers.svg", "categoryImg-Strollers.svg", "Коляски", 12 },
                    { 1210, 10, "categoryIcon-KidsScooters.svg", "categoryImg-KidsScooters.svg", "Самокати", 12 },
                    { 1301, 1, "categoryIcon-Dogs.svg", "categoryImg-Dogs.svg", "Для собак", 13 },
                    { 1302, 2, "categoryIcon-DogFood.svg", "categoryImg-DogFood.svg", "Корм", 13 },
                    { 1303, 3, "categoryIcon-DogToys.svg", "categoryImg-DogToys.svg", "Іграшки", 13 },
                    { 1304, 4, "categoryIcon-Cats.svg", "categoryImg-Cats.svg", "Для котів", 13 },
                    { 1305, 5, "categoryIcon-CatFood.svg", "categoryImg-CatFood.svg", "Корм", 13 },
                    { 1306, 6, "categoryIcon-CatLitter.svg", "categoryImg-CatLitter.svg", "Наповнювачі", 13 },
                    { 1307, 7, "categoryIcon-Rodents.svg", "categoryImg-Rodents.svg", "Для гризунів", 13 },
                    { 1308, 8, "categoryIcon-Cages.svg", "categoryImg-Cages.svg", "Клітки", 13 },
                    { 1309, 9, "categoryIcon-RodentFood.svg", "categoryImg-RodentFood.svg", "Корм", 13 },
                    { 1401, 1, "categoryIcon-Stationery.svg", "categoryImg-Stationery.svg", "Канцтовари", 14 },
                    { 1402, 2, "categoryIcon-Pens.svg", "categoryImg-Pens.svg", "Ручки", 14 },
                    { 1403, 3, "categoryIcon-Notebooks.svg", "categoryImg-Notebooks.svg", "Зошити", 14 },
                    { 1404, 4, "categoryIcon-Paper.svg", "categoryImg-Paper.svg", "Папір", 14 },
                    { 1405, 5, "categoryIcon-Books.svg", "categoryImg-Books.svg", "Книги", 14 },
                    { 1406, 6, "categoryIcon-Fiction.svg", "categoryImg-Fiction.svg", "Художні", 14 },
                    { 1407, 7, "categoryIcon-Educational.svg", "categoryImg-Educational.svg", "Навчальні", 14 },
                    { 1501, 1, "categoryIcon-Alcohol.svg", "categoryImg-Alcohol.svg", "Алкоголь", 15 },
                    { 1502, 2, "categoryIcon-Wine.svg", "categoryImg-Wine.svg", "Вино", 15 },
                    { 1503, 3, "categoryIcon-Beer.svg", "categoryImg-Beer.svg", "Пиво", 15 },
                    { 1504, 4, "categoryIcon-Whiskey.svg", "categoryImg-Whiskey.svg", "Віскі", 15 },
                    { 1505, 5, "categoryIcon-Food.svg", "categoryImg-Food.svg", "Продукти", 15 },
                    { 1506, 6, "categoryIcon-Sweets.svg", "categoryImg-Sweets.svg", "Солодощі", 15 },
                    { 1507, 7, "categoryIcon-Snacks.svg", "categoryImg-Snacks.svg", "Снеки", 15 },
                    { 1601, 1, "categoryIcon-Office.svg", "categoryImg-Office.svg", "Офіс", 16 },
                    { 1602, 2, "categoryIcon-OfficeEquipment.svg", "categoryImg-OfficeEquipment.svg", "Офісна техніка", 16 },
                    { 1603, 3, "categoryIcon-OfficeFurniture.svg", "categoryImg-OfficeFurniture.svg", "Меблі", 16 },
                    { 1604, 4, "categoryIcon-BusinessEquipment.svg", "categoryImg-BusinessEquipment.svg", "Бізнес обладнання", 16 },
                    { 1605, 5, "categoryIcon-POS.svg", "categoryImg-POS.svg", "POS системи", 16 },
                    { 1606, 6, "categoryIcon-CashRegisters.svg", "categoryImg-CashRegisters.svg", "Касові апарати", 16 },
                    { 1701, 1, "categoryIcon-TourismGear.svg", "categoryImg-TourismGear.svg", "Туристичне спорядження", 17 },
                    { 1702, 2, "categoryIcon-Tents.svg", "categoryImg-Tents.svg", "Намет", 17 },
                    { 1703, 3, "categoryIcon-SleepingBags.svg", "categoryImg-SleepingBags.svg", "Спальні мішки", 17 },
                    { 1704, 4, "categoryIcon-Travel.svg", "categoryImg-Travel.svg", "Подорожі", 17 },
                    { 1705, 5, "categoryIcon-Suitcases.svg", "categoryImg-Suitcases.svg", "Валізи", 17 },
                    { 1706, 6, "categoryIcon-Backpacks.svg", "categoryImg-Backpacks.svg", "Рюкзаки", 17 },
                    { 1801, 1, "categoryIcon-Discounted.svg", "categoryImg-Discounted.svg", "Товари зі знижками", 18 },
                    { 1802, 2, "categoryIcon-SeasonalSales.svg", "categoryImg-SeasonalSales.svg", "Сезонні розпродажі", 18 },
                    { 1901, 1, "categoryIcon-UpTo50.svg", "categoryImg-UpTo50.svg", "До −50%", 19 },
                    { 1902, 2, "categoryIcon-UpTo70.svg", "categoryImg-UpTo70.svg", "До −70%", 19 },
                    { 1903, 3, "categoryIcon-LastItems.svg", "categoryImg-LastItems.svg", "Останні екземпляри", 19 }
                });

            migrationBuilder.InsertData(
                table: "CategoryBrands",
                columns: new[] { "BrandId", "CategoryId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 12, 1 },
                    { 13, 1 },
                    { 14, 1 },
                    { 15, 1 },
                    { 16, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 2 },
                    { 4, 3 },
                    { 15, 3 },
                    { 2, 4 },
                    { 4, 4 },
                    { 5, 4 },
                    { 6, 4 },
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
                table: "CategoryFilters",
                columns: new[] { "Id", "CategoryId", "DisplayName", "ItemId", "Key", "ValueType" },
                values: new object[,]
                {
                    { -2045, 19, "Категорія товару", null, "category", 0 },
                    { -2044, 19, "Розмір знижки", null, "discount_size", 1 },
                    { -2043, 18, "Категорія товару", null, "category", 0 },
                    { -2042, 18, "Розмір знижки", null, "discount_size", 1 },
                    { -2041, 17, "Місткість", null, "capacity", 1 },
                    { -2040, 17, "Тип", null, "type", 0 },
                    { -2039, 16, "Тип обладнання", null, "equipment_type", 0 },
                    { -2038, 15, "Міцність", null, "strength", 1 },
                    { -2037, 15, "Об'єм", null, "volume", 1 },
                    { -2036, 15, "Тип", null, "type", 0 },
                    { -2035, 14, "Формат", null, "format", 0 },
                    { -2034, 14, "Тип", null, "type", 0 },
                    { -2033, 13, "Вік тварини", null, "animal_age", 0 },
                    { -2032, 13, "Вид товару", null, "product_type", 0 },
                    { -2031, 13, "Тип тварини", null, "animal_type", 0 },
                    { -2030, 12, "Тип", null, "type", 0 },
                    { -2029, 12, "Вік", null, "age", 1 },
                    { -2028, 11, "Для кого", null, "for_whom", 0 },
                    { -2027, 11, "Тип", null, "type", 0 },
                    { -2026, 10, "Стать", null, "gender", 0 },
                    { -2025, 10, "Матеріал", null, "material", 0 },
                    { -2024, 10, "Колір", null, "color", 0 },
                    { -2023, 10, "Розмір", null, "size", 0 },
                    { -2022, 9, "Розмір", null, "size", 0 },
                    { -2021, 9, "Вид спорту", null, "sport_type", 0 },
                    { -2020, 8, "Матеріал", null, "material", 0 },
                    { -2019, 8, "Тип", null, "type", 0 },
                    { -2018, 7, "Розмір", null, "size", 0 },
                    { -2017, 7, "Матеріал", null, "material", 0 },
                    { -2016, 6, "Тип інструменту", null, "type", 0 },
                    { -2015, 6, "Потужність", null, "power", 1 },
                    { -2014, 5, "Розмір", null, "size", 0 },
                    { -2013, 5, "Колір", null, "color", 0 },
                    { -2012, 5, "Матеріал", null, "material", 0 },
                    { -2011, 4, "Тип", null, "type", 0 },
                    { -2010, 4, "Колір", null, "color", 0 },
                    { -2009, 4, "Клас енергоспоживання", null, "energy_class", 0 },
                    { -2008, 3, "Жанр", null, "genre", 0 },
                    { -2007, 3, "Платформа", null, "platform", 0 },
                    { -2006, 2, "Вбудована пам'ять", null, "memory", 1 },
                    { -2005, 2, "Колір", null, "color", 0 },
                    { -2004, 2, "Діагональ екрану", null, "screen_size", 1 },
                    { -2003, 1, "Тип відеокарти", null, "graphics", 0 },
                    { -2002, 1, "Об'єм накопичувача", null, "storage", 1 },
                    { -2001, 1, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2000, 1, "Тип процесора", null, "processor", 0 },
                    { -2596, 1903, "Категорія товару", null, "category", 0 },
                    { -2595, 1903, "Розмір знижки", null, "discount_size", 1 },
                    { -2594, 1902, "Категорія товару", null, "category", 0 },
                    { -2593, 1902, "Розмір знижки", null, "discount_size", 1 },
                    { -2592, 1901, "Категорія товару", null, "category", 0 },
                    { -2591, 1901, "Розмір знижки", null, "discount_size", 1 },
                    { -2590, 1802, "Категорія товару", null, "category", 0 },
                    { -2589, 1802, "Розмір знижки", null, "discount_size", 1 },
                    { -2588, 1801, "Категорія товару", null, "category", 0 },
                    { -2587, 1801, "Розмір знижки", null, "discount_size", 1 },
                    { -2586, 1706, "Місткість", null, "capacity", 1 },
                    { -2585, 1706, "Тип", null, "type", 0 },
                    { -2584, 1705, "Місткість", null, "capacity", 1 },
                    { -2583, 1705, "Тип", null, "type", 0 },
                    { -2582, 1704, "Місткість", null, "capacity", 1 },
                    { -2581, 1704, "Тип", null, "type", 0 },
                    { -2580, 1703, "Місткість", null, "capacity", 1 },
                    { -2579, 1703, "Тип", null, "type", 0 },
                    { -2578, 1702, "Місткість", null, "capacity", 1 },
                    { -2577, 1702, "Тип", null, "type", 0 },
                    { -2576, 1701, "Місткість", null, "capacity", 1 },
                    { -2575, 1701, "Тип", null, "type", 0 },
                    { -2574, 1606, "Тип обладнання", null, "equipment_type", 0 },
                    { -2573, 1605, "Тип обладнання", null, "equipment_type", 0 },
                    { -2572, 1604, "Тип обладнання", null, "equipment_type", 0 },
                    { -2571, 1603, "Тип обладнання", null, "equipment_type", 0 },
                    { -2570, 1602, "Тип обладнання", null, "equipment_type", 0 },
                    { -2569, 1601, "Тип обладнання", null, "equipment_type", 0 },
                    { -2568, 1507, "Міцність", null, "strength", 1 },
                    { -2567, 1507, "Об'єм", null, "volume", 1 },
                    { -2566, 1507, "Тип", null, "type", 0 },
                    { -2565, 1506, "Міцність", null, "strength", 1 },
                    { -2564, 1506, "Об'єм", null, "volume", 1 },
                    { -2563, 1506, "Тип", null, "type", 0 },
                    { -2562, 1505, "Міцність", null, "strength", 1 },
                    { -2561, 1505, "Об'єм", null, "volume", 1 },
                    { -2560, 1505, "Тип", null, "type", 0 },
                    { -2559, 1504, "Міцність", null, "strength", 1 },
                    { -2558, 1504, "Об'єм", null, "volume", 1 },
                    { -2557, 1504, "Тип", null, "type", 0 },
                    { -2556, 1503, "Міцність", null, "strength", 1 },
                    { -2555, 1503, "Об'єм", null, "volume", 1 },
                    { -2554, 1503, "Тип", null, "type", 0 },
                    { -2553, 1502, "Міцність", null, "strength", 1 },
                    { -2552, 1502, "Об'єм", null, "volume", 1 },
                    { -2551, 1502, "Тип", null, "type", 0 },
                    { -2550, 1501, "Міцність", null, "strength", 1 },
                    { -2549, 1501, "Об'єм", null, "volume", 1 },
                    { -2548, 1501, "Тип", null, "type", 0 },
                    { -2547, 1407, "Формат", null, "format", 0 },
                    { -2546, 1407, "Тип", null, "type", 0 },
                    { -2545, 1406, "Формат", null, "format", 0 },
                    { -2544, 1406, "Тип", null, "type", 0 },
                    { -2543, 1405, "Формат", null, "format", 0 },
                    { -2542, 1405, "Тип", null, "type", 0 },
                    { -2541, 1404, "Формат", null, "format", 0 },
                    { -2540, 1404, "Тип", null, "type", 0 },
                    { -2539, 1403, "Формат", null, "format", 0 },
                    { -2538, 1403, "Тип", null, "type", 0 },
                    { -2537, 1402, "Формат", null, "format", 0 },
                    { -2536, 1402, "Тип", null, "type", 0 },
                    { -2535, 1401, "Формат", null, "format", 0 },
                    { -2534, 1401, "Тип", null, "type", 0 },
                    { -2533, 1309, "Вік тварини", null, "animal_age", 0 },
                    { -2532, 1309, "Вид товару", null, "product_type", 0 },
                    { -2531, 1309, "Тип тварини", null, "animal_type", 0 },
                    { -2530, 1308, "Вік тварини", null, "animal_age", 0 },
                    { -2529, 1308, "Вид товару", null, "product_type", 0 },
                    { -2528, 1308, "Тип тварини", null, "animal_type", 0 },
                    { -2527, 1307, "Вік тварини", null, "animal_age", 0 },
                    { -2526, 1307, "Вид товару", null, "product_type", 0 },
                    { -2525, 1307, "Тип тварини", null, "animal_type", 0 },
                    { -2524, 1306, "Вік тварини", null, "animal_age", 0 },
                    { -2523, 1306, "Вид товару", null, "product_type", 0 },
                    { -2522, 1306, "Тип тварини", null, "animal_type", 0 },
                    { -2521, 1305, "Вік тварини", null, "animal_age", 0 },
                    { -2520, 1305, "Вид товару", null, "product_type", 0 },
                    { -2519, 1305, "Тип тварини", null, "animal_type", 0 },
                    { -2518, 1304, "Вік тварини", null, "animal_age", 0 },
                    { -2517, 1304, "Вид товару", null, "product_type", 0 },
                    { -2516, 1304, "Тип тварини", null, "animal_type", 0 },
                    { -2515, 1303, "Вік тварини", null, "animal_age", 0 },
                    { -2514, 1303, "Вид товару", null, "product_type", 0 },
                    { -2513, 1303, "Тип тварини", null, "animal_type", 0 },
                    { -2512, 1302, "Вік тварини", null, "animal_age", 0 },
                    { -2511, 1302, "Вид товару", null, "product_type", 0 },
                    { -2510, 1302, "Тип тварини", null, "animal_type", 0 },
                    { -2509, 1301, "Вік тварини", null, "animal_age", 0 },
                    { -2508, 1301, "Вид товару", null, "product_type", 0 },
                    { -2507, 1301, "Тип тварини", null, "animal_type", 0 },
                    { -2506, 1210, "Тип", null, "type", 0 },
                    { -2505, 1210, "Вік", null, "age", 1 },
                    { -2504, 1209, "Тип", null, "type", 0 },
                    { -2503, 1209, "Вік", null, "age", 1 },
                    { -2502, 1208, "Тип", null, "type", 0 },
                    { -2501, 1208, "Вік", null, "age", 1 },
                    { -2500, 1207, "Тип", null, "type", 0 },
                    { -2499, 1207, "Вік", null, "age", 1 },
                    { -2498, 1206, "Тип", null, "type", 0 },
                    { -2497, 1206, "Вік", null, "age", 1 },
                    { -2496, 1205, "Тип", null, "type", 0 },
                    { -2495, 1205, "Вік", null, "age", 1 },
                    { -2494, 1204, "Тип", null, "type", 0 },
                    { -2493, 1204, "Вік", null, "age", 1 },
                    { -2492, 1203, "Тип", null, "type", 0 },
                    { -2491, 1203, "Вік", null, "age", 1 },
                    { -2490, 1202, "Тип", null, "type", 0 },
                    { -2489, 1202, "Вік", null, "age", 1 },
                    { -2488, 1201, "Тип", null, "type", 0 },
                    { -2487, 1201, "Вік", null, "age", 1 },
                    { -2486, 1109, "Для кого", null, "for_whom", 0 },
                    { -2485, 1109, "Тип", null, "type", 0 },
                    { -2484, 1108, "Для кого", null, "for_whom", 0 },
                    { -2483, 1108, "Тип", null, "type", 0 },
                    { -2482, 1107, "Для кого", null, "for_whom", 0 },
                    { -2481, 1107, "Тип", null, "type", 0 },
                    { -2480, 1106, "Для кого", null, "for_whom", 0 },
                    { -2479, 1106, "Тип", null, "type", 0 },
                    { -2478, 1105, "Для кого", null, "for_whom", 0 },
                    { -2477, 1105, "Тип", null, "type", 0 },
                    { -2476, 1104, "Для кого", null, "for_whom", 0 },
                    { -2475, 1104, "Тип", null, "type", 0 },
                    { -2474, 1103, "Для кого", null, "for_whom", 0 },
                    { -2473, 1103, "Тип", null, "type", 0 },
                    { -2472, 1102, "Для кого", null, "for_whom", 0 },
                    { -2471, 1102, "Тип", null, "type", 0 },
                    { -2470, 1101, "Для кого", null, "for_whom", 0 },
                    { -2469, 1101, "Тип", null, "type", 0 },
                    { -2468, 1013, "Стать", null, "gender", 0 },
                    { -2467, 1013, "Матеріал", null, "material", 0 },
                    { -2466, 1013, "Колір", null, "color", 0 },
                    { -2465, 1013, "Розмір", null, "size", 0 },
                    { -2464, 1012, "Стать", null, "gender", 0 },
                    { -2463, 1012, "Матеріал", null, "material", 0 },
                    { -2462, 1012, "Колір", null, "color", 0 },
                    { -2461, 1012, "Розмір", null, "size", 0 },
                    { -2460, 1011, "Стать", null, "gender", 0 },
                    { -2459, 1011, "Матеріал", null, "material", 0 },
                    { -2458, 1011, "Колір", null, "color", 0 },
                    { -2457, 1011, "Розмір", null, "size", 0 },
                    { -2456, 1010, "Стать", null, "gender", 0 },
                    { -2455, 1010, "Матеріал", null, "material", 0 },
                    { -2454, 1010, "Колір", null, "color", 0 },
                    { -2453, 1010, "Розмір", null, "size", 0 },
                    { -2452, 1009, "Стать", null, "gender", 0 },
                    { -2451, 1009, "Матеріал", null, "material", 0 },
                    { -2450, 1009, "Колір", null, "color", 0 },
                    { -2449, 1009, "Розмір", null, "size", 0 },
                    { -2448, 1008, "Стать", null, "gender", 0 },
                    { -2447, 1008, "Матеріал", null, "material", 0 },
                    { -2446, 1008, "Колір", null, "color", 0 },
                    { -2445, 1008, "Розмір", null, "size", 0 },
                    { -2444, 1007, "Стать", null, "gender", 0 },
                    { -2443, 1007, "Матеріал", null, "material", 0 },
                    { -2442, 1007, "Колір", null, "color", 0 },
                    { -2441, 1007, "Розмір", null, "size", 0 },
                    { -2440, 1006, "Стать", null, "gender", 0 },
                    { -2439, 1006, "Матеріал", null, "material", 0 },
                    { -2438, 1006, "Колір", null, "color", 0 },
                    { -2437, 1006, "Розмір", null, "size", 0 },
                    { -2436, 1005, "Стать", null, "gender", 0 },
                    { -2435, 1005, "Матеріал", null, "material", 0 },
                    { -2434, 1005, "Колір", null, "color", 0 },
                    { -2433, 1005, "Розмір", null, "size", 0 },
                    { -2432, 1004, "Стать", null, "gender", 0 },
                    { -2431, 1004, "Матеріал", null, "material", 0 },
                    { -2430, 1004, "Колір", null, "color", 0 },
                    { -2429, 1004, "Розмір", null, "size", 0 },
                    { -2428, 1003, "Стать", null, "gender", 0 },
                    { -2427, 1003, "Матеріал", null, "material", 0 },
                    { -2426, 1003, "Колір", null, "color", 0 },
                    { -2425, 1003, "Розмір", null, "size", 0 },
                    { -2424, 1002, "Стать", null, "gender", 0 },
                    { -2423, 1002, "Матеріал", null, "material", 0 },
                    { -2422, 1002, "Колір", null, "color", 0 },
                    { -2421, 1002, "Розмір", null, "size", 0 },
                    { -2420, 1001, "Стать", null, "gender", 0 },
                    { -2419, 1001, "Матеріал", null, "material", 0 },
                    { -2418, 1001, "Колір", null, "color", 0 },
                    { -2417, 1001, "Розмір", null, "size", 0 },
                    { -2416, 909, "Розмір", null, "size", 0 },
                    { -2415, 909, "Вид спорту", null, "sport_type", 0 },
                    { -2414, 908, "Розмір", null, "size", 0 },
                    { -2413, 908, "Вид спорту", null, "sport_type", 0 },
                    { -2412, 907, "Розмір", null, "size", 0 },
                    { -2411, 907, "Вид спорту", null, "sport_type", 0 },
                    { -2410, 906, "Розмір", null, "size", 0 },
                    { -2409, 906, "Вид спорту", null, "sport_type", 0 },
                    { -2408, 905, "Розмір", null, "size", 0 },
                    { -2407, 905, "Вид спорту", null, "sport_type", 0 },
                    { -2406, 904, "Розмір", null, "size", 0 },
                    { -2405, 904, "Вид спорту", null, "sport_type", 0 },
                    { -2404, 903, "Розмір", null, "size", 0 },
                    { -2403, 903, "Вид спорту", null, "sport_type", 0 },
                    { -2402, 902, "Розмір", null, "size", 0 },
                    { -2401, 902, "Вид спорту", null, "sport_type", 0 },
                    { -2400, 901, "Розмір", null, "size", 0 },
                    { -2399, 901, "Вид спорту", null, "sport_type", 0 },
                    { -2398, 809, "Матеріал", null, "material", 0 },
                    { -2397, 809, "Тип", null, "type", 0 },
                    { -2396, 808, "Матеріал", null, "material", 0 },
                    { -2395, 808, "Тип", null, "type", 0 },
                    { -2394, 807, "Матеріал", null, "material", 0 },
                    { -2393, 807, "Тип", null, "type", 0 },
                    { -2392, 806, "Матеріал", null, "material", 0 },
                    { -2391, 806, "Тип", null, "type", 0 },
                    { -2390, 805, "Матеріал", null, "material", 0 },
                    { -2389, 805, "Тип", null, "type", 0 },
                    { -2388, 804, "Матеріал", null, "material", 0 },
                    { -2387, 804, "Тип", null, "type", 0 },
                    { -2386, 803, "Матеріал", null, "material", 0 },
                    { -2385, 803, "Тип", null, "type", 0 },
                    { -2384, 802, "Матеріал", null, "material", 0 },
                    { -2383, 802, "Тип", null, "type", 0 },
                    { -2382, 801, "Матеріал", null, "material", 0 },
                    { -2381, 801, "Тип", null, "type", 0 },
                    { -2380, 711, "Розмір", null, "size", 0 },
                    { -2379, 711, "Матеріал", null, "material", 0 },
                    { -2378, 710, "Розмір", null, "size", 0 },
                    { -2377, 710, "Матеріал", null, "material", 0 },
                    { -2376, 709, "Розмір", null, "size", 0 },
                    { -2375, 709, "Матеріал", null, "material", 0 },
                    { -2374, 708, "Розмір", null, "size", 0 },
                    { -2373, 708, "Матеріал", null, "material", 0 },
                    { -2372, 707, "Розмір", null, "size", 0 },
                    { -2371, 707, "Матеріал", null, "material", 0 },
                    { -2370, 706, "Розмір", null, "size", 0 },
                    { -2369, 706, "Матеріал", null, "material", 0 },
                    { -2368, 705, "Розмір", null, "size", 0 },
                    { -2367, 705, "Матеріал", null, "material", 0 },
                    { -2366, 704, "Розмір", null, "size", 0 },
                    { -2365, 704, "Матеріал", null, "material", 0 },
                    { -2364, 703, "Розмір", null, "size", 0 },
                    { -2363, 703, "Матеріал", null, "material", 0 },
                    { -2362, 702, "Розмір", null, "size", 0 },
                    { -2361, 702, "Матеріал", null, "material", 0 },
                    { -2360, 701, "Розмір", null, "size", 0 },
                    { -2359, 701, "Матеріал", null, "material", 0 },
                    { -2358, 610, "Тип інструменту", null, "type", 0 },
                    { -2357, 610, "Потужність", null, "power", 1 },
                    { -2356, 609, "Тип інструменту", null, "type", 0 },
                    { -2355, 609, "Потужність", null, "power", 1 },
                    { -2354, 608, "Тип інструменту", null, "type", 0 },
                    { -2353, 608, "Потужність", null, "power", 1 },
                    { -2352, 607, "Тип інструменту", null, "type", 0 },
                    { -2351, 607, "Потужність", null, "power", 1 },
                    { -2350, 606, "Тип інструменту", null, "type", 0 },
                    { -2349, 606, "Потужність", null, "power", 1 },
                    { -2348, 605, "Тип інструменту", null, "type", 0 },
                    { -2347, 605, "Потужність", null, "power", 1 },
                    { -2346, 604, "Тип інструменту", null, "type", 0 },
                    { -2345, 604, "Потужність", null, "power", 1 },
                    { -2344, 603, "Тип інструменту", null, "type", 0 },
                    { -2343, 603, "Потужність", null, "power", 1 },
                    { -2342, 602, "Тип інструменту", null, "type", 0 },
                    { -2341, 602, "Потужність", null, "power", 1 },
                    { -2340, 601, "Тип інструменту", null, "type", 0 },
                    { -2339, 601, "Потужність", null, "power", 1 },
                    { -2338, 512, "Розмір", null, "size", 0 },
                    { -2337, 512, "Колір", null, "color", 0 },
                    { -2336, 512, "Матеріал", null, "material", 0 },
                    { -2335, 511, "Розмір", null, "size", 0 },
                    { -2334, 511, "Колір", null, "color", 0 },
                    { -2333, 511, "Матеріал", null, "material", 0 },
                    { -2332, 510, "Розмір", null, "size", 0 },
                    { -2331, 510, "Колір", null, "color", 0 },
                    { -2330, 510, "Матеріал", null, "material", 0 },
                    { -2329, 509, "Розмір", null, "size", 0 },
                    { -2328, 509, "Колір", null, "color", 0 },
                    { -2327, 509, "Матеріал", null, "material", 0 },
                    { -2326, 508, "Розмір", null, "size", 0 },
                    { -2325, 508, "Колір", null, "color", 0 },
                    { -2324, 508, "Матеріал", null, "material", 0 },
                    { -2323, 507, "Розмір", null, "size", 0 },
                    { -2322, 507, "Колір", null, "color", 0 },
                    { -2321, 507, "Матеріал", null, "material", 0 },
                    { -2320, 506, "Розмір", null, "size", 0 },
                    { -2319, 506, "Колір", null, "color", 0 },
                    { -2318, 506, "Матеріал", null, "material", 0 },
                    { -2317, 505, "Розмір", null, "size", 0 },
                    { -2316, 505, "Колір", null, "color", 0 },
                    { -2315, 505, "Матеріал", null, "material", 0 },
                    { -2314, 504, "Розмір", null, "size", 0 },
                    { -2313, 504, "Колір", null, "color", 0 },
                    { -2312, 504, "Матеріал", null, "material", 0 },
                    { -2311, 503, "Розмір", null, "size", 0 },
                    { -2310, 503, "Колір", null, "color", 0 },
                    { -2309, 503, "Матеріал", null, "material", 0 },
                    { -2308, 502, "Розмір", null, "size", 0 },
                    { -2307, 502, "Колір", null, "color", 0 },
                    { -2306, 502, "Матеріал", null, "material", 0 },
                    { -2305, 501, "Розмір", null, "size", 0 },
                    { -2304, 501, "Колір", null, "color", 0 },
                    { -2303, 501, "Матеріал", null, "material", 0 },
                    { -2302, 416, "Тип", null, "type", 0 },
                    { -2301, 416, "Колір", null, "color", 0 },
                    { -2300, 416, "Клас енергоспоживання", null, "energy_class", 0 },
                    { -2299, 415, "Тип", null, "type", 0 },
                    { -2298, 415, "Колір", null, "color", 0 },
                    { -2297, 415, "Клас енергоспоживання", null, "energy_class", 0 },
                    { -2296, 414, "Тип", null, "type", 0 },
                    { -2295, 414, "Колір", null, "color", 0 },
                    { -2294, 414, "Клас енергоспоживання", null, "energy_class", 0 },
                    { -2293, 413, "Тип", null, "type", 0 },
                    { -2292, 413, "Колір", null, "color", 0 },
                    { -2291, 413, "Клас енергоспоживання", null, "energy_class", 0 },
                    { -2290, 412, "Тип", null, "type", 0 },
                    { -2289, 412, "Колір", null, "color", 0 },
                    { -2288, 412, "Клас енергоспоживання", null, "energy_class", 0 },
                    { -2287, 411, "Тип", null, "type", 0 },
                    { -2286, 411, "Колір", null, "color", 0 },
                    { -2285, 411, "Клас енергоспоживання", null, "energy_class", 0 },
                    { -2284, 410, "Тип", null, "type", 0 },
                    { -2283, 410, "Колір", null, "color", 0 },
                    { -2282, 410, "Клас енергоспоживання", null, "energy_class", 0 },
                    { -2281, 409, "Тип", null, "type", 0 },
                    { -2280, 409, "Колір", null, "color", 0 },
                    { -2279, 409, "Клас енергоспоживання", null, "energy_class", 0 },
                    { -2278, 408, "Тип", null, "type", 0 },
                    { -2277, 408, "Колір", null, "color", 0 },
                    { -2276, 408, "Клас енергоспоживання", null, "energy_class", 0 },
                    { -2275, 407, "Тип", null, "type", 0 },
                    { -2274, 407, "Колір", null, "color", 0 },
                    { -2273, 407, "Клас енергоспоживання", null, "energy_class", 0 },
                    { -2272, 406, "Тип", null, "type", 0 },
                    { -2271, 406, "Колір", null, "color", 0 },
                    { -2270, 406, "Клас енергоспоживання", null, "energy_class", 0 },
                    { -2269, 405, "Тип", null, "type", 0 },
                    { -2268, 405, "Колір", null, "color", 0 },
                    { -2267, 405, "Клас енергоспоживання", null, "energy_class", 0 },
                    { -2266, 404, "Тип", null, "type", 0 },
                    { -2265, 404, "Колір", null, "color", 0 },
                    { -2264, 404, "Клас енергоспоживання", null, "energy_class", 0 },
                    { -2263, 403, "Тип", null, "type", 0 },
                    { -2262, 403, "Колір", null, "color", 0 },
                    { -2261, 403, "Клас енергоспоживання", null, "energy_class", 0 },
                    { -2260, 402, "Тип", null, "type", 0 },
                    { -2259, 402, "Колір", null, "color", 0 },
                    { -2258, 402, "Клас енергоспоживання", null, "energy_class", 0 },
                    { -2257, 401, "Тип", null, "type", 0 },
                    { -2256, 401, "Колір", null, "color", 0 },
                    { -2255, 401, "Клас енергоспоживання", null, "energy_class", 0 },
                    { -2254, 315, "Жанр", null, "genre", 0 },
                    { -2253, 315, "Платформа", null, "platform", 0 },
                    { -2252, 314, "Жанр", null, "genre", 0 },
                    { -2251, 314, "Платформа", null, "platform", 0 },
                    { -2250, 313, "Жанр", null, "genre", 0 },
                    { -2249, 313, "Платформа", null, "platform", 0 },
                    { -2248, 312, "Жанр", null, "genre", 0 },
                    { -2247, 312, "Платформа", null, "platform", 0 },
                    { -2246, 311, "Жанр", null, "genre", 0 },
                    { -2245, 311, "Платформа", null, "platform", 0 },
                    { -2244, 310, "Жанр", null, "genre", 0 },
                    { -2243, 310, "Платформа", null, "platform", 0 },
                    { -2242, 309, "Жанр", null, "genre", 0 },
                    { -2241, 309, "Платформа", null, "platform", 0 },
                    { -2240, 308, "Жанр", null, "genre", 0 },
                    { -2239, 308, "Платформа", null, "platform", 0 },
                    { -2238, 307, "Жанр", null, "genre", 0 },
                    { -2237, 307, "Платформа", null, "platform", 0 },
                    { -2236, 306, "Жанр", null, "genre", 0 },
                    { -2235, 306, "Платформа", null, "platform", 0 },
                    { -2234, 305, "Жанр", null, "genre", 0 },
                    { -2233, 305, "Платформа", null, "platform", 0 },
                    { -2232, 304, "Жанр", null, "genre", 0 },
                    { -2231, 304, "Платформа", null, "platform", 0 },
                    { -2230, 303, "Жанр", null, "genre", 0 },
                    { -2229, 303, "Платформа", null, "platform", 0 },
                    { -2228, 302, "Жанр", null, "genre", 0 },
                    { -2227, 302, "Платформа", null, "platform", 0 },
                    { -2226, 301, "Жанр", null, "genre", 0 },
                    { -2225, 301, "Платформа", null, "platform", 0 },
                    { -2224, 221, "Вбудована пам'ять", null, "memory", 1 },
                    { -2223, 221, "Колір", null, "color", 0 },
                    { -2222, 221, "Діагональ екрану", null, "screen_size", 1 },
                    { -2221, 220, "Вбудована пам'ять", null, "memory", 1 },
                    { -2220, 220, "Колір", null, "color", 0 },
                    { -2219, 220, "Діагональ екрану", null, "screen_size", 1 },
                    { -2218, 219, "Вбудована пам'ять", null, "memory", 1 },
                    { -2217, 219, "Колір", null, "color", 0 },
                    { -2216, 219, "Діагональ екрану", null, "screen_size", 1 },
                    { -2215, 218, "Вбудована пам'ять", null, "memory", 1 },
                    { -2214, 218, "Колір", null, "color", 0 },
                    { -2213, 218, "Діагональ екрану", null, "screen_size", 1 },
                    { -2212, 217, "Вбудована пам'ять", null, "memory", 1 },
                    { -2211, 217, "Колір", null, "color", 0 },
                    { -2210, 217, "Діагональ екрану", null, "screen_size", 1 },
                    { -2209, 216, "Вбудована пам'ять", null, "memory", 1 },
                    { -2208, 216, "Колір", null, "color", 0 },
                    { -2207, 216, "Діагональ екрану", null, "screen_size", 1 },
                    { -2206, 215, "Вбудована пам'ять", null, "memory", 1 },
                    { -2205, 215, "Колір", null, "color", 0 },
                    { -2204, 215, "Діагональ екрану", null, "screen_size", 1 },
                    { -2203, 214, "Вбудована пам'ять", null, "memory", 1 },
                    { -2202, 214, "Колір", null, "color", 0 },
                    { -2201, 214, "Діагональ екрану", null, "screen_size", 1 },
                    { -2200, 213, "Вбудована пам'ять", null, "memory", 1 },
                    { -2199, 213, "Колір", null, "color", 0 },
                    { -2198, 213, "Діагональ екрану", null, "screen_size", 1 },
                    { -2197, 212, "Вбудована пам'ять", null, "memory", 1 },
                    { -2196, 212, "Колір", null, "color", 0 },
                    { -2195, 212, "Діагональ екрану", null, "screen_size", 1 },
                    { -2194, 211, "Вбудована пам'ять", null, "memory", 1 },
                    { -2193, 211, "Колір", null, "color", 0 },
                    { -2192, 211, "Діагональ екрану", null, "screen_size", 1 },
                    { -2191, 210, "Вбудована пам'ять", null, "memory", 1 },
                    { -2190, 210, "Колір", null, "color", 0 },
                    { -2189, 210, "Діагональ екрану", null, "screen_size", 1 },
                    { -2188, 209, "Вбудована пам'ять", null, "memory", 1 },
                    { -2187, 209, "Колір", null, "color", 0 },
                    { -2186, 209, "Діагональ екрану", null, "screen_size", 1 },
                    { -2185, 208, "Вбудована пам'ять", null, "memory", 1 },
                    { -2184, 208, "Колір", null, "color", 0 },
                    { -2183, 208, "Діагональ екрану", null, "screen_size", 1 },
                    { -2182, 207, "Вбудована пам'ять", null, "memory", 1 },
                    { -2181, 207, "Колір", null, "color", 0 },
                    { -2180, 207, "Діагональ екрану", null, "screen_size", 1 },
                    { -2179, 206, "Вбудована пам'ять", null, "memory", 1 },
                    { -2178, 206, "Колір", null, "color", 0 },
                    { -2177, 206, "Діагональ екрану", null, "screen_size", 1 },
                    { -2176, 205, "Вбудована пам'ять", null, "memory", 1 },
                    { -2175, 205, "Колір", null, "color", 0 },
                    { -2174, 205, "Діагональ екрану", null, "screen_size", 1 },
                    { -2173, 204, "Вбудована пам'ять", null, "memory", 1 },
                    { -2172, 204, "Колір", null, "color", 0 },
                    { -2171, 204, "Діагональ екрану", null, "screen_size", 1 },
                    { -2170, 203, "Вбудована пам'ять", null, "memory", 1 },
                    { -2169, 203, "Колір", null, "color", 0 },
                    { -2168, 203, "Діагональ екрану", null, "screen_size", 1 },
                    { -2167, 202, "Вбудована пам'ять", null, "memory", 1 },
                    { -2166, 202, "Колір", null, "color", 0 },
                    { -2165, 202, "Діагональ екрану", null, "screen_size", 1 },
                    { -2164, 201, "Вбудована пам'ять", null, "memory", 1 },
                    { -2163, 201, "Колір", null, "color", 0 },
                    { -2162, 201, "Діагональ екрану", null, "screen_size", 1 },
                    { -2161, 129, "Тип відеокарти", null, "graphics", 0 },
                    { -2160, 129, "Об'єм накопичувача", null, "storage", 1 },
                    { -2159, 129, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2158, 129, "Тип процесора", null, "processor", 0 },
                    { -2157, 128, "Тип відеокарти", null, "graphics", 0 },
                    { -2156, 128, "Об'єм накопичувача", null, "storage", 1 },
                    { -2155, 128, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2154, 128, "Тип процесора", null, "processor", 0 },
                    { -2153, 127, "Тип відеокарти", null, "graphics", 0 },
                    { -2152, 127, "Об'єм накопичувача", null, "storage", 1 },
                    { -2151, 127, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2150, 127, "Тип процесора", null, "processor", 0 },
                    { -2149, 126, "Тип відеокарти", null, "graphics", 0 },
                    { -2148, 126, "Об'єм накопичувача", null, "storage", 1 },
                    { -2147, 126, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2146, 126, "Тип процесора", null, "processor", 0 },
                    { -2145, 125, "Тип відеокарти", null, "graphics", 0 },
                    { -2144, 125, "Об'єм накопичувача", null, "storage", 1 },
                    { -2143, 125, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2142, 125, "Тип процесора", null, "processor", 0 },
                    { -2141, 124, "Тип відеокарти", null, "graphics", 0 },
                    { -2140, 124, "Об'єм накопичувача", null, "storage", 1 },
                    { -2139, 124, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2138, 124, "Тип процесора", null, "processor", 0 },
                    { -2137, 123, "Тип відеокарти", null, "graphics", 0 },
                    { -2136, 123, "Об'єм накопичувача", null, "storage", 1 },
                    { -2135, 123, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2134, 123, "Тип процесора", null, "processor", 0 },
                    { -2133, 122, "Тип відеокарти", null, "graphics", 0 },
                    { -2132, 122, "Об'єм накопичувача", null, "storage", 1 },
                    { -2131, 122, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2130, 122, "Тип процесора", null, "processor", 0 },
                    { -2129, 121, "Тип відеокарти", null, "graphics", 0 },
                    { -2128, 121, "Об'єм накопичувача", null, "storage", 1 },
                    { -2127, 121, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2126, 121, "Тип процесора", null, "processor", 0 },
                    { -2125, 120, "Тип відеокарти", null, "graphics", 0 },
                    { -2124, 120, "Об'єм накопичувача", null, "storage", 1 },
                    { -2123, 120, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2122, 120, "Тип процесора", null, "processor", 0 },
                    { -2121, 119, "Тип відеокарти", null, "graphics", 0 },
                    { -2120, 119, "Об'єм накопичувача", null, "storage", 1 },
                    { -2119, 119, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2118, 119, "Тип процесора", null, "processor", 0 },
                    { -2117, 118, "Тип відеокарти", null, "graphics", 0 },
                    { -2116, 118, "Об'єм накопичувача", null, "storage", 1 },
                    { -2115, 118, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2114, 118, "Тип процесора", null, "processor", 0 },
                    { -2113, 117, "Тип відеокарти", null, "graphics", 0 },
                    { -2112, 117, "Об'єм накопичувача", null, "storage", 1 },
                    { -2111, 117, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2110, 117, "Тип процесора", null, "processor", 0 },
                    { -2109, 116, "Тип відеокарти", null, "graphics", 0 },
                    { -2108, 116, "Об'єм накопичувача", null, "storage", 1 },
                    { -2107, 116, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2106, 116, "Тип процесора", null, "processor", 0 },
                    { -2105, 115, "Тип відеокарти", null, "graphics", 0 },
                    { -2104, 115, "Об'єм накопичувача", null, "storage", 1 },
                    { -2103, 115, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2102, 115, "Тип процесора", null, "processor", 0 },
                    { -2101, 114, "Тип відеокарти", null, "graphics", 0 },
                    { -2100, 114, "Об'єм накопичувача", null, "storage", 1 },
                    { -2099, 114, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2098, 114, "Тип процесора", null, "processor", 0 },
                    { -2097, 113, "Тип відеокарти", null, "graphics", 0 },
                    { -2096, 113, "Об'єм накопичувача", null, "storage", 1 },
                    { -2095, 113, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2094, 113, "Тип процесора", null, "processor", 0 },
                    { -2093, 112, "Тип відеокарти", null, "graphics", 0 },
                    { -2092, 112, "Об'єм накопичувача", null, "storage", 1 },
                    { -2091, 112, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2090, 112, "Тип процесора", null, "processor", 0 },
                    { -2089, 111, "Тип відеокарти", null, "graphics", 0 },
                    { -2088, 111, "Об'єм накопичувача", null, "storage", 1 },
                    { -2087, 111, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2086, 111, "Тип процесора", null, "processor", 0 },
                    { -2085, 110, "Тип відеокарти", null, "graphics", 0 },
                    { -2084, 110, "Об'єм накопичувача", null, "storage", 1 },
                    { -2083, 110, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2082, 110, "Тип процесора", null, "processor", 0 },
                    { -2081, 109, "Тип відеокарти", null, "graphics", 0 },
                    { -2080, 109, "Об'єм накопичувача", null, "storage", 1 },
                    { -2079, 109, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2078, 109, "Тип процесора", null, "processor", 0 },
                    { -2077, 108, "Тип відеокарти", null, "graphics", 0 },
                    { -2076, 108, "Об'єм накопичувача", null, "storage", 1 },
                    { -2075, 108, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2074, 108, "Тип процесора", null, "processor", 0 },
                    { -2073, 107, "Тип відеокарти", null, "graphics", 0 },
                    { -2072, 107, "Об'єм накопичувача", null, "storage", 1 },
                    { -2071, 107, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2070, 107, "Тип процесора", null, "processor", 0 },
                    { -2069, 106, "Тип відеокарти", null, "graphics", 0 },
                    { -2068, 106, "Об'єм накопичувача", null, "storage", 1 },
                    { -2067, 106, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2066, 106, "Тип процесора", null, "processor", 0 },
                    { -2065, 105, "Тип відеокарти", null, "graphics", 0 },
                    { -2064, 105, "Об'єм накопичувача", null, "storage", 1 },
                    { -2063, 105, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2062, 105, "Тип процесора", null, "processor", 0 },
                    { -2061, 104, "Тип відеокарти", null, "graphics", 0 },
                    { -2060, 104, "Об'єм накопичувача", null, "storage", 1 },
                    { -2059, 104, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2058, 104, "Тип процесора", null, "processor", 0 },
                    { -2057, 103, "Тип відеокарти", null, "graphics", 0 },
                    { -2056, 103, "Об'єм накопичувача", null, "storage", 1 },
                    { -2055, 103, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2054, 103, "Тип процесора", null, "processor", 0 },
                    { -2053, 102, "Тип відеокарти", null, "graphics", 0 },
                    { -2052, 102, "Об'єм накопичувача", null, "storage", 1 },
                    { -2051, 102, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2050, 102, "Тип процесора", null, "processor", 0 },
                    { -2049, 101, "Тип відеокарти", null, "graphics", 0 },
                    { -2048, 101, "Об'єм накопичувача", null, "storage", 1 },
                    { -2047, 101, "Об'єм оперативної пам'яті", null, "ram", 1 },
                    { -2046, 101, "Тип процесора", null, "processor", 0 }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "BrandId", "CategoryId", "Country", "Desc", "Garantia", "ImageUrl", "IsAvailable", "IsNoPercentCredit", "IsReadyToSend", "Name", "Price", "SellerType", "UserId" },
                values: new object[,]
                {
                    { -1106, 1, 205, 0, "Опис товару 3 для категорії 205. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 3 категорії 205", 2500, 0, 1 },
                    { -1105, 1, 205, 0, "Опис товару 2 для категорії 205. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 2 категорії 205", 2000, 0, 1 },
                    { -1104, 1, 205, 0, "Опис товару 1 для категорії 205. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 1 категорії 205", 1500, 0, 1 },
                    { -1103, 1, 204, 0, "Опис товару 3 для категорії 204. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 3 категорії 204", 2500, 0, 1 },
                    { -1102, 1, 204, 0, "Опис товару 2 для категорії 204. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 2 категорії 204", 2000, 0, 1 },
                    { -1101, 1, 204, 0, "Опис товару 1 для категорії 204. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 1 категорії 204", 1500, 0, 1 },
                    { -1100, 1, 203, 0, "Опис товару 3 для категорії 203. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 3 категорії 203", 2500, 0, 1 },
                    { -1099, 1, 203, 0, "Опис товару 2 для категорії 203. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 2 категорії 203", 2000, 0, 1 },
                    { -1098, 1, 203, 0, "Опис товару 1 для категорії 203. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 1 категорії 203", 1500, 0, 1 },
                    { -1097, 1, 202, 0, "Опис товару 3 для категорії 202. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 3 категорії 202", 2500, 0, 1 },
                    { -1096, 1, 202, 0, "Опис товару 2 для категорії 202. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 2 категорії 202", 2000, 0, 1 },
                    { -1095, 1, 202, 0, "Опис товару 1 для категорії 202. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 1 категорії 202", 1500, 0, 1 },
                    { -1094, 1, 201, 0, "Опис товару 3 для категорії 201. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 3 категорії 201", 2500, 0, 1 },
                    { -1093, 1, 201, 0, "Опис товару 2 для категорії 201. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 2 категорії 201", 2000, 0, 1 },
                    { -1092, 1, 201, 0, "Опис товару 1 для категорії 201. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 1 категорії 201", 1500, 0, 1 },
                    { -1091, 1, 123, 0, "Опис товару 3 для категорії 123. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 3 категорії 123", 2500, 0, 1 },
                    { -1090, 1, 123, 0, "Опис товару 2 для категорії 123. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 2 категорії 123", 2000, 0, 1 },
                    { -1089, 1, 123, 0, "Опис товару 1 для категорії 123. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 1 категорії 123", 1500, 0, 1 },
                    { -1088, 1, 122, 0, "Опис товару 3 для категорії 122. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 3 категорії 122", 2500, 0, 1 },
                    { -1087, 1, 122, 0, "Опис товару 2 для категорії 122. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 2 категорії 122", 2000, 0, 1 },
                    { -1086, 1, 122, 0, "Опис товару 1 для категорії 122. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 1 категорії 122", 1500, 0, 1 },
                    { -1085, 1, 121, 0, "Опис товару 3 для категорії 121. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 3 категорії 121", 2500, 0, 1 },
                    { -1084, 1, 121, 0, "Опис товару 2 для категорії 121. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 2 категорії 121", 2000, 0, 1 },
                    { -1083, 1, 121, 0, "Опис товару 1 для категорії 121. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 1 категорії 121", 1500, 0, 1 },
                    { -1082, 1, 110, 0, "Опис товару 3 для категорії 110. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 3 категорії 110", 2500, 0, 1 },
                    { -1081, 1, 110, 0, "Опис товару 2 для категорії 110. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 2 категорії 110", 2000, 0, 1 },
                    { -1080, 1, 110, 0, "Опис товару 1 для категорії 110. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 1 категорії 110", 1500, 0, 1 },
                    { -1079, 1, 109, 0, "Опис товару 3 для категорії 109. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 3 категорії 109", 2500, 0, 1 },
                    { -1078, 1, 109, 0, "Опис товару 2 для категорії 109. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 2 категорії 109", 2000, 0, 1 },
                    { -1077, 1, 109, 0, "Опис товару 1 для категорії 109. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 1 категорії 109", 1500, 0, 1 },
                    { -1076, 1, 108, 0, "Опис товару 3 для категорії 108. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 3 категорії 108", 2500, 0, 1 },
                    { -1075, 1, 108, 0, "Опис товару 2 для категорії 108. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 2 категорії 108", 2000, 0, 1 },
                    { -1074, 1, 108, 0, "Опис товару 1 для категорії 108. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 1 категорії 108", 1500, 0, 1 },
                    { -1073, 1, 107, 0, "Опис товару 3 для категорії 107. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 3 категорії 107", 2500, 0, 1 },
                    { -1072, 1, 107, 0, "Опис товару 2 для категорії 107. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 2 категорії 107", 2000, 0, 1 },
                    { -1071, 1, 107, 0, "Опис товару 1 для категорії 107. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 1 категорії 107", 1500, 0, 1 },
                    { -1070, 1, 106, 0, "Опис товару 3 для категорії 106. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 3 категорії 106", 2500, 0, 1 },
                    { -1069, 1, 106, 0, "Опис товару 2 для категорії 106. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 2 категорії 106", 2000, 0, 1 },
                    { -1068, 1, 106, 0, "Опис товару 1 для категорії 106. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 1 категорії 106", 1500, 0, 1 },
                    { -1067, 1, 105, 0, "Опис товару 3 для категорії 105. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 3 категорії 105", 2500, 0, 1 },
                    { -1066, 1, 105, 0, "Опис товару 2 для категорії 105. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 2 категорії 105", 2000, 0, 1 },
                    { -1065, 1, 105, 0, "Опис товару 1 для категорії 105. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 1 категорії 105", 1500, 0, 1 },
                    { -1064, 1, 104, 0, "Опис товару 3 для категорії 104. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 3 категорії 104", 2500, 0, 1 },
                    { -1063, 1, 104, 0, "Опис товару 2 для категорії 104. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 2 категорії 104", 2000, 0, 1 },
                    { -1062, 1, 104, 0, "Опис товару 1 для категорії 104. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 1 категорії 104", 1500, 0, 1 },
                    { -1061, 1, 103, 0, "Опис товару 3 для категорії 103. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 3 категорії 103", 2500, 0, 1 },
                    { -1060, 1, 103, 0, "Опис товару 2 для категорії 103. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 2 категорії 103", 2000, 0, 1 },
                    { -1059, 1, 103, 0, "Опис товару 1 для категорії 103. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 1 категорії 103", 1500, 0, 1 },
                    { -1058, 1, 102, 0, "Опис товару 3 для категорії 102. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 3 категорії 102", 2500, 0, 1 },
                    { -1057, 1, 102, 0, "Опис товару 2 для категорії 102. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 2 категорії 102", 2000, 0, 1 },
                    { -1056, 1, 102, 0, "Опис товару 1 для категорії 102. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 1 категорії 102", 1500, 0, 1 },
                    { -1055, 1, 101, 0, "Опис товару 3 для категорії 101. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 3 категорії 101", 2500, 0, 1 },
                    { -1054, 1, 101, 0, "Опис товару 2 для категорії 101. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 2 категорії 101", 2000, 0, 1 },
                    { -1053, 1, 101, 0, "Опис товару 1 для категорії 101. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, false, false, "Товар 1 категорії 101", 1500, 0, 1 }
                });

            migrationBuilder.InsertData(
                table: "ItemCharacteristics",
                columns: new[] { "Id", "ItemId", "Key", "Value" },
                values: new object[,]
                {
                    { -3134, -1106, "memory", "256GB" },
                    { -3133, -1106, "color", "Silver" },
                    { -3132, -1106, "screen_size", "6.7\"" },
                    { -3131, -1105, "memory", "256GB" },
                    { -3130, -1105, "color", "Silver" },
                    { -3129, -1105, "screen_size", "6.7\"" },
                    { -3128, -1104, "memory", "256GB" },
                    { -3127, -1104, "color", "Silver" },
                    { -3126, -1104, "screen_size", "6.7\"" },
                    { -3125, -1103, "memory", "128GB" },
                    { -3124, -1103, "color", "Black" },
                    { -3123, -1103, "screen_size", "6.1\"" },
                    { -3122, -1102, "memory", "128GB" },
                    { -3121, -1102, "color", "Black" },
                    { -3120, -1102, "screen_size", "6.1\"" },
                    { -3119, -1101, "memory", "128GB" },
                    { -3118, -1101, "color", "Black" },
                    { -3117, -1101, "screen_size", "6.1\"" },
                    { -3116, -1100, "memory", "256GB" },
                    { -3115, -1100, "color", "Silver" },
                    { -3114, -1100, "screen_size", "6.7\"" },
                    { -3113, -1099, "memory", "256GB" },
                    { -3112, -1099, "color", "Silver" },
                    { -3111, -1099, "screen_size", "6.7\"" },
                    { -3110, -1098, "memory", "256GB" },
                    { -3109, -1098, "color", "Silver" },
                    { -3108, -1098, "screen_size", "6.7\"" },
                    { -3107, -1097, "memory", "128GB" },
                    { -3106, -1097, "color", "Black" },
                    { -3105, -1097, "screen_size", "6.1\"" },
                    { -3104, -1096, "memory", "128GB" },
                    { -3103, -1096, "color", "Black" },
                    { -3102, -1096, "screen_size", "6.1\"" },
                    { -3101, -1095, "memory", "128GB" },
                    { -3100, -1095, "color", "Black" },
                    { -3099, -1095, "screen_size", "6.1\"" },
                    { -3098, -1094, "memory", "256GB" },
                    { -3097, -1094, "color", "Silver" },
                    { -3096, -1094, "screen_size", "6.7\"" },
                    { -3095, -1093, "memory", "256GB" },
                    { -3094, -1093, "color", "Silver" },
                    { -3093, -1093, "screen_size", "6.7\"" },
                    { -3092, -1092, "memory", "256GB" },
                    { -3091, -1092, "color", "Silver" },
                    { -3090, -1092, "screen_size", "6.7\"" },
                    { -3089, -1082, "storage", "256GB SSD" },
                    { -3088, -1082, "ram", "8GB" },
                    { -3087, -1082, "processor", "Intel Core i5" },
                    { -3086, -1081, "storage", "256GB SSD" },
                    { -3085, -1081, "ram", "8GB" },
                    { -3084, -1081, "processor", "Intel Core i5" },
                    { -3083, -1080, "storage", "256GB SSD" },
                    { -3082, -1080, "ram", "8GB" },
                    { -3081, -1080, "processor", "Intel Core i5" },
                    { -3080, -1079, "storage", "512GB SSD" },
                    { -3079, -1079, "ram", "16GB" },
                    { -3078, -1079, "processor", "Intel Core i7" },
                    { -3077, -1078, "storage", "512GB SSD" },
                    { -3076, -1078, "ram", "16GB" },
                    { -3075, -1078, "processor", "Intel Core i7" },
                    { -3074, -1077, "storage", "512GB SSD" },
                    { -3073, -1077, "ram", "16GB" },
                    { -3072, -1077, "processor", "Intel Core i7" },
                    { -3071, -1076, "storage", "256GB SSD" },
                    { -3070, -1076, "ram", "8GB" },
                    { -3069, -1076, "processor", "Intel Core i5" },
                    { -3068, -1075, "storage", "256GB SSD" },
                    { -3067, -1075, "ram", "8GB" },
                    { -3066, -1075, "processor", "Intel Core i5" },
                    { -3065, -1074, "storage", "256GB SSD" },
                    { -3064, -1074, "ram", "8GB" },
                    { -3063, -1074, "processor", "Intel Core i5" },
                    { -3062, -1073, "storage", "512GB SSD" },
                    { -3061, -1073, "ram", "16GB" },
                    { -3060, -1073, "processor", "Intel Core i7" },
                    { -3059, -1072, "storage", "512GB SSD" },
                    { -3058, -1072, "ram", "16GB" },
                    { -3057, -1072, "processor", "Intel Core i7" },
                    { -3056, -1071, "storage", "512GB SSD" },
                    { -3055, -1071, "ram", "16GB" },
                    { -3054, -1071, "processor", "Intel Core i7" },
                    { -3053, -1070, "storage", "256GB SSD" },
                    { -3052, -1070, "ram", "8GB" },
                    { -3051, -1070, "processor", "Intel Core i5" },
                    { -3050, -1069, "storage", "256GB SSD" },
                    { -3049, -1069, "ram", "8GB" },
                    { -3048, -1069, "processor", "Intel Core i5" },
                    { -3047, -1068, "storage", "256GB SSD" },
                    { -3046, -1068, "ram", "8GB" },
                    { -3045, -1068, "processor", "Intel Core i5" },
                    { -3044, -1067, "storage", "512GB SSD" },
                    { -3043, -1067, "ram", "16GB" },
                    { -3042, -1067, "processor", "Intel Core i7" },
                    { -3041, -1066, "storage", "512GB SSD" },
                    { -3040, -1066, "ram", "16GB" },
                    { -3039, -1066, "processor", "Intel Core i7" },
                    { -3038, -1065, "storage", "512GB SSD" },
                    { -3037, -1065, "ram", "16GB" },
                    { -3036, -1065, "processor", "Intel Core i7" },
                    { -3035, -1064, "storage", "256GB SSD" },
                    { -3034, -1064, "ram", "8GB" },
                    { -3033, -1064, "processor", "Intel Core i5" },
                    { -3032, -1063, "storage", "256GB SSD" },
                    { -3031, -1063, "ram", "8GB" },
                    { -3030, -1063, "processor", "Intel Core i5" },
                    { -3029, -1062, "storage", "256GB SSD" },
                    { -3028, -1062, "ram", "8GB" },
                    { -3027, -1062, "processor", "Intel Core i5" },
                    { -3026, -1061, "storage", "512GB SSD" },
                    { -3025, -1061, "ram", "16GB" },
                    { -3024, -1061, "processor", "Intel Core i7" },
                    { -3023, -1060, "storage", "512GB SSD" },
                    { -3022, -1060, "ram", "16GB" },
                    { -3021, -1060, "processor", "Intel Core i7" },
                    { -3020, -1059, "storage", "512GB SSD" },
                    { -3019, -1059, "ram", "16GB" },
                    { -3018, -1059, "processor", "Intel Core i7" },
                    { -3017, -1058, "storage", "256GB SSD" },
                    { -3016, -1058, "ram", "8GB" },
                    { -3015, -1058, "processor", "Intel Core i5" },
                    { -3014, -1057, "storage", "256GB SSD" },
                    { -3013, -1057, "ram", "8GB" },
                    { -3012, -1057, "processor", "Intel Core i5" },
                    { -3011, -1056, "storage", "256GB SSD" },
                    { -3010, -1056, "ram", "8GB" },
                    { -3009, -1056, "processor", "Intel Core i5" },
                    { -3008, -1055, "storage", "512GB SSD" },
                    { -3007, -1055, "ram", "16GB" },
                    { -3006, -1055, "processor", "Intel Core i7" },
                    { -3005, -1054, "storage", "512GB SSD" },
                    { -3004, -1054, "ram", "16GB" },
                    { -3003, -1054, "processor", "Intel Core i7" },
                    { -3002, -1053, "storage", "512GB SSD" },
                    { -3001, -1053, "ram", "16GB" },
                    { -3000, -1053, "processor", "Intel Core i7" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_Name",
                table: "Brands",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ItemId",
                table: "CartItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_UserId_ItemId",
                table: "CartItems",
                columns: new[] { "UserId", "ItemId" },
                unique: true);

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
                name: "IX_CategoryFilters_ItemId",
                table: "CategoryFilters",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name",
                unique: true);

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
                name: "IX_Orders_Number",
                table: "Orders",
                column: "Number",
                unique: true);

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
                name: "IX_SearchItems_UserId",
                table: "SearchItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Uslugi_ItemId",
                table: "Uslugi",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_ItemId",
                table: "WishlistItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_UserId_ItemId",
                table: "WishlistItems",
                columns: new[] { "UserId", "ItemId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

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
                name: "SearchItems");

            migrationBuilder.DropTable(
                name: "Uslugi");

            migrationBuilder.DropTable(
                name: "WishlistItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Complects");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
