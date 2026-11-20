using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VentasApi.Migrations
{
    /// <inheritdoc />
    public partial class RestringirBorradoCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productos_categorias_CategoriaId",
                table: "productos");

            migrationBuilder.AddForeignKey(
                name: "FK_productos_categorias_CategoriaId",
                table: "productos",
                column: "CategoriaId",
                principalTable: "categorias",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productos_categorias_CategoriaId",
                table: "productos");

            migrationBuilder.AddForeignKey(
                name: "FK_productos_categorias_CategoriaId",
                table: "productos",
                column: "CategoriaId",
                principalTable: "categorias",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
