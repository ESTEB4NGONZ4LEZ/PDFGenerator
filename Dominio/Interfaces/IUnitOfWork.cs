
namespace Dominio.Interfaces
{
    public interface IUnitOfWork
    {
        ICliente Clientes { get; }
        ICompra Compras { get; }
        IProducto Productos { get; }
        Task<int> SaveAsync();
    }
}