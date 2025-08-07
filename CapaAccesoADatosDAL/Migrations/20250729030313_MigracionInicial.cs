using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapaAccesoADatosDAL.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Agregar nuevas columnas a la tabla ListaEvento existente
            migrationBuilder.AddColumn<string>(
                name: "Banner",
                table: "ListaEvento",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioBase",
                table: "ListaEvento",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Asientos",
                columns: table => new
                {
                    AsientoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventoID = table.Column<int>(type: "int", nullable: false),
                    Fila = table.Column<int>(type: "int", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    EstaOcupado = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "SYSUTCDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asientos", x => x.AsientoID);
                    table.ForeignKey(
                        name: "FK_Asientos_ListaEvento_EventoID",
                        column: x => x.EventoID,
                        principalTable: "ListaEvento",
                        principalColumn: "EventoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Boletos",
                columns: table => new
                {
                    BoletoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventoID = table.Column<int>(type: "int", nullable: false),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    AsientoID = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    FechaCompra = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    Estado = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boletos", x => x.BoletoID);
                    table.ForeignKey(
                        name: "FK_Boletos_Asientos_AsientoID",
                        column: x => x.AsientoID,
                        principalTable: "Asientos",
                        principalColumn: "AsientoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Boletos_ListaEvento_EventoID",
                        column: x => x.EventoID,
                        principalTable: "ListaEvento",
                        principalColumn: "EventoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Boletos_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asientos_Evento_Fila_Numero",
                table: "Asientos",
                columns: new[] { "EventoID", "Fila", "Numero" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boletos_AsientoID",
                table: "Boletos",
                column: "AsientoID");

            migrationBuilder.CreateIndex(
                name: "IX_Boletos_EventoID",
                table: "Boletos",
                column: "EventoID");

            migrationBuilder.CreateIndex(
                name: "IX_Boletos_UsuarioID",
                table: "Boletos",
                column: "UsuarioID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boletos");

            migrationBuilder.DropTable(
                name: "Asientos");

            migrationBuilder.DropColumn(
                name: "Banner",
                table: "ListaEvento");

            migrationBuilder.DropColumn(
                name: "PrecioBase",
                table: "ListaEvento");
        }
    }
}
