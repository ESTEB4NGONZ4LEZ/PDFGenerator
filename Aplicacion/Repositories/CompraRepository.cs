
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repositories
{
    public class CompraRepository : GenericRepository<Compra>, ICompra
    {
        public CompraRepository(MainContext context) : base(context)
        {
        }

        public Task<List<ReporteVenta>> ReporteVentaPorMes(int mes)
        {
            var reporteCompras = _context.Compras
                                         .Join
                                         (
                                            _context.Clientes,
                                            compra => compra.IdCliente,
                                            cliente => cliente.Id,
                                            (compra, cliente) => new { compra, cliente }
                                         )
                                         .Join
                                         (
                                            _context.Productos,
                                            combined => combined.compra.IdProducto,
                                            producto => producto.Id,
                                            (combined, producto) => new ReporteVenta
                                            {
                                                Nombre = combined.cliente.Nombres + " " + combined.cliente.Apellidos,
                                                Cedula = combined.cliente.NroCedula,
                                                ReferenciaProducto = producto.Referencia,
                                                Producto = producto.Nombre,
                                                Fecha = combined.compra.Fecha,
                                                Cantidad = combined.compra.Cantidad,
                                                Total = combined.compra.Cantidad * producto.PrecioVenta
                                            })
                                         .Where(x => x.Fecha.Month == mes)
                                         .OrderBy(x => x.Fecha)
                                         .ToListAsync();

                                    return reporteCompras;
        }
    }
}