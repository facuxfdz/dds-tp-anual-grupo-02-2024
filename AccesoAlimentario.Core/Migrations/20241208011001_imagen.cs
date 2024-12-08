using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoAlimentario.Core.Migrations
{
    /// <inheritdoc />
    public partial class imagen : Migration
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

            migrationBuilder.CreateTable(
                name: "ImagenCid",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Cid = table.Column<string>(type: "longtext", nullable: false),
                    Imagen = table.Column<string>(type: "longtext", nullable: false),
                    NotificacionId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagenCid", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagenCid_Notificaciones_NotificacionId",
                        column: x => x.NotificacionId,
                        principalTable: "Notificaciones",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ImagenCid_NotificacionId",
                table: "ImagenCid",
                column: "NotificacionId");

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

            migrationBuilder.DropTable(
                name: "ImagenCid");

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
