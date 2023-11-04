using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cuchillos2023.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class CreateNumerosTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Numeros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numeroo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Numeros", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Numeros_Numeroo",
                table: "Numeros",
                column: "Numeroo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Numeros");
        }
    }
}
