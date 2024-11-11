using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppRezeptSammlungMVC.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lebensmittel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bezeichnung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kategorie = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lebensmittel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rezept",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Beschreibung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bezeichnung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zubereitung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zubereitungszeit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezept", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zutat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RezeptId = table.Column<int>(type: "int", nullable: false),
                    LebensmittelId = table.Column<int>(type: "int", nullable: false),
                    Menge = table.Column<int>(type: "int", nullable: false),
                    Einheit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zutat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zutat_Lebensmittel_LebensmittelId",
                        column: x => x.LebensmittelId,
                        principalTable: "Lebensmittel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zutat_Rezept_RezeptId",
                        column: x => x.RezeptId,
                        principalTable: "Rezept",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zutat_LebensmittelId",
                table: "Zutat",
                column: "LebensmittelId");

            migrationBuilder.CreateIndex(
                name: "IX_Zutat_RezeptId",
                table: "Zutat",
                column: "RezeptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zutat");

            migrationBuilder.DropTable(
                name: "Lebensmittel");

            migrationBuilder.DropTable(
                name: "Rezept");
        }
    }
}
