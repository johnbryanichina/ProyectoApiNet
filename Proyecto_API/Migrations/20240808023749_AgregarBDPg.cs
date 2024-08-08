using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProyectoAPI.Migrations
{
    /// <inheritdoc />
    public partial class AgregarBDPg : Migration
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
                    imagenUrl = table.Column<string>(type: "text", nullable: false),
                    fechaCreacion = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    fechaActualizacion = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instrumentos", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "instrumentos",
                columns: new[] { "id", "cantidad", "descripcion", "fechaActualizacion", "fechaCreacion", "imagenUrl", "nombre", "precio" },
                values: new object[,]
                {
                    { 1, 10, "Guitarra color café con cuerdas de metal", new DateTimeOffset(new DateTime(2024, 8, 7, 21, 37, 49, 796, DateTimeKind.Unspecified).AddTicks(6573), new TimeSpan(0, -5, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 8, 2, 37, 49, 796, DateTimeKind.Unspecified).AddTicks(6571), new TimeSpan(0, 0, 0, 0, 0)), "", "Guitarra", 5.0 },
                    { 2, 5, "Saxofon alto color negro Yamaha yas 23", new DateTimeOffset(new DateTime(2024, 8, 7, 21, 37, 49, 796, DateTimeKind.Unspecified).AddTicks(6602), new TimeSpan(0, -5, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 7, 21, 37, 49, 796, DateTimeKind.Unspecified).AddTicks(6601), new TimeSpan(0, -5, 0, 0, 0)), "", "Saxofon", 10.5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "instrumentos");
        }
    }
}
