using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cuchillos2023.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class CreateCategoriasTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCategoria = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "DisplayOrder", "NombreCategoria" },
                values: new object[,]
                {
                    { 1, 1, "Filetero" },
                    { 2, 3, "Trinchero" },
                    { 3, 2, "Puntillero" },
                    { 4, 4, "Chuletero" },
                    { 5, 5, "Cocinero" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_NombreCategoria",
                table: "Categorias",
                column: "NombreCategoria",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
