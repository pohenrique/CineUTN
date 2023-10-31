using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class addfunciontarifas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Funcion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaHoraFuncion = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    PeliculaRefId = table.Column<int>(type: "int", nullable: true),
                    SalaRefId = table.Column<int>(type: "int", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcion", x => x.Id);
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
                });

            migrationBuilder.CreateTable(
                name: "FuncionTarifa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TarifaRefId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FuncionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncionTarifa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuncionTarifa_Funcion_FuncionId",
                        column: x => x.FuncionId,
                        principalTable: "Funcion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FuncionTarifa_Tarifa_TarifaRefId",
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
                name: "IX_FuncionTarifa_FuncionId",
                table: "FuncionTarifa",
                column: "FuncionId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionTarifa_TarifaRefId",
                table: "FuncionTarifa",
                column: "TarifaRefId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuncionTarifa");

            migrationBuilder.DropTable(
                name: "Funcion");
        }
    }
}
