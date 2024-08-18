using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datos.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class FourthMIgration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_ApplicationUser_usuarioId",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Productos");

            migrationBuilder.RenameColumn(
                name: "usuarioId",
                table: "Compras",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Compras_usuarioId",
                table: "Compras",
                newName: "IX_Compras_UsuarioId");

            migrationBuilder.CreateTable(
                name: "Fotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fotos", x => x.Id);
                    table.ForeignKey(
                        name: "fk_Producto_Foto",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fotos_IdProducto",
                table: "Fotos",
                column: "IdProducto");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_ApplicationUser_UsuarioId",
                table: "Compras",
                column: "UsuarioId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_ApplicationUser_UsuarioId",
                table: "Compras");

            migrationBuilder.DropTable(
                name: "Fotos");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Compras",
                newName: "usuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Compras_UsuarioId",
                table: "Compras",
                newName: "IX_Compras_usuarioId");

            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Productos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_ApplicationUser_usuarioId",
                table: "Compras",
                column: "usuarioId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");
        }
    }
}
