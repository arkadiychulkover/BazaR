using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BazaR.Migrations
{
    /// <inheritdoc />
    public partial class morecatsfixxxx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "Users",
                columns: new[] { "Id", "Email", "IsAdmin", "Name", "PasswordHash" },
                values: new object[,]
                {
                    { 1, "admin@example.com", true, "Admin User", "AQAAAAIAAYagAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==" },
                    { 2, "test@example.com", false, "Test User", "AQAAAAIAAYagAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==" }
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
                table: "Items",
                columns: new[] { "Id", "BrandId", "CategoryId", "Desc", "Garantia", "ImageUrl", "IsAvailable", "Name", "Price", "UserId" },
                values: new object[,]
                {
                    { -1106, 1, 205, "Опис товару 3 для категорії 205. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 3 категорії 205", 2500, 1 },
                    { -1105, 1, 205, "Опис товару 2 для категорії 205. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 2 категорії 205", 2000, 1 },
                    { -1104, 1, 205, "Опис товару 1 для категорії 205. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 1 категорії 205", 1500, 1 },
                    { -1103, 1, 204, "Опис товару 3 для категорії 204. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 3 категорії 204", 2500, 1 },
                    { -1102, 1, 204, "Опис товару 2 для категорії 204. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 2 категорії 204", 2000, 1 },
                    { -1101, 1, 204, "Опис товару 1 для категорії 204. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 1 категорії 204", 1500, 1 },
                    { -1100, 1, 203, "Опис товару 3 для категорії 203. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 3 категорії 203", 2500, 1 },
                    { -1099, 1, 203, "Опис товару 2 для категорії 203. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 2 категорії 203", 2000, 1 },
                    { -1098, 1, 203, "Опис товару 1 для категорії 203. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 1 категорії 203", 1500, 1 },
                    { -1097, 1, 202, "Опис товару 3 для категорії 202. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 3 категорії 202", 2500, 1 },
                    { -1096, 1, 202, "Опис товару 2 для категорії 202. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 2 категорії 202", 2000, 1 },
                    { -1095, 1, 202, "Опис товару 1 для категорії 202. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 1 категорії 202", 1500, 1 },
                    { -1094, 1, 201, "Опис товару 3 для категорії 201. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 3 категорії 201", 2500, 1 },
                    { -1093, 1, 201, "Опис товару 2 для категорії 201. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 2 категорії 201", 2000, 1 },
                    { -1092, 1, 201, "Опис товару 1 для категорії 201. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 1 категорії 201", 1500, 1 },
                    { -1091, 1, 123, "Опис товару 3 для категорії 123. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 3 категорії 123", 2500, 1 },
                    { -1090, 1, 123, "Опис товару 2 для категорії 123. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 2 категорії 123", 2000, 1 },
                    { -1089, 1, 123, "Опис товару 1 для категорії 123. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 1 категорії 123", 1500, 1 },
                    { -1088, 1, 122, "Опис товару 3 для категорії 122. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 3 категорії 122", 2500, 1 },
                    { -1087, 1, 122, "Опис товару 2 для категорії 122. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 2 категорії 122", 2000, 1 },
                    { -1086, 1, 122, "Опис товару 1 для категорії 122. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 1 категорії 122", 1500, 1 },
                    { -1085, 1, 121, "Опис товару 3 для категорії 121. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 3 категорії 121", 2500, 1 },
                    { -1084, 1, 121, "Опис товару 2 для категорії 121. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 2 категорії 121", 2000, 1 },
                    { -1083, 1, 121, "Опис товару 1 для категорії 121. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 1 категорії 121", 1500, 1 },
                    { -1082, 1, 110, "Опис товару 3 для категорії 110. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 3 категорії 110", 2500, 1 },
                    { -1081, 1, 110, "Опис товару 2 для категорії 110. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 2 категорії 110", 2000, 1 },
                    { -1080, 1, 110, "Опис товару 1 для категорії 110. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 1 категорії 110", 1500, 1 },
                    { -1079, 1, 109, "Опис товару 3 для категорії 109. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 3 категорії 109", 2500, 1 },
                    { -1078, 1, 109, "Опис товару 2 для категорії 109. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 2 категорії 109", 2000, 1 },
                    { -1077, 1, 109, "Опис товару 1 для категорії 109. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 1 категорії 109", 1500, 1 },
                    { -1076, 1, 108, "Опис товару 3 для категорії 108. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 3 категорії 108", 2500, 1 },
                    { -1075, 1, 108, "Опис товару 2 для категорії 108. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 2 категорії 108", 2000, 1 },
                    { -1074, 1, 108, "Опис товару 1 для категорії 108. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 1 категорії 108", 1500, 1 },
                    { -1073, 1, 107, "Опис товару 3 для категорії 107. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 3 категорії 107", 2500, 1 },
                    { -1072, 1, 107, "Опис товару 2 для категорії 107. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 2 категорії 107", 2000, 1 },
                    { -1071, 1, 107, "Опис товару 1 для категорії 107. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 1 категорії 107", 1500, 1 },
                    { -1070, 1, 106, "Опис товару 3 для категорії 106. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 3 категорії 106", 2500, 1 },
                    { -1069, 1, 106, "Опис товару 2 для категорії 106. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 2 категорії 106", 2000, 1 },
                    { -1068, 1, 106, "Опис товару 1 для категорії 106. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 1 категорії 106", 1500, 1 },
                    { -1067, 1, 105, "Опис товару 3 для категорії 105. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 3 категорії 105", 2500, 1 },
                    { -1066, 1, 105, "Опис товару 2 для категорії 105. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 2 категорії 105", 2000, 1 },
                    { -1065, 1, 105, "Опис товару 1 для категорії 105. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 1 категорії 105", 1500, 1 },
                    { -1064, 1, 104, "Опис товару 3 для категорії 104. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 3 категорії 104", 2500, 1 },
                    { -1063, 1, 104, "Опис товару 2 для категорії 104. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 2 категорії 104", 2000, 1 },
                    { -1062, 1, 104, "Опис товару 1 для категорії 104. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 1 категорії 104", 1500, 1 },
                    { -1061, 1, 103, "Опис товару 3 для категорії 103. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 3 категорії 103", 2500, 1 },
                    { -1060, 1, 103, "Опис товару 2 для категорії 103. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 2 категорії 103", 2000, 1 },
                    { -1059, 1, 103, "Опис товару 1 для категорії 103. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 1 категорії 103", 1500, 1 },
                    { -1058, 1, 102, "Опис товару 3 для категорії 102. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 3 категорії 102", 2500, 1 },
                    { -1057, 1, 102, "Опис товару 2 для категорії 102. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 2 категорії 102", 2000, 1 },
                    { -1056, 1, 102, "Опис товару 1 для категорії 102. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 1 категорії 102", 1500, 1 },
                    { -1055, 1, 101, "Опис товару 3 для категорії 101. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 3 категорії 101", 2500, 1 },
                    { -1054, 1, 101, "Опис товару 2 для категорії 101. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 2 категорії 101", 2000, 1 },
                    { -1053, 1, 101, "Опис товару 1 для категорії 101. Це якісний товар від відомого бренду.", 12, "/images/items/default.jpg", true, "Товар 1 категорії 101", 1500, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 12, 1 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 13, 1 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 14, 1 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 15, 1 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 16, 1 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 15, 3 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 6, 4 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 7, 9 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 8, 9 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 9, 9 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 7, 10 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 8, 10 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 9, 10 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 10, 10 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 11, 10 });

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1106);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1105);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1104);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1103);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1102);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1101);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1100);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1099);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1098);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1097);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1096);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1095);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1094);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1093);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1092);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1091);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1090);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1089);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1088);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1087);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1086);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1085);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1084);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1083);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1082);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1081);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1080);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1079);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1078);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1077);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1076);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1075);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1074);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1073);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1072);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1071);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1070);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1069);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1068);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1067);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1066);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1065);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1064);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1063);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1062);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1061);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1060);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1059);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1058);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1057);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1056);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1055);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1054);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1053);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
