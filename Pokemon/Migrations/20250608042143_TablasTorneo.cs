using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pokemon.Migrations
{
    /// <inheritdoc />
    public partial class TablasTorneo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JugadoresTorneos",
                table: "JugadoresTorneos");

            migrationBuilder.RenameColumn(
                name: "Puntaje",
                table: "JugadoresTorneos",
                newName: "Puntos");

            migrationBuilder.AddColumn<int>(
                name: "GanadorId",
                table: "Torneos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "JugadoresTorneos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "PartidosEmpatados",
                table: "JugadoresTorneos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PartidosGanados",
                table: "JugadoresTorneos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PartidosJugados",
                table: "JugadoresTorneos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PartidosPerdidos",
                table: "JugadoresTorneos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JugadoresTorneos",
                table: "JugadoresTorneos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Partidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TorneoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Jugador1Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Jugador2Id = table.Column<int>(type: "INTEGER", nullable: false),
                    PuntosJugador1 = table.Column<int>(type: "INTEGER", nullable: true),
                    PuntosJugador2 = table.Column<int>(type: "INTEGER", nullable: true),
                    GanadorId = table.Column<int>(type: "INTEGER", nullable: true),
                    Fase = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partidos_Torneos_TorneoId",
                        column: x => x.TorneoId,
                        principalTable: "Torneos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Partidos_Users_GanadorId",
                        column: x => x.GanadorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Partidos_Users_Jugador1Id",
                        column: x => x.Jugador1Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Partidos_Users_Jugador2Id",
                        column: x => x.Jugador2Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Torneos_GanadorId",
                table: "Torneos",
                column: "GanadorId");

            migrationBuilder.CreateIndex(
                name: "IX_JugadoresTorneos_UserId_TorneoId",
                table: "JugadoresTorneos",
                columns: new[] { "UserId", "TorneoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_GanadorId",
                table: "Partidos",
                column: "GanadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_Jugador1Id",
                table: "Partidos",
                column: "Jugador1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_Jugador2Id",
                table: "Partidos",
                column: "Jugador2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_TorneoId",
                table: "Partidos",
                column: "TorneoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Torneos_Users_GanadorId",
                table: "Torneos",
                column: "GanadorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Torneos_Users_GanadorId",
                table: "Torneos");

            migrationBuilder.DropTable(
                name: "Partidos");

            migrationBuilder.DropIndex(
                name: "IX_Torneos_GanadorId",
                table: "Torneos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JugadoresTorneos",
                table: "JugadoresTorneos");

            migrationBuilder.DropIndex(
                name: "IX_JugadoresTorneos_UserId_TorneoId",
                table: "JugadoresTorneos");

            migrationBuilder.DropColumn(
                name: "GanadorId",
                table: "Torneos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "JugadoresTorneos");

            migrationBuilder.DropColumn(
                name: "PartidosEmpatados",
                table: "JugadoresTorneos");

            migrationBuilder.DropColumn(
                name: "PartidosGanados",
                table: "JugadoresTorneos");

            migrationBuilder.DropColumn(
                name: "PartidosJugados",
                table: "JugadoresTorneos");

            migrationBuilder.DropColumn(
                name: "PartidosPerdidos",
                table: "JugadoresTorneos");

            migrationBuilder.RenameColumn(
                name: "Puntos",
                table: "JugadoresTorneos",
                newName: "Puntaje");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JugadoresTorneos",
                table: "JugadoresTorneos",
                columns: new[] { "UserId", "TorneoId" });
        }
    }
}
