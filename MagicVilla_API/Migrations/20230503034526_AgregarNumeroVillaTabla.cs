using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AgregarNumeroVillaTabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumeroVillas",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    VillaId = table.Column<int>(type: "int", nullable: false),
                    DetalleEspecial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumeroVillas", x => x.VillaNo);
                    table.ForeignKey(
                        name: "FK_NumeroVillas_Villas_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 5, 2, 22, 45, 26, 202, DateTimeKind.Local).AddTicks(6824), new DateTime(2023, 5, 2, 22, 45, 26, 202, DateTimeKind.Local).AddTicks(6836) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 5, 2, 22, 45, 26, 202, DateTimeKind.Local).AddTicks(6840), new DateTime(2023, 5, 2, 22, 45, 26, 202, DateTimeKind.Local).AddTicks(6840) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreationDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 5, 2, 22, 45, 26, 202, DateTimeKind.Local).AddTicks(6843), new DateTime(2023, 5, 2, 22, 45, 26, 202, DateTimeKind.Local).AddTicks(6844) });

            migrationBuilder.CreateIndex(
                name: "IX_NumeroVillas_VillaId",
                table: "NumeroVillas",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumeroVillas");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 5, 2, 16, 4, 40, 709, DateTimeKind.Local).AddTicks(8849), new DateTime(2023, 5, 2, 16, 4, 40, 709, DateTimeKind.Local).AddTicks(8861) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 5, 2, 16, 4, 40, 709, DateTimeKind.Local).AddTicks(8864), new DateTime(2023, 5, 2, 16, 4, 40, 709, DateTimeKind.Local).AddTicks(8865) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreationDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 5, 2, 16, 4, 40, 709, DateTimeKind.Local).AddTicks(8867), new DateTime(2023, 5, 2, 16, 4, 40, 709, DateTimeKind.Local).AddTicks(8868) });
        }
    }
}
