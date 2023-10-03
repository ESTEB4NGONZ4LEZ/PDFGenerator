
using API.Dto.Compra;

namespace API.Dto.Producto
{
    public class ProductoxCompraDto
    {
        public int Id { get; set; }
        public string Referencia { get; set; }
        public string Nombre { get; set; }
        public DateOnly Fecha { get; set; }
        public int Stock { get; set; }
        public decimal PrecioProveedor { get; set; }
        public decimal PrecioVenta { get; set; }
        public List<CompraDto> Compras { get; set; }
    }
}