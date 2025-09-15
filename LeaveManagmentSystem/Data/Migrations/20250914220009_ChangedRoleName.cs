using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagmentSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedRoleName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "BB70A46B-19D5-4872-8911-666B6777CE84",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Supervisor", "SUPERVISOR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3C0BDB6C-3412-4E27-97AE-909F54967281",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2ceb0f0a-3e1c-432f-bc86-72e9369a0188", "AQAAAAIAAYagAAAAENtWU7hFZMinhatAybEQwC6NWUzspqUtAe711dZ5TxU6RXkk6XhxFBfZ4we7SYxEAw==", "1366baf7-710b-41e5-9b99-b06610707313" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "BB70A46B-19D5-4872-8911-666B6777CE84",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3C0BDB6C-3412-4E27-97AE-909F54967281",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "db4a9a4c-46ea-4da7-91a1-97315ba63aa9", "AQAAAAIAAYagAAAAEEdFtbDMSy3Qz6z0QYF5gMfZ3kiiP2zCdoqXmBszGXTOEYwbrcw7HpJWUEH6S650sg==", "1ef71270-4398-4499-b12d-558652541438" });
        }
    }
}
