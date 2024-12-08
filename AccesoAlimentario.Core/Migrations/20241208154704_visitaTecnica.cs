using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoAlimentario.Core.Migrations
{
    /// <inheritdoc />
    public partial class visitaTecnica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddForeignKey(
                name: "FK_VisitasTecnicas_Tecnicos_TecnicoId",
                table: "VisitasTecnicas",
                column: "TecnicoId",
                principalTable: "Tecnicos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddForeignKey(
                name: "FK_VisitasTecnicas_Tecnicos_TecnicoId",
                table: "VisitasTecnicas",
                column: "TecnicoId",
                principalTable: "Tecnicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
