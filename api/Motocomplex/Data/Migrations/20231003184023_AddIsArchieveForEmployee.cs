using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Motocomplex.Migrations
{
    /// <inheritdoc />
    public partial class AddIsArchieveForEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchive",
                table: "Employee",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchive",
                table: "Employee");
        }
    }
}