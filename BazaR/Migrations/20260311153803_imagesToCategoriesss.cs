using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BazaR.Migrations
{
    /// <inheritdoc />
    public partial class imagesToCategoriesss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Country",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsNoPercentCredit",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReadyToSend",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SellerType",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "CategoryFilters",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1106,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1105,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1104,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1103,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1102,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1101,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1100,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1099,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1098,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1097,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1096,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1095,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1094,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1093,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1092,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1091,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1090,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1089,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1088,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1087,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1086,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1085,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1084,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1083,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1082,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1081,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1080,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1079,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1078,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1077,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1076,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1075,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1074,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1073,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1072,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1071,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1070,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1069,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1068,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1067,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1066,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1065,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1064,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1063,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1062,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1061,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1060,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1059,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1058,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1057,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1056,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1055,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1054,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: -1053,
                columns: new[] { "Country", "IsNoPercentCredit", "IsReadyToSend", "SellerType" },
                values: new object[] { 0, false, false, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryFilters_ItemId",
                table: "CategoryFilters",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryFilters_Items_ItemId",
                table: "CategoryFilters",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryFilters_Items_ItemId",
                table: "CategoryFilters");

            migrationBuilder.DropIndex(
                name: "IX_CategoryFilters_ItemId",
                table: "CategoryFilters");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IsNoPercentCredit",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IsReadyToSend",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SellerType",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "CategoryFilters");
        }
    }
}
