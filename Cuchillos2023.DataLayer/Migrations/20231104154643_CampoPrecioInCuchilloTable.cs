﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cuchillos2023.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class CampoPrecioInCuchilloTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Precio",
                table: "Cuchillos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Cuchillos");
        }
    }
}
