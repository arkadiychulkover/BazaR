using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BazaR.Migrations
{
    /// <inheritdoc />
    public partial class Logging : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SearchFilters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Query = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Page = table.Column<int>(type: "int", nullable: false),
                    Sort = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BrandIds = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchFilters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VisitingModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    userAction = table.Column<int>(type: "int", nullable: false),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ControllerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    SearchFiltersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitingModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitingModels_SearchFilters_SearchFiltersId",
                        column: x => x.SearchFiltersId,
                        principalTable: "SearchFilters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitingModels_SearchFiltersId",
                table: "VisitingModels",
                column: "SearchFiltersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitingModels");

            migrationBuilder.DropTable(
                name: "SearchFilters");
        }
    }
}
