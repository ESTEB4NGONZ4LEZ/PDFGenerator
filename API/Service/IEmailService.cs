

using API.Dto.Compra;

namespace API.Service
{
    public interface IEmailService
    {
        void SendEmail(CompraDto data);
    }
}