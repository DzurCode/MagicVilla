using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Cost", "CreationDate", "Description", "ImageUrl", "MetrosCuadrados", "Name", "Ocupantes", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "", 200.0, new DateTime(2023, 5, 2, 16, 4, 40, 709, DateTimeKind.Local).AddTicks(8849), "Detalle de la villa.......", "", 80, "Villa Real", 5, new DateTime(2023, 5, 2, 16, 4, 40, 709, DateTimeKind.Local).AddTicks(8861) },
                    { 2, "", 250.0, new DateTime(2023, 5, 2, 16, 4, 40, 709, DateTimeKind.Local).AddTicks(8864), "Detalle de la villa.......", "", 50, "Premium Vista a la Piscina", 4, new DateTime(2023, 5, 2, 16, 4, 40, 709, DateTimeKind.Local).AddTicks(8865) },
                    { 3, "", 100.0, new DateTime(2023, 5, 2, 16, 4, 40, 709, DateTimeKind.Local).AddTicks(8867), "Detalle de la villa.......", "", 20, "Villa Real La morena", 2, new DateTime(2023, 5, 2, 16, 4, 40, 709, DateTimeKind.Local).AddTicks(8868) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
