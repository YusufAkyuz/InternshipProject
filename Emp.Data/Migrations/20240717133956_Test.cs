using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("05625c09-4c13-42ca-a0d7-ab2d3465a65b"),
                column: "DateOfEntry",
                value: new DateTime(2024, 7, 17, 13, 39, 56, 384, DateTimeKind.Utc).AddTicks(9480));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfEntry", "Department", "Email", "IsDeleted", "LastName", "Name", "PhoneNumber", "RoleOfEmp", "Salary" },
                values: new object[] { new Guid("00cefd26-89cb-4c1c-9fce-ddf1ff5a727c"), new DateTime(2024, 7, 17, 13, 39, 56, 384, DateTimeKind.Utc).AddTicks(9480), "Back End Development", "yusufakyus47@gmail.com", false, "Akyüz", "Yusuf", "05415125099", 1, "40000" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00cefd26-89cb-4c1c-9fce-ddf1ff5a727c"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("05625c09-4c13-42ca-a0d7-ab2d3465a65b"),
                column: "DateOfEntry",
                value: new DateTime(2024, 7, 16, 13, 29, 53, 108, DateTimeKind.Utc).AddTicks(6510));
        }
    }
}
