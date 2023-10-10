using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class CineUTN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CondicionPago",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CondicionPago", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sonido",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sonido", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Subtitulo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subtitulo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tipo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ListaPrecio",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaHasta = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CondicionPagoRefId = table.Column<int>(type: "int", nullable: true),
                    Precio = table.Column<decimal>(type: "money", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaPrecio", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ListaPrecio_CondicionPago_CondicionPagoRefId",
                        column: x => x.CondicionPagoRefId,
                        principalTable: "CondicionPago",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Pelicula",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ImagemPelicula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duracion = table.Column<int>(type: "int", nullable: true),
                    Clasificacion = table.Column<int>(type: "int", nullable: true),
                    GeneroRefId = table.Column<int>(type: "int", nullable: true),
                    TipoRefId = table.Column<int>(type: "int", nullable: true),
                    SubtituloRefId = table.Column<int>(type: "int", nullable: true),
                    FechaEstreno = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pelicula", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pelicula_Genero_GeneroRefId",
                        column: x => x.GeneroRefId,
                        principalTable: "Genero",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Pelicula_Subtitulo_SubtituloRefId",
                        column: x => x.SubtituloRefId,
                        principalTable: "Subtitulo",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Pelicula_Tipo_TipoRefId",
                        column: x => x.TipoRefId,
                        principalTable: "Tipo",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoRefId = table.Column<int>(type: "int", nullable: true),
                    SonidoRefId = table.Column<int>(type: "int", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sala_Sonido_SonidoRefId",
                        column: x => x.SonidoRefId,
                        principalTable: "Sonido",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Sala_Tipo_TipoRefId",
                        column: x => x.TipoRefId,
                        principalTable: "Tipo",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Tarifa",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PorcentajeDescuento = table.Column<int>(type: "int", nullable: true),
                    ListaPrecioRefId = table.Column<int>(type: "int", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarifa", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tarifa_ListaPrecio_ListaPrecioRefId",
                        column: x => x.ListaPrecioRefId,
                        principalTable: "ListaPrecio",
                        principalColumn: "ID");
                });

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
                    Subtitulada = table.Column<bool>(type: "bit", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_ListaPrecio_CondicionPagoRefId",
                table: "ListaPrecio",
                column: "CondicionPagoRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Pelicula_GeneroRefId",
                table: "Pelicula",
                column: "GeneroRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Pelicula_SubtituloRefId",
                table: "Pelicula",
                column: "SubtituloRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Pelicula_TipoRefId",
                table: "Pelicula",
                column: "TipoRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Sala_SonidoRefId",
                table: "Sala",
                column: "SonidoRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Sala_TipoRefId",
                table: "Sala",
                column: "TipoRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarifa_ListaPrecioRefId",
                table: "Tarifa",
                column: "ListaPrecioRefId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Funcion");

            migrationBuilder.DropTable(
                name: "Pelicula");

            migrationBuilder.DropTable(
                name: "Sala");

            migrationBuilder.DropTable(
                name: "Tarifa");

            migrationBuilder.DropTable(
                name: "Genero");

            migrationBuilder.DropTable(
                name: "Subtitulo");

            migrationBuilder.DropTable(
                name: "Sonido");

            migrationBuilder.DropTable(
                name: "Tipo");

            migrationBuilder.DropTable(
                name: "ListaPrecio");

            migrationBuilder.DropTable(
                name: "CondicionPago");
        }
    }
}
