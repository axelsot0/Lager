using Entidades.Dtos.Email;
using Entidades.Settings;

namespace Servicio.Interface
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}