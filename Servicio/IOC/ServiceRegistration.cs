using Microsoft.Extensions.DependencyInjection;
using Servicio.Interface;
using Servicio.Services;
using Servicio.Services.Account;
using Servicio.Services.Service;

namespace Servicio.IOC
{
    public static class ServiceRegistration
    {
        public static void AddServicesPlayer(this IServiceCollection services)
        {
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IFotoService, FotoService>();
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
