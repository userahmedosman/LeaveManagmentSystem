using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagmentSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedLeaveRequestAndLeaveRequestStatus_Models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveRequestStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequestStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    LeaveRequestStatusId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReviewerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RequestComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEmergency = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_AspNetUsers_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeaveRequests_LeaveRequestStatuses_LeaveRequestStatusId",
                        column: x => x.LeaveRequestStatusId,
                        principalTable: "LeaveRequestStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3C0BDB6C-3412-4E27-97AE-909F54967281",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b2b18b3-d9d6-48d3-96f7-9456bc5090f1", "AQAAAAIAAYagAAAAEK+lPHQVk0dTw3c9uFTuIS/8BzuB/Y2aXgUnlsMZtPmeXxMq9pCMGstEI85eWS1B8g==", "fc76dff3-155c-40c0-99fe-29d3b47f3468" });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_EmployeeId",
                table: "LeaveRequests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_LeaveRequestStatusId",
                table: "LeaveRequests",
                column: "LeaveRequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_LeaveTypeId",
                table: "LeaveRequests",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_ReviewerId",
                table: "LeaveRequests",
                column: "ReviewerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveRequests");

            migrationBuilder.DropTable(
                name: "LeaveRequestStatuses");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3C0BDB6C-3412-4E27-97AE-909F54967281",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "50a8a1cc-6f41-4ef8-a3b2-52c8c0863d53", "AQAAAAIAAYagAAAAEMiggIx3DTGognZJzh3jJnSikvoz+p5/4Pw7tYSn8p/Iwa8wH3IxW44HB9vRKJovqg==", "16102907-44b5-4bd5-bdfa-7393719c3d28" });
        }
    }
}
