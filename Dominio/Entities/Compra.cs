
namespace Dominio.Entities
{
    public class Compra
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public Cliente Cliente { get; set; }
        public Producto Producto { get; set; }
    }
}