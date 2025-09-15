using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagmentSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedRoleForSeededUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "BB70A46B-19D5-4872-8911-666B6777CE84", "3C0BDB6C-3412-4E27-97AE-909F54967281" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1C1AA946-E470-4D45-9E1B-55C7BB9C4ECE", "3C0BDB6C-3412-4E27-97AE-909F54967281" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3C0BDB6C-3412-4E27-97AE-909F54967281",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "605efea7-efcc-420c-8339-165ff5be39c8", "AQAAAAIAAYagAAAAEIG6YSnTV7Bz7M0HBVzDmGAdMoZqEBFhHLrHf1vnuBC8LllwGSDLLIdRZdzOj2/URw==", "138f4484-2cad-4ea5-abf9-48f679b3cb75" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1C1AA946-E470-4D45-9E1B-55C7BB9C4ECE", "3C0BDB6C-3412-4E27-97AE-909F54967281" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "BB70A46B-19D5-4872-8911-666B6777CE84", "3C0BDB6C-3412-4E27-97AE-909F54967281" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3C0BDB6C-3412-4E27-97AE-909F54967281",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2ceb0f0a-3e1c-432f-bc86-72e9369a0188", "AQAAAAIAAYagAAAAENtWU7hFZMinhatAybEQwC6NWUzspqUtAe711dZ5TxU6RXkk6XhxFBfZ4we7SYxEAw==", "1366baf7-710b-41e5-9b99-b06610707313" });
        }
    }
}
