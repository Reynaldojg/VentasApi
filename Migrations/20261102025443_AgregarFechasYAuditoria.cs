using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VentasApi.Migrations
{
    /// <inheritdoc />
    public partial class AgregarFechasYAuditoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "productos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "productos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "categorias",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "categorias",
                type: "datetime2",
                nullable: true);

            // Rellenar la fecha de creación de los registros que ya existían
            migrationBuilder.Sql("UPDATE categorias SET FechaCreacion = GETDATE()");
            migrationBuilder.Sql("UPDATE productos SET FechaCreacion = GETDATE()");

            migrationBuilder.CreateTable(
                name: "AuditiEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    OperationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaAccion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditiEntities", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditiEntities");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "productos");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "productos");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "categorias");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "categorias");
        }
    }
}
