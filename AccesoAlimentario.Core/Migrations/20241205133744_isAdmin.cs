using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoAlimentario.Core.Migrations
{
    /// <inheritdoc />
    public partial class isAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sensores_Heladeras_HeladeraId",
                table: "Sensores");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "UsuariosSistema",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "HeladeraId",
                table: "Sensores",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sensores_Heladeras_HeladeraId",
                table: "Sensores",
                column: "HeladeraId",
                principalTable: "Heladeras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sensores_Heladeras_HeladeraId",
                table: "Sensores");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "UsuariosSistema");

            migrationBuilder.AlterColumn<Guid>(
                name: "HeladeraId",
                table: "Sensores",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensores_Heladeras_HeladeraId",
                table: "Sensores",
                column: "HeladeraId",
                principalTable: "Heladeras",
                principalColumn: "Id");
        }
    }
}
