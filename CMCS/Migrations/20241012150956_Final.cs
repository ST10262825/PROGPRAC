using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CMCS.Migrations
{
    /// <inheritdoc />
    public partial class Final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "HourlyRate",
                table: "Claims",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "70576cb7-6fb4-47c6-99c5-0595b9de4e29", null, "AcademicManager", "ACADEMICMANAGER" },
                    { "af4ac8a1-5e4c-4577-a13c-795576972d84", null, "ProgrammeCoordinator", "PROGRAMMECOORDINATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8c5bd7f9-f5a2-44d0-9a17-62fd7e1ec615", 0, "cc9de2a4-008b-4fa0-8b8f-ede2a9c47be9", "academicmanager@cmcs.com", true, "Academic Manager", false, null, "ACADEMICMANAGER@CMCS.COM", "ACADEMICMANAGER@CMCS.COM", "AQAAAAIAAYagAAAAENcnyFcx2yaoiuvV/U5gwKAD3IxjB3p7gANmnhTJrEsZQi7Ga/h5goq6nxMIfU1bgw==", null, false, "b13a87c4-eb9d-479a-9c57-5fe160aa750c", false, "academicmanager@cmcs.com" },
                    { "bf900cdf-2a6b-4e0f-9cf2-5400b9f45772", 0, "e6188ba5-111d-4778-8fe9-895c59101b74", "programmecoordinator@cmcs.com", true, "Programme Coordinator", false, null, "PROGRAMMECOORDINATOR@CMCS.COM", "PROGRAMMECOORDINATOR@CMCS.COM", "AQAAAAIAAYagAAAAEKlFX3jf2VCvs+59SoNffKIuuqArxyPqhIArt7QM6vlxf6SxInvJ5jD8QRdf/IoUUg==", null, false, "dbd2d91d-9e79-4b07-8640-55f2a8231e90", false, "programmecoordinator@cmcs.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "70576cb7-6fb4-47c6-99c5-0595b9de4e29", "8c5bd7f9-f5a2-44d0-9a17-62fd7e1ec615" },
                    { "af4ac8a1-5e4c-4577-a13c-795576972d84", "bf900cdf-2a6b-4e0f-9cf2-5400b9f45772" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "70576cb7-6fb4-47c6-99c5-0595b9de4e29", "8c5bd7f9-f5a2-44d0-9a17-62fd7e1ec615" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "af4ac8a1-5e4c-4577-a13c-795576972d84", "bf900cdf-2a6b-4e0f-9cf2-5400b9f45772" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70576cb7-6fb4-47c6-99c5-0595b9de4e29");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af4ac8a1-5e4c-4577-a13c-795576972d84");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8c5bd7f9-f5a2-44d0-9a17-62fd7e1ec615");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bf900cdf-2a6b-4e0f-9cf2-5400b9f45772");

            migrationBuilder.AlterColumn<decimal>(
                name: "HourlyRate",
                table: "Claims",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");
        }
    }
}
