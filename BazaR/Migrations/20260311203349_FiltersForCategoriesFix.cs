using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BazaR.Migrations
{
    /// <inheritdoc />
    public partial class FiltersForCategoriesFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3134);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3133);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3132);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3131);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3130);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3129);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3128);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3127);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3126);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3125);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3124);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3123);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3122);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3121);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3120);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3119);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3118);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3117);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3116);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3115);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3114);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3113);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3112);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3111);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3110);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3109);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3108);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3107);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3106);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3105);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3104);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3103);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3102);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3101);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3100);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3099);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3098);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3097);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3096);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3095);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3094);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3093);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3092);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3091);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3090);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3089);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3088);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3087);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3086);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3085);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3084);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3083);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3082);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3081);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3080);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3079);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3078);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3077);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3076);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3075);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3074);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3073);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3072);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3071);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3070);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3069);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3068);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3067);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3066);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3065);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3064);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3063);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3062);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3061);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3060);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3059);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3058);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3057);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3056);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3055);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3054);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3053);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3052);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3051);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3050);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3049);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3048);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3047);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3046);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3045);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3044);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3043);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3042);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3041);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3040);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3039);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3038);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3037);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3036);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3035);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3034);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3033);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3032);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3031);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3030);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3029);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3028);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3027);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3026);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3025);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3024);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3023);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3022);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3021);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3020);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3019);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3018);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3017);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3016);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3015);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3014);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3013);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3012);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3011);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3010);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3009);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3008);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3007);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3006);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3005);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3004);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3003);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3002);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3001);

            migrationBuilder.DeleteData(
                table: "ItemCharacteristics",
                keyColumn: "Id",
                keyValue: -3000);
        }
    }
}
