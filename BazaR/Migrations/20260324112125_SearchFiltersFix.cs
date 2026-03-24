using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BazaR.Migrations
{
    /// <inheritdoc />
    public partial class SearchFiltersFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitingModels_SearchFilters_SearchFiltersId",
                table: "VisitingModels");

            migrationBuilder.DropTable(
                name: "SearchFilters");

            migrationBuilder.DropIndex(
                name: "IX_VisitingModels_SearchFiltersId",
                table: "VisitingModels");

            migrationBuilder.DropColumn(
                name: "SearchFiltersId",
                table: "VisitingModels");

            migrationBuilder.AddColumn<string>(
                name: "SearchFilters",
                table: "VisitingModels",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SearchFilters",
                table: "VisitingModels");

            migrationBuilder.AddColumn<int>(
                name: "SearchFiltersId",
                table: "VisitingModels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SearchFilters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Page = table.Column<int>(type: "int", nullable: false),
                    Query = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sort = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchFilters", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitingModels_SearchFiltersId",
                table: "VisitingModels",
                column: "SearchFiltersId");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitingModels_SearchFilters_SearchFiltersId",
                table: "VisitingModels",
                column: "SearchFiltersId",
                principalTable: "SearchFilters",
                principalColumn: "Id");
        }
    }
}
