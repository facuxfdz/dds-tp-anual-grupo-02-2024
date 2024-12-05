using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoAlimentario.Core.Migrations
{
    /// <inheritdoc />
    public partial class ColaboradorOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarjetas_Colaboradores_ResponsableId",
                table: "Tarjetas");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarjetas_Colaboradores_ResponsableId",
                table: "Tarjetas",
                column: "ResponsableId",
                principalTable: "Colaboradores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarjetas_Colaboradores_ResponsableId",
                table: "Tarjetas");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarjetas_Colaboradores_ResponsableId",
                table: "Tarjetas",
                column: "ResponsableId",
                principalTable: "Colaboradores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
