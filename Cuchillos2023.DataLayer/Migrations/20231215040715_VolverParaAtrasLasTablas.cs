using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cuchillos2023.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class VolverParaAtrasLasTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Ciudades_CiudadId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CiudadId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "CiudadId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Provincia",
                table: "OrderHeaders",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "Pais",
                table: "OrderHeaders",
                newName: "City");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "CiudadId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CiudadId",
                table: "AspNetUsers",
                column: "CiudadId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Ciudades_CiudadId",
                table: "AspNetUsers",
                column: "CiudadId",
                principalTable: "Ciudades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
