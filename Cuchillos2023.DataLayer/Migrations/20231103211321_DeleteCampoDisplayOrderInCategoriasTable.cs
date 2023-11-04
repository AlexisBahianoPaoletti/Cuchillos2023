using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cuchillos2023.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCampoDisplayOrderInCategoriasTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Categorias");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Categorias",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
