
using Aplicacion.Repositories;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MainContext _context;
        public UnitOfWork(MainContext context)
        {
            _context = context;
        }
        private ClienteRepository _clientes;
        private CompraRepository _compras;
        private ProductoRepository _productos;
        public ICliente Clientes
        {
            get
            {
                _clientes ??= new ClienteRepository(_context);
                return _clientes;
            }
        }

        public ICompra Compras
        {
            get
            {
                _compras ??= new CompraRepository(_context);
                return _compras;
            }
        }

        public IProducto Productos
        {
            get
            {
                _productos ??= new ProductoRepository(_context);
                return _productos;
            }
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}