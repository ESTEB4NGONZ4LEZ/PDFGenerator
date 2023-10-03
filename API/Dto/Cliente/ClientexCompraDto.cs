
using API.Dto.Compra;

namespace API.Dto.Cliente
{
    public class ClientexCompraDto
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NroCedula { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public List<CompraDto> Compras { get; set; }
    }
}