using Entidades.Email;
using Entidades.Settings;

namespace Servicio.Interface
{
    public interface IEmailService
    {
        MailSettings MailSettings { get; }

        Task SendAsync(EmailRequest request);
    }
}