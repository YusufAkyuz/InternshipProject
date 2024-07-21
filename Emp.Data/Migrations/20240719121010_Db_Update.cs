using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Db_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00cefd26-89cb-4c1c-9fce-ddf1ff5a727c"),
                column: "DateOfEntry",
                value: new DateTime(2024, 7, 19, 12, 10, 10, 828, DateTimeKind.Utc).AddTicks(5920));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("05625c09-4c13-42ca-a0d7-ab2d3465a65b"),
                column: "DateOfEntry",
                value: new DateTime(2024, 7, 19, 12, 10, 10, 828, DateTimeKind.Utc).AddTicks(5920));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00cefd26-89cb-4c1c-9fce-ddf1ff5a727c"),
                columns: new[] { "DateOfEntry", "IsDeleted" },
                values: new object[] { new DateTime(2024, 7, 17, 13, 39, 56, 384, DateTimeKind.Utc).AddTicks(9480), false });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("05625c09-4c13-42ca-a0d7-ab2d3465a65b"),
                columns: new[] { "DateOfEntry", "IsDeleted" },
                values: new object[] { new DateTime(2024, 7, 17, 13, 39, 56, 384, DateTimeKind.Utc).AddTicks(9480), false });
        }
    }
}
