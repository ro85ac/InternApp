using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intern.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InsertEmployees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"INSERT INTO Employees (Id, FirstName, LastName) VALUES ('{Guid.NewGuid()}', 'Adrian', 'Constantinache')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
