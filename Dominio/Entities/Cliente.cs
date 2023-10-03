
namespace Dominio.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NroCedula { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public ICollection<Compra> Compras { get; set; }
    }
}