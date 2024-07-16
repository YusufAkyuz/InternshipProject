using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Department = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Salary = table.Column<string>(type: "text", nullable: false),
                    DateOfEntry = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    RoleOfEmp = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfEntry", "Department", "Email", "IsDeleted", "LastName", "Name", "PhoneNumber", "RoleOfEmp", "Salary" },
                values: new object[] { new Guid("05625c09-4c13-42ca-a0d7-ab2d3465a65b"), new DateTime(2024, 7, 16, 13, 29, 53, 108, DateTimeKind.Utc).AddTicks(6510), "Back End Development", "yusufakyus47@gmail.com", false, "Akyüz", "Yusuf", "05415125099", 1, "40000" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
