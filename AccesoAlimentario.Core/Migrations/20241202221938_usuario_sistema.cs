using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoAlimentario.Core.Migrations
{
    /// <inheritdoc />
    public partial class usuario_sistema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "UsuariosSistema",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "RegisterType",
                table: "UsuariosSistema",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "UsuariosSistema");

            migrationBuilder.DropColumn(
                name: "RegisterType",
                table: "UsuariosSistema");
        }
    }
}
