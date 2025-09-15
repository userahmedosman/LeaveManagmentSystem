using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveManagmentSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingInitialUserWithRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1C1AA946-E470-4D45-9E1B-55C7BB9C4ECE", null, "Admin", "ADMIN" },
                    { "5B65FA0F-8691-4C82-8241-DA2BF3A1FE0F", null, "Employee", "EMPLOYEE" },
                    { "BB70A46B-19D5-4872-8911-666B6777CE84", null, "SuperAdmin", "SUPERADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3C0BDB6C-3412-4E27-97AE-909F54967281", 0, "51f28907-1adb-4877-97cf-77afd218cf8a", "ahmedhaj000@gmail.com", true, false, null, "AHMEDHAJ000@GMAIL.COM", "AHMEDHAJ000@GMAIL.COM", "AQAAAAIAAYagAAAAECPDKpjfc689Ca4u9Y4oS48SMLaWOjPzc6v9xbD6uP7p5WyIWexGFA5m/xuh4FiOfQ==", null, false, "0e931989-2515-41b9-b06e-93d289a94398", false, "ahmedhaj000@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "BB70A46B-19D5-4872-8911-666B6777CE84", "3C0BDB6C-3412-4E27-97AE-909F54967281" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1C1AA946-E470-4D45-9E1B-55C7BB9C4ECE");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5B65FA0F-8691-4C82-8241-DA2BF3A1FE0F");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "BB70A46B-19D5-4872-8911-666B6777CE84", "3C0BDB6C-3412-4E27-97AE-909F54967281" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "BB70A46B-19D5-4872-8911-666B6777CE84");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3C0BDB6C-3412-4E27-97AE-909F54967281");
        }
    }
}
