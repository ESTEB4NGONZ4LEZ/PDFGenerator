
using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface ICompra : IGeneric<Compra>
    {
        Task<List<ReporteVenta>>  ReporteVentaPorMes(int mes);
    }
}