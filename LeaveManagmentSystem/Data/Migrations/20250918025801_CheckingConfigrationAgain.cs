using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagmentSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class CheckingConfigrationAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3C0BDB6C-3412-4E27-97AE-909F54967281",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0065761f-245f-4a83-889e-da39c8206ecd", "AQAAAAIAAYagAAAAEBW4F3peslQUS8duZbv5MpQAo4Rnw+A5LdXomM4eZlHqBRLMIWxVMH0ZC91llpWv+g==", "64196be9-8571-4dc1-898a-54dcc5a146a1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3C0BDB6C-3412-4E27-97AE-909F54967281",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b8d2d932-6ed2-4c8a-94b8-80f4b538e81d", "AQAAAAIAAYagAAAAEP3q/DoUx8TmrXzuPCDG51xUzpYPKP8peh3g8yZs0tBSiIjTQe+qgFXZaza3AGJ7QA==", "6e09128d-3067-44e8-90bb-3984ebc81b4e" });
        }
    }
}
