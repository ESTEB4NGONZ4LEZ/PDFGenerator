
namespace Dominio.Entities
{
    public class Producto
    {
        public int Id { get; set; }
        public string Referencia { get; set; }
        public string Nombre { get; set; }
        public DateOnly Fecha { get; set; }
        public int Stock { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public ICollection<Compra> Compras { get; set; }
    }
}