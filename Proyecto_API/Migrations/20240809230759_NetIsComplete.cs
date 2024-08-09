using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProyectoAPI.Migrations
{
    /// <inheritdoc />
    public partial class NetIsComplete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "instrumentos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    precio = table.Column<double>(type: "double precision", nullable: false),
                    cantidad = table.Column<int>(type: "integer", nullable: false),
                    imagenurl = table.Column<string>(name: "imagen_url", type: "text", nullable: false),
                    fechacreacion = table.Column<DateTimeOffset>(name: "fecha_creacion", type: "timestamp with time zone", nullable: false),
                    fechaactualizacion = table.Column<DateTimeOffset>(name: "fecha_actualizacion", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instrumentos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "numeroInstrumentos",
                columns: table => new
                {
                    instrumentono = table.Column<int>(name: "instrumento_no", type: "integer", nullable: false),
                    instrumentoid = table.Column<int>(name: "instrumento_id", type: "integer", nullable: false),
                    descripcioninstrumento = table.Column<string>(name: "descripcion_instrumento", type: "text", nullable: false),
                    fechacreacion = table.Column<DateTimeOffset>(name: "fecha_creacion", type: "timestamp with time zone", nullable: false),
                    fechaactualizacion = table.Column<DateTimeOffset>(name: "fecha_actualizacion", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_numeroInstrumentos", x => x.instrumentono);
                    table.ForeignKey(
                        name: "FK_numeroInstrumentos_instrumentos_instrumento_id",
                        column: x => x.instrumentoid,
                        principalTable: "instrumentos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "instrumentos",
                columns: new[] { "id", "cantidad", "descripcion", "fecha_actualizacion", "fecha_creacion", "imagen_url", "nombre", "precio" },
                values: new object[,]
                {
                    { 1, 10, "Guitarra color café con cuerdas de metal", new DateTimeOffset(new DateTime(2024, 8, 9, 23, 7, 59, 304, DateTimeKind.Unspecified).AddTicks(7392), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 9, 23, 7, 59, 304, DateTimeKind.Unspecified).AddTicks(7391), new TimeSpan(0, 0, 0, 0, 0)), "", "Guitarra", 5.0 },
                    { 2, 5, "Saxofon alto color negro Yamaha yas 23", new DateTimeOffset(new DateTime(2024, 8, 9, 23, 7, 59, 304, DateTimeKind.Unspecified).AddTicks(7394), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 9, 23, 7, 59, 304, DateTimeKind.Unspecified).AddTicks(7394), new TimeSpan(0, 0, 0, 0, 0)), "", "Saxofon", 10.5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_numeroInstrumentos_instrumento_id",
                table: "numeroInstrumentos",
                column: "instrumento_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "numeroInstrumentos");

            migrationBuilder.DropTable(
                name: "instrumentos");
        }
    }
}
