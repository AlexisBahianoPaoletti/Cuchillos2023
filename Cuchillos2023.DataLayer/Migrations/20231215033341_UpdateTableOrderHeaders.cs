using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cuchillos2023.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableOrderHeaders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "OrderHeaders",
                newName: "Provincia");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "OrderHeaders",
                newName: "Pais");

            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "OrderHeaders");

            migrationBuilder.RenameColumn(
                name: "Provincia",
                table: "OrderHeaders",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "Pais",
                table: "OrderHeaders",
                newName: "City");
        }
    }
}
