using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BazaR.Migrations
{
    /// <inheritdoc />
    public partial class asets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/images/icon-laptops-and-computers.svg", "/AssetsIconImg/images/categoryImg-laptops-and-computers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/images/icon-smartphones-tv-electronics.svg", "/AssetsIconImg/images/categoryImg-smartphones-tv-electronics.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/images/icon-gaming.svg", "/AssetsIconImg/images/categoryImg-gaming.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/images/icon-home-appliances.svg", "/AssetsIconImg/images/categoryImg-home-appliances.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/images/icon-home-goods.svg", "/AssetsIconImg/images/categoryImg-home-goods.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/images/icon-tools-auto.svg", "/AssetsIconImg/images/categoryImg-tools-auto.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/images/icon-plumbing-renovation.svg", "/AssetsIconImg/images/categoryImg-plumbing-renovation.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/images/icon-garden.svg", "/AssetsIconImg/images/categoryImg-garden.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/images/icon-sports-hobbies.svg", "/AssetsIconImg/images/categoryImg-sports-hobbies.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/images/icon-clothing-footwear-jewelry.svg", "/AssetsIconImg/images/categoryImg-clothing-footwear-jewelry.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/images/icon-beauty-health.svg", "/AssetsIconImg/images/categoryImg-beauty-health.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/images/icon-baby-products.svg", "/AssetsIconImg/images/categoryImg-baby-products.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/images/icon-pet-supplies.svg", "/AssetsIconImg/images/categoryImg-pet-supplies.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/images/icon-stationery-books.svg", "/AssetsIconImg/images/categoryImg-stationery-books.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/images/icon-alcohol-food.svg", "/AssetsIconImg/images/categoryImg-alcohol-food.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/images/icon-business-services.svg", "/AssetsIconImg/images/categoryImg-business-services.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/images/icon-tourism-outdoor.svg", "/AssetsIconImg/images/categoryImg-tourism-outdoor.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/images/icon-promotions.svg", "/AssetsIconImg/images/categoryImg-promotions.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/images/icon-total-sale.svg", "/AssetsIconImg/images/categoryImg-total-sale.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Notebooks.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-Notebooks.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-GamingNotebooks.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-GamingNotebooks.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Ultrabooks.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-Ultrabooks.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-ForStudy.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-ForStudy.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-ForWork.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-ForWork.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Chromebook.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-Chromebook.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Computers.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-Computers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-DesktopPC.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-DesktopPC.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-GamingPC.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-GamingPC.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-MiniPC.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-MiniPC.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Monoblocks.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-Monoblocks.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Workstations.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-Workstations.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Components.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-Components.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Processors.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-Processors.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-VideoCards.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-VideoCards.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Motherboards.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-Motherboards.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-RAM.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-RAM.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-PowerSupplies.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-PowerSupplies.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Cases.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-Cases.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Storage.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-Storage.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 121,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-SSD.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-SSD.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 122,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-HDD.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-HDD.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 123,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-ExternalDrives.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-ExternalDrives.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 124,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-NAS.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-NAS.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 125,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Peripherals.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-Peripherals.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 126,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Keyboards.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-Keyboards.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 127,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Mice.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-Mice.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 128,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-MousePads.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-MousePads.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 129,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Webcams.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryImg-Webcams.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 201,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Smartphones.svg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryImg-Smartphones.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 202,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Android.svg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryImg-Android.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 203,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-iPhone.svg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryImg-iPhone.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 204,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Budget.svg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryImg-Budget.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 205,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Flagship.svg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryImg-Flagship.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 206,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-TVs.svg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryImg-TVs.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 207,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-SmartTV.svg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryImg-SmartTV.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 208,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-LED.svg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryImg-LED.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 209,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-OLED.svg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryImg-OLED.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 210,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-QLED.svg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryImg-QLED.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 211,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Audio.svg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryImg-Audio.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 212,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Headphones.svg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryImg-Headphones.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 213,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Soundbars.svg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryImg-Soundbars.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 214,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Speakers.svg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryImg-Speakers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 215,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-HomeTheaters.svg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryImg-HomeTheaters.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 216,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Tablets.svg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryImg-Tablets.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 217,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-iPad.svg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryImg-iPad.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 218,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-AndroidTablets.svg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryImg-AndroidTablets.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 219,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Gadgets.svg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryImg-Gadgets.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 220,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-SmartWatches.svg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryImg-SmartWatches.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 221,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-FitnessBands.svg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryImg-FitnessBands.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 301,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-Consoles.svg", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryImg-Consoles.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 302,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-PlayStation.svg", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryImg-PlayStation.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 303,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-Xbox.svg", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryImg-Xbox.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 304,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-Nintendo.svg", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryImg-Nintendo.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 305,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-Games.svg", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryImg-Games.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 306,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-PlayStationGames.svg", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryImg-PlayStationGames.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 307,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-XboxGames.svg", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryImg-XboxGames.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 308,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-PCGames.svg", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryImg-PCGames.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 309,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-GamingPeripherals.svg", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryImg-GamingPeripherals.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 310,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-GamingMice.svg", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryImg-GamingMice.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 311,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-GamingKeyboards.svg", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryImg-GamingKeyboards.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 312,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-GamingHeadphones.svg", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryImg-GamingHeadphones.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 313,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-VR.svg", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryImg-VR.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 314,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-VRHeadsets.svg", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryImg-VRHeadsets.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 315,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-VRAccessories.svg", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryImg-VRAccessories.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 401,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-LargeAppliances.svg", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryImg-LargeAppliances.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 402,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Refrigerators.svg", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryImg-Refrigerators.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 403,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-WashingMachines.svg", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryImg-WashingMachines.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 404,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Dishwashers.svg", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryImg-Dishwashers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 405,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-KitchenAppliances.svg", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryImg-KitchenAppliances.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 406,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Microwaves.svg", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryImg-Microwaves.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 407,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Blenders.svg", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryImg-Blenders.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 408,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Mixers.svg", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryImg-Mixers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 409,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Multicookers.svg", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryImg-Multicookers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 410,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-ClimateControl.svg", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryImg-ClimateControl.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 411,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-AirConditioners.svg", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryImg-AirConditioners.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 412,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Heaters.svg", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryImg-Heaters.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 413,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Fans.svg", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryImg-Fans.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 414,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Cleaning.svg", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryImg-Cleaning.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 415,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-VacuumCleaners.svg", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryImg-VacuumCleaners.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 416,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-RobotVacuums.svg", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryImg-RobotVacuums.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 501,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Furniture.svg", "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryImg-Furniture.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 502,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Sofas.svg", "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryImg-Sofas.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 503,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Beds.svg", "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryImg-Beds.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 504,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Wardrobes.svg", "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryImg-Wardrobes.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 505,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Lighting.svg", "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryImg-Lighting.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 506,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Lamps.svg", "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryImg-Lamps.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 507,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Chandeliers.svg", "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryImg-Chandeliers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 508,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-LEDLighting.svg", "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryImg-LEDLighting.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 509,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Decor.svg", "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryImg-Decor.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 510,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Paintings.svg", "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryImg-Paintings.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 511,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Mirrors.svg", "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryImg-Mirrors.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 512,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Clocks.svg", "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryImg-Clocks.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 601,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-PowerTools.svg", "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryImg-PowerTools.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 602,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-Drills.svg", "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryImg-Drills.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 603,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-Screwdrivers.svg", "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryImg-Screwdrivers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 604,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-AngleGrinders.svg", "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryImg-AngleGrinders.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 605,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-CarElectronics.svg", "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryImg-CarElectronics.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 606,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-DashCams.svg", "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryImg-DashCams.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 607,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-GPS.svg", "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryImg-GPS.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 608,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-CarAccessories.svg", "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryImg-CarAccessories.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 609,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-PhoneHolders.svg", "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryImg-PhoneHolders.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 610,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-CarChargers.svg", "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryImg-CarChargers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 701,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Bathroom.svg", "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryImg-Bathroom.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 702,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-ShowerCubicles.svg", "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryImg-ShowerCubicles.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 703,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Toilets.svg", "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryImg-Toilets.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 704,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Sinks.svg", "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryImg-Sinks.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 705,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Tools.svg", "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryImg-Tools.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 706,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-HandTools.svg", "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryImg-HandTools.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 707,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-MeasuringTools.svg", "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryImg-MeasuringTools.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 708,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Materials.svg", "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryImg-Materials.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 709,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Paint.svg", "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryImg-Paint.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 710,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Tiles.svg", "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryImg-Tiles.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 711,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Laminate.svg", "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryImg-Laminate.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 801,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-GardenTools.svg", "/AssetsIconImg/SecondLevelCategories/Garden/categoryImg-GardenTools.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 802,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-LawnMowers.svg", "/AssetsIconImg/SecondLevelCategories/Garden/categoryImg-LawnMowers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 803,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-Trimmers.svg", "/AssetsIconImg/SecondLevelCategories/Garden/categoryImg-Trimmers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 804,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-GardenImplements.svg", "/AssetsIconImg/SecondLevelCategories/Garden/categoryImg-GardenImplements.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 805,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-Shovels.svg", "/AssetsIconImg/SecondLevelCategories/Garden/categoryImg-Shovels.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 806,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-Pruners.svg", "/AssetsIconImg/SecondLevelCategories/Garden/categoryImg-Pruners.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 807,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-GardenFurniture.svg", "/AssetsIconImg/SecondLevelCategories/Garden/categoryImg-GardenFurniture.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 808,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-GardenTables.svg", "/AssetsIconImg/SecondLevelCategories/Garden/categoryImg-GardenTables.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 809,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-GardenChairs.svg", "/AssetsIconImg/SecondLevelCategories/Garden/categoryImg-GardenChairs.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 901,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-Fitness.svg", "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryImg-Fitness.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 902,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-Dumbbells.svg", "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryImg-Dumbbells.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 903,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-Treadmills.svg", "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryImg-Treadmills.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 904,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-Cycling.svg", "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryImg-Cycling.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 905,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-Bicycles.svg", "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryImg-Bicycles.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 906,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-CyclingAccessories.svg", "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryImg-CyclingAccessories.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 907,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-OutdoorRecreation.svg", "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryImg-OutdoorRecreation.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 908,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-KickScooters.svg", "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryImg-KickScooters.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 909,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-ElectricScooters.svg", "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryImg-ElectricScooters.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1001,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-MensClothing.svg", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryImg-MensClothing.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1002,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-TShirts.svg", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryImg-TShirts.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1003,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Jeans.svg", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryImg-Jeans.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1004,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Jackets.svg", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryImg-Jackets.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1005,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-WomensClothing.svg", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryImg-WomensClothing.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1006,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Dresses.svg", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryImg-Dresses.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1007,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Skirts.svg", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryImg-Skirts.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1008,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Footwear.svg", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryImg-Footwear.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1009,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Sneakers.svg", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryImg-Sneakers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1010,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Boots.svg", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryImg-Boots.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1011,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Accessories.svg", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryImg-Accessories.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1012,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Bags.svg", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryImg-Bags.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1013,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Belts.svg", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryImg-Belts.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1101,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-FaceCare.svg", "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryImg-FaceCare.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1102,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-Creams.svg", "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryImg-Creams.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1103,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-Serums.svg", "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryImg-Serums.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1104,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-HairCare.svg", "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryImg-HairCare.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1105,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-Shampoos.svg", "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryImg-Shampoos.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1106,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-HairMasks.svg", "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryImg-HairMasks.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1107,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-BeautyTech.svg", "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryImg-BeautyTech.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1108,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-HairDryers.svg", "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryImg-HairDryers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1109,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-Razors.svg", "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryImg-Razors.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1201,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Toys.svg", "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryImg-Toys.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1202,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-ConstructionToys.svg", "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryImg-ConstructionToys.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1203,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Dolls.svg", "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryImg-Dolls.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1204,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Cars.svg", "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryImg-Cars.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1205,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Baby.svg", "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryImg-Baby.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1206,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Diapers.svg", "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryImg-Diapers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1207,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-BabyBottles.svg", "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryImg-BabyBottles.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1208,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-KidsVehicles.svg", "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryImg-KidsVehicles.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1209,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Strollers.svg", "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryImg-Strollers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1210,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-KidsScooters.svg", "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryImg-KidsScooters.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1301,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-Dogs.svg", "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryImg-Dogs.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1302,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-DogFood.svg", "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryImg-DogFood.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1303,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-DogToys.svg", "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryImg-DogToys.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1304,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-Cats.svg", "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryImg-Cats.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1305,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-CatFood.svg", "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryImg-CatFood.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1306,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-CatLitter.svg", "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryImg-CatLitter.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1307,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-Rodents.svg", "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryImg-Rodents.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1308,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-Cages.svg", "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryImg-Cages.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1309,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-RodentFood.svg", "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryImg-RodentFood.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1401,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Stationery.svg", "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryImg-Stationery.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1402,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Pens.svg", "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryImg-Pens.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1403,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Notebooks.svg", "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryImg-Notebooks.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1404,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Paper.svg", "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryImg-Paper.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1405,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Books.svg", "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryImg-Books.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1406,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Fiction.svg", "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryImg-Fiction.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1407,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Educational.svg", "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryImg-Educational.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1501,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Alcohol.svg", "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryImg-Alcohol.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1502,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Wine.svg", "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryImg-Wine.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1503,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Beer.svg", "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryImg-Beer.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1504,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Whiskey.svg", "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryImg-Whiskey.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1505,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Food.svg", "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryImg-Food.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1506,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Sweets.svg", "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryImg-Sweets.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1507,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Snacks.svg", "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryImg-Snacks.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1601,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-Office.svg", "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryImg-Office.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1602,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-OfficeEquipment.svg", "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryImg-OfficeEquipment.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1603,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-OfficeFurniture.svg", "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryImg-OfficeFurniture.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1604,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-BusinessEquipment.svg", "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryImg-BusinessEquipment.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1605,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-POS.svg", "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryImg-POS.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1606,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-CashRegisters.svg", "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryImg-CashRegisters.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1701,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-TourismGear.svg", "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryImg-TourismGear.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1702,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-Tents.svg", "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryImg-Tents.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1703,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-SleepingBags.svg", "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryImg-SleepingBags.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1704,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-Travel.svg", "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryImg-Travel.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1705,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-Suitcases.svg", "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryImg-Suitcases.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1706,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-Backpacks.svg", "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryImg-Backpacks.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1801,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Promotions/categoryIcon-Discounted.svg", "/AssetsIconImg/SecondLevelCategories/Promotions/categoryImg-Discounted.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1802,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Promotions/categoryIcon-SeasonalSales.svg", "/AssetsIconImg/SecondLevelCategories/Promotions/categoryImg-SeasonalSales.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1901,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/TotalSale/categoryIcon-UpTo50.svg", "/AssetsIconImg/SecondLevelCategories/TotalSale/categoryImg-UpTo50.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1902,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/TotalSale/categoryIcon-UpTo70.svg", "/AssetsIconImg/SecondLevelCategories/TotalSale/categoryImg-UpTo70.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1903,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/TotalSale/categoryIcon-LastItems.svg", "/AssetsIconImg/SecondLevelCategories/TotalSale/categoryImg-LastItems.svg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "icon-laptops-and-computers.svg", "categoryImg-laptops-and-computers.svg" });

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
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "icon-tourism-outdoor.svg", "categoryImg-tourism-outdoor.svg" });

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
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Notebooks.svg", "categoryImg-Notebooks.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-GamingNotebooks.svg", "categoryImg-GamingNotebooks.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Ultrabooks.svg", "categoryImg-Ultrabooks.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-ForStudy.svg", "categoryImg-ForStudy.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-ForWork.svg", "categoryImg-ForWork.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Chromebook.svg", "categoryImg-Chromebook.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Computers.svg", "categoryImg-Computers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-DesktopPC.svg", "categoryImg-DesktopPC.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-GamingPC.svg", "categoryImg-GamingPC.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-MiniPC.svg", "categoryImg-MiniPC.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Monoblocks.svg", "categoryImg-Monoblocks.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Workstations.svg", "categoryImg-Workstations.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Components.svg", "categoryImg-Components.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Processors.svg", "categoryImg-Processors.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-VideoCards.svg", "categoryImg-VideoCards.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Motherboards.svg", "categoryImg-Motherboards.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-RAM.svg", "categoryImg-RAM.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-PowerSupplies.svg", "categoryImg-PowerSupplies.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Cases.svg", "categoryImg-Cases.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Storage.svg", "categoryImg-Storage.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 121,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-SSD.svg", "categoryImg-SSD.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 122,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-HDD.svg", "categoryImg-HDD.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 123,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-ExternalDrives.svg", "categoryImg-ExternalDrives.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 124,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-NAS.svg", "categoryImg-NAS.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 125,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Peripherals.svg", "categoryImg-Peripherals.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 126,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Keyboards.svg", "categoryImg-Keyboards.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 127,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Mice.svg", "categoryImg-Mice.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 128,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-MousePads.svg", "categoryImg-MousePads.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 129,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Webcams.svg", "categoryImg-Webcams.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 201,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Smartphones.svg", "categoryImg-Smartphones.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 202,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Android.svg", "categoryImg-Android.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 203,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-iPhone.svg", "categoryImg-iPhone.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 204,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Budget.svg", "categoryImg-Budget.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 205,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Flagship.svg", "categoryImg-Flagship.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 206,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-TVs.svg", "categoryImg-TVs.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 207,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-SmartTV.svg", "categoryImg-SmartTV.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 208,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-LED.svg", "categoryImg-LED.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 209,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-OLED.svg", "categoryImg-OLED.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 210,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-QLED.svg", "categoryImg-QLED.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 211,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Audio.svg", "categoryImg-Audio.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 212,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Headphones.svg", "categoryImg-Headphones.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 213,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Soundbars.svg", "categoryImg-Soundbars.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 214,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Speakers.svg", "categoryImg-Speakers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 215,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-HomeTheaters.svg", "categoryImg-HomeTheaters.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 216,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Tablets.svg", "categoryImg-Tablets.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 217,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-iPad.svg", "categoryImg-iPad.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 218,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-AndroidTablets.svg", "categoryImg-AndroidTablets.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 219,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Gadgets.svg", "categoryImg-Gadgets.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 220,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-SmartWatches.svg", "categoryImg-SmartWatches.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 221,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-FitnessBands.svg", "categoryImg-FitnessBands.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 301,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Consoles.svg", "categoryImg-Consoles.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 302,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-PlayStation.svg", "categoryImg-PlayStation.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 303,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Xbox.svg", "categoryImg-Xbox.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 304,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Nintendo.svg", "categoryImg-Nintendo.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 305,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Games.svg", "categoryImg-Games.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 306,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-PlayStationGames.svg", "categoryImg-PlayStationGames.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 307,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-XboxGames.svg", "categoryImg-XboxGames.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 308,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-PCGames.svg", "categoryImg-PCGames.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 309,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-GamingPeripherals.svg", "categoryImg-GamingPeripherals.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 310,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-GamingMice.svg", "categoryImg-GamingMice.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 311,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-GamingKeyboards.svg", "categoryImg-GamingKeyboards.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 312,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-GamingHeadphones.svg", "categoryImg-GamingHeadphones.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 313,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-VR.svg", "categoryImg-VR.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 314,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-VRHeadsets.svg", "categoryImg-VRHeadsets.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 315,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-VRAccessories.svg", "categoryImg-VRAccessories.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 401,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-LargeAppliances.svg", "categoryImg-LargeAppliances.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 402,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Refrigerators.svg", "categoryImg-Refrigerators.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 403,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-WashingMachines.svg", "categoryImg-WashingMachines.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 404,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Dishwashers.svg", "categoryImg-Dishwashers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 405,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-KitchenAppliances.svg", "categoryImg-KitchenAppliances.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 406,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Microwaves.svg", "categoryImg-Microwaves.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 407,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Blenders.svg", "categoryImg-Blenders.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 408,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Mixers.svg", "categoryImg-Mixers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 409,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Multicookers.svg", "categoryImg-Multicookers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 410,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-ClimateControl.svg", "categoryImg-ClimateControl.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 411,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-AirConditioners.svg", "categoryImg-AirConditioners.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 412,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Heaters.svg", "categoryImg-Heaters.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 413,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Fans.svg", "categoryImg-Fans.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 414,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Cleaning.svg", "categoryImg-Cleaning.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 415,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-VacuumCleaners.svg", "categoryImg-VacuumCleaners.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 416,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-RobotVacuums.svg", "categoryImg-RobotVacuums.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 501,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Furniture.svg", "categoryImg-Furniture.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 502,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Sofas.svg", "categoryImg-Sofas.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 503,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Beds.svg", "categoryImg-Beds.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 504,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Wardrobes.svg", "categoryImg-Wardrobes.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 505,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Lighting.svg", "categoryImg-Lighting.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 506,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Lamps.svg", "categoryImg-Lamps.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 507,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Chandeliers.svg", "categoryImg-Chandeliers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 508,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-LEDLighting.svg", "categoryImg-LEDLighting.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 509,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Decor.svg", "categoryImg-Decor.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 510,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Paintings.svg", "categoryImg-Paintings.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 511,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Mirrors.svg", "categoryImg-Mirrors.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 512,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Clocks.svg", "categoryImg-Clocks.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 601,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-PowerTools.svg", "categoryImg-PowerTools.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 602,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Drills.svg", "categoryImg-Drills.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 603,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Screwdrivers.svg", "categoryImg-Screwdrivers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 604,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-AngleGrinders.svg", "categoryImg-AngleGrinders.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 605,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-CarElectronics.svg", "categoryImg-CarElectronics.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 606,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-DashCams.svg", "categoryImg-DashCams.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 607,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-GPS.svg", "categoryImg-GPS.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 608,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-CarAccessories.svg", "categoryImg-CarAccessories.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 609,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-PhoneHolders.svg", "categoryImg-PhoneHolders.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 610,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-CarChargers.svg", "categoryImg-CarChargers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 701,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Bathroom.svg", "categoryImg-Bathroom.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 702,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-ShowerCubicles.svg", "categoryImg-ShowerCubicles.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 703,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Toilets.svg", "categoryImg-Toilets.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 704,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Sinks.svg", "categoryImg-Sinks.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 705,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Tools.svg", "categoryImg-Tools.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 706,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-HandTools.svg", "categoryImg-HandTools.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 707,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-MeasuringTools.svg", "categoryImg-MeasuringTools.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 708,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Materials.svg", "categoryImg-Materials.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 709,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Paint.svg", "categoryImg-Paint.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 710,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Tiles.svg", "categoryImg-Tiles.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 711,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Laminate.svg", "categoryImg-Laminate.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 801,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-GardenTools.svg", "categoryImg-GardenTools.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 802,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-LawnMowers.svg", "categoryImg-LawnMowers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 803,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Trimmers.svg", "categoryImg-Trimmers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 804,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-GardenImplements.svg", "categoryImg-GardenImplements.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 805,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Shovels.svg", "categoryImg-Shovels.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 806,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Pruners.svg", "categoryImg-Pruners.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 807,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-GardenFurniture.svg", "categoryImg-GardenFurniture.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 808,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-GardenTables.svg", "categoryImg-GardenTables.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 809,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-GardenChairs.svg", "categoryImg-GardenChairs.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 901,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Fitness.svg", "categoryImg-Fitness.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 902,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Dumbbells.svg", "categoryImg-Dumbbells.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 903,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Treadmills.svg", "categoryImg-Treadmills.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 904,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Cycling.svg", "categoryImg-Cycling.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 905,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Bicycles.svg", "categoryImg-Bicycles.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 906,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-CyclingAccessories.svg", "categoryImg-CyclingAccessories.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 907,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-OutdoorRecreation.svg", "categoryImg-OutdoorRecreation.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 908,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-KickScooters.svg", "categoryImg-KickScooters.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 909,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-ElectricScooters.svg", "categoryImg-ElectricScooters.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1001,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-MensClothing.svg", "categoryImg-MensClothing.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1002,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-TShirts.svg", "categoryImg-TShirts.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1003,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Jeans.svg", "categoryImg-Jeans.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1004,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Jackets.svg", "categoryImg-Jackets.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1005,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-WomensClothing.svg", "categoryImg-WomensClothing.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1006,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Dresses.svg", "categoryImg-Dresses.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1007,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Skirts.svg", "categoryImg-Skirts.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1008,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Footwear.svg", "categoryImg-Footwear.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1009,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Sneakers.svg", "categoryImg-Sneakers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1010,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Boots.svg", "categoryImg-Boots.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1011,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Accessories.svg", "categoryImg-Accessories.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1012,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Bags.svg", "categoryImg-Bags.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1013,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Belts.svg", "categoryImg-Belts.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1101,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-FaceCare.svg", "categoryImg-FaceCare.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1102,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Creams.svg", "categoryImg-Creams.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1103,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Serums.svg", "categoryImg-Serums.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1104,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-HairCare.svg", "categoryImg-HairCare.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1105,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Shampoos.svg", "categoryImg-Shampoos.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1106,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-HairMasks.svg", "categoryImg-HairMasks.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1107,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-BeautyTech.svg", "categoryImg-BeautyTech.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1108,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-HairDryers.svg", "categoryImg-HairDryers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1109,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Razors.svg", "categoryImg-Razors.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1201,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Toys.svg", "categoryImg-Toys.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1202,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-ConstructionToys.svg", "categoryImg-ConstructionToys.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1203,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Dolls.svg", "categoryImg-Dolls.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1204,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Cars.svg", "categoryImg-Cars.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1205,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Baby.svg", "categoryImg-Baby.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1206,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Diapers.svg", "categoryImg-Diapers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1207,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-BabyBottles.svg", "categoryImg-BabyBottles.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1208,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-KidsVehicles.svg", "categoryImg-KidsVehicles.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1209,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Strollers.svg", "categoryImg-Strollers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1210,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-KidsScooters.svg", "categoryImg-KidsScooters.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1301,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Dogs.svg", "categoryImg-Dogs.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1302,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-DogFood.svg", "categoryImg-DogFood.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1303,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-DogToys.svg", "categoryImg-DogToys.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1304,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Cats.svg", "categoryImg-Cats.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1305,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-CatFood.svg", "categoryImg-CatFood.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1306,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-CatLitter.svg", "categoryImg-CatLitter.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1307,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Rodents.svg", "categoryImg-Rodents.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1308,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Cages.svg", "categoryImg-Cages.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1309,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-RodentFood.svg", "categoryImg-RodentFood.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1401,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Stationery.svg", "categoryImg-Stationery.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1402,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Pens.svg", "categoryImg-Pens.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1403,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Notebooks.svg", "categoryImg-Notebooks.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1404,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Paper.svg", "categoryImg-Paper.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1405,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Books.svg", "categoryImg-Books.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1406,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Fiction.svg", "categoryImg-Fiction.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1407,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Educational.svg", "categoryImg-Educational.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1501,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Alcohol.svg", "categoryImg-Alcohol.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1502,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Wine.svg", "categoryImg-Wine.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1503,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Beer.svg", "categoryImg-Beer.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1504,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Whiskey.svg", "categoryImg-Whiskey.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1505,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Food.svg", "categoryImg-Food.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1506,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Sweets.svg", "categoryImg-Sweets.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1507,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Snacks.svg", "categoryImg-Snacks.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1601,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Office.svg", "categoryImg-Office.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1602,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-OfficeEquipment.svg", "categoryImg-OfficeEquipment.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1603,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-OfficeFurniture.svg", "categoryImg-OfficeFurniture.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1604,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-BusinessEquipment.svg", "categoryImg-BusinessEquipment.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1605,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-POS.svg", "categoryImg-POS.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1606,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-CashRegisters.svg", "categoryImg-CashRegisters.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1701,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-TourismGear.svg", "categoryImg-TourismGear.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1702,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Tents.svg", "categoryImg-Tents.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1703,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-SleepingBags.svg", "categoryImg-SleepingBags.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1704,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Travel.svg", "categoryImg-Travel.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1705,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Suitcases.svg", "categoryImg-Suitcases.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1706,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Backpacks.svg", "categoryImg-Backpacks.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1801,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-Discounted.svg", "categoryImg-Discounted.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1802,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-SeasonalSales.svg", "categoryImg-SeasonalSales.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1901,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-UpTo50.svg", "categoryImg-UpTo50.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1902,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-UpTo70.svg", "categoryImg-UpTo70.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1903,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "categoryIcon-LastItems.svg", "categoryImg-LastItems.svg" });
        }
    }
}
