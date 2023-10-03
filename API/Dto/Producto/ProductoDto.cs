
namespace API.Dto.Producto
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Referencia { get; set; }
        public string Nombre { get; set; }
        public DateOnly Fecha { get; set; }
        public int Stock { get; set; }
        public decimal PrecioProveedor { get; set; }
        public decimal PrecioVenta { get; set; }
    }
}