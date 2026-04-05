using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BazaR.Migrations
{
    /// <inheritdoc />
    public partial class morecats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Users_UserId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WishlistItems_Users_UserId",
                table: "WishlistItems");

            migrationBuilder.DropIndex(
                name: "IX_WishlistItems_UserId",
                table: "WishlistItems");

            migrationBuilder.DropIndex(
                name: "IX_Items_IsAvailable",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_Name",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_Price",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_UserId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_Brands_Name",
                table: "Brands");

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 417);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 418);

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
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 5, 1 });

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
                keyValues: new object[] { 6, 6 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 19, 6 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 20, 6 });

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
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 1, 201 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 2, 201 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 3, 201 });

            migrationBuilder.DeleteData(
                table: "CategoryBrands",
                keyColumns: new[] { "BrandId", "CategoryId" },
                keyValues: new object[] { 4, 201 });

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1053);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1052);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1051);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1050);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1049);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1048);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1047);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1046);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1045);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1044);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1043);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1042);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1041);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1040);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1039);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1038);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1037);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1036);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1035);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1034);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1033);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1032);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1031);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1030);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1029);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1028);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1027);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1026);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1025);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1024);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1023);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1022);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1021);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1020);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1019);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1018);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1017);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1016);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1015);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1014);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1013);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1012);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1011);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1010);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1009);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1008);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1007);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1006);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1005);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1004);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1003);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1002);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1001);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1000);

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
                table: "Brands",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Uslugi",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Uslugi",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Reviews",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Ttn",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentStatus",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMethod",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryMethod",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Orders",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Items",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Items",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Desc",
                table: "Items",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "ItemColors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ItemCharacteristics",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ItemCharacteristics",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SendingPlace",
                table: "Deliveries",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryPlace",
                table: "Deliveries",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Complects",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "CategoryFilters",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "CategoryFilters",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ImgUrl",
                table: "Categories",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IconUrl",
                table: "Categories",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Brands",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                table: "Brands",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IconUrl", "ImgUrl", "Name" },
                values: new object[] { "icon-laptops-and-computers.svg", "categoryImg-laptops-and-computers.svg", "Ноутбуки та комп'ютери" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "icon-smartphones-tv-electronics.svg", "categoryImg-smartphones-tv-electronics.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "icon-gaming.svg", "categoryImg-gaming.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "icon-home-appliances.svg", "categoryImg-home-appliances.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "icon-home-goods.svg", "categoryImg-home-goods.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "icon-tools-auto.svg", "categoryImg-tools-auto.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "icon-plumbing-renovation.svg", "categoryImg-plumbing-renovation.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "icon-garden.svg", "categoryImg-garden.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "icon-sports-hobbies.svg", "categoryImg-sports-hobbies.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "icon-clothing-footwear-jewelry.svg", "categoryImg-clothing-footwear-jewelry.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "icon-beauty-health.svg", "categoryImg-beauty-health.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "icon-baby-products.svg", "categoryImg-baby-products.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "icon-pet-supplies.svg", "categoryImg-pet-supplies.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "icon-stationery-books.svg", "categoryImg-stationery-books.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "icon-alcohol-food.svg", "categoryImg-alcohol-food.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "icon-business-services.svg", "categoryImg-business-services.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "IconUrl", "ImgUrl", "Name" },
                values: new object[] { "icon-tourism-outdoor.svg", "categoryImg-tourism-outdoor.svg", "Туризм та відпочинок" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "icon-promotions.svg", "categoryImg-promotions.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "icon-total-sale.svg", "categoryImg-total-sale.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 101,
                column: "Name",
                value: "Ноутбуки");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 102,
                column: "Name",
                value: "Ігрові ноутбуки");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 103,
                column: "Name",
                value: "Ультрабуки");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 104,
                column: "Name",
                value: "Для навчання");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 105,
                column: "Name",
                value: "Для роботи");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 106,
                column: "Name",
                value: "Chromebook");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 107,
                column: "Name",
                value: "Комп'ютери");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 8, "Настільні ПК", 1 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 9, "Ігрові ПК", 1 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 10, "Міні-ПК", 1 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 11, "Моноблоки", 1 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DisplayOrder", "Name" },
                values: new object[] { 20, "Накопичувачі" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 121,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 21, "SSD", 1 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 122,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 22, "HDD", 1 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 123,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 23, "Зовнішні диски", 1 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 124,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 24, "NAS", 1 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 125,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 25, "Периферія", 1 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 202,
                column: "Name",
                value: "Android");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 203,
                column: "Name",
                value: "iPhone");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 204,
                column: "Name",
                value: "Бюджетні");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 205,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 5, "Флагмани", 2 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 206,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 6, "Телевізори", 2 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 207,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 7, "Smart TV", 2 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 208,
                columns: new[] { "DisplayOrder", "Name" },
                values: new object[] { 8, "LED" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 209,
                columns: new[] { "DisplayOrder", "Name" },
                values: new object[] { 9, "OLED" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 210,
                columns: new[] { "DisplayOrder", "Name" },
                values: new object[] { 10, "QLED" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 211,
                columns: new[] { "DisplayOrder", "Name" },
                values: new object[] { 11, "Аудіо" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 212,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 12, "Навушники", 2 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 213,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 13, "Саундбари", 2 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 214,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 14, "Колонки", 2 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 215,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 15, "Домашні кінотеатри", 2 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 301,
                column: "Name",
                value: "Консолі");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 302,
                column: "Name",
                value: "PlayStation");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 303,
                column: "Name",
                value: "Xbox");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 304,
                column: "Name",
                value: "Nintendo");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 305,
                column: "Name",
                value: "Ігри");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 306,
                column: "Name",
                value: "PlayStation ігри");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 307,
                column: "Name",
                value: "Xbox ігри");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 308,
                column: "Name",
                value: "PC ігри");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 309,
                column: "Name",
                value: "Геймерська периферія");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 310,
                column: "Name",
                value: "Ігрові миші");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 311,
                column: "Name",
                value: "Ігрові клавіатури");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 401,
                column: "Name",
                value: "Велика техніка");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 402,
                columns: new[] { "DisplayOrder", "ParentCategoryId" },
                values: new object[] { 2, 4 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 403,
                columns: new[] { "DisplayOrder", "ParentCategoryId" },
                values: new object[] { 3, 4 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 404,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 4, "Посудомийні машини", 4 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 405,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 5, "Кухонна техніка", 4 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 406,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 6, "Мікрохвильові печі", 4 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 407,
                columns: new[] { "DisplayOrder", "Name" },
                values: new object[] { 7, "Блендери" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 408,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 8, "Міксери", 4 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 409,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 9, "Мультиварки", 4 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 410,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 10, "Кліматична техніка", 4 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 411,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 11, "Кондиціонери", 4 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 412,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 12, "Обігрівачі", 4 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 413,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 13, "Вентилятори", 4 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 414,
                columns: new[] { "DisplayOrder", "Name" },
                values: new object[] { 14, "Прибирання" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 415,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 15, "Пилососи", 4 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 416,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 16, "Роботи-пилососи", 4 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "IconUrl", "ImgUrl", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { 112, 12, null, null, "Робочі станції", 1 },
                    { 113, 13, null, null, "Комплектуючі", 1 },
                    { 114, 14, null, null, "Процесори", 1 },
                    { 115, 15, null, null, "Відеокарти", 1 },
                    { 116, 16, null, null, "Материнські плати", 1 },
                    { 117, 17, null, null, "Оперативна пам'ять", 1 },
                    { 118, 18, null, null, "Блоки живлення", 1 },
                    { 119, 19, null, null, "Корпуси", 1 },
                    { 126, 26, null, null, "Клавіатури", 1 },
                    { 127, 27, null, null, "Миші", 1 },
                    { 128, 28, null, null, "Килимки", 1 },
                    { 129, 29, null, null, "Вебкамери", 1 },
                    { 216, 16, null, null, "Планшети", 2 },
                    { 217, 17, null, null, "iPad", 2 },
                    { 218, 18, null, null, "Android планшети", 2 },
                    { 219, 19, null, null, "Гаджети", 2 },
                    { 220, 20, null, null, "Смарт-годинники", 2 },
                    { 221, 21, null, null, "Фітнес-браслети", 2 },
                    { 312, 12, null, null, "Геймерські навушники", 3 },
                    { 313, 13, null, null, "VR", 3 },
                    { 314, 14, null, null, "VR шоломи", 3 },
                    { 315, 15, null, null, "VR аксесуари", 3 },
                    { 501, 1, null, null, "Меблі", 5 },
                    { 502, 2, null, null, "Дивани", 5 },
                    { 503, 3, null, null, "Ліжка", 5 },
                    { 504, 4, null, null, "Шафи", 5 },
                    { 505, 5, null, null, "Освітлення", 5 },
                    { 506, 6, null, null, "Лампи", 5 },
                    { 507, 7, null, null, "Люстри", 5 },
                    { 508, 8, null, null, "LED освітлення", 5 },
                    { 509, 9, null, null, "Декор", 5 },
                    { 510, 10, null, null, "Картини", 5 },
                    { 511, 11, null, null, "Дзеркала", 5 },
                    { 512, 12, null, null, "Годинники", 5 },
                    { 601, 1, null, null, "Електроінструменти", 6 },
                    { 602, 2, null, null, "Дрилі", 6 },
                    { 603, 3, null, null, "Шуруповерти", 6 },
                    { 604, 4, null, null, "Болгарки", 6 },
                    { 605, 5, null, null, "Автоелектроніка", 6 },
                    { 606, 6, null, null, "Відеореєстратори", 6 },
                    { 607, 7, null, null, "GPS навігатори", 6 },
                    { 608, 8, null, null, "Автоаксесуари", 6 },
                    { 609, 9, null, null, "Тримачі телефону", 6 },
                    { 610, 10, null, null, "Зарядні пристрої", 6 },
                    { 701, 1, null, null, "Ванна кімната", 7 },
                    { 702, 2, null, null, "Душові кабіни", 7 },
                    { 703, 3, null, null, "Унітази", 7 },
                    { 704, 4, null, null, "Раковини", 7 },
                    { 705, 5, null, null, "Інструменти", 7 },
                    { 706, 6, null, null, "Ручний інструмент", 7 },
                    { 707, 7, null, null, "Вимірювальні прилади", 7 },
                    { 708, 8, null, null, "Матеріали", 7 },
                    { 709, 9, null, null, "Фарба", 7 },
                    { 710, 10, null, null, "Плитка", 7 },
                    { 711, 11, null, null, "Ламінат", 7 },
                    { 801, 1, null, null, "Садова техніка", 8 },
                    { 802, 2, null, null, "Газонокосарки", 8 },
                    { 803, 3, null, null, "Тримери", 8 },
                    { 804, 4, null, null, "Садові інструменти", 8 },
                    { 805, 5, null, null, "Лопати", 8 },
                    { 806, 6, null, null, "Секатори", 8 },
                    { 807, 7, null, null, "Меблі для саду", 8 },
                    { 808, 8, null, null, "Садові столи", 8 },
                    { 809, 9, null, null, "Крісла", 8 },
                    { 901, 1, null, null, "Фітнес", 9 },
                    { 902, 2, null, null, "Гантелі", 9 },
                    { 903, 3, null, null, "Бігові доріжки", 9 },
                    { 904, 4, null, null, "Велоспорт", 9 },
                    { 905, 5, null, null, "Велосипеди", 9 },
                    { 906, 6, null, null, "Аксесуари", 9 },
                    { 907, 7, null, null, "Активний відпочинок", 9 },
                    { 908, 8, null, null, "Самокати", 9 },
                    { 909, 9, null, null, "Електросамокати", 9 },
                    { 1001, 1, null, null, "Чоловічий одяг", 10 },
                    { 1002, 2, null, null, "Футболки", 10 },
                    { 1003, 3, null, null, "Джинси", 10 },
                    { 1004, 4, null, null, "Куртки", 10 },
                    { 1005, 5, null, null, "Жіночий одяг", 10 },
                    { 1006, 6, null, null, "Сукні", 10 },
                    { 1007, 7, null, null, "Спідниці", 10 },
                    { 1008, 8, null, null, "Взуття", 10 },
                    { 1009, 9, null, null, "Кросівки", 10 },
                    { 1010, 10, null, null, "Черевики", 10 },
                    { 1011, 11, null, null, "Аксесуари", 10 },
                    { 1012, 12, null, null, "Сумки", 10 },
                    { 1013, 13, null, null, "Ремені", 10 },
                    { 1101, 1, null, null, "Догляд за обличчям", 11 },
                    { 1102, 2, null, null, "Креми", 11 },
                    { 1103, 3, null, null, "Сироватки", 11 },
                    { 1104, 4, null, null, "Догляд за волоссям", 11 },
                    { 1105, 5, null, null, "Шампуні", 11 },
                    { 1106, 6, null, null, "Маски", 11 },
                    { 1107, 7, null, null, "Техніка", 11 },
                    { 1108, 8, null, null, "Фени", 11 },
                    { 1109, 9, null, null, "Бритви", 11 },
                    { 1201, 1, null, null, "Іграшки", 12 },
                    { 1202, 2, null, null, "Конструктори", 12 },
                    { 1203, 3, null, null, "Ляльки", 12 },
                    { 1204, 4, null, null, "Машинки", 12 },
                    { 1205, 5, null, null, "Для немовлят", 12 },
                    { 1206, 6, null, null, "Підгузки", 12 },
                    { 1207, 7, null, null, "Пляшечки", 12 },
                    { 1208, 8, null, null, "Дитячий транспорт", 12 },
                    { 1209, 9, null, null, "Коляски", 12 },
                    { 1210, 10, null, null, "Самокати", 12 },
                    { 1301, 1, null, null, "Для собак", 13 },
                    { 1302, 2, null, null, "Корм", 13 },
                    { 1303, 3, null, null, "Іграшки", 13 },
                    { 1304, 4, null, null, "Для котів", 13 },
                    { 1305, 5, null, null, "Корм", 13 },
                    { 1306, 6, null, null, "Наповнювачі", 13 },
                    { 1307, 7, null, null, "Для гризунів", 13 },
                    { 1308, 8, null, null, "Клітки", 13 },
                    { 1309, 9, null, null, "Корм", 13 },
                    { 1401, 1, null, null, "Канцтовари", 14 },
                    { 1402, 2, null, null, "Ручки", 14 },
                    { 1403, 3, null, null, "Зошити", 14 },
                    { 1404, 4, null, null, "Папір", 14 },
                    { 1405, 5, null, null, "Книги", 14 },
                    { 1406, 6, null, null, "Художні", 14 },
                    { 1407, 7, null, null, "Навчальні", 14 },
                    { 1501, 1, null, null, "Алкоголь", 15 },
                    { 1502, 2, null, null, "Вино", 15 },
                    { 1503, 3, null, null, "Пиво", 15 },
                    { 1504, 4, null, null, "Віскі", 15 },
                    { 1505, 5, null, null, "Продукти", 15 },
                    { 1506, 6, null, null, "Солодощі", 15 },
                    { 1507, 7, null, null, "Снеки", 15 },
                    { 1601, 1, null, null, "Офіс", 16 },
                    { 1602, 2, null, null, "Офісна техніка", 16 },
                    { 1603, 3, null, null, "Меблі", 16 },
                    { 1604, 4, null, null, "Бізнес обладнання", 16 },
                    { 1605, 5, null, null, "POS системи", 16 },
                    { 1606, 6, null, null, "Касові апарати", 16 },
                    { 1701, 1, null, null, "Туристичне спорядження", 17 },
                    { 1702, 2, null, null, "Намет", 17 },
                    { 1703, 3, null, null, "Спальні мішки", 17 },
                    { 1704, 4, null, null, "Подорожі", 17 },
                    { 1705, 5, null, null, "Валізи", 17 },
                    { 1706, 6, null, null, "Рюкзаки", 17 },
                    { 1801, 1, null, null, "Товари зі знижками", 18 },
                    { 1802, 2, null, null, "Сезонні розпродажі", 18 },
                    { 1901, 1, null, null, "До −50%", 19 },
                    { 1902, 2, null, null, "До −70%", 19 },
                    { 1903, 3, null, null, "Останні екземпляри", 19 }
                });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Kyiv");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Kharkiv");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Odesa");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Dnipro");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Lviv");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Zaporizhzhia");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Mykolaiv");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Vinnytsia");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Kherson");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Poltava");

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
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

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_UserId_ItemId",
                table: "WishlistItems",
                columns: new[] { "UserId", "ItemId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Number",
                table: "Orders",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_UserId_ItemId",
                table: "CartItems",
                columns: new[] { "UserId", "ItemId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brands_Name",
                table: "Brands",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Users_UserId",
                table: "CartItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistItems_Users_UserId",
                table: "WishlistItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Users_UserId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WishlistItems_Users_UserId",
                table: "WishlistItems");

            migrationBuilder.DropIndex(
                name: "IX_WishlistItems_UserId_ItemId",
                table: "WishlistItems");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Number",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Name",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_UserId_ItemId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_Brands_Name",
                table: "Brands");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 312);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 313);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 314);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 315);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 501);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 502);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 503);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 504);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 505);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 506);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 507);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 508);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 509);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 510);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 511);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 512);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 601);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 602);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 603);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 604);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 605);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 606);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 607);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 608);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 609);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 610);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 701);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 702);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 703);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 704);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 705);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 706);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 707);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 708);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 709);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 710);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 711);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 801);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 802);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 803);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 804);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 805);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 806);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 807);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 808);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 809);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 901);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 902);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 903);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 904);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 905);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 906);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 907);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 908);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 909);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1004);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1006);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1007);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1008);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1009);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1010);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1011);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1012);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1013);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1101);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1102);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1103);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1104);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1105);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1106);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1107);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1108);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1109);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1201);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1202);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1203);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1204);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1205);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1206);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1207);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1208);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1209);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1210);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1301);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1302);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1303);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1304);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1305);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1306);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1307);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1308);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1309);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1401);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1402);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1403);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1404);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1405);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1406);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1407);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1501);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1502);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1503);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1504);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1505);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1506);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1507);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1601);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1602);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1603);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1604);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1605);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1606);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1701);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1702);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1703);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1704);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1705);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1706);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1801);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1802);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1901);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1902);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1903);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Uslugi",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Uslugi",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<string>(
                name: "Ttn",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMethod",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryMethod",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Items",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Desc",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "ItemColors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ItemCharacteristics",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ItemCharacteristics",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "SendingPlace",
                table: "Deliveries",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryPlace",
                table: "Deliveries",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Complects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "CategoryFilters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "CategoryFilters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "ImgUrl",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IconUrl",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Brands",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

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

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IconUrl", "ImgUrl", "Name" },
                values: new object[] { "bi-laptop", null, "Ноутбуки" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "bi-phone", null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "bi-controller", null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "bi-fan", null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "bi-house", null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "bi-tools", null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "bi-droplet", null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "bi-flower", null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "bi-bicycle", null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "bi-tag", null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "bi-heart", null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "bi-emoji-smile", null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "bi-bug", null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "bi-pencil", null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "bi-cup-straw", null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "bi-briefcase", null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "IconUrl", "ImgUrl", "Name" },
                values: new object[] { "bi-tree", null, "Тури та відпочинок" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "bi-percent", null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "bi-fire", null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 101,
                column: "Name",
                value: "Asus");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 102,
                column: "Name",
                value: "Acer");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 103,
                column: "Name",
                value: "HP");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 104,
                column: "Name",
                value: "Lenovo");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 105,
                column: "Name",
                value: "Dell");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 106,
                column: "Name",
                value: "Apple");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 107,
                column: "Name",
                value: "Аксесуари для ноутбуків і ПК");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 1, "Флеш пам'ять USB", 107 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 2, "Сумки та рюкзаки для ноутбуків", 107 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 3, "Підставки та столики для ноутбуків", 107 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 4, "Веб-камери", 107 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DisplayOrder", "Name" },
                values: new object[] { 8, "Комп'ютери" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 121,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 1, "Монітори", 120 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 122,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 2, "Миші", 120 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 123,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 3, "Клавіатури", 120 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 124,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 4, "Комплект: клавіатури + миші", 120 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 125,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 5, "Мережеві сховища (NAS)", 120 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 202,
                column: "Name",
                value: "Телевізори");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 203,
                column: "Name",
                value: "Планшети");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 204,
                column: "Name",
                value: "Аудіотехніка");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 205,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 1, "Навушники", 204 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 206,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 2, "Колонки", 204 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 207,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 3, "MP3-плеєри", 204 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 208,
                columns: new[] { "DisplayOrder", "Name" },
                values: new object[] { 5, "Фотоапарати" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 209,
                columns: new[] { "DisplayOrder", "Name" },
                values: new object[] { 6, "Відеокамери" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 210,
                columns: new[] { "DisplayOrder", "Name" },
                values: new object[] { 7, "Розумний дім" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 211,
                columns: new[] { "DisplayOrder", "Name" },
                values: new object[] { 8, "Аксесуари до телефонів" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 212,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 1, "Чохли для телефонів", 211 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 213,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 2, "Захисні скельця", 211 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 214,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 3, "Зарядні пристрої", 211 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 215,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 4, "Power Bank", 211 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 301,
                column: "Name",
                value: "PlayStation");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 302,
                column: "Name",
                value: "Xbox");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 303,
                column: "Name",
                value: "Nintendo");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 304,
                column: "Name",
                value: "Ігрові консолі та приставки");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 305,
                column: "Name",
                value: "Джойстики та аксесуари");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 306,
                column: "Name",
                value: "Ігри");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 307,
                column: "Name",
                value: "Ігрові поверхні");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 308,
                column: "Name",
                value: "Ігрові крісла");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 309,
                column: "Name",
                value: "Ігрові миші");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 310,
                column: "Name",
                value: "Ігрові клавіатури");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 311,
                column: "Name",
                value: "Ігрові навушники");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 401,
                column: "Name",
                value: "Велика побутова техніка");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 402,
                columns: new[] { "DisplayOrder", "ParentCategoryId" },
                values: new object[] { 1, 401 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 403,
                columns: new[] { "DisplayOrder", "ParentCategoryId" },
                values: new object[] { 2, 401 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 404,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 3, "Плити та духовки", 401 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 405,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 4, "Мікрохвильові печі", 401 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 406,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 5, "Посудомийні машини", 401 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 407,
                columns: new[] { "DisplayOrder", "Name" },
                values: new object[] { 2, "Мала побутова техніка" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 408,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 1, "Пилососи", 407 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 409,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 2, "Праски", 407 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 410,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 3, "Блендери, міксери", 407 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 411,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 4, "Кавомашини", 407 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 412,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 5, "Електрочайники", 407 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 413,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 6, "Фени", 407 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 414,
                columns: new[] { "DisplayOrder", "Name" },
                values: new object[] { 3, "Кліматична техніка" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 415,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 1, "Кондиціонери", 414 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 416,
                columns: new[] { "DisplayOrder", "Name", "ParentCategoryId" },
                values: new object[] { 2, "Обігрівачі", 414 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "IconUrl", "ImgUrl", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { 130, 9, null, null, "Комплектуючі", 1 },
                    { 140, 10, null, null, "Мережеве обладнання", 1 },
                    { 150, 11, null, null, "Серверне обладнання", 1 },
                    { 160, 12, null, null, "Оргтехніка", 1 },
                    { 170, 13, null, null, "Програмне забезпечення", 1 },
                    { 417, 3, null, null, "Зволожувачі повітря", 414 },
                    { 418, 4, null, null, "Вентилятори", 414 }
                });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Київ");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Харків");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Одеса");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Дніпро");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Львів");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Запоріжжя");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Миколаїв");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Вінниця");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Херсон");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Полтава");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsAdmin", "Name", "PasswordHash" },
                values: new object[] { 1, "test@example.com", false, "Test User", "123456" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "IconUrl", "ImgUrl", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { 131, 1, null, null, "Відеокарти", 130 },
                    { 132, 2, null, null, "Жорсткі диски та дискові масиви", 130 },
                    { 133, 3, null, null, "Процесори", 130 },
                    { 134, 4, null, null, "SSD", 130 },
                    { 135, 5, null, null, "Оперативна пам'ять", 130 },
                    { 136, 6, null, null, "Материнські плати", 130 },
                    { 137, 7, null, null, "Блоки живлення", 130 },
                    { 141, 1, null, null, "Патч-корди", 140 },
                    { 142, 2, null, null, "Маршрутизатори", 140 },
                    { 143, 3, null, null, "IP-камери", 140 },
                    { 144, 4, null, null, "Комутатори", 140 },
                    { 145, 5, null, null, "Бездротові точки доступу", 140 },
                    { 161, 1, null, null, "БФП/Принтери", 160 },
                    { 162, 2, null, null, "Проектори", 160 },
                    { 163, 3, null, null, "Витратні матеріали для принтерів", 160 },
                    { 164, 4, null, null, "Телефонні апарати", 160 },
                    { 171, 1, null, null, "Операційні системи", 170 },
                    { 172, 2, null, null, "Офісні програми", 170 },
                    { 173, 3, null, null, "Антивірусні програми", 170 }
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
                    { 11, 10 },
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
                    { -1053, 1, 205, "Опис товару 3", 12, "/images/items/default.jpg", true, "Товар 3 категорії 205", 2500, 1 },
                    { -1052, 1, 205, "Опис товару 2", 12, "/images/items/default.jpg", true, "Товар 2 категорії 205", 2000, 1 },
                    { -1051, 1, 205, "Опис товару 1", 12, "/images/items/default.jpg", true, "Товар 1 категорії 205", 1500, 1 },
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
                    { -1021, 1, 108, "Опис товару 1", 12, "/images/items/default.jpg", true, "Товар 1 категорії 108", 1500, 1 },
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
                    { -1000, 1, 101, "Опис товару 1", 12, "/images/items/default.jpg", true, "Товар 1 категорії 101", 1500, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_UserId",
                table: "WishlistItems",
                column: "UserId");

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
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_UserId",
                table: "CartItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_Name",
                table: "Brands",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Users_UserId",
                table: "CartItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistItems_Users_UserId",
                table: "WishlistItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
