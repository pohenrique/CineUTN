using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class AddPelicula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tipo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            //migrationBuilder.CreateTable(
            //    name: "Genero",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Genero", x => x.ID);
            //    });

            migrationBuilder.CreateTable(
                name: "Peliculas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImagemPelicula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duracion = table.Column<int>(type: "int", nullable: true),
                    Clasificacion = table.Column<int>(type: "int", nullable: true),
                    GeneroRefId = table.Column<int>(type: "int", nullable: true),
                    TipoRefId = table.Column<int>(type: "int", nullable: true),
                    Subtitulada = table.Column<bool>(type: "bit", nullable: true),
                    FechaEstreno = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peliculas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Peliculas_Genero_GeneroRefId",
                        column: x => x.GeneroRefId,
                        principalTable: "Genero",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Peliculas_Tipo_TipoRefId",
                        column: x => x.TipoRefId,
                        principalTable: "Tipo",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Peliculas_GeneroRefId",
                table: "Peliculas",
                column: "GeneroRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Peliculas_TipoRefId",
                table: "Peliculas",
                column: "TipoRefId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Peliculas");

            //migrationBuilder.DropTable(
            //    name: "Genero");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tipo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
