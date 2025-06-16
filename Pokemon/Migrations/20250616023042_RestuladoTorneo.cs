using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pokemon.Migrations
{
    /// <inheritdoc />
    public partial class RestuladoTorneo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Torneos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EstaActivo",
                table: "Torneos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFin",
                table: "Torneos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Orden",
                table: "Torneos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ResultadoTorneo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TorneoId = table.Column<int>(type: "INTEGER", nullable: false),
                    JugadorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Posicion = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultadoTorneo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResultadoTorneo_Torneos_TorneoId",
                        column: x => x.TorneoId,
                        principalTable: "Torneos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultadoTorneo_Users_JugadorId",
                        column: x => x.JugadorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResultadoTorneo_JugadorId",
                table: "ResultadoTorneo",
                column: "JugadorId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadoTorneo_TorneoId",
                table: "ResultadoTorneo",
                column: "TorneoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResultadoTorneo");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Torneos");

            migrationBuilder.DropColumn(
                name: "EstaActivo",
                table: "Torneos");

            migrationBuilder.DropColumn(
                name: "FechaFin",
                table: "Torneos");

            migrationBuilder.DropColumn(
                name: "Orden",
                table: "Torneos");
        }
    }
}
