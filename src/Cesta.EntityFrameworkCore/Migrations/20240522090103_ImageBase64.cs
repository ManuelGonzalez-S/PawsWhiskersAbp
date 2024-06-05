using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cesta.Migrations
{
    /// <inheritdoc />
    public partial class ImageBase64 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NombreImagen",
                table: "Productos",
                newName: "ImageBase64");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageBase64",
                table: "Productos",
                newName: "NombreImagen");
        }
    }
}
