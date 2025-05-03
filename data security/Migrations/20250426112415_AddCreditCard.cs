using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace data_security.Migrations
{
    /// <inheritdoc />
    public partial class AddCreditCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValidDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CVC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditCards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CreditCards",
                columns: new[] { "Id", "CVC", "CardNumber", "FirstName", "IdNumber", "LastName", "UserId", "ValidDate" },
                values: new object[] { 1, "123", "1234 5567 8901 2345", "Israeli", "123456789", "Israeili", 1, "12/32" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "CreatedAt", "Email", "IsAdmin", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { 2, new DateTime(2025, 4, 26, 4, 24, 14, 609, DateTimeKind.Local).AddTicks(5445), "user1@example.com", false, "bc5848f227cc161eb5f68dfe98cb13110a9c843ce69e953a88107d865583d397", "user1" },
                    { 3, new DateTime(2025, 4, 26, 4, 24, 14, 609, DateTimeKind.Local).AddTicks(5611), "user2@example.com", false, "bc5848f227cc161eb5f68dfe98cb13110a9c843ce69e953a88107d865583d397", "user2" },
                    { 4, new DateTime(2025, 4, 26, 4, 24, 14, 609, DateTimeKind.Local).AddTicks(5635), "user3@example.com", false, "bc5848f227cc161eb5f68dfe98cb13110a9c843ce69e953a88107d865583d397", "user3" },
                    { 5, new DateTime(2025, 4, 26, 4, 24, 14, 609, DateTimeKind.Local).AddTicks(5654), "user4@example.com", false, "bc5848f227cc161eb5f68dfe98cb13110a9c843ce69e953a88107d865583d397", "user4" },
                    { 6, new DateTime(2025, 4, 26, 4, 24, 14, 609, DateTimeKind.Local).AddTicks(5685), "user5@example.com", false, "bc5848f227cc161eb5f68dfe98cb13110a9c843ce69e953a88107d865583d397", "user5" },
                    { 7, new DateTime(2025, 4, 26, 4, 24, 14, 609, DateTimeKind.Local).AddTicks(5703), "user6@example.com", false, "bc5848f227cc161eb5f68dfe98cb13110a9c843ce69e953a88107d865583d397", "user6" },
                    { 8, new DateTime(2025, 4, 26, 4, 24, 14, 609, DateTimeKind.Local).AddTicks(5720), "user7@example.com", false, "bc5848f227cc161eb5f68dfe98cb13110a9c843ce69e953a88107d865583d397", "user7" },
                    { 9, new DateTime(2025, 4, 26, 4, 24, 14, 609, DateTimeKind.Local).AddTicks(5736), "user8@example.com", false, "bc5848f227cc161eb5f68dfe98cb13110a9c843ce69e953a88107d865583d397", "user8" },
                    { 10, new DateTime(2025, 4, 26, 4, 24, 14, 609, DateTimeKind.Local).AddTicks(5755), "user9@example.com", false, "bc5848f227cc161eb5f68dfe98cb13110a9c843ce69e953a88107d865583d397", "user9" }
                });

            migrationBuilder.InsertData(
                table: "CreditCards",
                columns: new[] { "Id", "CVC", "CardNumber", "FirstName", "IdNumber", "LastName", "UserId", "ValidDate" },
                values: new object[,]
                {
                    { 2, "102", "4000 1002 5678 9002", "User1", "100000002", "Last1", 2, "10/30" },
                    { 3, "103", "4000 1003 5678 9003", "User2", "100000003", "Last2", 3, "10/30" },
                    { 4, "104", "4000 1004 5678 9004", "User3", "100000004", "Last3", 4, "10/30" },
                    { 5, "105", "4000 1005 5678 9005", "User4", "100000005", "Last4", 5, "10/30" },
                    { 6, "106", "4000 1006 5678 9006", "User5", "100000006", "Last5", 6, "10/30" },
                    { 7, "107", "4000 1007 5678 9007", "User6", "100000007", "Last6", 7, "10/30" },
                    { 8, "108", "4000 1008 5678 9008", "User7", "100000008", "Last7", 8, "10/30" },
                    { 9, "109", "4000 1009 5678 9009", "User8", "100000009", "Last8", 9, "10/30" },
                    { 10, "110", "4000 1010 5678 9010", "User9", "100000010", "Last9", 10, "10/30" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_UserId",
                table: "CreditCards",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 10);
        }
    }
}
