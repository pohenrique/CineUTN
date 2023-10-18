using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class add_programacion_funciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "PrecioConDescuento",
            //    table: "Tarifa");

            migrationBuilder.AddColumn<decimal>(
                name: "TarifaPrecio",
                table: "Tarifa",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Programaciones",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaHoraFuncion = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    PeliculaRefId = table.Column<int>(type: "int", nullable: true),
                    SalaRefId = table.Column<int>(type: "int", nullable: true),
                    Tarifa1RefId = table.Column<int>(type: "int", nullable: true),
                    Tarifa2RefId = table.Column<int>(type: "int", nullable: true),
                    Tarifa3RefId = table.Column<int>(type: "int", nullable: true),
                    Tarifa4RefId = table.Column<int>(type: "int", nullable: true),
                    Tarifa5RefId = table.Column<int>(type: "int", nullable: true),
                    Tarifa6RefId = table.Column<int>(type: "int", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programaciones", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Programaciones_Pelicula_PeliculaRefId",
                        column: x => x.PeliculaRefId,
                        principalTable: "Pelicula",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Programaciones_Sala_SalaRefId",
                        column: x => x.SalaRefId,
                        principalTable: "Sala",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Programaciones_Tarifa_Tarifa1RefId",
                        column: x => x.Tarifa1RefId,
                        principalTable: "Tarifa",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Programaciones_Tarifa_Tarifa2RefId",
                        column: x => x.Tarifa2RefId,
                        principalTable: "Tarifa",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Programaciones_Tarifa_Tarifa3RefId",
                        column: x => x.Tarifa3RefId,
                        principalTable: "Tarifa",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Programaciones_Tarifa_Tarifa4RefId",
                        column: x => x.Tarifa4RefId,
                        principalTable: "Tarifa",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Programaciones_Tarifa_Tarifa5RefId",
                        column: x => x.Tarifa5RefId,
                        principalTable: "Tarifa",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Programaciones_Tarifa_Tarifa6RefId",
                        column: x => x.Tarifa6RefId,
                        principalTable: "Tarifa",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Programaciones_PeliculaRefId",
                table: "Programaciones",
                column: "PeliculaRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Programaciones_SalaRefId",
                table: "Programaciones",
                column: "SalaRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Programaciones_Tarifa1RefId",
                table: "Programaciones",
                column: "Tarifa1RefId");

            migrationBuilder.CreateIndex(
                name: "IX_Programaciones_Tarifa2RefId",
                table: "Programaciones",
                column: "Tarifa2RefId");

            migrationBuilder.CreateIndex(
                name: "IX_Programaciones_Tarifa3RefId",
                table: "Programaciones",
                column: "Tarifa3RefId");

            migrationBuilder.CreateIndex(
                name: "IX_Programaciones_Tarifa4RefId",
                table: "Programaciones",
                column: "Tarifa4RefId");

            migrationBuilder.CreateIndex(
                name: "IX_Programaciones_Tarifa5RefId",
                table: "Programaciones",
                column: "Tarifa5RefId");

            migrationBuilder.CreateIndex(
                name: "IX_Programaciones_Tarifa6RefId",
                table: "Programaciones",
                column: "Tarifa6RefId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Programaciones");

            migrationBuilder.DropColumn(
                name: "TarifaPrecio",
                table: "Tarifa");

            //migrationBuilder.AddColumn<decimal>(
            //    name: "PrecioConDescuento",
            //    table: "Tarifa",
            //    type: "decimal(18,2)",
            //    nullable: true);
        }
    }
}
