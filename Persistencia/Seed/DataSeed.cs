
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistencia.Seed
{
    public static class DataSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Cliente>()
                .HasData(
                    new Cliente { Id = 1, Nombres = "Juan", Apellidos = "Pérez", NroCedula = "1234567890", Telefono = "1234567890", Email = "juan.perez@example.com" },
                    new Cliente { Id = 2, Nombres = "María", Apellidos = "González", NroCedula = "0987654321", Telefono = "0987654321", Email = "maria.gonzalez@example.com" },
                    new Cliente { Id = 3, Nombres = "Carlos", Apellidos = "López", NroCedula = "5678901234", Telefono = "5678901234", Email = "carlos.lopez@example.com" },
                    new Cliente { Id = 4, Nombres = "Laura", Apellidos = "Rodríguez", NroCedula = "9876543210", Telefono = "9876543210", Email = "laura.rodriguez@example.com" },
                    new Cliente { Id = 5, Nombres = "Pedro", Apellidos = "Martínez", NroCedula = "5432109876", Telefono = "5432109876", Email = "pedro.martinez@example.com" },
                    new Cliente { Id = 6, Nombres = "Ana", Apellidos = "Sánchez", NroCedula = "1230987654", Telefono = "1230987654", Email = "ana.sanchez@example.com" },
                    new Cliente { Id = 7, Nombres = "Luis", Apellidos = "Torres", NroCedula = "8765432109", Telefono = "8765432109", Email = "luis.torres@example.com" },
                    new Cliente { Id = 8, Nombres = "Sofía", Apellidos = "Díaz", NroCedula = "6789012345", Telefono = "6789012345", Email = "sofia.diaz@example.com" },
                    new Cliente { Id = 9, Nombres = "Javier", Apellidos = "Gómez", NroCedula = "3456789012", Telefono = "3456789012", Email = "javier.gomez@example.com" },
                    new Cliente { Id = 10, Nombres = "Esteban", Apellidos = "Gonzalez", NroCedula = "2109876543", Telefono = "2109876543", Email = "fabestebece@gmail.com" }
                );

            modelBuilder.Entity<Compra>()
                .HasData(
                    new Compra { Id = 1, IdCliente = 1, IdProducto = 1, Fecha = new DateTime(2023, 10, 1), Cantidad = 3 },
                    new Compra { Id = 2, IdCliente = 2, IdProducto = 2, Fecha = new DateTime(2023, 10, 1), Cantidad = 2 },
                    new Compra { Id = 3, IdCliente = 3, IdProducto = 3, Fecha = new DateTime(2023, 10, 1), Cantidad = 4 },
                    new Compra { Id = 4, IdCliente = 4, IdProducto = 4, Fecha = new DateTime(2023, 10, 1), Cantidad = 5 },
                    new Compra { Id = 5, IdCliente = 5, IdProducto = 5, Fecha = new DateTime(2023, 10, 1), Cantidad = 2 },
                    new Compra { Id = 6, IdCliente = 6, IdProducto = 6, Fecha = new DateTime(2023, 10, 1), Cantidad = 3 },
                    new Compra { Id = 7, IdCliente = 7, IdProducto = 7, Fecha = new DateTime(2023, 10, 1), Cantidad = 1 },
                    new Compra { Id = 8, IdCliente = 8, IdProducto = 8, Fecha = new DateTime(2023, 10, 1), Cantidad = 4 },
                    new Compra { Id = 9, IdCliente = 9, IdProducto = 9, Fecha = new DateTime(2023, 10, 1), Cantidad = 2 },
                    new Compra { Id = 10, IdCliente = 10, IdProducto = 10, Fecha = new DateTime(2023, 10, 1), Cantidad = 3 }
                );

            modelBuilder.Entity<Producto>()
                .HasData(
                    new Producto { Id = 1, Nombre = "Producto A", Fecha = new DateOnly(2023, 10, 1), Stock = 100, PrecioCompra = 10.0M, PrecioVenta = 15.0M, Referencia = "ABC123" },
                    new Producto { Id = 2, Nombre = "Producto B", Fecha = new DateOnly(2023, 10, 1), Stock = 200, PrecioCompra = 12.0M, PrecioVenta = 18.0M, Referencia = "DEF456" },
                    new Producto { Id = 3, Nombre = "Producto C", Fecha = new DateOnly(2023, 10, 1), Stock = 50, PrecioCompra = 8.0M, PrecioVenta = 14.0M, Referencia = "GHI789" },
                    new Producto { Id = 4, Nombre = "Producto D", Fecha = new DateOnly(2023, 10, 1), Stock = 150, PrecioCompra = 9.0M, PrecioVenta = 16.0M, Referencia = "JKL012" },
                    new Producto { Id = 5, Nombre = "Producto E", Fecha = new DateOnly(2023, 10, 1), Stock = 250, PrecioCompra = 11.0M, PrecioVenta = 20.0M, Referencia = "MNO345" },
                    new Producto { Id = 6, Nombre = "Producto F", Fecha = new DateOnly(2023, 10, 1), Stock = 30, PrecioCompra = 7.0M, PrecioVenta = 12.0M, Referencia = "PQR678" },
                    new Producto { Id = 7, Nombre = "Producto G", Fecha = new DateOnly(2023, 10, 1), Stock = 80, PrecioCompra = 14.0M, PrecioVenta = 22.0M, Referencia = "STU901" },
                    new Producto { Id = 8, Nombre = "Producto H", Fecha = new DateOnly(2023, 10, 1), Stock = 120, PrecioCompra = 10.5M, PrecioVenta = 19.0M, Referencia = "VWX234" },
                    new Producto { Id = 9, Nombre = "Producto I", Fecha = new DateOnly(2023, 10, 1), Stock = 40, PrecioCompra = 6.0M, PrecioVenta = 13.0M, Referencia = "YZA567" },
                    new Producto { Id = 10, Nombre = "Producto J", Fecha = new DateOnly(2023, 10, 1), Stock = 60, PrecioCompra = 8.0M, PrecioVenta = 15.0M, Referencia = "BCD890" }
                );
        }
    }
}