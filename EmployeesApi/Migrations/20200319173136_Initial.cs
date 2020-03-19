using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeesApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 200, nullable: true),
                    LastName = table.Column<string>(maxLength: 200, nullable: true),
                    Department = table.Column<string>(maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "FirstName", "IsActive", "LastName" },
                values: new object[] { 1, "CEO", "Sue", true, "Jones" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "FirstName", "IsActive", "LastName" },
                values: new object[] { 2, "DEV", "Bob", true, "Smith" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "FirstName", "IsActive", "LastName" },
                values: new object[] { 3, "QA", "Sean", false, "Carlin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
