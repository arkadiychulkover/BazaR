using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BazaR.Migrations
{
    /// <inheritdoc />
    public partial class IvaDbFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                column: "Logo",
                value: "/AssetsIconImg/images/brands/apple.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                column: "Logo",
                value: "/AssetsIconImg/images/brands/samsung.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                column: "Logo",
                value: "/AssetsIconImg/images/brands/xiaomi.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                column: "Logo",
                value: "/AssetsIconImg/images/brands/sony.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                column: "Logo",
                value: "/AssetsIconImg/images/brands/lg.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                column: "Logo",
                value: "/AssetsIconImg/images/brands/bosch.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                column: "Logo",
                value: "/AssetsIconImg/images/brands/nike.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                column: "Logo",
                value: "/AssetsIconImg/images/brands/adidas.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                column: "Logo",
                value: "/AssetsIconImg/images/brands/puma.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 10,
                column: "Logo",
                value: "/AssetsIconImg/images/brands/zara.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 11,
                column: "Logo",
                value: "/AssetsIconImg/images/brands/hm.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 12,
                column: "Logo",
                value: "/AssetsIconImg/images/brands/dell.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 13,
                column: "Logo",
                value: "/AssetsIconImg/images/brands/hp.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 14,
                column: "Logo",
                value: "/AssetsIconImg/images/brands/lenovo.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 15,
                column: "Logo",
                value: "/AssetsIconImg/images/brands/asus.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 16,
                column: "Logo",
                value: "/AssetsIconImg/images/brands/acer.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 17,
                column: "Logo",
                value: "/AssetsIconImg/images/brands/microsoft.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 18,
                column: "Logo",
                value: "/AssetsIconImg/images/brands/canon.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 19,
                column: "Logo",
                value: "/AssetsIconImg/images/brands/nikon.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 20,
                column: "Logo",
                value: "/AssetsIconImg/images/brands/panasonic.png");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/TopLevelCategory/icon-laptops-and-computers.svg", "/AssetsIconImg/TopLevelCategory/icon-laptops-and-computers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/TopLevelCategory/icon-smartphones-tv-electronics.svg", "/AssetsIconImg/TopLevelCategory/icon-smartphones-tv-electronics.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/TopLevelCategory/icon-gaming.svg", "/AssetsIconImg/TopLevelCategory/icon-gaming.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/TopLevelCategory/icon-home-appliances.svg", "/AssetsIconImg/TopLevelCategory/icon-home-appliances.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/TopLevelCategory/icon-home-goods.svg", "/AssetsIconImg/TopLevelCategory/icon-home-goods.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/TopLevelCategory/icon-tools-auto.svg", "/AssetsIconImg/TopLevelCategory/icon-tools-auto.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/TopLevelCategory/icon-plumbing-renovation.svg", "/AssetsIconImg/TopLevelCategory/icon-plumbing-renovation.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/TopLevelCategory/icon-garden.svg", "/AssetsIconImg/TopLevelCategory/icon-garden.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/TopLevelCategory/icon-sports-hobbies.svg", "/AssetsIconImg/TopLevelCategory/icon-sports-hobbies.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/TopLevelCategory/icon-clothing-footwear-jewelry.svg", "/AssetsIconImg/TopLevelCategory/icon-clothing-footwear-jewelry.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/TopLevelCategory/icon-beauty-health.svg", "/AssetsIconImg/TopLevelCategory/icon-beauty-health.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/TopLevelCategory/icon-baby-products.svg", "/AssetsIconImg/TopLevelCategory/icon-baby-products.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/TopLevelCategory/icon-pet-supplies.svg", "/AssetsIconImg/TopLevelCategory/icon-pet-supplies.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/TopLevelCategory/icon-stationery-books.svg", "/AssetsIconImg/TopLevelCategory/icon-stationery-books.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/TopLevelCategory/icon-alcohol-food.svg", "/AssetsIconImg/TopLevelCategory/icon-alcohol-food.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/TopLevelCategory/icon-business-services.svg", "/AssetsIconImg/TopLevelCategory/icon-business-services.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/TopLevelCategory/icon-tourism-outdoor.svg", "/AssetsIconImg/TopLevelCategory/icon-tourism-outdoor.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/TopLevelCategory/icon-promotions.svg", "/AssetsIconImg/TopLevelCategory/icon-promotions.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/TopLevelCategory/icon-total-sale.svg", "/AssetsIconImg/TopLevelCategory/icon-total-sale.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Notebooks.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Notebooks.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-GamingNotebooks.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-GamingNotebooks.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Ultrabooks.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Ultrabooks.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-ForStudy.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-ForStudy.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-ForWork.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-ForWork.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Chromebook.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Chromebook.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Computers.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Computers.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-DesktopPC.webp", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-DesktopPC.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-GamingPC.webp", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-GamingPC.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-MiniPC.jpg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-MiniPC.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Monoblocks.webp", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Monoblocks.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Workstations.jpg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Workstations.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Components.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Components.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Processors.webp", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Processors.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-VideoCards.webp", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-VideoCards.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Motherboards.webp", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Motherboards.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-RAM.webp", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-RAM.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-PowerSupplies.webp", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-PowerSupplies.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Cases.webp", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Cases.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Storage.webp", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Storage.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 121,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-SSD.webp", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-SSD.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 122,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-HDD.webp", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-HDD.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 123,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-ExternalDrives.jfif", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-ExternalDrives.jfif" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 124,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-NAS.webp", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-NAS.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 125,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Peripherals.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Peripherals.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 126,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Keyboards.svg", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Keyboards.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 127,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Mice.webp", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Mice.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 128,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-MousePads.webp", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-MousePads.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 129,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Webcams.webp", "/AssetsIconImg/SecondLevelCategories/LaptopsAndComputers/categoryIcon-Webcams.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 201,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Smartphones.svg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Smartphones.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 202,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Android.svg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Android.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 203,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-iPhone.svg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-iPhone.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 204,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Budget.webp", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Budget.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 205,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Flagship.webp", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Flagship.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 206,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-TVs.jpg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-TVs.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 207,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-SmartTV.webp", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-SmartTV.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 208,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-LED.webp", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-LED.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 209,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-OLED.webp", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-OLED.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 210,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-QLED.webp", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-QLED.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 211,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Audio.jpg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Audio.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 212,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Headphones.png", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Headphones.png" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 213,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Soundbars.webp", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Soundbars.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 214,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Speakers.webp", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Speakers.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 215,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-HomeTheaters.jpg", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-HomeTheaters.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 216,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Tablets.webp", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Tablets.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 217,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-iPad.webp", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-iPad.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 218,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-AndroidTablets.webp", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-AndroidTablets.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 219,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Gadgets.webp", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-Gadgets.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 220,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-SmartWatches.webp", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-SmartWatches.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 221,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-FitnessBands.webp", "/AssetsIconImg/SecondLevelCategories/SmartphonesTvElectronics/categoryIcon-FitnessBands.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 301,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-Consoles.jpg", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-Consoles.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 302,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-PlayStation.webp", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-PlayStation.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 303,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-Xbox.webp", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-Xbox.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 304,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-Nintendo.webp", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-Nintendo.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 305,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-Games.webp", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-Games.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 306,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-PlayStationGames.webp", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-PlayStationGames.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 307,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-XboxGames.webp", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-XboxGames.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 308,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-PCGames.webp", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-PCGames.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 309,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-GamingPeripherals.jpg", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-GamingPeripherals.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 310,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-GamingMice.webp", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-GamingMice.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 311,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-GamingKeyboards.webp", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-GamingKeyboards.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 312,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-GamingHeadphones.webp", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-GamingHeadphones.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 313,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-VR.jpg", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-VR.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 314,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-VRHeadsets.webp", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-VRHeadsets.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 315,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-VRAccessories.webp", "/AssetsIconImg/SecondLevelCategories/Gaming/categoryIcon-VRAccessories.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 401,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-LargeAppliances.webp", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-LargeAppliances.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 402,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Refrigerators.webp", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Refrigerators.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 403,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-WashingMachines.webp", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-WashingMachines.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 404,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Dishwashers.webp", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Dishwashers.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 405,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-KitchenAppliances.webp", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-KitchenAppliances.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 406,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Microwaves.webp", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Microwaves.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 407,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Blenders.webp", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Blenders.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 408,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Mixers.webp", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Mixers.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 409,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Multicookers.webp", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Multicookers.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 410,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-ClimateControl.webp", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-ClimateControl.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 411,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-AirConditioners.webp", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-AirConditioners.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 412,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Heaters.webp", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Heaters.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 413,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Fans.webp", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Fans.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 414,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Cleaning.webp", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-Cleaning.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 415,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-VacuumCleaners.webp", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-VacuumCleaners.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 416,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-RobotVacuums.webp", "/AssetsIconImg/SecondLevelCategories/HomeAppliances/categoryIcon-RobotVacuums.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 501,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Furniture.jpg", "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Furniture.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 502,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Sofas.webp", "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Sofas.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 503,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Beds.webp", "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Beds.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 504,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Wardrobes.webp", "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Wardrobes.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 505,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Lighting.jpg", "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Lighting.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 506,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Lamps.webp", "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Lamps.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 507,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Chandeliers.webp", "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Chandeliers.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 508,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-LEDLighting.webp", "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-LEDLighting.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 509,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Decor.jpg", "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Decor.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 510,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Paintings.webp", "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Paintings.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 511,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Mirrors.webp", "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Mirrors.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 512,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Clocks.webp", "/AssetsIconImg/SecondLevelCategories/HomeGoods/categoryIcon-Clocks.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 601,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-PowerTools.jpg", "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-PowerTools.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 602,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-Drills.webp", "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-Drills.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 603,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-Screwdrivers.webp", "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-Screwdrivers.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 604,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-AngleGrinders.webp", "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-AngleGrinders.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 605,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-CarElectronics.webp", "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-CarElectronics.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 606,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-DashCams.webp", "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-DashCams.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 607,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-GPS.webp", "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-GPS.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 608,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-CarAccessories.jpg", "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-CarAccessories.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 609,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-PhoneHolders.webp", "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-PhoneHolders.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 610,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-CarChargers.webp", "/AssetsIconImg/SecondLevelCategories/ToolsAndAuto/categoryIcon-CarChargers.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 701,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Bathroom.webp", "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Bathroom.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 702,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-ShowerCubicles.webp", "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-ShowerCubicles.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 703,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Toilets.webp", "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Toilets.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 704,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Sinks.webp", "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Sinks.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 705,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Tools.webp", "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Tools.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 706,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-HandTools.jpg", "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-HandTools.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 707,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-MeasuringTools.jpg", "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-MeasuringTools.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 708,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Materials.webp", "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Materials.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 709,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Paint.webp", "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Paint.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 710,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Tiles.webp", "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Tiles.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 711,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Laminate.webp", "/AssetsIconImg/SecondLevelCategories/PlumbingRenovation/categoryIcon-Laminate.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 801,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-GardenTools.jpg", "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-GardenTools.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 802,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-LawnMowers.webp", "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-LawnMowers.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 803,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-Trimmers.webp", "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-Trimmers.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 804,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-GardenImplements.webp", "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-GardenImplements.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 805,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-Shovels.webp", "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-Shovels.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 806,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-Pruners.webp", "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-Pruners.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 807,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-GardenFurniture.jpg", "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-GardenFurniture.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 808,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-GardenTables.webp", "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-GardenTables.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 809,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-GardenChairs.webp", "/AssetsIconImg/SecondLevelCategories/Garden/categoryIcon-GardenChairs.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 901,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-Fitness.webp", "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-Fitness.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 902,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-Dumbbells.webp", "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-Dumbbells.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 903,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-Treadmills.webp", "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-Treadmills.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 904,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-Cycling.webp", "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-Cycling.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 905,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-Bicycles.webp", "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-Bicycles.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 906,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-CyclingAccessories.webp", "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-CyclingAccessories.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 907,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-OutdoorRecreation.webp", "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-OutdoorRecreation.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 908,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-KickScooters.webp", "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-KickScooters.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 909,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-ElectricScooters.webp", "/AssetsIconImg/SecondLevelCategories/SportsHobbies/categoryIcon-ElectricScooters.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1001,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-MensClothing.jpg", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-MensClothing.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1002,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-TShirts.jpg", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-TShirts.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1003,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Jeans.webp", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Jeans.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1004,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Jackets.webp", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Jackets.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1005,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-WomensClothing.jpg", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-WomensClothing.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1006,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Dresses.webp", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Dresses.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1007,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Skirts.webp", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Skirts.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1008,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Footwear.jpg", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Footwear.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1009,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Sneakers.webp", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Sneakers.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1010,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Boots.webp", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Boots.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1011,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Accessories.webp", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Accessories.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1012,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Bags.webp", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Bags.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1013,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Belts.webp", "/AssetsIconImg/SecondLevelCategories/ClothingFootwearJewelry/categoryIcon-Belts.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1101,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-FaceCare.png", "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-FaceCare.png" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1102,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-Creams.png", "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-Creams.png" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1103,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-Serums.png", "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-Serums.png" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1104,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-HairCare.png", "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-HairCare.png" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1105,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-Shampoos.png", "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-Shampoos.png" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1106,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-HairMasks.png", "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-HairMasks.png" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1107,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-BeautyTech.jpg", "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-BeautyTech.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1108,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-HairDryers.jpg", "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-HairDryers.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1109,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-Razors.jpg", "/AssetsIconImg/SecondLevelCategories/BeautyHealth/categoryIcon-Razors.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1201,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Toys.png", "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Toys.png" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1202,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-ConstructionToys.png", "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-ConstructionToys.png" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1203,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Dolls.jpg", "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Dolls.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1204,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Cars.jpg", "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Cars.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1205,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Baby.jpg", "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Baby.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1206,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Diapers.png", "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Diapers.png" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1207,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-BabyBottles.webp", "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-BabyBottles.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1208,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-KidsVehicles.jpg", "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-KidsVehicles.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1209,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Strollers.webp", "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-Strollers.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1210,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-KidsScooters.webp", "/AssetsIconImg/SecondLevelCategories/BabyProducts/categoryIcon-KidsScooters.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1301,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-Dogs.webp", "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-Dogs.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1302,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-DogFood.webp", "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-DogFood.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1303,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-DogToys.png", "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-DogToys.png" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1304,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-Cats.webp", "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-Cats.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1305,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-CatFood.webp", "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-CatFood.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1306,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-CatLitter.webp", "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-CatLitter.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1307,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-Rodents.webp", "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-Rodents.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1308,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-Cages.jpg", "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-Cages.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1309,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-RodentFood.jpg", "/AssetsIconImg/SecondLevelCategories/PetSupplies/categoryIcon-RodentFood.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1401,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Stationery.jpg", "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Stationery.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1402,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Pens.jpg", "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Pens.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1403,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Notebooks.jpg", "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Notebooks.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1404,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Paper.jpg", "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Paper.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1405,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Books.jpeg", "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Books.jpeg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1406,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Fiction.jpg", "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Fiction.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1407,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Educational.jpg", "/AssetsIconImg/SecondLevelCategories/StationeryBooks/categoryIcon-Educational.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1501,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Alcohol.webp", "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Alcohol.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1502,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Wine.webp", "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Wine.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1503,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Beer.webp", "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Beer.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1504,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Whiskey.webp", "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Whiskey.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1505,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Food.webp", "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Food.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1506,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Sweets.webp", "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Sweets.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1507,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Snacks.jpg", "/AssetsIconImg/SecondLevelCategories/AlcoholFood/categoryIcon-Snacks.jpg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1601,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-Office.jpeg", "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-Office.jpeg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1602,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-OfficeEquipment.png", "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-OfficeEquipment.png" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1603,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-OfficeFurniture.webp", "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-OfficeFurniture.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1604,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-BusinessEquipment.webp", "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-BusinessEquipment.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1605,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-POS.webp", "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-POS.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1606,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-CashRegisters.webp", "/AssetsIconImg/SecondLevelCategories/BusinessService/categoryIcon-CashRegisters.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1701,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-TourismGear.webp", "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-TourismGear.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1702,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-Tents.webp", "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-Tents.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1703,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-SleepingBags.webp", "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-SleepingBags.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1704,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-Travel.webp", "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-Travel.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1705,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-Suitcases.webp", "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-Suitcases.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1706,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-Backpacks.webp", "/AssetsIconImg/SecondLevelCategories/TourismOutdoor/categoryIcon-Backpacks.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1801,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Promotions/categoryIcon-Discounted.webp", "/AssetsIconImg/SecondLevelCategories/Promotions/categoryIcon-Discounted.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1802,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/Promotions/categoryIcon-SeasonalSales.webp", "/AssetsIconImg/SecondLevelCategories/Promotions/categoryIcon-SeasonalSales.webp" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1901,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/TotalSale/categoryIcon-UpTo50.svg", "/AssetsIconImg/SecondLevelCategories/TotalSale/categoryIcon-UpTo50.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1902,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/TotalSale/categoryIcon-UpTo70.svg", "/AssetsIconImg/SecondLevelCategories/TotalSale/categoryIcon-UpTo70.svg" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1903,
                columns: new[] { "IconUrl", "ImgUrl" },
                values: new object[] { "/AssetsIconImg/SecondLevelCategories/TotalSale/categoryIcon-LastItems.svg", "/AssetsIconImg/SecondLevelCategories/TotalSale/categoryIcon-LastItems.svg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                column: "Logo",
                value: "/images/brands/apple.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                column: "Logo",
                value: "/images/brands/samsung.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                column: "Logo",
                value: "/images/brands/xiaomi.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                column: "Logo",
                value: "/images/brands/sony.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                column: "Logo",
                value: "/images/brands/lg.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                column: "Logo",
                value: "/images/brands/bosch.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                column: "Logo",
                value: "/images/brands/nike.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                column: "Logo",
                value: "/images/brands/adidas.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                column: "Logo",
                value: "/images/brands/puma.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 10,
                column: "Logo",
                value: "/images/brands/zara.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 11,
                column: "Logo",
                value: "/images/brands/hm.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 12,
                column: "Logo",
                value: "/images/brands/dell.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 13,
                column: "Logo",
                value: "/images/brands/hp.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 14,
                column: "Logo",
                value: "/images/brands/lenovo.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 15,
                column: "Logo",
                value: "/images/brands/asus.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 16,
                column: "Logo",
                value: "/images/brands/acer.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 17,
                column: "Logo",
                value: "/images/brands/microsoft.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 18,
                column: "Logo",
                value: "/images/brands/canon.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 19,
                column: "Logo",
                value: "/images/brands/nikon.png");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 20,
                column: "Logo",
                value: "/images/brands/panasonic.png");

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
