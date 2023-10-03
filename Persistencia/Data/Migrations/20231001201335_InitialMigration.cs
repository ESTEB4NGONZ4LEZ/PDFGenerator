using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistencia.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombres = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Apellidos = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NroCedula = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefono = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cliente", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "producto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Referencia = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    PrecioCompra = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    PrecioVenta = table.Column<decimal>(type: "decimal(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_producto", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "compra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_compra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_compra_cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_compra_producto_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "cliente",
                columns: new[] { "Id", "Apellidos", "Email", "Nombres", "NroCedula", "Telefono" },
                values: new object[,]
                {
                    { 1, "Pérez", "juan.perez@example.com", "Juan", "1234567890", "1234567890" },
                    { 2, "González", "maria.gonzalez@example.com", "María", "0987654321", "0987654321" },
                    { 3, "López", "carlos.lopez@example.com", "Carlos", "5678901234", "5678901234" },
                    { 4, "Rodríguez", "laura.rodriguez@example.com", "Laura", "9876543210", "9876543210" },
                    { 5, "Martínez", "pedro.martinez@example.com", "Pedro", "5432109876", "5432109876" },
                    { 6, "Sánchez", "ana.sanchez@example.com", "Ana", "1230987654", "1230987654" },
                    { 7, "Torres", "luis.torres@example.com", "Luis", "8765432109", "8765432109" },
                    { 8, "Díaz", "sofia.diaz@example.com", "Sofía", "6789012345", "6789012345" },
                    { 9, "Gómez", "javier.gomez@example.com", "Javier", "3456789012", "3456789012" },
                    { 10, "Luna", "marcela.luna@example.com", "Marcela", "2109876543", "2109876543" }
                });

            migrationBuilder.InsertData(
                table: "producto",
                columns: new[] { "Id", "Fecha", "Nombre", "PrecioCompra", "PrecioVenta", "Referencia", "Stock" },
                values: new object[,]
                {
                    { 1, new DateOnly(2023, 10, 1), "Producto A", 10.0m, 15.0m, "ABC123", 100 },
                    { 2, new DateOnly(2023, 10, 1), "Producto B", 12.0m, 18.0m, "DEF456", 200 },
                    { 3, new DateOnly(2023, 10, 1), "Producto C", 8.0m, 14.0m, "GHI789", 50 },
                    { 4, new DateOnly(2023, 10, 1), "Producto D", 9.0m, 16.0m, "JKL012", 150 },
                    { 5, new DateOnly(2023, 10, 1), "Producto E", 11.0m, 20.0m, "MNO345", 250 },
                    { 6, new DateOnly(2023, 10, 1), "Producto F", 7.0m, 12.0m, "PQR678", 30 },
                    { 7, new DateOnly(2023, 10, 1), "Producto G", 14.0m, 22.0m, "STU901", 80 },
                    { 8, new DateOnly(2023, 10, 1), "Producto H", 10.5m, 19.0m, "VWX234", 120 },
                    { 9, new DateOnly(2023, 10, 1), "Producto I", 6.0m, 13.0m, "YZA567", 40 },
                    { 10, new DateOnly(2023, 10, 1), "Producto J", 8.0m, 15.0m, "BCD890", 60 }
                });

            migrationBuilder.InsertData(
                table: "compra",
                columns: new[] { "Id", "Cantidad", "Fecha", "IdCliente", "IdProducto" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, 2, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2 },
                    { 3, 4, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3 },
                    { 4, 5, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4 },
                    { 5, 2, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 5 },
                    { 6, 3, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 6 },
                    { 7, 1, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 7 },
                    { 8, 4, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 8 },
                    { 9, 2, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 9 },
                    { 10, 3, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_compra_IdCliente",
                table: "compra",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_compra_IdProducto",
                table: "compra",
                column: "IdProducto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "compra");

            migrationBuilder.DropTable(
                name: "cliente");

            migrationBuilder.DropTable(
                name: "producto");
        }
    }
}
