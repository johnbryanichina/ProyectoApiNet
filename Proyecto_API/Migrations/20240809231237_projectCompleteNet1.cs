using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoAPI.Migrations
{
    /// <inheritdoc />
    public partial class projectCompleteNet1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_numeroInstrumentos_instrumentos_instrumento_id",
                table: "numeroInstrumentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_numeroInstrumentos",
                table: "numeroInstrumentos");

            migrationBuilder.RenameTable(
                name: "numeroInstrumentos",
                newName: "numero_instrumentos");

            migrationBuilder.RenameIndex(
                name: "IX_numeroInstrumentos_instrumento_id",
                table: "numero_instrumentos",
                newName: "IX_numero_instrumentos_instrumento_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_numero_instrumentos",
                table: "numero_instrumentos",
                column: "instrumento_no");

            migrationBuilder.UpdateData(
                table: "instrumentos",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "fecha_actualizacion", "fecha_creacion", "imagen_url" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 8, 9, 23, 12, 37, 65, DateTimeKind.Unspecified).AddTicks(6209), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 9, 23, 12, 37, 65, DateTimeKind.Unspecified).AddTicks(6204), new TimeSpan(0, 0, 0, 0, 0)), "" });

            migrationBuilder.UpdateData(
                table: "instrumentos",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "fecha_actualizacion", "fecha_creacion", "imagen_url" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 8, 9, 23, 12, 37, 65, DateTimeKind.Unspecified).AddTicks(6211), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 9, 23, 12, 37, 65, DateTimeKind.Unspecified).AddTicks(6211), new TimeSpan(0, 0, 0, 0, 0)), "" });

            migrationBuilder.AddForeignKey(
                name: "FK_numero_instrumentos_instrumentos_instrumento_id",
                table: "numero_instrumentos",
                column: "instrumento_id",
                principalTable: "instrumentos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_numero_instrumentos_instrumentos_instrumento_id",
                table: "numero_instrumentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_numero_instrumentos",
                table: "numero_instrumentos");

            migrationBuilder.RenameTable(
                name: "numero_instrumentos",
                newName: "numeroInstrumentos");

            migrationBuilder.RenameIndex(
                name: "IX_numero_instrumentos_instrumento_id",
                table: "numeroInstrumentos",
                newName: "IX_numeroInstrumentos_instrumento_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_numeroInstrumentos",
                table: "numeroInstrumentos",
                column: "instrumento_no");

            migrationBuilder.UpdateData(
                table: "instrumentos",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "fecha_actualizacion", "fecha_creacion", "imagen_url" },
                values: new object[] { new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null });

            migrationBuilder.UpdateData(
                table: "instrumentos",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "fecha_actualizacion", "fecha_creacion", "imagen_url" },
                values: new object[] { new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null });

            migrationBuilder.AddForeignKey(
                name: "FK_numeroInstrumentos_instrumentos_instrumento_id",
                table: "numeroInstrumentos",
                column: "instrumento_id",
                principalTable: "instrumentos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
