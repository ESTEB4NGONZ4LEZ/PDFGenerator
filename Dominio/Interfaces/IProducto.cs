
using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface IProducto : IGeneric<Producto>
    {
        Task<List<Producto>> GetReporteProductos(int mes);
    }
}