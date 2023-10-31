using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class func_rm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Funcion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Funcion",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeliculaRefId = table.Column<int>(type: "int", nullable: true),
                    SalaRefId = table.Column<int>(type: "int", nullable: true),
                    TarifaRefId = table.Column<int>(type: "int", nullable: true),
                    FechaHoraFuncion = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Subtitulada = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Funcion_Pelicula_PeliculaRefId",
                        column: x => x.PeliculaRefId,
                        principalTable: "Pelicula",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Funcion_Sala_SalaRefId",
                        column: x => x.SalaRefId,
                        principalTable: "Sala",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Funcion_Tarifa_TarifaRefId",
                        column: x => x.TarifaRefId,
                        principalTable: "Tarifa",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Funcion_PeliculaRefId",
                table: "Funcion",
                column: "PeliculaRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcion_SalaRefId",
                table: "Funcion",
                column: "SalaRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcion_TarifaRefId",
                table: "Funcion",
                column: "TarifaRefId");
        }
    }
}
