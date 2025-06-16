using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pokemon.Migrations
{
    /// <inheritdoc />
    public partial class AddJugadorModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JugadorId",
                table: "JugadoresTorneos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Jugadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Apodo = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jugadores", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JugadoresTorneos_JugadorId",
                table: "JugadoresTorneos",
                column: "JugadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_JugadoresTorneos_Jugadores_JugadorId",
                table: "JugadoresTorneos",
                column: "JugadorId",
                principalTable: "Jugadores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JugadoresTorneos_Jugadores_JugadorId",
                table: "JugadoresTorneos");

            migrationBuilder.DropTable(
                name: "Jugadores");

            migrationBuilder.DropIndex(
                name: "IX_JugadoresTorneos_JugadorId",
                table: "JugadoresTorneos");

            migrationBuilder.DropColumn(
                name: "JugadorId",
                table: "JugadoresTorneos");
        }
    }
}
