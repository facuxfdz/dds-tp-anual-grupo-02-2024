using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoAlimentario.Core.Migrations
{
    /// <inheritdoc />
    public partial class fallaTecnica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidentes_Colaboradores_ColaboradorId",
                table: "Incidentes");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidentes_Colaboradores_ColaboradorId",
                table: "Incidentes",
                column: "ColaboradorId",
                principalTable: "Colaboradores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidentes_Colaboradores_ColaboradorId",
                table: "Incidentes");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidentes_Colaboradores_ColaboradorId",
                table: "Incidentes",
                column: "ColaboradorId",
                principalTable: "Colaboradores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
