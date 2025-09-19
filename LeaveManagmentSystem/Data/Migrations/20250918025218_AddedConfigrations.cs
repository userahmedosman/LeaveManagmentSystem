using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveManagmentSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedConfigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3C0BDB6C-3412-4E27-97AE-909F54967281",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b8d2d932-6ed2-4c8a-94b8-80f4b538e81d", "AQAAAAIAAYagAAAAEP3q/DoUx8TmrXzuPCDG51xUzpYPKP8peh3g8yZs0tBSiIjTQe+qgFXZaza3AGJ7QA==", "6e09128d-3067-44e8-90bb-3984ebc81b4e" });

            migrationBuilder.InsertData(
                table: "LeaveRequestStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Pending.." },
                    { 2, "Approved" },
                    { 3, "Declined" },
                    { 4, "Canceled" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LeaveRequestStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LeaveRequestStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LeaveRequestStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LeaveRequestStatuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3C0BDB6C-3412-4E27-97AE-909F54967281",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b2b18b3-d9d6-48d3-96f7-9456bc5090f1", "AQAAAAIAAYagAAAAEK+lPHQVk0dTw3c9uFTuIS/8BzuB/Y2aXgUnlsMZtPmeXxMq9pCMGstEI85eWS1B8g==", "fc76dff3-155c-40c0-99fe-29d3b47f3468" });
        }
    }
}
