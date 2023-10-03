
namespace Dominio.Entities
{
    public class ReporteVenta
    {
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public string ReferenciaProducto { get; set; }
        public string Producto { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
    }
}