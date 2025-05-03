using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data_security.Migrations
{
    /// <inheritdoc />
    public partial class InitialFixedSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 19, 14, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 19, 14, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 19, 14, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 19, 14, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 19, 14, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 19, 14, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 19, 14, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 19, 14, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 19, 14, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 26, 4, 24, 14, 609, DateTimeKind.Local).AddTicks(5445));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 26, 4, 24, 14, 609, DateTimeKind.Local).AddTicks(5611));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 26, 4, 24, 14, 609, DateTimeKind.Local).AddTicks(5635));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 26, 4, 24, 14, 609, DateTimeKind.Local).AddTicks(5654));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 26, 4, 24, 14, 609, DateTimeKind.Local).AddTicks(5685));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 26, 4, 24, 14, 609, DateTimeKind.Local).AddTicks(5703));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 26, 4, 24, 14, 609, DateTimeKind.Local).AddTicks(5720));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 26, 4, 24, 14, 609, DateTimeKind.Local).AddTicks(5736));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 26, 4, 24, 14, 609, DateTimeKind.Local).AddTicks(5755));
        }
    }
}
