
using System.Xml.Linq;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repositories
{
    public class ProductoRepository : GenericRepository<Producto>, IProducto
    {
        public ProductoRepository(MainContext context) : base(context)
        {
        }

        public async Task<List<Producto>> GetReporteProductos(int mes)
        {
            return await _context.Productos
                                 .Where(x => x.Fecha.Month == mes)
                                 .OrderBy(x => x.Fecha)
                                 .ToListAsync();
        }
    }
}