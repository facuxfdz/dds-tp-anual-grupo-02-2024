using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoAlimentario.Core.Migrations
{
    /// <inheritdoc />
    public partial class viandaSinColaborador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Viandas_Colaboradores_ColaboradorId",
                table: "Viandas");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitasTecnicas_Tecnicos_TecnicoId",
                table: "VisitasTecnicas");

            migrationBuilder.AlterColumn<Guid>(
                name: "TecnicoId",
                table: "VisitasTecnicas",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ColaboradorId",
                table: "Viandas",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AddForeignKey(
                name: "FK_Viandas_Colaboradores_ColaboradorId",
                table: "Viandas",
                column: "ColaboradorId",
                principalTable: "Colaboradores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitasTecnicas_Tecnicos_TecnicoId",
                table: "VisitasTecnicas",
                column: "TecnicoId",
                principalTable: "Tecnicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Viandas_Colaboradores_ColaboradorId",
                table: "Viandas");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitasTecnicas_Tecnicos_TecnicoId",
                table: "VisitasTecnicas");

            migrationBuilder.AlterColumn<Guid>(
                name: "TecnicoId",
                table: "VisitasTecnicas",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<Guid>(
                name: "ColaboradorId",
                table: "Viandas",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Viandas_Colaboradores_ColaboradorId",
                table: "Viandas",
                column: "ColaboradorId",
                principalTable: "Colaboradores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitasTecnicas_Tecnicos_TecnicoId",
                table: "VisitasTecnicas",
                column: "TecnicoId",
                principalTable: "Tecnicos",
                principalColumn: "Id");
        }
    }
}
